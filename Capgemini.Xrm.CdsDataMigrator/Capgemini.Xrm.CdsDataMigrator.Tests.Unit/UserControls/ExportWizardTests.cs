using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.UserControls;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.UserControls.Tests
{
    [Ignore("Will fix!")]
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
    }
}