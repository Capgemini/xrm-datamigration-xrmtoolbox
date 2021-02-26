using Capgemini.DataMigration.Core;
using Capgemini.DataMigration.Resiliency.Polly;
using Capgemini.Xrm.DataMigration.Config;
using Capgemini.Xrm.DataMigration.CrmStore.Config;
using Capgemini.Xrm.DataMigration.Engine;
using Capgemini.Xrm.DataMigration.Repositories;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Models;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Services
{
    public class DataMigrationService
    {
        private ILogger logger;
        private CrmExporterConfig exportConfig;

        public DataMigrationService(ILogger logger)
        {
            this.logger = logger;
        }

        public void ExportData(ExportSettings exportSettings)
        {
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
                    OneEntityPerBatch = true,
                    FilePrefix = "ExtractedData",
                    SeperateFilesPerEntity = true,
                    FetchXMLFolderPath = string.Empty
                };

                exportConfig.CrmMigrationToolSchemaPaths.Clear();
                exportConfig.CrmMigrationToolSchemaPaths.Add(exportSettings.SchemaPath);
            }

            CrmSchemaConfiguration schema = CrmSchemaConfiguration.ReadFromFile(exportSettings.SchemaPath);

            if (exportSettings.DataFormat == "json")
            {
                CrmFileDataExporter exporter = new CrmFileDataExporter(logger, repo, exportConfig, tokenSource.Token);
                exporter.MigrateData();
            }
            else
            {
                CrmFileDataExporterCsv exporter = new CrmFileDataExporterCsv(logger, repo, exportConfig, schema, tokenSource.Token);
                exporter.MigrateData();
            }
        }

        private void InjectAdditionalValuesIntoTheExportConfig(CrmExporterConfig config, ExportSettings exportSettings)
        {
            config.CrmMigrationToolSchemaPaths.Clear();
            config.CrmMigrationToolSchemaPaths.Add(exportSettings.SchemaPath);

            // TODO need add code for the minimize if JSON stuff
            config.JsonFolderPath = exportSettings.SavePath;
            config.OnlyActiveRecords = !exportSettings.ExportInactiveRecords;
            config.BatchSize = exportSettings.BatchSize;
        }
    }
}