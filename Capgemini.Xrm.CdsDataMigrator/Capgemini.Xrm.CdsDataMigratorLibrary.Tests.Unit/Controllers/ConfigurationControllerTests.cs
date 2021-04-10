using System;
using System.Collections.Generic;
using Capgemini.Xrm.CdsDataMigratorLibrary.Controllers;
using Capgemini.Xrm.CdsDataMigratorLibrary.Core;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Moq;

namespace Capgemini.Xrm.CdsDataMigrator.Tests.Unit.Controllers
{
    [TestClass]
    public class ConfigurationControllerTests : TestBase
    {
        private ConfigurationController systemUnderTest;

        [TestInitialize]
        public void Setup()
        {
            SetupServiceMocks();

            systemUnderTest = new ConfigurationController();
        }

        [TestMethod]
        public void LoadExportConfigFileWithEmptyExportConfigPath()
        {
            string exportConfigFilename = string.Empty;
            var inputFilterQuery = new Dictionary<string, string>();
            var inputLookupMaping = new Dictionary<string, Dictionary<string, List<string>>>();

            NotificationServiceMock.Setup(x => x.DisplayFeedback(It.IsAny<string>()))
                                .Verifiable();

            using (System.Windows.Forms.TextBox exportConfigTextBox = new System.Windows.Forms.TextBox())
            {
                exportConfigTextBox.Text = exportConfigFilename;

                FluentActions.Invoking(() => systemUnderTest.LoadExportConfigFile(NotificationServiceMock.Object, exportConfigTextBox, inputFilterQuery, inputLookupMaping))
                             .Should()
                             .NotThrow();
            }

            NotificationServiceMock.Verify(x => x.DisplayFeedback(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void LoadExportConfigFileWithInValidExportConfigPath()
        {
            string exportConfigFilename = "hello.txt";
            var inputFilterQuery = new Dictionary<string, string>();
            var inputLookupMaping = new Dictionary<string, Dictionary<string, List<string>>>();

            NotificationServiceMock.Setup(x => x.DisplayFeedback("Invalid Export Config File"))
                                .Verifiable();

            using (System.Windows.Forms.TextBox exportConfigTextBox = new System.Windows.Forms.TextBox())
            {
                exportConfigTextBox.Text = exportConfigFilename;

                FluentActions.Invoking(() => systemUnderTest.LoadExportConfigFile(NotificationServiceMock.Object, exportConfigTextBox, inputFilterQuery, inputLookupMaping))
                             .Should()
                             .NotThrow();
            }

            NotificationServiceMock.Verify(x => x.DisplayFeedback("Invalid Export Config File"), Times.Once);
        }

        [TestMethod]
        public void LoadExportConfigFileWithValidExportConfigPath()
        {
            string exportConfigFilename = "TestData\\ExportConfig.json";
            var inputFilterQuery = new Dictionary<string, string>();
            var inputLookupMaping = new Dictionary<string, Dictionary<string, List<string>>>();

            NotificationServiceMock.Setup(x => x.DisplayFeedback("Filters and Lookup Mappings loaded from Export Config File"))
                                .Verifiable();

            using (System.Windows.Forms.TextBox exportConfigTextBox = new System.Windows.Forms.TextBox())
            {
                exportConfigTextBox.Text = exportConfigFilename;

                FluentActions.Invoking(() => systemUnderTest.LoadExportConfigFile(NotificationServiceMock.Object, exportConfigTextBox, inputFilterQuery, inputLookupMaping))
                             .Should()
                             .NotThrow();
            }

            NotificationServiceMock.Verify(x => x.DisplayFeedback("Filters and Lookup Mappings loaded from Export Config File"), Times.Once);
        }

        [TestMethod]
        public void LoadExportConfigFileThrowsException()
        {
            string exportConfigFilename = "TestData\\ExportConfig.json";
            var exception = new Exception("TestException here!");
            var inputFilterQuery = new Dictionary<string, string>();
            var inputLookupMaping = new Dictionary<string, Dictionary<string, List<string>>>();

            NotificationServiceMock.Setup(x => x.DisplayFeedback("Filters and Lookup Mappings loaded from Export Config File"))
                               .Throws(exception);

            NotificationServiceMock.Setup(x => x.DisplayFeedback($"Load Correct Export Config file, error:{exception.Message}"))
                                .Verifiable();

            using (System.Windows.Forms.TextBox exportConfigTextBox = new System.Windows.Forms.TextBox())
            {
                exportConfigTextBox.Text = exportConfigFilename;

                FluentActions.Invoking(() => systemUnderTest.LoadExportConfigFile(NotificationServiceMock.Object, exportConfigTextBox, inputFilterQuery, inputLookupMaping))
                             .Should()
                             .NotThrow();
            }

            NotificationServiceMock.Verify(x => x.DisplayFeedback($"Load Correct Export Config file, error:{exception.Message}"), Times.Once);
        }

        [TestMethod]
        public void LoadImportConfigFileWithNoImportConfig()
        {
            var inputMapper = new Dictionary<string, Dictionary<Guid, Guid>>();
            var inputMapping = new Dictionary<string, List<Item<EntityReference, EntityReference>>>();

            NotificationServiceMock.Setup(x => x.DisplayFeedback(It.IsAny<string>()))
                                .Verifiable();

            using (var importConfig = new System.Windows.Forms.TextBox())
            {
                FluentActions.Invoking(() => systemUnderTest.LoadImportConfigFile(NotificationServiceMock.Object, importConfig, inputMapper, inputMapping))
                                 .Should()
                                 .NotThrow();
            }

            NotificationServiceMock.Verify(x => x.DisplayFeedback(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void LoadImportConfigFileWithInvalidImportConfigFilePath()
        {
            var inputMapper = new Dictionary<string, Dictionary<Guid, Guid>>();
            var inputMapping = new Dictionary<string, List<Item<EntityReference, EntityReference>>>();

            NotificationServiceMock.Setup(x => x.DisplayFeedback("Invalid Import Config File"))
                                .Verifiable();

            using (var importConfig = new System.Windows.Forms.TextBox())
            {
                importConfig.Text = "Hello.txt";

                FluentActions.Invoking(() => systemUnderTest.LoadImportConfigFile(NotificationServiceMock.Object, importConfig, inputMapper, inputMapping))
                                .Should()
                                .NotThrow();
            }

            NotificationServiceMock.Verify(x => x.DisplayFeedback(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void LoadImportConfigFileWithValidImportConfigFilePathButNoMigrationConfig()
        {
            var inputMapper = new Dictionary<string, Dictionary<Guid, Guid>>();
            var inputMapping = new Dictionary<string, List<Item<EntityReference, EntityReference>>>();

            NotificationServiceMock.Setup(x => x.DisplayFeedback("Invalid Import Config File"))
                                .Verifiable();

            using (var importConfig = new System.Windows.Forms.TextBox())
            {
                importConfig.Text = "TestData\\ImportConfig.json";

                FluentActions.Invoking(() => systemUnderTest.LoadImportConfigFile(NotificationServiceMock.Object, importConfig, inputMapper, inputMapping))
                                 .Should()
                                 .NotThrow();
            }

            NotificationServiceMock.Verify(x => x.DisplayFeedback(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void LoadImportConfigFileWithValidImportConfigFilePathAndMigrationConfig()
        {
            var inputMapper = new Dictionary<string, Dictionary<Guid, Guid>>();
            var inputMapping = new Dictionary<string, List<Item<EntityReference, EntityReference>>>();

            NotificationServiceMock.Setup(x => x.DisplayFeedback("Guid Id Mappings loaded from Import Config File"))
                                .Verifiable();

            using (var importConfig = new System.Windows.Forms.TextBox())
            {
                importConfig.Text = "TestData/ImportConfig2.json";

                FluentActions.Invoking(() => systemUnderTest.LoadImportConfigFile(NotificationServiceMock.Object, importConfig, inputMapper, inputMapping))
                                 .Should()
                                 .NotThrow();
            }

            NotificationServiceMock.Verify(x => x.DisplayFeedback(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void LoadImportConfigFileHandleException()
        {
            var inputMapper = new Dictionary<string, Dictionary<Guid, Guid>>();
            var inputMapping = new Dictionary<string, List<Item<EntityReference, EntityReference>>>();

            NotificationServiceMock.Setup(x => x.DisplayFeedback("Guid Id Mappings loaded from Import Config File"))
                .Throws<Exception>();

            using (var importConfig = new System.Windows.Forms.TextBox())
            {
                importConfig.Text = "TestData/ImportConfig2.json";

                FluentActions.Invoking(() => systemUnderTest.LoadImportConfigFile(NotificationServiceMock.Object, importConfig, inputMapper, inputMapping))
                                 .Should()
                                 .NotThrow();
            }

            NotificationServiceMock.Verify(x => x.DisplayFeedback(It.IsAny<string>()), Times.Exactly(2));
        }
    }
}