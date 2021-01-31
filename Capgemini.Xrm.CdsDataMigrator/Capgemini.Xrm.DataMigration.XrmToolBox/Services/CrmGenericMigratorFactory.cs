using Capgemini.DataMigration.Core;
using Capgemini.Xrm.DataMigration.Config;
using Capgemini.Xrm.DataMigration.Core;
using Capgemini.Xrm.DataMigration.CrmStore.Config;
using Capgemini.Xrm.DataMigration.Engine;
using System;
using System.Threading;

namespace Capgemini.Xrm.DataMigration.XrmToolBox.Services
{
    public class CrmGenericMigratorFactory : ICrmGenericMigratorFactory
    {
        public GenericCrmDataMigrator GetCrmDataMigrator(string dataFormat, ILogger logger, IEntityRepository repo, CrmExporterConfig exportConfig, CancellationToken token, CrmSchemaConfiguration schema)
        {
            // TODO: refactor to enum
            switch (dataFormat)
            {
                case "json":
                    return new CrmFileDataExporter(logger, repo, exportConfig, token);

                case "csv":
                    return new CrmFileDataExporterCsv(logger, repo, exportConfig, schema, token);

                default:
                    throw new NotSupportedException($"Data format: '{dataFormat}' is not supported.");
            }
        }
    }
}