using System;
using AeroWizard;
using Capgemini.Xrm.CdsDataMigratorLibrary.UserControls;
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
        public void ShowExecuteButtonIsFinishedPageIsTrue()
        {
            using (var systemUnderTest = new WizardButtons())
            {
                systemUnderTest.PageContainer = new WizardPageContainer();
                systemUnderTest.PageContainer.Pages.Add(new WizardPage());
                systemUnderTest.PageContainer.Pages.Add(new WizardPage());

                FluentActions.Invoking(() => systemUnderTest.HandleSelectedPageChanged(true))
                             .Should()
                             .NotThrow();
            }
        }

        [TestMethod]
        public void ShowExecuteButtonIsFinishedPageIsFalse()
        {
            using (var systemUnderTest = new WizardButtons())
            {
                systemUnderTest.PageContainer = new WizardPageContainer();
                systemUnderTest.PageContainer.Pages.Add(new WizardPage());
                systemUnderTest.PageContainer.Pages.Add(new WizardPage());

                FluentActions.Invoking(() => systemUnderTest.HandleSelectedPageChanged(false))
                             .Should()
                             .NotThrow();
            }
        }

        [TestMethod]
        public void ExecutePreviousButtonClickWithoutCustomEvent()
        {
            using (var systemUnderTest = new WizardButtons())
            {
                systemUnderTest.PageContainer = new WizardPageContainer();
                systemUnderTest.PageContainer.Pages.Add(new WizardPage());
                systemUnderTest.PageContainer.Pages.Add(new WizardPage());

                var eventArgs = new EventArgs();

                FluentActions.Invoking(() => systemUnderTest.ExecutePreviousButtonClick(eventArgs))
                             .Should()
                             .Throw<InvalidOperationException>()
                             .WithMessage("Stack empty.");
            }
        }

        [TestMethod]
        public void ExecutePreviousButtonClickWithCustomEvent()
        {
            var customEventExecuted = false;

            using (var systemUnderTest = new WizardButtons())
            {
                systemUnderTest.PageContainer = new WizardPageContainer();
                systemUnderTest.PageContainer.Pages.Add(new WizardPage());
                systemUnderTest.PageContainer.Pages.Add(new WizardPage());

                var eventArgs = new EventArgs();

                systemUnderTest.OnCustomPreviousNavigation += (x, y) => { customEventExecuted = true; };

                systemUnderTest.HandleSelectedPageChanged(false);

                FluentActions.Invoking(() => systemUnderTest.ExecutePreviousButtonClick(eventArgs))
                             .Should()
                             .NotThrow();

                customEventExecuted.Should().BeTrue();
            }
        }

        [TestMethod]
        public void ExecuteNextButtonClickWithCustomEvent()
        {
            var customEventExecuted = false;

            using (var systemUnderTest = new WizardButtons())
            {
                systemUnderTest.PageContainer = new WizardPageContainer();
                systemUnderTest.PageContainer.Pages.Add(new WizardPage());
                systemUnderTest.PageContainer.Pages.Add(new WizardPage());

                var eventArgs = new EventArgs();

                systemUnderTest.OnCustomNextNavigation += (x, y) => { customEventExecuted = true; };

                systemUnderTest.HandleSelectedPageChanged(false);

                FluentActions.Invoking(() => systemUnderTest.ExecuteNextButtonClick(eventArgs))
                             .Should()
                             .NotThrow();

                customEventExecuted.Should().BeTrue();
            }
        }

        [TestMethod]
        public void ExecuteNextButtonClickWithoutCustomEvent()
        {
            using (var systemUnderTest = new WizardButtons())
            {
                systemUnderTest.PageContainer = new WizardPageContainer();
                systemUnderTest.PageContainer.Pages.Add(new WizardPage());
                systemUnderTest.PageContainer.Pages.Add(new WizardPage());

                var eventArgs = new EventArgs();

                systemUnderTest.OnCustomNextNavigation += null;

                systemUnderTest.HandleSelectedPageChanged(false);

                FluentActions.Invoking(() => systemUnderTest.ExecuteNextButtonClick(eventArgs))
                             .Should()
                             .NotThrow();
            }
        }

        [TestMethod]
        public void ExecuteActionNullExecuteActionDoesNotThrowException()
        {
            using (var systemUnderTest = new WizardButtons())
            {
                var eventArgs = new EventArgs();
                systemUnderTest.OnExecute += null;

                FluentActions.Invoking(() => systemUnderTest.ExecuteAction(eventArgs))
                             .Should()
                             .NotThrow();
            }
        }

        [TestMethod]
        public void ExecuteAction()
        {
            var customEventExecuted = false;

            using (var systemUnderTest = new WizardButtons())
            {
                var eventArgs = new EventArgs();
                systemUnderTest.OnExecute += (x, y) => { customEventExecuted = true; };

                FluentActions.Invoking(() => systemUnderTest.ExecuteAction(eventArgs))
                             .Should()
                             .NotThrow();

                customEventExecuted.Should().BeTrue();
            }
        }

        [TestMethod]
        public void CancelActionNullExecuteActionDoesNotThrowException()
        {
            using (var systemUnderTest = new WizardButtons())
            {
                var eventArgs = new EventArgs();
                systemUnderTest.OnCancel += null;

                FluentActions.Invoking(() => systemUnderTest.CancelAction(eventArgs))
                             .Should()
                             .NotThrow();
            }
        }

        [TestMethod]
        public void CancelAction()
        {
            var customEventExecuted = false;

            using (var systemUnderTest = new WizardButtons())
            {
                var eventArgs = new EventArgs();
                systemUnderTest.OnCancel += (x, y) => { customEventExecuted = true; };

                FluentActions.Invoking(() => systemUnderTest.CancelAction(eventArgs))
                             .Should()
                             .NotThrow();

                customEventExecuted.Should().BeTrue();
            }
        }
    }
}