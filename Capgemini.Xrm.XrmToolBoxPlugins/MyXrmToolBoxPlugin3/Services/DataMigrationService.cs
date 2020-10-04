using Capgemini.DataMigration.Core;
using Capgemini.DataMigration.Resiliency.Polly;
using Capgemini.Xrm.DataMigration.Config;
using Capgemini.Xrm.DataMigration.CrmStore.Config;
using Capgemini.Xrm.DataMigration.Engine;
using Capgemini.Xrm.DataMigration.Repositories;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Models;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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