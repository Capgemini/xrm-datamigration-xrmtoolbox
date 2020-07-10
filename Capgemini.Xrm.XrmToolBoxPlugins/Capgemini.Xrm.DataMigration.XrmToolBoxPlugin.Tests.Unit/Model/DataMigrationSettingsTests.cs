using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using Moq;
using System;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Model.Tests
{
    [TestClass]
    public class DataMigrationSettingsTests
    {
        private const string ConnectionString = "Url = https://capgeminitest.crm4.dynamics.com; Username=someusername@someorg.onmicrosoft.com; Password=SomePassword; AuthType=Office365; RequireNewInstance=True;";
        private Mock<IOrganizationService> organizationServiceMock;

        private DataMigrationSettings systemUnderTest;

        [TestInitialize]
        public void Setup()
        {
            organizationServiceMock = new Mock<IOrganizationService>();

            systemUnderTest = new DataMigrationSettings();
        }

        [TestMethod]
        public void ValidateExportNoPropertiesConfigured()
        {
            FluentActions.Invoking(() => systemUnderTest.ValidateExport())
                .Should()
                .Throw<NullReferenceException>();
        }

        [TestMethod]
        public void ValidateExportSchemaFilePathConfigured()
        {
            systemUnderTest.SchemaFilePath = "TestData/TestSchemaFile.xml";
            FluentActions.Invoking(() => systemUnderTest.ValidateExport())
                .Should()
                .Throw<ArgumentNullException>()
                .Where(e => e.Message.Contains("path"));
        }

        [TestMethod]
        public void ValidateExportSchemaFilePathAndJsonFolderPathConfigured()
        {
            systemUnderTest.SchemaFilePath = "TestData/TestSchemaFile.xml";
            systemUnderTest.JsonFolderPath = "TestData";
            FluentActions.Invoking(() => systemUnderTest.ValidateExport())
                .Should()
                .NotThrow();

            systemUnderTest.FailedValidationMessage.Should().NotBeNullOrEmpty();
            systemUnderTest.FailedValidation.Should().BeTrue();
        }

        [TestMethod]
        public void ValidateExportBatchSizeLessThanPageSize()
        {
            systemUnderTest.SchemaFilePath = "TestData/TestSchemaFile.xml";
            systemUnderTest.JsonFolderPath = "TestData";
            systemUnderTest.SourceConnectionString = "Test";
            systemUnderTest.BatchSize = 9;
            systemUnderTest.PageSize = 10;

            using (var serviceClient = new CrmServiceClient(ConnectionString))
            {
                systemUnderTest.GetType()
                .GetProperty("SourceServiceClient")
                .SetValue(systemUnderTest, serviceClient, null);
            }

            FluentActions.Invoking(() => systemUnderTest.ValidateExport())
                .Should()
                .NotThrow();

            systemUnderTest.FailedValidationMessage.Contains("Batch Size cannot be less than Page Size").Should().BeTrue();
            systemUnderTest.FailedValidation.Should().BeTrue();
        }

        [TestMethod]
        public void ValidateExport()
        {
            systemUnderTest.SchemaFilePath = "TestData/TestSchemaFile.xml";
            systemUnderTest.JsonFolderPath = "TestData";
            systemUnderTest.SourceConnectionString = "Test";
            systemUnderTest.BatchSize = 12;
            systemUnderTest.PageSize = 10;

            using (var serviceClient = new CrmServiceClient(ConnectionString))
            {
                systemUnderTest.GetType()
                .GetProperty("SourceServiceClient")
                .SetValue(systemUnderTest, serviceClient, null);
            }

            FluentActions.Invoking(() => systemUnderTest.ValidateExport())
                .Should()
                .NotThrow();

            systemUnderTest.FailedValidationMessage.Should().BeNullOrEmpty();
            systemUnderTest.FailedValidation.Should().BeFalse();
        }

        [TestMethod]
        public void ValidateImportNullTargetConnectionString()
        {
            systemUnderTest.JsonFolderPath = "string.Empty";
            systemUnderTest.TargetConnectionString = "Test connection string";

            using (var serviceClient = new CrmServiceClient(ConnectionString))
            {
                systemUnderTest.GetType()
                    .GetProperty("TargetServiceClient")
                    .SetValue(systemUnderTest, serviceClient, null);

                FluentActions.Invoking(() => systemUnderTest.ValidateImport())
                    .Should()
                    .NotThrow();

                systemUnderTest.FailedValidationMessage.Should().BeNullOrEmpty();
                systemUnderTest.FailedValidation.Should().BeFalse();
            }
        }

        [TestMethod]
        public void ValidateImportNullTargetServiceClientEmptyTargetConnectionStringAndJsonFolderPathNotEmpty()
        {
            systemUnderTest.JsonFolderPath = "string.Empty";
            systemUnderTest.TargetConnectionString = string.Empty;
            systemUnderTest.GetType()
                .GetProperty("TargetServiceClient")
                .SetValue(systemUnderTest, null, null);

            FluentActions.Invoking(() => systemUnderTest.ValidateImport())
                .Should()
                .NotThrow();

            systemUnderTest.FailedValidationMessage.Contains("Select correct target connection details").Should().BeTrue();
            systemUnderTest.FailedValidationMessage.Contains("Enter correct Json file Path").Should().BeFalse();
            systemUnderTest.FailedValidation.Should().BeTrue();
        }

        [TestMethod]
        public void ValidateImportNullTargetServiceClientEmptyTargetConnectionStringAndEmptyJsonFolderPath()
        {
            systemUnderTest.JsonFolderPath = string.Empty;
            systemUnderTest.TargetConnectionString = string.Empty;

            systemUnderTest.GetType()
                .GetProperty("TargetServiceClient")
                .SetValue(systemUnderTest, null, null);

            FluentActions.Invoking(() => systemUnderTest.ValidateImport())
                .Should()
                .NotThrow();

            systemUnderTest.FailedValidationMessage.Contains("Select correct target connection details").Should().BeTrue();
            systemUnderTest.FailedValidationMessage.Contains("Enter correct Json file Path").Should().BeTrue();
            systemUnderTest.FailedValidation.Should().BeTrue();
        }

        //protected static void SetFieldValue(object input, string fieldName, object newValue)
        //{
        //    if (input == null)
        //    {
        //        throw new ArgumentNullException(nameof(input));
        //    }

        //    var field = input.GetType().GetRuntimeFields().First(a => a.Name == fieldName);
        //    field.SetValue(input, newValue);
        //}

        //protected static void SetPropertyValue(object input, string fieldName, object newValue)
        //{
        //    if (input == null)
        //    {
        //        throw new ArgumentNullException(nameof(input));
        //    }

        //    var field = input.GetType().GetProperty("",).GetRuntimeFields().First(a => a.Name == fieldName);
        //    field.SetValue(input, newValue);
        //}
    }
}