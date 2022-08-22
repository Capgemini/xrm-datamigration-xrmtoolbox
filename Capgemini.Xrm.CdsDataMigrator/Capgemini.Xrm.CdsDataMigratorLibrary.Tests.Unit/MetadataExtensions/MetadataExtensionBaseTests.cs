using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Capgemini.Xrm.CdsDataMigratorLibrary.Controllers;
using Capgemini.Xrm.CdsDataMigratorLibrary.Core;
using Capgemini.Xrm.CdsDataMigratorLibrary.Exceptions;
using Capgemini.Xrm.CdsDataMigratorLibrary.Forms;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Moq;

namespace Capgemini.Xrm.CdsDataMigrator.Tests.Unit.Controllers
{
    [TestClass]
    public class MetadataExtensionBaseTests : TestBase
    {
        private Dictionary<string, HashSet<string>> inputEntityRelationships;
        private Dictionary<string, HashSet<string>> inputEntityAttributes;
        private HashSet<string> inputCheckedEntity;

        private MetadataExtensionBase systemUnderTest;

        [TestInitialize]
        public void Setup()
        {
            SetupServiceMocks();
            inputEntityRelationships = new Dictionary<string, HashSet<string>>();
            inputEntityAttributes = new Dictionary<string, HashSet<string>>();
            inputCheckedEntity = new HashSet<string>();

            systemUnderTest = new MetadataExtensionBase();
        }

        [TestMethod]
        public void CollectCrmEntityRelationShipNoInputEntityRelationships()
        {
            var entityLogicalName = "contact";

            var entityMetadata = InstantiateEntityMetaData(entityLogicalName);
            InsertAttributeList(entityMetadata, new List<string> { "contactId", "firstname", "lastname" });

            var crmEntity = new Capgemini.Xrm.DataMigration.Model.CrmEntity();

            FluentActions.Invoking(() => systemUnderTest.CollectCrmEntityRelationShip(entityMetadata, crmEntity, inputEntityRelationships))
                                 .Should()
                                 .NotThrow();

            crmEntity.CrmRelationships.Count.Should().Be(0);
        }

        [TestMethod]
        public void CollectCrmEntityRelationShipWithInputEntityRelationships()
        {
            var entityLogicalName = "contact";
            var intersectEntityName = "account_contact";

            var relationship = new ManyToManyRelationshipMetadata
            {
                Entity1LogicalName = "account",
                Entity1IntersectAttribute = "accountid",
                IntersectEntityName = intersectEntityName,
                Entity2LogicalName = "contact",
                Entity2IntersectAttribute = "contactid",
                IsCustomizable = new BooleanManagedProperty() { Value = true }
            };

            var entityRelationshipSet = new HashSet<string>() { intersectEntityName };

            inputEntityRelationships.Add(entityLogicalName, entityRelationshipSet);

            var entityMetadata = InstantiateEntityMetaData(entityLogicalName);
            InsertAttributeList(entityMetadata, new List<string> { "contactId", "firstname", "lastname" });
            InsertManyToManyRelationshipMetadata(entityMetadata, relationship);

            var crmEntity = new Capgemini.Xrm.DataMigration.Model.CrmEntity();

            FluentActions.Invoking(() => systemUnderTest.CollectCrmEntityRelationShip(entityMetadata, crmEntity, inputEntityRelationships))
                                .Should()
                                .NotThrow();
            crmEntity.CrmRelationships.Count.Should().Be(1);
        }

