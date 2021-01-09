using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Model.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class LoadAllSettingsTests
    {
        private LoadAllSettings systemUnderTest;

        [TestInitialize]
        public void TestSetup()
        {
            systemUnderTest = new LoadAllSettings();
        }

        [TestMethod]
        public void ValidateNullSchemaPath()
        {
            systemUnderTest.SchemaPath = null;
            systemUnderTest.ImportPath = "ImportPath";
            systemUnderTest.ExportPath = "ExportPath";

            systemUnderTest.Validate();

            systemUnderTest.FailedValidation.Should().BeTrue();
            systemUnderTest.FailedValidationMessage.Should().Contain("Schema file path is empty");
            systemUnderTest.FailedValidationMessage.Should().NotContain("Import config file path is empty");
            systemUnderTest.FailedValidationMessage.Should().NotContain("Export config file path is empty");
        }

        [TestMethod]
        public void ValidateNullImportPath()
        {
            systemUnderTest.SchemaPath = "SchemaPath";
            systemUnderTest.ImportPath = null;
            systemUnderTest.ExportPath = "ExportPath";

            systemUnderTest.Validate();

            systemUnderTest.FailedValidation.Should().BeTrue();
            systemUnderTest.FailedValidationMessage.Should().NotContain("Schema file path is empty");
            systemUnderTest.FailedValidationMessage.Should().Contain("Import config file path is empty");
            systemUnderTest.FailedValidationMessage.Should().NotContain("Export config file path is empty");
        }

        [TestMethod]
        public void ValidateNullExportPath()
        {
            systemUnderTest.SchemaPath = "SchemaPath";
            systemUnderTest.ImportPath = "ImportPath";
            systemUnderTest.ExportPath = null;

            systemUnderTest.Validate();

            systemUnderTest.FailedValidation.Should().BeTrue();
            systemUnderTest.FailedValidationMessage.Should().NotContain("Schema file path is empty");
            systemUnderTest.FailedValidationMessage.Should().NotContain("Import config file path is empty");
            systemUnderTest.FailedValidationMessage.Should().Contain("Export config file path is empty");
        }

        [TestMethod]
        public void ValidateNullProperties()
        {
            systemUnderTest.SchemaPath = string.Empty;
            systemUnderTest.ImportPath = null;
            systemUnderTest.ExportPath = null;

            systemUnderTest.Validate();

            systemUnderTest.FailedValidation.Should().BeTrue();
            systemUnderTest.FailedValidationMessage.Should().Contain("Schema file path is empty");
            systemUnderTest.FailedValidationMessage.Should().Contain("Import config file path is empty");
            systemUnderTest.FailedValidationMessage.Should().Contain("Export config file path is empty");
        }

        [TestMethod]
        public void Validate()
        {
            systemUnderTest.SchemaPath = "SchemaPath";
            systemUnderTest.ImportPath = "ImportPath";
            systemUnderTest.ExportPath = "ExportPath";

            systemUnderTest.Validate();

            systemUnderTest.FailedValidation.Should().BeFalse();
            systemUnderTest.FailedValidationMessage.Should().NotContain("Schema file path is empty");
            systemUnderTest.FailedValidationMessage.Should().NotContain("Import config file path is empty");
            systemUnderTest.FailedValidationMessage.Should().NotContain("Export config file path is empty");
        }
    }
}