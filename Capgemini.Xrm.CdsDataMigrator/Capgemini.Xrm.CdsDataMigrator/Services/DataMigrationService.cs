using Capgemini.DataMigration.Core;
using Capgemini.DataMigration.Resiliency.Polly;
using Capgemini.Xrm.DataMigration.Config;
using Capgemini.Xrm.DataMigration.CrmStore.Config;
using Capgemini.Xrm.DataMigration.Engine;
using Capgemini.Xrm.DataMigration.Repositories;
using Capgemini.Xrm.DataMigration.XrmToolBox.Enums;
using Capgemini.Xrm.DataMigration.XrmToolBox.Services;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Models;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Services
{
    public class DataMigrationService : IDataMigrationService
    {
        private readonly ILogger logger;
        private readonly ICrmGenericMigratorFactory migratorFactory;
        private CrmExporterConfig exportConfig;

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

            var tokenSource = new CancellationTokenSource();

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

        private void InjectAdditionalValuesIntoTheExportConfig(CrmExporterConfig config, ExportSettings exportSettings)
        {
            config.CrmMigrationToolSchemaPaths.Clear();
            config.CrmMigrationToolSchemaPaths.Add(exportSettings.SchemaPath);

            config.JsonFolderPath = exportSettings.SavePath;
            config.OnlyActiveRecords = !exportSettings.ExportInactiveRecords;
            config.BatchSize = exportSettings.BatchSize;
        }
    }
}