        [TestMethod]
        public void StoreCrmEntityRelationShipData()
        {
            var entityLogicalName = "contact";
            var intersectEntityName = "account_contact";

            var relationship = new ManyToManyRelationshipMetadata
            {
                Entity1LogicalName = "account",
                Entity1IntersectAttribute = "accountid",
                IntersectEntityName = intersectEntityName,
                Entity2LogicalName = "contact",
                Entity2IntersectAttribute = "contactid",
                IsCustomizable = new BooleanManagedProperty() { Value = true }
            };

            var relationshipList = new List<Capgemini.Xrm.DataMigration.Model.CrmRelationship>();

            var crmEntity = new Capgemini.Xrm.DataMigration.Model.CrmEntity
            {
                Name = entityLogicalName,
                DisplayName = entityLogicalName
            };

            FluentActions.Invoking(() => systemUnderTest.StoreCrmEntityRelationShipData(crmEntity, relationship, relationshipList))
                                 .Should()
                                 .NotThrow();

            relationshipList.Count.Should().Be(1);
            relationshipList[0].RelatedEntityName.Should().Be(relationship.IntersectEntityName);
            relationshipList[0].RelationshipName.Should().Be(relationship.IntersectEntityName);
            relationshipList[0].IsReflexive.Should().Be(relationship.IsCustomizable.Value);
            relationshipList[0].ManyToMany.Should().BeTrue();
        }

        [TestMethod]
        public void CollectCrmAttributesFields()
        {
            var entityLogicalName = "contact";

            var attributeSet = new HashSet<string>() { "contactId", "firstname", "lastname" };
            inputEntityAttributes.Add(entityLogicalName, attributeSet);
            var inputAttributeMapping = new AttributeTypeMapping();

            var entityMetadata = InstantiateEntityMetaData(entityLogicalName);
            InsertAttributeList(entityMetadata, new List<string> { "contactId", "firstname", "lastname" });

            var crmEntity = new Capgemini.Xrm.DataMigration.Model.CrmEntity
            {
                Name = entityLogicalName,
                DisplayName = entityLogicalName
            };

            FluentActions.Invoking(() => systemUnderTest.CollectCrmAttributesFields(entityMetadata, crmEntity, inputEntityAttributes, inputAttributeMapping, NotificationServiceMock.Object))
                                 .Should()
                                 .NotThrow();

            crmEntity.CrmFields.Count.Should().Be(3);
        }

        [TestMethod]
        public void CollectCrmAttributesFieldsWithNoInputEntityAttributes()
        {
            var entityLogicalName = "contact";

            var inputAttributeMapping = new AttributeTypeMapping();

            var entityMetadata = InstantiateEntityMetaData(entityLogicalName);
            InsertAttributeList(entityMetadata, new List<string> { "contactId", "firstname", "lastname" });

            var crmEntity = new Capgemini.Xrm.DataMigration.Model.CrmEntity
            {
                Name = entityLogicalName,
                DisplayName = entityLogicalName
            };

            FluentActions.Invoking(() => systemUnderTest.CollectCrmAttributesFields(entityMetadata, crmEntity, inputEntityAttributes, inputAttributeMapping, NotificationServiceMock.Object))
                                 .Should()
                                 .NotThrow();

            crmEntity.CrmFields.Count.Should().Be(0);
        }

        [TestMethod]
        public void StoreAttributeMetadataWithNullInputEntityAttributes()
        {
            var entityLogicalName = "contact";
            inputEntityAttributes = null;
            var inputAttributeMapping = new AttributeTypeMapping();

            string primaryAttribute = "contactId";
            var entityMetadata = InstantiateEntityMetaData(entityLogicalName);
            InsertAttributeList(entityMetadata, new List<string> { primaryAttribute, "firstname", "lastname" });

            var crmFieldList = new List<Capgemini.Xrm.DataMigration.Model.CrmField>();

            FluentActions.Invoking(() => systemUnderTest.StoreAttributeMetadata(entityMetadata.Attributes[0], entityMetadata, primaryAttribute, crmFieldList, inputEntityAttributes, inputAttributeMapping, NotificationServiceMock.Object))
                                 .Should()
                                 .NotThrow();

            crmFieldList.Count.Should().Be(0);
        }

