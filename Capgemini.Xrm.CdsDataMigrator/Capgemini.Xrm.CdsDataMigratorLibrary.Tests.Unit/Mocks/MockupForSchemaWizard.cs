using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin;
using System;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Tests.Unit.Mocks
{
    internal class MockupForSchemaWizard : SchemaWizard
    {
        public void InvokeRadioButton1CheckedChanged(EventArgs eventArgs)
        {
            RadioButton1CheckedChanged(null, eventArgs);
        }

        public void InvokeRadioButton2CheckedChanged(EventArgs eventArgs)
        {
            RadioButton2CheckedChanged(null, eventArgs);
        }

        public void InvokeRadioButton3CheckedChanged(EventArgs eventArgs)
        {
            RadioButton3CheckedChanged(null, eventArgs);
        }

        public void RadioButton4CheckedChanged(EventArgs eventArgs)
        {
            RadioButton4CheckedChanged(null, eventArgs);
        }
    }
}