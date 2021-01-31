using Capgemini.DataMigration.Core;
using Capgemini.Xrm.DataMigration.Config;
using Capgemini.Xrm.DataMigration.Core;
using Capgemini.Xrm.DataMigration.CrmStore.Config;
using Capgemini.Xrm.DataMigration.Repositories;
using Capgemini.Xrm.DataMigration.XrmToolBox.Enums;
using System.Threading;

namespace Capgemini.Xrm.DataMigration.XrmToolBox.Services
{
    public interface ICrmGenericMigratorFactory
    {
        GenericCrmDataMigrator GetCrmDataMigrator(DataFormat dataFormat, ILogger logger, IEntityRepository repo, CrmExporterConfig exportConfig, CancellationToken token, CrmSchemaConfiguration schema);
    }
}