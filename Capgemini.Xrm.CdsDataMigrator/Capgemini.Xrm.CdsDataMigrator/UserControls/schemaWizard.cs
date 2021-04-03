using Capgemini.Xrm.CdsDataMigrator.Services;
using Capgemini.Xrm.DataMigration.Config;
using Capgemini.Xrm.DataMigration.Model;
using Capgemini.Xrm.DataMigration.XrmToolBox.Core;
using Capgemini.Xrm.DataMigration.XrmToolBox.Services;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Forms;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Model;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using MyXrmToolBoxPlugin3;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
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
        private readonly Dictionary<string, string> filterQuery = new Dictionary<string, string>();
        private readonly Dictionary<string, Dictionary<string, List<string>>> lookupMaping = new Dictionary<string, Dictionary<string, List<string>>>();
        private readonly Dictionary<string, Dictionary<Guid, Guid>> mapper = new Dictionary<string, Dictionary<Guid, Guid>>();
        private readonly SchemaWizardDelegate schemaWizardDelegate = new SchemaWizardDelegate();

        private bool workingstate;

        private Panel informationPanel;
        private Guid organisationId = Guid.Empty;

        private string entityLogicalName;
        private List<EntityMetadata> cachedMetadata = null;

        public SchemaWizard()
        {
            InitializeComponent();
        }

        public event EventHandler<RequestConnectionEventArgs> OnConnectionRequested;

        public IOrganizationService OrganizationService { get; set; }

        public IMetadataService MetadataService { get; set; }

        public INotificationService NotificationService { get; set; }

        public IExceptionService ExceptionService { get; set; }

        public Core.Settings Settings { get; set; }

        public void OnConnectionUpdated(Guid ConnectedOrgId, string ConnectedOrgFriendlyName)
        {
            organisationId = ConnectedOrgId;
            toolStripLabelConnection.Text = $"Connected to: {ConnectedOrgFriendlyName}";
            toolStrip2.Enabled = true;
            RefreshEntities(cachedMetadata, workingstate, true);
        }

        public void HandleListViewEntitiesSelectedIndexChanged(Dictionary<string, HashSet<string>> inputEntityRelationships, string inputEntityLogicalName, HashSet<string> inputSelectedEntity, ListView.SelectedListViewItemCollection selectedItems, ServiceParameters serviceParameters)
        {
            ListViewItem listViewSelectedItem = selectedItems.Count > 0 ? selectedItems[0] : null;

            PopulateAttributes(inputEntityLogicalName, listViewSelectedItem, serviceParameters);

            PopulateRelationship(inputEntityLogicalName, inputEntityRelationships, listViewSelectedItem, serviceParameters);
            schemaWizardDelegate.AddSelectedEntities(selectedItems.Count, inputEntityLogicalName, inputSelectedEntity);
        }

        public void PopulateRelationship(string entityLogicalName, Dictionary<string, HashSet<string>> inputEntityRelationships, ListViewItem listViewSelectedItem, ServiceParameters migratorParameters)
        {
            if (!workingstate)
            {
                lvRelationship.Items.Clear();
                InitFilter(listViewSelectedItem);
                if (listViewSelectedItem != null)
                {
                    using (var bwFill = new BackgroundWorker())
                    {
                        bwFill.DoWork += (sender, e) =>
                        {
                            e.Result = schemaWizardDelegate.PopulateRelationshipAction(entityLogicalName, inputEntityRelationships, migratorParameters);
                        };
                        bwFill.RunWorkerCompleted += (sender, e) =>
                        {
                            schemaWizardDelegate.OnPopulateCompletedAction(e, NotificationService, this, lvRelationship);
                            ManageWorkingState(false);
                        };
                        bwFill.RunWorkerAsync();
                    }
                }
            }
        }

        public void LoadSchemaFile(string schemaFilePath, bool working, INotificationService notificationService, Dictionary<string, HashSet<string>> inputEntityAttributes, Dictionary<string, HashSet<string>> inputEntityRelationships)
        {
            if (!string.IsNullOrWhiteSpace(schemaFilePath))
            {
                try
                {
                    var crmSchema = CrmSchemaConfiguration.ReadFromFile(schemaFilePath);
                    StoreEntityData(crmSchema.Entities?.ToArray(), inputEntityAttributes, inputEntityRelationships);
                    ClearAllListViews();
                    PopulateEntities(working);
                }
                catch (Exception ex)
                {
                    notificationService.DisplayFeedback($"Schema File load error, ensure to load correct Schema file, Error: {ex.Message}");
                }
            }
        }

        public void RefreshEntities(List<EntityMetadata> inputCachedMetadata, bool inputWorkingstate, bool isNewConnection = false)
        {
            if (inputCachedMetadata == null || isNewConnection)
            {
                ClearMemory();
                PopulateEntities(inputWorkingstate);
            }
        }

        public void ClearMemory()
        {
            ClearInternalMemory();
            ClearUIMemory();
        }

        public void ManageWorkingState(bool working)
        {
            workingstate = working;
            gbEntities.Enabled = !working;
            gbAttributes.Enabled = !working;
            gbRelationship.Enabled = !working;
            Cursor = working ? Cursors.WaitCursor : Cursors.Default;
        }

        private void TabStripButtonRetrieveEntitiesClick(object sender, EventArgs e)
        {
            ClearMemory();
            PopulateEntities(workingstate);
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
            var migratorParameters = new ServiceParameters(OrganizationService, MetadataService, NotificationService, ExceptionService);

            var entityitem = lvEntities.SelectedItems.Count > 0 ? lvEntities.SelectedItems[0] : null;
            entityLogicalName = schemaWizardDelegate.GetEntityLogicalName(entityitem);
            HandleListViewEntitiesSelectedIndexChanged(entityRelationships, entityLogicalName, selectedEntity, lvEntities.SelectedItems, migratorParameters);
        }

        private void PopulateAttributes(string entityLogicalName, ListViewItem listViewSelectedItem, ServiceParameters serviceParameters)
        {
            if (!workingstate)
            {
                lvAttributes.Items.Clear();
                chkAllAttributes.Checked = true;

                InitFilter(listViewSelectedItem);
                if (listViewSelectedItem != null)
                {
                    using (var bwFill = new BackgroundWorker())
                    {
                        bwFill.DoWork += (sender, e) =>
                        {
                            var unmarkedattributes = Settings[organisationId.ToString()][this.entityLogicalName].UnmarkedAttributes;

                            AttributeMetadata[] attributes = schemaWizardDelegate.GetAttributeList(entityLogicalName, cbShowSystemAttributes.Checked, serviceParameters);

                            e.Result = schemaWizardDelegate.ProcessAllAttributeMetadata(unmarkedattributes, attributes, entityLogicalName, entityAttributes);
                        };
                        bwFill.RunWorkerCompleted += (sender, e) =>
                        {
                            schemaWizardDelegate.OnPopulateCompletedAction(e, NotificationService, this, lvRelationship);
                            ManageWorkingState(false);
                        };
                        bwFill.RunWorkerAsync();
                    }
                }
            }
        }

        private void InitFilter(ListViewItem entityitem)
        {
            string filter = null;

            if (entityitem != null && entityitem.Tag != null)
            {
                var entity = (EntityMetadata)entityitem.Tag;
                filter = Settings[organisationId.ToString()][entity.LogicalName].Filter;
            }

            tsbtFilters.ForeColor = string.IsNullOrEmpty(filter) ? Color.Black : Color.Blue;
        }

        private void PopulateEntities(bool working)
        {
            if (!working)
            {
                ClearAllListViews();
                ManageWorkingState(true);

                informationPanel = InformationPanel.GetInformationPanel(this, "Loading entities...", 340, 150);

                using (var bwFill = new BackgroundWorker())
                {
                    bwFill.DoWork += (sender, e) =>
                    {
                        var serviceParameters = new ServiceParameters(OrganizationService, MetadataService, NotificationService, ExceptionService);

                        e.Result = schemaWizardDelegate.RetrieveSourceEntitiesList(cbShowSystemAttributes.Checked, cachedMetadata, entityAttributes, serviceParameters);
                    };
                    bwFill.RunWorkerCompleted += (sender, e) =>
                    {
                        informationPanel.Dispose();
                        schemaWizardDelegate.PopulateEntitiesListView(e.Result as List<ListViewItem>, e.Error, this, lvEntities, NotificationService);
                        ManageWorkingState(false);
                    };
                    bwFill.RunWorkerAsync();
                }
            }
        }

        private void TabControlSelected(object sender, TabControlEventArgs e)
        {
            toolStrip2.Enabled = true;
            RefreshEntities(cachedMetadata, workingstate);
        }

        private void tsbtMappings_Click(object sender, EventArgs e)
        {
            schemaWizardDelegate.HandleMappingControlItemClick(NotificationService, entityLogicalName, lvEntities.SelectedItems.Count > 0, mapping, mapper, ParentForm);
        }

        private void TabStripFiltersClick(object sender, EventArgs e)
        {
            var currentFilter = filterQuery.ContainsKey(entityLogicalName) ? filterQuery[entityLogicalName] : null;
            using (var filterDialog = new FilterEditor(currentFilter, FormStartPosition.CenterParent))
            {
                schemaWizardDelegate.ProcessFilterQuery(NotificationService, ParentForm, entityLogicalName, lvEntities.SelectedItems.Count > 0, filterQuery, filterDialog);
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
                schemaWizardDelegate.SetListViewSorting(lvAttributes, e.Column, organisationId.ToString(), Settings);
            }
        }

        private void ListViewEntitiesColumnClick(object sender, ColumnClickEventArgs e)
        {
            schemaWizardDelegate.SetListViewSorting(lvEntities, e.Column, organisationId.ToString(), Settings);
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
                schemaWizardDelegate.StoreAttriubteIfKeyExists(logicalName, e, entityAttributes, entityLogicalName);
            }
            else
            {
                schemaWizardDelegate.StoreAttributeIfRequiresKey(logicalName, e, entityAttributes, entityLogicalName);
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

        private void TbSaveSchemaClick(object sender, EventArgs e)
        {
            var serviceParameters = new ServiceParameters(OrganizationService, MetadataService, NotificationService, ExceptionService);

            if (schemaWizardDelegate.AreCrmEntityFieldsSelected(checkedEntity, entityRelationships, entityAttributes, attributeMapping, serviceParameters))
            {
                schemaWizardDelegate.CollectCrmEntityFields(checkedEntity, crmSchemaConfiguration, entityRelationships, entityAttributes, attributeMapping, serviceParameters);
                schemaWizardDelegate.GenerateXMLFile(tbSchemaPath, crmSchemaConfiguration);
                crmSchemaConfiguration.Entities.Clear();
            }
            else
            {
                NotificationService.DisplayFeedback("Please select at least one attribute for each selected entity!");
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
                        LoadSchemaFile(tbSchemaPath.Text, workingstate, NotificationService, entityAttributes, entityRelationships);
                    }
                }
                else if (result == DialogResult.Cancel)
                {
                    tbSchemaPath.Text = null;
                }
            }
        }

        public void ClearAllListViews()
        {
            lvEntities.Items.Clear();
            lvAttributes.Items.Clear();
            lvRelationship.Items.Clear();
        }

        private static void StoreEntityData(CrmEntity[] crmEntity, Dictionary<string, HashSet<string>> inputEntityAttributes, Dictionary<string, HashSet<string>> inputEntityRelationships)
        {
            inputEntityAttributes.Clear();
            inputEntityRelationships.Clear();
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

                inputEntityAttributes.Add(logicalName, attributeSet);
                inputEntityRelationships.Add(logicalName, relationShipSet);
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
                schemaWizardDelegate.StoreRelationshipIfKeyExists(logicalName, e, entityLogicalName, entityRelationships);
            }
            else
            {
                schemaWizardDelegate.StoreRelationshipIfRequiresKey(logicalName, e, entityLogicalName, entityRelationships);
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
                        schemaWizardDelegate.LoadImportConfigFile(NotificationService, tbImportConfig, mapper, mapping);
                    }
                }
                else if (result == DialogResult.Cancel)
                {
                    tbImportConfig.Text = null;
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
                        schemaWizardDelegate.LoadExportConfigFile(NotificationService, tbExportConfig, filterQuery, lookupMaping);
                    }
                }
                else if (result == DialogResult.Cancel)
                {
                    tbExportConfig.Text = null;
                }
            }
        }

        private void ToolBarSaveMappingsClick(object sender, EventArgs e)
        {
            schemaWizardDelegate.GenerateImportConfigFile(NotificationService, tbImportConfig, mapper);
        }

        private void ToolBarSaveFiltersClick(object sender, EventArgs e)
        {
            schemaWizardDelegate.GenerateExportConfigFile(tbExportConfig, tbSchemaPath, filterQuery, lookupMaping);
        }

        private void ToolBarLoadMappingsFileClick(object sender, EventArgs e)
        {
            schemaWizardDelegate.LoadImportConfigFile(NotificationService, tbImportConfig, mapper, mapping);
        }

        private void ToolBarLoadSchemaFileClick(object sender, EventArgs e)
        {
            LoadSchemaFile(tbSchemaPath.Text, workingstate, NotificationService, entityAttributes, entityRelationships);
        }

        private void ToolBarLoadFiltersFileClick(object sender, EventArgs e)
        {
            schemaWizardDelegate.LoadExportConfigFile(NotificationService, tbExportConfig, filterQuery, lookupMaping);
        }

        private void LoadAllToolStripMenuItemClick(object sender, EventArgs e)
        {
            LoadAllFiles(NotificationService, tbSchemaPath, tbExportConfig, tbImportConfig, workingstate, entityAttributes, entityRelationships, filterQuery, lookupMaping, mapper, mapping);
        }

        public void LoadAllFiles(INotificationService feedbackManager, System.Windows.Forms.TextBox schemaPath, System.Windows.Forms.TextBox exportConfig, System.Windows.Forms.TextBox importConfig, bool inputWorkingstate, Dictionary<string, HashSet<string>> inputEntityAttributes, Dictionary<string, HashSet<string>> inputEntityRelationships, Dictionary<string, string> inputFilterQuery, Dictionary<string, Dictionary<string, List<string>>> inputLookupMaping, Dictionary<string, Dictionary<Guid, Guid>> inputMapper, Dictionary<string, List<Item<EntityReference, EntityReference>>> inputMapping)
        {
            LoadSchemaFile(schemaPath.Text, inputWorkingstate, feedbackManager, inputEntityAttributes, inputEntityRelationships);
            schemaWizardDelegate.LoadExportConfigFile(feedbackManager, exportConfig, inputFilterQuery, inputLookupMaping);
            schemaWizardDelegate.LoadImportConfigFile(feedbackManager, importConfig, inputMapper, inputMapping);
        }

        private void SaveAllToolStripMenuItemClick(object sender, EventArgs e)
        {
            schemaWizardDelegate.GenerateImportConfigFile(NotificationService, tbImportConfig, mapper);
            schemaWizardDelegate.GenerateExportConfigFile(tbExportConfig, tbSchemaPath, filterQuery, lookupMaping);

            var serviceParameters = new ServiceParameters(OrganizationService, MetadataService, NotificationService, ExceptionService);
            schemaWizardDelegate.CollectCrmEntityFields(checkedEntity, crmSchemaConfiguration, entityRelationships, entityAttributes, attributeMapping, serviceParameters);
            schemaWizardDelegate.GenerateXMLFile(tbSchemaPath, crmSchemaConfiguration);
            crmSchemaConfiguration.Entities.Clear();
        }

        private void ToolStripButton1Click(object sender, EventArgs e)
        {
            var serviceParameters = new ServiceParameters(OrganizationService, MetadataService, NotificationService, ExceptionService);

            schemaWizardDelegate.OpenMappingForm(serviceParameters, ParentForm, cachedMetadata, lookupMaping, entityLogicalName);

            tsbtMappings.ForeColor = Settings[organisationId.ToString()].Mappings.Count == 0 ? Color.Black : Color.Blue;
            Settings[organisationId.ToString()].Mappings.Clear();
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