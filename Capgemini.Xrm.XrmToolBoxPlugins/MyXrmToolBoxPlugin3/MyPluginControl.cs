using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XrmToolBox.Extensibility;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;
using McTools.Xrm.Connection;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core;
using System.Threading;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Logging;

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
        }

        public new event EventHandler OnRequestConnection;

        protected CancellationTokenSource TokenSource { get; set; } = null;

        protected MessageLogger Logger { get; set; } = null;

        protected override void OnConnectionUpdated(ConnectionUpdatedEventArgs e)
        {
            base.OnConnectionUpdated(e);
        }

        public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
        {
            if (detail != null)
            {
                DataImportWizard.CrmServiceClient = detail.ServiceClient;

                SchemaGeneratorWizard.CrmServiceClient = detail.ServiceClient;
                SchemaGeneratorWizard.OnConnectionUpdated();

                DataExportWizard.CrmServiceClient = detail.ServiceClient;
            }
        }

        private void ImportWizard1_onConnectionRequested(object sender, RequestConnectionEventArgs e)
        {
            if (OnRequestConnection != null)
            {
                var args = new RequestConnectionEventArgs { ActionName = "Custom", Control = this };
                OnRequestConnection(this, args);
            }
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