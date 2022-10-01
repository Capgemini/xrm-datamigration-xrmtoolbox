using Capgemini.DataMigration.Core;
using Capgemini.DataMigration.Resiliency.Polly;
using Capgemini.Xrm.CdsDataMigratorLibrary.Enums;
using Capgemini.Xrm.CdsDataMigratorLibrary.Models;
using Capgemini.Xrm.DataMigration.Config;
using Capgemini.Xrm.DataMigration.Core;
using Capgemini.Xrm.DataMigration.CrmStore.Config;
using Capgemini.Xrm.DataMigration.Engine;
using Capgemini.Xrm.DataMigration.Repositories;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Services
{
    public class DataMigrationService : IDataMigrationService
    {
        private readonly ILogger logger;
        private readonly ICrmGenericMigratorFactory migratorFactory;
        private CrmExporterConfig exportConfig;
        private CrmImportConfig importConfig;
        private CancellationTokenSource tokenSource;

        public DataMigrationService(ILogger logger, ICrmGenericMigratorFactory migratorFactory)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.migratorFactory = migratorFactory ?? throw new ArgumentNullException(nameof(migratorFactory));
        }

        public void ExportData(ExportSettings exportSettings)
        {
            if (exportSettings is null)
            {
                throw new ArgumentNullException(nameof(exportSettings));
            }

            tokenSource = new CancellationTokenSource();

            var repo = new EntityRepository(exportSettings.EnvironmentConnection, new ServiceRetryExecutor());

            if (!string.IsNullOrEmpty(exportSettings.ExportConfigPath))
            {
                exportConfig = CrmExporterConfig.GetConfiguration(exportSettings.ExportConfigPath);
                InjectAdditionalValuesIntoTheExportConfig(exportConfig, exportSettings);
            }
            else
            {
                exportConfig = new CrmExporterConfig
                {
                    BatchSize = Convert.ToInt32(exportSettings.BatchSize),
                    PageSize = 5000,
                    TopCount = Convert.ToInt32(1000000),
                    OnlyActiveRecords = !exportSettings.ExportInactiveRecords,
                    JsonFolderPath = exportSettings.SavePath,
                    OneEntityPerBatch = true,
                    FilePrefix = "ExtractedData",
                    SeperateFilesPerEntity = true,
                    FetchXMLFolderPath = string.Empty
                };

                exportConfig.CrmMigrationToolSchemaPaths.Clear();
                exportConfig.CrmMigrationToolSchemaPaths.Add(exportSettings.SchemaPath);
            }

            var schema = CrmSchemaConfiguration.ReadFromFile(exportSettings.SchemaPath);

            var exporter = migratorFactory.GetCrmDataMigrator(exportSettings.DataFormat, logger, repo, exportConfig, tokenSource.Token, schema);

            exporter.MigrateData();
        }

        public void ExportData(IOrganizationService service, DataFormat format, CrmExporterConfig config)
        {
            try
            {
                tokenSource = new CancellationTokenSource();

                var repo = new EntityRepository(service, new ServiceRetryExecutor());

                var schema = CrmSchemaConfiguration.ReadFromFile(config.CrmMigrationToolSchemaPaths.FirstOrDefault());

                var exporter = migratorFactory.GetCrmDataMigrator(format, logger, repo, config, tokenSource.Token, schema);

                exporter.MigrateData();
            }
            catch (Exception error)
            {
                logger.LogError(error.Message);
                throw;
            }
        }

        public void CancelDataExport()
        {
            tokenSource?.Cancel();
        }

        private void InjectAdditionalValuesIntoTheExportConfig(CrmExporterConfig config, ExportSettings exportSettings)
        {
            config.CrmMigrationToolSchemaPaths.Clear();
            config.CrmMigrationToolSchemaPaths.Add(exportSettings.SchemaPath);

            config.JsonFolderPath = exportSettings.SavePath;
            config.OnlyActiveRecords = !exportSettings.ExportInactiveRecords;
            config.BatchSize = exportSettings.BatchSize;
        }

        public void ImportData(IOrganizationService service, DataFormat format, CrmSchemaConfiguration schema, CrmImportConfig config, decimal maxThreads, IEntityRepositoryService entityRepositoryService)
        {
            tokenSource = new CancellationTokenSource();

            if (maxThreads > 1)
            {
                logger.LogInfo($"Starting MultiThreaded Processing, using {maxThreads} threads");
                var repos = new List<IEntityRepository>();
                decimal threadCount = maxThreads;

                while (threadCount > 0)
                {
                    threadCount--;
                    repos.Add(entityRepositoryService.InstantiateEntityRepository(true));
                }

                //var fileExporter = new CrmFileDataImporter(logger, repos, config, tokenSource.Token);
                //fileExporter.MigrateData();
                var multiThreadimporter = migratorFactory.GetCrmImportDataMigrator(format, logger, repos, config, tokenSource.Token, schema);
                multiThreadimporter.MigrateData();
                return;
            }

            var repo = new EntityRepository(service, new ServiceRetryExecutor());

            var singleThreadimporter = migratorFactory.GetCrmImportDataMigrator(format, logger, repo, config, tokenSource.Token, schema);

            singleThreadimporter.MigrateData();
            return;
        }

        public void CancelDataImport()
        {
            tokenSource?.Cancel();
        }
    }
}