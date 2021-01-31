using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Model;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.Xrm.XrmToolBoxPluginBase.Tests.Unit.Model
{
    [TestClass]
    public class SaveAllSettingsTests
    {
        private readonly string schemaFilePathErrorMessage = "Select schema file path";
        private readonly string importFilePathErrorMessage = "Select import config file path";
        private readonly string exportFilePathErrorMessage = "Select export config file path";

        private SaveAllSettings systemUnderTest;

        [TestInitialize]
        public void TestSetup()
        {
            systemUnderTest = new SaveAllSettings();
        }

        [TestMethod]
        public void SaveAllSettingsInstantiation()
        {
            FluentActions.Invoking(() => new DataMigrationSettings())
                                .Should()
                                .NotThrow();
        }

        [TestMethod]
        public void ValidateNullSchemaFilePath()
        {
            systemUnderTest.SchemaFilePath = null;
            systemUnderTest.ExportFilePath = "ExportPath";
            systemUnderTest.ImportFilePath = "ImportPath";

            systemUnderTest.Validate();

            systemUnderTest.FailedValidation.Should().BeTrue();
            systemUnderTest.FailedValidationMessage.Should().Contain(schemaFilePathErrorMessage);
            systemUnderTest.FailedValidationMessage.Should().NotContain(importFilePathErrorMessage);
            systemUnderTest.FailedValidationMessage.Should().NotContain(exportFilePathErrorMessage);
        }

        [TestMethod]
        public void ValidateEmptySchemaFilePath()
        {
            systemUnderTest.SchemaFilePath = string.Empty;
            systemUnderTest.ExportFilePath = "ExportPath";
            systemUnderTest.ImportFilePath = "ImportPath";

            systemUnderTest.Validate();

            systemUnderTest.FailedValidation.Should().BeTrue();
            systemUnderTest.FailedValidationMessage.Should().Contain(schemaFilePathErrorMessage);
            systemUnderTest.FailedValidationMessage.Should().NotContain(importFilePathErrorMessage);
            systemUnderTest.FailedValidationMessage.Should().NotContain(exportFilePathErrorMessage);
        }

        [TestMethod]
        public void ValidateNullImportFilePath()
        {
            systemUnderTest.SchemaFilePath = "SchemaPath";
            systemUnderTest.ExportFilePath = "ExportPath";
            systemUnderTest.ImportFilePath = null;

            systemUnderTest.Validate();

            systemUnderTest.FailedValidation.Should().BeTrue();
            systemUnderTest.FailedValidationMessage.Should().NotContain(schemaFilePathErrorMessage);
            systemUnderTest.FailedValidationMessage.Should().Contain(importFilePathErrorMessage);
            systemUnderTest.FailedValidationMessage.Should().NotContain(exportFilePathErrorMessage);
        }

        [TestMethod]
        public void ValidateEmptyImportFilePath()
        {
            systemUnderTest.SchemaFilePath = "SchemaPath";
            systemUnderTest.ExportFilePath = "ExportPath";
            systemUnderTest.ImportFilePath = string.Empty;

            systemUnderTest.Validate();

            systemUnderTest.FailedValidation.Should().BeTrue();
            systemUnderTest.FailedValidationMessage.Should().NotContain(schemaFilePathErrorMessage);
            systemUnderTest.FailedValidationMessage.Should().Contain(importFilePathErrorMessage);
            systemUnderTest.FailedValidationMessage.Should().NotContain(exportFilePathErrorMessage);
        }

        [TestMethod]
        public void ValidateNullExportFilePath()
        {
            systemUnderTest.SchemaFilePath = "SchemaPath";
            systemUnderTest.ExportFilePath = null;
            systemUnderTest.ImportFilePath = "ImportPath";

            systemUnderTest.Validate();

            systemUnderTest.FailedValidation.Should().BeTrue();
            systemUnderTest.FailedValidationMessage.Should().NotContain(schemaFilePathErrorMessage);
            systemUnderTest.FailedValidationMessage.Should().NotContain(importFilePathErrorMessage);
            systemUnderTest.FailedValidationMessage.Should().Contain(exportFilePathErrorMessage);
        }

        [TestMethod]
        public void ValidateEmptyExportFilePath()
        {
            systemUnderTest.SchemaFilePath = "SchemaPath";
            systemUnderTest.ExportFilePath = string.Empty;
            systemUnderTest.ImportFilePath = "ImportPath";

            systemUnderTest.Validate();

            systemUnderTest.FailedValidation.Should().BeTrue();
            systemUnderTest.FailedValidationMessage.Should().NotContain(schemaFilePathErrorMessage);
            systemUnderTest.FailedValidationMessage.Should().NotContain(importFilePathErrorMessage);
            systemUnderTest.FailedValidationMessage.Should().Contain(exportFilePathErrorMessage);
        }

        [TestMethod]
        public void ValidateSuccessfully()
        {
            systemUnderTest.SchemaFilePath = "SchemaPath";
            systemUnderTest.ExportFilePath = "ExportPath";
            systemUnderTest.ImportFilePath = "ImportPath";

            systemUnderTest.Validate();

            systemUnderTest.FailedValidation.Should().BeFalse();
            systemUnderTest.FailedValidationMessage.Should().NotContain(schemaFilePathErrorMessage);
            systemUnderTest.FailedValidationMessage.Should().NotContain(importFilePathErrorMessage);
            systemUnderTest.FailedValidationMessage.Should().NotContain(exportFilePathErrorMessage);
        }
    }
}
