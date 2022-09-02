using Capgemini.Xrm.CdsDataMigratorLibrary.Exceptions;
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
using XrmToolBox.Extensibility;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Tests.Unit.Presenters
{
    [TestClass]
    public class ExportLookupMappingsFormPresenterTests
    {
        private Mock<IExportLookupMappingsView> mockExportView;
        private ExportLookupMappingsFormPresenter systemUnderTest;

        [TestInitialize]
        public void TestSetup()
        {
            mockExportView = new Mock<IExportLookupMappingsView>();
            systemUnderTest = new ExportLookupMappingsFormPresenter(mockExportView.Object);
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
                mockExportView.VerifySet(x => x.EntityList = It.IsAny<List<string>>(), Times.Never);
        }
    }

}
