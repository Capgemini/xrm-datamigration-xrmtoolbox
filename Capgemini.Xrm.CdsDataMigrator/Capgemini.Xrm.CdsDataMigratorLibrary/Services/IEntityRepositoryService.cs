using Capgemini.Xrm.DataMigration.Core;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Services
{
    public interface IEntityRepositoryService
    {
        IEntityRepository InstantiateEntityRepository(bool useCloneConnection);
    }
}