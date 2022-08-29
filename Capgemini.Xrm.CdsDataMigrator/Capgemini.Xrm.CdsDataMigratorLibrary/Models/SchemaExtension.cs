using Capgemini.Xrm.CdsDataMigratorLibrary.Core;
using Capgemini.Xrm.CdsDataMigratorLibrary.Models;
using Capgemini.Xrm.CdsDataMigratorLibrary.Services;
using Capgemini.Xrm.DataMigration.Config;
using Capgemini.Xrm.DataMigration.Model;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Controllers
{
    public class SchemaExtension
    {
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
                foreach (var relationship in manyToManyRelationship
                                                .Where(relationship => inputEntityRelationships.ContainsKey(sourceList.LogicalName))
                                                .SelectMany(relationship => inputEntityRelationships[sourceList.LogicalName]
                                                                            .Where(relationshipName => relationshipName ==
                                                                                                relationship.IntersectEntityName)
                                                                            .Select(relationshipName => relationship)
                                                            )
                         )
                {
                    StoreCrmEntityRelationShipData(crmEntity, relationship, relationshipList);
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

        public void StoreAttributeMetadata(AttributeMetadata attribute, EntityMetadata entityMetadata, string primaryAttribute, List<CrmField> crmFieldList, Dictionary<string, HashSet<string>> inputEntityAttributes, AttributeTypeMapping attributeMapping, INotificationService notificationService)
        {
            if (inputEntityAttributes != null && inputEntityAttributes.ContainsKey(entityMetadata.LogicalName))
            {
                var crmField = new CrmField();
                foreach (var _ in inputEntityAttributes[entityMetadata.LogicalName]
                                            .Where(attributeLogicalName => attribute.LogicalName.Equals(attributeLogicalName, StringComparison.InvariantCulture))
                                                .Select(attributeLogicalName => new { }))
                {
                    crmField.DisplayName = attribute.DisplayName.UserLocalizedLabel == null ? string.Empty : attribute.DisplayName.UserLocalizedLabel.Label;
                    crmField.FieldName = attribute.LogicalName;
                    attributeMapping.AttributeMetadataType = attribute.AttributeTypeName.Value.ToString(CultureInfo.InvariantCulture);
                    attributeMapping.GetMapping(notificationService);
                    crmField.FieldType = attributeMapping.AttributeMetadataTypeResult;
                    StoreLookUpAttribute(attribute, crmField, notificationService);
                    StoreAttributePrimaryKey(primaryAttribute, crmField);
                    crmFieldList.Add(crmField);
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

        public void SchemaFolderPathAction(INotificationService notificationService, TextBox schemaPathTextBox, bool inputWorkingstate, CollectionParameters collectionParameters, DialogResult dialogResult, SaveFileDialog fileDialog,
    Action<string, bool, INotificationService, Dictionary<string, HashSet<string>>, Dictionary<string, HashSet<string>>> loadSchemaFile
    )
        {
            if (dialogResult == DialogResult.OK)
            {
                schemaPathTextBox.Text = fileDialog.FileName.ToString(CultureInfo.InvariantCulture);

                if (File.Exists(schemaPathTextBox.Text))
                {
                    loadSchemaFile(schemaPathTextBox.Text, inputWorkingstate, notificationService, collectionParameters.EntityAttributes, collectionParameters.EntityRelationships);
                }
            }
        }

        public void SaveSchema(ServiceParameters serviceParameters, HashSet<string> inputCheckedEntity, Dictionary<string, HashSet<string>> inputEntityRelationships, Dictionary<string, HashSet<string>> inputEntityAttributes, AttributeTypeMapping inputAttributeMapping, CrmSchemaConfiguration inputCrmSchemaConfiguration, string schemaPath)
        {
            if (AreCrmEntityFieldsSelected(inputCheckedEntity, inputEntityRelationships, inputEntityAttributes, inputAttributeMapping, serviceParameters))
            {
                CollectCrmEntityFields(inputCheckedEntity, inputCrmSchemaConfiguration, inputEntityRelationships, inputEntityAttributes, inputAttributeMapping, serviceParameters);

                GenerateXmlFile(schemaPath, inputCrmSchemaConfiguration);
                inputCrmSchemaConfiguration.Entities.Clear();
            }
            else
            {
                serviceParameters.NotificationService.DisplayFeedback("Please select at least one attribute for each selected entity!");
            }
        }

        public void GenerateXmlFile(string schemaFilePath, CrmSchemaConfiguration schemaConfiguration)
        {
            if (!string.IsNullOrWhiteSpace(schemaFilePath))
            {
                schemaConfiguration.SaveToFile(schemaFilePath);
            }
        }
    }
}