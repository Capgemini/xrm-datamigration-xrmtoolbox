using System;
using XrmToolBox.Extensibility;
using Microsoft.Xrm.Sdk;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core;
using System.Threading;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Logging;
using McTools.Xrm.Connection;
using Capgemini.Xrm.DataMigration.XrmToolBox.Services;
using Capgemini.Xrm.DataMigration.XrmToolBox.Core;

namespace MyXrmToolBoxPlugin3
{
    public partial class MyPluginControl : PluginControlBase
    {
        private readonly Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core.Settings settings;

        public MyPluginControl()
        {
            SettingFileHandler.GetConfigData(out settings);
            InitializeComponent();
            DataImportWizard.OnConnectionRequested += ImportWizard1_onConnectionRequested;
            DataExportWizard.OnConnectionRequested += ImportWizard1_onConnectionRequested;
            SchemaGeneratorWizard.OnConnectionRequested += ImportWizard1_onConnectionRequested;
            SchemaGeneratorWizard.Settings = settings;
            SchemaGeneratorWizard.BringToFront();
        }

        protected CancellationTokenSource TokenSource { get; set; } = null;

        protected MessageLogger Logger { get; set; } = null;

        public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
        {
            if (detail != null)
            {
                if (actionName == "SchemaConnection" || actionName == "")
                {
                    SchemaGeneratorWizard.OrganizationService = detail.ServiceClient;
                    SchemaGeneratorWizard.MetadataService = new MetadataService();
                    SchemaGeneratorWizard.FeedbackManager = new FeedbackManager();
                    SchemaGeneratorWizard.DataMigratorExceptionHelper = new DataMigratorExceptionHelper();
                    SchemaGeneratorWizard.OnConnectionUpdated(detail.ServiceClient.ConnectedOrgId, detail.ServiceClient.ConnectedOrgFriendlyName);
                }

                if (actionName == "SourceConnection" || actionName == "")
                {
                    DataExportWizard.OrganizationService = detail.ServiceClient;
                    DataExportWizard.OnConnectionUpdated(detail.ServiceClient.ConnectedOrgFriendlyName);
                }

                if (actionName == "TargetConnection" || actionName == "")
                {
                    DataImportWizard.OrganizationService = detail.ServiceClient;
                    DataImportWizard.OnConnectionUpdated(detail.ServiceClient.ConnectedOrgFriendlyName);
                }
            }

            if (actionName == "")
            {
                base.UpdateConnection(newService, detail, actionName, parameter);
            }
        }

        private void ImportWizard1_onConnectionRequested(object sender, RequestConnectionEventArgs e)
        {
            RaiseRequestConnectionEvent(e);
        }

        private void toolStripButtonSchemaConfig_Click(object sender, EventArgs e)
        {
            SchemaGeneratorWizard.BringToFront();
        }

        private void toolStripButtonDataImport_Click(object sender, EventArgs e)
        {
            DataImportWizard.BringToFront();
        }

        private void toolStripButtonDataExport_Click(object sender, EventArgs e)
        {
            DataExportWizard.BringToFront();
        }
    }
}