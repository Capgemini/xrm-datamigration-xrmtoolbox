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
            mockExportView = new Mock<IExportLookupMappingsView>();
            systemUnderTest = new ExportLookupMappingsFormPresenter(mockExportView.Object);
            SetupServiceMocks();
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
            mockExportView.Raise(x => x.OnVisible += null, EventArgs.Empty);

                // Assert
                mockExportView.Verify(x => x.ShowMessage(
                        "Please make sure you are connected to an organisation", "No connection madde",
                        System.Windows.Forms.MessageBoxButtons.OK,
                        System.Windows.Forms.MessageBoxIcon.Information), Times.Once);
                mockExportView.Verify(x => x.Close(), Times.Once);
                mockExportView.VerifySet(x => x.EntityListDataSource = It.IsAny<List<string>>(), Times.Never);
        }

        [TestMethod]
        public void OnVisible()
        {
            using (var systemUnderTest = new ExportLookupMappingsFormPresenter(mockExportView.Object))
            {
                string entityLogicalName = "account";
                SetupMockObjects(entityLogicalName);

                systemUnderTest.OrganizationService = ServiceMock.Object;
                systemUnderTest.MetaDataService = MetadataServiceMock.Object;

                mockExportView.Raise(x => x.OnVisible += null, EventArgs.Empty);
                mockExportView.VerifySet(x => x.EntityListDataSource = It.IsAny<List<string>>(), Times.Once);
            }
        }

        [TestMethod]
        public void OnMapFieldChanged()
        {
            using (var systemUnderTest = new ExportLookupMappingsFormPresenter(mockExportView.Object))
            {
                string entityLogicalName = "account";
                SetupMockObjects(entityLogicalName);

                systemUnderTest.OrganizationService = ServiceMock.Object;
                systemUnderTest.MetaDataService = MetadataServiceMock.Object;
                systemUnderTest.ExceptionService = ExceptionServicerMock.Object;
                mockExportView
                    .SetupGet(x => x.MappingCells)
                    .Returns(new List<string> { null, null});
                mockExportView
                    .SetupGet(x => x.CurrentCell)
                    .Returns("account");

                mockExportView.Raise(x => x.OnEntityColumnChanged += null, EventArgs.Empty);
                mockExportView.VerifySet(x => x.SetRefFieldDataSource = It.IsAny<AttributeMetadata[]>(), Times.Once);
            }
        }

        [TestMethod]
        public void OnRefFieldChanged()
        {
            using (var systemUnderTest = new ExportLookupMappingsFormPresenter(mockExportView.Object))
            {
                string entityLogicalName = "account";
                SetupMockObjects(entityLogicalName);

                systemUnderTest.OrganizationService = ServiceMock.Object;
                systemUnderTest.MetaDataService = MetadataServiceMock.Object;
                systemUnderTest.ExceptionService = ExceptionServicerMock.Object;
                mockExportView
                    .SetupGet(x => x.FirstCellInRow)
                    .Returns("account");

                mockExportView.Raise(x => x.OnRefFieldChanged += null, EventArgs.Empty);
                mockExportView.VerifySet(x => x.SetMapFieldDataSource = It.IsAny<AttributeMetadata []>(), Times.Once);
            }
        }
    }

}
