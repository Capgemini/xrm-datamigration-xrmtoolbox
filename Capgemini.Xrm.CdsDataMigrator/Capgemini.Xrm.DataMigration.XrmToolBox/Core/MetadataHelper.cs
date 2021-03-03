using System;
using System.Collections.Generic;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Exceptions;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Extensions;
using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core
{
    public static class MetadataHelper
    {
        private static Dictionary<string, EntityMetadata> entityMetadataCache = new Dictionary<string, EntityMetadata>();

        public static List<EntityMetadata> RetrieveEntities(IOrganizationService oService)
        {
            entityMetadataCache.Clear();

            List<EntityMetadata> entities = new List<EntityMetadata>();

            if (oService == null)
            {
                return entities;
            }

            var request = new RetrieveAllEntitiesRequest
            {
                RetrieveAsIfPublished = true,
                EntityFilters = EntityFilters.Entity
            };

            var response = (RetrieveAllEntitiesResponse)oService.Execute(request);

            if (response.EntityMetadata != null)
            {
                foreach (EntityMetadata emd in response.EntityMetadata)
                {
                    if (emd.DisplayName.UserLocalizedLabel != null)
                    {
                        entities.Add(emd);
                    }
                }
            }

            entityMetadataCache.Clear();

            return entities;
        }

        public static EntityMetadata RetrieveEntities(string logicalName, IOrganizationService oService)
        {
            oService.ThrowArgumentNullExceptionIfNull(nameof(oService));

            try
            {
                lock (entityMetadataCache)
                {
                    if (entityMetadataCache.ContainsKey(logicalName))
                    {
                        return entityMetadataCache[logicalName];
                    }

                    var request = new RetrieveEntityRequest
                    {
                        LogicalName = logicalName,
                        EntityFilters = EntityFilters.Attributes | EntityFilters.Relationships 
                    };

                    var response = (RetrieveEntityResponse)oService.Execute(request);

                    entityMetadataCache.Add(logicalName, response.EntityMetadata);
                    return response.EntityMetadata;
                }
            }
            catch (Exception error)
            {
                string errorMessage = CrmExceptionHelper.GetErrorMessage(error, false);
                throw new OrganizationalServiceException($"Error while retrieving entity: {errorMessage}");
            }
        }
    }
}