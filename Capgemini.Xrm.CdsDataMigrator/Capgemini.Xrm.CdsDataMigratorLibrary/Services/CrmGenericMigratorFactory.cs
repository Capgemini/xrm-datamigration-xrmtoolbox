using Capgemini.DataMigration.Core;
using Capgemini.Xrm.CdsDataMigratorLibrary.Enums;
using Capgemini.Xrm.DataMigration.Config;
using Capgemini.Xrm.DataMigration.Core;
using Capgemini.Xrm.DataMigration.CrmStore.Config;
using Capgemini.Xrm.DataMigration.Engine;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Services
{
    public class CrmGenericMigratorFactory : ICrmGenericMigratorFactory
    {
        public IGenericCrmDataMigrator GetCrmExportDataMigrator(DataFormat dataFormat, ILogger logger, IEntityRepository repo, CrmExporterConfig exportConfig, CancellationToken token, CrmSchemaConfiguration schema)
        {
            switch (dataFormat)
            {
                case DataFormat.Json:
                    return new CrmFileDataExporter(logger, repo, exportConfig, token);

                case DataFormat.Csv:
                    return new CrmFileDataExporterCsv(logger, repo, exportConfig, schema, token);

                default:
                    throw new NotSupportedException($"Data format: '{dataFormat}' is not supported.");
            }
        }

        public IGenericCrmDataMigrator GetCrmImportDataMigrator(DataFormat dataFormat, ILogger logger, IEntityRepository repo, CrmImportConfig importConfig, CancellationToken token, CrmSchemaConfiguration schema)
        {
            switch (dataFormat)
            {
                case DataFormat.Json:
                    return new CrmFileDataImporter(logger, repo, importConfig, token);

                case DataFormat.Csv:
                    return new CrmFileDataImporterCsv(logger, repo, importConfig, schema, token);

                default:
                    throw new NotSupportedException($"Data format: '{dataFormat}' is not supported.");
            }
        }

        public IGenericCrmDataMigrator GetCrmImportDataMigrator(DataFormat dataFormat, ILogger logger, List<IEntityRepository> repos, CrmImportConfig importConfig, CancellationToken token, CrmSchemaConfiguration schema)
        {
            switch (dataFormat)
            {
                case DataFormat.Json:
                    return new CrmFileDataImporter(logger, repos, importConfig, token);

                case DataFormat.Csv:
                    return new CrmFileDataImporterCsv(logger, repos, importConfig, schema, token);

                default:
                    throw new NotSupportedException($"Data format: '{dataFormat}' is not supported.");
            }
        }
    }
}