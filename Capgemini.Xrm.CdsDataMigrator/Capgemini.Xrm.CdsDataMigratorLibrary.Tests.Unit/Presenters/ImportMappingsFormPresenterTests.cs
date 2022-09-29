using Capgemini.Xrm.CdsDataMigrator.Tests.Unit;
using Capgemini.Xrm.CdsDataMigratorLibrary.Presenters;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Moq;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Tests.Unit.Presenters
{
    [TestClass]
    public class ImportMappingsFormPresenterTests : TestBase
    {
        private Mock<IImportMappingsFormView> mockImportView;
        private ImportMappingsFormPresenter systemUnderTest;
        private Mock<Func<IOrganizationService>> mockOrganizationServiceGetter;

        [TestInitialize]
        public void TestSetup()
        {
            SetupServiceMocks();
            mockImportView = new Mock<IImportMappingsFormView>();
            mockOrganizationServiceGetter = new Mock<Func<IOrganizationService>>();
            mockOrganizationServiceGetter.SetReturnsDefault(ServiceMock.Object);

            systemUnderTest = new ImportMappingsFormPresenter(
                mockImportView.Object,
                MetadataServiceMock.Object,
                ViewHelpersMock.Object,
                mockOrganizationServiceGetter.Object);
        }

        [TestMethod]
        public void ImportLookupMappingsFormPresenterInstantiation()
        {
            FluentActions.Invoking(() => new ImportMappingsFormPresenter(
                mockImportView.Object,
                MetadataServiceMock.Object,
                ViewHelpersMock.Object,
                mockOrganizationServiceGetter.Object))
                 .Should()
                 .NotThrow();
        }
        
        [TestMethod]
        public void OnVisible_ShouldShowMessageAndCloseWhenNullOrgServiceProvided()
        {
            // Act
            mockOrganizationServiceGetter.SetReturnsDefault(null as IOrganizationService);
            mockImportView.Raise(x => x.OnVisible += null, EventArgs.Empty);

            // Assert
            ViewHelpersMock.Verify(x => x.ShowMessage("Please make sure you are connected to an organisation", "No connection made", MessageBoxButtons.OK, MessageBoxIcon.Information), Times.Once);
            mockImportView.Verify(x => x.Close(), Times.Once);
            mockImportView.VerifySet(x => x.EntityListDataSource = It.IsAny<List<string>>(), Times.Never);
        }

        [TestMethod]
        public void OnVisible_ShouldPopulateEntityListAndSelectedEntity()
        {
            string entityLogicalName = "account";
            SetupMockObjects(entityLogicalName);
            mockImportView.Raise(x => x.OnVisible += null, EventArgs.Empty);
            mockImportView.VerifySet(x => x.EntityListDataSource = It.IsAny<IEnumerable<string>>(), Times.Once);
        }
    }
}
