using Microsoft.Xrm.Sdk;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;
using XrmToolBox.Extensibility;
using static XrmToolBox.Extensibility.PluginControlBase;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.UserControls
{
    [ExcludeFromCodeCoverage]
    public partial class DataverseEnvironmentSelector : UserControl
    {
        private PluginControlBase xrmToolBoxControl;

        public DataverseEnvironmentSelector()
        {
            InitializeComponent();
        }

        public enum Scope
        {
            /// <summary>
            /// Any time a connection is changed, this updates.
            /// </summary>
            Global,

            /// <summary>
            /// This changes only when specifically requested to via the button.
            /// </summary>
            Local
        }

        /// <summary>
        /// Determine when the value will get updated.
        /// 
        /// Global - Any time a connection is changed, this updates.
        /// Local - This changes only when specifically requested to via the button.
        /// </summary>
        public Scope ConnectionUpdatedScope { get; set; } = Scope.Local;

        public IOrganizationService Service { get; private set; }

        protected override void OnLoad(EventArgs e)
        {
            xrmToolBoxControl = FindPluginControlBase();

            if (xrmToolBoxControl is null) return;

            if (ConnectionUpdatedScope == Scope.Global)
            {
                xrmToolBoxControl.ConnectionUpdated += OnConnectionUpdated;
            }

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

            if (ConnectionUpdatedScope != Scope.Global)
            {
                xrmToolBoxControl.ConnectionUpdated -= OnConnectionUpdated;
            }
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
