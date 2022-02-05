using System;
using System.Windows.Forms;
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
using System.Diagnostics.CodeAnalysis;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.UserControls
{
    public partial class ExportWizard : UserControl, IExportView
    {
        public ExportWizard()
        {
            InitializeComponent();

            wizardButtonsExportData.OnExecute += ExportDataAction;
            wizardButtonsExportData.OnCustomNextNavigation += WizardButtonsOnNavigateToNextPage;
            wizardButtonsExportData.OnCustomPreviousNavigation += WizardButtonsOnCustomPreviousNavigation;
            wizardButtonsExportData.OnCancel += ExportDataCancellationAction;

            FormatCsvSelected = false;
            FormatJsonSelected = true;
            numericUpDownBatchSize.Value = 5000;

            comboBoxLogLevel.PopulateComboBoxLogLevel();
        }

        public event EventHandler<RequestConnectionEventArgs> OnConnectionRequested;

        public event EventHandler SelectExportLocationHandler;

        public event EventHandler SelectExportConfigFileHandler;

        public event EventHandler SelectSchemaFileHandler;

        public event EventHandler ExportDataHandler;

        public event EventHandler CancelHandler;

        public TextBox LogDisplay
        {
            get
            {
                return textBoxLogs;
            }
        }

        public bool FormatJsonSelected { get => radioButtonFormatJson.Checked; set => radioButtonFormatJson.Checked = value; }

        public bool FormatCsvSelected { get => radioButtonFormatCsv.Checked; set => radioButtonFormatCsv.Checked = value; }

        public IOrganizationService OrganizationService { get; set; }

        public ILogManager LoggerService { get; set; }

        public ExportPresenter Presenter { get; set; }

        public IDataMigrationService DataMigrationService { get; set; }

        public ICrmGenericMigratorFactory MigratorFactory { get; set; }

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

        public void OnConnectionUpdated(string connectedOrgFriendlyName)
        {
            labelTargetConnectionString.Text = connectedOrgFriendlyName;
        }

        [ExcludeFromCodeCoverage]
        public string ShowFolderBrowserDialog()
        {
            folderBrowserDialogExportLocation.ShowDialog();
            return folderBrowserDialogExportLocation.SelectedPath;
        }

        [ExcludeFromCodeCoverage]
        public string ShowFileDialog()
        {
            openFileDialogExportConfigFile.ShowDialog();
            return openFileDialogExportConfigFile.FileName;
        }

        private void ButtonExportLocationClick(object sender, EventArgs e)
        {
            SelectExportLocationHandler(sender, e);
        }

        private void ButtonExportConfigLocationClick(object sender, EventArgs e)
        {
            SelectExportConfigFileHandler(sender, e);
        }

        protected void ButtonSchemaLocationClick(object sender, EventArgs e)
        {
            SelectSchemaFileHandler(sender, e);
        }

        protected void ExportDataAction(object sender, EventArgs e)
        {
            textBoxLogs.Clear();
            ExportDataHandler(sender, e);
            wizardButtonsExportData.PerformExecutionCompletedActions();
        }

        protected void ExportDataCancellationAction(object sender, EventArgs e)
        {
            CancelHandler(sender, e);
        }

        protected void ButtonTargetConnectionStringClick(object sender, EventArgs e)
        {
            OnConnectionRequested?.Invoke(this, new RequestConnectionEventArgs { ActionName = "SourceConnection", Control = (CdsMigratorPluginControl)Parent });
        }

        protected void ComboBoxLogLevelSelectedIndexChanged(object sender, EventArgs e)
        {
            if (LoggerService != null)
            {
                LoggerService.LogLevel = (LogLevel)comboBoxLogLevel.SelectedItem;
            }
        }

        private bool LoadSettingsFromConfig()
        {
            try
            {
                LoggerService.LogVerbose("About to load settings from config");

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