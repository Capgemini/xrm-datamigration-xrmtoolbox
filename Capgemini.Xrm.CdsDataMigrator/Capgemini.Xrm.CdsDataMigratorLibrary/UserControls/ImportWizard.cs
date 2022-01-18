using Capgemini.Xrm.DataMigration.Config;
using Capgemini.Xrm.DataMigration.Core;
using Capgemini.Xrm.DataMigration.CrmStore.Config;
using Capgemini.Xrm.DataMigration.Engine;
using Microsoft.Xrm.Sdk;
using Capgemini.Xrm.CdsDataMigratorLibrary;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using XrmToolBox.Extensibility;
using Capgemini.Xrm.CdsDataMigratorLibrary.Helpers;
using Capgemini.Xrm.CdsDataMigratorLibrary.Services;
using Capgemini.Xrm.CdsDataMigratorLibrary.UserControls;
using Capgemini.Xrm.CdsDataMigratorLibrary.Extensions;
using Capgemini.Xrm.CdsDataMigratorLibrary.Enums;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.UserControls
{
    public partial class ImportWizard : UserControl
    {
        private CrmImportConfig importConfig;
        private readonly LoggerService logger;
        private IEntityRepositoryService entityRepositoryService;
        private CancellationTokenSource tokenSource;

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

            wizardButtons1.OnExecute += Button2Click;
            wizardButtons1.OnCancel += WizardButtonsOnCancel;
            logger = new LoggerService(tbLogger, SynchronizationContext.Current);

            wizardButtons1.OnCustomNextNavigation += WizardButtonsOnNavigateToNextPage;

            comboBoxLogLevel.PopulateComboBoxLogLevel();
        }

        public event EventHandler<RequestConnectionEventArgs> OnConnectionRequested;

        public string TargetConnectionString { get; set; }

        public IOrganizationService OrganizationService { get; set; }

        public void PerformImportAction(string importSchemaFilePath, int maxThreads, bool jsonFormat, Capgemini.DataMigration.Core.ILogger currentLogger, IEntityRepositoryService entityRepositoryService, CrmImportConfig currentImportConfig, CancellationTokenSource tokenSource)
        {
            try
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
            catch (Exception ex)
            {
                currentLogger.LogError($"Critical import error, processing stopped: {ex.Message}");
                throw;
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

        private void ButtonClick(object sender, EventArgs e)
        {
            var dialogResult = folderBrowserDialog1.ShowDialog();

            HandleFileDialogOpen(dialogResult);
        }

        private void ButtonTargetConnectionStringClick(object sender, EventArgs e)
        {
            OnConnectionRequested?.Invoke(this, new RequestConnectionEventArgs { ActionName = "TargetConnection", Control = (CdsMigratorPluginControl)Parent });
        }

        public void OnConnectionUpdated(string connectedOrgFriendlyName)
        {
            labelTargetConnectionString.Text = connectedOrgFriendlyName;
            stepWizardControl1.Pages[3].AllowNext = true;
            entityRepositoryService = new EntityRepositoryService(OrganizationService);
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

        public void RadioButtonCheckedChanged(object sender, EventArgs e)
        {
            radioButtonCSVFormat.Checked = !radioButtonJsonFormat.Checked;
            groupBox1.Visible = false;
            stepWizardControl1.Pages[0].AllowNext = true;
        }

        public void RadioButton1CheckedChanged(object sender, EventArgs e)
        {
            stepWizardControl1.Pages[0].AllowNext = radioButtonCSVFormat.Checked && tbImportSchema.Text != string.Empty;
            radioButtonJsonFormat.Checked = !radioButtonCSVFormat.Checked;
            groupBox1.Visible = true;
        }

        public void Button2Click(object sender, EventArgs e)
        {
            importConfig.JsonFolderPath = tbSourceDataLocation.Text;
            importConfig.IgnoreStatuses = cbIgnoreStatuses.Checked;
            importConfig.IgnoreSystemFields = cbIgnoreSystemFields.Checked;
            importConfig.SaveBatchSize = Convert.ToInt32(nudSavePageSize.Value);

            tokenSource = new CancellationTokenSource();
            tbLogger.Clear();
            Task.Run(() =>
            {
                PerformImportAction(tbImportSchema.Text, Convert.ToInt32(nudMaxThreads.Value), radioButtonJsonFormat.Checked, logger, entityRepositoryService, importConfig, tokenSource);
            });
        }

        protected void WizardButtonsOnCancel(object sender, EventArgs e)
        {
            tokenSource?.Cancel();
        }

        private void LoadImportConfigFileButtonClick(object sender, EventArgs e)
        {
            var fd = openFileDialog1.ShowDialog();

            if (fd == DialogResult.OK)
            {
                tbImportConfigFile.Text = openFileDialog1.FileName;
            }
        }

        private void Button3Click(object sender, EventArgs e)
        {
            var fd = openFileDialog1.ShowDialog();

            if (fd == DialogResult.OK)
            {
                tbImportSchema.Text = openFileDialog1.FileName;
            }
        }

        public void TbImportSchemeTextChanged(object sender, EventArgs e)
        {
            if (radioButtonCSVFormat.Checked)
            {
                stepWizardControl1.Pages[0].AllowNext = true;
            }
        }

        public void WizardButtonsOnNavigateToNextPage(object sender, EventArgs e)
        {
            var wizardButtons = (WizardButtons)sender;
            WizardNavigation(labelFolderPathValidation, tbSourceDataLocation, wizardButtons.PageContainer.SelectedPage, wizardButtons.PageContainer);
        }

        public void TabImportConfigFileTextChanged(object sender, EventArgs e)
        {
            importConfig = CrmImportConfig.GetConfiguration(openFileDialog1.FileName);

            cbIgnoreSystemFields.Checked = importConfig.IgnoreSystemFields;
            cbIgnoreStatuses.Checked = importConfig.IgnoreStatuses;
            tbSourceDataLocation.Text = importConfig.JsonFolderPath;
            nudSavePageSize.Value = importConfig.SaveBatchSize;
        }

        protected void TabSourceDataLocationTextChanged(object sender, EventArgs e)
        {
            ValidationHelpers.IsTextControlNotEmpty(labelFolderPathValidation, tbSourceDataLocation);
        }

        protected void ComboBoxLogLevelSelectedIndexChanged(object sender, EventArgs e)
        {
            logger.LogLevel = (LogLevel)comboBoxLogLevel.SelectedItem;
        }
    }
}