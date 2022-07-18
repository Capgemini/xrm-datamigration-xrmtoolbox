using Capgemini.Xrm.CdsDataMigratorLibrary.Enums;
using Capgemini.Xrm.CdsDataMigratorLibrary.Presenters;
using Capgemini.Xrm.CdsDataMigratorLibrary.UserControls;
using Capgemini.Xrm.DataMigration.Config;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XrmToolBox.Extensibility;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Tests.Unit.UserControls
{
    [TestClass]
    public class ExportPageTests
    {
        [TestMethod]
        public void PageSize_GetSet()
        {
            // Arrange
            var value = 1234;
            using (var systemUnderTest = new ExportPage() {  Parent = new PluginControlBase() })
            {
                // Act
                systemUnderTest.As<IExportPageView>().PageSize = value;

                // Assert
                systemUnderTest.As<IExportPageView>().PageSize.Should().Be(value);
            }
        }

        [TestMethod]
        public void BatchSize_GetSet()
        {
            // Arrange
            var value = 123;
            using (var systemUnderTest = new ExportPage() { Parent = new PluginControlBase() })
            {
                // Act
                systemUnderTest.As<IExportPageView>().BatchSize = value;

                // Assert
                systemUnderTest.As<IExportPageView>().BatchSize.Should().Be(value);
            }
        }

        [TestMethod]
        public void TopCount_GetSet()
        {
            // Arrange
            var value = 1234;
            using (var systemUnderTest = new ExportPage() { Parent = new PluginControlBase() })
            {
                // Act
                systemUnderTest.As<IExportPageView>().TopCount = value;

                // Assert
                systemUnderTest.As<IExportPageView>().TopCount.Should().Be(value);
            }
        }

        [TestMethod]
        public void OnlyActiveRecords_GetSet()
        {
            // Arrange
            var value = true;
            using (var systemUnderTest = new ExportPage() { Parent = new PluginControlBase() })
            {
                // Act
                systemUnderTest.As<IExportPageView>().OnlyActiveRecords = value;

                // Assert
                systemUnderTest.As<IExportPageView>().OnlyActiveRecords.Should().Be(value);
            }
        }

        [TestMethod]
        public void JsonFolderPath_GetSet()
        {
            // Arrange
            var value = "Some string";
            using (var systemUnderTest = new ExportPage() { Parent = new PluginControlBase() })
            {
                // Act
                systemUnderTest.As<IExportPageView>().JsonFolderPath = value;

                // Assert
                systemUnderTest.As<IExportPageView>().JsonFolderPath.Should().Be(value);
            }
        }

        [TestMethod]
        public void CrmMigrationToolSchemaPath_GetSet()
        {
            // Arrange
            var value = "Some string";
            using (var systemUnderTest = new ExportPage() { Parent = new PluginControlBase() })
            {
                bool isCalled = false;
                systemUnderTest.SchemaConfigPathChanged += (sender, e) => isCalled = true;
                // Act
                systemUnderTest.As<IExportPageView>().CrmMigrationToolSchemaPath = value;

                // Assert
                systemUnderTest.As<IExportPageView>().CrmMigrationToolSchemaPath.Should().Be(value);
                isCalled.Should().BeTrue();
            }
        }

        [TestMethod]
        public void DataFormat_GetSet_Json()
        {
            // Arrange
            var value = DataFormat.Json;
            using (var systemUnderTest = new ExportPage() { Parent = new PluginControlBase() })
            {
                // Act
                systemUnderTest.As<IExportPageView>().DataFormat = value;

                // Assert
                systemUnderTest.As<IExportPageView>().DataFormat.Should().Be(value);
            }
        }

        [TestMethod]
        public void DataFormat_GetSet_Csv()
        {
            // Arrange
            var value = DataFormat.Csv;
            using (var systemUnderTest = new ExportPage() { Parent = new PluginControlBase() })
            {
                // Act
                systemUnderTest.As<IExportPageView>().DataFormat = value;

                // Assert
                systemUnderTest.As<IExportPageView>().DataFormat.Should().Be(value);
            }
        }

        [TestMethod]
        public void OneEntityPerBatch_GetSet()
        {
            // Arrange
            var value = false;
            using (var systemUnderTest = new ExportPage() { Parent = new PluginControlBase() })
            {
                // Act
                systemUnderTest.As<IExportPageView>().OneEntityPerBatch = value;

                // Assert
                systemUnderTest.As<IExportPageView>().OneEntityPerBatch.Should().Be(value);
            }
        }

        [TestMethod]
        public void SeperateFilesPerEntity_GetSet()
        {
            // Arrange
            var value = false;
            using (var systemUnderTest = new ExportPage() { Parent = new PluginControlBase() })
            {
                // Act
                systemUnderTest.As<IExportPageView>().SeperateFilesPerEntity= value;

                // Assert
                systemUnderTest.As<IExportPageView>().SeperateFilesPerEntity.Should().Be(value);
            }
        }

        [TestMethod]
        public void FilePrefix_GetSet()
        {
            // Arrange
            var value = "Some string";
            using (var systemUnderTest = new ExportPage() { Parent = new PluginControlBase() })
            {
                // Act
                systemUnderTest.As<IExportPageView>().FilePrefix = value;

                // Assert
                systemUnderTest.As<IExportPageView>().FilePrefix.Should().Be(value);
            }
        }

        [TestMethod]
        public void CrmMigrationToolSchemaFilters_GetSet()
        {
            // Arrange
            var value = new Dictionary<string, string> { { "entity", "<filter></filter>"} };
            using (var systemUnderTest = new ExportPage() { Parent = new PluginControlBase() })
            {
                // Act
                systemUnderTest.As<IExportPageView>().CrmMigrationToolSchemaFilters = value;

                // Assert
                systemUnderTest.As<IExportPageView>().CrmMigrationToolSchemaFilters.Should().BeEquivalentTo(value);
            }
        }

        [TestMethod]
        public void SchemaConfiguration_GetSet()
        {
            // Arrange
            var value = new CrmSchemaConfiguration();
            using (var systemUnderTest = new ExportPage() { Parent = new PluginControlBase() })
            {
                // Act
                systemUnderTest.As<IExportPageView>().SchemaConfiguration = value;

                // Assert
                systemUnderTest.As<IExportPageView>().SchemaConfiguration.Should().BeEquivalentTo(value);
            }
        }

    }
}
