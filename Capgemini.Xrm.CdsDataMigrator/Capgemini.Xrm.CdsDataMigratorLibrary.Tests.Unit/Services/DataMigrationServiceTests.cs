using System;
using System.Collections.Generic;
using System.Threading;
using Capgemini.DataMigration.Core;
using Capgemini.Xrm.CdsDataMigratorLibrary.Enums;
using Capgemini.Xrm.CdsDataMigratorLibrary.Models;
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

namespace Capgemini.Xrm.CdsDataMigrator.Tests.Unit.Services
{
    [TestClass]
    public class DataMigrationServiceTests
    {
        private Mock<ILogger> loggerMock;
        private Mock<ICrmGenericMigratorFactory> migratorFactoryMock;

        private DataMigrationService systemUnderTest;

        [TestInitialize]
        public void TestSetup()
        {
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
        public void ExportDataWithNullExportSettings()
        {
            FluentActions.Invoking(() => systemUnderTest.ExportData(null))
                .Should()
                .Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ExportDataAsJson()
        {
            var dataFormat = DataFormat.Json;
            var exportSettings = new ExportSettings
            {
                // this is not really unit test but it is the quckest way to get this tested as CrmSchemaConfiguration.ReadFromFile actually looks for the file!
                SchemaPath = "TestData/ContactSchemaWithOwner.xml",
                DataFormat = dataFormat,
                BatchSize = 5000,
                ExportConfigPath = "TestData",
                SavePath = "TestData"
            };

            var storeReader = new Mock<IDataStoreReader<Entity, EntityWrapper>>().Object;
            var storeWriter = new Mock<IDataStoreWriter<Entity, EntityWrapper>>().Object;

            var genericCrmDataMigrator = new GenericCrmDataMigrator(loggerMock.Object, storeReader, storeWriter);

            migratorFactoryMock.Setup(x => x.GetCrmDataMigrator(
                                                                dataFormat,
                                                                It.IsAny<ILogger>(),
                                                                It.IsAny<EntityRepository>(),
                                                                It.IsAny<CrmExporterConfig>(),
                                                                It.IsAny<CancellationToken>(),
                                                                It.IsAny<CrmSchemaConfiguration>()))
                               .Returns(genericCrmDataMigrator)
                               .Verifiable();

            FluentActions.Invoking(() => systemUnderTest.ExportData(exportSettings))
                         .Should()
                         .Throw<NullReferenceException>();

            migratorFactoryMock.Verify(x => x.GetCrmDataMigrator(dataFormat, It.IsAny<ILogger>(), It.IsAny<EntityRepository>(), It.IsAny<CrmExporterConfig>(), It.IsAny<CancellationToken>(), It.IsAny<CrmSchemaConfiguration>()), Times.Once);
        }

        [TestMethod]
        public void ExportDataAsCsv()
        {
            var dataFormat = DataFormat.Csv;
            var exportSettings = new ExportSettings
            {
                // this is not really unit test but it is the quckest way to get this tested as CrmSchemaConfiguration.ReadFromFile actually looks for the file!
                SchemaPath = "TestData/BusinessUnitSchema.xml",
                DataFormat = dataFormat,
                ExportConfigPath = "TestData",
                BatchSize = 5000,
                SavePath = "TestData"
            };

            var storeReader = new Mock<IDataStoreReader<Entity, EntityWrapper>>().Object;
            var storeWriter = new Mock<IDataStoreWriter<Entity, EntityWrapper>>().Object;

            var genericCrmDataMigrator = new GenericCrmDataMigrator(loggerMock.Object, storeReader, storeWriter);

            var factoryMock = new Mock<ICrmGenericMigratorFactory>();
            factoryMock.Setup(x => x.GetCrmDataMigrator(
                                                                dataFormat,
                                                                It.IsAny<ILogger>(),
                                                                It.IsAny<EntityRepository>(),
                                                                It.IsAny<CrmExporterConfig>(),
                                                                It.IsAny<CancellationToken>(),
                                                                It.IsAny<CrmSchemaConfiguration>()))
                               .Returns(genericCrmDataMigrator)
                               .Verifiable();

            var localSystemUnderTest = new DataMigrationService(loggerMock.Object, factoryMock.Object);

            FluentActions.Invoking(() => localSystemUnderTest.ExportData(exportSettings))
                         .Should()
                         .Throw<NullReferenceException>();

            factoryMock.Verify(x => x.GetCrmDataMigrator(dataFormat, It.IsAny<ILogger>(), It.IsAny<EntityRepository>(), It.IsAny<CrmExporterConfig>(), It.IsAny<CancellationToken>(), It.IsAny<CrmSchemaConfiguration>()), Times.Once);
        }

        [TestMethod]
        public void ExportDataAsJsonExportConfigPathIsNull()
        {
            var dataFormat = DataFormat.Json;
            var exportSettings = new ExportSettings
            {
                // this is not really unit test but it is the quckest way to get this tested as CrmSchemaConfiguration.ReadFromFile actually looks for the file!
                SchemaPath = "TestData/ContactSchemaWithOwner.xml",
                DataFormat = dataFormat,
                BatchSize = 5000,
                ExportConfigPath = null,
                SavePath = "TestData"
            };

            var storeReader = new Mock<IDataStoreReader<Entity, EntityWrapper>>().Object;
            var storeWriter = new Mock<IDataStoreWriter<Entity, EntityWrapper>>().Object;

            var genericCrmDataMigrator = new GenericCrmDataMigrator(loggerMock.Object, storeReader, storeWriter);

            migratorFactoryMock.Setup(x => x.GetCrmDataMigrator(
                                                                dataFormat,
                                                                It.IsAny<ILogger>(),
                                                                It.IsAny<EntityRepository>(),
                                                                It.IsAny<CrmExporterConfig>(),
                                                                It.IsAny<CancellationToken>(),
                                                                It.IsAny<CrmSchemaConfiguration>()))
                               .Returns(genericCrmDataMigrator)
                               .Verifiable();

            FluentActions.Invoking(() => systemUnderTest.ExportData(exportSettings))
                         .Should()
                         .Throw<NullReferenceException>();

            migratorFactoryMock.Verify(x => x.GetCrmDataMigrator(dataFormat, It.IsAny<ILogger>(), It.IsAny<EntityRepository>(), It.IsAny<CrmExporterConfig>(), It.IsAny<CancellationToken>(), It.IsAny<CrmSchemaConfiguration>()), Times.Once);
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

            migratorFactoryMock.Setup(x => x.GetCrmDataMigrator(
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

            migratorFactoryMock.Verify(x => x.GetCrmDataMigrator(
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

            migratorFactoryMock.Setup(x => x.GetCrmDataMigrator(
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

            migratorFactoryMock.Verify(x => x.GetCrmDataMigrator(
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

            migratorFactoryMock.Setup(x => x.GetCrmDataMigrator(
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

            migratorFactoryMock.Setup(x => x.GetCrmImportDataMigrator(
                        DataFormat.Json,
                        loggerMock.Object,
                        It.IsAny<EntityRepository>(),
                        importConfig,
                        It.IsAny<CancellationToken>(),
                        MockCrmSchemaConfig.Object))
                    .Returns(mockGenericCrmDataMigrator.Object)
                    .Verifiable();

            FluentActions.Invoking(() => systemUnderTest.ImportData(mockOrganisationService.Object, DataFormat.Json, MockCrmSchemaConfig.Object, importConfig))
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

            migratorFactoryMock.Setup(x => x.GetCrmImportDataMigrator(
                        DataFormat.Csv,
                        loggerMock.Object,
                        It.IsAny<EntityRepository>(),
                        importConfig,
                        It.IsAny<CancellationToken>(),
                        MockCrmSchemaConfig.Object))
                    .Returns(mockGenericCrmDataMigrator.Object)
                    .Verifiable();

            FluentActions.Invoking(() => systemUnderTest.ImportData(mockOrganisationService.Object, DataFormat.Csv, MockCrmSchemaConfig.Object, importConfig))
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
        public void CancelDataExportShouldNotThrowExceptionEvenIfCancellationTokenSourceIsNull()
        {
            FluentActions.Invoking(() => systemUnderTest.CancelDataExport())
                         .Should()
                         .NotThrow();
        }

        [TestMethod]
        public void CancelDataExport()
        {
            var dataFormat = DataFormat.Json;
            var exportSettings = new ExportSettings
            {
                // this is not really unit test but it is the quckest way to get this tested as CrmSchemaConfiguration.ReadFromFile actually looks for the file!
                SchemaPath = "TestData/ContactSchemaWithOwner.xml",
                DataFormat = dataFormat,
                BatchSize = 5000,
                ExportConfigPath = "TestData",
                SavePath = "TestData"
            };

            var storeReader = new Mock<IDataStoreReader<Entity, EntityWrapper>>().Object;
            var storeWriter = new Mock<IDataStoreWriter<Entity, EntityWrapper>>().Object;

            var genericCrmDataMigrator = new GenericCrmDataMigrator(loggerMock.Object, storeReader, storeWriter);

            migratorFactoryMock.Setup(x => x.GetCrmDataMigrator(
                                                                dataFormat,
                                                                It.IsAny<ILogger>(),
                                                                It.IsAny<EntityRepository>(),
                                                                It.IsAny<CrmExporterConfig>(),
                                                                It.IsAny<CancellationToken>(),
                                                                It.IsAny<CrmSchemaConfiguration>()))
                               .Returns(genericCrmDataMigrator)
                               .Verifiable();

            try
            {
                systemUnderTest.ExportData(exportSettings);
            }
            catch (Exception)
            {
                // lets handle any exception here as we really want to test if the cancellation method can be invoked!
                // we expect the CancellationTokenSource to be initialized within the exportdata method prior ro actually doing any real work;
            }

            FluentActions.Invoking(() => systemUnderTest.CancelDataExport())
                         .Should()
                         .NotThrow();
        }
    }
}