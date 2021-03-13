using System.Collections.Generic;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;

namespace Capgemini.Xrm.DataMigration.XrmToolBox.Services
{
    public interface IMetadataService
    {
        List<EntityMetadata> RetrieveEntities(IOrganizationService orgService);

        EntityMetadata RetrieveEntities(string logicalName, IOrganizationService orgService);
    }
}