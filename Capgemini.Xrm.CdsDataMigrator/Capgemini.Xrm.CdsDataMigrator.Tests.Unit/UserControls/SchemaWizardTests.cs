using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Capgemini.Xrm.DataMigration.XrmToolBox.Services;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Moq;

namespace Capgemini.Xrm.CdsDataMigrator.Tests.Unit.UserControls
{
    [TestClass]
    public class SchemaWizardTests
    {
        private Mock<IOrganizationService> serviceMock;
        private Mock<IMetadataService> metadataServiceMock;

        [TestInitialize]
        public void Setup()
        {
            serviceMock = new Mock<IOrganizationService>();
            metadataServiceMock = new Mock<IMetadataService>();
        }

        [TestMethod]
        public void SchemaGeneratorInstatiation()
        {
            FluentActions.Invoking(() => new SchemaWizard())
                        .Should()
                        .NotThrow();
        }

        //[Ignore("Will have to fix!")]
        [TestMethod]
        public void OnConnectionUpdated()
        {
            string entityLogicalName = "account_contact";
            SetupMockObjects(entityLogicalName);

            using (var systemUnderTest = new SchemaWizard())
            {
                systemUnderTest.OrganizationService = serviceMock.Object;
                systemUnderTest.MetadataService = metadataServiceMock.Object;

                FluentActions.Invoking(() => systemUnderTest.OnConnectionUpdated(Guid.NewGuid(), "TestOrg"))
                        .Should()
                        .NotThrow();
            }
        }

        [TestMethod]
        public void GetEntityLogicalName()
        {
            using (var systemUnderTest = new SchemaWizard())
            {
                systemUnderTest.OrganizationService = serviceMock.Object;

                FluentActions.Invoking(() => systemUnderTest.GetEntityLogicalName())
                        .Should()
                        .NotThrow();
            }
        }

        [TestMethod]
        public void ProcessListViewEntitiesSelectedIndexChanged()
        {
            using (var systemUnderTest = new SchemaWizard())
            {
                systemUnderTest.OrganizationService = serviceMock.Object;
                systemUnderTest.MetadataService = metadataServiceMock.Object;

                FluentActions.Invoking(() => systemUnderTest.ProcessListViewEntitiesSelectedIndexChanged(metadataServiceMock.Object))
                        .Should()
                        .NotThrow();
            }
        }

        [TestMethod]
        public void RetrieveSourceEntitiesListShowSystemAttributes()
        {
            var showSystemAttributes = true;
            string entityLogicalName = "account_contact";
            SetupMockObjects(entityLogicalName);

            using (var systemUnderTest = new SchemaWizard())
            {
                systemUnderTest.OrganizationService = serviceMock.Object;
                systemUnderTest.MetadataService = metadataServiceMock.Object;

                var actual = systemUnderTest.RetrieveSourceEntitiesList(showSystemAttributes, serviceMock.Object, metadataServiceMock.Object);

                actual.Count.Should().Be(1);
            }
        }

        [TestMethod]
        public void PopulateRelationshipActionNoManyToManyRelationships()
        {
            string entityLogicalName = "contact";
            var entityMetadata = new EntityMetadata();

            metadataServiceMock.Setup(x => x.RetrieveEntities(It.IsAny<string>(), It.IsAny<IOrganizationService>()))
                .Returns(entityMetadata)
                .Verifiable();

            using (var systemUnderTest = new SchemaWizard())
            {
                systemUnderTest.OrganizationService = serviceMock.Object;
                systemUnderTest.MetadataService = metadataServiceMock.Object;

                var actual = systemUnderTest.PopulateRelationshipAction(entityLogicalName, serviceMock.Object, metadataServiceMock.Object);

                actual.Count.Should().Be(0);
            }

            serviceMock.VerifyAll();
            metadataServiceMock.VerifyAll();
        }

        [Ignore("Will have to fix!")]
        [TestMethod]
        public void PopulateRelationshipAction()
        {
            string entityLogicalName = "account_contact";
            var entityMetadata = new EntityMetadata();

            var relationship = new ManyToManyRelationshipMetadata
            {
                Entity1LogicalName = "account",
                Entity1IntersectAttribute = "accountid",
                IntersectEntityName = "account_contact",
                Entity2LogicalName = "contact",
                Entity2IntersectAttribute = "contactid"
            };

            var manyToManyRelationshipMetadataList = new List<ManyToManyRelationshipMetadata>
            {
                relationship
            };

            var field = entityMetadata.GetType().GetRuntimeFields().First(a => a.Name == "_manyToManyRelationships");
            field.SetValue(entityMetadata, manyToManyRelationshipMetadataList.ToArray());

            metadataServiceMock.Setup(x => x.RetrieveEntities(It.IsAny<string>(), It.IsAny<IOrganizationService>()))
                .Returns(entityMetadata)
                .Verifiable();

            using (var systemUnderTest = new SchemaWizard())
            {
                systemUnderTest.OrganizationService = serviceMock.Object;
                systemUnderTest.MetadataService = metadataServiceMock.Object;
                systemUnderTest.GetEntityLogicalName();

                var actual = systemUnderTest.PopulateRelationshipAction(entityLogicalName, serviceMock.Object, metadataServiceMock.Object);

                actual.Count.Should().BeGreaterThan(0);
            }

            serviceMock.VerifyAll();
            metadataServiceMock.VerifyAll();
        }

        [TestMethod]
        public void PopulateRelationship()
        {
            string entityLogicalName = "contact";

            using (var systemUnderTest = new SchemaWizard())
            {
                systemUnderTest.OrganizationService = serviceMock.Object;
                systemUnderTest.MetadataService = metadataServiceMock.Object;

                FluentActions.Invoking(() => systemUnderTest.PopulateRelationship(entityLogicalName, serviceMock.Object, metadataServiceMock.Object))
                        .Should()
                        .NotThrow();
            }

            serviceMock.VerifyAll();
            metadataServiceMock.VerifyAll();
        }

        private void SetupMockObjects(string entityLogicalName)
        {
            var entityMetadata = new EntityMetadata
            {
                LogicalName = entityLogicalName,
                DisplayName = new Label
                {
                    UserLocalizedLabel = new LocalizedLabel { Label = "Test" }
                }
            };

            var attributeList = new List<AttributeMetadata>()
            {
                new AttributeMetadata { LogicalName = "contactattnoentity1" }
            };

            var field = entityMetadata.GetType().GetRuntimeFields().First(a => a.Name == "_attributes");
            field.SetValue(entityMetadata, attributeList.ToArray());

            var isIntersectField = entityMetadata.GetType().GetRuntimeFields().First(a => a.Name == "_isIntersect");
            isIntersectField.SetValue(entityMetadata, (bool?)false);

            var isLogicalEntityField = entityMetadata.GetType().GetRuntimeFields().First(a => a.Name == "_isLogicalEntity");
            isLogicalEntityField.SetValue(entityMetadata, (bool?)false);

            var isCustomEntityField = entityMetadata.GetType().GetRuntimeFields().First(a => a.Name == "_isCustomEntity");
            isCustomEntityField.SetValue(entityMetadata, (bool?)true);

            var metadataList = new List<EntityMetadata>
            {
                entityMetadata
            };

            metadataServiceMock.Setup(x => x.RetrieveEntities(It.IsAny<string>(), It.IsAny<IOrganizationService>()))
                                .Returns(entityMetadata)
                                .Verifiable();

            metadataServiceMock.Setup(x => x.RetrieveEntities(It.IsAny<IOrganizationService>()))
                                .Returns(metadataList)
                                .Verifiable();
        }
    }
}