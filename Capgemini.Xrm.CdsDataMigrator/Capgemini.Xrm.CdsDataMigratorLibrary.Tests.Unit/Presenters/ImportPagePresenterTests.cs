using Capgemini.Xrm.CdsDataMigratorLibrary.Presenters;
using Capgemini.Xrm.CdsDataMigratorLibrary.Services;
using Capgemini.Xrm.DataMigration.Config;
using Capgemini.Xrm.DataMigration.CrmStore.Config;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Moq;
using System;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Tests.Unit.Presenters
{
    [TestClass]
    public class ImportPresenterTests
    {
        private Mock<IImportPageView> mockImportView;
        private Mock<IWorkerHost> mockWorkerHost;
        private Mock<IDataMigrationService> mockDataMigrationService;
        private ImportPagePresenter systemUnderTest;

        [TestInitialize]
        public void TestSetup()
        {
            mockImportView = new Mock<IImportPageView>();
            mockWorkerHost = new Mock<IWorkerHost>();
            mockDataMigrationService = new Mock<IDataMigrationService>();

            systemUnderTest = new ImportPagePresenter(mockImportView.Object, mockWorkerHost.Object, mockDataMigrationService.Object);
        }

        [TestMethod]
        public void Constructor_ShouldSetDefaultConfigProperties()
        {
            // Arrange
            var importConfig = new CrmImportConfig();

            // Assert
            VerifyViewPropertiesSet(importConfig);
        }

        [TestMethod]
        public void LoadConfig_ShouldSetConfigProperties_WhenValidFilePathSelected()
        {
            // Arrange
            var importConfigFilePath = @"TestData\ImportConfig.json";
            var importConfig = CrmImportConfig.GetConfiguration(importConfigFilePath); ;
            mockImportView
                .Setup(x => x.AskForFilePathToOpen())
                .Returns(importConfigFilePath);

            // Act
            mockImportView.Raise(x => x.LoadConfigClicked += null, EventArgs.Empty);

            // Assert
            mockImportView.VerifyAll();
            VerifyViewPropertiesSet(importConfig);
        }

        [TestMethod]
        public void LoadConfig_ShouldDoNothing_WhenInvalidFilePathSelected()
        {
            // Arrange
            mockImportView
                .Setup(x => x.AskForFilePathToOpen())
                .Returns("$a-random-non-existent-file$");

            // Act
            mockImportView.Raise(x => x.LoadConfigClicked += null, EventArgs.Empty);

            // Assert
            mockImportView.VerifyAll();
            VerifyViewPropertiesNotSet();
        }

        [TestMethod]
        public void LoadConfig_ShouldDoNothing_WhenEmptyFilePathSelected()
        {
            // Arrange
            mockImportView
                .Setup(x => x.AskForFilePathToOpen())
                .Returns("");

            // Act
            mockImportView.Raise(x => x.LoadConfigClicked += null, EventArgs.Empty);

            // Assert
            mockImportView.VerifyAll();
            VerifyViewPropertiesNotSet();
        }

        [TestMethod]
        public void SaveConfig_ShouldUpdateOrCreateConfigFile_WhenValidFilePathSelected()
        {
            var importConfigFilePath = @"TestData\NewImportConfig.json";
            mockImportView.SetupGet(x => x.SaveBatchSize).Returns(1000);
            mockImportView.SetupGet(x => x.IgnoreStatuses).Returns(true);
            mockImportView.SetupGet(x => x.IgnoreSystemFields).Returns(true);
            mockImportView.SetupGet(x => x.JsonFolderPath).Returns(@"C:\\Some\Path\To\A\Folder");
            mockImportView
                .Setup(x => x.AskForFilePathToSave(null))
                .Returns(importConfigFilePath);

            // Act
            mockImportView.Raise(x => x.SaveConfigClicked +=null, EventArgs.Empty);

            // Assert
            mockImportView.VerifyAll();
            var importConfig = CrmImportConfig.GetConfiguration(importConfigFilePath);
            importConfig.SaveBatchSize.Should().Be(1000);
            importConfig.IgnoreStatuses.Should().Be(true);
            importConfig.IgnoreSystemFields.Should().Be(true);
            importConfig.JsonFolderPath.Should().Be(@"C:\\Some\Path\To\A\Folder");
        }


        [TestMethod]
        //[Ignore("What is an invalid file?")] -- Check to see if this test should be included
        public void SaveConfig_ShouldDoNothing_WhenInvalidFilePathSelected()
        {
            // Arrange
            mockImportView
                .Setup(x => x.AskForFilePathToSave(null))
                .Returns("a-random-non-existent-file");

            // Act
            mockImportView.Raise(x => x.SaveConfigClicked +=null, EventArgs.Empty);

            //Assert
            mockImportView.VerifyAll();
        }

        [TestMethod]
        public void SaveConfig_ShouldDoNothing_whenEmptyFilePathSelected()
        {
            // Arrange
            mockImportView
                .Setup(x => x.AskForFilePathToSave(null))
                .Returns("");

            // Act
            mockImportView.Raise(x => x.SaveConfigClicked += null, EventArgs.Empty);

            // Assert
            mockImportView.VerifyAll();
        }

        [TestMethod]
        public void SaveConfig_ShouldReuseLoadedFilePath()
        {
            // Arrange
            var importConfigFilePath  = @"TestData\NewImportConfig.json";
            var importConfig = CrmImportConfig.GetConfiguration(importConfigFilePath);
            mockImportView
                .Setup(x => x.AskForFilePathToOpen())
                .Returns(importConfigFilePath);

            // Act
            mockImportView.Raise(x => x.LoadConfigClicked += null, EventArgs.Empty);
            mockImportView.Raise(x => x.SaveConfigClicked += null, EventArgs.Empty);

            // Assert
            mockImportView.VerifyAll();
        }

        [TestMethod]
        public void RunConfig_ShouldReadValuesFromView()
        {
            // Arrange
            var mockIOrganisationService = new Mock<IOrganizationService>();

            mockImportView.SetupGet(x => x.SaveBatchSize).Returns(1000);
            mockImportView.SetupGet(x => x.IgnoreStatuses).Returns(true);
            mockImportView.SetupGet(x => x.IgnoreSystemFields).Returns(true);
            mockImportView.SetupGet(x => x.JsonFolderPath).Returns(@"C:\\Some\Path\To\A\Folder");
            mockImportView.SetupGet(x => x.DataFormat).Returns(CdsDataMigratorLibrary.Enums.DataFormat.Json);
            mockImportView.SetupGet(x => x.Service).Returns(mockIOrganisationService.Object);

            // Act
            mockImportView.Raise(x => x.RunConfigClicked += null, EventArgs.Empty);

            var workInfo = mockWorkerHost.Invocations[0].Arguments[0].As<WorkAsyncInfo>();
            workInfo.Work(null, null);
            
            // Assert
            mockImportView.VerifyAll();
            workInfo.Message.Should().Be("Importing data...");
            mockDataMigrationService.Verify(x => x.ImportData(mockIOrganisationService.Object, Enums.DataFormat.Json, It.IsAny<CrmSchemaConfiguration>(), It.IsAny<CrmImportConfig>()));

            var importConfig = mockDataMigrationService.Invocations[0].Arguments[3].As<CrmImportConfig>();

            importConfig.SaveBatchSize.Should().Be(1000);
            importConfig.IgnoreStatuses.Should().Be(true);
            importConfig.IgnoreSystemFields.Should().Be(true);
            importConfig.JsonFolderPath.Should().Be(@"C:\\Some\Path\To\A\Folder");
        }

        [TestMethod]
        public void GetSchemaConfiguration_ShouldReturnNullWhenCrmMigrationToolSchemaPathIsNull()
        {
            // Arrange
            mockImportView
                .SetupGet(x => x.CrmMigrationToolSchemaPath)
                .Returns(() => null);

            // Act
            var result = systemUnderTest.GetSchemaConfiguration();

            // Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public void GetSchemaConfiguration_ShouldReturnNullWhenCrmMigrationToolSchemaPathIsEmpty()
        {
            // Arrange
            mockImportView
                .SetupGet(x => x.CrmMigrationToolSchemaPath)
                .Returns(" ");

            // Act
            var result = systemUnderTest.GetSchemaConfiguration();

            // Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public void GetSchemaConfiguration_ShouldReturnNullWhenCrmMigrationToolSchemaPathIsInvalid()
        {
            // Arrange
            mockImportView
                .SetupGet(x => x.CrmMigrationToolSchemaPath)
                .Returns("a-random-non-existent-file");

            // Act
            var result = systemUnderTest.GetSchemaConfiguration();

            // Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public void GetSchemaConfiguration_ShouldReturnSchemaWhenCrmMigrationToolSchemaPathIsValid()
        {
            // Arrange
            var filePath = @"TestData\BusinessUnitSchema.xml";
            mockImportView
                .SetupGet(x => x.CrmMigrationToolSchemaPath)
                .Returns(filePath);

            // Act
            var result = systemUnderTest.GetSchemaConfiguration();

            // Assert
            result.Should().BeEquivalentTo(CrmSchemaConfiguration.ReadFromFile(filePath));
        }


        private void VerifyViewPropertiesSet(CrmImportConfig ImportConfig)
        {
            mockImportView.VerifySet(x => x.SaveBatchSize = ImportConfig.SaveBatchSize, "Batch Size does not match config");
            mockImportView.VerifySet(x => x.IgnoreStatuses = ImportConfig.IgnoreStatuses, "IgnoreStatuses does not match config");
            mockImportView.VerifySet(x => x.IgnoreSystemFields = ImportConfig.IgnoreSystemFields, "IgnoreSystemFields does not match config");
            mockImportView.VerifySet(x => x.JsonFolderPath = ImportConfig.JsonFolderPath, "JsonFolderPath does not match config");
        }

        private void VerifyViewPropertiesNotSet()
        {
            // One time is expected in the constuctor, not after the file is loaded.
            mockImportView.VerifySet(x => x.SaveBatchSize = It.IsAny<int>(), Times.Once, "Page size was set unexpectedly");
            mockImportView.VerifySet(x => x.IgnoreStatuses = It.IsAny<bool>(), Times.Once, "IgnoreStatusese was set unexpectedly");
            mockImportView.VerifySet(x => x.IgnoreSystemFields = It.IsAny<bool>(), Times.Once, "IgnoreSystemFields was set unexpectedly");
            mockImportView.VerifySet(x => x.JsonFolderPath = It.IsAny<string>(), Times.Once, "JsonFolderPathh was set unexpectedly");
        }
    }  
}
