using Capgemini.Xrm.DataMigration.Config;
using Capgemini.Xrm.DataMigration.Core;
using Capgemini.Xrm.DataMigration.CrmStore.Config;
using Capgemini.Xrm.DataMigration.Engine;
using Microsoft.Xrm.Sdk;
using MyXrmToolBoxPlugin3;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using XrmToolBox.Extensibility;
using Capgemini.Xrm.CdsDataMigratorLibrary.Helpers;
using Capgemini.Xrm.CdsDataMigratorLibrary.Services;
using Capgemini.Xrm.CdsDataMigratorLibrary.UserControls;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.UserControls
{
    public partial class ImportWizard : UserControl
    {
        private CrmImportConfig importConfig;
        private readonly Capgemini.DataMigration.Core.ILogger logger;
        private readonly IEntityRepositoryService entityRepositoryService;

        public ImportWizard()
        {
            InitializeComponent();

            importConfig = new CrmImportConfig()
            {
                IgnoreStatuses = cbIgnoreStatuses.Checked,
                IgnoreSystemFields = cbIgnoreSystemFields.Checked,
                SaveBatchSize = Convert.ToInt32(nudSavePageSize.Value),
                JsonFolderPath = tbSourceDataLocation.Text,
                FilePrefix = "ExtractedData"
            };

            wizardButtons1.OnExecute += button2_Click;
            logger = new LoggerService(tbLogger, SynchronizationContext.Current);
            entityRepositoryService = new EntityRepositoryService(OrganizationService);
            wizardButtons1.OnCustomNextNavigation += WizardButtons1OnNavigateToNextPage;
        }

        public event EventHandler<RequestConnectionEventArgs> OnConnectionRequested;

        public string TargetConnectionString { get; set; }

        public IOrganizationService OrganizationService { get; set; }

        public void PerformImportAction(string importSchemaFilePath, int maxThreads, bool jsonFormat, Capgemini.DataMigration.Core.ILogger currentLogger, IEntityRepositoryService entityRepositoryService, CrmImportConfig currentImportConfig, CancellationTokenSource tokenSource)
        {
            if (maxThreads > 1)
            {
                currentLogger.LogInfo($"Starting MultiThreaded Processing, using {maxThreads} threads");
                var repos = new List<IEntityRepository>();
                int threadCount = maxThreads;

                while (threadCount > 0)
                {
                    threadCount--;
                    repos.Add(entityRepositoryService.InstantiateEntityRepository(true));
                }

                var fileExporter = new CrmFileDataImporter(currentLogger, repos, currentImportConfig, tokenSource.Token);
                fileExporter.MigrateData();
            }
            else
            {
                currentLogger.LogInfo("Starting Single Threaded processing, you must set up max threads to more than 1");
                var entityRepo = entityRepositoryService.InstantiateEntityRepository(false);

                if (jsonFormat)
                {
                    var fileExporter = new CrmFileDataImporter(currentLogger, entityRepo, currentImportConfig, tokenSource.Token);
                    fileExporter.MigrateData();
                }
                else
                {
                    var schema = CrmSchemaConfiguration.ReadFromFile(importSchemaFilePath);
                    var fileExporter = new CrmFileDataImporterCsv(currentLogger, entityRepo, currentImportConfig, schema, tokenSource.Token);
                    fileExporter.MigrateData();
                }
            }
        }

        public void HandleFileDialogOpen(DialogResult dialogResult)
        {
            if (dialogResult == DialogResult.OK)
            {
                tbSourceDataLocation.Text = folderBrowserDialog1.SelectedPath;
                importConfig.JsonFolderPath = folderBrowserDialog1.SelectedPath;
                stepWizardControl1.Pages[1].AllowNext = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var dialogResult = folderBrowserDialog1.ShowDialog();

            HandleFileDialogOpen(dialogResult);
        }

        private void buttonTargetConnectionString_Click(object sender, EventArgs e)
        {
            OnConnectionRequested?.Invoke(this, new RequestConnectionEventArgs { ActionName = "TargetConnection", Control = (MyPluginControl)Parent });
        }

        public void OnConnectionUpdated(string connectedOrgFriendlyName)
        {
            labelTargetConnectionString.Text = connectedOrgFriendlyName;
            stepWizardControl1.Pages[3].AllowNext = true;
        }

        public void WizardNavigation(System.Windows.Forms.Label folderPathValidationLabel, TextBox sourceDataLocationTextBox, AeroWizard.WizardPage selectedPage, AeroWizard.WizardPageContainer pageContainer)
        {
            if (selectedPage.Name == "wizardPage2")
            {
                ValidationHelpers.IsTextControlNotEmpty(folderPathValidationLabel, sourceDataLocationTextBox);

                if (!folderPathValidationLabel.Visible)
                {
                    pageContainer.NextPage();
                }
            }
            else if (!selectedPage.IsFinishPage)
            {
                pageContainer.NextPage();
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            radioButtonCSVFormat.Checked = !radioButtonJsonFormat.Checked;
            groupBox1.Visible = false;
            stepWizardControl1.Pages[0].AllowNext = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            stepWizardControl1.Pages[0].AllowNext = radioButtonCSVFormat.Checked && tbImportSchema.Text != string.Empty;
            radioButtonJsonFormat.Checked = !radioButtonCSVFormat.Checked;
            groupBox1.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            importConfig.JsonFolderPath = tbSourceDataLocation.Text;
            importConfig.IgnoreStatuses = cbIgnoreStatuses.Checked;
            importConfig.IgnoreSystemFields = cbIgnoreSystemFields.Checked;
            importConfig.SaveBatchSize = Convert.ToInt32(nudSavePageSize.Value);

            var tokenSource = new CancellationTokenSource();
            tbLogger.Clear();
            Task.Run(() =>
            {
                PerformImportAction(tbImportSchema.Text, Convert.ToInt32(nudMaxThreads.Value), radioButtonJsonFormat.Checked, logger, entityRepositoryService, importConfig, tokenSource);
            });
        }

        private void btLoadImportConfigFile_Click(object sender, EventArgs e)
        {
            var fd = openFileDialog1.ShowDialog();

            if (fd == DialogResult.OK)
            {
                tbImportConfigFile.Text = openFileDialog1.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var fd = openFileDialog1.ShowDialog();

            if (fd == DialogResult.OK)
            {
                tbImportSchema.Text = openFileDialog1.FileName;
            }
        }

        private void TbImportSchemeTextChanged(object sender, EventArgs e)
        {
            if (radioButtonCSVFormat.Checked)
            {
                stepWizardControl1.Pages[0].AllowNext = true;
            }
        }

        private void WizardButtons1OnNavigateToNextPage(object sender, EventArgs e)
        {
            var wizardButtons = ((WizardButtons)sender);
            WizardNavigation(labelFolderPathValidation, tbSourceDataLocation, wizardButtons.PageContainer.SelectedPage, wizardButtons.PageContainer);
        }

        private void tbSourceDataLocation_TextChanged(object sender, EventArgs e)
        {
            ValidationHelpers.IsTextControlNotEmpty(labelFolderPathValidation, tbSourceDataLocation);
        }

        private void tbImportConfigFile_TextChanged(object sender, EventArgs e)
        {
            importConfig = CrmImportConfig.GetConfiguration(openFileDialog1.FileName);

            cbIgnoreSystemFields.Checked = importConfig.IgnoreSystemFields;
            cbIgnoreStatuses.Checked = importConfig.IgnoreStatuses;
            tbSourceDataLocation.Text = importConfig.JsonFolderPath;
            nudSavePageSize.Value = importConfig.SaveBatchSize;
        }
    }
}