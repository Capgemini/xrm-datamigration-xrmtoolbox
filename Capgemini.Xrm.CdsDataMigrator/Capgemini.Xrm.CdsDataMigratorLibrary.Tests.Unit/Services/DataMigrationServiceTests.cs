using Capgemini.DataMigration.Core;
using Capgemini.Xrm.CdsDataMigratorLibrary.Enums;
using Capgemini.Xrm.CdsDataMigratorLibrary.Services;
using Capgemini.Xrm.DataMigration.Config;
using Capgemini.Xrm.DataMigration.Core;
using Capgemini.Xrm.DataMigration.CrmStore.Config;
using Capgemini.Xrm.DataMigration.DataStore;
using Capgemini.Xrm.DataMigration.Repositories;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Capgemini.Xrm.CdsDataMigrator.Tests.Unit.Services
{
    [TestClass]
    public class DataMigrationServiceTests : TestBase
    {
        private Mock<ILogger> loggerMock;
        private Mock<ICrmGenericMigratorFactory> migratorFactoryMock;

        private DataMigrationService systemUnderTest;

        [TestInitialize]
        public void TestSetup()
        {
            SetupServiceMocks();
            loggerMock = new Mock<ILogger>();
            migratorFactoryMock = new Mock<ICrmGenericMigratorFactory>();

            systemUnderTest = new DataMigrationService(loggerMock.Object, migratorFactoryMock.Object);
        }

        [TestMethod]
        public void DataMigrationServiceIntantiation()
        {
            FluentActions.Invoking(() => new DataMigrationService(loggerMock.Object, migratorFactoryMock.Object))
                            .Should()
                            .NotThrow();
        }

        [TestMethod]
        public void DataMigrationServiceIntantiationWithNullInputs()
        {
            FluentActions.Invoking(() => new DataMigrationService(null, null))
                            .Should()
                            .Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void DataMigrationServiceIntantiationWithNullLogger()
        {
            FluentActions.Invoking(() => new DataMigrationService(null, migratorFactoryMock.Object))
                            .Should()
                            .Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void DataMigrationServiceIntantiationWithNullMigratorFactor()
        {
            FluentActions.Invoking(() => new DataMigrationService(loggerMock.Object, null))
                            .Should()
                            .Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ExportDataAsJsonV2()
        {
            var exportConfig = new CrmExporterConfig();
            exportConfig.CrmMigrationToolSchemaPaths.Add("TestData/ContactSchemaWithOwner.xml");
            var mockOrganisationService = new Mock<IOrganizationService>();
            var mockStoreReader = new Mock<IDataStoreReader<Entity, EntityWrapper>>();
            var mockStoreWriter = new Mock<IDataStoreWriter<Entity, EntityWrapper>>();
            var mockGenericCrmDataMigrator = new Mock<IGenericCrmDataMigrator>();

            migratorFactoryMock.Setup(x => x.GetCrmExportDataMigrator(
                                    DataFormat.Json,
                                    loggerMock.Object,
                                    It.IsAny<EntityRepository>(),
                                    exportConfig,
                                    It.IsAny<CancellationToken>(),
                                    It.IsAny<CrmSchemaConfiguration>()))
                               .Returns(mockGenericCrmDataMigrator.Object)
                               .Verifiable();

            FluentActions.Invoking(() => systemUnderTest.ExportData(mockOrganisationService.Object, DataFormat.Json, exportConfig))
                         .Should()
                         .NotThrow();

            migratorFactoryMock.Verify(x => x.GetCrmExportDataMigrator(
                DataFormat.Json,
                loggerMock.Object,
                It.IsAny<EntityRepository>(),
                exportConfig,
                It.IsAny<CancellationToken>(),
                It.IsAny<CrmSchemaConfiguration>()),
                Times.Once);

            mockGenericCrmDataMigrator.Verify(x => x.MigrateData(), Times.Once);
        }

        [TestMethod]
        public void ExportDataAsCsvV2()
        {
            var exportConfig = new CrmExporterConfig();
            exportConfig.CrmMigrationToolSchemaPaths.Add("TestData/ContactSchemaWithOwner.xml");
            var mockOrganisationService = new Mock<IOrganizationService>();
            var mockStoreReader = new Mock<IDataStoreReader<Entity, EntityWrapper>>();
            var mockStoreWriter = new Mock<IDataStoreWriter<Entity, EntityWrapper>>();
            var mockGenericCrmDataMigrator = new Mock<IGenericCrmDataMigrator>();

            migratorFactoryMock.Setup(x => x.GetCrmExportDataMigrator(
                                    DataFormat.Csv,
                                    loggerMock.Object,
                                    It.IsAny<EntityRepository>(),
                                    exportConfig,
                                    It.IsAny<CancellationToken>(),
                                    It.IsAny<CrmSchemaConfiguration>()))
                               .Returns(mockGenericCrmDataMigrator.Object)
                               .Verifiable();

            FluentActions.Invoking(() => systemUnderTest.ExportData(mockOrganisationService.Object, DataFormat.Csv, exportConfig))
                         .Should()
                         .NotThrow();

            migratorFactoryMock.Verify(x => x.GetCrmExportDataMigrator(
                DataFormat.Csv,
                loggerMock.Object,
                It.IsAny<EntityRepository>(),
                exportConfig,
                It.IsAny<CancellationToken>(),
                It.IsAny<CrmSchemaConfiguration>()),
                Times.Once);

            mockGenericCrmDataMigrator.Verify(x => x.MigrateData(), Times.Once);
        }

        [TestMethod]
        public void ExportData_ShouldNotifyExecptionWhenAnExceptionIsThrown()
        {
            // Arrange
            var thrownException = new Exception("Test exception.");
            var exportConfig = new CrmExporterConfig();
            exportConfig.CrmMigrationToolSchemaPaths.Add("TestData/ContactSchemaWithOwner.xml");
            var mockOrganisationService = new Mock<IOrganizationService>();
            var mockStoreReader = new Mock<IDataStoreReader<Entity, EntityWrapper>>();
            var mockStoreWriter = new Mock<IDataStoreWriter<Entity, EntityWrapper>>();
            var mockGenericCrmDataMigrator = new Mock<IGenericCrmDataMigrator>();

            migratorFactoryMock.Setup(x => x.GetCrmExportDataMigrator(
                                    DataFormat.Json,
                                    loggerMock.Object,
                                    It.IsAny<EntityRepository>(),
                                    exportConfig,
                                    It.IsAny<CancellationToken>(),
                                    It.IsAny<CrmSchemaConfiguration>()))
                                .Returns(mockGenericCrmDataMigrator.Object)
                                .Verifiable();

            mockGenericCrmDataMigrator
                .Setup(x => x.MigrateData())
                .Throws(thrownException);

            // Act
            var action = FluentActions.Invoking(() => systemUnderTest.ExportData(mockOrganisationService.Object, DataFormat.Json, exportConfig));

            // Assert
            action.Should().Throw<Exception>().WithMessage(thrownException.Message);
            loggerMock.Verify(x => x.LogError(thrownException.Message));
        }

        [TestMethod]
        public void ImportDataAsJsonV2()
        {
            var importConfig = new CrmImportConfig();
            var mockOrganisationService = new Mock<IOrganizationService>();
            var mockGenericCrmDataMigrator = new Mock<IGenericCrmDataMigrator>();
            var MockCrmSchemaConfig = new Mock<CrmSchemaConfiguration>();
            var maxThreads = 1;

            migratorFactoryMock.Setup(x => x.GetCrmImportDataMigrator(
                        DataFormat.Json,
                        loggerMock.Object,
                        It.IsAny<EntityRepository>(),
                        importConfig,
                        It.IsAny<CancellationToken>(),
                        MockCrmSchemaConfig.Object))
                    .Returns(mockGenericCrmDataMigrator.Object)
                    .Verifiable();

            FluentActions.Invoking(() => systemUnderTest.ImportData(mockOrganisationService.Object, DataFormat.Json, MockCrmSchemaConfig.Object, importConfig, maxThreads, EntityRepositoryServiceMock.Object))
                        .Should()
                        .NotThrow();

            migratorFactoryMock.Verify(x => x.GetCrmImportDataMigrator(
                DataFormat.Json,
                loggerMock.Object,
                It.IsAny<EntityRepository>(),
                importConfig,
                It.IsAny<CancellationToken>(),
                It.IsAny<CrmSchemaConfiguration>()),
                Times.Once);

            mockGenericCrmDataMigrator.Verify(x => x.MigrateData(), Times.Once);
        }

        [TestMethod]
        public void ImportDataAsCsvV2()
        {
            var importConfig = new CrmImportConfig();
            var mockOrganisationService = new Mock<IOrganizationService>();
            var mockGenericCrmDataMigrator = new Mock<IGenericCrmDataMigrator>();
            var MockCrmSchemaConfig = new Mock<CrmSchemaConfiguration>();
            var maxThreads = 1;

            migratorFactoryMock.Setup(x => x.GetCrmImportDataMigrator(
                        DataFormat.Csv,
                        loggerMock.Object,
                        It.IsAny<EntityRepository>(),
                        importConfig,
                        It.IsAny<CancellationToken>(),
                        MockCrmSchemaConfig.Object))
                    .Returns(mockGenericCrmDataMigrator.Object)
                    .Verifiable();

            FluentActions.Invoking(() => systemUnderTest.ImportData(mockOrganisationService.Object, DataFormat.Csv, MockCrmSchemaConfig.Object, importConfig, maxThreads, EntityRepositoryServiceMock.Object))
                        .Should()
                        .NotThrow();

            migratorFactoryMock.Verify(x => x.GetCrmImportDataMigrator(
                DataFormat.Csv,
                loggerMock.Object,
                It.IsAny<EntityRepository>(),
                importConfig,
                It.IsAny<CancellationToken>(),
                It.IsAny<CrmSchemaConfiguration>()),
                Times.Once);

            mockGenericCrmDataMigrator.Verify(x => x.MigrateData(), Times.Once);
        }

        [TestMethod]
        public void ImportDataAsJsonWhenMaxThreadsGreaterThanOne()
        {
            var importConfig = new CrmImportConfig();
            var mockOrganisationService = new Mock<IOrganizationService>();
            var mockGenericCrmDataMigrator = new Mock<IGenericCrmDataMigrator>();
            var MockCrmSchemaConfig = new Mock<CrmSchemaConfiguration>();
            var maxThreads = 2;

            migratorFactoryMock.Setup(x => x.GetCrmImportDataMigrator(
                        DataFormat.Json,
                        loggerMock.Object,
                        It.IsAny<List<IEntityRepository>>(),
                        importConfig,
                        It.IsAny<CancellationToken>(),
                        MockCrmSchemaConfig.Object))
                    .Returns(mockGenericCrmDataMigrator.Object)
                    .Verifiable();

            FluentActions.Invoking(() => systemUnderTest.ImportData(mockOrganisationService.Object, DataFormat.Json, MockCrmSchemaConfig.Object, importConfig, maxThreads, EntityRepositoryServiceMock.Object))
                        .Should()
                        .NotThrow();

            migratorFactoryMock.Verify(x => x.GetCrmImportDataMigrator(
                DataFormat.Json,
                loggerMock.Object,
                It.IsAny<List<IEntityRepository>>(),
                importConfig,
                It.IsAny<CancellationToken>(),
                It.IsAny<CrmSchemaConfiguration>()),
                Times.Once);

            mockGenericCrmDataMigrator.Verify(x => x.MigrateData(), Times.Once);
        }

        [TestMethod]
        public void ImportDataAsCsvWhenMaxThreadsGreaterThanOne()
        {
            var importConfig = new CrmImportConfig();
            var mockOrganisationService = new Mock<IOrganizationService>();
            var mockGenericCrmDataMigrator = new Mock<IGenericCrmDataMigrator>();
            var MockCrmSchemaConfig = new Mock<CrmSchemaConfiguration>();
            var maxThreads = 3;

            migratorFactoryMock.Setup(x => x.GetCrmImportDataMigrator(
                        DataFormat.Csv,
                        loggerMock.Object,
                        It.IsAny<List<IEntityRepository>>(),
                        importConfig,
                        It.IsAny<CancellationToken>(),
                        MockCrmSchemaConfig.Object))
                    .Returns(mockGenericCrmDataMigrator.Object)
                    .Verifiable();

            FluentActions.Invoking(() => systemUnderTest.ImportData(mockOrganisationService.Object, DataFormat.Csv, MockCrmSchemaConfig.Object, importConfig, maxThreads, EntityRepositoryServiceMock.Object))
                        .Should()
                        .NotThrow();

            migratorFactoryMock.Verify(x => x.GetCrmImportDataMigrator(
                DataFormat.Csv,
                loggerMock.Object,
                It.IsAny<List<IEntityRepository>>(),
                importConfig,
                It.IsAny<CancellationToken>(),
                It.IsAny<CrmSchemaConfiguration>()),
                Times.Once);

            mockGenericCrmDataMigrator.Verify(x => x.MigrateData(), Times.Once);
        }
    }
}