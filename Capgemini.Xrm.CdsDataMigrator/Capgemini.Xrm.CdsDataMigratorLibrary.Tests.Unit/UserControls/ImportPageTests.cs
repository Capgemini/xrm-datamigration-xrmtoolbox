﻿using Capgemini.Xrm.CdsDataMigratorLibrary.Enums;
using Capgemini.Xrm.CdsDataMigratorLibrary.Presenters;
using Capgemini.Xrm.CdsDataMigratorLibrary.UserControls;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XrmToolBox.Extensibility;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Tests.Unit.UserControls
{
    [TestClass]
    public class ImportPageTests
    {
        [TestMethod]
        public void SaveBatchSize_GetSet()
        {
            // Arrange
            var value = 1234;
            using (var systemUnderTest = new ImportPage() { Parent = new PluginControlBase() })
            {
                // Act
                systemUnderTest.As<IImportPageView>().SaveBatchSize = value;

                // Assert
                systemUnderTest.As<IImportPageView>().SaveBatchSize.Should().Be(value);
            }
        }

        [TestMethod]
        public void IgnoreStatuses_GetSet()
        {
            // Arrange
            var value = true;
            using (var systemUnderTest = new ImportPage() { Parent = new PluginControlBase() })
            {
                // Act
                systemUnderTest.As<IImportPageView>().IgnoreStatuses = value;

                // Assert
                systemUnderTest.As<IImportPageView>().IgnoreStatuses.Should().Be(value);
            }
        }

        [TestMethod]
        public void IgnoreSystemFields_GetSet()
        {
            // Arrange
            var value = true;
            using (var systemUnderTest = new ImportPage() { Parent = new PluginControlBase() })
            {
                // Act
                systemUnderTest.As<IImportPageView>().IgnoreSystemFields = value;

                // Assert
                systemUnderTest.As<IImportPageView>().IgnoreSystemFields.Should().Be(value);
            }
        }

        [TestMethod]
        public void JsonFolderPath_GetSet()
        {
            // Arrange
            var value = "Some string";
            using (var systemUnderTest = new ImportPage() { Parent = new PluginControlBase() })
            {
                // Act
                systemUnderTest.As<IImportPageView>().JsonFolderPath = value;

                // Assert
                systemUnderTest.As<IImportPageView>().JsonFolderPath.Should().Be(value);
            }
        }

        [TestMethod]
        public void CrmMigrationToolSchemaPath_GetSet()
        {
            // Arrange
            var value = "Some string";
            using (var systemUnderTest = new ImportPage() { Parent = new PluginControlBase() })
            {
                // Act
                systemUnderTest.As<IImportPageView>().CrmMigrationToolSchemaPath = value;

                // Assert
                systemUnderTest.As<IImportPageView>().CrmMigrationToolSchemaPath.Should().Be(value);
            }
        }

        [TestMethod]
        public void DataFormat_GetSet_Json()
        {
            // Arrange
            var value = DataFormat.Json;
            using (var systemUnderTest = new ImportPage() { Parent = new PluginControlBase() })
            {
                // Act
                systemUnderTest.As<IImportPageView>().DataFormat = value;

                // Assert
                systemUnderTest.As<IImportPageView>().DataFormat.Should().Be(value);
            }
        }

        [TestMethod]
        public void DataFormat_GetSet_Csv()
        {
            // Arrange
            var value = DataFormat.Csv;
            using (var systemUnderTest = new ImportPage() { Parent = new PluginControlBase() })
            {
                // Act
                systemUnderTest.As<IImportPageView>().DataFormat = value;

                // Assert
                systemUnderTest.As<IImportPageView>().DataFormat.Should().Be(value);
            }
        }
    }  
}