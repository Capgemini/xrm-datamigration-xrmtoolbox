using System;
using System.Threading;
using Capgemini.DataMigration.Core;
using Capgemini.Xrm.DataMigration.Config;
using Capgemini.Xrm.DataMigration.Core;
using Capgemini.Xrm.DataMigration.CrmStore.Config;
using Capgemini.Xrm.DataMigration.Repositories;
using Capgemini.Xrm.DataMigration.XrmToolBox.Services;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Models;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Services;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Capgemini.Xrm.XrmToolBoxPluginBase.Tests.Unit.Services
{
    [TestClass]
    public class DataMigrationServiceTests
    {
        private Mock<ILogger> loggerMock;
        private Mock<CrmGenericMigratorFactory> migratorFactoryMock;
        private DataMigrationService systemUnderTest;

        [TestInitialize]
        public void TestSetup()
        {
            loggerMock = new Mock<ILogger>();
            migratorFactoryMock = new Mock<CrmGenericMigratorFactory>();
            systemUnderTest = new DataMigrationService(loggerMock.Object);
        }

        [TestMethod]
        public void DataMigrationServiceIntantiation()
        {
            FluentActions.Invoking(() => new DataMigrationService(loggerMock.Object))
                            .Should()
                            .NotThrow();
        }

        [TestMethod]
        public void ExportDataWithNullExportSettings()
        {
            FluentActions.Invoking(() => systemUnderTest.ExportData(null))
                .Should()
                .Throw<ArgumentNullException>();
        }

        [TestMethod]
        [Ignore("Untestable due to file system access. Requires refactoring.")]
        public void ExportDataWithExportSettings()
        {
            var exportSettings = new ExportSettings
            {
                SchemaPath = string.Empty,
            };

            FluentActions.Invoking(() => systemUnderTest.ExportData(exportSettings))
                .Should()
                .NotThrow();
        }

        [TestMethod]
        //[Ignore("Untestable due to file system access. Requires refactoring.")]
        public void ExportDataAsJson()
        {
            var exportSettings = new ExportSettings
            {
                SchemaPath = string.Empty,
                DataFormat = "json",
            };

            migratorFactoryMock
                .Setup(x => x.GetCrmDataMigrator(
                    "json",
                    It.IsAny<ILogger>(),
                    It.IsAny<EntityRepository>(),
                    It.IsAny<CrmExporterConfig>(),
                    It.IsAny<CancellationToken>(),
                    It.IsAny<CrmSchemaConfiguration>()))
                .Returns(new Mock<GenericCrmDataMigrator>().Object)
                .Verifiable();

            FluentActions.Invoking(() => systemUnderTest.ExportData(exportSettings))
                .Should()
                .NotThrow();

            migratorFactoryMock.Verify();
        }

        [TestMethod]
        //[Ignore("Untestable due to file system access. Requires refactoring.")]
        public void ExportDataAsCsv()
        {
            var exportSettings = new ExportSettings
            {
                SchemaPath = string.Empty,
                DataFormat = "csv",
            };

            migratorFactoryMock
                .Setup(x => x.GetCrmDataMigrator(
                    "csv",
                    It.IsAny<ILogger>(),
                    It.IsAny<EntityRepository>(),
                    It.IsAny<CrmExporterConfig>(),
                    It.IsAny<CancellationToken>(),
                    It.IsAny<CrmSchemaConfiguration>()))
                .Returns(new Mock<GenericCrmDataMigrator>().Object)
                .Verifiable();

            FluentActions.Invoking(() => systemUnderTest.ExportData(exportSettings))
                .Should()
                .NotThrow();

            migratorFactoryMock.Verify();
        }
    }
}
