using Capgemini.DataMigration.Core;
using Capgemini.DataMigration.Resiliency.Polly;
using Capgemini.Xrm.CdsDataMigratorLibrary.Enums;
using Capgemini.Xrm.DataMigration.Config;
using Capgemini.Xrm.DataMigration.Core;
using Capgemini.Xrm.DataMigration.CrmStore.Config;
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
        private CancellationTokenSource tokenSource;

        public DataMigrationService(ILogger logger, ICrmGenericMigratorFactory migratorFactory)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.migratorFactory = migratorFactory ?? throw new ArgumentNullException(nameof(migratorFactory));
        }

        public void ExportData(IOrganizationService service, DataFormat format, CrmExporterConfig config)
        {
            try
            {
                tokenSource = new CancellationTokenSource();

                var repo = new EntityRepository(service, new ServiceRetryExecutor());

                var schema = CrmSchemaConfiguration.ReadFromFile(config.CrmMigrationToolSchemaPaths.FirstOrDefault());

                var exporter = migratorFactory.GetCrmExportDataMigrator(format, logger, repo, config, tokenSource.Token, schema);

                exporter.MigrateData();
            }
            catch (Exception error)
            {
                logger.LogError(error.Message);
                throw;
            }
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
                
                var multiThreadimporter = migratorFactory.GetCrmImportDataMigrator(format, logger, repos, config, tokenSource.Token, schema);
                multiThreadimporter.MigrateData();
                return;
            }

            var repo = new EntityRepository(service, new ServiceRetryExecutor());

            var singleThreadimporter = migratorFactory.GetCrmImportDataMigrator(format, logger, repo, config, tokenSource.Token, schema);

            singleThreadimporter.MigrateData();
        }
    }
}