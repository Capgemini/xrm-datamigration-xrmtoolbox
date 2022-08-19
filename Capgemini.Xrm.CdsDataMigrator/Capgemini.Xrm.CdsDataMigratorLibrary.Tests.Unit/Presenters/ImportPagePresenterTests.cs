using Capgemini.Xrm.CdsDataMigratorLibrary.Enums;
using Capgemini.Xrm.CdsDataMigratorLibrary.Presenters;
using Capgemini.Xrm.CdsDataMigratorLibrary.Services;
using Capgemini.Xrm.CdsDataMigratorLibrary.Tests.Unit.Extensions;
using Capgemini.Xrm.DataMigration.Config;
using Capgemini.Xrm.DataMigration.CrmStore.Config;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Moq;
using NuGet;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
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
        private Mock<INotifier> mockNotifier;
        private ImportPagePresenter systemUnderTest;

        [TestInitialize]
        public void TestSetup()
        {
            mockImportView = new Mock<IImportPageView>();
            mockWorkerHost = new Mock<IWorkerHost>();
            mockDataMigrationService = new Mock<IDataMigrationService>();
            mockNotifier = new Mock<INotifier>();

            systemUnderTest = new ImportPagePresenter(mockImportView.Object, mockWorkerHost.Object, mockDataMigrationService.Object, mockNotifier.Object);
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
        public void LoadConfig_ShouldSetConfigPropertiesWhenValidFilePathSelected()
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
        public void LoadConfig_ShouldDoNothingWhenInvalidFilePathSelected()
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
        public void LoadConfig_ShouldDoNothingWhenEmptyFilePathSelected()
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
        public void LoadConfig_ShouldNotifyExceptionWhenAnExceptionIsThrown()
        {
            // Arrange
            var thrownException = new Exception("Test exception");
            mockImportView
                .Setup(x => x.AskForFilePathToOpen())
                .Throws(thrownException);

            // Act
            mockImportView.Raise(x => x.LoadConfigClicked += null, EventArgs.Empty);

            // Assert
            mockImportView.VerifyAll();
            mockNotifier.Verify(x => x.ShowError(thrownException));
        }

        [TestMethod]
        public void SaveConfig_ShouldUpdateOrCreateConfigFileWhenValidFilePathSelected()
        {
            // Arrange
            var importConfigFilePath = @"TestData\NewImportConfig.json";
            var viewMappings = ProvideMappingsAsViewType();
            var configMappings = ProvideMappingsAsConfigType();

            mockImportView.SetupGet(x => x.SaveBatchSize).Returns(1000);
            mockImportView.SetupGet(x => x.IgnoreStatuses).Returns(true);
            mockImportView.SetupGet(x => x.IgnoreSystemFields).Returns(true);
            mockImportView.SetupGet(x => x.JsonFolderPath).Returns(@"C:\\Some\Path\To\A\Folder");
            mockImportView.SetupGet(x => x.Mappings).Returns(viewMappings);
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
            importConfig.MigrationConfig.Should().BeEquivalentTo(configMappings);
        }

        [TestMethod]
        public void SaveConfig_ShouldNotIncludeRowWithEmptyCellIntheMappings()
        {
            // Arrange
            var importConfigFilePath = @"TestData\NewImportConfig.json";
            var viewMappings = ProvideMappingsAsViewType();
            var newRow = GetRowWithBlankCell();
            viewMappings.Add(newRow);
            var configMappings = ProvideMappingsAsConfigType();
            mockImportView.SetupGet(x => x.Mappings).Returns(viewMappings);
            mockImportView
                .Setup(x => x.AskForFilePathToSave(null))
                .Returns(importConfigFilePath);

            // Act
            mockImportView.Raise(x => x.SaveConfigClicked += null, EventArgs.Empty);

            // Assert
            mockImportView.VerifyAll();
            var importConfig = CrmImportConfig.GetConfiguration(importConfigFilePath);
            importConfig.MigrationConfig.Should().BeEquivalentTo(configMappings);
        }

        [TestMethod]
        public void SaveConfig_ShouldNotIncludeRowWithDefaultIdsIntheMappings()
        {
            // Arrange
            var importConfigFilePath = @"TestData\NewImportConfig.json";
            var viewMappings = ProvideMappingsAsViewType();
            var newRow = GetRowWithDefaultIds();
            viewMappings.Add(newRow);
            var configMappings = ProvideMappingsAsConfigType();
            mockImportView.SetupGet(x => x.Mappings).Returns(viewMappings);
            mockImportView
                .Setup(x => x.AskForFilePathToSave(null))
                .Returns(importConfigFilePath);

            // Act
            mockImportView.Raise(x => x.SaveConfigClicked += null, EventArgs.Empty);

            // Assert
            mockImportView.VerifyAll();
            var importConfig = CrmImportConfig.GetConfiguration(importConfigFilePath);
            importConfig.MigrationConfig.Should().BeEquivalentTo(configMappings);
        }

        [TestMethod]
        public void SaveConfig_ShouldCorrectlyAddNewMappingWhenExistingMappingAlreadyExistsForEntity()
        {
            // Arrange
            var importConfigFilePath = @"TestData\NewImportConfig.json";
            var viewMappings = ProvideMappingsAsViewType();
            var newRow = GetRowWithAccountEntityAndValidGuids();
            viewMappings.Add(newRow);
            var configMappings = ProvideTwoMappingsForSameEntityAsConfigType();
            mockImportView.SetupGet(x => x.Mappings).Returns(viewMappings);
            mockImportView
                .Setup(x => x.AskForFilePathToSave(null))
                .Returns(importConfigFilePath);

            // Act
            mockImportView.Raise(x => x.SaveConfigClicked += null, EventArgs.Empty);

            // Assert
            mockImportView.VerifyAll();
            var importConfig = CrmImportConfig.GetConfiguration(importConfigFilePath);
            importConfig.MigrationConfig.Should().BeEquivalentTo(configMappings);
        }

        [TestMethod]
        [Ignore("What is an invalid file?")]
        // To do: add UI logic that prevents invalid file from being saved
        public void SaveConfig_ShouldDoNothingWhenInvalidFilePathSelected()
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
        public void SaveConfig_ShouldDoNothingWhenEmptyFilePathSelected()
        {
            // Arrange
            var viewMappings = ProvideMappingsAsViewType();
            mockImportView.SetupGet(x => x.Mappings).Returns(viewMappings);
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
            var viewMappings = ProvideMappingsAsViewType();
            mockImportView.SetupGet(x => x.Mappings).Returns(viewMappings);
            mockImportView
                .Setup(x => x.AskForFilePathToOpen())
                .Returns(importConfigFilePath);

            // Act
            mockImportView.Raise(x => x.LoadConfigClicked += null, EventArgs.Empty);
            mockImportView.Raise(x => x.SaveConfigClicked += null, EventArgs.Empty);

            // Assert
            mockImportView.Verify(x => x.AskForFilePathToSave(importConfigFilePath));
        }

        [TestMethod]
        public void SaveConfig_ShouldNotifyExceptionWhenAnExceptionIsThrown()
        {
            // Arrange
            var viewMappings = ProvideMappingsAsViewType();
            mockImportView.SetupGet(x => x.Mappings).Returns(viewMappings);
            var thrownException = new Exception("Test exception");
            mockImportView
                .Setup(x => x.AskForFilePathToSave(null))
                .Throws(thrownException);

            // Act
            mockImportView.Raise(x => x.SaveConfigClicked += null, EventArgs.Empty);

            // Assert
            mockImportView.VerifyAll();
            mockNotifier.Verify(x => x.ShowError(thrownException));
        }

        [TestMethod]
        public void RunConfig_ShouldReadValuesFromView()
        {
            // Arrange
            var mockIOrganisationService = new Mock<IOrganizationService>();
            var viewMappings = ProvideMappingsAsViewType();
            var configMappings = ProvideMappingsAsConfigType();

            mockImportView.SetupGet(x => x.SaveBatchSize).Returns(1000);
            mockImportView.SetupGet(x => x.IgnoreStatuses).Returns(true);
            mockImportView.SetupGet(x => x.IgnoreSystemFields).Returns(true);
            mockImportView.SetupGet(x => x.JsonFolderPath).Returns(@"C:\\Some\Path\To\A\Folder");
            mockImportView.SetupGet(x => x.DataFormat).Returns(Enums.DataFormat.Json);
            mockImportView.SetupGet(x => x.Service).Returns(mockIOrganisationService.Object);
            mockImportView.SetupGet(x => x.Mappings).Returns(viewMappings);

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
            importConfig.MigrationConfig.Should().BeEquivalentTo(configMappings);
        }

        [TestMethod]
        public void RunConfig_ShouldNotifyExceptionWhenAnExceptionIsThrownOutsideWorkerHost()
        {
            // Arrange
            var viewMappings = ProvideMappingsAsViewType();
            mockImportView.SetupGet(x => x.Mappings).Returns(viewMappings);
            var thrownException = new Exception("Test exception");
            mockWorkerHost
                .Setup(x => x.WorkAsync(It.IsAny<WorkAsyncInfo>()))
                .Throws(thrownException);

            // Act
            mockImportView.Raise(x => x.RunConfigClicked += null, EventArgs.Empty);

            // Assert
            mockImportView.VerifyAll();
            mockNotifier.Verify(x => x.ShowError(thrownException));
        }

        [TestMethod]
        public void RunConfig_ShouldNotifyExceptionWhenAnExceptionIsThrownInsideWorkerHost()
        {
            // Arrange
            var viewMappings = ProvideMappingsAsViewType();
            mockImportView.SetupGet(x => x.Mappings).Returns(viewMappings);
            var thrownException = new Exception("Test exception");
            mockDataMigrationService
                .Setup(x => x.ImportData(It.IsAny<IOrganizationService>(), It.IsAny<DataFormat>(), It.IsAny< CrmSchemaConfiguration>(), It.IsAny<CrmImportConfig>()))
                .Throws(thrownException);

            // Act
            mockImportView.Raise(x => x.RunConfigClicked += null, EventArgs.Empty);
            mockWorkerHost.ExecuteWork(0);

            // Assert
            mockImportView.VerifyAll();
            mockNotifier.Verify(x => x.ShowError(thrownException));
        }

        [TestMethod]
        public void RunConfig_ShouldNotifySuccessWhenNotExceptionIsThrownInsideWorkerHost()
        {
            // Arrange
            var viewMappings = ProvideMappingsAsViewType();
            mockImportView.SetupGet(x => x.Mappings).Returns(viewMappings);

            // Act
            mockImportView.Raise(x => x.RunConfigClicked += null, EventArgs.Empty);
            mockWorkerHost.ExecuteWork(0);

            // Assert
            mockImportView.VerifyAll();
            mockNotifier.Verify(x => x.ShowSuccess("Data import is complete."));
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

        private static List<DataGridViewRow> ProvideMappingsAsViewType()
        {
            List<DataGridViewRow> mappings = new List<DataGridViewRow>();
            DataGridViewRow dataGridViewRow = new DataGridViewRow();
            dataGridViewRow.Cells.Add(new DataGridViewTextBoxCell { Value = "Account" });
            dataGridViewRow.Cells.Add(new DataGridViewTextBoxCell { Value = "00000000-0000-0000-0000-000000000001" });
            dataGridViewRow.Cells.Add(new DataGridViewTextBoxCell { Value = "00000000-0000-0000-0000-000000000002" });
            mappings.Add(dataGridViewRow);
            
            return mappings;
        }

        private static MappingConfiguration ProvideMappingsAsConfigType()
        {
            var importConfig = new CrmImportConfig();
            Dictionary<string, Dictionary<Guid, Guid>> mappings = new Dictionary<string, Dictionary<Guid, Guid>>();
            var guidsDictionary = new Dictionary<Guid, Guid>();
            var entity = "Account";
            var sourceId = Guid.Parse("00000000-0000-0000-0000-000000000001");
            var targetId = Guid.Parse("00000000-0000-0000-0000-000000000002");
            guidsDictionary.Add(sourceId, targetId);
            mappings.Add(entity, guidsDictionary);
            importConfig.MigrationConfig = new MappingConfiguration();
            importConfig.MigrationConfig.Mappings.AddRange(mappings);
            return importConfig.MigrationConfig;
        }

        private static MappingConfiguration ProvideTwoMappingsForSameEntityAsConfigType()
        {
            var importConfig = new CrmImportConfig();
            Dictionary<string, Dictionary<Guid, Guid>> mappings = new Dictionary<string, Dictionary<Guid, Guid>>();
            var guidsDictionary = new Dictionary<Guid, Guid>();
            var entity = "Account";
            var sourceId = Guid.Parse("00000000-0000-0000-0000-000000000001");
            var targetId = Guid.Parse("00000000-0000-0000-0000-000000000002");
            guidsDictionary.Add(sourceId, targetId);
            mappings.Add(entity, guidsDictionary);
            mappings[entity].Add(Guid.Parse("00000000-0000-0000-0000-000000000003"), Guid.Parse("00000000-0000-0000-0000-000000000004"));
            importConfig.MigrationConfig = new MappingConfiguration();
            importConfig.MigrationConfig.Mappings.AddRange(mappings);
            return importConfig.MigrationConfig;
        }

        private static DataGridViewRow GetRowWithBlankCell()
        {
            DataGridViewRow dataGridViewRow = new DataGridViewRow();
            dataGridViewRow.Cells.Add(new DataGridViewTextBoxCell { Value = "Account" });
            dataGridViewRow.Cells.Add(new DataGridViewTextBoxCell { Value = "" });
            dataGridViewRow.Cells.Add(new DataGridViewTextBoxCell { Value = "00000000-0000-0000-0000-000000000002" });
            return dataGridViewRow;
        }

        private static DataGridViewRow GetRowWithDefaultIds()
        {
            DataGridViewRow dataGridViewRow = new DataGridViewRow();
            dataGridViewRow.Cells.Add(new DataGridViewTextBoxCell { Value = "Account" });
            dataGridViewRow.Cells.Add(new DataGridViewTextBoxCell { Value = "00000000-0000-0000-0000-000000000000" });
            dataGridViewRow.Cells.Add(new DataGridViewTextBoxCell { Value = "00000000-0000-0000-0000-000000000000" });
            return dataGridViewRow;
        }

        private static DataGridViewRow GetRowWithAccountEntityAndValidGuids()
        {
            DataGridViewRow dataGridViewRow = new DataGridViewRow();
            dataGridViewRow.Cells.Add(new DataGridViewTextBoxCell { Value = "Account" });
            dataGridViewRow.Cells.Add(new DataGridViewTextBoxCell { Value = "00000000-0000-0000-0000-000000000003" });
            dataGridViewRow.Cells.Add(new DataGridViewTextBoxCell { Value = "00000000-0000-0000-0000-000000000004" });
            return dataGridViewRow;
        }
    }  
}