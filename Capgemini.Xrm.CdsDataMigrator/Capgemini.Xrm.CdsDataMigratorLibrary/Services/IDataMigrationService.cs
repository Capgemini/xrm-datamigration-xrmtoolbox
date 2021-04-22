using Capgemini.Xrm.CdsDataMigratorLibrary.Models;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Services
{
    public interface IDataMigrationService
    {
        void ExportData(ExportSettings exportSettings);
    }
}