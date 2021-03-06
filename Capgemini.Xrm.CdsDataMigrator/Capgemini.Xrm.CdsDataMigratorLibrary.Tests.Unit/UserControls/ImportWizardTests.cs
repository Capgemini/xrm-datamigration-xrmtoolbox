﻿using System;
using System.Threading;
using Capgemini.DataMigration.Core;
using Capgemini.Xrm.CdsDataMigratorLibrary.Services;
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
    }
}