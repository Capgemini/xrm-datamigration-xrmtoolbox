using System;
using System.Windows.Forms;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Views;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Presenters;
using System.Threading;
using Microsoft.Xrm.Tooling.Connector;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Logging;
using XrmToolBox.Extensibility;
using MyXrmToolBoxPlugin3;
using System.Linq;
using Capgemini.Xrm.DataMigration.XrmToolBox.Helpers;
using Capgemini.Xrm.DataMigration.CrmStore.Config;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Services;
using Capgemini.Xrm.DataMigration.XrmToolBox.Services;
using Microsoft.Xrm.Sdk;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.UserControls
{
    public partial class ExportWizard : UserControl, IExportView
    {
        private readonly MessageLogger logger;
        private readonly ExportPresenter presenter;
        private readonly IDataMigrationService dataMigrationService;
        private readonly ICrmGenericMigratorFactory migratorFactory;

        public ExportWizard()
        {
            InitializeComponent();

            logger = new MessageLogger(textBoxLogs, SynchronizationContext.Current);

            migratorFactory = new CrmGenericMigratorFactory();
            logger = new MessageLogger(textBoxLogs, SynchronizationContext.Current);
            dataMigrationService = new DataMigrationService(logger, migratorFactory);
            presenter = new ExportPresenter(this, logger, dataMigrationService);

            logger.LogVerbose($"ExportPresenter {presenter} successfully instatiated!");
            wizardButtons1.OnExecute += WizardButtons1_OnExecute;
            wizardButtons1.OnCustomNextNavigation += WizardButtons1_OnNavigateToNextPage;
            wizardButtons1.OnCustomPreviousNavigation += WizardButtons1_OnCustomPreviousNavigation;

            FormatCsvSelected = false;
            FormatJsonSelected = true;
            numericUpDownBatchSize.Value = 5000;
        }

        private void WizardButtons1_OnCustomPreviousNavigation(object sender, EventArgs e)
        {
            var wizardButtons = (WizardButtons)sender;

            WizardValidation(wizardButtons.PageContainer.SelectedPage.Name);
            wizardButtons.PageContainer.PreviousPage();
        }

        private void WizardButtons1_OnNavigateToNextPage(object sender, EventArgs e)
        {
            var wizardButtons = ((WizardButtons)sender);

            if (WizardValidation(wizardButtons.PageContainer.SelectedPage.Name))
            {
                wizardButtons.PageContainer.NextPage();
            }
        }

        public bool WizardValidation(string selectedPage)
        {
            bool valResults = true;

            if (selectedPage == "exportConfig")
            {
                if (!string.IsNullOrWhiteSpace(ExportConfigFileLocation))
                {
                    valResults = LoadSettingsFromConfig();
                }
            }
            else if (selectedPage == "exportLocation")
            {
                valResults = ValidationHelpers.IsTextControlNotEmpty(labelFolderPathValidation, textBoxExportLocation);
            }
            else if (selectedPage == "executeExport")
            {
                valResults = ValidationHelpers.IsTextControlNotEmpty(labelSchemaLocationFileValidation, textBoxSchemaLocation) &&
                             ValidationHelpers.IsTextControlNotEmpty(labelExportConnectionValidation, labelTargetConnectionString);
            }

            return valResults;
        }

        public event EventHandler<RequestConnectionEventArgs> OnConnectionRequested;

        public event EventHandler SelectExportLocationHandler;

        public event EventHandler SelectExportConfigFileHandler;

        public event EventHandler SelectSchemaFileHandler;

        public event EventHandler ExportDataHandler;

        public bool FormatJsonSelected { get => radioButtonFormatJson.Checked; set => radioButtonFormatJson.Checked = value; }

        public bool FormatCsvSelected { get => radioButtonFormatCsv.Checked; set => radioButtonFormatCsv.Checked = value; }

        public IOrganizationService OrganizationService { get; set; }

        public string SaveExportLocation { get => textBoxExportLocation.Text; set => textBoxExportLocation.Text = value; }

        public string ExportConfigFileLocation { get => textBoxExportConfigLocation.Text; set => textBoxExportConfigLocation.Text = value; }

        public string ExportSchemaFileLocation { get => textBoxSchemaLocation.Text; set => textBoxSchemaLocation.Text = value; }

        public bool ExportInactiveRecordsChecked { get => checkBoxExportInactiveRecords.Checked; set => checkBoxExportInactiveRecords.Checked = value; }

        public bool MinimizeJsonChecked { get => checkBoxMinimize.Checked; set => checkBoxMinimize.Checked = value; }

        public decimal BatchSize
        {
            get => numericUpDownBatchSize.Value;
            set => numericUpDownBatchSize.Value = value;
        }

        private void buttonExportLocation_Click(object sender, EventArgs e)
        {
            SelectExportLocationHandler(sender, e);
        }

        private void buttonExportConfigLocation_Click(object sender, EventArgs e)
        {
            SelectExportConfigFileHandler(sender, e);
        }

        private void buttonSchemaLocation_Click(object sender, EventArgs e)
        {
            SelectSchemaFileHandler(sender, e);
        }

        private void buttonExportData_Click(object sender, EventArgs e)
        {
            ExportDataHandler(sender, e);
        }

        private void WizardButtons1_OnExecute(object sender, EventArgs e)
        {
            textBoxLogs.Clear();
            ExportDataHandler(sender, e);
        }

        private void buttonTargetConnectionString_Click(object sender, EventArgs e)
        {
            OnConnectionRequested?.Invoke(this, new RequestConnectionEventArgs { ActionName = "SourceConnection", Control = (MyPluginControl)Parent });
        }

        public void OnConnectionUpdated(string connectedOrgFriendlyName)
        {
            labelTargetConnectionString.Text = connectedOrgFriendlyName;
        }

        public string ShowFolderBrowserDialog()
        {
            folderBrowserDialogExportLocation.ShowDialog();
            return folderBrowserDialogExportLocation.SelectedPath;
        }

        public string ShowFileDialog()
        {
            openFileDialogExportConfigFile.ShowDialog();
            return openFileDialogExportConfigFile.FileName;
        }

        private bool LoadSettingsFromConfig()
        {
            try
            {
                var config = CrmExporterConfig.GetConfiguration(ExportConfigFileLocation);
                ExportSchemaFileLocation = config.CrmMigrationToolSchemaPaths.FirstOrDefault();
                SaveExportLocation = config.JsonFolderPath;
                BatchSize = config.BatchSize;
                ExportInactiveRecordsChecked = !config.OnlyActiveRecords;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Export Config Error: {ex}");
                return false;
            }

            return true;
        }
    }
}