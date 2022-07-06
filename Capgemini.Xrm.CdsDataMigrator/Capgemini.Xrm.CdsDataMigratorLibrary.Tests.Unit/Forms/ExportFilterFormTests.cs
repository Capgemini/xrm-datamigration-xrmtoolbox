using Capgemini.Xrm.CdsDataMigratorLibrary.Forms;
using Capgemini.Xrm.CdsDataMigratorLibrary.Models;
using Capgemini.Xrm.CdsDataMigratorLibrary.Presenters;
using Capgemini.Xrm.DataMigration.Config;
using Capgemini.Xrm.DataMigration.Model;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Tests.Unit.Forms
{
    [TestClass]
    public class ExportFilterFormTests
    {
        [TestMethod]
        public void EntityList_GetSet()
        {
            // Arrange
            var value = new List<ListBoxItem<CrmEntity>>();
            using (var systemUnderTest = new ExportFilterForm())
            {
                // Act
                systemUnderTest.As<IExportFilterFormView>().EntityList = value;

                // Assert
                systemUnderTest.As<IExportFilterFormView>().EntityList.Should().BeEquivalentTo(value);
            }
        }

        [TestMethod]
        public void SelectedEntity_GetSet()
        {
            // Arrange
            var mockPresenter = new Mock<IExportFilterFormPresenter>();
            var value = new CrmEntity();
            using (var systemUnderTest = new ExportFilterForm(mockPresenter.Object))
            {
                systemUnderTest.As<IExportFilterFormView>().EntityList = new List<ListBoxItem<CrmEntity>>
                {
                    new ListBoxItem<CrmEntity> { DisplayName = "Entity", Item = value }
                };

                // Act
                systemUnderTest.As<IExportFilterFormView>().SelectedEntity = value;

                // Assert
                systemUnderTest.As<IExportFilterFormView>().SelectedEntity.Should().Be(value);
            }
        }

        [TestMethod]
        public void SelectedEntity_ShouldNotifyPresenterWhenUpdated()
        {
            // Arrange
            var mockPresenter = new Mock<IExportFilterFormPresenter>();
            var value = new CrmEntity();
            using (var systemUnderTest = new ExportFilterForm(mockPresenter.Object))
            {
                systemUnderTest.As<IExportFilterFormView>().EntityList = new List<ListBoxItem<CrmEntity>>
                {
                    new ListBoxItem<CrmEntity> { DisplayName = "Entity", Item = value } 
                };

                // Act
                systemUnderTest.As<IExportFilterFormView>().SelectedEntity = value;

                // Assert
                mockPresenter.Verify(x => x.OnEntitySelected());
            }
        }

        [TestMethod]
        public void EntityFilters_GetSet()
        {
            // Arrange
            var value = new Dictionary<string,string>();
            using (var systemUnderTest = new ExportFilterForm())
            {
                // Act
                systemUnderTest.EntityFilters = value;

                // Assert
                systemUnderTest.As<IExportFilterFormView>().EntityFilters.Should().BeEquivalentTo(value);
            }
        }

        [TestMethod]
        public void SchemaConfiguration_GetSet()
        {
            // Arrange
            var value = new CrmSchemaConfiguration();
            using (var systemUnderTest = new ExportFilterForm())
            {
                // Act
                systemUnderTest.SchemaConfiguration = value;

                // Assert
                systemUnderTest.As<IExportFilterFormView>().SchemaConfiguration.Should().Be(value);
            }
        }

        [TestMethod]
        public void FilterText_GetSet()
        {
            // Arrange
            var mockPresenter = new Mock<IExportFilterFormPresenter>();
            var value = "some text";
            using (var systemUnderTest = new ExportFilterForm(mockPresenter.Object))
            {
                // Act
                systemUnderTest.As<IExportFilterFormView>().FilterText = value;

                // Assert
                systemUnderTest.As<IExportFilterFormView>().FilterText.Should().Be(value);
            }
        }

        [TestMethod]
        public void FilterText_ShouldNotifyPresenterWhenSet()
        {
            // Arrange
            var mockPresenter = new Mock<IExportFilterFormPresenter>();
            var value = "some text";
            using (var systemUnderTest = new ExportFilterForm(mockPresenter.Object))
            {
                // Act
                systemUnderTest.As<IExportFilterFormView>().FilterText = value;

                // Assert
                mockPresenter.Verify(x => x.UpdateFilterForEntity());
            }
        }

        [TestMethod]
        public void OnVisibleChanged_ShouldNotifyPresenterWhenTrue()
        {
            // Arrange
            var mockPresenter = new Mock<IExportFilterFormPresenter>();
            using (var systemUnderTest = new ExportFilterForm(mockPresenter.Object))
            {
                // Act
                systemUnderTest.Visible = true;

                // Assert
                mockPresenter.Verify(x => x.OnVisible(), Times.Once);
            }
        }

        [TestMethod]
        public void OnVisibleChanged_ShouldNotNotifyPresenterWhenFalse()
        {
            // Arrange
            var mockPresenter = new Mock<IExportFilterFormPresenter>();
            using (var systemUnderTest = new ExportFilterForm(mockPresenter.Object))
            {
                // Act
                systemUnderTest.Visible = false;

                // Assert
                mockPresenter.Verify(x => x.OnVisible(), Times.Never);
            }
        }
    }
}
