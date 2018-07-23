using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;
using Microsoft.Xrm.Sdk;
using McTools.Xrm.Connection;
using System.Threading;
using Capgemini.Xrm.Audit.Reports;
using Capgemini.Xrm.Audit.Reports.Generators;
using System.IO;
using Capgemini.Xrm.Audit.DataAccess.Repos;

namespace Capgemini.Xrm.SolutionAudit.XrmToolBoxPlugin
{
    public partial class PluginControl : PluginControlBase, IXrmToolBoxPluginControl
    {
        private ConnectionDetail _latestConnection;
        private Task _currentTask;

        public PluginControl()
        {
            InitializeComponent();
        }

        private void PluginControl_ConnectionUpdated(object sender, ConnectionUpdatedEventArgs e)
        {
            _latestConnection = e.ConnectionDetail;
        }

        private async void btGenerate_Click(object sender, EventArgs e)
        {
            var logger = new MessageLogger(this.tbLog, SynchronizationContext.Current);
            btGenerate.Enabled = false;

            try
            {

                if (string.IsNullOrWhiteSpace(tbExportFolderPath.Text) || !Directory.Exists(tbExportFolderPath.Text))
                {
                    MessageBox.Show("Error: You have not selected valid foler path for report output");
                    return;
                }

                if (_latestConnection == null)
                {
                    MessageBox.Show("Error: Connect to CRM first");
                    return;
                }

                if (!(cbJson.Checked || cbExcel.Checked || cbXml.Checked || cbHtml.Checked))
                {
                    MessageBox.Show("Select at least one report type");
                    return;
                }

                var service = _latestConnection.GetCrmServiceClient(true);

                SolutionRepository repo = new SolutionRepository(service, logger);
                CrmAuditor auditor = new CrmAuditor(repo, logger);

                List<string> publishers = new List<string>(tbPublishers.Text.Split(',').ToList());

                _currentTask = Task.Run(() =>
                {
                    logger.Info("Preparing solution audit");
                    var audiResults = auditor.AuditCrmInstance(service.ConnectedOrgUniqueName, publishers);

                    if (cbExcel.Checked)
                    {
                        logger.Info("Exporting Excel file");
                        ExcelReport report = new ExcelReport(tbExportFolderPath.Text);
                        var exResult = report.SaveSolutionAudit(audiResults);
                        exResult.ForEach(p => logger.Info(p));
                    }

                    if (cbHtml.Checked)
                    {
                        logger.Info("Exporting Html file");
                        HtmlReport reportHtml = new HtmlReport(tbExportFolderPath.Text);
                        var exResultHtml = reportHtml.SaveSolutionAudit(audiResults);
                        exResultHtml.ForEach(p => logger.Info(p));
                    }

                    if (cbJson.Checked)
                    {
                        logger.Info("Exporting Json file");
                        JsonReport reportJson = new JsonReport(tbExportFolderPath.Text);
                        var exResultJson = reportJson.SaveSolutionAudit(audiResults);
                        exResultJson.ForEach(p => logger.Info(p));
                    }

                    if (cbXml.Checked)
                    {
                        logger.Info("Exporting Xml file");
                        XmlReport reportXml = new XmlReport(tbExportFolderPath.Text);
                        var exResultXml = reportXml.SaveSolutionAudit(audiResults);
                        exResultXml.ForEach(p => logger.Info(p));
                    }

                    logger.Info("Solution Audit finished");
                });

                await _currentTask;
            }
            catch (Exception ex)
            {
                logger.Error("Export Error:", ex);
            }
            finally
            {
                btGenerate.Enabled = true;
            }
        }

        private void btExportPath_Click(object sender, EventArgs e)
        {
            var result = folderBrowserDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
               tbExportFolderPath.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.Text)
            {
                case "Error":
                    MessageLogger.LogLevel = 0;
                    break;

                case "Warning":
                    MessageLogger.LogLevel = 1;
                    break;

                case "Info":
                    MessageLogger.LogLevel = 2;
                    break;

                case "Verbose":
                    MessageLogger.LogLevel = 3;
                    break;

                default:
                    MessageLogger.LogLevel = 2;
                    break;
            }
        }
    }
}
