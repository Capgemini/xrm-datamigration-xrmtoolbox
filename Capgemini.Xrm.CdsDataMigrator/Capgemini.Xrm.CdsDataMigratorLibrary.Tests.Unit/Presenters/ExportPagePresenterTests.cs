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
using System.Linq;
using System.Windows.Forms;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

namespace Capgemini.Xrm.CdsDataMigrator.Tests.Unit.Presenters
{
    [TestClass]
    public class ExportPagePresenterTests : TestBase
    {
        private Mock<IExportPageView> mockExportView;
        private Mock<IWorkerHost> mockWorkerHost;
        private Mock<INotifier> mockNotifier;
        private ExportPagePresenter systemUnderTest;

        [TestInitialize]
        public void TestSetup()
        {
            SetupServiceMocks();
            mockExportView = new Mock<IExportPageView>();
            mockWorkerHost = new Mock<IWorkerHost>();
            mockNotifier = new Mock<INotifier>();

            systemUnderTest = new ExportPagePresenter(mockExportView.Object, mockWorkerHost.Object, DataMigrationServiceMock.Object, mockNotifier.Object, ServiceMock.Object, MetadataServiceMock.Object, ExceptionServicerMock.Object);
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
        public void LoadConfig_ShouldSetConfigPropertiesWhenValidFilePathSelected()
        {
            // Arrange
            var exportConfigFilePath = @"TestData\ExportConfig.json";
            var exportConfig = CrmExporterConfig.GetConfiguration(exportConfigFilePath);
            mockExportView
                .Setup(x => x.AskForFilePathToOpen())
                .Returns(exportConfigFilePath);

            // Act
            mockExportView.Raise(x => x.LoadConfigClicked += null, EventArgs.Empty);

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
                .Returns("$a-random-non-existent-file$");

            // Act
            mockExportView.Raise(x => x.LoadConfigClicked += null, EventArgs.Empty);

            // Assert
            mockExportView.VerifyAll();
            VerifyViewPropertiesNotSet();
        }

        [TestMethod]
        public void LoadConfig_ShouldDoNothingWhenEmptyFilePathSelected()
        {
            // Arrange
            mockExportView
                .Setup(x => x.AskForFilePathToOpen())
                .Returns("");

            // Act
            mockExportView.Raise(x => x.LoadConfigClicked += null, EventArgs.Empty);

            // Assert
            mockExportView.VerifyAll();
            VerifyViewPropertiesNotSet();
        }

        [TestMethod]
        public void LoadConfig_ShouldNotifyExceptionWhenAnExceptionIsThrown()
        {
            // Arrange
            var thrownException = new Exception("Test exception");
            mockExportView
                .Setup(x => x.AskForFilePathToOpen())
                .Throws(thrownException);

            // Act
            mockExportView.Raise(x => x.LoadConfigClicked += null, EventArgs.Empty);

            // Assert
            mockExportView.VerifyAll();
            mockNotifier.Verify(x => x.ShowError(thrownException));
        }

        [TestMethod]
        public void LoadConfig_ShouldSetLookupMappingsInViewCorrectly()
        {
            // Arrange
            var viewMappings = GetMappingsAsViewTypeToMatchConfigFile();
            var exportConfigFilePath = @"TestData\ExportConfig.json";
            mockExportView
                .Setup(x => x.AskForFilePathToOpen())
                .Returns(exportConfigFilePath);

            // Act
            mockExportView.Raise(x => x.LoadConfigClicked += null, EventArgs.Empty);

            // Assert
            mockExportView.VerifyAll();
            mockExportView.SetupSet(m => m.LookupMappings = viewMappings).Verifiable();
        }

        [TestMethod]
        public void SaveConfig_ShouldUpdateOrCreateConfigFileWhenValidFilePathSelected()
        {
            // Arrange
            var exportConfigFilePath = @"TestData\NewExportConfig.json";
            var viewMappings = ProvideMappingsAsViewType();
            var configMappings = ProvideMappingsAsConfigType();
            mockExportView.SetupGet(x => x.PageSize).Returns(1000);
            mockExportView.SetupGet(x => x.BatchSize).Returns(2000);
            mockExportView.SetupGet(x => x.TopCount).Returns(3000);
            mockExportView.SetupGet(x => x.OnlyActiveRecords).Returns(true);
            mockExportView.SetupGet(x => x.OneEntityPerBatch).Returns(false);
            mockExportView.SetupGet(x => x.CrmMigrationToolSchemaPath).Returns(@"C:\\Some\Path\To\A\Schema.xml");
            mockExportView.SetupGet(x => x.JsonFolderPath).Returns(@"C:\\Some\Path\To\A\Folder");
            mockExportView.SetupGet(x => x.FilePrefix).Returns("Release_X_");
            mockExportView.SetupGet(x => x.CrmMigrationToolSchemaFilters).Returns(new Dictionary<string, string> { { "entity", "filters" } });
            mockExportView.SetupGet(x => x.LookupMappings).Returns(viewMappings);
            mockExportView
                .Setup(x => x.AskForFilePathToSave(null))
                .Returns(exportConfigFilePath);

            // Act
            mockExportView.Raise(x => x.SaveConfigClicked += null, EventArgs.Empty);

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
            exportConfig.JsonFolderPath.Should().Be(@"C:\\Some\Path\To\A\Folder");
            exportConfig.FilePrefix.Should().Be("Release_X_");
            exportConfig.CrmMigrationToolSchemaFilters.Should().BeEquivalentTo(new Dictionary<string, string> { { "entity", "filters" } });
            exportConfig.LookupMapping.Should().BeEquivalentTo(configMappings);
        }

        [TestMethod]
        public void SaveConfig_ShouldNotIncludeRowWithEmptyCellIntheMappings()
        {
            // Arrange
            var exportConfigFilePath = @"TestData\NewExportConfig.json";
            var viewMappings = ProvideMappingsAsViewType();
            var newRow = GetRowWithBlankCell();
            viewMappings.Add(newRow);
            var configMappings = ProvideMappingsAsConfigType();
            mockExportView.SetupGet(x => x.LookupMappings).Returns(viewMappings);
            mockExportView
                .Setup(x => x.AskForFilePathToSave(null))
                .Returns(exportConfigFilePath);

            // Act
            mockExportView.Raise(x => x.SaveConfigClicked += null, EventArgs.Empty);

            // Assert
            mockExportView.VerifyAll();
            var exportConfig = CrmExporterConfig.GetConfiguration(exportConfigFilePath);
            exportConfig.LookupMapping.Should().BeEquivalentTo(configMappings);
        }

        [TestMethod]
        public void SaveConfig_ShouldNotIncludeDuplicateRowInTheMappings()
        {
            // Arrange
            var exportConfigFilePath = @"TestData\NewExportConfig.json";
            var viewMappings = ProvideMappingsAsViewType();
            var newRow = GetDuplicateRow();
            viewMappings.Add(newRow);
            var configMappings = ProvideMappingsAsConfigType();
            mockExportView.SetupGet(x => x.LookupMappings).Returns(viewMappings);
            mockExportView
                .Setup(x => x.AskForFilePathToSave(null))
                .Returns(exportConfigFilePath);

            // Act
            mockExportView.Raise(x => x.SaveConfigClicked += null, EventArgs.Empty);

            // Assert
            mockExportView.VerifyAll();
            var exportConfig = CrmExporterConfig.GetConfiguration(exportConfigFilePath);
            exportConfig.LookupMapping.Should().BeEquivalentTo(configMappings);
        }

        [TestMethod]
        public void SaveConfig_ShouldCorrectlyAddMappingWhereEntityAlreadyExistsAndMapFieldIsDifferent()
        {
            // Arrange
            var exportConfigFilePath = @"TestData\NewExportConfig.json";
            var viewMappings = ProvideTwoMappingsForSameEntityAndDifferentRefFieldAsViewType();
            var configMappings = ProvideTwoMappingsForSameEntityAndDifferentRefFieldAsConfigType();
            mockExportView.SetupGet(x => x.LookupMappings).Returns(viewMappings);
            mockExportView
                .Setup(x => x.AskForFilePathToSave(null))
                .Returns(exportConfigFilePath);

            // Act
            mockExportView.Raise(x => x.SaveConfigClicked += null, EventArgs.Empty);

            // Assert
            mockExportView.VerifyAll();
            var exportConfig = CrmExporterConfig.GetConfiguration(exportConfigFilePath);
            exportConfig.LookupMapping.Should().BeEquivalentTo(configMappings);
        }
        
        [TestMethod]
        public void SaveConfig_ShouldCorrectlyAddMappingWhereEntityAlreadyExistsAndMapFieldAlreadyExistsAndRefFieldIsDifferent()
        {
            // Arrange
            var exportConfigFilePath = @"TestData\NewExportConfig.json";
            var viewMappings = ProvideTwoMappingsForSameEntityAndSameRefFieldAndDifferentMapFieldAsViewType();
            var configMappings = ProvideTwoMappingsForSameEntityAndSameRefFieldAndDifferentMapFieldAsConfigType();
            mockExportView.SetupGet(x => x.LookupMappings).Returns(viewMappings);
            mockExportView
                .Setup(x => x.AskForFilePathToSave(null))
                .Returns(exportConfigFilePath);

            // Act
            mockExportView.Raise(x => x.SaveConfigClicked += null, EventArgs.Empty);

            // Assert
            mockExportView.VerifyAll();
            var exportConfig = CrmExporterConfig.GetConfiguration(exportConfigFilePath);
            exportConfig.LookupMapping.Should().BeEquivalentTo(configMappings);
        }

        [TestMethod]
        [Ignore("What is an invalid file?")]
        public void SaveConfig_ShouldDoNothingWhenInvalidFilePathSelected()
        {
            // Arrange
            mockExportView
                .Setup(x => x.AskForFilePathToSave(null))
                .Returns("a-random-non-existent-file");

            // Act
            mockExportView.Raise(x => x.SaveConfigClicked += null, EventArgs.Empty);

            // Assert
            mockExportView.VerifyAll();
        }

        [TestMethod]
        public void SaveConfig_ShouldDoNothingWhenEmptyFilePathSelected()
        {
            // Arrange
            var viewMappings = ProvideMappingsAsViewType();
            mockExportView.SetupGet(x => x.LookupMappings).Returns(viewMappings);
            mockExportView
                .Setup(x => x.AskForFilePathToSave(null))
                .Returns("");

            // Act
            mockExportView.Raise(x => x.SaveConfigClicked += null, EventArgs.Empty);

            // Assert
            mockExportView.VerifyAll();
        }

        [TestMethod]
        public void SaveConfig_ShouldReuseLoadedFilePath()
        {
            // Arrange
            var viewMappings = ProvideMappingsAsViewType();
            mockExportView.SetupGet(x => x.LookupMappings).Returns(viewMappings);
            var exportConfigFilePath = @"TestData\NewExportConfig.json";
            var exportConfig = CrmExporterConfig.GetConfiguration(exportConfigFilePath);
            mockExportView
                .Setup(x => x.AskForFilePathToOpen())
                .Returns(exportConfigFilePath);
            mockExportView
                .Setup(x => x.AskForFilePathToSave(exportConfigFilePath))
                .Returns(exportConfigFilePath);

            // Act
            mockExportView.Raise(x => x.LoadConfigClicked += null, EventArgs.Empty);
            mockExportView.Raise(x => x.SaveConfigClicked += null, EventArgs.Empty);

            // Assert
            mockExportView.VerifyAll();
        }

        [TestMethod]
        public void SaveConfig_ShouldNotifyExceptionWhenAnExceptionIsThrown()
        {
            // Arrange
            var viewMappings = ProvideMappingsAsViewType();
            mockExportView.SetupGet(x => x.LookupMappings).Returns(viewMappings);
            var thrownException = new Exception("Test exception");
            mockExportView
                .Setup(x => x.AskForFilePathToSave(null))
                .Throws(thrownException);

            // Act
            mockExportView.Raise(x => x.SaveConfigClicked += null, EventArgs.Empty);

            // Assert
            mockExportView.VerifyAll();
            mockNotifier.Verify(x => x.ShowError(thrownException));
        }

        [TestMethod]
        public void RunConfig_ShouldReadValuesFromView()
        {
            // Arrange
            var mockIOrganisationService = new Mock<IOrganizationService>();
            var viewMappings = ProvideMappingsAsViewType();
            mockExportView.SetupGet(x => x.LookupMappings).Returns(viewMappings);
            mockExportView.SetupGet(x => x.PageSize).Returns(1000);
            mockExportView.SetupGet(x => x.BatchSize).Returns(2000);
            mockExportView.SetupGet(x => x.TopCount).Returns(3000);
            mockExportView.SetupGet(x => x.OnlyActiveRecords).Returns(true);
            mockExportView.SetupGet(x => x.OneEntityPerBatch).Returns(false);
            mockExportView.SetupGet(x => x.SeperateFilesPerEntity).Returns(false);
            mockExportView.SetupGet(x => x.DataFormat).Returns(CdsDataMigratorLibrary.Enums.DataFormat.Json);
            mockExportView.SetupGet(x => x.CrmMigrationToolSchemaPath).Returns(@"C:\\Some\Path\To\A\Schema.xml");
            mockExportView.SetupGet(x => x.JsonFolderPath).Returns(@"C:\\Some\Path\To\A\Folder");
            mockExportView.SetupGet(x => x.FilePrefix).Returns("Release_X_");
            mockExportView.SetupGet(x => x.CrmMigrationToolSchemaFilters).Returns(new Dictionary<string, string> { { "entity", "filters" } });
            mockExportView.SetupGet(x => x.Service).Returns(mockIOrganisationService.Object);

            // Act
            mockExportView.Raise(x => x.RunConfigClicked += null, EventArgs.Empty);
            var workInfo = mockWorkerHost.Invocations[0].Arguments[0].As<WorkAsyncInfo>();
            workInfo.Work(null, null);

            // Assert
            mockExportView.VerifyAll();
            workInfo.Message.Should().Be("Exporting data...");
            DataMigrationServiceMock.Verify(x => x.ExportData(mockIOrganisationService.Object, CdsDataMigratorLibrary.Enums.DataFormat.Json, It.IsAny<CrmExporterConfig>()));
            var exportConfig = DataMigrationServiceMock.Invocations[0].Arguments[2].As<CrmExporterConfig>();
            exportConfig.PageSize.Should().Be(1000);
            exportConfig.BatchSize.Should().Be(2000);
            exportConfig.TopCount.Should().Be(3000);
            exportConfig.OnlyActiveRecords.Should().Be(true);
            exportConfig.OneEntityPerBatch.Should().Be(false);
            exportConfig.SeperateFilesPerEntity.Should().Be(false);
            exportConfig.CrmMigrationToolSchemaPaths.Count.Should().Be(1);
            exportConfig.CrmMigrationToolSchemaPaths.FirstOrDefault().Should().Be(@"C:\\Some\Path\To\A\Schema.xml");
            exportConfig.JsonFolderPath.Should().Be(@"C:\\Some\Path\To\A\Folder");
            exportConfig.FilePrefix.Should().Be("Release_X_");
            exportConfig.CrmMigrationToolSchemaFilters.Should().BeEquivalentTo(new Dictionary<string, string> { { "entity", "filters" } });
        }

        [TestMethod]
        public void RunConfig_ShouldNotifyExceptionWhenAnExceptionIsThrownOutsideWorkerHost()
        {
            // Arrange
            var viewMappings = ProvideMappingsAsViewType();
            mockExportView.SetupGet(x => x.LookupMappings).Returns(viewMappings);
            var thrownException = new Exception("Test exception");
            mockWorkerHost
                .Setup(x => x.WorkAsync(It.IsAny<WorkAsyncInfo>()))
                .Throws(thrownException);

            // Act
            mockExportView.Raise(x => x.RunConfigClicked += null, EventArgs.Empty);

            // Assert
            mockExportView.VerifyAll();
            mockNotifier.Verify(x => x.ShowError(thrownException));
        }

        [TestMethod]
        public void RunConfig_ShouldNotifyExceptionWhenAnExceptionIsThrownInsideWorkerHost()
        {
            // Arrange
            var viewMappings = ProvideMappingsAsViewType();
            mockExportView.SetupGet(x => x.LookupMappings).Returns(viewMappings);
            var thrownException = new Exception("Test exception");
            DataMigrationServiceMock
                .Setup(x => x.ExportData(It.IsAny<IOrganizationService>(), It.IsAny<DataFormat>(), It.IsAny<CrmExporterConfig>()))
                .Throws(thrownException);

            // Act
            mockExportView.Raise(x => x.RunConfigClicked += null, EventArgs.Empty);
            mockWorkerHost.ExecuteWork(0);

            // Assert
            mockExportView.VerifyAll();
            mockNotifier.Verify(x => x.ShowError(thrownException));
        }

        [TestMethod]
        public void RunConfig_ShouldNotifySuccessWhenNotExceptionIsThrownInsideWorkerHost()
        {
            // Act
            var viewMappings = ProvideMappingsAsViewType();
            mockExportView.SetupGet(x => x.LookupMappings).Returns(viewMappings);
            mockExportView.Raise(x => x.RunConfigClicked += null, EventArgs.Empty);
            mockWorkerHost.ExecuteWork(0);

            // Assert
            mockExportView.VerifyAll();
            mockNotifier.Verify(x => x.ShowSuccess("Data export is complete."));
        }

        [TestMethod]
        public void GetSchemaConfiguration_ShouldReturnNullWhenCrmMigrationToolSchemaPathIsNull()
        {
            // Arrange
            CrmSchemaConfiguration result = null;
            mockExportView
                            .SetupGet(x => x.CrmMigrationToolSchemaPath)
                .Returns(() => null);
            mockExportView
                .SetupSet(x => x.SchemaConfiguration = It.IsAny<CrmSchemaConfiguration>())
                .Callback<CrmSchemaConfiguration>(x => result = x);

            // Act
            mockExportView.Raise(x => x.SchemaConfigPathChanged += null, EventArgs.Empty);

            // Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public void GetSchemaConfiguration_ShouldReturnNullWhenCrmMigrationToolSchemaPathIsEmpty()
        {
            // Arrange
            CrmSchemaConfiguration result = null;
            mockExportView
                .SetupGet(x => x.CrmMigrationToolSchemaPath)
                .Returns(" ");
            mockExportView
                .SetupSet(x => x.SchemaConfiguration = It.IsAny<CrmSchemaConfiguration>())
                .Callback<CrmSchemaConfiguration>(x => result = x);

            // Act
            mockExportView.Raise(x => x.SchemaConfigPathChanged += null, EventArgs.Empty);

            // Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public void GetSchemaConfiguration_ShouldReturnNullWhenCrmMigrationToolSchemaPathIsInvalid()
        {
            // Arrange
            CrmSchemaConfiguration result = null;
            mockExportView
                .SetupGet(x => x.CrmMigrationToolSchemaPath)
                .Returns("a-random-non-existent-file");
            mockExportView
                .SetupSet(x => x.SchemaConfiguration = It.IsAny<CrmSchemaConfiguration>())
                .Callback<CrmSchemaConfiguration>(x => result = x);

            // Act
            mockExportView.Raise(x => x.SchemaConfigPathChanged += null, EventArgs.Empty);

            // Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public void GetSchemaConfiguration_ShouldReturnSchemaWhenCrmMigrationToolSchemaPathIsValid()
        {
            // Arrange
            var filePath = @"TestData\BusinessUnitSchema.xml";
            CrmSchemaConfiguration result = null;
            mockExportView
                .SetupGet(x => x.CrmMigrationToolSchemaPath)
                .Returns(filePath);
            mockExportView
                .SetupSet(x => x.SchemaConfiguration = It.IsAny<CrmSchemaConfiguration>())
                .Callback<CrmSchemaConfiguration>(x => result = x);

            // Act
            mockExportView.Raise(x => x.SchemaConfigPathChanged += null, EventArgs.Empty);

            // Assert
            result.Should().BeEquivalentTo(CrmSchemaConfiguration.ReadFromFile(filePath));
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
            mockExportView.VerifySet(x => x.FilePrefix = exportConfig.FilePrefix, "File prefix does not match config");
            mockExportView.VerifySet(x => x.CrmMigrationToolSchemaFilters = exportConfig.CrmMigrationToolSchemaFilters, "CrmMigrationToolSchemaFilters does not match config");

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
            mockExportView.VerifySet(x => x.FilePrefix = It.IsAny<string>(), Times.Once, "File prefix was set unexpectedly");
            mockExportView.VerifySet(x => x.CrmMigrationToolSchemaFilters = It.IsAny<Dictionary<string, string>>(), "CrmMigrationToolSchemaFilters was set unexpectedly");
        }

        private static List<DataGridViewRow> ProvideMappingsAsViewType()
        {
            List<DataGridViewRow> mappings = new List<DataGridViewRow>();
            DataGridViewRow dataGridViewRow = new DataGridViewRow();
            dataGridViewRow.Cells.Add(new DataGridViewTextBoxCell { Value = "Account" });
            dataGridViewRow.Cells.Add(new DataGridViewTextBoxCell { Value = "accountid" });
            dataGridViewRow.Cells.Add(new DataGridViewTextBoxCell { Value = "accountnumber" });
            mappings.Add(dataGridViewRow);

            return mappings;
        }

        private static Dictionary<string, Dictionary<string, List<string>>> ProvideMappingsAsConfigType()
        {
            var exportConfig = new CrmExporterConfig();
            Dictionary<string, Dictionary<string, List<string>>> lookupMappings = new Dictionary<string, Dictionary<string, List<string>>>();
            var lookupsDictionary = new Dictionary<string, List<string>>();
            var entity = "Account";
            var refField = "accountid";
            var mapField = "accountnumber";
            lookupsDictionary.Add(refField, new List<string> { mapField });
            lookupMappings.Add(entity, lookupsDictionary);
            exportConfig.LookupMapping.AddRange(lookupMappings);
            return exportConfig.LookupMapping;
        }

        private static List<DataGridViewRow> ProvideTwoMappingsForSameEntityAndDifferentRefFieldAsViewType()
        {
            List<DataGridViewRow> mappings = new List<DataGridViewRow>();
            DataGridViewRow dataGridViewRow = new DataGridViewRow();
            DataGridViewRow dataGridViewRow2 = new DataGridViewRow();
            dataGridViewRow.Cells.Add(new DataGridViewTextBoxCell { Value = "Account" });
            dataGridViewRow.Cells.Add(new DataGridViewTextBoxCell { Value = "accountid" });
            dataGridViewRow.Cells.Add(new DataGridViewTextBoxCell { Value = "accountnumber" });
            dataGridViewRow2.Cells.Add(new DataGridViewTextBoxCell { Value = "Account" });
            dataGridViewRow2.Cells.Add(new DataGridViewTextBoxCell { Value = "ownerid" });
            dataGridViewRow2.Cells.Add(new DataGridViewTextBoxCell { Value = "accountcategorycode" });
            mappings.Add(dataGridViewRow);
            mappings.Add(dataGridViewRow2);
            return mappings;
        }

        private static Dictionary<string, Dictionary<string, List<string>>> ProvideTwoMappingsForSameEntityAndDifferentRefFieldAsConfigType()
        {
            var exportConfig = new CrmExporterConfig();
            Dictionary<string, Dictionary<string, List<string>>> lookupMappings = new Dictionary<string, Dictionary<string, List<string>>>();
            var lookupsDictionary = new Dictionary<string, List<string>>();
            var lookupsDictionary2 = new Dictionary<string, List<string>>();
            var entity = "Account";
            var refField = "accountid";
            var refField2 = "ownerid";
            var mapField = "accountnumber";
            var mapField2 = "accountcategorycode";
            lookupsDictionary.Add(refField, new List<string> { mapField });
            lookupsDictionary2.Add(refField2, new List<string> { mapField2 });
            lookupMappings.Add(entity, lookupsDictionary);
            lookupMappings[entity].AddRange(lookupsDictionary2);
            exportConfig.LookupMapping.AddRange(lookupMappings);
            return exportConfig.LookupMapping;
        }

        private static List<DataGridViewRow> ProvideTwoMappingsForSameEntityAndSameRefFieldAndDifferentMapFieldAsViewType()
        {
            List<DataGridViewRow> mappings = new List<DataGridViewRow>();
            DataGridViewRow dataGridViewRow = new DataGridViewRow();
            DataGridViewRow dataGridViewRow2 = new DataGridViewRow();
            dataGridViewRow.Cells.Add(new DataGridViewTextBoxCell { Value = "Account" });
            dataGridViewRow.Cells.Add(new DataGridViewTextBoxCell { Value = "accountid" });
            dataGridViewRow.Cells.Add(new DataGridViewTextBoxCell { Value = "accountnumber" });
            dataGridViewRow2.Cells.Add(new DataGridViewTextBoxCell { Value = "Account" });
            dataGridViewRow2.Cells.Add(new DataGridViewTextBoxCell { Value = "accountid" });
            dataGridViewRow2.Cells.Add(new DataGridViewTextBoxCell { Value = "accountcategorycode" });
            mappings.Add(dataGridViewRow);
            mappings.Add(dataGridViewRow2);
            return mappings;
        }

        private static Dictionary<string, Dictionary<string, List<string>>> ProvideTwoMappingsForSameEntityAndSameRefFieldAndDifferentMapFieldAsConfigType()
        {
            var exportConfig = new CrmExporterConfig();
            Dictionary<string, Dictionary<string, List<string>>> lookupMappings = new Dictionary<string, Dictionary<string, List<string>>>();
            var lookupsDictionary = new Dictionary<string, List<string>>();
            var entity = "Account";
            var refField = "accountid";
            var mapField = "accountnumber";
            var mapField2 = "accountcategorycode";
            lookupsDictionary.Add(refField, new List<string> { mapField });
            lookupMappings.Add(entity, lookupsDictionary);
            lookupMappings[entity][refField].Add(mapField2);
            exportConfig.LookupMapping.AddRange(lookupMappings);
            return exportConfig.LookupMapping;
        }

        private static DataGridViewRow GetRowWithBlankCell()
        {
            DataGridViewRow dataGridViewRow = new DataGridViewRow();
            dataGridViewRow.Cells.Add(new DataGridViewTextBoxCell { Value = "Account" });
            dataGridViewRow.Cells.Add(new DataGridViewTextBoxCell { Value = "" });
            dataGridViewRow.Cells.Add(new DataGridViewTextBoxCell { Value = "accountid" });
            return dataGridViewRow;
        }

        private static DataGridViewRow GetDuplicateRow()
        {
            DataGridViewRow dataGridViewRow = new DataGridViewRow();
            dataGridViewRow.Cells.Add(new DataGridViewTextBoxCell { Value = "Account" });
            dataGridViewRow.Cells.Add(new DataGridViewTextBoxCell { Value = "accountid" });
            dataGridViewRow.Cells.Add(new DataGridViewTextBoxCell { Value = "accountnumber" });
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

        private static List<DataGridViewRow> GetMappingsAsViewTypeToMatchConfigFile()
        {
            List<DataGridViewRow> mappings = new List<DataGridViewRow>();
            DataGridViewRow dataGridViewRow = new DataGridViewRow();
            dataGridViewRow.Cells.Add(new DataGridViewTextBoxCell { Value = "systemuser" });
            dataGridViewRow.Cells.Add(new DataGridViewTextBoxCell { Value = "businessunitid" });
            dataGridViewRow.Cells.Add(new DataGridViewTextBoxCell { Value = "name" });
            mappings.Add(dataGridViewRow);

            return mappings;
        }
    }
}