using Capgemini.Xrm.CdsDataMigrator.Tests.Unit;
using Capgemini.Xrm.CdsDataMigratorLibrary.Presenters;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk.Metadata;
using Moq;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Tests.Unit.Presenters
{
    [TestClass]
    public class ExportLookupMappingsFormPresenterTests : TestBase
    {
        private Mock<IExportLookupMappingsView> mockExportView;
        private ExportLookupMappingsFormPresenter systemUnderTest;

        [TestInitialize]
        public void TestSetup()
        {
            SetupServiceMocks();
            mockExportView = new Mock<IExportLookupMappingsView>();
            systemUnderTest = new ExportLookupMappingsFormPresenter(mockExportView.Object);
            systemUnderTest.OrganizationService = ServiceMock.Object;
            systemUnderTest.MetaDataService = MetadataServiceMock.Object;
            systemUnderTest.ExceptionService = ExceptionServicerMock.Object;
            systemUnderTest.ViewHelpers = ViewHelpersMock.Object;
        }

        [TestMethod]
        public void ExportLookupMappingsFormInstantiation()
        {
            FluentActions.Invoking(() => new ExportLookupMappingsFormPresenter(mockExportView.Object))
                 .Should()
                 .NotThrow();
        }

        [TestMethod]
        public void OnVisible_ShouldShowMessageAndCloseWhenNullOrgServiceProvided()
        {
            // Act
            systemUnderTest.OrganizationService = null;
            mockExportView.Raise(x => x.OnVisible += null, EventArgs.Empty);

            // Assert
            ViewHelpersMock.Verify(x => x.ShowMessage(
                    "Please make sure you are connected to an organisation", "No connection made",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error), Times.Once);
            mockExportView.Verify(x => x.Close(), Times.Once);
            mockExportView.VerifySet(x => x.EntityListDataSource = It.IsAny<List<string>>(), Times.Never);
        }

        [TestMethod]
        public void OnVisible()
        {
            string entityLogicalName = "account";
            SetupMockObjects(entityLogicalName);
            mockExportView.Raise(x => x.OnVisible += null, EventArgs.Empty);
            mockExportView.VerifySet(x => x.EntityListDataSource = It.IsAny<IEnumerable<string>>(), Times.Once);
        }

        [TestMethod]
        public void OnMapFieldChanged()
        {
            string entityLogicalName = "account";
            SetupMockObjects(entityLogicalName);
            mockExportView
                .SetupGet(x => x.CurrentCell)
                .Returns("account");

            mockExportView.Raise(x => x.OnEntityColumnChanged += null, EventArgs.Empty);
            mockExportView.Verify(x => x.SetRefFieldDataSource(It.IsAny<AttributeMetadata[]>()), Times.Once);
        }

        [TestMethod]
        public void OnRefFieldChanged()
        {
            string entityLogicalName = "account";
            SetupMockObjects(entityLogicalName);
            mockExportView
                .SetupGet(x => x.CurrentRowEntityName)
                .Returns("account");

            mockExportView.Raise(x => x.OnRefFieldChanged += null, EventArgs.Empty);
            mockExportView.Verify(x => x.SetMapFieldDataSource(It.IsAny<AttributeMetadata[]>()), Times.Once);
        }
    }

}
