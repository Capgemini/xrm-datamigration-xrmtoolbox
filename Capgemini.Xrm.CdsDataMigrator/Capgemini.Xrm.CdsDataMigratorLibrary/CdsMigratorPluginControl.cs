using Capgemini.Xrm.CdsDataMigratorLibrary.Core;
using Capgemini.Xrm.CdsDataMigratorLibrary.Exceptions;
using Capgemini.Xrm.CdsDataMigratorLibrary.Presenters;
using Capgemini.Xrm.CdsDataMigratorLibrary.Services;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.UserControls;
using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Args;
using XrmToolBox.Extensibility.Interfaces;

namespace Capgemini.Xrm.CdsDataMigratorLibrary
{
    [ExcludeFromCodeCoverage]
    public partial class CdsMigratorPluginControl : PluginControlBase, IStatusBarMessenger
    {
        private readonly Settings settings;

        public CdsMigratorPluginControl()
        {
            SettingFileHandler.GetConfigData<SchemaWizard>(out settings);
            InitializeComponent();
            DataImportWizard.OnConnectionRequested += OnConnectionRequestedHandler;
            DataExportWizard.OnConnectionRequested += OnConnectionRequestedHandler;
            SchemaGeneratorWizard.OnConnectionRequested += OnConnectionRequestedHandler;
            SchemaGeneratorWizard.Settings = settings;
            SchemaGeneratorWizard.BringToFront();

            var logger = new LogToFileService(new LogManagerContainer(new LogManager(typeof(CdsMigratorPluginControl))));
            var dataMigrationService = new DataMigrationService(logger, new CrmGenericMigratorFactory());
            this.importPage1.Tag = new ImportPagePresenter(this.importPage1, this, dataMigrationService);
        }

        public event EventHandler<StatusBarMessageEventArgs> SendMessageToStatusBar;

        public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
        {
            if (detail != null)
            {
                if (actionName == "SchemaConnection" || actionName == "")
                {
                    SchemaGeneratorWizard.OrganizationService = detail.ServiceClient;
                    SchemaGeneratorWizard.MetadataService = new MetadataService();
                    SchemaGeneratorWizard.NotificationService = new NotificationService();
                    SchemaGeneratorWizard.ExceptionService = new ExceptionService();
                    SchemaGeneratorWizard.OnConnectionUpdated(detail.ServiceClient.ConnectedOrgId, detail.ServiceClient.ConnectedOrgFriendlyName);
                }

                if (actionName == "SourceConnection" || actionName == "")
                {
                    DataExportWizard.OrganizationService = detail.ServiceClient;
                    DataExportWizard.OnConnectionUpdated(detail.ServiceClient.ConnectedOrgFriendlyName);

                    var logManagerContainer = new LogManagerContainer(new LogManager(typeof(ExportWizard)));

                    DataExportWizard.LoggerService = new LogToTextboxService(DataExportWizard.LogDisplay, SynchronizationContext.Current, logManagerContainer);
                    DataExportWizard.MigratorFactory = new CrmGenericMigratorFactory();
                    DataExportWizard.DataMigrationService = new DataMigrationService(DataExportWizard.LoggerService, DataExportWizard.MigratorFactory);
                    DataExportWizard.Presenter = new ExportPresenter(DataExportWizard, DataExportWizard.LoggerService, DataExportWizard.DataMigrationService);
                    DataExportWizard.OnActionStarted += OnActionStarted;
                    DataExportWizard.OnActionProgressed += OnActionProgressed;
                    DataExportWizard.OnActionCompleted += OnActionCompleted;
                }

                if (actionName == "TargetConnection" || actionName == "")
                {
                    var logManagerContainer = new LogManagerContainer(new LogManager(typeof(ImportWizard)));
                    DataImportWizard.LoggerService = new LogToTextboxService(DataImportWizard.LogDisplay, SynchronizationContext.Current, logManagerContainer);
                    DataImportWizard.OrganizationService = detail.ServiceClient;
                    DataImportWizard.OnConnectionUpdated(detail.ServiceClient.ConnectedOrgFriendlyName);
                    DataImportWizard.OnActionStarted += OnActionStarted;
                    DataImportWizard.OnActionProgressed += OnActionProgressed;
                    DataImportWizard.OnActionCompleted += OnActionCompleted;
                }
            }

            if (actionName == "")
            {
                base.UpdateConnection(newService, detail, actionName, parameter);
            }
        }

        private void OnActionCompleted(object sender, EventArgs e)
        {
            SendMessageToStatusBar?.Invoke(this, new StatusBarMessageEventArgs(100, $"Completed!"));
        }

        private void OnActionProgressed(object sender, EventArgs e)
        {
            SendMessageToStatusBar?.Invoke(this, new StatusBarMessageEventArgs(50, $"Progressing..."));
        }

        private void OnActionStarted(object sender, EventArgs e)
        {
            SendMessageToStatusBar?.Invoke(this, new StatusBarMessageEventArgs(0, $"Starting..."));
        }

        private void OnConnectionRequestedHandler(object sender, RequestConnectionEventArgs e)
        {
            RaiseRequestConnectionEvent(e);
        }

        private void ToolStripButtonSchemaConfigClick(object sender, EventArgs e)
        {
            SendMessageToStatusBar?.Invoke(this, new StatusBarMessageEventArgs(0, ""));
            SchemaGeneratorWizard.BringToFront();
        }

        private void ToolStripButtonDataImportClick(object sender, EventArgs e)
        {
            SendMessageToStatusBar?.Invoke(this, new StatusBarMessageEventArgs(0, ""));
            DataImportWizard.BringToFront();
        }

        private void ToolStripButtonDataExportClick(object sender, EventArgs e)
        {
            SendMessageToStatusBar?.Invoke(this, new StatusBarMessageEventArgs(0, ""));
            DataExportWizard.BringToFront();
        }

        private void tsbShowExportPage_Click(object sender, EventArgs e)
        {
            exportPage1.BringToFront();
        }

        private void tsbShowImportPage_Click(object sender, EventArgs e)
        {
            importPage1.BringToFront();
        }
    }
}