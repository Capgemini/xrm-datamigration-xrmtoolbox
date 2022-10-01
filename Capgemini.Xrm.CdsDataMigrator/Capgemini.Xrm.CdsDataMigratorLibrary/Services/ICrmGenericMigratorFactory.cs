using Capgemini.DataMigration.Core;
using Capgemini.Xrm.CdsDataMigratorLibrary.Enums;
using Capgemini.Xrm.DataMigration.Config;
using Capgemini.Xrm.DataMigration.Core;
using Capgemini.Xrm.DataMigration.CrmStore.Config;
using System.Collections.Generic;
using System.Threading;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Services
{
    public interface ICrmGenericMigratorFactory
    {
        IGenericCrmDataMigrator GetCrmDataMigrator(DataFormat dataFormat, ILogger logger, IEntityRepository repo, CrmExporterConfig exportConfig, CancellationToken token, CrmSchemaConfiguration schema);
        IGenericCrmDataMigrator GetCrmImportDataMigrator(DataFormat dataFormat, ILogger logger, IEntityRepository repo, CrmImportConfig importConfig, CancellationToken token, CrmSchemaConfiguration schema);
        IGenericCrmDataMigrator GetCrmImportDataMigrator(DataFormat dataFormat, ILogger logger, List<IEntityRepository> repos, CrmImportConfig importConfig, CancellationToken token, CrmSchemaConfiguration schema);
    }
}