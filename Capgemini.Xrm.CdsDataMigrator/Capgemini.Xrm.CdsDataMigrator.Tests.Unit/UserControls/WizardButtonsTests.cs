using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.UserControls.Tests
{
    [TestClass]
    public class WizardButtonsTests
    {
        private WizardButtons systemUnderTest;

        [TestCleanup]
        public void Setup()
        {
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (systemUnderTest != null)
            {
                systemUnderTest.Dispose();
            }
        }

        [TestMethod]
        public void InitializeWizardButtons()
        {
            FluentActions.Invoking(() => systemUnderTest = new WizardButtons())
                             .Should()
                             .NotThrow();
        }

        [TestMethod]
        public void ShowExecuteButton()
        {
            using (systemUnderTest = new WizardButtons())
            {
                systemUnderTest.ShowExecuteButton = true;

                systemUnderTest.ShowExecuteButton.Should().BeTrue();
            }
        }
    }
}