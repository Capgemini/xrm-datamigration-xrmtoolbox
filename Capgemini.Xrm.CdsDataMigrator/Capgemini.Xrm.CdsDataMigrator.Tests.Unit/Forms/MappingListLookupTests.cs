using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Capgemini.Xrm.DataMigration.XrmToolBox.Core;
using Capgemini.Xrm.DataMigration.XrmToolBox.Services;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Exceptions;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Forms;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Moq;

namespace Capgemini.Xrm.CdsDataMigrator.Tests.Unit.Forms
{
    [TestClass]
    public class MappingListLookupTests
    {
        private Dictionary<string, Dictionary<string, List<string>>> mappings;
        private Mock<IOrganizationService> orgServiceMock;
        private Mock<IMetadataService> metadataServiceMock;
        private Mock<IExceptionService> dataMigratorExceptionHelperMock;

        private List<EntityMetadata> metadata;
        private string selectedValue;

        [TestInitialize]
        public void Setup()
        {
            orgServiceMock = new Mock<IOrganizationService>();
            metadataServiceMock = new Mock<IMetadataService>();
            dataMigratorExceptionHelperMock = new Mock<IExceptionService>();

            selectedValue = "samplekey";

            mappings = new Dictionary<string, Dictionary<string, List<string>>>();
            var values = new Dictionary<string, List<string>>
            {
                { "samplekey", new List<string>() { "contactattnoentity1" } }
            };
            mappings.Add(selectedValue, values);

            var entityMetadata = new EntityMetadata();
            var attributeList = new List<AttributeMetadata>()
            {
                new AttributeMetadata
                {
                    LogicalName = "contactattnoentity1"
                }
            };
            entityMetadata.LogicalName = "contact";

            var field = entityMetadata.GetType().GetRuntimeFields().First(a => a.Name == "_attributes");
            field.SetValue(entityMetadata, attributeList.ToArray());

            metadata = new List<EntityMetadata>
            {
                entityMetadata
            };
        }

        [TestMethod]
        public void MappingListLookupInstantiation()
        {
            FluentActions.Invoking(() => new MappingListLookup(
                                                                mappings,
                                                                orgServiceMock.Object,
                                                                new List<EntityMetadata>(),
                                                                selectedValue,
                                                                metadataServiceMock.Object,
                                                                dataMigratorExceptionHelperMock.Object))
                         .Should()
                         .NotThrow();
        }

        [TestMethod]
        public void MappingListLookupInstantiationNoMetaDataLogicalNameFails()
        {
            var entityMetadata = new EntityMetadata();
            var attributeList = new List<AttributeMetadata>()
            {
                new AttributeMetadata { LogicalName = "contactattnoentity1" }
            };

            var field = entityMetadata.GetType().GetRuntimeFields().First(a => a.Name == "_attributes");
            field.SetValue(entityMetadata, attributeList.ToArray());
            metadata = new List<EntityMetadata>
            {
                entityMetadata
            };

            FluentActions.Invoking(() => new MappingListLookup(
                                                                mappings,
                                                                orgServiceMock.Object,
                                                                metadata,
                                                                selectedValue,
                                                                metadataServiceMock.Object,
                                                                dataMigratorExceptionHelperMock.Object))
                         .Should()
                         .Throw<InvalidOperationException>()
                         .WithMessage("One or more items in the collection are null.");
        }

        [TestMethod]
        public void MappingListLookupInstantiationWithMetaDataLogicalNameSucceeds()
        {
            FluentActions.Invoking(() => new MappingListLookup(
                                                                mappings,
                                                                orgServiceMock.Object,
                                                                metadata,
                                                                selectedValue,
                                                                metadataServiceMock.Object,
                                                                dataMigratorExceptionHelperMock.Object))
                         .Should()
                         .NotThrow();
        }

        [TestMethod]
        public void RefreshMappingList()
        {
            string entityName = "contactattnoattributes";

            selectedValue = "contactattnoattributes1";

            mappings = new Dictionary<string, Dictionary<string, List<string>>>();
            var values = new Dictionary<string, List<string>>
            {
                { selectedValue, new List<string>() { entityName } }
            };
            mappings.Add(selectedValue, values);

            var attributeMetaDataItem = new UniqueIdentifierAttributeMetadata
            {
                LogicalName = selectedValue
            };

            var attributes = new List<AttributeMetadata>
            {
                attributeMetaDataItem
            };

            var entityMetadata = new EntityMetadata();
            var attributesField = entityMetadata.GetType().GetRuntimeFields().First(a => a.Name == "_attributes");
            attributesField.SetValue(entityMetadata, attributes.ToArray());

            metadataServiceMock.Setup(x => x.RetrieveEntities(It.IsAny<string>(), It.IsAny<IOrganizationService>(), It.IsAny<IExceptionService>()))
                .Returns(entityMetadata)
                .Verifiable();

            using (var systemUnderTest = new MappingListLookup(mappings, orgServiceMock.Object, metadata, selectedValue, metadataServiceMock.Object, dataMigratorExceptionHelperMock.Object))
            {
                systemUnderTest.LoadMappedItems();

                FluentActions.Invoking(() => systemUnderTest.RefreshMappingList())
                             .Should()
                             .NotThrow();
            }

            metadataServiceMock.Verify(x => x.RetrieveEntities(It.IsAny<string>(), It.IsAny<IOrganizationService>(), It.IsAny<IExceptionService>()), Times.Exactly(2));
        }

