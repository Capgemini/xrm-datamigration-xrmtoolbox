using System;
using System.Threading;
using Capgemini.DataMigration.Core;
using Capgemini.Xrm.CdsDataMigratorLibrary.Services;
using Capgemini.Xrm.CdsDataMigratorLibrary.Tests.Unit.Mocks;
using Capgemini.Xrm.CdsDataMigratorLibrary.UserControls;
using Capgemini.Xrm.DataMigration.Core;
using Capgemini.Xrm.DataMigration.CrmStore.Config;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.UserControls.Tests
{
    [TestClass]
    public class ImportWizardTests
    {
        private Mock<ILogger> logger;
        private Mock<IEntityRepositoryService> entityRepositoryService;
        private CrmImportConfig importConfig;

        [TestInitialize]
        public void TestSetup()
        {
            logger = new Mock<ILogger>();
            entityRepositoryService = new Mock<IEntityRepositoryService>();

            importConfig = new CrmImportConfig()
            {
                IgnoreStatuses = false,
                IgnoreSystemFields = true,
                SaveBatchSize = 5000,
                JsonFolderPath = "TestData",
                FilePrefix = "ExtractedData"
            };
        }

        [TestMethod]
        public void ImportWizardInstantiated()
        {
            using (var systemUnderTest = new ImportWizard())
            {
                systemUnderTest.TargetConnectionString.Should().BeNull();
                systemUnderTest.OrganizationService.Should().BeNull();
            }
        }

        [TestMethod]
        public void HandleFileDialogOpenDialogResultIsCancel()
        {
            using (var systemUnderTest = new ImportWizard())
            {
                var dialogResult = System.Windows.Forms.DialogResult.Cancel;

                FluentActions.Invoking(() =>
                        systemUnderTest.HandleFileDialogOpen(dialogResult))
                       .Should()
                       .NotThrow();
            }
        }

        [TestMethod]
        public void HandleFileDialogOpenDialogResultOk()
        {
            using (var systemUnderTest = new ImportWizard())
            {
                var dialogResult = System.Windows.Forms.DialogResult.OK;

                FluentActions.Invoking(() =>
                        systemUnderTest.HandleFileDialogOpen(dialogResult))
                       .Should()
                       .NotThrow();
            }
        }

        [TestMethod]
        public void OnConnectionUpdated()
        {
            string connectedOrgFriendlyName = "test connection";

            using (var systemUnderTest = new ImportWizard())
            {
                FluentActions.Invoking(() =>
                        systemUnderTest.OnConnectionUpdated(connectedOrgFriendlyName))
                       .Should()
                       .NotThrow();
            }
        }

        [TestMethod]
        public void PerformImportActionHandleException()
        {
            var entityRepository = new Mock<IEntityRepository>();
            entityRepositoryService.Setup(x => x.InstantiateEntityRepository(false))
                                    .Returns(entityRepository.Object)
                                    .Verifiable();

            logger.Setup(x => x.LogInfo(It.IsAny<string>())).Throws<Exception>();
            logger.Setup(x => x.LogError(It.IsAny<string>()));

            using (var systemUnderTest = new ImportWizard())
            {
                using (var tokenSource = new CancellationTokenSource())
                {
                    string importSchemaFilePath = "TestData\\ImportConfig.json";
                    int maxThreads = 1;
                    bool jsonFormat = true;

                    FluentActions.Invoking(() =>
                            systemUnderTest.PerformImportAction(
                                                                importSchemaFilePath,
                                                                maxThreads,
                                                                jsonFormat,
                                                                logger.Object,
                                                                entityRepositoryService.Object,
                                                                importConfig,
                                                                tokenSource))
                           .Should()
                           .Throw<Exception>();
                }
            }

            logger.VerifyAll();
        }

        [TestMethod]
        public void PerformImportActionJsonFormat()
        {
            var entityRepository = new Mock<IEntityRepository>();
            entityRepositoryService.Setup(x => x.InstantiateEntityRepository(false))
                                    .Returns(entityRepository.Object)
                                    .Verifiable();

            using (var systemUnderTest = new ImportWizard())
            {
                using (var tokenSource = new CancellationTokenSource())
                {
                    string importSchemaFilePath = "TestData\\ImportConfig.json";
                    int maxThreads = 1;
                    bool jsonFormat = true;

                    FluentActions.Invoking(() =>
                            systemUnderTest.PerformImportAction(
                                                                importSchemaFilePath,
                                                                maxThreads,
                                                                jsonFormat,
                                                                logger.Object,
                                                                entityRepositoryService.Object,
                                                                importConfig,
                                                                tokenSource))
                           .Should()
                           .NotThrow();
                }
            }

            entityRepositoryService.VerifyAll();
        }

        [TestMethod]
        public void PerformImportActionJsonFormatMoreThanOneThread()
        {
            var entityRepository = new Mock<IEntityRepository>();
            entityRepositoryService.Setup(x => x.InstantiateEntityRepository(true))
                                    .Returns(entityRepository.Object)
                                    .Verifiable();

            using (var systemUnderTest = new ImportWizard())
            {
                using (var tokenSource = new CancellationTokenSource())
                {
                    string importSchemaFilePath = "TestData\\ImportConfig.json";
                    int maxThreads = 2;
                    bool jsonFormat = true;

                    FluentActions.Invoking(() =>
                            systemUnderTest.PerformImportAction(
                                                                importSchemaFilePath,
                                                                maxThreads,
                                                                jsonFormat,
                                                                logger.Object,
                                                                entityRepositoryService.Object,
                                                                importConfig,
                                                                tokenSource))
                           .Should()
                           .NotThrow();
                }
            }

            entityRepositoryService.VerifyAll();
        }

        [TestMethod]
        public void PerformImportActionCsvFormat()
        {
            var entityRepository = new Mock<IEntityRepository>();
            entityRepositoryService.Setup(x => x.InstantiateEntityRepository(false))
                                    .Returns(entityRepository.Object)
                                    .Verifiable();

            using (var systemUnderTest = new ImportWizard())
            {
                using (var tokenSource = new CancellationTokenSource())
                {
                    string importSchemaFilePath = "TestData\\BusinessUnitSchema.xml";
                    int maxThreads = 1;
                    bool jsonFormat = false;

                    FluentActions.Invoking(() =>
                                        systemUnderTest.PerformImportAction(
                                                                importSchemaFilePath,
                                                                maxThreads,
                                                                jsonFormat,
                                                                logger.Object,
                                                                entityRepositoryService.Object,
                                                                importConfig,
                                                                tokenSource))
                           .Should()
                           .NotThrow();
                }
            }

            entityRepositoryService.VerifyAll();
        }

        [TestMethod]
        public void PerformImportActionCsvFormatMoreThanOneThread()
        {
            var entityRepository = new Mock<IEntityRepository>();
            entityRepositoryService.Setup(x => x.InstantiateEntityRepository(false))
                                    .Returns(entityRepository.Object)
                                    .Verifiable();

            using (var systemUnderTest = new ImportWizard())
            {
                using (var tokenSource = new CancellationTokenSource())
                {
                    string importSchemaFilePath = "TestData\\ContactSchemaWithOwner.xml";
                    int maxThreads = 1;
                    bool jsonFormat = false;

                    FluentActions.Invoking(() =>
                                systemUnderTest.PerformImportAction(
                                                                    importSchemaFilePath,
                                                                    maxThreads,
                                                                    jsonFormat,
                                                                    logger.Object,
                                                                    entityRepositoryService.Object,
                                                                    importConfig,
                                                                    tokenSource))
                           .Should()
                           .NotThrow();
                }
            }

            entityRepositoryService.VerifyAll();
        }

        [TestMethod]
        public void WizardNavigation()
        {
            using (var selectedPage = new AeroWizard.WizardPage())
            {
                using (var pageContainer = new AeroWizard.WizardPageContainer())
                {
                    using (var folderPathValidationLabel = new System.Windows.Forms.Label())
                    {
                        using (var sourceDataLocationTextBox = new System.Windows.Forms.TextBox())
                        {
                            using (var systemUnderTest = new ImportWizard())
                            {
                                using (var tokenSource = new CancellationTokenSource())
                                {
                                    FluentActions.Invoking(() => systemUnderTest.WizardNavigation(folderPathValidationLabel, sourceDataLocationTextBox, selectedPage, pageContainer))
                                                   .Should()
                                                   .Throw<NullReferenceException>()
                                                   .WithMessage("Object reference not set to an instance of an object.");
                                }
                            }
                        }

                        folderPathValidationLabel.Visible.Should().BeTrue();
                    }
                }
            }
        }

        [TestMethod]
        public void WizardNavigationwizardPage2AndSourceDataLocationTextBoxHasText()
        {
            using (var selectedPage = new AeroWizard.WizardPage())
            {
                selectedPage.Name = "wizardPage2";
                using (var pageContainer = new AeroWizard.WizardPageContainer())
                {
                    using (var folderPathValidationLabel = new System.Windows.Forms.Label())
                    {
                        using (var sourceDataLocationTextBox = new System.Windows.Forms.TextBox())
                        {
                            sourceDataLocationTextBox.Text = "Sample text";
                            using (var systemUnderTest = new ImportWizard())
                            {
                                using (var tokenSource = new CancellationTokenSource())
                                {
                                    FluentActions.Invoking(() => systemUnderTest.WizardNavigation(folderPathValidationLabel, sourceDataLocationTextBox, selectedPage, pageContainer))
                                                   .Should()
                                                   .Throw<NullReferenceException>()
                                                   .WithMessage("Object reference not set to an instance of an object.");
                                }
                            }
                        }

                        folderPathValidationLabel.Visible.Should().BeFalse();
                    }
                }
            }
        }

        [TestMethod]
        public void WizardNavigationwizardPage2AndSourceDataLocationTextBoxHasNoText()
        {
            using (var selectedPage = new AeroWizard.WizardPage())
            {
                selectedPage.Name = "wizardPage2";
                using (var pageContainer = new AeroWizard.WizardPageContainer())
                {
                    using (var folderPathValidationLabel = new System.Windows.Forms.Label())
                    {
                        using (var sourceDataLocationTextBox = new System.Windows.Forms.TextBox())
                        {
                            using (var systemUnderTest = new ImportWizard())
                            {
                                using (var tokenSource = new CancellationTokenSource())
                                {
                                    FluentActions.Invoking(() => systemUnderTest.WizardNavigation(folderPathValidationLabel, sourceDataLocationTextBox, selectedPage, pageContainer))
                                                   .Should()
                                                   .NotThrow();
                                }
                            }
                        }

                        folderPathValidationLabel.Visible.Should().BeTrue();
                    }
                }
            }
        }

        [TestMethod]
        public void WizardButtonsOnNavigateToNextPage()
        {
            var sender = new WizardButtons();
            var pageContainer = new AeroWizard.WizardPageContainer();

            pageContainer.Pages.Add(new AeroWizard.WizardPage());
            pageContainer.Pages.Add(new AeroWizard.WizardPage() { Name = "wizardPage2" });
            sender.PageContainer = pageContainer;

            using (var systemUnderTest = new ImportWizard())
            {
                FluentActions.Invoking(() => systemUnderTest.WizardButtonsOnNavigateToNextPage(sender, new EventArgs()))
                            .Should()
                            .NotThrow();
            }
        }

        [TestMethod]
        public void TabImportConfigFileTextChanged()
        {
            using (var systemUnderTest = new ImportWizard())
            {
                FluentActions.Invoking(() => systemUnderTest.TabImportConfigFileTextChanged(null, new EventArgs()))
                            .Should()
                            .NotThrow();
            }
        }

        [TestMethod]
        public void TbImportSchemeTextChanged()
        {
            using (var systemUnderTest = new ImportWizard())
            {
                FluentActions.Invoking(() => systemUnderTest.TbImportSchemeTextChanged(null, new EventArgs()))
                            .Should()
                            .NotThrow();
            }
        }

        [TestMethod]
        public void Button2Click()
        {
            using (var systemUnderTest = new ImportWizard())
            {
                FluentActions.Invoking(() => systemUnderTest.Button2Click(null, new EventArgs()))
                            .Should()
                            .NotThrow();
            }
        }

        [TestMethod]
        public void RadioButton1CheckedChanged()
        {
            using (var systemUnderTest = new ImportWizard())
            {
                FluentActions.Invoking(() => systemUnderTest.RadioButton1CheckedChanged(null, new EventArgs()))
                            .Should()
                            .NotThrow();
            }
        }

        [TestMethod]
        public void RadioButtonCheckedChanged()
        {
            using (var systemUnderTest = new ImportWizard())
            {
                FluentActions.Invoking(() => systemUnderTest.RadioButtonCheckedChanged(null, new EventArgs()))
                            .Should()
                            .NotThrow();
            }
        }

        [TestMethod]
        public void InvokeWizardButtonsOnCancel()
        {
            using (var systemUnderTest = new MockupForImportWizard())
            {
                FluentActions.Invoking(() => systemUnderTest.InvokeWizardButtonsOnCancel(new EventArgs()))
                            .Should()
                            .NotThrow();
            }
        }

        [TestMethod]
        public void InvokeComboBoxLogLevelSelectedIndexChanged()
        {
            using (var systemUnderTest = new MockupForImportWizard())
            {
                FluentActions.Invoking(() => systemUnderTest.InvokeComboBoxLogLevelSelectedIndexChanged(new EventArgs()))
                            .Should()
                            .NotThrow();
            }
        }

        [TestMethod]
        public void InvokeTabSourceDataLocationTextChanged()
        {
            using (var systemUnderTest = new MockupForImportWizard())
            {
                FluentActions.Invoking(() => systemUnderTest.InvokeTabSourceDataLocationTextChanged(new EventArgs()))
                            .Should()
                            .NotThrow();
            }
        }
    }
}