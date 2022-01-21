using Capgemini.DataMigration.Core;
using Capgemini.Xrm.CdsDataMigratorLibrary.Enums;
using Capgemini.Xrm.CdsDataMigratorLibrary.Exceptions;
using Capgemini.Xrm.CdsDataMigratorLibrary.Models;
using Capgemini.Xrm.CdsDataMigratorLibrary.Presenters;
using Capgemini.Xrm.CdsDataMigratorLibrary.Services;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Capgemini.Xrm.CdsDataMigrator.Tests.Unit.Presenters
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
                                .Throws<OrganizationalServiceException>();
            logger.Setup(a => a.LogError(It.IsAny<string>()));

            FluentActions.Invoking(() => systemUnderTest.ExportData(null, new EventArgs()))
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

            FluentActions.Invoking(() => systemUnderTest.ExportData(null, new EventArgs()))
                .Should()
                .NotThrow();

            exportView.VerifyAll();
            logger.Verify(a => a.LogError(It.IsAny<string>()), Times.Never);
            dataMigrationService.VerifyAll();
        }

        [TestMethod]
        public void CancelAction()
        {
            dataMigrationService.Setup(a => a.CancelDataExport());

            FluentActions.Invoking(() => systemUnderTest.CancelAction(null, new EventArgs()))
                .Should()
                .NotThrow();

            dataMigrationService.VerifyAll();
        }

        [TestMethod]
        public void CancelActionThrowsException()
        {
            dataMigrationService.Setup(a => a.CancelDataExport()).Throws<Exception>();

            FluentActions.Invoking(() => systemUnderTest.CancelAction(null, new EventArgs()))
                .Should()
                .NotThrow();

            logger.Verify(a => a.LogError(It.IsAny<string>()), Times.Once);
            dataMigrationService.VerifyAll();
        }
    }
}