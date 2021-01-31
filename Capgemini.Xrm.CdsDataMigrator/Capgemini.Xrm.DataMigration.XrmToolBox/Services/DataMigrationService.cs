using Capgemini.DataMigration.Core;
using Capgemini.DataMigration.Resiliency.Polly;
using Capgemini.Xrm.DataMigration.Config;
using Capgemini.Xrm.DataMigration.Core;
using Capgemini.Xrm.DataMigration.CrmStore.Config;
using Capgemini.Xrm.DataMigration.Repositories;
using Capgemini.Xrm.DataMigration.XrmToolBox.Services;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Models;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Services
{
    public class DataMigrationService
    {
        private readonly ILogger logger;
        private readonly ICrmGenericMigratorFactory migratorFactory;
        private CrmExporterConfig exportConfig;

        public DataMigrationService(ILogger logger) : this(logger, new CrmGenericMigratorFactory())
        {

        }

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
                    CrmMigrationToolSchemaPaths = new List<string> { exportSettings.SchemaPath },
                    BatchSize = Convert.ToInt32(exportSettings.BatchSize),
                    PageSize = Convert.ToInt32(exportSettings.BatchSize),
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
            }

            CrmSchemaConfiguration schema = CrmSchemaConfiguration.ReadFromFile(exportSettings.SchemaPath);

            GenericCrmDataMigrator migrator = migratorFactory.GetCrmDataMigrator(exportSettings.DataFormat, logger, repo, exportConfig, tokenSource.Token, schema);
            migrator.MigrateData();

        }

        private void InjectAdditionalValuesIntoTheExportConfig(CrmExporterConfig config, ExportSettings exportSettings)
        {
            config.CrmMigrationToolSchemaPaths = new List<string>() { exportSettings.SchemaPath };

            // TODO need add code for the minimize if JSON stuff
            config.JsonFolderPath = exportSettings.SavePath;
            if (exportConfig.CrmMigrationToolSchemaFilters != null && exportConfig.CrmMigrationToolSchemaFilters.Count > 0)
            {
                config.OnlyActiveRecords = false;
            }
            else
            {
                config.OnlyActiveRecords = !exportSettings.ExportInactiveRecords;
            }
            config.BatchSize = exportSettings.BatchSize > 0 ? exportSettings.BatchSize : 1;
            config.PageSize = config.BatchSize;
        }
    }
}