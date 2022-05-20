using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.UserControls;
using System;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Tests.Unit.Mocks
{
    internal class MockupForExportWizard : ExportWizard
    {
        public MockupForExportWizard()
        {
        }

        public void InvokeWizardButtonsOnCancel(EventArgs eventArgs)
        {
            ExportDataCancellationAction(null, eventArgs);
        }

        public void InvokeComboBoxLogLevelSelectedIndexChanged(EventArgs eventArgs)
        {
            ComboBoxLogLevelSelectedIndexChanged(null, eventArgs);
        }

        public void InvokeButtonSchemaLocationClick(EventArgs eventArgs)
        {
            ButtonSchemaLocationClick(null, eventArgs);
        }

        public void InvokeExportDataAction()
        {
            ExportDataAction(this, new EventArgs());
        }
    }
}