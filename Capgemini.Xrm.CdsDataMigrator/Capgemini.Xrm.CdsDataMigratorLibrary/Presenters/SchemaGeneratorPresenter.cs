using Capgemini.Xrm.CdsDataMigratorLibrary.Controllers;
using Capgemini.Xrm.CdsDataMigratorLibrary.Exceptions;
using Capgemini.Xrm.CdsDataMigratorLibrary.Models;
using Capgemini.Xrm.CdsDataMigratorLibrary.Services;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Capgemini.Xrm.CdsDataMigratorLibrary.Core;
using System.ComponentModel;
using Capgemini.Xrm.DataMigration.Config;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Presenters
{
    public class SchemaGeneratorPresenter
    {
        private readonly ISchemaGeneratorView view;
        private readonly IOrganizationService organizationService;
        private readonly IMetadataService metadataService;
        private readonly INotificationService notificationService;
        private readonly IExceptionService exceptionService;
        private readonly List<EntityMetadata> cachedMetadata = new List<EntityMetadata>();
        private bool workingstate;

        //private readonly CrmSchemaConfiguration crmSchemaConfiguration = new CrmSchemaConfiguration();
        private readonly AttributeTypeMapping attributeMapping = new AttributeTypeMapping();
        private readonly HashSet<string> checkedEntity = new HashSet<string>();
        private readonly HashSet<string> selectedEntity = new HashSet<string>();
        private readonly HashSet<string> checkedRelationship = new HashSet<string>();
        //private readonly Dictionary<string, List<Item<EntityReference, EntityReference>>> mapping = new Dictionary<string, List<Item<EntityReference, EntityReference>>>();
        private readonly Dictionary<string, HashSet<string>> entityAttributes = new Dictionary<string, HashSet<string>>();
        private readonly Dictionary<string, HashSet<string>> entityRelationships = new Dictionary<string, HashSet<string>>();
        //private readonly Dictionary<string, string> filterQuery = new Dictionary<string, string>();
        //private readonly Dictionary<string, Dictionary<string, List<string>>> lookupMaping = new Dictionary<string, Dictionary<string, List<string>>>();
        //private readonly Dictionary<string, Dictionary<Guid, Guid>> mapper = new Dictionary<string, Dictionary<Guid, Guid>>();
        //private readonly List<EntityMetadata> cachedMetadata = new List<EntityMetadata>();

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
                this.view.LoadSchema += LoadSchemaEventHandler;
                this.view.SaveSchema += SaveSchemaEventHandler;
            }
        }

        //public async Task RetrieveEntitiesFromDatasource()
        //{
        //    if (!workingstate)
        //    {
        //        //ClearAllListViews();
        //        ManageWorkingState(true);

        //        //informationPanel = InformationPanel.GetInformationPanel(this, "Loading entities...", 340, 150);

        //        view.EntityMetadataList = await Task.Run(() =>
        //        {
        //            var inputCachedMetadata = new List<EntityMetadata>();

        //            if (inputCachedMetadata.Count == 0)//|| isNewConnection) ///TODO: address new connection
        //            {
        //                var serviceParameters = new ServiceParameters(organizationService, metadataService, notificationService, exceptionService);
        //                var sourceList = serviceParameters.MetadataService.RetrieveEntities(serviceParameters.OrganizationService);

        //                //var controller = new EntityController();
        //                //e.Result = controller.RetrieveSourceEntitiesList(view.ShowSystemAttributes, cachedMetadata, entityAttributes, serviceParameters);

        //                if (!view.ShowSystemAttributes)
        //                {
        //                    sourceList = sourceList.Where(p => !p.IsLogicalEntity.Value && !p.IsIntersect.Value).ToList();
        //                }

        //                if (sourceList != null)
        //                {
        //                    inputCachedMetadata.AddRange(sourceList.OrderBy(p => p.IsLogicalEntity.Value).ThenBy(p => p.IsIntersect.Value).ThenByDescending(p => p.IsCustomEntity.Value).ThenBy(p => p.LogicalName).ToList());
        //                }

        //                ///TODO; handle when retrieving entities result to erorrs or no results
        //                //if (exception != null)
        //                //{
        //                //    notificationService.DisplayErrorFeedback(owner, $"An error occured: {exception.Message}");
        //                //}
        //                //else
        //                //{
        //                //    if (items != null && items.Count > 0)
        //                //    {
        //                //        listView.Items.AddRange(items.ToArray());
        //                //    }
        //                //    else
        //                //    {
        //                //        notificationService.DisplayWarningFeedback(owner, "The system does not contain any entities");
        //                //    }
        //                //}
        //            }

        //            return inputCachedMetadata;
        //        });

        //        // view.EntityMetadataList = await RetrieveEntitiesFromDatasource();
        //        ManageWorkingState(false);
        //    }
        //}

        //public async Task PopulateAttributes(string entityLogicalName, List<EntityMetadata> listViewSelectedItem, ServiceParameters serviceParameters)
        //{
        //    //if (!workingstate)
        //    //{
        //    //    lvAttributes.Items.Clear();
        //    //    chkAllAttributes.Checked = true;

        //    //InitFilter(listViewSelectedItem);
        //    if (listViewSelectedItem != null)
        //    {
        //        // Exception error = null;
        //        //  List<ListViewItem> result = null;

        //        // await Task.Run(() =>
        //        //    {
        //        //   var attributeController = new AttributeController();
        //        // try
        //        // {
        //        //  var unmarkedattributes = Settings[organisationId.ToString()][this.entityLogicalName].UnmarkedAttributes;
        //        //   var attributes = attributeController.GetAttributeList(entityLogicalName, cbShowSystemAttributes.Checked, serviceParameters);
        //        //      result = attributeController.ProcessAllAttributeMetadata(unmarkedattributes, attributes, entityLogicalName, entityAttributes);
        //        //  }
        //        //catch (Exception ex)
        //        // {
        //        //   error = ex;
        //        // }
        //        //    });
        //        //var controller = new ListController();
        //        //var e = new RunWorkerCompletedEventArgs(result, error, false);
        //        //controller.OnPopulateCompletedAction(e, NotificationService, this, lvAttributes, cbShowSystemAttributes.Checked);
        //        //ManageWorkingState(false);
        //    }
        //    // }
        //}


        public List<AttributeMetadata> GetAttributeList(string entityLogicalName)//, ServiceParameters serviceParameters)
        {
            ServiceParameters serviceParameters = InstantiateServiceParameters();
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

        public void ClearMemory()
        {
            //ClearInternalMemory();=>
            checkedEntity.Clear();
            entityAttributes.Clear();
            entityRelationships.Clear();
            //mapper.Clear();
            //lookupMaping.Clear();
            //filterQuery.Clear();
            selectedEntity.Clear();
            checkedRelationship.Clear();
            //mapping.Clear();

            //ClearUIMemory();=>
            //tbSchemaPath.Clear();
            //tbImportConfig.Clear();
            //tbExportConfig.Clear();
        }

        public async Task LoadSchemaFile(string schemaFilePath, bool working, INotificationService notificationService, Dictionary<string, HashSet<string>> inputEntityAttributes, Dictionary<string, HashSet<string>> inputEntityRelationships)
        {
            if (!string.IsNullOrWhiteSpace(schemaFilePath))
            {
                try
                {
                    var crmSchema = CrmSchemaConfiguration.ReadFromFile(schemaFilePath);
                    var controller = new EntityController();
                    controller.StoreEntityData(crmSchema.Entities?.ToArray(), inputEntityAttributes, inputEntityRelationships);
                    ClearAllListViews();
                    await PopulateEntities(working);
                }
                catch (Exception ex)
                {
                    notificationService.DisplayFeedback($"Schema File load error, ensure to load correct Schema file, Error: {ex.Message}");
                }
            }
        }

        private async Task PopulateEntities(bool working)
        {
            if (!working)
            {
                ClearAllListViews();
                ManageWorkingState(true);

                var systemAttributes = view.ShowSystemAttributes;
                List<TreeNode> entityList = new List<TreeNode>();
                var controller = new EntityController();
                Exception exception = null;

                //informationPanel = InformationPanel.GetInformationPanel(this, "Loading entities...", 340, 150);

                // using (var bwFill = new BackgroundWorker()) 
                await Task.Run(() =>
                    {
                        //bwFill.DoWork += (sender, e) =>
                        // {
                        var serviceParameters = new ServiceParameters(organizationService, metadataService, notificationService, exceptionService);

                        entityList = controller.RetrieveSourceEntitiesList(systemAttributes, cachedMetadata, entityAttributes, serviceParameters);

                        ///TODO: HANDLE EXCEPTION
                        //  };
                    });
                //    bwFill.RunWorkerCompleted += (sender, e) =>
                //{
                //      informationPanel.Dispose();
                //var controller = new EntityController();
                controller.PopulateEntitiesListView(entityList, exception, null, view.EntityList, notificationService);
                ManageWorkingState(false);
                //     };
                //     bwFill.RunWorkerAsync();                
            }
        }


        public void ClearAllListViews()
        {
            view.EntityList.Nodes.Clear();
            view.EntityAttributeList.Items.Clear();
            view.EntityRelationshipList.Items.Clear();
        }

        private void SaveSchemaEventHandler(object sender, MigratorEventArgs<string> e)
        {
            var serviceParameters = InstantiateServiceParameters();
            var controller = new SchemaController();
            var crmSchemaConfiguration = new CrmSchemaConfiguration();

            controller.SaveSchema(serviceParameters, checkedEntity, entityRelationships, entityAttributes, attributeMapping, crmSchemaConfiguration, e.Input);
        }

        private async void LoadSchemaEventHandler(object sender, MigratorEventArgs<string> e)
        {
            await LoadSchemaFile(e.Input, workingstate, notificationService, entityAttributes, entityRelationships);
        }

        private async void ShowSystemEntitiesChanged(object sender, System.EventArgs e)
        {
            var serviceParameters = InstantiateServiceParameters();

            var entityitem = view.EntityList.SelectedNode;
            var controller = new EntityController();
            var entityLogicalName = controller.GetEntityLogicalName(entityitem);
            await HandleListViewEntitiesSelectedIndexChanged(entityRelationships, entityLogicalName, selectedEntity,/*  lvEntities.SelectedItems, */ serviceParameters);
        }

        public async Task HandleListViewEntitiesSelectedIndexChanged(Dictionary<string, HashSet<string>> inputEntityRelationships, string inputEntityLogicalName, HashSet<string> inputSelectedEntity, /*ListView.SelectedListViewItemCollection selectedItems,*/ ServiceParameters serviceParameters)
        {
            //ListViewItem listViewSelectedItem = selectedItems.Count > 0 ? selectedItems[0] : null;

            await PopulateAttributes(inputEntityLogicalName,/* listViewSelectedItem,*/ serviceParameters);

            await PopulateRelationship(inputEntityLogicalName, inputEntityRelationships,/* listViewSelectedItem,*/ serviceParameters);
            var controller = new EntityController();
            controller.AddSelectedEntities(view.SelectedEntities.Count, inputEntityLogicalName, inputSelectedEntity);
        }

        private async void HandleRetrieveEntitiesRequest(object sender, System.EventArgs e)
        {
            ClearMemory();
            await PopulateEntities(workingstate);
        }

        private async void HandleCurrentSelectedEntityChanged(object sender, MigratorEventArgs<EntityMetadata> e)
        {
            var inputEntityRelationships = new Dictionary<string, HashSet<string>>();
            var serviceParameters = InstantiateServiceParameters();

            await PopulateAttributes(e.Input.LogicalName,/* listViewSelectedItem,*/ serviceParameters);
            await PopulateRelationship(e.Input.LogicalName, inputEntityRelationships,/* listViewSelectedItem,*/ serviceParameters);
            var controller = new EntityController();
            controller.AddSelectedEntities(view.SelectedEntities.Count, e.Input.LogicalName, selectedEntity);
        }

        public Settings Settings { get; set; }

        public void ManageWorkingState(bool working)
        {
            workingstate = working;
            view.EntityList.Enabled = !working;
            view.EntityAttributeList.Enabled = !working;
            view.EntityRelationshipList.Enabled = !working;
            view.Cursor = working ? Cursors.WaitCursor : Cursors.Default;
        }

        public async Task PopulateAttributes(string entityLogicalName,/* ListViewItem listViewSelectedItem,*/ ServiceParameters serviceParameters)
        {
            if (!workingstate)
            {
                view.EntityAttributeList.Items.Clear();
                //   chkAllAttributes.Checked = true;

                //   InitFilter(listViewSelectedItem);
                //    if (listViewSelectedItem != null)
                // {
                Exception error = null;
                List<ListViewItem> result = null;

                await Task.Run(() =>
                {
                    var attributeController = new AttributeController();
                    try
                    {
                        var unmarkedattributes = new List<string>();
                        var entityAttributes = new Dictionary<string, HashSet<string>>();
                    //var unmarkedattributes = Settings[organisationId.ToString()][entityLogicalName].UnmarkedAttributes;
                        var attributes = attributeController.GetAttributeList(entityLogicalName, view.ShowSystemAttributes, serviceParameters);
                        result = attributeController.ProcessAllAttributeMetadata(unmarkedattributes, attributes, entityLogicalName, entityAttributes);
                    }
                    catch (Exception ex)
                    {
                        error = ex;
                    }
                });
                var controller = new ListController();
                var e = new RunWorkerCompletedEventArgs(result, error, false);
                controller.OnPopulateCompletedAction(e, notificationService, null, view.EntityAttributeList, view.ShowSystemAttributes);
                ManageWorkingState(false);
                // }
            }
        }

        public async Task PopulateRelationship(string entityLogicalName, Dictionary<string, HashSet<string>> inputEntityRelationships,/* ListViewItem listViewSelectedItem,*/ ServiceParameters migratorParameters)
        {
            if (!workingstate)
            {
                view.EntityRelationshipList.Items.Clear();
                //InitFilter(listViewSelectedItem);
                //if (listViewSelectedItem != null)
                //{
                Exception error = null;
                List<ListViewItem> result = null;

                await Task.Run(() =>
                {
                    var controller = new RelationshipController();

                    try
                    {
                        result = controller.PopulateRelationshipAction(entityLogicalName, inputEntityRelationships, migratorParameters);
                    }
                    catch (Exception ex)
                    {
                        error = ex;
                    }
                });
                var listController = new ListController();
                var e = new RunWorkerCompletedEventArgs(result, error, false);
                listController.OnPopulateCompletedAction(e, notificationService, null, view.EntityRelationshipList, view.ShowSystemAttributes);
                ManageWorkingState(false);
                // }
            }
        }

        private ServiceParameters InstantiateServiceParameters()
        {
            return new ServiceParameters(organizationService, metadataService, notificationService, exceptionService);
        }

    }
}