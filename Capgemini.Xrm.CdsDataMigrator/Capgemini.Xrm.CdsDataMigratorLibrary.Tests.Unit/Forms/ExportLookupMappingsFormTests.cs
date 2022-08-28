using Capgemini.Xrm.CdsDataMigratorLibrary.Forms;
using Capgemini.Xrm.CdsDataMigratorLibrary.Presenters;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
                systemUnderTest.As<IExportLookupMappingsView>().EntityList = value;

                // Assert
                systemUnderTest.As<IExportLookupMappingsView>().EntityList.Should().BeEquivalentTo(value);

            }
        }

        [TestMethod]
        public void Mappings_GetSet()
        {
            // Arrange
            var value = new DataGridView();
            using (var systemUnderTest = new ExportLookupMappings())
            {
                // Act
                systemUnderTest.As<IExportLookupMappingsView>().LookupMappings = value;

                // Assert
                systemUnderTest.As<IExportLookupMappingsView>().LookupMappings.Should().BeEquivalentTo(value);

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
