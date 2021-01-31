using System;
using System.Windows.Forms;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Views;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Presenters;
using System.Threading;
using Microsoft.Xrm.Tooling.Connector;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Logging;
using XrmToolBox.Extensibility;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Services;
using Capgemini.Xrm.DataMigration.XrmToolBox.Services;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.UserControls
{
    public partial class exportWizard : UserControl, IExportView
    {
        private readonly MessageLogger logger;
        private readonly ExportPresenter presenter;
        private readonly IDataMigrationService dataMigrationService;
        private readonly ICrmGenericMigratorFactory migratorFactory;

        public exportWizard()
        {
            InitializeComponent();

            migratorFactory = new CrmGenericMigratorFactory();
            logger = new MessageLogger(textBoxLogs, SynchronizationContext.Current);
            dataMigrationService = new DataMigrationService(logger, migratorFactory);
            presenter = new ExportPresenter(this, logger, dataMigrationService);

            logger.LogVerbose($"ExportPresenter {presenter} successfully instatiated!");
            wizardButtons1.OnExecute += WizardButtons1_OnExecute;
            wizardButtons1.OnCustomNextNavigation += WizardButtons1_OnNavigateToNextPage;
            wizardButtons1.OnCustomPreviousNavigation += WizardButtons1_OnCustomPreviousNavigation;
        }

        private void WizardNavigation(WizardButtons wizardButtons, bool isNextNavigation)
        {
            if (wizardButtons.Container.SelectedPage.Name == "wizardPage2")
            {
                NavigationValidation(wizardButtons.Container.SelectedPage.Controls[0], wizardButtons.Container.SelectedPage.Controls[2], isNextNavigation, wizardButtons);
            }
            else if (wizardButtons.Container.SelectedPage.Name == "wizardPage4")
            {
                NavigationValidation(wizardButtons.Container.SelectedPage.Controls[0], wizardButtons.Container.SelectedPage.Controls[10], isNextNavigation, wizardButtons);
            }
            else
            {
                if (isNextNavigation)
                {
                    wizardButtons.Container.NextPage();
                }
            }
        }

        private void WizardButtons1_OnCustomPreviousNavigation(object sender, EventArgs e)
        {
            var wizardButtons = ((WizardButtons)sender);
            wizardButtons.Container.PreviousPage();
            WizardNavigation(wizardButtons, false);
        }

        private void WizardButtons1_OnNavigateToNextPage(object sender, EventArgs e)
        {
            var wizardButtons = ((WizardButtons)sender);
            WizardNavigation(wizardButtons, true);
        }

        public event EventHandler<RequestConnectionEventArgs> OnConnectionRequested;

        public event EventHandler SelectExportLocationHandler;

        public event EventHandler SelectExportConfigFileHandler;

        public event EventHandler SelectSchemaFileHandler;

        public event EventHandler ExportDataHandler;

        public bool FormatJsonSelected { get => radioButtonFormatJson.Checked; set => radioButtonFormatJson.Checked = value; }

        public bool FormatCsvSelected { get => radioButtonFormatCsv.Checked; set => radioButtonFormatCsv.Checked = value; }

        public CrmServiceClient CrmServiceClient { get; set; }

        public string SaveExportLocation { get => textBoxExportLocation.Text; set => textBoxExportLocation.Text = value; }

        public string ExportConfigFileLocation { get => textBoxExportConfigLocation.Text; set => textBoxExportConfigLocation.Text = value; }

        public string ExportSchemaFileLocation { get => textBoxSchemaLocation.Text; set => textBoxSchemaLocation.Text = value; }

        public bool ExportInactiveRecordsChecked { get => checkBoxExportInactiveRecords.Checked; set => checkBoxExportInactiveRecords.Checked = value; }

        public bool MinimizeJsonChecked { get => checkBoxMinimize.Checked; set => checkBoxMinimize.Checked = value; }

        public decimal BatchSize
        {
            get => numericUpDownBatchSize.Value;
            set
            {
                if (value > 0 && value <= 5000)
                {
                    numericUpDownBatchSize.Value = value;
                }
            }
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
            OnConnectionRequested(this, new RequestConnectionEventArgs { ActionName = string.Empty });
            labelTargetConnectionString.Text = CrmServiceClient.ConnectedOrgFriendlyName;
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

        private void wizardPage4_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
        {
            numericUpDownBatchSize.Value = 5000;
        }

        private void textBoxExportLocation_TextChanged(object sender, EventArgs e)
        {
            Validation(textBoxExportLocation, labelFolderPathValidation);
        }

        private void textBoxSchemaLocation_TextChanged(object sender, EventArgs e)
        {
            Validation(textBoxSchemaLocation, labelSchemaLocationFile);
        }

        private void Validation(TextBox textBoxTovalidate, Label validationLabel)
        {
            var pathToExporData = textBoxTovalidate.Text;
            var splitPath = pathToExporData.Split('\\');
            if (splitPath != null && splitPath.Length > 1)
            {
                validationLabel.Visible = false;
            }
            else
            {
                validationLabel.Visible = true;
            }
        }

        private void NavigationValidation(Control validatorControl, Control currentContainerControl, bool isNextButton, WizardButtons wizardButtons)
        {
            var elmControl = validatorControl;
            var PathToExporData = currentContainerControl.Text;
            var splitPath = PathToExporData.Split('\\');
            if (splitPath != null && splitPath.Length > 1)
            {
                if (!isNextButton)
                {
                    elmControl.Visible = false;
                }
                else
                {
                    wizardButtons.Container.NextPage();
                }
            }
            else
            {
                if (!isNextButton)
                {
                    elmControl.Visible = true;
                }
                else
                {
                    wizardButtons.Container.SelectedPage.Controls[0].Visible = true;
                }
            }
        }
    }
}