using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.UserControls;
using System;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Tests.Unit.Mocks
{
    internal class MockupForImportWizard : ImportWizard
    {
        public void InvokeWizardButtonsOnCancel(EventArgs eventArgs)
        {
            WizardButtonsOnCancel(null, eventArgs);
        }

        public void InvokeComboBoxLogLevelSelectedIndexChanged(EventArgs eventArgs)
        {
            ComboBoxLogLevelSelectedIndexChanged(null, eventArgs);
        }
    }
}