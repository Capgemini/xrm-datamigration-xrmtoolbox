using Capgemini.Xrm.CdsDataMigratorLibrary.Core;
using Capgemini.Xrm.CdsDataMigratorLibrary.Exceptions;
using Capgemini.Xrm.CdsDataMigratorLibrary.Presenters;
using Capgemini.Xrm.CdsDataMigratorLibrary.Services;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.UserControls;
using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using System;
using System.Threading;
using XrmToolBox.Extensibility;

namespace Capgemini.Xrm.CdsDataMigratorLibrary
{
    public partial class CdsMigratorPluginControl : PluginControlBase
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
        }

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

                    DataExportWizard.LoggerService = new LoggerService(DataExportWizard.LogDisplay, SynchronizationContext.Current, logManagerContainer);
                    DataExportWizard.MigratorFactory = new CrmGenericMigratorFactory();
                    DataExportWizard.DataMigrationService = new DataMigrationService(DataExportWizard.LoggerService, DataExportWizard.MigratorFactory);
                    DataExportWizard.Presenter = new ExportPresenter(DataExportWizard, DataExportWizard.LoggerService, DataExportWizard.DataMigrationService);
                }

                if (actionName == "TargetConnection" || actionName == "")
                {
                    var logManagerContainer = new LogManagerContainer(new LogManager(typeof(ImportWizard)));
                    DataImportWizard.LoggerService = new LoggerService(DataImportWizard.LogDisplay, SynchronizationContext.Current, logManagerContainer);
                    DataImportWizard.OrganizationService = detail.ServiceClient;
                    DataImportWizard.OnConnectionUpdated(detail.ServiceClient.ConnectedOrgFriendlyName);
                }
            }

            if (actionName == "")
            {
                base.UpdateConnection(newService, detail, actionName, parameter);
            }
        }

        private void OnConnectionRequestedHandler(object sender, RequestConnectionEventArgs e)
        {
            RaiseRequestConnectionEvent(e);
        }

        private void ToolStripButtonSchemaConfigClick(object sender, EventArgs e)
        {
            SchemaGeneratorWizard.BringToFront();
        }

        private void ToolStripButtonDataImportClick(object sender, EventArgs e)
        {
            DataImportWizard.BringToFront();
        }

        private void ToolStripButtonDataExportClick(object sender, EventArgs e)
        {
            DataExportWizard.BringToFront();
        }
    }
}