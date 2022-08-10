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
    public class ImportMappingsFormPresenterTests
    {
        private Mock<IImportMappingsFormView> mockImportView;
        private ImportMappingsFormPresenter systemUnderTest;

        [TestInitialize]
        public void TestSetup()
        {
            mockImportView = new Mock<IImportMappingsFormView>();

            systemUnderTest = new ImportMappingsFormPresenter(mockImportView.Object);
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
            mockImportView.Verify(x => x.ShowMessage(
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
            mockImportView.Verify(x => x.ShowMessage(
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
                DisplayName = "Entity",
                Name = "entity"

            });

            mockImportView
                .Setup(x => x.SchemaConfiguration)
                .Returns(schema);
            mockImportView
                .SetupGet(x => x.EntityList)
                .Returns(schema.Entities.Select(x => x.DisplayName).OrderBy(n => n));

            // Act
            mockImportView.Raise(x => x.OnVisible += null, EventArgs.Empty);

            // Assert
            mockImportView.Object.EntityList.Count().Should().Be(1);
            mockImportView.VerifySet(x => x.EntityList = It.Is<IEnumerable<string>>(a => a.First() == "Entity" && a.First() == schema.Entities.FirstOrDefault().DisplayName), Times.Once);
        }
    }
}
