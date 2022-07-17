using Capgemini.Xrm.CdsDataMigratorLibrary.Exceptions;
using Capgemini.Xrm.CdsDataMigratorLibrary.Models;
using Capgemini.Xrm.CdsDataMigratorLibrary.Services;
using Capgemini.Xrm.DataMigration.CrmStore.Config;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Presenters
{
    public class SchemaGeneratorPresenter
    {
        private readonly ISchemaGeneratorView view;
        private readonly IOrganizationService organizationService;
        private readonly IMetadataService metadataService;
        private readonly INotificationService notificationService;
        private readonly IExceptionService exceptionService; 

        public SchemaGeneratorPresenter(ISchemaGeneratorView view, IOrganizationService organizationService, IMetadataService metadataService, INotificationService notificationService, IExceptionService exceptionService)
        {
            this.view = view;
            this.organizationService = organizationService;
            this.metadataService = metadataService;
            this.notificationService = notificationService;
            this.exceptionService = exceptionService;
            if (this.view != null)
            {
                this.view.RetrieveEntities += RetrieveEntities;
            }
        }

        private void RetrieveEntities(object sender, System.EventArgs e)
        {
            var inputCachedMetadata = new List<EntityMetadata>();

            if (inputCachedMetadata.Count == 0 )//|| isNewConnection) ///TODO: address new connection
            { 
                var serviceParameters = new ServiceParameters(organizationService, metadataService, notificationService, exceptionService); 
                var sourceList = serviceParameters.MetadataService.RetrieveEntities(serviceParameters.OrganizationService);

                if (!view.ShowSystemAttributes)
                {
                    sourceList = sourceList.Where(p => !p.IsLogicalEntity.Value && !p.IsIntersect.Value).ToList();
                }
                
                if (sourceList != null)
                { 
                    inputCachedMetadata.AddRange(sourceList.OrderBy(p => p.IsLogicalEntity.Value).ThenBy(p => p.IsIntersect.Value).ThenByDescending(p => p.IsCustomEntity.Value).ThenBy(p => p.LogicalName).ToList());
                }
            }

            view.EntityMetadataList = inputCachedMetadata;
        }
    }
}