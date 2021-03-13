using Capgemini.Xrm.DataMigration.Core;

namespace Capgemini.Xrm.DataMigration.XrmToolBox.Services
{
    public interface IEntityRepositoryService
    {
        IEntityRepository InstantiateEntityRepository(bool useCloneConnection);
    }
}