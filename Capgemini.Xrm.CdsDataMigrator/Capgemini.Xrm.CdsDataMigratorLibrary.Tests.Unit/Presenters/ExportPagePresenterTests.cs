using Capgemini.Xrm.CdsDataMigratorLibrary.Presenters;
using Capgemini.Xrm.CdsDataMigratorLibrary.Services;
using Capgemini.Xrm.DataMigration.CrmStore.Config;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Moq;
using System;
using System.Linq;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

namespace Capgemini.Xrm.CdsDataMigrator.Tests.Unit.Presenters
{
    [TestClass]
    public class ExportPagePresenterTests
    {
        private Mock<IExportPageView> mockExportView;
        private Mock<IWorkerHost> mockWorkerHost;
        private Mock<IDataMigrationService> mockDataMigrationService;
        private ExportPagePresenter systemUnderTest;

        [TestInitialize]
        public void TestSetup()
        {
            mockExportView = new Mock<IExportPageView>();
            mockWorkerHost = new Mock<IWorkerHost>();
            mockDataMigrationService = new Mock<IDataMigrationService>();

            systemUnderTest = new ExportPagePresenter(mockExportView.Object, mockWorkerHost.Object, mockDataMigrationService.Object);
        }

        [TestMethod]
        public void Constructor_ShouldSetDefaultConfigProperties()
        {
            // Arrange
            var exportConfig = new CrmExporterConfig();

            // Assert
            VerifyViewPropertiesSet(exportConfig);
        }

        [TestMethod]
        public void LoadConfig_ShouldSetConfigProperties_WhenValidFilePathSelected()
        {
            // Arrange
            var exportConfigFilePath = @"TestData\ExportConfig.json";
            var exportConfig = CrmExporterConfig.GetConfiguration(exportConfigFilePath);
            mockExportView
                .Setup(x => x.AskForFilePathToOpen())
                .Returns(exportConfigFilePath);

            // Act
            systemUnderTest.LoadConfig();

            // Assert
            mockExportView.VerifyAll();
            VerifyViewPropertiesSet(exportConfig);
        }

        [TestMethod]
        public void LoadConfig_ShouldDoNothing_WhenInvalidFilePathSelected()
        {
            // Arrange
            mockExportView
                .Setup(x => x.AskForFilePathToOpen())
                .Returns("a-random-non-existent-file");

            // Act
            systemUnderTest.LoadConfig();

            // Assert
            mockExportView.VerifyAll();
            VerifyViewPropertiesNotSet();
        }

        [TestMethod]
        public void LoadConfig_ShouldDoNothing_WhenEmptyFilePathSelected()
        {
            // Arrange
            mockExportView
                .Setup(x => x.AskForFilePathToOpen())
                .Returns("");

            // Act
            systemUnderTest.LoadConfig();

            // Assert
            mockExportView.VerifyAll();
            VerifyViewPropertiesNotSet();
        }

        [TestMethod]
        public void SaveConfig_ShouldUpdateOrCreateConfigFile_WhenValidFilePathSelected()
        {
            // Arrange
            var exportConfigFilePath = @"TestData\NewExportConfig.json";
            mockExportView.SetupGet(x => x.PageSize).Returns(1000);
            mockExportView.SetupGet(x => x.BatchSize).Returns(2000);
            mockExportView.SetupGet(x => x.TopCount).Returns(3000);
            mockExportView.SetupGet(x => x.OnlyActiveRecords).Returns(true);
            mockExportView.SetupGet(x => x.OneEntityPerBatch).Returns(false);
            mockExportView.SetupGet(x => x.CrmMigrationToolSchemaPath).Returns(@"C:\\Some\Path\To\A\Schema.xml");
            mockExportView
                .Setup(x => x.AskForFilePathToSave(null))
                .Returns(exportConfigFilePath);

            // Act
            systemUnderTest.SaveConfig();

            // Assert
            mockExportView.VerifyAll();
            var exportConfig = CrmExporterConfig.GetConfiguration(exportConfigFilePath);
            exportConfig.PageSize.Should().Be(1000);
            exportConfig.BatchSize.Should().Be(2000);
            exportConfig.TopCount.Should().Be(3000);
            exportConfig.OnlyActiveRecords.Should().Be(true);
            exportConfig.OneEntityPerBatch.Should().Be(false);
            exportConfig.CrmMigrationToolSchemaPaths.Count.Should().Be(1);
            exportConfig.CrmMigrationToolSchemaPaths.FirstOrDefault().Should().Be(@"C:\\Some\Path\To\A\Schema.xml");
        }

        [TestMethod]
        [Ignore("What is an invalid file?")]
        public void SaveConfig_ShouldDoNothing_WhenInvalidFilePathSelected()
        {
            // Arrange
            mockExportView
                .Setup(x => x.AskForFilePathToSave(null))
                .Returns("a-random-non-existent-file");

            // Act
            systemUnderTest.SaveConfig();

            // Assert
            mockExportView.VerifyAll();
        }

        [TestMethod]
        public void SaveConfig_ShouldDoNothing_WhenEmptyFilePathSelected()
        {
            // Arrange
            mockExportView
                .Setup(x => x.AskForFilePathToSave(null))
                .Returns("");

            // Act
            systemUnderTest.SaveConfig();

            // Assert
            mockExportView.VerifyAll();
        }

