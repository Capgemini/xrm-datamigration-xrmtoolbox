using System;
using Capgemini.Xrm.CdsDataMigratorLibrary.UserControls;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Tests.Unit.Mocks
{
    internal class MockupForWizardButtons : WizardButtons
    {
        public void InvokeButtonExecuteClick(EventArgs eventArgs)
        {
            ButtonExecuteClick(null, eventArgs);
        }

        public void InvokeButtonCancelClick(EventArgs eventArgs)
        {
            buttonCancelClick(null, eventArgs);
        }

        public void InvokePreviousButtonClick(EventArgs eventArgs)
        {
            PreviousButtonClick(null, eventArgs);
        }

        public void InvokeNextButtonClick(EventArgs eventArgs)
        {
            NextButtonClick(null, eventArgs);
        }
    }
}