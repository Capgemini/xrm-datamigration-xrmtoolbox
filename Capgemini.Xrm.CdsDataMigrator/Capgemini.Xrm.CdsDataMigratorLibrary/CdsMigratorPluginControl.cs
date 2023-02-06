using Capgemini.Xrm.CdsDataMigratorLibrary.Core;
using Capgemini.Xrm.CdsDataMigratorLibrary.Exceptions;
using Capgemini.Xrm.CdsDataMigratorLibrary.Helpers;
using Capgemini.Xrm.CdsDataMigratorLibrary.Presenters;
using Capgemini.Xrm.CdsDataMigratorLibrary.Services;
using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using System;
using System.Diagnostics.CodeAnalysis;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Args;
using XrmToolBox.Extensibility.Interfaces;

namespace Capgemini.Xrm.CdsDataMigratorLibrary
{
    [ExcludeFromCodeCoverage]
    public partial class CdsMigratorPluginControl : PluginControlBase, IStatusBarMessenger
    {
        private readonly Settings settings;
        private ImportPagePresenter ImportPagePresenter;
        private ExportPagePresenter ExportPagePresenter;
        private SchemaGeneratorPresenter schemaGeneratorPresenter;

        public CdsMigratorPluginControl()
        {
            SettingFileHandler.GetConfigData<CdsMigratorPluginControl>(out settings);
            InitializeComponent();
        }

        public event EventHandler<StatusBarMessageEventArgs> SendMessageToStatusBar;

        public override async void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
        {
            if (detail != null)
            {
                var logger = new LogToFileService(new LogManagerContainer(new LogManager(typeof(CdsMigratorPluginControl))));
                var dataMigrationService = new DataMigrationService(logger, new CrmGenericMigratorFactory());
                var metaDataService = new MetadataService();
                var exceptionService = new ExceptionService();
                var viewHelpers = new ViewHelpers();
                var entityRepositoryService = new EntityRepositoryService(detail.ServiceClient);
                ImportPagePresenter = new ImportPagePresenter(this.importPage1, this, dataMigrationService, detail.ServiceClient, metaDataService, viewHelpers, entityRepositoryService);
                ExportPagePresenter = new ExportPagePresenter(this.exportPage1, this, dataMigrationService, detail.ServiceClient, metaDataService, exceptionService, viewHelpers);
                this.importPage1.SetServices(metaDataService, detail.ServiceClient, viewHelpers);
                this.exportPage1.SetServices(metaDataService, detail.ServiceClient, exceptionService, viewHelpers);
                schemaGeneratorPresenter = new SchemaGeneratorPresenter(sgpManageSchema, detail.ServiceClient, new MetadataService(), new NotificationService(), new ExceptionService(), settings);
                await schemaGeneratorPresenter.OnConnectionUpdated(detail.ServiceClient.ConnectedOrgId, detail.ServiceClient.ConnectedOrgFriendlyName);
            }
            base.UpdateConnection(newService, detail, actionName, parameter);
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

        private void ShowExportPage_Click(object sender, EventArgs e)
        {
            exportPage1.BringToFront();
        }

        private void ShowSchemaManager(object sender, EventArgs e)
        {
            sgpManageSchema.BringToFront();
        }

        private void ShowImportPage_Click(object sender, EventArgs e)
        {
            importPage1.BringToFront();
        }
    }
}