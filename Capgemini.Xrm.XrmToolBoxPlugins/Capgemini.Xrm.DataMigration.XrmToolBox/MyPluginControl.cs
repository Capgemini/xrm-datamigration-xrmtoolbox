using System;
using XrmToolBox.Extensibility;
using Microsoft.Xrm.Sdk;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core;
using System.Threading;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Logging;
using McTools.Xrm.Connection;

namespace MyXrmToolBoxPlugin3
{
    public partial class MyPluginControl : PluginControlBase
    {
        private Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core.Settings settings;

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
                if (actionName == "SchemaConnection" || actionName == "" )
                {
                    SchemaGeneratorWizard.CrmServiceClient = detail.ServiceClient;
                    SchemaGeneratorWizard.OnConnectionUpdated();
                }

                if (actionName == "SourceConnection" || actionName == "")
                {
                    DataExportWizard.CrmServiceClient = detail.ServiceClient;
                    DataExportWizard.OnConnectionUpdated();
                }
                
                if (actionName == "TargetConnection" || actionName == "")
                {
                    DataImportWizard.CrmServiceClient = detail.ServiceClient;
                    DataImportWizard.OnConnectionUpdated();
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