        [TestMethod]
        public void LoadMappedItemsSchemaLogicalnameNotinAttributeMetaData()
        {
            var attributeMetaDataItem = new AttributeMetadata
            {
                LogicalName = "contactattnoattributes1"
            };

            var attributes = new List<AttributeMetadata>
            {
                attributeMetaDataItem
            };

            var entityMetadata = new EntityMetadata();
            var field = entityMetadata.GetType().GetRuntimeFields().First(a => a.Name == "_attributes");
            field.SetValue(entityMetadata, attributes.ToArray());

            metadataServiceMock.Setup(x => x.RetrieveEntities(It.IsAny<string>(), It.IsAny<IOrganizationService>(), It.IsAny<IExceptionService>()))
                .Returns(entityMetadata)
                .Verifiable();

            using (var systemUnderTest = new MappingListLookup(mappings, orgServiceMock.Object, metadata, selectedValue, metadataServiceMock.Object, dataMigratorExceptionHelperMock.Object))
            {
                FluentActions.Invoking(() => systemUnderTest.LoadMappedItems())
                             .Should()
                             .Throw<MappingException>();
            }

            metadataServiceMock.VerifyAll();
        }

        [TestMethod]
        public void LoadMappedItemsUniqueidentifier()
        {
            string entityName = "contactattnoattributes";

            selectedValue = "contactattnoattributes1";

            mappings = new Dictionary<string, Dictionary<string, List<string>>>();
            var values = new Dictionary<string, List<string>>
            {
                { selectedValue, new List<string>() { entityName } }
            };
            mappings.Add(selectedValue, values);

            var attributeMetaDataItem = new UniqueIdentifierAttributeMetadata
            {
                LogicalName = selectedValue
            };

            var attributes = new List<AttributeMetadata>
            {
                attributeMetaDataItem
            };

            var entityMetadata = new EntityMetadata();
            var attributesField = entityMetadata.GetType().GetRuntimeFields().First(a => a.Name == "_attributes");
            attributesField.SetValue(entityMetadata, attributes.ToArray());

            metadataServiceMock.Setup(x => x.RetrieveEntities(It.IsAny<string>(), It.IsAny<IOrganizationService>(), It.IsAny<IExceptionService>()))
                .Returns(entityMetadata)
                .Verifiable();

            using (var systemUnderTest = new MappingListLookup(mappings, orgServiceMock.Object, metadata, selectedValue, metadataServiceMock.Object, dataMigratorExceptionHelperMock.Object))
            {
                FluentActions.Invoking(() => systemUnderTest.LoadMappedItems())
                             .Should()
                             .NotThrow();
            }

            metadataServiceMock.Verify(x => x.RetrieveEntities(It.IsAny<string>(), It.IsAny<IOrganizationService>(), It.IsAny<IExceptionService>()), Times.Exactly(2));
        }

        [TestMethod]
        public void LoadMappedItemsOwnerAttributeTypeCode()
        {
            string entityName = "contactattnoattributes";

            selectedValue = "contactattnoattributes1";

            mappings = new Dictionary<string, Dictionary<string, List<string>>>();
            var values = new Dictionary<string, List<string>>
            {
                { selectedValue, new List<string>() { entityName } }
            };
            mappings.Add(selectedValue, values);

            var attributeMetaDataItem = new AttributeMetadata
            {
                LogicalName = selectedValue
            };
            var attributeTypeField = attributeMetaDataItem.GetType().GetRuntimeFields().First(a => a.Name == "_attributeType");
            attributeTypeField.SetValue(attributeMetaDataItem, AttributeTypeCode.Owner);

            var attributes = new List<AttributeMetadata>
            {
                attributeMetaDataItem
            };

            var entityMetadata = new EntityMetadata();
            var attributesField = entityMetadata.GetType().GetRuntimeFields().First(a => a.Name == "_attributes");
            attributesField.SetValue(entityMetadata, attributes.ToArray());

            metadataServiceMock.Setup(x => x.RetrieveEntities(It.IsAny<string>(), It.IsAny<IOrganizationService>(), It.IsAny<IExceptionService>()))
                .Returns(entityMetadata)
                .Verifiable();

            using (var systemUnderTest = new MappingListLookup(mappings, orgServiceMock.Object, metadata, selectedValue, metadataServiceMock.Object, dataMigratorExceptionHelperMock.Object))
            {
                FluentActions.Invoking(() => systemUnderTest.LoadMappedItems())
                             .Should()
                             .NotThrow();
            }

            metadataServiceMock.Verify(x => x.RetrieveEntities(It.IsAny<string>(), It.IsAny<IOrganizationService>(), It.IsAny<IExceptionService>()), Times.Exactly(2));
        }

