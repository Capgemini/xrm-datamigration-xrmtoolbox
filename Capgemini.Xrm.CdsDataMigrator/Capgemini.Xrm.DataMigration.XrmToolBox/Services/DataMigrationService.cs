using Capgemini.DataMigration.Core;
using Capgemini.DataMigration.Resiliency.Polly;
using Capgemini.Xrm.DataMigration.Config;
using Capgemini.Xrm.DataMigration.CrmStore.Config;
using Capgemini.Xrm.DataMigration.Repositories;
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

            EntityRepository repo = new EntityRepository(exportSettings.EnvironmentConnection, new ServiceRetryExecutor());

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
                    CrmMigrationToolSchemaFilters = new Dictionary<string, string>(),
                    OneEntityPerBatch = false,
                    LookupMapping = new Dictionary<string, Dictionary<string, List<string>>>(),
                    FilePrefix = "0.1",
                    ExcludedFields = new List<string> { },
                    SeperateFilesPerEntity = true,
                    FetchXMLFolderPath = string.Empty
                };

                exportConfig.CrmMigrationToolSchemaPaths.Clear();
                exportConfig.CrmMigrationToolSchemaPaths.Add(exportSettings.SchemaPath);
            }

            var schema = CrmSchemaConfiguration.ReadFromFile(exportSettings.SchemaPath);

            if (exportSettings.DataFormat == "json")
            {
                CrmFileDataExporter exporter = new CrmFileDataExporter(logger, repo, exportConfig, tokenSource.Token);
                exporter.MigrateData();
            }
            else
            {
                CrmFileDataExporterCsv exporter = new CrmFileDataExporterCsv(logger, repo, exportConfig, tokenSource.Token, schema);
                exporter.MigrateData();
            }
        }

        private void InjectAdditionalValuesIntoTheExportConfig(CrmExporterConfig config, ExportSettings exportSettings)
        {
            config.CrmMigrationToolSchemaPaths = new List<string>() { exportSettings.SchemaPath };
            // TODO need add code for the minimize if JSON stuff
            config.JsonFolderPath = exportSettings.SavePath;
            config.OnlyActiveRecords = !exportSettings.ExportInactiveRecords;
            config.BatchSize = exportSettings.BatchSize;
        }
    }
}