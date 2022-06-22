using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Windows.Forms;
using XrmToolBox.Extensibility;
using static XrmToolBox.Extensibility.PluginControlBase;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.UserControls
{
    public partial class DataverseEnvironmentSelector : UserControl
    {
        private PluginControlBase xrmToolBoxControl;

        public DataverseEnvironmentSelector()
        {
            InitializeComponent();
        }

        public IOrganizationService Service { get; private set; }

        protected override void OnLoad(EventArgs e)
        {
            xrmToolBoxControl = FindPluginControlBase();

            if (xrmToolBoxControl is null) return;

            OnConnectionUpdated(null, new ConnectionUpdatedEventArgs(xrmToolBoxControl.Service, xrmToolBoxControl.ConnectionDetail));
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (xrmToolBoxControl is null) return;

            xrmToolBoxControl.ConnectionUpdated += OnConnectionUpdated;
            xrmToolBoxControl.RaiseRequestConnectionEvent(new RequestConnectionEventArgs
            {
                ActionName = "",
                Control = xrmToolBoxControl
            });
        }

        private void OnConnectionUpdated(object sender, ConnectionUpdatedEventArgs e)
        {
            Service = e?.Service;
            lblConnectionName.Text = e?.ConnectionDetail?.OrganizationFriendlyName ?? "No environment selected.";
            xrmToolBoxControl.ConnectionUpdated -= OnConnectionUpdated;
        }

        private PluginControlBase FindPluginControlBase()
        {
            var parent = Parent;

            while (!(parent is PluginControlBase || parent is null))
            {
                parent = parent.Parent;
            }

            return parent as PluginControlBase;
        }
    }
}
