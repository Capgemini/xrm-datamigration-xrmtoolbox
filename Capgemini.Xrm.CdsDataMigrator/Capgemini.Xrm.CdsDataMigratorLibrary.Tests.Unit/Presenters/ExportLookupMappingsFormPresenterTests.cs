using Capgemini.Xrm.CdsDataMigratorLibrary.Presenters;
using Capgemini.Xrm.CdsDataMigratorLibrary.Services;
using Capgemini.Xrm.DataMigration.Config;
using Capgemini.Xrm.DataMigration.Model;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Tests.Unit.Presenters
{
    [TestClass]
    public class ExportLookupMappingsFormPresenterTests
    {
        private Mock<IExportLookupMappingsView> mockExportView;
        private Mock<MetadataService> metaDataService;
        private Mock<IOrganizationService> orgService;
        private ExportLookupMappingsFormPresenter systemUnderTest;

        [TestInitialize]
        public void TestSetup()
        {
            mockExportView = new Mock<IExportLookupMappingsView>();
            orgService = new Mock<IOrganizationService>();
            metaDataService = new Mock<MetadataService>();
            systemUnderTest = new ExportLookupMappingsFormPresenter(mockExportView.Object);
        }

        [TestMethod]
        public void ExportLookupMappingsFormInstantiation()
        {
            FluentActions.Invoking(() => new ExportLookupMappingsFormPresenter(mockExportView.Object))
                 .Should()
                 .NotThrow();
        }
    }
}
