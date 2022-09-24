using Capgemini.Xrm.CdsDataMigrator.Tests.Unit;
using Capgemini.Xrm.CdsDataMigratorLibrary.Helpers;
using Capgemini.Xrm.CdsDataMigratorLibrary.Presenters;
using Capgemini.Xrm.DataMigration.Config;
using Capgemini.Xrm.DataMigration.Model;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Tests.Unit.Presenters
{
    [TestClass]
    public class ImportMappingsFormPresenterTests : TestBase
    {
        private Mock<IImportMappingsFormView> mockImportView;
        private ImportMappingsFormPresenter systemUnderTest;

        [TestInitialize]
        public void TestSetup()
        {
            SetupServiceMocks();
            mockImportView = new Mock<IImportMappingsFormView>();

            systemUnderTest = new ImportMappingsFormPresenter(mockImportView.Object);
            systemUnderTest.ViewHelpers = ViewHelpersMock.Object;
        }

        [TestMethod]
        public void OnVisible_ShouldShowMessageAndCloseWhenNullSchemaProvided()
        {
            // Arrange
            mockImportView
                .SetupGet(x => x.SchemaConfiguration)
                .Returns(() => null);

            // Act
            mockImportView.Raise(x => x.OnVisible += null, EventArgs.Empty);

            // Assert
            ViewHelpersMock.Verify(x => x.ShowMessage(
                    "Please specify a schema file with atleast one entity defined.",
                    "No entities available",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Information), Times.Once);
            mockImportView.Verify(x => x.Close(), Times.Once);
            mockImportView.VerifySet(x => x.EntityList = It.IsAny<List<string>>(), Times.Never);
        }

        [TestMethod]
        public void OnVisible_ShouldShowMessageAndCloseWhenEmptySchemaProvided()
        {
            // Arrange
            mockImportView
                .SetupGet(x => x.SchemaConfiguration)
                .Returns(() => new CrmSchemaConfiguration());

            // Act
            mockImportView.Raise(x => x.OnVisible += null, EventArgs.Empty);

            // Assert
            ViewHelpersMock.Verify(x => x.ShowMessage(
                    "Please specify a schema file with atleast one entity defined.",
                    "No entities available",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Information), Times.Once);
            mockImportView.Verify(x => x.Close(), Times.Once);
            mockImportView.VerifySet(x => x.EntityList = It.IsAny<List<string>>(), Times.Never);
        }

        [TestMethod]
        public void OnVisible_ShouldPopulateEntityListAndSelectedEntityWhenSchemaProvidedContainsEntities()
        {
            // Arrange
            var schema = new CrmSchemaConfiguration();
            schema.Entities.Add(new CrmEntity
            {
                Name = "entity"

            });

            mockImportView
                .Setup(x => x.SchemaConfiguration)
                .Returns(schema);
            mockImportView
                .SetupGet(x => x.EntityList)
                .Returns(schema.Entities.Select(x => x.Name).OrderBy(n => n));

            // Act
            mockImportView.Raise(x => x.OnVisible += null, EventArgs.Empty);

            // Assert
            mockImportView.Object.EntityList.Count().Should().Be(1);
            mockImportView.VerifySet(x => x.EntityList = It.Is<IEnumerable<string>>(a => a.First() == schema.Entities.FirstOrDefault().Name), Times.Once);
        }

        [TestMethod]
        public void OnVisible_ShouldRemoveEntityListWhenNoLongerPresentInTheSchema()
        {
            // Arrange
            var entity = new CrmEntity
            {
                Name = "entity1"
            };
            var schemaOld = new CrmSchemaConfiguration();
            schemaOld.Entities.Add(entity);
            mockImportView.SetupGet(x => x.SchemaConfiguration).Returns(schemaOld);
            mockImportView
                .SetupGet(x => x.EntityList)
                .Returns(schemaOld.Entities.Select(x => x.Name).OrderBy(n => n));

            mockImportView.Raise(x => x.OnVisible += null, EventArgs.Empty); // Loads old schema entities

            var schemaNew = new CrmSchemaConfiguration();
            schemaNew.Entities.Add(new CrmEntity
            {
                Name = "entity2"
            });

            mockImportView
                .SetupGet(x => x.SchemaConfiguration)
                .Returns(schemaNew);
            mockImportView
                .SetupGet(x => x.EntityList)
                .Returns(schemaNew.Entities.Select(x => x.Name).OrderBy(n => n));

            // Act
            mockImportView.Raise(x => x.OnVisible += null, EventArgs.Empty);

            // Assert
            mockImportView.Object.EntityList.Count().Should().Be(1);
            mockImportView.VerifySet(
               x => x.EntityList = It.Is<IEnumerable<string>>(a =>
                   a.First() == schemaNew.Entities.FirstOrDefault().Name),
               Times.Once);
        }
    }
}
