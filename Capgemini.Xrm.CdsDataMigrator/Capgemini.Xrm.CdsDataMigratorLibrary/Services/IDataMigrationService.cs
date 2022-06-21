using Capgemini.Xrm.CdsDataMigratorLibrary.Enums;
using Capgemini.Xrm.CdsDataMigratorLibrary.Models;
using Capgemini.Xrm.DataMigration.CrmStore.Config;
using Microsoft.Xrm.Sdk;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Services
{
    public interface IDataMigrationService
    {
        void ExportData(ExportSettings exportSettings);
        void ExportData(IOrganizationService service, DataFormat format, CrmExporterConfig config);

        void CancelDataExport();
    }
}