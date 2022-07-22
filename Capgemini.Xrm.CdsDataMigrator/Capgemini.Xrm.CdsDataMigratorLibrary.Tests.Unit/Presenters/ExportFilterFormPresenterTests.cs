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

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Tests.Unit.Presenters
{
    [TestClass]
    public class ExportFilterFormPresenterTests
    {
        private Mock<IExportFilterFormView> mockExportView;
        private ExportFilterFormPresenter systemUnderTest;

        [TestInitialize]
        public void TestSetup()
        {
            mockExportView = new Mock<IExportFilterFormView>();

            systemUnderTest = new ExportFilterFormPresenter(mockExportView.Object);
        }

        [TestMethod]
        public void OnVisible_ShouldShowMessageAndCloseWhenNullSchemaProvided()
        {
            //Arrange
            mockExportView
                .SetupGet(x => x.SchemaConfiguration)
                .Returns(() => null);

            // Act
            mockExportView.Raise(x => x.OnVisible += null, EventArgs.Empty);

            // Assert
            mockExportView.Verify(x => x.ShowMessage(
                    "Please specify a schema file with atleast one entity defined.",
                    "No entities available",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Information), Times.Once);
            mockExportView.Verify(x => x.Close(), Times.Once);
            mockExportView.VerifySet(x => x.EntityList = It.IsAny<IEnumerable<ListBoxItem<CrmEntity>>>(), Times.Never);
            mockExportView.VerifySet(x => x.SelectedEntity = It.IsAny<CrmEntity>(), Times.Never);
        }

        [TestMethod]
        public void OnVisible_ShouldShowMessageAndCloseWhenEmptySchemaProvided()
        {
            //Arrange
            mockExportView
                .SetupGet(x => x.SchemaConfiguration)
                .Returns(new CrmSchemaConfiguration());

            // Act
            mockExportView.Raise(x => x.OnVisible += null, EventArgs.Empty);

            // Assert
            mockExportView.Verify(x => x.ShowMessage(
                    "Please specify a schema file with atleast one entity defined.",
                    "No entities available",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Information), Times.Once);
            mockExportView.Verify(x => x.Close(), Times.Once);
            mockExportView.VerifySet(x => x.EntityList = It.IsAny<IEnumerable<ListBoxItem<CrmEntity>>>(), Times.Never);
            mockExportView.VerifySet(x => x.SelectedEntity = It.IsAny<CrmEntity>(), Times.Never);
        }

        [TestMethod]
        public void OnVisible_ShouldPopulateEntityListAndSelectedEntityWhenSchemaProvidedContainsEntities()
        {
            //Arrange
            var schema = new CrmSchemaConfiguration();
            schema.Entities.Add(new CrmEntity
            {
                DisplayName = "Entity",
                Name = "entity"
            });

            mockExportView
                .SetupGet(x => x.SchemaConfiguration)
                .Returns(schema);
            mockExportView
                .SetupGet(x => x.EntityList)
                .Returns(schema.Entities.Select(x => new ListBoxItem<CrmEntity> { DisplayName = x.DisplayName, Item = x }));
            mockExportView
                .SetupGet(x => x.EntityFilters)
                .Returns(new Dictionary<string, string>());

            // Act
            mockExportView.Raise(x => x.OnVisible += null, EventArgs.Empty);

            // Assert
            mockExportView.VerifySet(
                x => x.EntityList = It.Is<IEnumerable<ListBoxItem<CrmEntity>>>(a => 
                    a.Count() == 1 &&
                    a.First().DisplayName == "Entity" &&
                    a.First().Item == schema.Entities.FirstOrDefault()), 
                Times.Once);
            mockExportView.VerifySet(x => x.SelectedEntity = schema.Entities.FirstOrDefault(), Times.Once);
        }

        [TestMethod]
        public void OnVisible_ShouldRemoveEntityListWhenNoLongerPresentInTheSchema()
        {
            //Arrange
            var entity = new CrmEntity
            {
                DisplayName = "Entity1",
                Name = "entity1"
            };
            var schemaOld = new CrmSchemaConfiguration();
            schemaOld.Entities.Add(entity);
            mockExportView.SetupGet(x => x.SchemaConfiguration).Returns(schemaOld);
            mockExportView.SetupGet(x => x.EntityList).Returns(schemaOld.Entities.Select(x => new ListBoxItem<CrmEntity> { DisplayName = x.DisplayName, Item = x }));
            mockExportView.SetupGet(x => x.EntityFilters).Returns(new Dictionary<string, string>());
            mockExportView.SetupGet(x => x.SelectedEntity).Returns(entity);
            mockExportView.SetupGet(x => x.FilterText).Returns("<filter></filter>");

            mockExportView.Raise(x => x.OnVisible += null, EventArgs.Empty); // Loads old schema entities
            mockExportView.Raise(x => x.OnEntitySelected += null, EventArgs.Empty);

            var schemaNew = new CrmSchemaConfiguration();
            schemaNew.Entities.Add(new CrmEntity
            {
                DisplayName = "Entity2",
                Name = "entity2"
            });
            mockExportView
                .SetupGet(x => x.SchemaConfiguration)
                .Returns(schemaNew);
            mockExportView
                .SetupGet(x => x.EntityList)
                .Returns(schemaNew.Entities.Select(x => new ListBoxItem<CrmEntity> { DisplayName = x.DisplayName, Item = x }));

            // Act
            mockExportView.Raise(x => x.OnVisible += null, EventArgs.Empty);

            // Assert
            mockExportView.VerifySet(
                x => x.EntityList = It.Is<IEnumerable<ListBoxItem<CrmEntity>>>(a =>
                    a.Count() == 1 &&
                    a.First().DisplayName == "Entity2" &&
                    a.First().Item == schemaNew.Entities.FirstOrDefault()),
                Times.Once);
            mockExportView.VerifySet(x => x.SelectedEntity = schemaNew.Entities.FirstOrDefault(), Times.Once);
        }

        [TestMethod]
        public void OnEntitySelected_ShouldUpdateFilterTextWithFilterWhenEntityAlreadyHasAFilter()
        {
            //Arrange
            var entityFilters = new Dictionary<string, string>
            {
                { "entity", "<filter></filter/>" }
            };
            mockExportView
                .SetupGet(x => x.SelectedEntity)
                .Returns(new CrmEntity { Name = "entity" });
            mockExportView
                .SetupGet(x => x.EntityFilters)
                .Returns(entityFilters);

            // Act
            mockExportView.Raise(x => x.OnEntitySelected += null, EventArgs.Empty);

            // Assert
            mockExportView.VerifySet(x => x.FilterText = "<filter></filter/>", Times.Once);
        }

        [TestMethod]
        public void OnEntitySelected_ShouldUpdateFilterTextToEmptyStringWhenNoFilterExistsForSelectedEntity()
        {
            //Arrange
            var entityFilters = new Dictionary<string, string>();
            mockExportView
                .SetupGet(x => x.SelectedEntity)
                .Returns(new CrmEntity { Name = "entity" });
            mockExportView
                .SetupGet(x => x.EntityFilters)
                .Returns(entityFilters);

            // Act
            mockExportView.Raise(x => x.OnEntitySelected += null, EventArgs.Empty);

            // Assert
            mockExportView.VerifySet(x => x.FilterText = String.Empty, Times.Once);
        }

        [TestMethod]
        public void UpdateFilterForEntity_ShouldUpdateEntityFiltersForCurrentlySelectedEntity()
        {
            //Arrange
            var entityFilters = new Dictionary<string, string>();
            mockExportView
                .SetupGet(x => x.SelectedEntity)
                .Returns(new CrmEntity { Name = "entity" });
            mockExportView
                .SetupGet(x => x.FilterText)
                .Returns("<filter></filter/>");
            mockExportView
                .SetupGet(x => x.EntityFilters)
                .Returns(entityFilters);

            // Act
            mockExportView.Raise(x => x.OnFilterTextChanged += null, EventArgs.Empty);

            // Assert
            entityFilters.ContainsKey("entity").Should().BeTrue();
            entityFilters["entity"].Should().Be("<filter></filter/>");
        }
    }
}
