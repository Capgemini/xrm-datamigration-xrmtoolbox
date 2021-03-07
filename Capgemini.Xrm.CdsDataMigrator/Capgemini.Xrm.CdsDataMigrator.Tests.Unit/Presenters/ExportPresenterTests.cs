using Capgemini.DataMigration.Core;
using Capgemini.Xrm.DataMigration.XrmToolBox.Enums;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Models;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Services;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Views;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Presenters.Tests
{
    [TestClass]
    public class ExportPresenterTests
    {
        private Mock<IExportView> exportView;
        private Mock<ILogger> logger;
        private Mock<IDataMigrationService> dataMigrationService;
        private ExportPresenter systemUnderTest;

        [TestInitialize]
        public void TestSetup()
        {
            exportView = new Mock<IExportView>();
            logger = new Mock<ILogger>();
            dataMigrationService = new Mock<IDataMigrationService>();

            systemUnderTest = new ExportPresenter(exportView.Object, logger.Object, dataMigrationService.Object);
        }

        [TestMethod]
        public void ExportPresenterIntantiation()
        {
            FluentActions.Invoking(() => new ExportPresenter(exportView.Object, logger.Object, dataMigrationService.Object))
                .Should()
                .NotThrow();
        }

        [TestMethod]
        public void GetExportSettingsObjectWhenFormatJsonSelectedIsTrue()
        {
            exportView.SetupGet(a => a.FormatJsonSelected).Returns(true);

            var actual = systemUnderTest.GetExportSettingsObject();

            actual.DataFormat.Should().Be(DataFormat.Json);

            exportView.VerifyAll();
        }

        [TestMethod]
        public void GetExportSettingsObjectWhenFormatJsonSelectedIsFalse()
        {
            exportView.SetupGet(a => a.FormatJsonSelected).Returns(false);

            var actual = systemUnderTest.GetExportSettingsObject();

            actual.DataFormat.Should().NotBe("json");

            exportView.VerifyAll();
        }

        [TestMethod]
        public void GetExportSettingsObjectWhenFormatCsvSelectedIsTrue()
        {
            exportView.SetupGet(a => a.FormatCsvSelected).Returns(true);

            var actual = systemUnderTest.GetExportSettingsObject();

            actual.DataFormat.Should().Be(DataFormat.Csv);

            exportView.VerifyAll();
        }

        [TestMethod]
        public void GetExportSettingsObjectWhenFormatCsvSelectedIsFalse()
        {
            exportView.SetupGet(a => a.FormatCsvSelected).Returns(false);

            var actual = systemUnderTest.GetExportSettingsObject();

            actual.DataFormat.Should().NotBe("csv");

            exportView.VerifyAll();
        }

        [TestMethod]
        public void ExportDataActionThrowsException()
        {
            exportView.SetupGet(a => a.FormatCsvSelected).Returns(false);
            dataMigrationService.Setup(a => a.ExportData(It.IsAny<ExportSettings>()))
                                .Throws<Exceptions.OrganizationalServiceException>();
            logger.Setup(a => a.LogError(It.IsAny<string>()));

            FluentActions.Invoking(() => systemUnderTest.ExportDataAction())
                .Should()
                .NotThrow();

            exportView.VerifyAll();
            logger.Verify(a => a.LogError(It.IsAny<string>()), Times.Once);
            dataMigrationService.VerifyAll();
        }

        [TestMethod]
        public void ExportDataActionDoesNotThrowException()
        {
            exportView.SetupGet(a => a.FormatCsvSelected).Returns(false);
            dataMigrationService.Setup(a => a.ExportData(It.IsAny<ExportSettings>()));
            logger.Setup(a => a.LogError(It.IsAny<string>()));

            FluentActions.Invoking(() => systemUnderTest.ExportDataAction())
                .Should()
                .NotThrow();

            exportView.VerifyAll();
            logger.Verify(a => a.LogError(It.IsAny<string>()), Times.Never);
            dataMigrationService.VerifyAll();
        }
    }
}