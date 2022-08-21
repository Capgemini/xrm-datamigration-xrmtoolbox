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
using XrmToolBox.Extensibility;

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
        private readonly Settings settings;
        private bool workingstate;
        private Guid organisationId = Guid.Empty;
        private string entityLogicalName;

        private readonly AttributeTypeMapping attributeMapping = new AttributeTypeMapping();
        private readonly HashSet<string> checkedEntity = new HashSet<string>();
        private readonly HashSet<string> selectedEntity = new HashSet<string>();
        private readonly HashSet<string> checkedRelationship = new HashSet<string>();
        private readonly Dictionary<string, HashSet<string>> entityAttributes = new Dictionary<string, HashSet<string>>();
        private readonly Dictionary<string, HashSet<string>> entityRelationships = new Dictionary<string, HashSet<string>>();

        public SchemaGeneratorPresenter(ISchemaGeneratorView view, IOrganizationService organizationService, IMetadataService metadataService, INotificationService notificationService, IExceptionService exceptionService, Settings settings)
        {
            this.view = view;
            this.organizationService = organizationService;
            this.metadataService = metadataService;
            this.notificationService = notificationService;
            this.exceptionService = exceptionService;
            this.settings = settings;

            if (this.view != null)
            {
                this.view.RetrieveEntities += HandleRetrieveEntitiesRequest;
                this.view.ShowSystemEntitiesChanged += ShowSystemEntitiesChanged;
                this.view.CurrentSelectedEntityChanged += HandleCurrentSelectedEntityChanged;
                this.view.LoadSchema += LoadSchemaEventHandler;
                this.view.SaveSchema += SaveSchemaEventHandler;
                this.view.SortAttributesList += HandleSortAttributesList;
                this.view.AttributeSelected += HandleAttributeSelected;
                this.view.SortRelationshipList += HandleSortRelationshipList;
                this.view.RelationshipSelected += HandleRelationshipSelected;
                this.view.EntitySelected += HandleEntitySelected; ;
            }
        }

        private void HandleEntitySelected(object sender, MigratorEventArgs<TreeNode> e)
        {
            var entity = e.Input.Tag as EntityMetadata;
            if (entity != null)
            {
                var logicalName = entity.LogicalName;
                if (!e.Input.Checked)
                {
                    if (checkedEntity.Contains(logicalName))
                    {
                        checkedEntity.Remove(logicalName);
                    }
                }
                else
                {
                    checkedEntity.Add(logicalName);
                }
            }
        }

        private void HandleRelationshipSelected(object sender, MigratorEventArgs<ItemCheckEventArgs> e)
        {
            var indexNumber = e.Input.Index;
            var logicalName = view.EntityRelationshipList.Items[indexNumber].SubItems[1].Text;
            var controller = new RelationshipController();

            if (entityRelationships.ContainsKey(entityLogicalName))
            {
                controller.StoreRelationshipIfKeyExists(logicalName, e.Input, entityLogicalName, entityRelationships);
            }
            else
            {
                controller.StoreRelationshipIfRequiresKey(logicalName, e.Input, entityLogicalName, entityRelationships);
            }
        }

        private void HandleAttributeSelected(object sender, MigratorEventArgs<ItemCheckEventArgs> e)
        {
            var indexNumber = e.Input.Index;
            var logicalName = view.EntityAttributeList.Items[indexNumber].SubItems[1].Text;
            var controller = new AttributeController();

            if (entityAttributes.ContainsKey(entityLogicalName))
            {
                controller.StoreAttriubteIfKeyExists(logicalName, e.Input, entityAttributes, entityLogicalName);
            }
            else
            {
                controller.StoreAttributeIfRequiresKey(logicalName, e.Input, entityAttributes, entityLogicalName);
            }
        }

        private void HandleSortRelationshipList(object sender, MigratorEventArgs<int> e)
        {
            if (e.Input != view.EntityRelationshipList.Columns.Count)// 3)
            {
                var controller = new ListController();
                controller.SetListViewSorting(view.EntityRelationshipList, e.Input, organisationId.ToString(), settings);
            }
        }

        private void HandleSortAttributesList(object sender, MigratorEventArgs<int> e)
        {
            if (e.Input != view.EntityAttributeList.Columns.Count)
            {
                var controller = new ListController();
                controller.SetListViewSorting(view.EntityAttributeList, e.Input, organisationId.ToString(), settings);
            }
        }

        public async Task OnConnectionUpdated(Guid connectedOrgId, string connectedOrgFriendlyName)
        {
            organisationId = connectedOrgId;
            view.CurrentConnection = $"Connected to: {connectedOrgFriendlyName}";

            ClearMemory();
            await PopulateEntities(workingstate);
        }

        public List<AttributeMetadata> GetAttributeList(string entityLogicalName)
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
            checkedEntity.Clear();
            entityAttributes.Clear();
            entityRelationships.Clear();
            selectedEntity.Clear();
            checkedRelationship.Clear();
        }

        public async Task LoadSchemaFile(string schemaFilePath, bool working, INotificationService notificationService, Dictionary<string, HashSet<string>> inputEntityAttributes, Dictionary<string, HashSet<string>> inputEntityRelationships)
        {
            if (!string.IsNullOrWhiteSpace(schemaFilePath))
            {
                try
                {
                    var crmSchema = CrmSchemaConfiguration.ReadFromFile(schemaFilePath);
                    var controller = new EntityController();
                    view.ShowInformationPanel("Loading entities...", 340, 150);
                    controller.StoreEntityData(crmSchema.Entities?.ToArray(), inputEntityAttributes, inputEntityRelationships);
                    ClearAllListViews();
                    await PopulateEntities(working);

                    checkedEntity.Clear();
                    foreach (var item in view.SelectedEntities)
                    {
                        checkedEntity.Add(item.LogicalName);
                    }
                }
                catch (Exception ex)
                {
                    notificationService.DisplayFeedback($"Schema File load error, ensure to load correct Schema file, Error: {ex.Message}");
                }
                finally
                {
                    view.CloseInformationPanel();
                }
            }
            else
            {
                notificationService.DisplayFeedback($"Please specify the Schema File to load!");
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

                await Task.Run(() =>
                    {
                        var serviceParameters = new ServiceParameters(organizationService, metadataService, notificationService, exceptionService);

                        entityList = controller.RetrieveSourceEntitiesList(systemAttributes, cachedMetadata, entityAttributes, serviceParameters);
                    });

                controller.PopulateEntitiesListView(entityList, exception, null, view.EntityList, notificationService);
                ManageWorkingState(false);
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
            if (!string.IsNullOrWhiteSpace(e.Input))
            {
                var serviceParameters = InstantiateServiceParameters();
                var controller = new SchemaController();
                var crmSchemaConfiguration = new CrmSchemaConfiguration();

                view.ShowInformationPanel($"Saving schema file {e.Input} ...");

                controller.SaveSchema(serviceParameters, checkedEntity, entityRelationships, entityAttributes, attributeMapping, crmSchemaConfiguration, e.Input);

                view.CloseInformationPanel();
            }
            else
            {
                notificationService.DisplayFeedback($"Please specify the Schema File to save to!");
            }
        }

        private async void LoadSchemaEventHandler(object sender, MigratorEventArgs<string> e)
        {
            await LoadSchemaFile(e.Input, workingstate, notificationService, entityAttributes, entityRelationships);
        }

        private async void ShowSystemEntitiesChanged(object sender, System.EventArgs e)
        {
            var entityitem = view.EntityList.SelectedNode;
            var controller = new EntityController();
            var logicalName = controller.GetEntityLogicalName(entityitem);
            await HandleListViewEntitiesSelectedIndexChanged(entityRelationships, logicalName, selectedEntity);
        }

        public async Task HandleListViewEntitiesSelectedIndexChanged(Dictionary<string, HashSet<string>> inputEntityRelationships, string inputEntityLogicalName, HashSet<string> inputSelectedEntity)
        {
            entityLogicalName = inputEntityLogicalName;
            var serviceParameters = InstantiateServiceParameters();

            await PopulateAttributes(inputEntityLogicalName, serviceParameters);

            await PopulateRelationship(inputEntityLogicalName, inputEntityRelationships, serviceParameters);
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
            await HandleListViewEntitiesSelectedIndexChanged(entityRelationships, e.Input.LogicalName, selectedEntity);
        }

        public void ManageWorkingState(bool working)
        {
            workingstate = working;
            view.EntityList.Enabled = !working;
            view.EntityAttributeList.Enabled = !working;
            view.EntityRelationshipList.Enabled = !working;
            view.Cursor = working ? Cursors.WaitCursor : Cursors.Default;
        }

        public async Task PopulateAttributes(string entityLogicalName, ServiceParameters serviceParameters)
        {
            if (!workingstate)
            {
                view.EntityAttributeList.Items.Clear();

                Exception error = null;
                List<ListViewItem> result = null;

                await Task.Run(() =>
                {
                    var attributeController = new AttributeController();
                    try
                    {
                        var unmarkedattributes = settings[organisationId.ToString()][entityLogicalName].UnmarkedAttributes;
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
            }
        }

        public async Task PopulateRelationship(string entityLogicalName, Dictionary<string, HashSet<string>> inputEntityRelationships, ServiceParameters migratorParameters)
        {
            if (!workingstate)
            {
                view.EntityRelationshipList.Items.Clear();
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
                
                var e = new RunWorkerCompletedEventArgs(result, error, false);
                var listcontroller = new ListController();
                listcontroller.OnPopulateCompletedAction(e, notificationService, null, view.EntityRelationshipList, view.ShowSystemAttributes);
                ManageWorkingState(false);
            }
        }        

        private ServiceParameters InstantiateServiceParameters()
        {
            return new ServiceParameters(organizationService, metadataService, notificationService, exceptionService);
        }

    }
}