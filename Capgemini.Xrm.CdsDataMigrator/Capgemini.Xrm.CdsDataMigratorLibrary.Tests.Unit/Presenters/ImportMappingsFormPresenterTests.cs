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
using System.Windows.Forms;

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
            systemUnderTest.OrganizationService = ServiceMock.Object;
            systemUnderTest.MetaDataService = MetadataServiceMock.Object;
            systemUnderTest.ViewHelpers = ViewHelpersMock.Object;
        }

        [TestMethod]
        public void ImportLookupMappingsFormInstantiation()
        {
            FluentActions.Invoking(() => new ImportMappingsFormPresenter(mockImportView.Object))
                 .Should()
                 .NotThrow();
        }
        
        [TestMethod]
        public void OnVisible_ShouldShowMessageAndCloseWhenNullOrgServiceProvided()
        {
            // Act
            systemUnderTest.OrganizationService = null;
            mockImportView.Raise(x => x.OnVisible += null, EventArgs.Empty);

            // Assert
            ViewHelpersMock.Verify(x => x.ShowMessage("Please specify a schema file with atleast one entity defined.", "No entities available", MessageBoxButtons.OK, MessageBoxIcon.Error), Times.Once);
            mockImportView.Verify(x => x.Close(), Times.Once);
            mockImportView.VerifySet(x => x.EntityListDataSource = It.IsAny<List<string>>(), Times.Never);
        }

        [TestMethod]
        public void OnVisible()
        {
            string entityLogicalName = "account";
            SetupMockObjects(entityLogicalName);
            mockImportView.Raise(x => x.OnVisible += null, EventArgs.Empty);
            mockImportView.VerifySet(x => x.EntityListDataSource = It.IsAny<IEnumerable<string>>(), Times.Once);
        }
    }
}
