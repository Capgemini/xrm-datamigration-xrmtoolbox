using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using McTools.Xrm.Connection;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core
{
    class MetadataHelper
    {
        private static Dictionary<string, EntityMetadata> EntityMetadataCache = new Dictionary<string, EntityMetadata>();

        public static List<EntityMetadata> RetrieveEntities(IOrganizationService oService)
        {
            EntityMetadataCache.Clear();

            List<EntityMetadata> entities = new List<EntityMetadata>();

            RetrieveAllEntitiesRequest request = new RetrieveAllEntitiesRequest
            {
                RetrieveAsIfPublished = true,
                EntityFilters = EntityFilters.Entity
            };

            RetrieveAllEntitiesResponse response = (RetrieveAllEntitiesResponse)oService.Execute(request);

            foreach (EntityMetadata emd in response.EntityMetadata)
            {
                // Get all entities
                //if (emd.DisplayName.UserLocalizedLabel != null && (emd.IsCustomizable.Value || emd.IsManaged.Value == false))
                if (emd.DisplayName.UserLocalizedLabel != null)
                {
                    entities.Add(emd);
                }
            }

            EntityMetadataCache.Clear();

            return entities;
        }

        public static EntityMetadata RetrieveEntities(string logicalName, IOrganizationService oService)
        {
            try
            {
                lock (EntityMetadataCache)
                {
                    if (EntityMetadataCache.ContainsKey(logicalName))
                        return EntityMetadataCache[logicalName];

                    RetrieveEntityRequest request = new RetrieveEntityRequest
                    {
                        LogicalName = logicalName,
                        EntityFilters = EntityFilters.Attributes | EntityFilters.Relationships
                    };

                    RetrieveEntityResponse response = (RetrieveEntityResponse)oService.Execute(request);

                    EntityMetadataCache.Add(logicalName, response.EntityMetadata);
                    return response.EntityMetadata;
                }
            }
            catch (Exception error)
            {
                string errorMessage = CrmExceptionHelper.GetErrorMessage(error, false);
                throw new Exception("Error while retrieving entity: " + errorMessage);
            }
        }
    }
}
