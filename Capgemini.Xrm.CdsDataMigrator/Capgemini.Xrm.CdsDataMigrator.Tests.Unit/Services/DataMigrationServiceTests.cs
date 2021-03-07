using System;
using System.Threading;
using Capgemini.DataMigration.Core;
using Capgemini.Xrm.DataMigration.Config;
using Capgemini.Xrm.DataMigration.Core;
using Capgemini.Xrm.DataMigration.CrmStore.Config;
using Capgemini.Xrm.DataMigration.DataStore;
using Capgemini.Xrm.DataMigration.Repositories;
using Capgemini.Xrm.DataMigration.XrmToolBox.Enums;
using Capgemini.Xrm.DataMigration.XrmToolBox.Services;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Models;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Services;
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
    }
}