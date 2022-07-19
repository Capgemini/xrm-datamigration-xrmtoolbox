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
                this.view.RetrieveEntities += HandleRetrieveEntitiesRequest;
                this.view.ShowSystemEntitiesChanged += ShowSystemEntitiesChanged;
                this.view.CurrentSelectedEntityChanged += HandleCurrentSelectedEntityChanged;
            }
        }



        public List<EntityMetadata> RetrieveEntitiesFromDatasource()
        {
            var inputCachedMetadata = new List<EntityMetadata>();

            if (inputCachedMetadata.Count == 0)//|| isNewConnection) ///TODO: address new connection
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

                ///TODO; handle when retrieving entities result to erorrs or no results
                //if (exception != null)
                //{
                //    notificationService.DisplayErrorFeedback(owner, $"An error occured: {exception.Message}");
                //}
                //else
                //{
                //    if (items != null && items.Count > 0)
                //    {
                //        listView.Items.AddRange(items.ToArray());
                //    }
                //    else
                //    {
                //        notificationService.DisplayWarningFeedback(owner, "The system does not contain any entities");
                //    }
                //}
            }

            return inputCachedMetadata;
        }

        public async Task PopulateAttributes(string entityLogicalName, List<EntityMetadata> listViewSelectedItem, ServiceParameters serviceParameters)
        {
            //if (!workingstate)
            //{
            //    lvAttributes.Items.Clear();
            //    chkAllAttributes.Checked = true;

            //InitFilter(listViewSelectedItem);
            if (listViewSelectedItem != null)
            {
                // Exception error = null;
                //  List<ListViewItem> result = null;

                // await Task.Run(() =>
                //    {
                //   var attributeController = new AttributeController();
                // try
                // {
                //  var unmarkedattributes = Settings[organisationId.ToString()][this.entityLogicalName].UnmarkedAttributes;
                //   var attributes = attributeController.GetAttributeList(entityLogicalName, cbShowSystemAttributes.Checked, serviceParameters);
                //      result = attributeController.ProcessAllAttributeMetadata(unmarkedattributes, attributes, entityLogicalName, entityAttributes);
                //  }
                //catch (Exception ex)
                // {
                //   error = ex;
                // }
                //    });
                //var controller = new ListController();
                //var e = new RunWorkerCompletedEventArgs(result, error, false);
                //controller.OnPopulateCompletedAction(e, NotificationService, this, lvAttributes, cbShowSystemAttributes.Checked);
                //ManageWorkingState(false);
            }
            // }
        }


        public List<AttributeMetadata> GetAttributeList(string entityLogicalName)//, ServiceParameters serviceParameters)
        {
            var serviceParameters = new ServiceParameters(organizationService, metadataService, notificationService, exceptionService);
            var entitymeta = serviceParameters.MetadataService.RetrieveEntities(entityLogicalName, serviceParameters.OrganizationService, serviceParameters.ExceptionService);

            var attributes = FilterAttributes(entitymeta, view.ShowSystemAttributes);

            if (attributes != null)
            {
                attributes = attributes.OrderByDescending(p => p.IsPrimaryId)
                                       .ThenByDescending(p => p.IsPrimaryName)
                                       .ThenByDescending(p => p.IsCustomAttribute != null && p.IsCustomAttribute.Value)
                                       .ThenBy(p => p.IsLogical != null && p.IsLogical.Value)
                                       .ThenBy(p => p.LogicalName).ToList();
            }

            return attributes;
        }

        public List<AttributeMetadata> FilterAttributes(EntityMetadata entityMetadata, bool showSystemAttributes)
        {
            var attributes = entityMetadata.Attributes?.ToList();

            if (attributes != null && !showSystemAttributes)
            {
                attributes = attributes.Where(p => p.IsLogical != null
                                                    && !p.IsLogical.Value
                                                    && p.IsValidForRead != null
                                                    && p.IsValidForRead.Value
                                                    && (p.IsValidForCreate != null && p.IsValidForCreate.Value || p.IsValidForUpdate != null && p.IsValidForUpdate.Value))
                                        .ToList();
            }

            return attributes;
        }


        private void ShowSystemEntitiesChanged(object sender, System.EventArgs e)
        {
            view.EntityMetadataList = RetrieveEntitiesFromDatasource();
        }

        private void HandleRetrieveEntitiesRequest(object sender, System.EventArgs e)
        {
            view.EntityMetadataList = RetrieveEntitiesFromDatasource();
        }

        private void HandleCurrentSelectedEntityChanged(object sender, UserControls.MigratorEventArgs<EntityMetadata> e)
        {
            var result = GetAttributeList(e.Input.LogicalName);
            view.EntityAttributes = result;
        }
    }
}