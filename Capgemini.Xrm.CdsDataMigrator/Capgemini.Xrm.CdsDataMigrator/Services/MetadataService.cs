﻿using System;
using System.Collections.Generic;
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
        private static Dictionary<string, EntityMetadata> entityMetadataCache = new Dictionary<string, EntityMetadata>();

        public List<EntityMetadata> RetrieveEntities(IOrganizationService orgService)
        {
            entityMetadataCache.Clear();

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

        public EntityMetadata RetrieveEntities(string logicalName, IOrganizationService orgService)
        {
            orgService.ThrowArgumentNullExceptionIfNull(nameof(orgService));

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

                    var response = (RetrieveEntityResponse)orgService.Execute(request);

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