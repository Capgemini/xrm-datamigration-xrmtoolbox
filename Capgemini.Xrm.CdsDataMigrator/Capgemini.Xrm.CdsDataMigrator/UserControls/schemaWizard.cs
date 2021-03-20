using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Capgemini.Xrm.DataMigration.Config;
using Capgemini.Xrm.DataMigration.CrmStore.Config;
using Capgemini.Xrm.DataMigration.Model;
using Capgemini.Xrm.DataMigration.XrmToolBox.Services;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Forms;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Model;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Tooling.Connector;
using MyXrmToolBoxPlugin3;
using NuGet;
using XrmToolBox.Extensibility;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin
{
    /// <summary>
    /// Implementation of PluginControl.
    /// </summary>
    public partial class SchemaWizard : UserControl
    {
        private readonly CrmSchemaConfiguration crmSchemaConfiguration = new CrmSchemaConfiguration();
        private readonly AttributeTypeMapping attributeMapping = new AttributeTypeMapping();
        private readonly HashSet<string> checkedEntity = new HashSet<string>();
        private readonly HashSet<string> selectedEntity = new HashSet<string>();
        private readonly HashSet<string> checkedRelationship = new HashSet<string>();
        private readonly Dictionary<string, List<Item<EntityReference, EntityReference>>> mapping = new Dictionary<string, List<Item<EntityReference, EntityReference>>>();
        private readonly Dictionary<string, HashSet<string>> entityAttributes = new Dictionary<string, HashSet<string>>();
        private readonly Dictionary<string, HashSet<string>> entityRelationships = new Dictionary<string, HashSet<string>>();

        //private readonly IMetadataService metadataService;
        private bool workingstate;

        private Panel informationPanel;
        private Guid organisationId = Guid.Empty;

        private string entityLogicalName;
        private Dictionary<string, string> filterQuery = new Dictionary<string, string>();

        private Dictionary<string, Dictionary<string, List<string>>> lookupMaping = new Dictionary<string, Dictionary<string, List<string>>>();
        private Dictionary<string, Dictionary<Guid, Guid>> mapper = new Dictionary<string, Dictionary<Guid, Guid>>();
        private List<EntityMetadata> cachedMetadata;

        public SchemaWizard()
        {
            InitializeComponent();
            //metadataService = new MetadataService();
        }

        public event EventHandler<RequestConnectionEventArgs> OnConnectionRequested;

        public IOrganizationService OrganizationService { get; set; }

        public IMetadataService MetadataService { get; set; }

        public IFeedbackManager FeedbackManager { get; set; }

        public Core.Settings Settings { get; internal set; }

        public void OnConnectionUpdated(Guid ConnectedOrgId, string ConnectedOrgFriendlyName)
        {
            organisationId = ConnectedOrgId;
            toolStripLabelConnection.Text = $"Connected to: {ConnectedOrgFriendlyName}";
            RefreshEntities(true);
        }

        public List<ListViewItem> PopulateRelationshipAction(string inputEntityLogicalName, IOrganizationService service, IMetadataService metadataService, Dictionary<string, HashSet<string>> inputEntityRelationships)
        {
            var entityMetaData = metadataService.RetrieveEntities(inputEntityLogicalName, service);
            var sourceAttributesList = new List<ListViewItem>();
            if (entityMetaData != null && entityMetaData.ManyToManyRelationships != null && entityMetaData.ManyToManyRelationships.Any())
            {
                foreach (var relationship in entityMetaData.ManyToManyRelationships)
                {
                    var item = new ListViewItem(relationship.IntersectEntityName);
                    AddRelationship(relationship, item, sourceAttributesList);
                    UpdateCheckBoxesRelationShip(relationship, item, inputEntityRelationships, inputEntityLogicalName);
                }
            }

            return sourceAttributesList;
        }

        public string GetEntityLogicalName(ListViewItem entityitem)
        {
            string logicalName = null;
            // if (lvEntities.SelectedItems.Count > 0)
            // {
            //var entityitem = lvEntities.SelectedItems[0];
            if (entityitem != null && entityitem.Tag != null)
            {
                var entity = (EntityMetadata)entityitem.Tag;
                logicalName = entity.LogicalName;
            }
            //  }
            return logicalName;
        }

        public void ProcessListViewEntitiesSelectedIndexChanged(IMetadataService metadataService, Dictionary<string, HashSet<string>> inputEntityRelationships, ListViewItem entityitem)
        {
            //if (lvEntities.SelectedItems.Count > 0)
            //{
            entityLogicalName = GetEntityLogicalName(entityitem);// lvEntities.SelectedItems[0]);
            //}

            PopulateAttributes(entityLogicalName, OrganizationService, metadataService);

            var listViewItemSelected = lvEntities.SelectedItems.Count > 0;
            PopulateRelationship(entityLogicalName, OrganizationService, metadataService, inputEntityRelationships, listViewItemSelected);
            AddSelectedEntities(lvEntities.SelectedItems.Count, entityLogicalName, selectedEntity);
        }

        public void PopulateRelationship(string entityLogicalName, IOrganizationService service, IMetadataService metadataService, Dictionary<string, HashSet<string>> inputEntityRelationships, bool listViewItemSelected)
        {
            if (!workingstate)
            {
                lvRelationship.Items.Clear();
                InitFilter();
                if (listViewItemSelected)// lvEntities.SelectedItems.Count > 0)
                {
                    using (var bwFill = new BackgroundWorker())
                    {
                        bwFill.DoWork += (sender, e) =>
                        {
                            e.Result = PopulateRelationshipAction(entityLogicalName, service, metadataService, inputEntityRelationships);
                        };
                        bwFill.RunWorkerCompleted += (sender, e) =>
                        {
                            AsyncRunnerCompleteRelationShip(e);
                        };
                        bwFill.RunWorkerAsync();
                    }
                }
            }
        }

        public List<ListViewItem> RetrieveSourceEntitiesList(bool showSystemAttributes, IOrganizationService organizationService, IMetadataService metadataService)
        {
            var sourceList = metadataService.RetrieveEntities(organizationService);

            if (!showSystemAttributes)
            {
                sourceList = sourceList.Where(p => !p.IsLogicalEntity.Value && !p.IsIntersect.Value).ToList();
            }

            if (sourceList != null)
            {
                cachedMetadata = sourceList.OrderBy(p => p.IsLogicalEntity.Value).ThenBy(p => p.IsIntersect.Value).ThenByDescending(p => p.IsCustomEntity.Value).ThenBy(p => p.LogicalName).ToList();
            }

            var sourceEntitiesList = new List<ListViewItem>();

            foreach (EntityMetadata entity in cachedMetadata)
            {
                var name = entity.DisplayName.UserLocalizedLabel == null ? string.Empty : entity.DisplayName.UserLocalizedLabel.Label;
                var item = new ListViewItem(name)
                {
                    Tag = entity
                };
                item.SubItems.Add(entity.LogicalName);
                IsInvalidForCustomization(entity, item);
                UpdateCheckBoxesEntities(entity, item);

                sourceEntitiesList.Add(item);
            }

            return sourceEntitiesList;
        }

        public void PopulateEntitiesListView(List<ListViewItem> items, Exception exception)//RunWorkerCompletedEventArgs e)
        {
            if (/*e.Error*/ exception != null)
            {
                // MessageBox.Show(this, $"An error occured: {exception.Message}" /*+ e.Error.Message*/, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                FeedbackManager.DisplayErrorFeedback(this, $"An error occured: {exception.Message}");
            }
            else
            {
                //var items = (List<ListViewItem>)e.Result;
                if (items != null && items.Count > 0)
                {
                    lvEntities.Items.AddRange(items.ToArray());
                }
                else
                {
                    //MessageBox.Show(this, "The system does not contain any entities", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    FeedbackManager.DisplayWarningFeedback(this, "The system does not contain any entities");
                }
            }

            ManageWorkingState(false);
        }

        public void AsyncRunnerCompleteAttributeOperation(List<ListViewItem> items, Exception exception) //RunWorkerCompletedEventArgs e)
        {
            if (/*e.Error*/ exception != null)
            {
                //MessageBox.Show(this, "An error occured: " + e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                FeedbackManager.DisplayErrorFeedback(this, $"An error occured: {exception.Message}");
            }
            else if (items != null)
            {
                //var items = (List<ListViewItem>)e.Result;
                lvAttributes.Items.AddRange(items.ToArray());
            }

            ManageWorkingState(false);
        }

        public void LoadSchemaFile(string schemaFilePath)
        {
            if (!string.IsNullOrWhiteSpace(schemaFilePath))
            {
                try
                {
                    var crmSchema = CrmSchemaConfiguration.ReadFromFile(schemaFilePath);
                    StoreEntityData(crmSchema.Entities?.ToArray());
                    ClearAllListViews();
                    PopulateEntities();
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Schema File load error, ensure to load correct Schema file, Error:" + ex.Message);
                    FeedbackManager.DisplayFeedback($"Schema File load error, ensure to load correct Schema file, Error: {ex.Message}");
                }
            }
        }

        public void RefreshEntities(bool isNewConnection = false)
        {
            toolStrip2.Enabled = true;
            if (cachedMetadata == null || isNewConnection)
            {
                ClearMemory();
                PopulateEntities();
            }
        }

        public void ClearMemory()
        {
            ClearInternalMemory();
            ClearUIMemory();
        }

        public void AddSelectedEntities(int selectedItemsCount, string inputEntityLogicalName, HashSet<string> inputSelectedEntity)
        {
            if (/*lvEntities.SelectedItems.Count*/selectedItemsCount > 0 &&
                !(
                    string.IsNullOrEmpty(inputEntityLogicalName) &&
                    inputSelectedEntity.Contains(inputEntityLogicalName)
                  )
                )
            {
                inputSelectedEntity.Add(inputEntityLogicalName);
            }
        }

        public void LoadExportConfigFile(string exportConfigFilename)
        {
            if (!string.IsNullOrWhiteSpace(exportConfigFilename))
            {
                try
                {
                    var configFile = CrmExporterConfig.GetConfiguration(exportConfigFilename);
                    if (!configFile.CrmMigrationToolSchemaPaths.Any())
                    {
                        //MessageBox.Show("Invalid Export Config File");
                        FeedbackManager.DisplayFeedback("Invalid Export Config File");
                        tbExportConfig.Text = "";
                        return;
                    }

                    filterQuery = configFile.CrmMigrationToolSchemaFilters;
                    lookupMaping = configFile.LookupMapping;

                    //MessageBox.Show("Filters and Lookup Mappings loaded from Export Config File");
                    FeedbackManager.DisplayFeedback("Filters and Lookup Mappings loaded from Export Config File");
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Load Correct Export Config file, error:" + ex.Message);
                    FeedbackManager.DisplayFeedback($"Load Correct Export Config file, error:{ex.Message}");
                }
            }
        }

        private void TabStripButtonRetrieveEntitiesClick(object sender, EventArgs e)
        {
            ClearMemory();
            PopulateEntities();
        }

        private void ClearInternalMemory()
        {
            checkedEntity.Clear();
            entityAttributes.Clear();
            entityRelationships.Clear();
            mapper.Clear();
            lookupMaping.Clear();
            filterQuery.Clear();
            selectedEntity.Clear();
            checkedRelationship.Clear();
            mapping.Clear();
        }

        private void ClearUIMemory()
        {
            tbSchemaPath.Clear();
            tbImportConfig.Clear();
            tbExportConfig.Clear();
        }

        private void ListViewEntitiesSelectedIndexChanged(object sender, EventArgs e)
        {
            var entityitem = lvEntities.SelectedItems.Count > 0 ? lvEntities.SelectedItems[0] : null;
            ProcessListViewEntitiesSelectedIndexChanged(MetadataService, entityRelationships, entityitem);
        }

        private void AsyncRunnerCompleteRelationShip(RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                //MessageBox.Show(
                //    this,
                //    $"An error occured: {e.Error.Message}",
                //    "Error",
                //    MessageBoxButtons.OK,
                //    MessageBoxIcon.Error);
                FeedbackManager.DisplayErrorFeedback(this, $"An error occured: {e.Error.Message}");
            }
            else
            {
                var items = (List<ListViewItem>)e.Result;
                lvRelationship.Items.AddRange(items.ToArray());
            }

            ManageWorkingState(false);
        }

        private static void UpdateCheckBoxesRelationShip(ManyToManyRelationshipMetadata relationship, ListViewItem item, Dictionary<string, HashSet<string>> inputEntityRelationships, string inputEntityLogicalName)
        {
            if (inputEntityRelationships.ContainsKey(inputEntityLogicalName))
            {
                foreach (string attr in inputEntityRelationships[inputEntityLogicalName])
                {
                    item.Checked |= attr.Equals(relationship.IntersectEntityName, StringComparison.InvariantCulture);
                }
            }
        }

        private static void AddRelationship(ManyToManyRelationshipMetadata relationship, ListViewItem item, List<ListViewItem> sourceAttributesList)
        {
            item.SubItems.Add(relationship.IntersectEntityName);
            item.SubItems.Add(relationship.Entity2LogicalName);
            item.SubItems.Add(relationship.Entity2IntersectAttribute);
            sourceAttributesList.Add(item);
        }

        private void PopulateAttributes(string entityLogicalName, IOrganizationService service, IMetadataService metadataService)
        {
            if (!workingstate)
            {
                lvAttributes.Items.Clear();
                chkAllAttributes.Checked = true;
                InitFilter();
                if (lvEntities.SelectedItems.Count > 0)
                {
                    using (var bwFill = new BackgroundWorker())
                    {
                        bwFill.DoWork += (sender, e) =>
                        {
                            var entitymeta = metadataService.RetrieveEntities(entityLogicalName, service);
                            var unmarkedattributes = Settings[organisationId.ToString()][this.entityLogicalName].UnmarkedAttributes;
                            var sourceAttributesList = new List<ListViewItem>();
                            var attributes = ProcessShowSystemAttributesChecked(entitymeta);

                            attributes = attributes.OrderByDescending(p => p.IsPrimaryId).ThenByDescending(p => p.IsPrimaryName).ThenByDescending(p => p.IsCustomAttribute.Value).ThenBy(p => p.IsLogical.Value).ThenBy(p => p.LogicalName).ToArray();

                            ProcessAllAttributeMetadata(unmarkedattributes, sourceAttributesList, attributes);

                            e.Result = sourceAttributesList;
                        };
                        bwFill.RunWorkerCompleted += (sender, e) =>
                        {
                            AsyncRunnerCompleteAttributeOperation(e.Result as List<ListViewItem>, e.Error);// e);
                        };
                        bwFill.RunWorkerAsync();
                    }
                }
            }
        }

        private void ProcessAllAttributeMetadata(List<string> unmarkedattributes, List<ListViewItem> sourceAttributesList, AttributeMetadata[] attributes)
        {
            foreach (AttributeMetadata attribute in attributes)
            {
                var name = attribute.DisplayName.UserLocalizedLabel == null ? string.Empty : attribute.DisplayName.UserLocalizedLabel.Label;
                var typename = attribute.AttributeTypeName == null ? string.Empty : attribute.AttributeTypeName.Value;
                var item = new ListViewItem(name);
                AddAttribute(attribute, item, typename);
                InvalidUpdate(attribute, item);
                item.Checked = unmarkedattributes.Contains(attribute.LogicalName);
                UpdateCheckBoxesAttribute(attribute, item);
                sourceAttributesList.Add(item);
            }
        }

        private AttributeMetadata[] ProcessShowSystemAttributesChecked(EntityMetadata entitymeta)
        {
            var attributes = entitymeta.Attributes.ToArray();

            if (!cbShowSystemAttributes.Checked)
            {
                attributes = attributes.Where(p => p.IsLogical != null
                && !p.IsLogical.Value
                && p.IsValidForRead != null
                && p.IsValidForRead.Value
                && ((p.IsValidForCreate != null && p.IsValidForCreate.Value) || (p.IsValidForUpdate != null && p.IsValidForUpdate.Value))).ToArray();
            }

            return attributes;
        }

        private void UpdateCheckBoxesAttribute(AttributeMetadata attribute, ListViewItem item)
        {
            if (entityAttributes.ContainsKey(entityLogicalName))
            {
                foreach (string attr in entityAttributes[entityLogicalName])
                {
                    item.Checked |= attr.Equals(attribute.LogicalName, StringComparison.InvariantCulture);
                }
            }
        }

        private void InvalidUpdate(AttributeMetadata attribute, ListViewItem item)
        {
            item.ToolTipText = string.Empty;

            if (attribute.IsValidForCreate != null && !attribute.IsValidForCreate.Value)
            {
                item.ForeColor = Color.Gray;
                item.ToolTipText = "Not createable, ";
            }

            CheckForPrimaryIdAndName(attribute, item);

            CheckForVirtual(attribute, item);

            if (attribute.IsLogical != null && attribute.IsLogical.Value)
            {
                item.ForeColor = Color.Red;
                item.ToolTipText += "Logical attribute, ";
            }

            if (attribute.IsValidForCreate != null && !attribute.IsValidForCreate.Value &&
                attribute.IsValidForUpdate != null && !attribute.IsValidForUpdate.Value)
            {
                item.ForeColor = Color.Red;
            }

            if (attribute.IsValidForRead != null && !attribute.IsValidForRead.Value)
            {
                item.ForeColor = Color.Red;
                item.ToolTipText += "Not readable, ";
            }

            if (attribute.Description != null && attribute.Description.LocalizedLabels.Count > 0)
            {
                item.ToolTipText += attribute.Description.LocalizedLabels.First().Label;
            }

            if (!string.IsNullOrWhiteSpace(attribute.DeprecatedVersion))
            {
                item.ForeColor = Color.Red;
                item.ToolTipText += "DeprecatedVersion:" + attribute.DeprecatedVersion;
            }

            item.SubItems.Add(item.ToolTipText);
        }

        private static void CheckForVirtual(AttributeMetadata attribute, ListViewItem item)
        {
            if (attribute.AttributeType == AttributeTypeCode.Virtual || attribute.AttributeType == AttributeTypeCode.ManagedProperty)
            {
                item.ForeColor = Color.Red;
                item.ToolTipText += "Virtual or managed property, ";
            }
        }

        private static void CheckForPrimaryIdAndName(AttributeMetadata attribute, ListViewItem item)
        {
            if (attribute.IsValidForUpdate != null && !attribute.IsValidForUpdate.Value)
            {
                item.ForeColor = Color.Gray;
                item.ToolTipText += "Not updateable, ";
            }

            if (attribute.IsCustomAttribute != null && attribute.IsCustomAttribute.Value)
            {
                item.ForeColor = Color.DarkGreen;
            }

            if (attribute.IsPrimaryId != null && attribute.IsPrimaryId.Value)
            {
                item.ForeColor = Color.DarkBlue;
            }

            if (attribute.IsPrimaryName != null && attribute.IsPrimaryName.Value)
            {
                item.ForeColor = Color.DarkBlue;
            }
        }

        private void AddAttribute(AttributeMetadata attribute, ListViewItem item, string typename)
        {
            item.Tag = attribute;
            item.SubItems.Add(attribute.LogicalName);
            item.SubItems.Add(typename.EndsWith("Type", StringComparison.Ordinal) ? typename.Substring(0, typename.LastIndexOf("Type", StringComparison.Ordinal)) : typename);
        }

        private void InitFilter()
        {
            string filter = null;

            if (lvEntities.SelectedItems.Count > 0)
            {
                var entityitem = lvEntities.SelectedItems[0];

                if (entityitem != null && entityitem.Tag != null)
                {
                    var entity = (EntityMetadata)entityitem.Tag;
                    filter = Settings[organisationId.ToString()][entity.LogicalName].Filter;
                }
            }

            tsbtFilters.ForeColor = string.IsNullOrEmpty(filter) ? Color.Black : Color.Blue;
        }

        private void PopulateEntities()
        {
            if (!workingstate)
            {
                ClearAllListViews();
                ManageWorkingState(true);

                informationPanel = InformationPanel.GetInformationPanel(this, "Loading entities...", 340, 150);

                using (var bwFill = new BackgroundWorker())
                {
                    bwFill.DoWork += (sender, e) =>
                    {
                        e.Result = RetrieveSourceEntitiesList(cbShowSystemAttributes.Checked, OrganizationService, MetadataService);
                    };
                    bwFill.RunWorkerCompleted += (sender, e) =>
                    {
                        informationPanel.Dispose();
                        PopulateEntitiesListView(e.Result as List<ListViewItem>, e.Error);
                    };
                    bwFill.RunWorkerAsync();
                }
            }
        }

        private void IsInvalidForCustomization(EntityMetadata entity, ListViewItem item)
        {
            if (entity.IsCustomEntity.Value)
            {
                item.ForeColor = Color.DarkGreen;
            }

            if (entity.IsIntersect.Value)
            {
                item.ForeColor = Color.Red;
                item.ToolTipText = "Intersect Entity, ";
            }

            if (entity.IsLogicalEntity.Value)
            {
                item.ForeColor = Color.Red;
                item.ToolTipText = "Logical Entity";
            }
        }

        private void UpdateCheckBoxesEntities(EntityMetadata entity, ListViewItem item)
        {
            item.Checked |= entityAttributes.ContainsKey(entity.LogicalName);
        }

        private void ManageWorkingState(bool working)
        {
            workingstate = working;
            gbEntities.Enabled = !working;
            gbAttributes.Enabled = !working;
            gbRelationship.Enabled = !working;
            Cursor = working ? Cursors.WaitCursor : Cursors.Default;
        }

        private void TabControlSelected(object sender, TabControlEventArgs e)
        {
            RefreshEntities();
        }

        private void tsbtMappings_Click(object sender, EventArgs e)
        {
            if (lvEntities.Items.Count != 0 && lvEntities.SelectedItems.Count > 0)
            {
                if (!string.IsNullOrEmpty(entityLogicalName))
                {
                    if (mapping.ContainsKey(entityLogicalName))
                    {
                        MappingIfContainsKey();
                    }
                    else
                    {
                        MappingIfKeyDoesNotExist();
                    }
                }
            }
            else
            {
                //MessageBox.Show("Entity is not selected");
                FeedbackManager.DisplayFeedback("Entity is not selected");
            }
        }

        private void MappingIfKeyDoesNotExist()
        {
            var mappingReference = new List<Item<EntityReference, EntityReference>>();
            using (var mappingDialog = new MappingList(mappingReference)
            {
                StartPosition = FormStartPosition.CenterParent
            })
            {
                mappingDialog.ShowDialog(ParentForm);

                var mapList = mappingDialog.GetMappingList(entityLogicalName);
                var guidMapList = mappingDialog.GetGuidMappingList();

                if (mapList.Count > 0)
                {
                    mapping.Add(entityLogicalName, mapList);
                    mapper.Add(entityLogicalName, guidMapList);
                }
            }

            InitMappings();
            Settings[organisationId.ToString()].Mappings.Clear();
        }

        private void MappingIfContainsKey()
        {
            using (var mappingDialog = new MappingList(mapping[entityLogicalName])
            {
                StartPosition = FormStartPosition.CenterParent
            })
            {
                mappingDialog.ShowDialog(ParentForm);

                var mapList = mappingDialog.GetMappingList(entityLogicalName);
                var guidMapList = mappingDialog.GetGuidMappingList();

                if (mapList.Count == 0)
                {
                    mapping.Remove(entityLogicalName);
                    mapper.Remove(entityLogicalName);
                }
                else
                {
                    mapping[entityLogicalName] = mapList;
                    mapper[entityLogicalName] = guidMapList;
                }
            }

            InitMappings();
        }

        private void OpenMappingForm(IMetadataService metadataService)
        {
            //using (var mappingDialog = new MappingListLookup(lookupMaping, CrmServiceClient.OrganizationWebProxyClient != null ? (IOrganizationService)CrmServiceClient.OrganizationWebProxyClient : (IOrganizationService)CrmServiceClient.OrganizationServiceProxy, cachedMetadata, entityLogicalName, metadataService)
            using (var mappingDialog = new MappingListLookup(lookupMaping, OrganizationService, cachedMetadata, entityLogicalName, metadataService)
            {
                StartPosition = FormStartPosition.CenterParent
            })
            {
                mappingDialog.ShowDialog(ParentForm);
                mappingDialog.RefreshMappingList();
                InitMappings();
                Settings[organisationId.ToString()].Mappings.Clear();
            }
        }

        private void InitMappings()
        {
            tsbtMappings.ForeColor = Settings[organisationId.ToString()].Mappings.Count == 0 ? Color.Black : Color.Blue;
        }

        private void TabStripFiltersClick(object sender, EventArgs e)
        {
            if (lvEntities.Items.Count != 0 && lvEntities.SelectedItems.Count > 0)
            {
                if (filterQuery.ContainsKey(entityLogicalName))
                {
                    FilterIfContainsKey();
                }
                else
                {
                    FilterIfKeyDoesNotExist();
                }
            }
            else
            {
                MessageBox.Show("Entity list is empty");
            }
        }

        private void FilterIfKeyDoesNotExist()
        {
            using (var filterDialog = new FilterEditor(null)
            {
                StartPosition = FormStartPosition.CenterParent
            })
            {
                filterDialog.ShowDialog(ParentForm);

                if (!string.IsNullOrWhiteSpace(filterDialog.QueryString))
                {
                    filterQuery[entityLogicalName] = filterDialog.QueryString;
                }
            }
        }

        private void FilterIfContainsKey()
        {
            using (var filterDialog = new FilterEditor(filterQuery[entityLogicalName])
            {
                StartPosition = FormStartPosition.CenterParent
            })
            {
                filterDialog.ShowDialog(ParentForm);

                if (string.IsNullOrWhiteSpace(filterDialog.QueryString))
                {
                    filterQuery.Remove(entityLogicalName);
                }
                else
                {
                    filterQuery[entityLogicalName] = filterDialog.QueryString;
                }
            }
        }

        private void CheckListAllAttributesCheckedChanged(object sender, EventArgs e)
        {
            lvAttributes.Items.OfType<ListViewItem>().ToList().ForEach(item => item.Checked = chkAllAttributes.Checked);
        }

        private void ListViewAttributesColumnClick(object sender, ColumnClickEventArgs e)
        {
            var columnNumber = e.Column;
            if (columnNumber != 3)
            {
                SetListViewSorting(lvAttributes, e.Column);
            }
        }

        private void ListViewEntitiesColumnClick(object sender, ColumnClickEventArgs e)
        {
            SetListViewSorting(lvEntities, e.Column);
        }

        private void SetListViewSorting(ListView listview, int column)
        {
            var setting = Settings[organisationId.ToString()].Sortcolumns.FirstOrDefault(s => s.Key == listview.Name);
            if (setting == null)
            {
                setting = new Item<string, int>(listview.Name, -1);
                Settings[organisationId.ToString()].Sortcolumns.Add(setting);
            }

            if (setting.Value != column)
            {
                setting.Value = column;
                listview.Sorting = SortOrder.Ascending;
            }
            else
            {
                if (listview.Sorting == SortOrder.Ascending)
                {
                    listview.Sorting = SortOrder.Descending;
                }
                else
                {
                    listview.Sorting = SortOrder.Ascending;
                }
            }

            listview.ListViewItemSorter = new ListViewItemComparer(column, listview.Sorting);
        }

        private void CheckListAllEntitiesCheckedChanged(object sender, EventArgs e)
        {
            lvEntities.Items.OfType<ListViewItem>().ToList().ForEach(item => item.Checked = chkAllEntities.Checked);
        }

        private void ListViewAttributesItemCheck(object sender, ItemCheckEventArgs e)
        {
            var indexNumber = e.Index;
            var logicalName = lvAttributes.Items[indexNumber].SubItems[1].Text;
            if (entityAttributes.ContainsKey(entityLogicalName))
            {
                StoreAttriubteIfKeyExists(logicalName, e);
            }
            else
            {
                StoreAttributeIfRequiresKey(logicalName, e);
            }
        }

        private void StoreAttributeIfRequiresKey(string logicalName, ItemCheckEventArgs e)
        {
            var attributeSet = new HashSet<string>();
            if (e.CurrentValue.ToString() != "Checked")
            {
                attributeSet.Add(logicalName);
            }

            entityAttributes.Add(entityLogicalName, attributeSet);
        }

        private void StoreAttriubteIfKeyExists(string logicalName, ItemCheckEventArgs e)
        {
            HashSet<string> attributeSet = entityAttributes[entityLogicalName];

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

        private void ListViewEntitiesItemCheck(object sender, ItemCheckEventArgs e)
        {
            var indexNumber = e.Index;
            var logicalName = lvEntities.Items[indexNumber].SubItems[1].Text;
            if (e.CurrentValue.ToString() == "Checked")
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

        private bool AreCrmEntityFieldsSelected(IMetadataService metadataService)
        {
            var fieldsSelected = false;
            if (checkedEntity.Count > 0)
            {
                var crmEntityList = new List<CrmEntity>();

                foreach (var item in checkedEntity)
                {
                    var crmEntity = new CrmEntity();
                    var sourceList = metadataService.RetrieveEntities(item, OrganizationService);
                    StoreCrmEntityData(crmEntity, sourceList, crmEntityList);

                    if (crmEntity.CrmFields != null && crmEntity.CrmFields.Any())
                    {
                        fieldsSelected = true;
                    }
                    else
                    {
                        fieldsSelected = false;
                        break;
                    }
                }
            }

            return fieldsSelected;
        }

        private void TbSaveSchemaClick(object sender, EventArgs e)
        {
            if (AreCrmEntityFieldsSelected(MetadataService))
            {
                CollectCrmEntityFields(MetadataService);
                GenerateXMLFile();
                crmSchemaConfiguration.Entities.Clear();
            }
            else
            {
                MessageBox.Show("Please select at least one attribute for each selected entity!");
            }
        }

        private void CollectCrmEntityFields(IMetadataService metadataService)
        {
            if (checkedEntity.Count > 0)
            {
                var crmEntityList = new List<CrmEntity>();

                foreach (var item in checkedEntity)
                {
                    var crmEntity = new CrmEntity();
                    var sourceList = metadataService.RetrieveEntities(item, OrganizationService);
                    StoreCrmEntityData(crmEntity, sourceList, crmEntityList);
                }

                crmSchemaConfiguration.Entities.Clear();
                crmSchemaConfiguration.Entities.AddRange(crmEntityList);
            }
        }

        private void StoreCrmEntityData(CrmEntity crmEntity, EntityMetadata sourceList, List<CrmEntity> crmEntityList)
        {
            crmEntity.Name = sourceList.LogicalName;
            crmEntity.DisplayName = sourceList.DisplayName.UserLocalizedLabel == null ? string.Empty : sourceList.DisplayName.UserLocalizedLabel.Label;
            crmEntity.EntityCode = sourceList.ObjectTypeCode.ToString();
            crmEntity.PrimaryIdField = sourceList.PrimaryIdAttribute;
            crmEntity.PrimaryNameField = sourceList.PrimaryNameAttribute;
            CollectCrmEntityRelationShip(sourceList, crmEntity);
            CollectCrmAttributesFields(sourceList, crmEntity);
            crmEntityList.Add(crmEntity);
        }

        private void CollectCrmEntityRelationShip(EntityMetadata sourceList, CrmEntity crmEntity)
        {
            var manyToManyRelationship = sourceList.ManyToManyRelationships;
            var relationshipList = new List<CrmRelationship>();
            foreach (var relationship in manyToManyRelationship)
            {
                if (entityRelationships.ContainsKey(sourceList.LogicalName))
                {
                    foreach (var relationshipName in entityRelationships[sourceList.LogicalName])
                    {
                        if (relationshipName == relationship.IntersectEntityName)
                        {
                            StoreCrmEntityRelationShipData(crmEntity, relationship, relationshipList);
                        }
                    }
                }
            }

            crmEntity.CrmRelationships.Clear();
            crmEntity.CrmRelationships.AddRange(relationshipList);
        }

        private void StoreCrmEntityRelationShipData(CrmEntity crmEntity, ManyToManyRelationshipMetadata relationship, List<CrmRelationship> relationshipList)
        {
            CrmRelationship crmRelationShip = new CrmRelationship
            {
                RelatedEntityName = relationship.IntersectEntityName,
                ManyToMany = true,
                IsReflexive = relationship.IsCustomizable.Value,
                TargetEntityPrimaryKey = crmEntity.PrimaryIdField == relationship.Entity2IntersectAttribute ? relationship.Entity1IntersectAttribute : relationship.Entity2IntersectAttribute,
                TargetEntityName = crmEntity.Name == relationship.Entity2LogicalName ? relationship.Entity1LogicalName : relationship.Entity2LogicalName,
                RelationshipName = relationship.IntersectEntityName
            };
            relationshipList.Add(crmRelationShip);
        }

        private void CollectCrmAttributesFields(EntityMetadata sourceList, CrmEntity crmEntity)
        {
            if (entityAttributes != null)
            {
                var attributes = sourceList.Attributes.ToArray();

                var primaryAttribute = sourceList.PrimaryIdAttribute;
                if (entityAttributes.ContainsKey(sourceList.LogicalName))
                {
                    var crmFieldList = new List<CrmField>();
                    foreach (AttributeMetadata attribute in attributes)
                    {
                        StoreAttributeMetadata(attribute, sourceList, primaryAttribute, crmFieldList);
                    }

                    crmEntity.CrmFields.Clear();
                    crmEntity.CrmFields.AddRange(crmFieldList);
                }
            }
        }

        private void StoreAttributeMetadata(AttributeMetadata attribute, EntityMetadata sourceList, string primaryAttribute, List<CrmField> crmFieldList)
        {
            CrmField crmField = new CrmField();
            foreach (var attributeLogicalName in entityAttributes[sourceList.LogicalName])
            {
                if (attribute.LogicalName.Equals(attributeLogicalName, StringComparison.InvariantCulture))
                {
                    crmField.DisplayName = attribute.DisplayName.UserLocalizedLabel == null ? string.Empty : attribute.DisplayName.UserLocalizedLabel.Label;
                    crmField.FieldName = attribute.LogicalName;
                    attributeMapping.AttributeMetadataType = attribute.AttributeTypeName.Value.ToString(CultureInfo.InvariantCulture);
                    attributeMapping.GetMapping();
                    crmField.FieldType = attributeMapping.AttributeMetadataTypeResult;
                    StoreLookUpAttribute(attribute, crmField);
                    StoreAttributePrimaryKey(primaryAttribute, crmField);
                    crmFieldList.Add(crmField);
                }
            }
        }

        private void StoreAttributePrimaryKey(string primaryAttribute, CrmField crmField)
        {
            if (crmField.FieldName.Equals(primaryAttribute, StringComparison.InvariantCulture))
            {
                crmField.PrimaryKey = true;
            }
        }

        private void StoreLookUpAttribute(AttributeMetadata attribute, CrmField crmField)
        {
            if (crmField.FieldType.Equals("entityreference", StringComparison.InvariantCulture))
            {
                try
                {
                    if ((LookupAttributeMetadata)attribute != null)
                    {
                        var lookUpAttribute = (LookupAttributeMetadata)attribute;
                        if (lookUpAttribute.Targets.Any())
                        {
                            crmField.LookupType = lookUpAttribute.Targets[0];
                        }
                    }
                }
                catch (InvalidCastException exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        private void GenerateXMLFile()
        {
            if (!string.IsNullOrWhiteSpace(tbSchemaPath.Text))
            {
                crmSchemaConfiguration.SaveToFile(tbSchemaPath.Text);
            }
        }

        private void ButtonSchemaFolderPathClick(object sender, EventArgs e)
        {
            using (var fileDialog = new SaveFileDialog
            {
                Filter = "XML Files|*.xml",
                OverwritePrompt = false
            })
            {
                var result = fileDialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    tbSchemaPath.Text = fileDialog.FileName.ToString(CultureInfo.InvariantCulture);

                    if (File.Exists(tbSchemaPath.Text))
                    {
                        LoadSchemaFile(tbSchemaPath.Text);
                    }
                }
                else if (result == DialogResult.Cancel)
                {
                    tbSchemaPath.Text = null;
                }
            }
        }

        private void ClearAllListViews()
        {
            lvEntities.Items.Clear();
            lvAttributes.Items.Clear();
            lvRelationship.Items.Clear();
        }

        private void StoreEntityData(CrmEntity[] crmEntity)
        {
            entityAttributes.Clear();
            entityRelationships.Clear();
            foreach (var entities in crmEntity)
            {
                var logicalName = entities.Name;
                HashSet<string> attributeSet = new HashSet<string>();
                HashSet<string> relationShipSet = new HashSet<string>();
                if (entities.CrmFields != null)
                {
                    foreach (var attributes in entities.CrmFields)
                    {
                        attributeSet.Add(attributes.FieldName);
                    }
                }

                if (entities.CrmRelationships != null)
                {
                    foreach (var relationship in entities.CrmRelationships)
                    {
                        relationShipSet.Add(relationship.RelationshipName);
                    }
                }

                entityAttributes.Add(logicalName, attributeSet);
                entityRelationships.Add(logicalName, relationShipSet);
            }
        }

        private void CheckBoxAllRelationshipsCheckedChanged(object sender, EventArgs e)
        {
            lvRelationship.Items.OfType<ListViewItem>().ToList().ForEach(item => item.Checked = chkAllRelationships.Checked);
        }

        private void ListViewRelationshipItemCheck(object sender, ItemCheckEventArgs e)
        {
            var indexNumber = e.Index;
            var logicalName = lvRelationship.Items[indexNumber].SubItems[1].Text;

            if (entityRelationships.ContainsKey(entityLogicalName))
            {
                StoreRelationshipIfKeyExists(logicalName, e);
            }
            else
            {
                StoreRelationshipIfRequiresKey(logicalName, e);
            }
        }

        private void StoreRelationshipIfRequiresKey(string logicalName, ItemCheckEventArgs e)
        {
            HashSet<string> relationshipSet = new HashSet<string>();
            if (e.CurrentValue.ToString() != "Checked")
            {
                relationshipSet.Add(logicalName);
            }

            entityRelationships.Add(entityLogicalName, relationshipSet);
        }

        private void StoreRelationshipIfKeyExists(string logicalName, ItemCheckEventArgs e)
        {
            HashSet<string> relationshipSet = entityRelationships[entityLogicalName];

            if (e.CurrentValue.ToString() == "Checked")
            {
                if (relationshipSet.Contains(logicalName))
                {
                    relationshipSet.Remove(logicalName);
                }
            }
            else
            {
                relationshipSet.Add(logicalName);
            }
        }

        private void ButtonImportConfigPathClick(object sender, EventArgs e)
        {
            using (var fileDialog = new SaveFileDialog
            {
                Filter = "JSON Files|*.json",
                OverwritePrompt = false
            })
            {
                var result = fileDialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    tbImportConfig.Text = fileDialog.FileName.ToString(CultureInfo.InvariantCulture);

                    if (File.Exists(tbImportConfig.Text))
                    {
                        LoadImportConfigFile();
                    }
                }
                else if (result == DialogResult.Cancel)
                {
                    tbImportConfig.Text = null;
                }
            }
        }

        private void LoadImportConfigFile()
        {
            if (!string.IsNullOrWhiteSpace(tbImportConfig.Text))
            {
                try
                {
                    var configImport = CrmImportConfig.GetConfiguration(tbImportConfig.Text);
                    if (configImport.MigrationConfig == null)
                    {
                        MessageBox.Show("Invalid Import Config File");
                        tbImportConfig.Text = "";
                        return;
                    }

                    mapper = configImport.MigrationConfig.Mappings;
                    DataConversion();

                    MessageBox.Show("Guid Id Mappings loaded from Import Config File");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Load Correct Import Config file, error:" + ex.Message);
                }
            }
        }

        private void btExportConfigPath_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog fileDialog = new SaveFileDialog
            {
                Filter = "JSON Files|*.json",
                OverwritePrompt = false
            })
            {
                var result = fileDialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    tbExportConfig.Text = fileDialog.FileName.ToString(CultureInfo.InvariantCulture);

                    if (File.Exists(tbExportConfig.Text))
                    {
                        LoadExportConfigFile(tbExportConfig.Text);
                    }
                }
                else if (result == DialogResult.Cancel)
                {
                    tbExportConfig.Text = null;
                }
            }
        }

        //private void ToolBarLoadSchemaClick(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrWhiteSpace(tbSchemaPath.Text))
        //    {
        //        try
        //        {
        //            CrmSchemaConfiguration crmSchema = CrmSchemaConfiguration.ReadFromFile(tbSchemaPath.Text);
        //            StoreEntityData(crmSchema.Entities?.ToArray());
        //            ClearAllListViews();
        //            PopulateEntities();
        //        }
        //        catch (Exception ex)
        //        {
        //            //MessageBox.Show("Load Correct Schema file, error:" + ex.Message);
        //            FeedbackManager.DisplayFeedback($"Load Correct Schema file, error:{ex.Message}");
        //        }
        //    }
        //}

        private void ToolBarSaveMappingsClick(object sender, EventArgs e)
        {
            GenerateImportConfigFile();
        }

        private void GenerateImportConfigFile()
        {
            try
            {
                CrmImportConfig migration = new CrmImportConfig()
                {
                    IgnoreStatuses = true,
                    IgnoreSystemFields = true,
                    SaveBatchSize = 1000,
                    JsonFolderPath = "ExtractedData"
                };

                if (File.Exists(tbImportConfig.Text))
                {
                    migration = CrmImportConfig.GetConfiguration(tbImportConfig.Text);
                }

                if (migration.MigrationConfig == null)
                {
                    migration.MigrationConfig = new MappingConfiguration();
                }

                if (mapping != null)
                {
                    migration.MigrationConfig.Mappings.Clear();
                    migration.MigrationConfig.Mappings.AddRange(mapper);

                    if (File.Exists(tbImportConfig.Text))
                    {
                        File.Delete(tbImportConfig.Text);
                    }

                    migration.SaveConfiguration(tbImportConfig.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Saving Import Config file, Error:" + ex.Message);
            }
        }

        private void ToolBarSaveFiltersClick(object sender, EventArgs e)
        {
            GenerateExportConfigFile();
        }

        private void GenerateExportConfigFile()
        {
            CrmExporterConfig config = new CrmExporterConfig()
            {
                JsonFolderPath = "ExtractedData",
            };

            if (File.Exists(tbExportConfig.Text))
            {
                config = CrmExporterConfig.GetConfiguration(tbExportConfig.Text);
            }

            config.CrmMigrationToolSchemaFilters.Clear();
            config.CrmMigrationToolSchemaFilters.AddRange(filterQuery);

            if (!string.IsNullOrWhiteSpace(tbSchemaPath.Text))
            {
                config.CrmMigrationToolSchemaPaths.Clear();
                config.CrmMigrationToolSchemaPaths.Add(tbSchemaPath.Text);
            }

            if (lookupMaping.Count > 0)
            {
                config.LookupMapping.Clear();
                config.LookupMapping.AddRange(lookupMaping);
            }

            if (File.Exists(tbExportConfig.Text))
            {
                File.Delete(tbExportConfig.Text);
            }

            config.SaveConfiguration(tbExportConfig.Text);
        }

        private void DataConversion()
        {
            mapping.Clear();
            foreach (var mappings in mapper)
            {
                var list = new List<Item<EntityReference, EntityReference>>();

                foreach (var values in mappings.Value)
                {
                    list.Add(new Item<EntityReference, EntityReference>(new EntityReference(mappings.Key, values.Key), new EntityReference(mappings.Key, values.Value)));
                }

                mapping.Add(mappings.Key, list);
            }
        }

        private void ToolBarLoadMappingsFileClick(object sender, EventArgs e)
        {
            LoadImportConfigFile();
        }

        private void ToolBarLoadSchemaFileClick(object sender, EventArgs e)
        {
            LoadSchemaFile(tbSchemaPath.Text);
        }

        private void ToolBarLoadFiltersFileClick(object sender, EventArgs e)
        {
            LoadExportConfigFile(tbExportConfig.Text);
        }

        private void LoadAllToolStripMenuItemClick(object sender, EventArgs e)
        {
            LoadAllFiles();
        }

        private void LoadAllFiles()
        {
            LoadSchemaFile(tbSchemaPath.Text);
            LoadExportConfigFile(tbExportConfig.Text);
            LoadImportConfigFile();
        }

        private void SaveAllToolStripMenuItemClick(object sender, EventArgs e)
        {
            GenerateImportConfigFile();
            GenerateExportConfigFile();
            CollectCrmEntityFields(MetadataService);
            GenerateXMLFile();
            crmSchemaConfiguration.Entities.Clear();
        }

        private void ToolStripButton1Click(object sender, EventArgs e)
        {
            OpenMappingForm(MetadataService);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            SetMenuVisibility(WizardMode.Schema);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            SetMenuVisibility(WizardMode.Export);
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            SetMenuVisibility(WizardMode.Import);
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            SetMenuVisibility(WizardMode.All);
        }

        private void SetMenuVisibility(WizardMode mode)
        {
            //Import
            tsbtMappings.Enabled = mode == WizardMode.All || mode == WizardMode.Import;
            loadMappingsToolStripMenuItem.Enabled = mode == WizardMode.All || mode == WizardMode.Import;
            saveMappingsToolStripMenuItem.Enabled = mode == WizardMode.All || mode == WizardMode.Import;
            tbImportConfig.Enabled = mode == WizardMode.All || mode == WizardMode.Import;
            btImportConfigPath.Enabled = mode == WizardMode.All || mode == WizardMode.Import;

            //Export
            lookupMappings.Enabled = mode == WizardMode.Export || mode == WizardMode.All;
            tsbtFilters.Enabled = mode == WizardMode.Export || mode == WizardMode.All;
            loadFiltersToolStripMenuItem.Enabled = mode == WizardMode.Export || mode == WizardMode.All;
            saveFiltersToolStripMenuItem.Enabled = mode == WizardMode.Export || mode == WizardMode.All;
            tbExportConfig.Enabled = mode == WizardMode.Export || mode == WizardMode.All;
            btExportConfigPath.Enabled = mode == WizardMode.Export || mode == WizardMode.All;

            //Schema
            loadSchemaToolStripMenuItem.Enabled = mode == WizardMode.Schema || mode == WizardMode.All;
            saveSchemaToolStripMenuItem.Enabled = mode == WizardMode.Schema || mode == WizardMode.All;
            tbSchemaPath.Enabled = mode == WizardMode.Schema || mode == WizardMode.All;
            btSchemaFolderPath.Enabled = mode == WizardMode.Schema || mode == WizardMode.All;

            //All
            loadAllToolStripMenuItem.Enabled = mode == WizardMode.All;
            saveAllToolStripMenuItem.Enabled = mode == WizardMode.All;
        }

        private void ToolStripButtonConnectClick(object sender, EventArgs e)
        {
            if (OnConnectionRequested != null)
            {
                var args = new RequestConnectionEventArgs { ActionName = "SchemaConnection", Control = (MyPluginControl)Parent };
                OnConnectionRequested(this, args);
            }
        }
    }
}