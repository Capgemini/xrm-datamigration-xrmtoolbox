using Capgemini.Xrm.CdsDataMigrator.Services;
using Capgemini.Xrm.DataMigration.Config;
using Capgemini.Xrm.DataMigration.CrmStore.Config;
using Capgemini.Xrm.DataMigration.Model;
using Capgemini.Xrm.DataMigration.XrmToolBox.Core;
using Capgemini.Xrm.DataMigration.XrmToolBox.Services;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Forms;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Model;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using NuGet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin
{
    public class SchemaWizardDelegate
    {
        public void GenerateXMLFile(TextBox tbSchemaPath, CrmSchemaConfiguration schemaConfiguration)
        {
            if (!string.IsNullOrWhiteSpace(tbSchemaPath.Text))
            {
                schemaConfiguration.SaveToFile(tbSchemaPath.Text);
            }
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

        public void OpenMappingForm(ServiceParameters serviceParameters, IWin32Window owner, List<EntityMetadata> inputCachedMetadata, Dictionary<string, Dictionary<string, List<string>>> inputLookupMaping, string inputEntityLogicalName)
        {
            using (var mappingDialog = new MappingListLookup(inputLookupMaping, serviceParameters.OrganizationService, inputCachedMetadata, inputEntityLogicalName, serviceParameters.MetadataService, serviceParameters.ExceptionService)
            {
                StartPosition = FormStartPosition.CenterParent
            })
            {
                if (owner != null)
                {
                    mappingDialog.ShowDialog(owner);
                }
                mappingDialog.RefreshMappingList();
            }
        }

        public List<ListViewItem> PopulateRelationshipAction(string inputEntityLogicalName, Dictionary<string, HashSet<string>> inputEntityRelationships, ServiceParameters migratorParameters)
        {
            var entityMetaData = migratorParameters.MetadataService.RetrieveEntities(inputEntityLogicalName, migratorParameters.OrganizationService, migratorParameters.ExceptionService);
            var sourceAttributesList = new List<ListViewItem>();
            if (entityMetaData != null && entityMetaData.ManyToManyRelationships != null && entityMetaData.ManyToManyRelationships.Any())
            {
                foreach (var relationship in entityMetaData.ManyToManyRelationships)
                {
                    var item = new ListViewItem(relationship.IntersectEntityName);
                    AddRelationship(relationship, item, sourceAttributesList);
                    UpdateAttributeMetadataCheckBoxes(relationship.IntersectEntityName, item, inputEntityRelationships, inputEntityLogicalName);
                }
            }

            return sourceAttributesList;
        }

        public string GetEntityLogicalName(ListViewItem entityitem)
        {
            string logicalName = null;
            if (entityitem != null && entityitem.Tag != null)
            {
                var entity = (EntityMetadata)entityitem.Tag;
                logicalName = entity.LogicalName;
            }
            return logicalName;
        }

        public AttributeMetadata[] GetAttributeList(string entityLogicalName, bool showSystemAttributes, ServiceParameters serviceParameters)
        {
            var entitymeta = serviceParameters.MetadataService.RetrieveEntities(entityLogicalName, serviceParameters.OrganizationService, serviceParameters.ExceptionService);

            var attributes = FilterAttributes(entitymeta, showSystemAttributes);

            if (attributes != null)
            {
                attributes = attributes.OrderByDescending(p => p.IsPrimaryId)
                                       .ThenByDescending(p => p.IsPrimaryName)
                                       .ThenByDescending(p => p.IsCustomAttribute != null && p.IsCustomAttribute.Value)
                                       .ThenBy(p => p.IsLogical != null && p.IsLogical.Value)
                                       .ThenBy(p => p.LogicalName).ToArray();
            }

            return attributes;
        }

        public List<ListViewItem> RetrieveSourceEntitiesList(bool showSystemAttributes, List<EntityMetadata> inputCachedMetadata, Dictionary<string, HashSet<string>> inputEntityAttributes, ServiceParameters serviceParameters)
        {
            var sourceList = serviceParameters.MetadataService.RetrieveEntities(serviceParameters.OrganizationService);

            if (!showSystemAttributes)
            {
                sourceList = sourceList.Where(p => !p.IsLogicalEntity.Value && !p.IsIntersect.Value).ToList();
            }

            if (sourceList != null)
            {
                inputCachedMetadata = sourceList.OrderBy(p => p.IsLogicalEntity.Value).ThenBy(p => p.IsIntersect.Value).ThenByDescending(p => p.IsCustomEntity.Value).ThenBy(p => p.LogicalName).ToList();
            }

            var sourceEntitiesList = new List<ListViewItem>();

            foreach (EntityMetadata entity in inputCachedMetadata)
            {
                var name = entity.DisplayName.UserLocalizedLabel == null ? string.Empty : entity.DisplayName.UserLocalizedLabel.Label;
                var item = new ListViewItem(name)
                {
                    Tag = entity
                };
                item.SubItems.Add(entity.LogicalName);
                IsInvalidForCustomization(entity, item);
                UpdateCheckBoxesEntities(entity, item, inputEntityAttributes);

                sourceEntitiesList.Add(item);
            }

            return sourceEntitiesList;
        }

        public void PopulateEntitiesListView(List<ListViewItem> items, Exception exception, IWin32Window owner, ListView listView, INotificationService notificationService)
        {
            if (exception != null)
            {
                notificationService.DisplayErrorFeedback(owner, $"An error occured: {exception.Message}");
            }
            else
            {
                if (items != null && items.Count > 0)
                {
                    listView.Items.AddRange(items.ToArray());
                }
                else
                {
                    notificationService.DisplayWarningFeedback(owner, "The system does not contain any entities");
                }
            }
        }

        public void OnPopulateCompletedAction(RunWorkerCompletedEventArgs e, INotificationService notificationService, IWin32Window owner, ListView listView)
        {
            if (e.Error != null)
            {
                notificationService.DisplayErrorFeedback(owner, $"An error occured: {e.Error.Message}");
            }
            else
            {
                var items = e.Result as List<ListViewItem>;
                if (items != null)
                {
                    listView.Items.AddRange(items.ToArray());
                }
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

        public void LoadExportConfigFile(INotificationService notificationService, TextBox exportConfigTextBox, Dictionary<string, string> inputFilterQuery, Dictionary<string, Dictionary<string, List<string>>> inputLookupMaping)
        {
            if (!string.IsNullOrWhiteSpace(exportConfigTextBox.Text))
            {
                try
                {
                    var configFile = CrmExporterConfig.GetConfiguration(exportConfigTextBox.Text);
                    if (!configFile.CrmMigrationToolSchemaPaths.Any())
                    {
                        notificationService.DisplayFeedback("Invalid Export Config File");
                        exportConfigTextBox.Text = "";
                        return;
                    }

                    inputFilterQuery.Clear();
                    inputFilterQuery.AddRange(configFile.CrmMigrationToolSchemaFilters);

                    inputLookupMaping.Clear();
                    inputLookupMaping.AddRange(configFile.LookupMapping);

                    notificationService.DisplayFeedback("Filters and Lookup Mappings loaded from Export Config File");
                }
                catch (Exception ex)
                {
                    notificationService.DisplayFeedback($"Load Correct Export Config file, error:{ex.Message}");
                }
            }
        }

        public void UpdateAttributeMetadataCheckBoxes(string predicate, ListViewItem item, Dictionary<string, HashSet<string>> inputEntityRelationships, string inputEntityLogicalName)
        {
            if (inputEntityRelationships.ContainsKey(inputEntityLogicalName))
            {
                foreach (string attr in inputEntityRelationships[inputEntityLogicalName])
                {
                    item.Checked |= attr.Equals(predicate, StringComparison.InvariantCulture);
                }
            }
        }

        public List<ListViewItem> ProcessAllAttributeMetadata(List<string> unmarkedattributes, AttributeMetadata[] attributes, string inputEntityLogicalName, Dictionary<string, HashSet<string>> inputEntityAttributes)
        {
            List<ListViewItem> sourceAttributesList = new List<ListViewItem>();
            foreach (AttributeMetadata attribute in attributes)
            {
                var name = attribute.DisplayName.UserLocalizedLabel == null ? string.Empty : attribute.DisplayName.UserLocalizedLabel.Label;
                var typename = attribute.AttributeTypeName == null ? string.Empty : attribute.AttributeTypeName.Value;
                var item = new ListViewItem(name);
                AddAttribute(attribute, item, typename);
                InvalidUpdate(attribute, item);
                item.Checked = unmarkedattributes.Contains(attribute.LogicalName);
                UpdateAttributeMetadataCheckBoxes(attribute.LogicalName, item, inputEntityAttributes, inputEntityLogicalName);
                sourceAttributesList.Add(item);
            }

            return sourceAttributesList;
        }

        public AttributeMetadata[] FilterAttributes(EntityMetadata entityMetadata, bool showSystemAttributes)
        {
            AttributeMetadata[] attributes = entityMetadata.Attributes?.ToArray();

            if (attributes != null && !showSystemAttributes)
            {
                attributes = attributes.Where(p => p.IsLogical != null
                                                    && !p.IsLogical.Value
                                                    && p.IsValidForRead != null
                                                    && p.IsValidForRead.Value
                                                    && ((p.IsValidForCreate != null && p.IsValidForCreate.Value) || (p.IsValidForUpdate != null && p.IsValidForUpdate.Value)))
                                        .ToArray();
            }

            return attributes;
        }

        public void InvalidUpdate(AttributeMetadata attribute, ListViewItem item)
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

        public void IsInvalidForCustomization(EntityMetadata entity, ListViewItem item)
        {
            if (entity != null)
            {
                if (entity.IsCustomEntity != null && entity.IsCustomEntity.Value)
                {
                    item.ForeColor = Color.DarkGreen;
                }

                if (entity.IsIntersect != null && entity.IsIntersect.Value)
                {
                    item.ForeColor = Color.Red;
                    item.ToolTipText = "Intersect Entity, ";
                }

                if (entity.IsLogicalEntity != null && entity.IsLogicalEntity.Value)
                {
                    item.ForeColor = Color.Red;
                    item.ToolTipText = "Logical Entity";
                }
            }
        }

        public void HandleMappingControlItemClick(INotificationService notificationService, string inputEntityLogicalName, bool listViewItemIsSelected, Dictionary<string, List<Item<EntityReference, EntityReference>>> inputMapping, Dictionary<string, Dictionary<Guid, Guid>> inputMapper, Form parentForm)
        {
            if (listViewItemIsSelected)
            {
                if (!string.IsNullOrEmpty(inputEntityLogicalName))
                {
                    if (inputMapping.ContainsKey(inputEntityLogicalName))
                    {
                        MappingIfContainsKey(inputEntityLogicalName, inputMapping, inputMapper, parentForm);
                    }
                    else
                    {
                        MappingIfKeyDoesNotExist(inputEntityLogicalName, inputMapping, inputMapper, parentForm);
                    }
                }
            }
            else
            {
                notificationService.DisplayFeedback("Entity is not selected");
            }
        }

        public static void MappingIfKeyDoesNotExist(string inputEntityLogicalName, Dictionary<string, List<Item<EntityReference, EntityReference>>> inputMapping, Dictionary<string, Dictionary<Guid, Guid>> inputMapper, Form parentForm)
        {
            var mappingReference = new List<Item<EntityReference, EntityReference>>();
            using (var mappingDialog = new MappingList(mappingReference)
            {
                StartPosition = FormStartPosition.CenterParent
            })
            {
                if (parentForm != null)
                {
                    mappingDialog.ShowDialog(parentForm);
                }

                var mapList = mappingDialog.GetMappingList(inputEntityLogicalName);
                var guidMapList = mappingDialog.GetGuidMappingList();

                if (mapList.Count > 0)
                {
                    inputMapping.Add(inputEntityLogicalName, mapList);
                    inputMapper.Add(inputEntityLogicalName, guidMapList);
                }
            }
        }

        public static void MappingIfContainsKey(string inputEntityLogicalName, Dictionary<string, List<Item<EntityReference, EntityReference>>> inputMapping, Dictionary<string, Dictionary<Guid, Guid>> inputMapper, Form parentForm)
        {
            using (var mappingDialog = new MappingList(inputMapping[inputEntityLogicalName])
            {
                StartPosition = FormStartPosition.CenterParent
            })
            {
                if (parentForm != null)
                {
                    mappingDialog.ShowDialog(parentForm);
                }

                var mapList = mappingDialog.GetMappingList(inputEntityLogicalName);
                var guidMapList = mappingDialog.GetGuidMappingList();

                if (mapList.Count == 0)
                {
                    inputMapping.Remove(inputEntityLogicalName);
                    inputMapper.Remove(inputEntityLogicalName);
                }
                else
                {
                    inputMapping[inputEntityLogicalName] = mapList;
                    inputMapper[inputEntityLogicalName] = guidMapList;
                }
            }
        }

        public void ProcessFilterQuery(INotificationService notificationService, Form parentForm, string inputEntityLogicalName, bool listViewItemIsSelected, Dictionary<string, string> inputFilterQuery, FilterEditor filterDialog)
        {
            if (listViewItemIsSelected)
            {
                if (parentForm != null)
                {
                    filterDialog.ShowDialog(parentForm);
                }

                if (inputFilterQuery.ContainsKey(inputEntityLogicalName))
                {
                    if (string.IsNullOrWhiteSpace(filterDialog.QueryString))
                    {
                        inputFilterQuery.Remove(inputEntityLogicalName);
                    }
                    else
                    {
                        inputFilterQuery[inputEntityLogicalName] = filterDialog.QueryString;
                    }
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(filterDialog.QueryString))
                    {
                        inputFilterQuery[inputEntityLogicalName] = filterDialog.QueryString;
                    }
                }
            }
            else
            {
                notificationService.DisplayFeedback("Entity list is empty");
            }
        }

        public void LoadImportConfigFile(INotificationService notificationService, TextBox importConfig, Dictionary<string, Dictionary<Guid, Guid>> inputMapper, Dictionary<string, List<Item<EntityReference, EntityReference>>> inputMapping)
        {
            if (!string.IsNullOrWhiteSpace(importConfig.Text))
            {
                try
                {
                    var configImport = CrmImportConfig.GetConfiguration(importConfig.Text);
                    if (configImport.MigrationConfig == null)
                    {
                        notificationService.DisplayFeedback("Invalid Import Config File");
                        importConfig.Text = "";
                        return;
                    }

                    inputMapper = configImport.MigrationConfig.Mappings;
                    DataConversion(inputMapping, inputMapper);

                    notificationService.DisplayFeedback("Guid Id Mappings loaded from Import Config File");
                }
                catch (Exception ex)
                {
                    notificationService.DisplayFeedback($"Load Correct Import Config file, error:{ex.Message}");
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

        private void UpdateCheckBoxesEntities(EntityMetadata entity, ListViewItem item, Dictionary<string, HashSet<string>> inputEntityAttributes)
        {
            item.Checked |= inputEntityAttributes.ContainsKey(entity.LogicalName);
        }

        public bool AreCrmEntityFieldsSelected(HashSet<string> inputCheckedEntity, Dictionary<string, HashSet<string>> inputEntityRelationships, Dictionary<string, HashSet<string>> inputEntityAttributes, AttributeTypeMapping inputAttributeMapping, ServiceParameters serviceParameters)
        {
            var fieldsSelected = false;
            if (inputCheckedEntity.Count > 0)
            {
                var crmEntityList = new List<CrmEntity>();

                foreach (var item in inputCheckedEntity)
                {
                    var crmEntity = new CrmEntity();
                    var sourceList = serviceParameters.MetadataService.RetrieveEntities(item, serviceParameters.OrganizationService, serviceParameters.ExceptionService);
                    StoreCrmEntityData(crmEntity, sourceList, crmEntityList, inputEntityRelationships, inputEntityAttributes, inputAttributeMapping, serviceParameters.NotificationService);

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

        public void CollectCrmEntityFields(HashSet<string> inputCheckedEntity, CrmSchemaConfiguration schemaConfiguration, Dictionary<string, HashSet<string>> inputEntityRelationships, Dictionary<string, HashSet<string>> inputEntityAttributes, AttributeTypeMapping inputAttributeMapping, ServiceParameters serviceParameters)
        {
            if (inputCheckedEntity.Count > 0)
            {
                var crmEntityList = new List<CrmEntity>();

                foreach (var item in inputCheckedEntity)
                {
                    var crmEntity = new CrmEntity();
                    var sourceList = serviceParameters.MetadataService.RetrieveEntities(item, serviceParameters.OrganizationService, serviceParameters.ExceptionService);
                    StoreCrmEntityData(crmEntity, sourceList, crmEntityList, inputEntityRelationships, inputEntityAttributes, inputAttributeMapping, serviceParameters.NotificationService);
                }

                schemaConfiguration.Entities.Clear();
                schemaConfiguration.Entities.AddRange(crmEntityList);
            }
        }

        public void StoreCrmEntityData(CrmEntity crmEntity, EntityMetadata sourceList, List<CrmEntity> crmEntityList, Dictionary<string, HashSet<string>> inputEntityRelationships, Dictionary<string, HashSet<string>> inputEntityAttributes, AttributeTypeMapping inputAttributeMapping, INotificationService notificationService)
        {
            crmEntity.Name = sourceList.LogicalName;

            crmEntity.DisplayName = sourceList.DisplayName?.UserLocalizedLabel == null ? string.Empty : sourceList.DisplayName.UserLocalizedLabel.Label;

            crmEntity.EntityCode = sourceList.ObjectTypeCode.ToString();
            crmEntity.PrimaryIdField = sourceList.PrimaryIdAttribute;
            crmEntity.PrimaryNameField = sourceList.PrimaryNameAttribute;
            CollectCrmEntityRelationShip(sourceList, crmEntity, inputEntityRelationships);
            CollectCrmAttributesFields(sourceList, crmEntity, inputEntityAttributes, inputAttributeMapping, notificationService);
            crmEntityList.Add(crmEntity);
        }

        public void CollectCrmEntityRelationShip(EntityMetadata sourceList, CrmEntity crmEntity, Dictionary<string, HashSet<string>> inputEntityRelationships)
        {
            var manyToManyRelationship = sourceList.ManyToManyRelationships;
            var relationshipList = new List<CrmRelationship>();

            if (manyToManyRelationship != null)
            {
                foreach (var relationship in manyToManyRelationship)
                {
                    if (inputEntityRelationships.ContainsKey(sourceList.LogicalName))
                    {
                        foreach (var relationshipName in inputEntityRelationships[sourceList.LogicalName])
                        {
                            if (relationshipName == relationship.IntersectEntityName)
                            {
                                StoreCrmEntityRelationShipData(crmEntity, relationship, relationshipList);
                            }
                        }
                    }
                }
            }

            crmEntity.CrmRelationships.Clear();
            crmEntity.CrmRelationships.AddRange(relationshipList);
        }

        public void StoreCrmEntityRelationShipData(CrmEntity crmEntity, ManyToManyRelationshipMetadata relationship, List<CrmRelationship> relationshipList)
        {
            var crmRelationShip = new CrmRelationship
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

        public void CollectCrmAttributesFields(EntityMetadata sourceList, CrmEntity crmEntity, Dictionary<string, HashSet<string>> inputEntityAttributes, AttributeTypeMapping inputAttributeMapping, INotificationService notificationService)
        {
            if (inputEntityAttributes != null)
            {
                var attributes = sourceList.Attributes.ToArray();

                var primaryAttribute = sourceList.PrimaryIdAttribute;
                if (sourceList.LogicalName != null && inputEntityAttributes.ContainsKey(sourceList.LogicalName))
                {
                    var crmFieldList = new List<CrmField>();
                    foreach (AttributeMetadata attribute in attributes)
                    {
                        StoreAttributeMetadata(attribute, sourceList, primaryAttribute, crmFieldList, inputEntityAttributes, inputAttributeMapping, notificationService);
                    }

                    crmEntity.CrmFields.Clear();
                    crmEntity.CrmFields.AddRange(crmFieldList);
                }
            }
        }

        public void StoreAttributeMetadata(AttributeMetadata attribute, EntityMetadata entityMetadata, string primaryAttribute, List<CrmField> crmFieldList, Dictionary<string, HashSet<string>> inputEntityAttributes, AttributeTypeMapping inputAttributeMapping, INotificationService notificationService)
        {
            if (inputEntityAttributes != null && inputEntityAttributes.ContainsKey(entityMetadata.LogicalName))
            {
                var crmField = new CrmField();
                foreach (var attributeLogicalName in inputEntityAttributes[entityMetadata.LogicalName])
                {
                    if (attribute.LogicalName.Equals(attributeLogicalName, StringComparison.InvariantCulture))
                    {
                        crmField.DisplayName = attribute.DisplayName.UserLocalizedLabel == null ? string.Empty : attribute.DisplayName.UserLocalizedLabel.Label;
                        crmField.FieldName = attribute.LogicalName;
                        inputAttributeMapping.AttributeMetadataType = attribute.AttributeTypeName.Value.ToString(CultureInfo.InvariantCulture);
                        inputAttributeMapping.GetMapping(notificationService);
                        crmField.FieldType = inputAttributeMapping.AttributeMetadataTypeResult;
                        StoreLookUpAttribute(attribute, crmField, notificationService);
                        StoreAttributePrimaryKey(primaryAttribute, crmField);
                        crmFieldList.Add(crmField);
                    }
                }
            }
        }

        public void StoreAttributePrimaryKey(string primaryAttribute, CrmField crmField)
        {
            if (crmField.FieldName.Equals(primaryAttribute, StringComparison.InvariantCulture))
            {
                crmField.PrimaryKey = true;
            }
        }

        public void StoreLookUpAttribute(AttributeMetadata attribute, CrmField crmField, INotificationService notificationService)
        {
            if (crmField.FieldType.Equals("entityreference", StringComparison.InvariantCulture))
            {
                if (attribute is LookupAttributeMetadata lookUpAttribute)
                {
                    if (lookUpAttribute.Targets != null && lookUpAttribute.Targets.Any())
                    {
                        crmField.LookupType = lookUpAttribute.Targets[0];
                    }
                }
                else
                {
                    notificationService.DisplayFeedback("The supplied attribute is null. Expecting an Entity Reference!");
                }
            }
        }

        public void GenerateImportConfigFile(INotificationService notificationService, TextBox importConfig, Dictionary<string, Dictionary<Guid, Guid>> inputMapper)
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

                if (File.Exists(importConfig.Text))
                {
                    migration = CrmImportConfig.GetConfiguration(importConfig.Text);
                }

                if (migration.MigrationConfig == null)
                {
                    migration.MigrationConfig = new MappingConfiguration();
                }

                if (inputMapper != null)
                {
                    migration.MigrationConfig.Mappings.Clear();
                    migration.MigrationConfig.Mappings.AddRange(inputMapper);

                    if (File.Exists(importConfig.Text))
                    {
                        File.Delete(importConfig.Text);
                    }

                    migration.SaveConfiguration(importConfig.Text);
                }
            }
            catch (Exception ex)
            {
                notificationService.DisplayFeedback($"Error Saving Import Config file, Error:{ex.Message}");
            }
        }

        public void GenerateExportConfigFile(TextBox exportConfig, TextBox schemaPath, Dictionary<string, string> inputFilterQuery, Dictionary<string, Dictionary<string, List<string>>> inputLookupMaping)
        {
            CrmExporterConfig config = new CrmExporterConfig()
            {
                JsonFolderPath = "ExtractedData",
            };

            if (File.Exists(exportConfig.Text))
            {
                config = CrmExporterConfig.GetConfiguration(exportConfig.Text);
            }

            config.CrmMigrationToolSchemaFilters.Clear();
            config.CrmMigrationToolSchemaFilters.AddRange(inputFilterQuery);

            if (!string.IsNullOrWhiteSpace(schemaPath.Text))
            {
                config.CrmMigrationToolSchemaPaths.Clear();
                config.CrmMigrationToolSchemaPaths.Add(schemaPath.Text);
            }

            if (inputLookupMaping.Count > 0)
            {
                config.LookupMapping.Clear();
                config.LookupMapping.AddRange(inputLookupMaping);
            }

            if (File.Exists(exportConfig.Text))
            {
                File.Delete(exportConfig.Text);
            }

            config.SaveConfiguration(exportConfig.Text);
        }

        public void DataConversion(Dictionary<string, List<Item<EntityReference, EntityReference>>> inputMapping, Dictionary<string, Dictionary<Guid, Guid>> inputMapper)
        {
            inputMapping.Clear();
            foreach (var mappings in inputMapper)
            {
                var list = new List<Item<EntityReference, EntityReference>>();

                foreach (var values in mappings.Value)
                {
                    list.Add(new Item<EntityReference, EntityReference>(new EntityReference(mappings.Key, values.Key), new EntityReference(mappings.Key, values.Value)));
                }

                inputMapping.Add(mappings.Key, list);
            }
        }
    }
}