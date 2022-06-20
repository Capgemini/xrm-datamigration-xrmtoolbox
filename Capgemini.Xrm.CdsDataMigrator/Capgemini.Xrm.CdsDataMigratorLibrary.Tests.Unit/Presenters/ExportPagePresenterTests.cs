using Capgemini.Xrm.CdsDataMigratorLibrary.Presenters;
using Capgemini.Xrm.DataMigration.CrmStore.Config;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;

namespace Capgemini.Xrm.CdsDataMigrator.Tests.Unit.Presenters
{
    [TestClass]
    public class ExportPagePresenterTests
    {
        private Mock<IExportPageView> exportView;
        private ExportPagePresenter systemUnderTest;

        [TestInitialize]
        public void TestSetup()
        {
            exportView = new Mock<IExportPageView>();

            systemUnderTest = new ExportPagePresenter(exportView.Object);
        }

        [TestMethod]
        public void Contructor_ShouldSetDefaultConfigProperties()
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
            exportView
                .Setup(x => x.AskForFilePathToOpen())
                .Returns(exportConfigFilePath);

            // Act
            systemUnderTest.LoadConfig();

            // Assert
            exportView.VerifyAll();
            VerifyViewPropertiesSet(exportConfig);
        }

        [TestMethod]
        public void LoadConfig_ShouldDoNothing_WhenInvalidFilePathSelected()
        {
            // Arrange
            exportView
                .Setup(x => x.AskForFilePathToOpen())
                .Returns("a-random-non-existent-file");

            // Act
            systemUnderTest.LoadConfig();

            // Assert
            exportView.VerifyAll();
            VerifyViewPropertiesNotSet();
        }

        [TestMethod]
        public void LoadConfig_ShouldDoNothing_WhenEmptyFilePathSelected()
        {
            // Arrange
            exportView
                .Setup(x => x.AskForFilePathToOpen())
                .Returns("");

            // Act
            systemUnderTest.LoadConfig();

            // Assert
            exportView.VerifyAll();
            VerifyViewPropertiesNotSet();
        }

        [TestMethod]
        public void SaveConfig_ShouldUpdateOrCreateConfigFile_WhenValidFilePathSelected()
        {
            // Arrange
            var exportConfigFilePath = @"TestData\NewExportConfig.json";
            exportView.SetupGet(x => x.PageSize).Returns(1000);
            exportView.SetupGet(x => x.BatchSize).Returns(2000);
            exportView.SetupGet(x => x.TopCount).Returns(3000);
            exportView.SetupGet(x => x.CrmMigrationToolSchemaPath).Returns(@"C:\\Some\Path\To\A\Schema.xml");
            exportView
                .Setup(x => x.AskForFilePathToSave(null))
                .Returns(exportConfigFilePath);

            // Act
            systemUnderTest.SaveConfig();

            // Assert
            exportView.VerifyAll();
            var exportConfig = CrmExporterConfig.GetConfiguration(exportConfigFilePath);
            exportConfig.PageSize.Should().Be(1000);
            exportConfig.BatchSize.Should().Be(2000);
            exportConfig.TopCount.Should().Be(3000);
            exportConfig.CrmMigrationToolSchemaPaths.Count.Should().Be(1);
            exportConfig.CrmMigrationToolSchemaPaths.FirstOrDefault().Should().Be(@"C:\\Some\Path\To\A\Schema.xml");
        }

        [TestMethod]
        [Ignore("What is an invalid file?")]
        public void SaveConfig_ShouldDoNothing_WhenInvalidFilePathSelected()
        {
            // Arrange
            exportView
                .Setup(x => x.AskForFilePathToSave(null))
                .Returns("a-random-non-existent-file");

            // Act
            systemUnderTest.SaveConfig();

            // Assert
            exportView.VerifyAll();
        }

        [TestMethod]
        public void SaveConfig_ShouldDoNothing_WhenEmptyFilePathSelected()
        {
            // Arrange
            exportView
                .Setup(x => x.AskForFilePathToSave(null))
                .Returns("");

            // Act
            systemUnderTest.SaveConfig();

            // Assert
            exportView.VerifyAll();
        }

        [TestMethod]
        public void SaveConfig_ShouldReuseLoadedFilePath()
        {
            // Arrange
            var exportConfigFilePath = @"TestData\NewExportConfig.json";
            var exportConfig = CrmExporterConfig.GetConfiguration(exportConfigFilePath);
            exportView
                .Setup(x => x.AskForFilePathToOpen())
                .Returns(exportConfigFilePath);
            exportView
                .Setup(x => x.AskForFilePathToSave(exportConfigFilePath))
                .Returns(exportConfigFilePath);

            // Act
            systemUnderTest.LoadConfig();
            systemUnderTest.SaveConfig();

            // Assert
            exportView.VerifyAll();
        }

        private void VerifyViewPropertiesSet(CrmExporterConfig exportConfig)
        {
            exportView.VerifySet(x => x.PageSize = exportConfig.PageSize, "Page size does not match config");
            exportView.VerifySet(x => x.BatchSize = exportConfig.BatchSize, "Batch size does not match config");
            exportView.VerifySet(x => x.TopCount = exportConfig.TopCount, "Top count does not match config");
            exportView.VerifySet(x => x.CrmMigrationToolSchemaPath = exportConfig.CrmMigrationToolSchemaPaths.FirstOrDefault(), "Schema Path does not match config");
        }

        private void VerifyViewPropertiesNotSet()
        {
            // One time is expected in the constuctor, not after the file is loaded.
            exportView.VerifySet(x => x.PageSize = It.IsAny<int>(), Times.Once, "Page size was set unexpectedly");
            exportView.VerifySet(x => x.BatchSize = It.IsAny<int>(), Times.Once, "Batch size was set unexpectedly");
            exportView.VerifySet(x => x.TopCount = It.IsAny<int>(), Times.Once, "Top count was set unexpectedly");
            exportView.VerifySet(x => x.CrmMigrationToolSchemaPath = It.IsAny<string>(), Times.Once, "Schema path was set unexpectedly");
        }
    }
}