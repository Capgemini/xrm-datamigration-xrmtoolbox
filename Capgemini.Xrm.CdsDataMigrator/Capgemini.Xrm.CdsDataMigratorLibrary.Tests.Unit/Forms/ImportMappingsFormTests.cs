using Capgemini.Xrm.CdsDataMigratorLibrary.Forms;
using Capgemini.Xrm.CdsDataMigratorLibrary.Presenters;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Tests.Unit.Forms
{
    [TestClass]
    public class ImportMappingsFormTest
    {
        [TestMethod]
        public void EntityList_GetSet()
        {
            // Arrange
            var value = new List<string>();
            using (var systemUnderTest = new ImportMappingsForm())
            {
                // Act
                systemUnderTest.As<IImportMappingsFormView>().EntityListDataSource = value;

                // Assert
                systemUnderTest.As<IImportMappingsFormView>().EntityListDataSource.Should().BeEquivalentTo(value);

            }
        }

        [TestMethod]
        public void OnVisibleChanged_ShouldNotifyPresenterWhenTrue()
        {
            // Arrange
            using (var systemUnderTest = new ImportMappingsForm())
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
            using (var systemUnderTest = new ImportMappingsForm())
            {
                var isCalled = false;
                systemUnderTest.OnVisible += (object sender,EventArgs e) => isCalled = true;

                // Act
                systemUnderTest.Visible = false;

                // Assert
                isCalled.Should().BeFalse();
            }
        }
    }
}
