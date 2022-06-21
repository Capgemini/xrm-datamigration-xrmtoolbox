using Capgemini.Xrm.CdsDataMigratorLibrary.Enums;
using Capgemini.Xrm.CdsDataMigratorLibrary.Presenters;
using Capgemini.Xrm.CdsDataMigratorLibrary.UserControls;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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

        //[TestMethod]
        //public void OnlyActiveRecords_GetSet_True()
        //{
        //    // Arrange
        //    var value = true;
        //    using (var systemUnderTest = new ExportPage() { Parent = new PluginControlBase() })
        //    {
        //        // Act
        //        systemUnderTest.As<IExportPageView>().OnlyActiveRecords = value;

        //        // Assert
        //        systemUnderTest.As<IExportPageView>().OnlyActiveRecords.Should().Be(value);
        //    }
        //}

        //[TestMethod]
        //public void OnlyActiveRecords_GetSet_False()
        //{
        //    // Arrange
        //    var value = false;
        //    using (var systemUnderTest = new ExportPage() { Parent = new PluginControlBase() })
        //    {
        //        // Act
        //        systemUnderTest.As<IExportPageView>().OnlyActiveRecords = value;

        //        // Assert
        //        systemUnderTest.As<IExportPageView>().OnlyActiveRecords.Should().Be(value);
        //    }
        //}

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
                // Act
                systemUnderTest.As<IExportPageView>().CrmMigrationToolSchemaPath = value;

                // Assert
                systemUnderTest.As<IExportPageView>().CrmMigrationToolSchemaPath.Should().Be(value);
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

    }
}
