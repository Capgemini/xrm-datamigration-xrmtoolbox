using System;
using System.Collections.Generic;
using Capgemini.Xrm.DataMigration.XrmToolBox.Core;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Exceptions;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Extensions;
using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;

namespace Capgemini.Xrm.DataMigration.XrmToolBox.Services
{
    public class MetadataService : IMetadataService
    {
        private readonly static Dictionary<string, EntityMetadata> EntityMetadataCache = new Dictionary<string, EntityMetadata>();

        public List<EntityMetadata> RetrieveEntities(IOrganizationService orgService)
        {
            EntityMetadataCache.Clear();

            var entities = new List<EntityMetadata>();

            if (orgService == null)
            {
                return entities;
            }

            var request = new RetrieveAllEntitiesRequest
            {
                RetrieveAsIfPublished = true,
                EntityFilters = EntityFilters.Entity
            };

            var response = (RetrieveAllEntitiesResponse)orgService.Execute(request);

            if (response != null && response.EntityMetadata != null)
            {
                foreach (EntityMetadata emd in response.EntityMetadata)
                {
                    if (emd.DisplayName.UserLocalizedLabel != null)
                    {
                        entities.Add(emd);
                    }
                }
            }

            EntityMetadataCache.Clear();

            return entities;
        }

        public EntityMetadata RetrieveEntities(string logicalName, IOrganizationService orgService, IDataMigratorExceptionHelper dataMigratorExceptionHelper)
        {
            orgService.ThrowArgumentNullExceptionIfNull(nameof(orgService));

            try
            {
                lock (EntityMetadataCache)
                {
                    if (EntityMetadataCache.ContainsKey(logicalName))
                    {
                        return EntityMetadataCache[logicalName];
                    }

                    var request = new RetrieveEntityRequest
                    {
                        LogicalName = logicalName,
                        EntityFilters = EntityFilters.Attributes | EntityFilters.Relationships
                    };

                    var response = (RetrieveEntityResponse)orgService.Execute(request);

                    EntityMetadataCache.Add(logicalName, response.EntityMetadata);
                    return response.EntityMetadata;
                }
            }
            catch (Exception error)
            {
                string errorMessage = dataMigratorExceptionHelper.GetErrorMessage(error, false);
                throw new OrganizationalServiceException($"Error while retrieving entity: {errorMessage}");
            }
        }
    }
}