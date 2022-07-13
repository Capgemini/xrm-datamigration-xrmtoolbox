using System;
using System.IO;
using System.Threading;
using Capgemini.DataMigration.Core;
using Capgemini.Xrm.CdsDataMigratorLibrary.Enums;
using Capgemini.Xrm.CdsDataMigratorLibrary.Services;
using Capgemini.Xrm.DataMigration.Config;
using Capgemini.Xrm.DataMigration.Core;
using Capgemini.Xrm.DataMigration.CrmStore.Config;
using Capgemini.Xrm.DataMigration.Engine;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Capgemini.Xrm.CdsDataMigrator.Tests.Unit.Services
{
    [TestClass]
    public class CrmGenericMigratorFactoryTests
    {
        private CrmGenericMigratorFactory systemUnderTest;

        [TestInitialize]
        public void TestSetup()
        {
            systemUnderTest = new CrmGenericMigratorFactory();
        }

        [TestMethod]
        public void CrmGenericMigratorIntantiation()
        {
            FluentActions.Invoking(() => new CrmGenericMigratorFactory())
                            .Should()
                            .NotThrow();
        }

        [TestMethod]
        public void RequestJsonMigrator()
        {
            var logger = new Mock<ILogger>().Object;
            var entityRepoMock = new Mock<IEntityRepository>();
            entityRepoMock.SetupGet(x => x.GetEntityMetadataCache).Returns(new Mock<IEntityMetadataCache>().Object);
            var exportConfig = new CrmExporterConfig
            {
                JsonFolderPath = Path.Combine(Environment.CurrentDirectory, "temp")
            };
            var cancellationToken = CancellationToken.None;
            var schema = new CrmSchemaConfiguration();

            var migrator = systemUnderTest.GetCrmDataMigrator(DataFormat.Json, logger, entityRepoMock.Object, exportConfig, cancellationToken, schema);

            migrator.Should().BeOfType<CrmFileDataExporter>();
        }

        [TestMethod]
        public void RequestCSVMigrator()
        {
            var logger = new Mock<ILogger>().Object;
            var entityRepoMock = new Mock<IEntityRepository>();
            entityRepoMock.SetupGet(x => x.GetEntityMetadataCache).Returns(new Mock<IEntityMetadataCache>().Object);
            var exportConfig = new CrmExporterConfig
            {
                JsonFolderPath = Path.Combine(Environment.CurrentDirectory, "temp")
            };
            var cancellationToken = CancellationToken.None;
            var schema = new CrmSchemaConfiguration();
            schema.Entities.AddRange(new DataMigration.Model.CrmEntity[] { new DataMigration.Model.CrmEntity { } });

            var migrator = systemUnderTest.GetCrmDataMigrator(DataFormat.Csv, logger, entityRepoMock.Object, exportConfig, cancellationToken, schema);

            migrator.Should().BeOfType<CrmFileDataExporterCsv>();
        }

        [TestMethod]
        public void RequestUnknownMigrator()
        {
            var logger = new Mock<ILogger>().Object;
            var entityRepo = new Mock<IEntityRepository>().Object;
            var exportConfig = new CrmExporterConfig();
            var cancellationToken = CancellationToken.None;
            var schema = new CrmSchemaConfiguration();

            FluentActions.Invoking(() => systemUnderTest.GetCrmDataMigrator(DataFormat.Unknown, logger, entityRepo, exportConfig, cancellationToken, schema))
                .Should()
                .Throw<NotSupportedException>();
        }
    }
}