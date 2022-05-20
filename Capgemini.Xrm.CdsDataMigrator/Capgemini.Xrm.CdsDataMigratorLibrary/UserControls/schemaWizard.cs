using Capgemini.Xrm.CdsDataMigratorLibrary.Controllers;
using Capgemini.Xrm.CdsDataMigratorLibrary.Core;
using Capgemini.Xrm.CdsDataMigratorLibrary.Exceptions;
using Capgemini.Xrm.CdsDataMigratorLibrary.Forms;
using Capgemini.Xrm.CdsDataMigratorLibrary.Models;
using Capgemini.Xrm.CdsDataMigratorLibrary.Services;
using Capgemini.Xrm.DataMigration.Config;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Capgemini.Xrm.CdsDataMigratorLibrary;
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
        private readonly List<EntityMetadata> cachedMetadata = new List<EntityMetadata>();

        private bool workingstate;
        private Panel informationPanel;
        private Guid organisationId = Guid.Empty;
        private string entityLogicalName;

        public SchemaWizard()
        {
            InitializeComponent();
            SetMenuVisibility(WizardMode.All);
        }

        public event EventHandler<RequestConnectionEventArgs> OnConnectionRequested;

        public IOrganizationService OrganizationService { get; set; }

        public IMetadataService MetadataService { get; set; }

        public INotificationService NotificationService { get; set; }

        public IExceptionService ExceptionService { get; set; }

        public Settings Settings { get; set; }

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
            var controller = new EntityController();
            controller.AddSelectedEntities(selectedItems.Count, inputEntityLogicalName, inputSelectedEntity);
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
                            var controller = new RelationshipController();
                            e.Result = controller.PopulateRelationshipAction(entityLogicalName, inputEntityRelationships, migratorParameters);
                        };
                        bwFill.RunWorkerCompleted += (sender, e) =>
                        {
                            var controller = new ListController();
                            controller.OnPopulateCompletedAction(e, NotificationService, this, lvRelationship, cbShowSystemAttributes.Checked);
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
                    var controller = new EntityController();
                    controller.StoreEntityData(crmSchema.Entities?.ToArray(), inputEntityAttributes, inputEntityRelationships);
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
            if (inputCachedMetadata.Count == 0 || isNewConnection)
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

        public void InitFilter(ListViewItem entityitem)
        {
            string filter = null;

            if (entityitem != null && entityitem.Tag != null)
            {
                var entity = (EntityMetadata)entityitem.Tag;
                filter = Settings[organisationId.ToString()][entity.LogicalName].Filter;
            }

            tsbtFilters.ForeColor = string.IsNullOrEmpty(filter) ? Color.Black : Color.Blue;
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
            var controller = new EntityController();
            entityLogicalName = controller.GetEntityLogicalName(entityitem);
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
                            var controller = new AttributeController();
                            var attributes = controller.GetAttributeList(entityLogicalName, cbShowSystemAttributes.Checked, serviceParameters);

                            e.Result = controller.ProcessAllAttributeMetadata(unmarkedattributes, attributes, entityLogicalName, entityAttributes);
                        };
                        bwFill.RunWorkerCompleted += (sender, e) =>
                        {
                            var controller = new ListController();
                            controller.OnPopulateCompletedAction(e, NotificationService, this, lvAttributes, cbShowSystemAttributes.Checked);
                            ManageWorkingState(false);
                        };
                        bwFill.RunWorkerAsync();
                    }
                }
            }
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
                        var controller = new EntityController();
                        e.Result = controller.RetrieveSourceEntitiesList(cbShowSystemAttributes.Checked, cachedMetadata, entityAttributes, serviceParameters);
                    };
                    bwFill.RunWorkerCompleted += (sender, e) =>
                    {
                        informationPanel.Dispose();
                        var controller = new EntityController();
                        controller.PopulateEntitiesListView(e.Result as List<ListViewItem>, e.Error, this, lvEntities, NotificationService);
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

        private void TabStripButtonMappingsClick(object sender, EventArgs e)
        {
            var controller = new ListController();
            controller.HandleMappingControlItemClick(NotificationService, entityLogicalName, lvEntities.SelectedItems.Count > 0, mapping, mapper, ParentForm);
        }

        private void TabStripFiltersClick(object sender, EventArgs e)
        {
            var currentFilter = filterQuery.ContainsKey(entityLogicalName) ? filterQuery[entityLogicalName] : null;
            using (var filterDialog = new FilterEditor(currentFilter, FormStartPosition.CenterParent))
            {
                var controller = new ListController();
                controller.ProcessFilterQuery(NotificationService, ParentForm, entityLogicalName, lvEntities.SelectedItems.Count > 0, filterQuery, filterDialog);
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
                var controller = new ListController();
                controller.SetListViewSorting(lvAttributes, e.Column, organisationId.ToString(), Settings);
            }
        }

        private void ListViewEntitiesColumnClick(object sender, ColumnClickEventArgs e)
        {
            var controller = new ListController();
            controller.SetListViewSorting(lvEntities, e.Column, organisationId.ToString(), Settings);
        }

        private void CheckListAllEntitiesCheckedChanged(object sender, EventArgs e)
        {
            lvEntities.Items.OfType<ListViewItem>().ToList().ForEach(item => item.Checked = chkAllEntities.Checked);
        }

        private void ListViewAttributesItemCheck(object sender, ItemCheckEventArgs e)
        {
            var indexNumber = e.Index;
            var logicalName = lvAttributes.Items[indexNumber].SubItems[1].Text;
            var controller = new AttributeController();

            if (entityAttributes.ContainsKey(entityLogicalName))
            {
                controller.StoreAttriubteIfKeyExists(logicalName, e, entityAttributes, entityLogicalName);
            }
            else
            {
                controller.StoreAttributeIfRequiresKey(logicalName, e, entityAttributes, entityLogicalName);
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
            var controller = new SchemaController();

            controller.SaveSchema(serviceParameters, checkedEntity, entityRelationships, entityAttributes, attributeMapping, crmSchemaConfiguration, tbSchemaPath);
        }

        private void ButtonSchemaFolderPathClick(object sender, EventArgs e)
        {
            using (var fileDialog = new SaveFileDialog
            {
                Filter = "XML Files|*.xml",
                OverwritePrompt = false
            })
            {
                var dialogResult = fileDialog.ShowDialog();
                var controller = new SchemaController();
                var collectionParameters = new CollectionParameters(entityAttributes, entityRelationships, null, null, null, null);

                controller.SchemaFolderPathAction(NotificationService, tbSchemaPath, workingstate, collectionParameters  /*entityAttributes, entityRelationships*/, dialogResult, fileDialog, LoadSchemaFile);
            }
        }

        public void ClearAllListViews()
        {
            lvEntities.Items.Clear();
            lvAttributes.Items.Clear();
            lvRelationship.Items.Clear();
        }

        private void CheckBoxAllRelationshipsCheckedChanged(object sender, EventArgs e)
        {
            lvRelationship.Items.OfType<ListViewItem>().ToList().ForEach(item => item.Checked = chkAllRelationships.Checked);
        }

        private void ListViewRelationshipItemCheck(object sender, ItemCheckEventArgs e)
        {
            var indexNumber = e.Index;
            var logicalName = lvRelationship.Items[indexNumber].SubItems[1].Text;
            var controller = new RelationshipController();

            if (entityRelationships.ContainsKey(entityLogicalName))
            {
                controller.StoreRelationshipIfKeyExists(logicalName, e, entityLogicalName, entityRelationships);
            }
            else
            {
                controller.StoreRelationshipIfRequiresKey(logicalName, e, entityLogicalName, entityRelationships);
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
                        var controller = new ConfigurationController();
                        controller.LoadImportConfigFile(NotificationService, tbImportConfig, mapper, mapping);
                    }
                }
            }
        }

        private void ExportConfigPathButtonClick(object sender, EventArgs e)
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
                        var controller = new ConfigurationController();
                        controller.LoadExportConfigFile(NotificationService, tbExportConfig, filterQuery, lookupMaping);
                    }
                }
            }
        }

        private void ToolBarSaveMappingsClick(object sender, EventArgs e)
        {
            var controller = new ConfigurationController();
            controller.GenerateImportConfigFile(NotificationService, tbImportConfig, mapper);
        }

        private void ToolBarSaveFiltersClick(object sender, EventArgs e)
        {
            var controller = new ConfigurationController();
            controller.GenerateExportConfigFile(tbExportConfig, tbSchemaPath, filterQuery, lookupMaping, NotificationService);
        }

        private void ToolBarLoadMappingsFileClick(object sender, EventArgs e)
        {
            var controller = new ConfigurationController();
            controller.LoadImportConfigFile(NotificationService, tbImportConfig, mapper, mapping);
        }

        private void ToolBarLoadSchemaFileClick(object sender, EventArgs e)
        {
            LoadSchemaFile(tbSchemaPath.Text, workingstate, NotificationService, entityAttributes, entityRelationships);
        }

        private void ToolBarLoadFiltersFileClick(object sender, EventArgs e)
        {
            var controller = new ConfigurationController();
            controller.LoadExportConfigFile(NotificationService, tbExportConfig, filterQuery, lookupMaping);
        }

        private void LoadAllToolStripMenuItemClick(object sender, EventArgs e)
        {
            var collectionParameters = new CollectionParameters(entityAttributes, entityRelationships,
              filterQuery, lookupMaping, mapper, mapping);

            LoadAllFiles(NotificationService, tbSchemaPath, tbExportConfig, tbImportConfig, workingstate, collectionParameters);
        }

        public void LoadAllFiles(INotificationService feedbackManager, TextBox schemaPath, TextBox exportConfig, TextBox importConfig, bool inputWorkingstate, CollectionParameters collectionParameters)
        {
            LoadSchemaFile(schemaPath.Text, inputWorkingstate, feedbackManager, collectionParameters.EntityAttributes, collectionParameters.EntityRelationships);

            var controller = new ConfigurationController();
            controller.LoadExportConfigFile(feedbackManager, exportConfig, collectionParameters.FilterQuery, collectionParameters.LookupMaping);
            controller.LoadImportConfigFile(feedbackManager, importConfig, collectionParameters.Mapper, collectionParameters.Mapping);
        }

        private void SaveAllToolStripMenuItemClick(object sender, EventArgs e)
        {
            var controller = new ConfigurationController();
            controller.GenerateImportConfigFile(NotificationService, tbImportConfig, mapper);
            controller.GenerateExportConfigFile(tbExportConfig, tbSchemaPath, filterQuery, lookupMaping, NotificationService);

            var serviceParameters = new ServiceParameters(OrganizationService, MetadataService, NotificationService, ExceptionService);
            var entityController = new EntityController();
            entityController.CollectCrmEntityFields(checkedEntity, crmSchemaConfiguration, entityRelationships, entityAttributes, attributeMapping, serviceParameters);

            var schemaController = new SchemaController();
            schemaController.GenerateXMLFile(tbSchemaPath, crmSchemaConfiguration);
            crmSchemaConfiguration.Entities.Clear();
        }

        private void ToolStripButton1Click(object sender, EventArgs e)
        {
            var serviceParameters = new ServiceParameters(OrganizationService, MetadataService, NotificationService, ExceptionService);

            var controller = new ListController();
            controller.OpenMappingForm(serviceParameters, ParentForm, cachedMetadata, lookupMaping, entityLogicalName);

            tsbtMappings.ForeColor = Settings[organisationId.ToString()].Mappings.Count == 0 ? Color.Black : Color.Blue;
            Settings[organisationId.ToString()].Mappings.Clear();
        }

        protected void RadioButton1CheckedChanged(object sender, EventArgs e)
        {
            SetMenuVisibility(WizardMode.Schema);
        }

        protected void RadioButton2CheckedChanged(object sender, EventArgs e)
        {
            SetMenuVisibility(WizardMode.Export);
        }

        protected void RadioButton3CheckedChanged(object sender, EventArgs e)
        {
            SetMenuVisibility(WizardMode.Import);
        }

        protected void RadioButton4CheckedChanged(object sender, EventArgs e)
        {
            SetMenuVisibility(WizardMode.All);
        }

        private void SetMenuVisibility(WizardMode mode)
        {
            SetImportMenu(mode, tsbtMappings, loadMappingsToolStripMenuItem, saveMappingsToolStripMenuItem, tbImportConfig, btImportConfigPath);
            SetExportMenu(mode, lookupMappings, tsbtFilters, loadFiltersToolStripMenuItem, saveFiltersToolStripMenuItem, tbExportConfig, btExportConfigPath);
            SetSchemaMenu(mode, loadSchemaToolStripMenuItem, saveSchemaToolStripMenuItem, tbSchemaPath, btSchemaFolderPath);
            SetAllMenu(mode, loadAllToolStripMenuItem, saveAllToolStripMenuItem);
        }

        public void SetImportMenu(WizardMode mode, System.Windows.Forms.ToolStripButton mappingsToolStripButton, System.Windows.Forms.ToolStripMenuItem inputLoadMappingsToolStripMenuItem, System.Windows.Forms.ToolStripMenuItem inputSaveMappingsToolStripMenuItem, System.Windows.Forms.TextBox importConfigTextBox, System.Windows.Forms.Button importConfigPathButton)
        {
            mappingsToolStripButton.Enabled = mode == WizardMode.All || mode == WizardMode.Import;
            inputLoadMappingsToolStripMenuItem.Enabled = mode == WizardMode.All || mode == WizardMode.Import;
            inputSaveMappingsToolStripMenuItem.Enabled = mode == WizardMode.All || mode == WizardMode.Import;
            importConfigTextBox.Enabled = mode == WizardMode.All || mode == WizardMode.Import;
            importConfigPathButton.Enabled = mode == WizardMode.All || mode == WizardMode.Import;
        }

        public void SetExportMenu(WizardMode mode, ToolStripButton inputLookupMappings, ToolStripButton filtersToolStripButton, ToolStripMenuItem inputLoadFiltersToolStripMenuItem, ToolStripMenuItem inputSaveFiltersToolStripMenuItem, TextBox exportConfigTextBox, Button exportConfigPathButton)
        {
            inputLookupMappings.Enabled = mode == WizardMode.Export || mode == WizardMode.All;
            filtersToolStripButton.Enabled = mode == WizardMode.Export || mode == WizardMode.All;
            inputLoadFiltersToolStripMenuItem.Enabled = mode == WizardMode.Export || mode == WizardMode.All;
            inputSaveFiltersToolStripMenuItem.Enabled = mode == WizardMode.Export || mode == WizardMode.All;
            exportConfigTextBox.Enabled = mode == WizardMode.Export || mode == WizardMode.All;
            exportConfigPathButton.Enabled = mode == WizardMode.Export || mode == WizardMode.All;
        }

        public void SetSchemaMenu(WizardMode mode, ToolStripMenuItem inputLoadSchemaToolStripMenuItem, ToolStripMenuItem inputSaveSchemaToolStripMenuItem, TextBox schemaPathTextBox, Button schemaFolderPathButton)
        {
            inputLoadSchemaToolStripMenuItem.Enabled = mode == WizardMode.Schema || mode == WizardMode.All;
            inputSaveSchemaToolStripMenuItem.Enabled = mode == WizardMode.Schema || mode == WizardMode.All;
            schemaPathTextBox.Enabled = mode == WizardMode.Schema || mode == WizardMode.All;
            schemaFolderPathButton.Enabled = mode == WizardMode.Schema || mode == WizardMode.All;
        }

        public void SetAllMenu(WizardMode mode, ToolStripMenuItem inputLoadAllToolStripMenuItem, ToolStripMenuItem inputSaveAllToolStripMenuItem)
        {
            inputLoadAllToolStripMenuItem.Enabled = mode == WizardMode.All;
            inputSaveAllToolStripMenuItem.Enabled = mode == WizardMode.All;
        }

        private void ToolStripButtonConnectClick(object sender, EventArgs e)
        {
            if (OnConnectionRequested != null)
            {
                var args = new RequestConnectionEventArgs { ActionName = "SchemaConnection", Control = (CdsMigratorPluginControl)Parent };
                OnConnectionRequested(this, args);
            }
        }
    }
}