        [TestMethod]
        public void StoreAttributeMetadataWithNoInputEntityAttributes()
        {
            var entityLogicalName = "contact";

            var inputAttributeMapping = new AttributeTypeMapping();

            string primaryAttribute = "contactId";
            var entityMetadata = InstantiateEntityMetaData(entityLogicalName);
            InsertAttributeList(entityMetadata, new List<string> { primaryAttribute, "firstname", "lastname" });

            var crmFieldList = new List<Capgemini.Xrm.DataMigration.Model.CrmField>();

            FluentActions.Invoking(() => systemUnderTest.StoreAttributeMetadata(entityMetadata.Attributes[0], entityMetadata, primaryAttribute, crmFieldList, inputEntityAttributes, inputAttributeMapping, NotificationServiceMock.Object))
                                 .Should()
                                 .NotThrow();

            crmFieldList.Count.Should().Be(0);
        }

        [TestMethod]
        public void StoreAttributeMetadata()
        {
            var entityLogicalName = "contact";
            var attributeSet = new HashSet<string>() { "contactId", "firstname", "lastname" };
            inputEntityAttributes.Add(entityLogicalName, attributeSet);
            var inputAttributeMapping = new AttributeTypeMapping();

            string primaryAttribute = "contactId";
            var entityMetadata = InstantiateEntityMetaData(entityLogicalName);
            InsertAttributeList(entityMetadata, new List<string> { primaryAttribute, "firstname", "lastname" });

            var crmFieldList = new List<Capgemini.Xrm.DataMigration.Model.CrmField>();

            FluentActions.Invoking(() => systemUnderTest.StoreAttributeMetadata(entityMetadata.Attributes[0], entityMetadata, primaryAttribute, crmFieldList, inputEntityAttributes, inputAttributeMapping, NotificationServiceMock.Object))
                                 .Should()
                                 .NotThrow();

            crmFieldList.Count.Should().Be(1);
        }

        [TestMethod]
        public void StoreAttributePrimaryKey()
        {
            string primaryAttribute = "contactId";
            var crmField = new Capgemini.Xrm.DataMigration.Model.CrmField() { PrimaryKey = false, FieldName = primaryAttribute };

            FluentActions.Invoking(() => systemUnderTest.StoreAttributePrimaryKey(primaryAttribute, crmField))
                                 .Should()
                                 .NotThrow();

            crmField.PrimaryKey.Should().BeTrue();
        }

        [TestMethod]
        public void StoreAttributePrimaryKeyWithWrongPrimaryAttributeName()
        {
            string primaryAttribute = "contactId";
            var crmField = new Capgemini.Xrm.DataMigration.Model.CrmField() { PrimaryKey = false, FieldName = "contactId2" };

            FluentActions.Invoking(() => systemUnderTest.StoreAttributePrimaryKey(primaryAttribute, crmField))
                               .Should()
                               .NotThrow();

            crmField.PrimaryKey.Should().BeFalse();
        }

        [TestMethod]
        public void StoreLookUpAttributeNotEntityReference()
        {
            var attributeLogicalName = "contactId";
            string primaryAttribute = "contactId";
            var crmField = new Capgemini.Xrm.DataMigration.Model.CrmField()
            {
                PrimaryKey = false,
                FieldName = primaryAttribute,
                FieldType = "string"
            };

            var attribute = new AttributeMetadata
            {
                LogicalName = attributeLogicalName,
                DisplayName = new Label
                {
                    UserLocalizedLabel = new LocalizedLabel { Label = attributeLogicalName }
                }
            };

            var attributeTypeName = attribute.GetType().GetRuntimeFields().First(a => a.Name == "_attributeTypeDisplayName");
            attributeTypeName.SetValue(attribute, new AttributeTypeDisplayName { Value = attributeLogicalName });

            NotificationServiceMock.Setup(x => x.DisplayFeedback("The supplied attribute is null. Expecting an Entity Reference!"))
                              .Verifiable();

            FluentActions.Invoking(() => systemUnderTest.StoreLookUpAttribute(attribute, crmField, NotificationServiceMock.Object))
                             .Should()
                             .NotThrow();
            NotificationServiceMock.Verify(x => x.DisplayFeedback("The supplied attribute is null. Expecting an Entity Reference!"), Times.Never);
        }

