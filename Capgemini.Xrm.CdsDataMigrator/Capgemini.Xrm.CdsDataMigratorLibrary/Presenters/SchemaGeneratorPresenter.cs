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
using Capgemini.Xrm.CdsDataMigratorLibrary.Extensions;
using Capgemini.Xrm.CdsDataMigratorLibrary.Helpers;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Presenters
{
    public class SchemaGeneratorPresenter
    {
        private readonly IOrganizationService organizationService;
        private readonly IMetadataService metadataService;
        private readonly INotificationService notificationService;
        private readonly IExceptionService exceptionService;
        private readonly Settings settings;
        private bool workingstate;
        private Guid organisationId = Guid.Empty;
        private string entityLogicalName;

        public SchemaGeneratorPresenter(ISchemaGeneratorView view, IOrganizationService organizationService, IMetadataService metadataService, INotificationService notificationService, IExceptionService exceptionService, Settings settings)
        {
            ParameterBag = new SchemaGeneratorParameterBag();
            View = view;
            this.organizationService = organizationService;
            this.metadataService = metadataService;
            this.notificationService = notificationService;
            this.exceptionService = exceptionService;
            this.settings = settings;

            if (View != null)
            {
                View.RetrieveEntities += HandleRetrieveEntitiesRequest;
                View.ShowSystemEntitiesChanged += ShowSystemEntitiesChanged;
                View.CurrentSelectedEntityChanged += HandleCurrentSelectedEntityChanged;
                View.LoadSchema += LoadSchemaEventHandler;
                View.SaveSchema += SaveSchemaEventHandler;
                View.SortAttributesList += HandleSortAttributesList;
                View.AttributeSelected += HandleAttributeSelected;
                View.SortRelationshipList += HandleSortRelationshipList;
                View.RelationshipSelected += HandleRelationshipSelected;
                View.EntitySelected += HandleEntitySelected;
            }
        }

        public SchemaGeneratorParameterBag ParameterBag { get; }

        public ISchemaGeneratorView View { get; }

        public async Task OnConnectionUpdated(Guid connectedOrgId, string connectedOrgFriendlyName)
        {
            organisationId = connectedOrgId;
            View.CurrentConnection = $"Connected to: {connectedOrgFriendlyName}";

            ClearMemory();
            await PopulateEntities(workingstate);
        }

        public List<AttributeMetadata> GetAttributeList(string entityLogicalName)
        {
            var serviceParameters = InstantiateServiceParameters();
            var entitymeta = serviceParameters.MetadataService.RetrieveEntities(entityLogicalName, serviceParameters.OrganizationService, serviceParameters.ExceptionService);

            var attributes = FilterAttributes(entitymeta, View.ShowSystemAttributes);

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
            var attributes = entityMetadata?.Attributes?.ToList();

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
            ParameterBag.CheckedEntity.Clear();
            ParameterBag.EntityAttributes.Clear();
            ParameterBag.EntityRelationships.Clear();
            ParameterBag.SelectedEntity.Clear();
            ParameterBag.CheckedRelationship.Clear();
        }

        public async Task LoadSchemaFile(string schemaFilePath, bool working, INotificationService notificationService, Dictionary<string, HashSet<string>> inputEntityAttributes, Dictionary<string, HashSet<string>> inputEntityRelationships)
        {
            if (!string.IsNullOrWhiteSpace(schemaFilePath))
            {
                try
                {
                    var crmSchema = CrmSchemaConfiguration.ReadFromFile(schemaFilePath);
                    View.ShowInformationPanel("Loading entities...", 340, 150);
                    crmSchema.Entities?.StoreEntityData(inputEntityAttributes, inputEntityRelationships);
                    ClearAllListViews();
                    await PopulateEntities(working);

                    ParameterBag.CheckedEntity.Clear();
                    foreach (var item in View.SelectedEntities)
                    {
                        ParameterBag.CheckedEntity.Add(item.LogicalName);
                    }
                }
                catch (Exception ex)
                {
                    notificationService.DisplayFeedback($"Schema File load error, ensure to load correct Schema file, Error: {ex.Message}");
                }
                finally
                {
                    View.CloseInformationPanel();
                }
            }
            else
            {
                notificationService.DisplayFeedback("Please specify the Schema File to load!");
            }
        }

        public async Task HandleListViewEntitiesSelectedIndexChanged(Dictionary<string, HashSet<string>> inputEntityRelationships, string inputEntityLogicalName, HashSet<string> inputSelectedEntity)
        {
            entityLogicalName = inputEntityLogicalName;
            var serviceParameters = InstantiateServiceParameters();

            await PopulateAttributes(inputEntityLogicalName, serviceParameters);

            await PopulateRelationship(inputEntityLogicalName, inputEntityRelationships, serviceParameters);

            if (View.SelectedEntities != null)
            {
                AddSelectedEntities(View.SelectedEntities.Count, inputEntityLogicalName, inputSelectedEntity);
            }
        }

        public void AddSelectedEntities(int selectedItemsCount, string inputEntityLogicalName, HashSet<string> inputSelectedEntity)
        {
            if (selectedItemsCount > 0 &&
                !(
                    string.IsNullOrEmpty(inputEntityLogicalName) &&
                    inputSelectedEntity.Contains(inputEntityLogicalName)
                  )
                )
            {
                inputSelectedEntity.Add(inputEntityLogicalName);
            }
        }

        public void ManageWorkingState(bool working)
        {
            workingstate = working;
            if (View != null)
            {
                if (View.EntityList != null)
                {
                    View.EntityList.Enabled = !working;
                }

                if (View.EntityAttributeList != null)
                {
                    View.EntityAttributeList.Enabled = !working;
                }

                if (View.EntityRelationshipList != null)
                {
                    View.EntityRelationshipList.Enabled = !working;
                }

                View.Cursor = working ? Cursors.WaitCursor : Cursors.Default;
            }
        }

        public async Task PopulateAttributes(string entityLogicalName, ServiceParameters serviceParameters)
        {
            if (!workingstate && View != null && View.EntityAttributeList != null)
            {
                View.EntityAttributeList.Items.Clear();

                Exception error = null;
                List<ListViewItem> result = null;

                await Task.Run(() =>
                {
                    try
                    {
                        var unmarkedattributes = settings[organisationId.ToString()][entityLogicalName].UnmarkedAttributes;
                        var attributes = serviceParameters.GetAttributeList(entityLogicalName, View.ShowSystemAttributes);
                        result = attributes.ProcessAllAttributeMetadata(unmarkedattributes, entityLogicalName, ParameterBag.EntityAttributes);
                    }
                    catch (Exception ex)
                    {
                        error = ex;
                    }
                });

                var e = new RunWorkerCompletedEventArgs(result, error, false);
                View.EntityAttributeList.OnPopulateCompletedAction(e, notificationService, null, View.ShowSystemAttributes);
                ManageWorkingState(false);
            }
        }

        public async Task PopulateRelationship(string entityLogicalName, Dictionary<string, HashSet<string>> inputEntityRelationships, ServiceParameters migratorParameters)
        {
            if (!workingstate && View != null && View.EntityRelationshipList != null)
            {
                View.EntityRelationshipList.Items.Clear();
                Exception error = null;
                List<ListViewItem> result = null;

                await Task.Run(() =>
                {
                    try
                    {
                        result = migratorParameters.PopulateRelationshipAction(entityLogicalName, inputEntityRelationships);
                    }
                    catch (Exception ex)
                    {
                        error = ex;
                    }
                });

                var e = new RunWorkerCompletedEventArgs(result, error, false);

                View.EntityRelationshipList.OnPopulateCompletedAction(e, notificationService, null, View.ShowSystemAttributes);
                ManageWorkingState(false);
            }
        }

        public void ClearAllListViews()
        {
            View?.EntityList?.Nodes?.Clear();
            View?.EntityAttributeList?.Items?.Clear();
            View?.EntityRelationshipList?.Items?.Clear();
        }

        public void StoreAttributeIfRequiresKey(string logicalName, ItemCheckEventArgs e, Dictionary<string, HashSet<string>> inputEntityAttributes, string inputEntityLogicalName)
        {
            var attributeSet = new HashSet<string>();
            if (e.CurrentValue.ToString() != "Checked")
            {
                attributeSet.Add(logicalName);
            }

            inputEntityAttributes.Add(inputEntityLogicalName, attributeSet);
        }

        public void StoreAttriubteIfKeyExists(string logicalName, ItemCheckEventArgs e, Dictionary<string, HashSet<string>> inputEntityAttributes, string inputEntityLogicalName)
        {
            var attributeSet = inputEntityAttributes[inputEntityLogicalName];

            if (e.CurrentValue.ToString() == "Checked")
            {
                if (attributeSet.Contains(logicalName))
                {
                    attributeSet.Remove(logicalName);
                }
            }
            else
            {
                attributeSet.Add(logicalName);
            }
        }

        public async void LoadSchemaEventHandler(object sender, MigratorEventArgs<string> e)
        {
            await LoadSchemaFile(e.Input, workingstate, notificationService, ParameterBag.EntityAttributes, ParameterBag.EntityRelationships);
        }

        private void HandleEntitySelected(object sender, MigratorEventArgs<TreeNode> e)
        {
            var entity = e.Input.Tag as EntityMetadata;
            if (entity != null)
            {
                var logicalName = entity.LogicalName;
                if (!e.Input.Checked)
                {
                    if (ParameterBag.CheckedEntity.Contains(logicalName))
                    {
                        ParameterBag.CheckedEntity.Remove(logicalName);
                    }
                }
                else
                {
                    ParameterBag.CheckedEntity.Add(logicalName);
                }
            }
        }

        private void HandleRelationshipSelected(object sender, MigratorEventArgs<ItemCheckEventArgs> e)
        {
            var indexNumber = e.Input.Index;
            var logicalName = View.EntityRelationshipList.Items[indexNumber].SubItems[1].Text;

            if (ParameterBag.EntityRelationships.ContainsKey(entityLogicalName))
            {
                CollectionHelpers.StoreRelationshipIfKeyExists(logicalName, e.Input, entityLogicalName, ParameterBag.EntityRelationships);
            }
            else
            {
                CollectionHelpers.StoreRelationshipIfRequiresKey(logicalName, e.Input, entityLogicalName, ParameterBag.EntityRelationships);
            }
        }

        private async Task PopulateEntities(bool working)
        {
            if (!working)
            {
                ClearAllListViews();
                ManageWorkingState(true);

                var systemAttributes = View.ShowSystemAttributes;
                List<TreeNode> entityList = new List<TreeNode>();

                await Task.Run(() =>
                    {
                        var serviceParameters = new ServiceParameters(organizationService, metadataService, notificationService, exceptionService);

                        entityList = serviceParameters.RetrieveSourceEntitiesList(systemAttributes, ParameterBag.CachedMetadata, ParameterBag.EntityAttributes);
                    });

                View.EntityList.PopulateEntitiesTreeView(entityList, null, notificationService);
                ManageWorkingState(false);
            }
        }

        private void SaveSchemaEventHandler(object sender, MigratorEventArgs<string> e)
        {
            if (!string.IsNullOrWhiteSpace(e.Input))
            {
                var serviceParameters = InstantiateServiceParameters();
                var schemaExtension = new SchemaExtension();
                var crmSchemaConfiguration = new CrmSchemaConfiguration();

                View.ShowInformationPanel($"Saving schema file {e.Input} ...");

                schemaExtension.SaveSchema(serviceParameters,
                                            ParameterBag.CheckedEntity,
                                            ParameterBag.EntityRelationships,
                                            ParameterBag.EntityAttributes,
                                            ParameterBag.AttributeMapping,
                                            crmSchemaConfiguration,
                                            e.Input);

                View.CloseInformationPanel();
            }
            else
            {
                notificationService.DisplayFeedback($"Please specify the Schema File to save to!");
            }
        }

        private async void ShowSystemEntitiesChanged(object sender, System.EventArgs e)
        {
            var entityitem = View.EntityList.SelectedNode;
            var logicalName = entityitem.GetEntityLogicalName();
            await HandleListViewEntitiesSelectedIndexChanged(ParameterBag.EntityRelationships, logicalName, ParameterBag.SelectedEntity);
        }

        private ServiceParameters InstantiateServiceParameters()
        {
            return new ServiceParameters(organizationService, metadataService, notificationService, exceptionService);
        }

        private async void HandleRetrieveEntitiesRequest(object sender, System.EventArgs e)
        {
            ClearMemory();
            await PopulateEntities(workingstate);
        }

        private async void HandleCurrentSelectedEntityChanged(object sender, MigratorEventArgs<EntityMetadata> e)
        {
            await HandleListViewEntitiesSelectedIndexChanged(ParameterBag.EntityRelationships, e.Input.LogicalName, ParameterBag.SelectedEntity);
        }

        private void HandleAttributeSelected(object sender, MigratorEventArgs<ItemCheckEventArgs> e)
        {
            var indexNumber = e.Input.Index;
            var logicalName = View.EntityAttributeList.Items[indexNumber].SubItems[1].Text;

            if (ParameterBag.EntityAttributes.ContainsKey(entityLogicalName))
            {
                StoreAttriubteIfKeyExists(logicalName, e.Input, ParameterBag.EntityAttributes, entityLogicalName);
            }
            else
            {
                StoreAttributeIfRequiresKey(logicalName, e.Input, ParameterBag.EntityAttributes, entityLogicalName);
            }
        }

        private void HandleSortRelationshipList(object sender, MigratorEventArgs<int> e)
        {
            if (e.Input != View.EntityRelationshipList.Columns.Count)
            {
                View.EntityRelationshipList.SetListViewSorting(e.Input, organisationId.ToString(), settings);
            }
        }

        private void HandleSortAttributesList(object sender, MigratorEventArgs<int> e)
        {
            if (e.Input != View.EntityAttributeList.Columns.Count)
            {
                View.EntityAttributeList.SetListViewSorting(e.Input, organisationId.ToString(), settings);
            }
        }
    }
}