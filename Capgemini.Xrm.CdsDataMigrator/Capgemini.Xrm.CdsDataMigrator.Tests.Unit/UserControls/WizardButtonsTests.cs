using AeroWizard;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.UserControls;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.UserControls.Tests
{
    [TestClass]
    public class WizardButtonsTests
    {
        [TestMethod]
        public void InitializeWizardButtons()
        {
            FluentActions.Invoking(() => new WizardButtons())
                             .Should()
                             .NotThrow();
        }

        [TestMethod]
        public void ShowExecuteButton()
        {
            using (var systemUnderTest = new WizardButtons())
            {
                systemUnderTest.ShowExecuteButton = true;

                systemUnderTest.ShowExecuteButton.Should().BeTrue();
            }
        }

        [TestMethod]
        public void ShowExecuteButton2()
        {
            using (var systemUnderTest = new WizardButtons())
            {
                systemUnderTest.PageContainer = new WizardPageContainer();
                systemUnderTest.PageContainer.Pages.Add(new WizardPage());
                systemUnderTest.PageContainer.Pages.Add(new WizardPage());

                FluentActions.Invoking(() => systemUnderTest.HandleSelectedPageChanged())
                             .Should()
                             .NotThrow();
            }
        }
    }
}