        [TestMethod]
        public void SaveConfig_ShouldReuseLoadedFilePath()
        {
            // Arrange
            var exportConfigFilePath = @"TestData\NewExportConfig.json";
            var exportConfig = CrmExporterConfig.GetConfiguration(exportConfigFilePath);
            mockExportView
                .Setup(x => x.AskForFilePathToOpen())
                .Returns(exportConfigFilePath);
            mockExportView
                .Setup(x => x.AskForFilePathToSave(exportConfigFilePath))
                .Returns(exportConfigFilePath);

            // Act
            systemUnderTest.LoadConfig();
            systemUnderTest.SaveConfig();

            // Assert
            mockExportView.VerifyAll();
        }

        [TestMethod]
        public void RunConfig_ShouldReadValuesFromView()
        {
            // Arrange
            var mockIOrganisationService = new Mock<IOrganizationService>();
            mockExportView.SetupGet(x => x.PageSize).Returns(1000);
            mockExportView.SetupGet(x => x.BatchSize).Returns(2000);
            mockExportView.SetupGet(x => x.TopCount).Returns(3000);
            mockExportView.SetupGet(x => x.OnlyActiveRecords).Returns(true);
            mockExportView.SetupGet(x => x.OneEntityPerBatch).Returns(false);
            mockExportView.SetupGet(x => x.SeperateFilesPerEntity).Returns(false);
            mockExportView.SetupGet(x => x.DataFormat).Returns(CdsDataMigratorLibrary.Enums.DataFormat.Json);
            mockExportView.SetupGet(x => x.CrmMigrationToolSchemaPath).Returns(@"C:\\Some\Path\To\A\Schema.xml");
            mockExportView.SetupGet(x => x.JsonFolderPath).Returns(@"C:\\Some\Path\To\A\Folder");
            mockExportView.SetupGet(x => x.Service).Returns(mockIOrganisationService.Object);

            // Act
            systemUnderTest.RunConfig();
            var workInfo = mockWorkerHost.Invocations[0].Arguments[0].As<WorkAsyncInfo>();
            workInfo.Work(null, null);

            // Assert
            mockExportView.VerifyAll();
            workInfo.Message.Should().Be("Exporting data...");
            mockDataMigrationService.Verify(x => x.ExportData(mockIOrganisationService.Object, CdsDataMigratorLibrary.Enums.DataFormat.Json, It.IsAny<CrmExporterConfig>()));
            var exportConfig = mockDataMigrationService.Invocations[0].Arguments[2].As<CrmExporterConfig>();
            exportConfig.PageSize.Should().Be(1000);
            exportConfig.BatchSize.Should().Be(2000);
            exportConfig.TopCount.Should().Be(3000);
            exportConfig.OnlyActiveRecords.Should().Be(true);
            exportConfig.OneEntityPerBatch.Should().Be(false);
            exportConfig.SeperateFilesPerEntity.Should().Be(false);
            exportConfig.CrmMigrationToolSchemaPaths.Count.Should().Be(1);
            exportConfig.CrmMigrationToolSchemaPaths.FirstOrDefault().Should().Be(@"C:\\Some\Path\To\A\Schema.xml");
        }

        private void VerifyViewPropertiesSet(CrmExporterConfig exportConfig)
        {
            mockExportView.VerifySet(x => x.PageSize = exportConfig.PageSize, "Page size does not match config");
            mockExportView.VerifySet(x => x.BatchSize = exportConfig.BatchSize, "Batch size does not match config");
            mockExportView.VerifySet(x => x.TopCount = exportConfig.TopCount, "Top count does not match config");
            mockExportView.VerifySet(x => x.CrmMigrationToolSchemaPath = exportConfig.CrmMigrationToolSchemaPaths.FirstOrDefault(), "Schema Path does not match config");
            mockExportView.VerifySet(x => x.OnlyActiveRecords = exportConfig.OnlyActiveRecords, "Only active records does not match config");
            mockExportView.VerifySet(x => x.OneEntityPerBatch = exportConfig.OneEntityPerBatch, "One entity per batch does not match config");
            mockExportView.VerifySet(x => x.SeperateFilesPerEntity = exportConfig.OneEntityPerBatch, "Separate files per entity does not match config");
        }

        private void VerifyViewPropertiesNotSet()
        {
            // One time is expected in the constuctor, not after the file is loaded.
            mockExportView.VerifySet(x => x.PageSize = It.IsAny<int>(), Times.Once, "Page size was set unexpectedly");
            mockExportView.VerifySet(x => x.BatchSize = It.IsAny<int>(), Times.Once, "Batch size was set unexpectedly");
            mockExportView.VerifySet(x => x.TopCount = It.IsAny<int>(), Times.Once, "Top count was set unexpectedly");
            mockExportView.VerifySet(x => x.CrmMigrationToolSchemaPath = It.IsAny<string>(), Times.Once, "Schema path was set unexpectedly");
            mockExportView.VerifySet(x => x.OnlyActiveRecords = It.IsAny<bool>(), Times.Once, "Only active records was set unexpectedly");
            mockExportView.VerifySet(x => x.OneEntityPerBatch = It.IsAny<bool>(), Times.Once, "One entity per batch was set unexpectedly");
            mockExportView.VerifySet(x => x.SeperateFilesPerEntity = It.IsAny<bool>(), Times.Once, "Separate files per entity was set unexpectedly");
        }
    }
}