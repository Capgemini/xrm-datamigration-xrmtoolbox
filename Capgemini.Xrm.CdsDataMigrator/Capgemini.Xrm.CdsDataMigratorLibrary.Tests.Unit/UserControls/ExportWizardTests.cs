using Capgemini.Xrm.CdsDataMigratorLibrary.Tests.Unit.Mocks;
using Capgemini.Xrm.CdsDataMigratorLibrary.UserControls;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.UserControls;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.UserControls.Tests
{
    [TestClass]
    public class ExportWizardTests
    {
        [TestMethod]
        public void WizardValidationExportConfigIsNull()
        {
            using (var systemUnderTest = new ExportWizard())
            {
                systemUnderTest.ExportConfigFileLocation = null;

                var actual = systemUnderTest.WizardValidation("exportConfig");

                actual.Should().BeTrue();
                systemUnderTest.ExportSchemaFileLocation.Should().BeNullOrEmpty();
                systemUnderTest.SaveExportLocation.Should().BeNullOrEmpty();
                systemUnderTest.BatchSize.Should().Be(5000);
                systemUnderTest.ExportInactiveRecordsChecked.Should().BeFalse();
            }
        }

        [TestMethod]
        public void WizardValidationExportConfig()
        {
            using (var systemUnderTest = new ExportWizard())
            {
                systemUnderTest.ExportConfigFileLocation = "TestData\\ExportConfig.json";

                var actual = systemUnderTest.WizardValidation("exportConfig");

                actual.Should().BeTrue();

                systemUnderTest.ExportSchemaFileLocation.Should().EndWith("TestData\\usersettingsschema.xml");
                systemUnderTest.SaveExportLocation.Should().EndWith("TestData");
                systemUnderTest.BatchSize.Should().Be(500);
                systemUnderTest.ExportInactiveRecordsChecked.Should().BeTrue();
            }
        }

        [TestMethod]
        public void WizardValidationExportLocation()
        {
            using (var systemUnderTest = new ExportWizard())
            {
                systemUnderTest.ExportConfigFileLocation = "TestData\\ExportConfig.json";
                var actual = systemUnderTest.WizardValidation("exportLocation");

                actual.Should().BeTrue();
            }
        }

        [TestMethod]
        public void WizardValidationExecuteExport()
        {
            using (var systemUnderTest = new ExportWizard())
            {
                systemUnderTest.ExportConfigFileLocation = "TestData\\ExportConfig.json";
                var actual = systemUnderTest.WizardValidation("executeExport");

                actual.Should().BeTrue();
            }
        }

        [TestMethod]
        public void WizardValidationEmptySelectedPage()
        {
            using (var systemUnderTest = new ExportWizard())
            {
                systemUnderTest.ExportConfigFileLocation = "TestData\\ExportConfig.json";
                var actual = systemUnderTest.WizardValidation(string.Empty);

                actual.Should().BeTrue();
            }
        }

        [TestMethod]
        public void WizardValidationNonExistingSelectedPage()
        {
            using (var systemUnderTest = new ExportWizard())
            {
                systemUnderTest.ExportConfigFileLocation = "TestData\\ExportConfig.json";
                var actual = systemUnderTest.WizardValidation(string.Empty);

                actual.Should().BeTrue();
            }
        }

        [TestMethod]
        public void WizardButtonsOnCustomPreviousNavigation()
        {
            var sender = new WizardButtons();
            var pageContainer = new AeroWizard.WizardPageContainer();
            pageContainer.Pages.Add(new AeroWizard.WizardPage());
            sender.PageContainer = pageContainer;

            using (var systemUnderTest = new ExportWizard())
            {
                FluentActions.Invoking(() => systemUnderTest.WizardButtonsOnCustomPreviousNavigation(sender, new EventArgs()))
                            .Should()
                            .Throw<InvalidOperationException>()
                            .WithMessage("Stack empty.");
            }
        }

        [TestMethod]
        public void WizardButtonsOnNavigateToNextPage()
        {
            var sender = new WizardButtons();
            var pageContainer = new AeroWizard.WizardPageContainer();

            pageContainer.Pages.Add(new AeroWizard.WizardPage());
            pageContainer.Pages.Add(new AeroWizard.WizardPage() { Name = "exportConfig" });
            sender.PageContainer = pageContainer;

            using (var systemUnderTest = new ExportWizard())
            {
                FluentActions.Invoking(() => systemUnderTest.WizardButtonsOnNavigateToNextPage(sender, new EventArgs()))
                            .Should()
                            .NotThrow();
            }
        }

        [TestMethod]
        public void InvokeWizardButtonsOnCancel()
        {
            var sender = new WizardButtons();
            var pageContainer = new AeroWizard.WizardPageContainer();

            pageContainer.Pages.Add(new AeroWizard.WizardPage());
            pageContainer.Pages.Add(new AeroWizard.WizardPage() { Name = "exportConfig" });
            sender.PageContainer = pageContainer;

            using (var systemUnderTest = new MockupForExportWizard())
            {
                FluentActions.Invoking(() => systemUnderTest.InvokeWizardButtonsOnCancel(new EventArgs()))
                            .Should()
                            .NotThrow();
            }
        }

        [TestMethod]
        public void ComboBoxLogLevelSelectedIndexChanged()
        {
            var sender = new WizardButtons();
            var pageContainer = new AeroWizard.WizardPageContainer();

            pageContainer.Pages.Add(new AeroWizard.WizardPage());
            pageContainer.Pages.Add(new AeroWizard.WizardPage() { Name = "exportConfig" });
            sender.PageContainer = pageContainer;

            using (var systemUnderTest = new MockupForExportWizard())
            {
                FluentActions.Invoking(() => systemUnderTest.ComboBoxLogLevelSelectedIndexChanged(new EventArgs()))
                            .Should()
                            .NotThrow();
            }
        }
    }
}