using Capgemini.DataMigration.Core;
using Capgemini.Xrm.DataMigration.Config;
using Capgemini.Xrm.DataMigration.Core;
using Capgemini.Xrm.DataMigration.CrmStore.Config;
using Capgemini.Xrm.DataMigration.Repositories;
using System.Threading;

namespace Capgemini.Xrm.DataMigration.XrmToolBox.Services
{
    public interface ICrmGenericMigratorFactory
    {
        GenericCrmDataMigrator GetCrmDataMigrator(string dataFormat, ILogger logger, EntityRepository repo, CrmExporterConfig exportConfig, CancellationToken token, CrmSchemaConfiguration schema);
    }
}