using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Capgemini.Xrm.CdsDataMigratorLibrary.Exceptions;
using Capgemini.Xrm.CdsDataMigratorLibrary.Models;
using Capgemini.Xrm.CdsDataMigratorLibrary.Services;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Moq;

namespace Capgemini.Xrm.CdsDataMigrator.Tests.Unit
{
    public abstract class TestBase
    {
        protected Mock<IOrganizationService> ServiceMock { get; set; }

        protected Mock<IMetadataService> MetadataServiceMock { get; set; }

        protected Mock<INotificationService> NotificationServiceMock { get; set; }

        protected Mock<IExceptionService> ExceptionServicerMock { get; set; }

        protected static void InsertManyToManyRelationshipMetadata(EntityMetadata entityMetadata, ManyToManyRelationshipMetadata relationship)
        {
            var manyToManyRelationshipMetadataList = new List<ManyToManyRelationshipMetadata>
            {
                relationship
            };

            var field = entityMetadata?.GetType().GetRuntimeFields().First(a => a.Name == "_manyToManyRelationships");
            field.SetValue(entityMetadata, manyToManyRelationshipMetadataList.ToArray());
        }

        protected static EntityMetadata InstantiateEntityMetaData(string logicalName)
        {
            var entityMetadata = new EntityMetadata
            {
                LogicalName = logicalName,
                DisplayName = new Label
                {
                    UserLocalizedLabel = new LocalizedLabel { Label = logicalName }
                }
            };

            return entityMetadata;
        }

        protected static void InsertAttributeList(EntityMetadata entityMetadata, List<string> attributeLogicalNames)
        {
            var attributeList = new List<AttributeMetadata>();

            if (attributeLogicalNames != null)
            {
                foreach (var item in attributeLogicalNames)
                {
                    var attribute = new AttributeMetadata
                    {
                        LogicalName = item,
                        DisplayName = new Label
                        {
                            UserLocalizedLabel = new LocalizedLabel { Label = item }
                        }
                    };

                    var attributeTypeName = attribute.GetType().GetRuntimeFields().First(a => a.Name == "_attributeTypeDisplayName");
                    attributeTypeName.SetValue(attribute, new AttributeTypeDisplayName { Value = item });

                    attributeList.Add(attribute);
                }

                var field = entityMetadata?.GetType().GetRuntimeFields().First(a => a.Name == "_attributes");
                field.SetValue(entityMetadata, attributeList.ToArray());

                var isIntersectField = entityMetadata.GetType().GetRuntimeFields().First(a => a.Name == "_isIntersect");
                isIntersectField.SetValue(entityMetadata, (bool?)false);

                var isLogicalEntityField = entityMetadata.GetType().GetRuntimeFields().First(a => a.Name == "_isLogicalEntity");
                isLogicalEntityField.SetValue(entityMetadata, (bool?)false);

                var isCustomEntityField = entityMetadata.GetType().GetRuntimeFields().First(a => a.Name == "_isCustomEntity");
                isCustomEntityField.SetValue(entityMetadata, (bool?)true);
            }
        }

        protected void SetupMockObjects(string entityLogicalName)
        {
            var entityMetadata = InstantiateEntityMetaData(entityLogicalName);

            InsertAttributeList(entityMetadata, new List<string> { "contactattnoentity1" });

            var metadataList = new List<EntityMetadata>
            {
                entityMetadata
            };

            MetadataServiceMock.Setup(x => x.RetrieveEntities(It.IsAny<string>(), It.IsAny<IOrganizationService>(), It.IsAny<IExceptionService>()))
                                .Returns(entityMetadata)
                                .Verifiable();

            MetadataServiceMock.Setup(x => x.RetrieveEntities(It.IsAny<IOrganizationService>()))
                                .Returns(metadataList)
                                .Verifiable();
        }

        protected ServiceParameters GenerateMigratorParameters()
        {
            return new ServiceParameters(ServiceMock.Object, MetadataServiceMock.Object, NotificationServiceMock.Object, ExceptionServicerMock.Object);
        }

        protected void SetupServiceMocks()
        {
            ServiceMock = new Mock<IOrganizationService>();
            MetadataServiceMock = new Mock<IMetadataService>();
            NotificationServiceMock = new Mock<INotificationService>();
            ExceptionServicerMock = new Mock<IExceptionService>();
        }
    }
}