using Capgemini.Xrm.CdsDataMigratorLibrary.Forms;
using Capgemini.Xrm.CdsDataMigratorLibrary.Presenters;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Tests.Unit.Forms
{
    [TestClass]
    public class ExportLookupMappingsFormTest
    {
        [TestMethod]
        public void EntityList_GetSet()
        {
            // Arrange
            var value = new List<string>();
            using (var systemUnderTest = new ExportLookupMappings())
            {
                // Act
                systemUnderTest.As<IExportLookupMappingsView>().EntityListDataSource = value;

                // Assert
                systemUnderTest.As<IExportLookupMappingsView>().EntityListDataSource.Should().BeEquivalentTo(value);

            }
        }

        [TestMethod]
        public void FirstCellInRow_GetSet()
        {
            // Arrange
            var value = new List<string>();
            using (var systemUnderTest = new ExportLookupMappings())
            {
                // Act
                systemUnderTest.As<IExportLookupMappingsView>().MappingCells = value;

                // Assert
                systemUnderTest.As<IExportLookupMappingsView>().MappingCells.Should().BeEquivalentTo(value);

            }
        }

        [TestMethod]
        public void Mappings2_GetSet()
        {
            // Arrange
            var value = new List<DataGridViewRow>();
            using (var systemUnderTest = new ExportLookupMappings())
            {
                // Act
                systemUnderTest.Mappings = value;

                // Assert
                systemUnderTest.Mappings.Count.Should().Be(1);

            }
        }

        [TestMethod]
        public void OnVisibleChanged_ShouldNotifyPresenterWhenTrue()
        {
            // Arrange
            using (var systemUnderTest = new ExportLookupMappings())
            {
                var isCalled = false;
                systemUnderTest.OnVisible += (object sender, EventArgs e) => isCalled = true;

                // Act
                systemUnderTest.Visible = true;

                // Assert
                isCalled.Should().BeTrue();
            }
        }

        [TestMethod]
        public void OnVisibleChanged_ShouldNotifyPresenterWhenFalse()
        {
            // Arrange
            using (var systemUnderTest = new ExportLookupMappings())
            {
                var isCalled = false;
                systemUnderTest.OnVisible += (object sender, EventArgs e) => isCalled = true;

                // Act
                systemUnderTest.Visible = false;

                // Assert
                isCalled.Should().BeFalse();
            }
        }
    }
}
