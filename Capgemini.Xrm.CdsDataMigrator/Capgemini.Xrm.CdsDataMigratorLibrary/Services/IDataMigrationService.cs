using Capgemini.Xrm.CdsDataMigratorLibrary.Enums;
using Capgemini.Xrm.DataMigration.Config;
using Capgemini.Xrm.DataMigration.CrmStore.Config;
using Microsoft.Xrm.Sdk;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Services
{
    public interface IDataMigrationService
    {
        void ExportData(IOrganizationService service, DataFormat format, CrmExporterConfig config);

        void ImportData(IOrganizationService service, DataFormat format, CrmSchemaConfiguration schema, CrmImportConfig config, decimal maxThreads, IEntityRepositoryService entityRepositoryService);
    }
}