        [TestMethod]
        public void StoreLookUpAttributeNotLookupAttributeMetadata()
        {
            var attributeLogicalName = "contactId";
            string primaryAttribute = "contactId";
            var crmField = new Capgemini.Xrm.DataMigration.Model.CrmField()
            {
                PrimaryKey = false,
                FieldName = primaryAttribute,
                FieldType = "entityreference"
            };

            var attribute = new AttributeMetadata
            {
                LogicalName = attributeLogicalName,
                DisplayName = new Label
                {
                    UserLocalizedLabel = new LocalizedLabel { Label = attributeLogicalName }
                }
            };

            var attributeTypeName = attribute.GetType().GetRuntimeFields().First(a => a.Name == "_attributeTypeDisplayName");
            attributeTypeName.SetValue(attribute, new AttributeTypeDisplayName { Value = attributeLogicalName });

            NotificationServiceMock.Setup(x => x.DisplayFeedback("The supplied attribute is null. Expecting an Entity Reference!"))
                               .Verifiable();

            FluentActions.Invoking(() => systemUnderTest.StoreLookUpAttribute(attribute, crmField, NotificationServiceMock.Object))
                             .Should()
                             .NotThrow();

            NotificationServiceMock.Verify(x => x.DisplayFeedback("The supplied attribute is null. Expecting an Entity Reference!"), Times.Once);
        }

        [TestMethod]
        public void StoreLookUpAttributeLookupAttributeMetadataIsNull()
        {
            string primaryAttribute = "contactId";
            var crmField = new Capgemini.Xrm.DataMigration.Model.CrmField()
            {
                PrimaryKey = false,
                FieldName = primaryAttribute,
                FieldType = "entityreference"
            };

            NotificationServiceMock.Setup(x => x.DisplayFeedback("The supplied attribute is null. Expecting an Entity Reference!"))
                               .Verifiable();

            FluentActions.Invoking(() => systemUnderTest.StoreLookUpAttribute(null, crmField, NotificationServiceMock.Object))
                             .Should()
                             .NotThrow();

            NotificationServiceMock.Verify(x => x.DisplayFeedback("The supplied attribute is null. Expecting an Entity Reference!"), Times.Once);
        }

        [TestMethod]
        public void StoreLookUpAttributeWithLookupAttributeMetadata()
        {
            var attributeLogicalName = "contactId";
            string primaryAttribute = "contactId";
            var crmField = new Capgemini.Xrm.DataMigration.Model.CrmField()
            {
                PrimaryKey = false,
                FieldName = primaryAttribute,
                FieldType = "entityreference"
            };

            var attribute = new LookupAttributeMetadata
            {
                LogicalName = attributeLogicalName,
                DisplayName = new Label
                {
                    UserLocalizedLabel = new LocalizedLabel { Label = attributeLogicalName }
                }
            };
            attribute.Targets = new List<string> { "Test" }.ToArray();

            NotificationServiceMock.Setup(x => x.DisplayFeedback("The supplied attribute is null. Expecting an Entity Reference!"))
                               .Verifiable();

            FluentActions.Invoking(() => systemUnderTest.StoreLookUpAttribute(attribute, crmField, NotificationServiceMock.Object))
                             .Should()
                             .NotThrow();

            NotificationServiceMock.Verify(x => x.DisplayFeedback("The supplied attribute is null. Expecting an Entity Reference!"), Times.Never);
            crmField.LookupType.Should().Be("Test");
        }

        [TestMethod]
        public void StoreLookUpAttributeWithLookupAttributeMetadataButEmptyTargetList()
        {
            var attributeLogicalName = "contactId";
            string primaryAttribute = "contactId";
            var crmField = new Capgemini.Xrm.DataMigration.Model.CrmField()
            {
                PrimaryKey = false,
                FieldName = primaryAttribute,
                FieldType = "entityreference"
            };

            var attribute = new LookupAttributeMetadata
            {
                LogicalName = attributeLogicalName,
                DisplayName = new Label
                {
                    UserLocalizedLabel = new LocalizedLabel { Label = attributeLogicalName }
                }
            };

            NotificationServiceMock.Setup(x => x.DisplayFeedback("The supplied attribute is null. Expecting an Entity Reference!"))
                               .Verifiable();

            FluentActions.Invoking(() => systemUnderTest.StoreLookUpAttribute(attribute, crmField, NotificationServiceMock.Object))
                             .Should()
                             .NotThrow();

            NotificationServiceMock.Verify(x => x.DisplayFeedback("The supplied attribute is null. Expecting an Entity Reference!"), Times.Never);
            crmField.LookupType.Should().BeNull();
        }

