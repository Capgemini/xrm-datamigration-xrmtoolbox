using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Models;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Services
{
    public interface IDataMigrationService
    {
        void ExportData(ExportSettings exportSettings);
    }
}