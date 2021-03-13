using System.Threading;
using Capgemini.DataMigration.Core;
using Capgemini.Xrm.DataMigration.Repositories;
using Capgemini.Xrm.DataMigration.Core;
using Capgemini.Xrm.DataMigration.CrmStore.Config;
using Capgemini.Xrm.DataMigration.XrmToolBox.Services;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.UserControls;
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
        public void PerformImportActionJsonFormat()
        {
            var entityRepository = new Mock<IEntityRepository>();
            entityRepositoryService.Setup(x => x.InstantiateEntityRepository(false))
                                    .Returns(entityRepository.Object)
                                    .Verifiable();

            using (var systemUnderTest = new importWizard())
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

            using (var systemUnderTest = new importWizard())
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

            using (var systemUnderTest = new importWizard())
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

            using (var systemUnderTest = new importWizard())
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
    }
}