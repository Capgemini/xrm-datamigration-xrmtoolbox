using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Model.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class DataMigrationSettingsTests
    {
        private readonly string targetConnectionStringErrorMessage = "Select correct target connection details";
        private readonly string jsonFolderPathErrorMessage = "Enter correct Json file Path";
        private readonly string schemaFilePathErrorMessage = "Enter correct Schema file Path";
        private readonly string sourceConnectionStringErrorMessage = "Select correct source connection details";
        private readonly string batchSizeErrorMessage = "Batch Size cannot be less than Page Size";

        private DataMigrationSettings systemUnderTest;

        [TestInitialize]
        public void TestSetup()
        {
            systemUnderTest = new DataMigrationSettings();
        }

        [TestMethod]
        public void ValidateExportInstantiation()
        {
            FluentActions.Invoking(() => new DataMigrationSettings())
                                .Should()
                                .NotThrow();
        }

        [TestMethod]
        public void ValidateExportNullSchemaFilePath()
        {
            systemUnderTest.SchemaFilePath = null;
            systemUnderTest.SourceConnectionString = "SourceConnectionString";
            systemUnderTest.JsonFolderPath = "JsonFolderPath";
            systemUnderTest.BatchSize = 1;
            systemUnderTest.PageSize = 1;

            systemUnderTest.ValidateExport();

            systemUnderTest.FailedValidation.Should().BeTrue();
            systemUnderTest.FailedValidationMessage.Should().Contain(schemaFilePathErrorMessage);
            systemUnderTest.FailedValidationMessage.Should().NotContain(sourceConnectionStringErrorMessage);
            systemUnderTest.FailedValidationMessage.Should().NotContain(jsonFolderPathErrorMessage);
            systemUnderTest.FailedValidationMessage.Should().NotContain(batchSizeErrorMessage);
        }

        [TestMethod]
        public void ValidateExportEmptySchemaFilePath()
        {
            systemUnderTest.SchemaFilePath = string.Empty;
            systemUnderTest.SourceConnectionString = "SourceConnectionString";
            systemUnderTest.JsonFolderPath = "JsonFolderPath";
            systemUnderTest.BatchSize = 1;
            systemUnderTest.PageSize = 1;

            systemUnderTest.ValidateExport();

            systemUnderTest.FailedValidation.Should().BeTrue();
            systemUnderTest.FailedValidationMessage.Should().Contain(schemaFilePathErrorMessage);
            systemUnderTest.FailedValidationMessage.Should().NotContain(sourceConnectionStringErrorMessage);
            systemUnderTest.FailedValidationMessage.Should().NotContain(jsonFolderPathErrorMessage);
            systemUnderTest.FailedValidationMessage.Should().NotContain(batchSizeErrorMessage);
        }

        [TestMethod]
        public void ValidateExportSchemaFilePathWithLessThan5Characters()
        {
            systemUnderTest.SchemaFilePath = "Four";
            systemUnderTest.SourceConnectionString = "SourceConnectionString";
            systemUnderTest.JsonFolderPath = "JsonFolderPath";
            systemUnderTest.BatchSize = 1;
            systemUnderTest.PageSize = 1;

            systemUnderTest.ValidateExport();

            systemUnderTest.FailedValidation.Should().BeTrue();
            systemUnderTest.FailedValidationMessage.Should().Contain(schemaFilePathErrorMessage);
            systemUnderTest.FailedValidationMessage.Should().NotContain(sourceConnectionStringErrorMessage);
            systemUnderTest.FailedValidationMessage.Should().NotContain(jsonFolderPathErrorMessage);
            systemUnderTest.FailedValidationMessage.Should().NotContain(batchSizeErrorMessage);
        }

        [TestMethod]
        public void ValidateExportValidSchemaFilePath()
        {
            systemUnderTest.SchemaFilePath = "SchemaFilePath";
            systemUnderTest.SourceConnectionString = "SourceConnectionString";
            systemUnderTest.JsonFolderPath = "JsonFolderPath";
            systemUnderTest.BatchSize = 1;
            systemUnderTest.PageSize = 1;

            systemUnderTest.ValidateExport();

            systemUnderTest.FailedValidation.Should().BeFalse();
            systemUnderTest.FailedValidationMessage.Should().NotContain(schemaFilePathErrorMessage);
            systemUnderTest.FailedValidationMessage.Should().NotContain(sourceConnectionStringErrorMessage);
            systemUnderTest.FailedValidationMessage.Should().NotContain(jsonFolderPathErrorMessage);
            systemUnderTest.FailedValidationMessage.Should().NotContain(batchSizeErrorMessage);
        }

        [TestMethod]
        public void ValidateExportNullSourceConnectionString()
        {
            systemUnderTest.SchemaFilePath = "SchemaFilePath";
            systemUnderTest.SourceConnectionString = null;
            systemUnderTest.JsonFolderPath = "JsonFolderPath";
            systemUnderTest.BatchSize = 1;
            systemUnderTest.PageSize = 1;

            systemUnderTest.ValidateExport();

            systemUnderTest.FailedValidation.Should().BeTrue();
            systemUnderTest.FailedValidationMessage.Should().NotContain(schemaFilePathErrorMessage);
            systemUnderTest.FailedValidationMessage.Should().Contain(sourceConnectionStringErrorMessage);
            systemUnderTest.FailedValidationMessage.Should().NotContain(jsonFolderPathErrorMessage);
            systemUnderTest.FailedValidationMessage.Should().NotContain(batchSizeErrorMessage);
        }

        [TestMethod]
        public void ValidateExportEmptySourceConnectionString()
        {
            systemUnderTest.SchemaFilePath = "SchemaFilePath";
            systemUnderTest.SourceConnectionString = string.Empty;
            systemUnderTest.JsonFolderPath = "JsonFolderPath";
            systemUnderTest.BatchSize = 1;
            systemUnderTest.PageSize = 1;

            systemUnderTest.ValidateExport();

            systemUnderTest.FailedValidation.Should().BeTrue();
            systemUnderTest.FailedValidationMessage.Should().NotContain(schemaFilePathErrorMessage);
            systemUnderTest.FailedValidationMessage.Should().Contain(sourceConnectionStringErrorMessage);
            systemUnderTest.FailedValidationMessage.Should().NotContain(jsonFolderPathErrorMessage);
            systemUnderTest.FailedValidationMessage.Should().NotContain(batchSizeErrorMessage);
        }

        [TestMethod]
        public void ValidateExportNullJsonFolderPath()
        {
            systemUnderTest.SchemaFilePath = "SchemaFilePath";
            systemUnderTest.SourceConnectionString = "SourceConnectionString";
            systemUnderTest.JsonFolderPath = null;
            systemUnderTest.BatchSize = 0;
            systemUnderTest.PageSize = 0;

            systemUnderTest.ValidateExport();

            systemUnderTest.FailedValidation.Should().BeTrue();
            systemUnderTest.FailedValidationMessage.Should().NotContain(schemaFilePathErrorMessage);
            systemUnderTest.FailedValidationMessage.Should().NotContain(sourceConnectionStringErrorMessage);
            systemUnderTest.FailedValidationMessage.Should().Contain(jsonFolderPathErrorMessage);
            systemUnderTest.FailedValidationMessage.Should().NotContain(batchSizeErrorMessage);
        }

        [TestMethod]
        public void ValidateExportJsonFolderPath()
        {
            systemUnderTest.SchemaFilePath = "SchemaFilePath";
            systemUnderTest.SourceConnectionString = "SourceConnectionString";
            systemUnderTest.JsonFolderPath = "JsonFolderPath";
            systemUnderTest.BatchSize = 10;
            systemUnderTest.PageSize = 1;

            systemUnderTest.ValidateExport();

            systemUnderTest.FailedValidation.Should().BeFalse();
            systemUnderTest.FailedValidationMessage.Should().NotContain(schemaFilePathErrorMessage);
            systemUnderTest.FailedValidationMessage.Should().NotContain(sourceConnectionStringErrorMessage);
            systemUnderTest.FailedValidationMessage.Should().NotContain(jsonFolderPathErrorMessage);
            systemUnderTest.FailedValidationMessage.Should().NotContain(batchSizeErrorMessage);
        }

        [TestMethod]
        public void ValidateExportBatchSizeLessThanPageSize()
        {
            systemUnderTest.SchemaFilePath = "SchemaFilePath";
            systemUnderTest.SourceConnectionString = "SourceConnectionString";
            systemUnderTest.JsonFolderPath = "JsonFolderPath";
            systemUnderTest.BatchSize = 1;
            systemUnderTest.PageSize = 2;

            systemUnderTest.ValidateExport();

            systemUnderTest.FailedValidation.Should().BeTrue();
            systemUnderTest.FailedValidationMessage.Should().NotContain(schemaFilePathErrorMessage);
            systemUnderTest.FailedValidationMessage.Should().NotContain(sourceConnectionStringErrorMessage);
            systemUnderTest.FailedValidationMessage.Should().NotContain(jsonFolderPathErrorMessage);
            systemUnderTest.FailedValidationMessage.Should().Contain(batchSizeErrorMessage);
        }

        [TestMethod]
        public void ValidateExportBatchSizeEqualToPageSize()
        {
            systemUnderTest.SchemaFilePath = "SchemaFilePath";
            systemUnderTest.SourceConnectionString = "SourceConnectionString";
            systemUnderTest.JsonFolderPath = "JsonFolderPath";
            systemUnderTest.BatchSize = 3;
            systemUnderTest.PageSize = 3;

            systemUnderTest.ValidateExport();

            systemUnderTest.FailedValidation.Should().BeFalse();
            systemUnderTest.FailedValidationMessage.Should().NotContain(schemaFilePathErrorMessage);
            systemUnderTest.FailedValidationMessage.Should().NotContain(sourceConnectionStringErrorMessage);
            systemUnderTest.FailedValidationMessage.Should().NotContain(jsonFolderPathErrorMessage);
            systemUnderTest.FailedValidationMessage.Should().NotContain(batchSizeErrorMessage);
        }

        [TestMethod]
        public void ValidateExportBatchSizeGreaterThanPageSize()
        {
            systemUnderTest.SchemaFilePath = "SchemaFilePath";
            systemUnderTest.SourceConnectionString = "SourceConnectionString";
            systemUnderTest.JsonFolderPath = "JsonFolderPath";
            systemUnderTest.BatchSize = 2;
            systemUnderTest.PageSize = 1;

            systemUnderTest.ValidateExport();

            systemUnderTest.FailedValidation.Should().BeFalse();
            systemUnderTest.FailedValidationMessage.Should().NotContain(schemaFilePathErrorMessage);
            systemUnderTest.FailedValidationMessage.Should().NotContain(sourceConnectionStringErrorMessage);
            systemUnderTest.FailedValidationMessage.Should().NotContain(jsonFolderPathErrorMessage);
            systemUnderTest.FailedValidationMessage.Should().NotContain(batchSizeErrorMessage);
        }

        [TestMethod]
        public void ValidateImportNullTargetConnectionString()
        {
            systemUnderTest.TargetConnectionString = null;
            systemUnderTest.JsonFolderPath = "Test JsonFolderPath";

            systemUnderTest.ValidateImport();

            systemUnderTest.FailedValidation.Should().BeTrue();
            systemUnderTest.FailedValidationMessage.Should().Contain(targetConnectionStringErrorMessage);
            systemUnderTest.FailedValidationMessage.Should().NotContain(jsonFolderPathErrorMessage);
        }

        [TestMethod]
        public void ValidateImportEmptyTargetConnectionString()
        {
            systemUnderTest.TargetConnectionString = string.Empty;
            systemUnderTest.JsonFolderPath = "Test JsonFolderPath";

            systemUnderTest.ValidateImport();

            systemUnderTest.FailedValidation.Should().BeTrue();
            systemUnderTest.FailedValidationMessage.Should().Contain(targetConnectionStringErrorMessage);
            systemUnderTest.FailedValidationMessage.Should().NotContain(jsonFolderPathErrorMessage);
        }

        [TestMethod]
        public void ValidateImportNullJsonFolderPath()
        {
            systemUnderTest.TargetConnectionString = "Test connection string";
            systemUnderTest.JsonFolderPath = null;

            systemUnderTest.ValidateImport();

            systemUnderTest.FailedValidation.Should().BeTrue();
            systemUnderTest.FailedValidationMessage.Should().NotContain(targetConnectionStringErrorMessage);
            systemUnderTest.FailedValidationMessage.Should().Contain(jsonFolderPathErrorMessage);
        }

        [TestMethod]
        public void ValidateImportEmptyJsonFolderPath()
        {
            systemUnderTest.TargetConnectionString = "Test connection string";
            systemUnderTest.JsonFolderPath = string.Empty;

            systemUnderTest.ValidateImport();

            systemUnderTest.FailedValidation.Should().BeTrue();
            systemUnderTest.FailedValidationMessage.Should().NotContain(targetConnectionStringErrorMessage);
            systemUnderTest.FailedValidationMessage.Should().Contain(jsonFolderPathErrorMessage);
        }

        [TestMethod]
        public void ValidateImportEmptyJsonFolderPathAndEmptyTargetConnectionString()
        {
            systemUnderTest.TargetConnectionString = string.Empty;
            systemUnderTest.JsonFolderPath = string.Empty;

            systemUnderTest.ValidateImport();

            systemUnderTest.FailedValidation.Should().BeTrue();
            systemUnderTest.FailedValidationMessage.Should().Contain(targetConnectionStringErrorMessage);
            systemUnderTest.FailedValidationMessage.Should().Contain(jsonFolderPathErrorMessage);
        }

        [TestMethod]
        public void ValidateImport()
        {
            systemUnderTest.TargetConnectionString = "Sample connection string";
            systemUnderTest.JsonFolderPath = "Sample Json folder pathh";

            systemUnderTest.ValidateImport();

            systemUnderTest.FailedValidation.Should().BeFalse();
            systemUnderTest.FailedValidationMessage.Should().NotContain(targetConnectionStringErrorMessage);
            systemUnderTest.FailedValidationMessage.Should().NotContain(jsonFolderPathErrorMessage);
        }
    }
}