        [TestMethod]
        public void LoadMappedItemsLookupAttributeMetadata()
        {
            string entityName = "contactattnoattributes";

            selectedValue = "contactattnoattributes1";

            mappings = new Dictionary<string, Dictionary<string, List<string>>>();
            var values = new Dictionary<string, List<string>>
            {
                { selectedValue, new List<string>() { entityName } }
            };
            mappings.Add(selectedValue, values);

            var attributeMetaDataItem = new LookupAttributeMetadata
            {
                LogicalName = selectedValue
            };
            attributeMetaDataItem.Targets = new List<string> { selectedValue }.ToArray();

            var attributes = new List<AttributeMetadata>
            {
                attributeMetaDataItem
            };

            var entityMetadata = new EntityMetadata();
            var attributesField = entityMetadata.GetType().GetRuntimeFields().First(a => a.Name == "_attributes");
            attributesField.SetValue(entityMetadata, attributes.ToArray());

            metadataServiceMock.Setup(x => x.RetrieveEntities(It.IsAny<string>(), It.IsAny<IOrganizationService>(), It.IsAny<IExceptionService>()))
                .Returns(entityMetadata)
                .Verifiable();

            using (var systemUnderTest = new MappingListLookup(mappings, orgServiceMock.Object, metadata, selectedValue, metadataServiceMock.Object, dataMigratorExceptionHelperMock.Object))
            {
                FluentActions.Invoking(() => systemUnderTest.LoadMappedItems())
                             .Should()
                             .NotThrow();
            }

            metadataServiceMock.Verify(x => x.RetrieveEntities(It.IsAny<string>(), It.IsAny<IOrganizationService>(), It.IsAny<IExceptionService>()), Times.Exactly(2));
        }

        [TestMethod]
        public void ValidateLookupColumnLookupAttributeMetadata()
        {
            string entityName = "contactattnoattributes";

            mappings = new Dictionary<string, Dictionary<string, List<string>>>();
            var values = new Dictionary<string, List<string>>
            {
                { entityName, new List<string>() { entityName } }
            };
            mappings.Add(entityName, values);

            var attributeMetaDataItem = new LookupAttributeMetadata
            {
                LogicalName = entityName
            };
            attributeMetaDataItem.Targets = new List<string> { entityName }.ToArray();

            var attributes = new List<AttributeMetadata>
            {
                attributeMetaDataItem
            };

            var entityMetadata = new EntityMetadata();
            var attributesField = entityMetadata.GetType().GetRuntimeFields().First(a => a.Name == "_attributes");
            attributesField.SetValue(entityMetadata, attributes.ToArray());

            metadataServiceMock.Setup(x => x.RetrieveEntities(It.IsAny<string>(), It.IsAny<IOrganizationService>(), It.IsAny<IExceptionService>()))
                .Returns(entityMetadata)
                .Verifiable();

            int rowIndex = 0;

            using (var systemUnderTest = new MappingListLookup(mappings, orgServiceMock.Object, metadata, selectedValue, metadataServiceMock.Object, dataMigratorExceptionHelperMock.Object))
            {
                FluentActions.Invoking(() => systemUnderTest.ValidateLookupColumn(rowIndex, entityName, attributes.ToArray()))
                             .Should()
                             .NotThrow();
            }

            metadataServiceMock.Verify(x => x.RetrieveEntities(It.IsAny<string>(), It.IsAny<IOrganizationService>(), It.IsAny<IExceptionService>()), Times.Exactly(1));
        }