        [TestMethod]
        public void AreCrmEntityFieldsSelectedCheckedEntityCountIsZero()
        {
            var inputAttributeMapping = new AttributeTypeMapping();
            var serviceParameters = GenerateMigratorParameters();

            var actual = systemUnderTest.AreCrmEntityFieldsSelected(inputCheckedEntity, inputEntityRelationships, inputEntityAttributes, inputAttributeMapping, serviceParameters);

            actual.Should().BeFalse();
        }

        [TestMethod]
        public void AreCrmEntityFieldsSelected()
        {
            var entityLogicalName = "contact";
            inputCheckedEntity.Add(entityLogicalName);
            var entityMetadata = InstantiateEntityMetaData(entityLogicalName);
            InsertAttributeList(entityMetadata, new List<string> { "contactId", "firstname", "lastname" });

            var attributeSet = new HashSet<string>() { "contactId", "firstname", "lastname" };
            inputEntityAttributes.Add(entityLogicalName, attributeSet);
            var inputAttributeMapping = new AttributeTypeMapping();
            var serviceParameters = GenerateMigratorParameters();

            MetadataServiceMock.Setup(x => x.RetrieveEntities(It.IsAny<string>(), It.IsAny<IOrganizationService>(), It.IsAny<IExceptionService>()))
                                .Returns(entityMetadata)
                                .Verifiable();

            var actual = systemUnderTest.AreCrmEntityFieldsSelected(inputCheckedEntity, inputEntityRelationships, inputEntityAttributes, inputAttributeMapping, serviceParameters);

            actual.Should().BeTrue();
            MetadataServiceMock.VerifyAll();
        }

        [TestMethod]
        public void CollectCrmEntityFieldsCheckedEntityCountIsZero()
        {
            var inputAttributeMapping = new AttributeTypeMapping();
            var serviceParameters = GenerateMigratorParameters();

            var schemaConfiguration = new Capgemini.Xrm.DataMigration.Config.CrmSchemaConfiguration();

            FluentActions.Invoking(() => systemUnderTest.CollectCrmEntityFields(inputCheckedEntity, schemaConfiguration, inputEntityRelationships, inputEntityAttributes, inputAttributeMapping, serviceParameters))
                                .Should()
                                .NotThrow();
        }

        [TestMethod]
        public void CollectCrmEntityFields()
        {
            var entityLogicalName = "contact";
            inputCheckedEntity.Add(entityLogicalName);
            var attributeSet = new HashSet<string>() { "contactId", "firstname", "lastname" };
            inputEntityAttributes.Add(entityLogicalName, attributeSet);
            var inputAttributeMapping = new AttributeTypeMapping();

            var schemaConfiguration = new Capgemini.Xrm.DataMigration.Config.CrmSchemaConfiguration();

            var entityMetadata = InstantiateEntityMetaData(entityLogicalName);
            InsertAttributeList(entityMetadata, new List<string> { "contactId", "firstname", "lastname" });

            var serviceParameters = GenerateMigratorParameters();

            MetadataServiceMock.Setup(x => x.RetrieveEntities(It.IsAny<string>(), It.IsAny<IOrganizationService>(), It.IsAny<IExceptionService>()))
                                .Returns(entityMetadata)
                                .Verifiable();

            FluentActions.Invoking(() => systemUnderTest.CollectCrmEntityFields(inputCheckedEntity, schemaConfiguration, inputEntityRelationships, inputEntityAttributes, inputAttributeMapping, serviceParameters))
                                 .Should()
                                 .NotThrow();

            MetadataServiceMock.VerifyAll();
        }
    }
}