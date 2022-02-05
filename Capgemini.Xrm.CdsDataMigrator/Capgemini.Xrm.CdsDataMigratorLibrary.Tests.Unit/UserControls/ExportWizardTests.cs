using Capgemini.Xrm.CdsDataMigrator.Tests.Unit;
using Capgemini.Xrm.CdsDataMigratorLibrary.Presenters;
using Capgemini.Xrm.CdsDataMigratorLibrary.Services;
using Capgemini.Xrm.CdsDataMigratorLibrary.Tests.Unit.Mocks;
using Capgemini.Xrm.CdsDataMigratorLibrary.UserControls;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.UserControls;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.UserControls.Tests
{
    [TestClass]
    public class ExportWizardTests : TestBase
    {
        [TestInitialize]
        public void Setup()
        {
            SetupServiceMocks();
        }

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
                systemUnderTest.MinimizeJsonChecked.Should().BeFalse();
                systemUnderTest.OrganizationService.Should().BeNull();
            }
        }

        [TestMethod]
        public void WizardValidationExportConfig()
        {
            using (var systemUnderTest = new ExportWizard())
            {
                systemUnderTest.LoggerService = LogConfigMock.Object;

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
        public void OnConnectionUpdated()
        {
            var sender = new WizardButtons();
            var pageContainer = new AeroWizard.WizardPageContainer();

            pageContainer.Pages.Add(new AeroWizard.WizardPage());
            pageContainer.Pages.Add(new AeroWizard.WizardPage() { Name = "exportConfig" });
            sender.PageContainer = pageContainer;

            string connectedOrgFriendlyName = "test value";

            using (var systemUnderTest = new ExportWizard())
            {
                FluentActions.Invoking(() => systemUnderTest.OnConnectionUpdated(connectedOrgFriendlyName))
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
                systemUnderTest.Presenter = new ExportPresenter(systemUnderTest, LogConfigMock.Object, DataMigrationServiceMock.Object);

                FluentActions.Invoking(() => systemUnderTest.InvokeWizardButtonsOnCancel(new EventArgs()))
                            .Should()
                            .NotThrow();
            }
        }

        [TestMethod]
        public void InvokeComboBoxLogLevelSelectedIndexChanged()
        {
            var sender = new WizardButtons();
            var pageContainer = new AeroWizard.WizardPageContainer();

            pageContainer.Pages.Add(new AeroWizard.WizardPage());
            pageContainer.Pages.Add(new AeroWizard.WizardPage() { Name = "exportConfig" });
            sender.PageContainer = pageContainer;

            using (var systemUnderTest = new MockupForExportWizard())
            {
                FluentActions.Invoking(() => systemUnderTest.InvokeComboBoxLogLevelSelectedIndexChanged(new EventArgs()))
                            .Should()
                            .NotThrow();
            }
        }
    }
}