        [TestMethod]
        public void ValidateLookupColumnUniqueIdentifierAttributeMetadata()
        {
            string entityName = "contactattnoattributes";

            mappings = new Dictionary<string, Dictionary<string, List<string>>>();
            var values = new Dictionary<string, List<string>>
            {
                { entityName, new List<string>() { entityName } }
            };
            mappings.Add(entityName, values);

            var attributeMetaDataItem = new UniqueIdentifierAttributeMetadata
            {
                LogicalName = entityName
            };

            var attributes = new List<AttributeMetadata>
            {
                attributeMetaDataItem
            };

            var entityMetadata = new EntityMetadata();
            var attributesField = entityMetadata.GetType().GetRuntimeFields().First(a => a.Name == "_attributes");
            attributesField.SetValue(entityMetadata, attributes.ToArray());

            metadataServiceMock.Setup(x => x.RetrieveEntities(It.IsAny<string>(), It.IsAny<IOrganizationService>(), It.IsAny<IExceptionService>()))
                .Returns(entityMetadata)
                .Verifiable();

            int rowIndex = 0;

            using (var systemUnderTest = new MappingListLookup(mappings, orgServiceMock.Object, metadata, selectedValue, metadataServiceMock.Object, dataMigratorExceptionHelperMock.Object))
            {
                FluentActions.Invoking(() => systemUnderTest.ValidateLookupColumn(rowIndex, entityName, attributes.ToArray()))
                             .Should()
                             .NotThrow();
            }

            metadataServiceMock.Verify(x => x.RetrieveEntities(It.IsAny<string>(), It.IsAny<IOrganizationService>(), It.IsAny<IExceptionService>()), Times.Exactly(1));
        }

        [TestMethod]
        public void ValidateLookupColumnAttributeTypeCodeOwner()
        {
            string entityName = "contactattnoattributes";

            mappings = new Dictionary<string, Dictionary<string, List<string>>>();
            var values = new Dictionary<string, List<string>>
            {
                { entityName, new List<string>() { entityName } }
            };
            mappings.Add(selectedValue, values);

            var attributeMetaDataItem = new AttributeMetadata
            {
                LogicalName = entityName
            };
            var attributeTypeField = attributeMetaDataItem.GetType().GetRuntimeFields().First(a => a.Name == "_attributeType");
            attributeTypeField.SetValue(attributeMetaDataItem, AttributeTypeCode.Owner);

            var attributes = new List<AttributeMetadata>
            {
                attributeMetaDataItem
            };

            var entityMetadata = new EntityMetadata();
            var attributesField = entityMetadata.GetType().GetRuntimeFields().First(a => a.Name == "_attributes");
            attributesField.SetValue(entityMetadata, attributes.ToArray());

            metadataServiceMock.Setup(x => x.RetrieveEntities(It.IsAny<string>(), It.IsAny<IOrganizationService>(), It.IsAny<IExceptionService>()))
                .Returns(entityMetadata)
                .Verifiable();

            int rowIndex = 0;

            using (var systemUnderTest = new MappingListLookup(mappings, orgServiceMock.Object, metadata, selectedValue, metadataServiceMock.Object, dataMigratorExceptionHelperMock.Object))
            {
                FluentActions.Invoking(() => systemUnderTest.ValidateLookupColumn(rowIndex, entityName, attributes.ToArray()))
                             .Should()
                             .NotThrow();
            }

            metadataServiceMock.Verify(x => x.RetrieveEntities(It.IsAny<string>(), It.IsAny<IOrganizationService>(), It.IsAny<IExceptionService>()), Times.Exactly(1));
        }

        [TestMethod]
        public void ValidateLookupColumnUnsupportedAttributeTypeCode()
        {
            string entityName = "contactattnoattributes";

            mappings = new Dictionary<string, Dictionary<string, List<string>>>();
            var values = new Dictionary<string, List<string>>
            {
                { entityName, new List<string>() { entityName } }
            };
            mappings.Add(selectedValue, values);

            var attributeMetaDataItem = new AttributeMetadata
            {
                LogicalName = entityName
            };
            var attributeTypeField = attributeMetaDataItem.GetType().GetRuntimeFields().First(a => a.Name == "_attributeType");
            attributeTypeField.SetValue(attributeMetaDataItem, AttributeTypeCode.CalendarRules);

            var attributes = new List<AttributeMetadata>
            {
                attributeMetaDataItem
            };

            var entityMetadata = new EntityMetadata();
            var attributesField = entityMetadata.GetType().GetRuntimeFields().First(a => a.Name == "_attributes");
            attributesField.SetValue(entityMetadata, attributes.ToArray());

            metadataServiceMock.Setup(x => x.RetrieveEntities(It.IsAny<string>(), It.IsAny<IOrganizationService>(), It.IsAny<IExceptionService>()))
                .Returns(entityMetadata)
                .Verifiable();

            int rowIndex = 0;

            using (var systemUnderTest = new MappingListLookup(mappings, orgServiceMock.Object, metadata, selectedValue, metadataServiceMock.Object, dataMigratorExceptionHelperMock.Object))
            {
                FluentActions.Invoking(() => systemUnderTest.ValidateLookupColumn(rowIndex, entityName, attributes.ToArray()))
                             .Should()
                             .Throw<MappingException>();
            }

            metadataServiceMock.Verify(x => x.RetrieveEntities(It.IsAny<string>(), It.IsAny<IOrganizationService>(), It.IsAny<IExceptionService>()), Times.Never);
        }
    }
}