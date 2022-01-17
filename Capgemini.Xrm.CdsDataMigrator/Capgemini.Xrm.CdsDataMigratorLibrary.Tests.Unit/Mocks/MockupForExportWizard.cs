using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.UserControls;
using System;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Tests.Unit.Mocks
{
    internal class MockupForExportWizard : ExportWizard
    {
        public void InvokeWizardButtonsOnCancel(EventArgs eventArgs)
        {
            WizardButtonsOnCancel(null, eventArgs);
        }

        public void ComboBoxLogLevelSelectedIndexChanged(EventArgs eventArgs)
        {
            ComboBoxLogLevelSelectedIndexChanged(null, eventArgs);
        }
    }
}