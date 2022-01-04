using System;
using System.Windows.Forms;
using System.Threading;
using Microsoft.Xrm.Tooling.Connector;
using XrmToolBox.Extensibility;
using Capgemini.Xrm.CdsDataMigratorLibrary;
using System.Linq;
using Capgemini.Xrm.DataMigration.CrmStore.Config;
using Microsoft.Xrm.Sdk;
using Capgemini.Xrm.CdsDataMigratorLibrary.Helpers;
using Capgemini.Xrm.CdsDataMigratorLibrary.Presenters;
using Capgemini.Xrm.CdsDataMigratorLibrary.Services;
using Capgemini.Xrm.CdsDataMigratorLibrary.UserControls;
using Capgemini.Xrm.CdsDataMigratorLibrary.Extensions;
using Capgemini.Xrm.CdsDataMigratorLibrary.Enums;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.UserControls
{
    public partial class ExportWizard : UserControl, IExportView
    {
        private readonly LoggerService logger;
        private readonly ExportPresenter presenter;
        private readonly IDataMigrationService dataMigrationService;
        private readonly ICrmGenericMigratorFactory migratorFactory;

        public ExportWizard()
        {
            InitializeComponent();

            logger = new LoggerService(textBoxLogs, SynchronizationContext.Current);

            migratorFactory = new CrmGenericMigratorFactory();
            logger = new LoggerService(textBoxLogs, SynchronizationContext.Current);
            dataMigrationService = new DataMigrationService(logger, migratorFactory);
            presenter = new ExportPresenter(this, logger, dataMigrationService);

            logger.LogVerbose($"ExportPresenter {presenter} successfully instatiated!");
            wizardButtons1.OnExecute += WizardButtons1_OnExecute;
            wizardButtons1.OnCustomNextNavigation += WizardButtonsOnNavigateToNextPage;
            wizardButtons1.OnCustomPreviousNavigation += WizardButtonsOnCustomPreviousNavigation;

            FormatCsvSelected = false;
            FormatJsonSelected = true;
            numericUpDownBatchSize.Value = 5000;

            comboBoxLogLevel.PopulateComboBoxLogLevel();
        }

        public void WizardButtonsOnCustomPreviousNavigation(object sender, EventArgs e)
        {
            var wizardButtons = (WizardButtons)sender;

            WizardValidation(wizardButtons.PageContainer.SelectedPage.Name);
            wizardButtons.PageContainer.PreviousPage();
        }

        public void WizardButtonsOnNavigateToNextPage(object sender, EventArgs e)
        {
            var wizardButtons = (WizardButtons)sender;

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

        private void ButtonExportLocationClick(object sender, EventArgs e)
        {
            SelectExportLocationHandler(sender, e);
        }

        private void ButtonExportConfigLocationClick(object sender, EventArgs e)
        {
            SelectExportConfigFileHandler(sender, e);
        }

        private void ButtonSchemaLocationClick(object sender, EventArgs e)
        {
            SelectSchemaFileHandler(sender, e);
        }

        private void ButtonExportDataClick(object sender, EventArgs e)
        {
            ExportDataHandler(sender, e);
        }

        private void WizardButtons1_OnExecute(object sender, EventArgs e)
        {
            textBoxLogs.Clear();
            ExportDataHandler(sender, e);
        }

        private void ButtonTargetConnectionStringClick(object sender, EventArgs e)
        {
            OnConnectionRequested?.Invoke(this, new RequestConnectionEventArgs { ActionName = "SourceConnection", Control = (CdsMigratorPluginControl)Parent });
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

        private void ComboBoxLogLevelSelectedIndexChanged(object sender, EventArgs e)
        {
            logger.LogLevel = (LogLevel)comboBoxLogLevel.SelectedItem;
        }
    }
}