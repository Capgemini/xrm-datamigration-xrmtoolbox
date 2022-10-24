using System.Collections.Generic;
using Capgemini.Xrm.CdsDataMigratorLibrary.Exceptions;
using Capgemini.Xrm.CdsDataMigratorLibrary.Extensions;
using Capgemini.Xrm.CdsDataMigratorLibrary.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Moq;

namespace Capgemini.Xrm.CdsDataMigrator.Tests.Unit.Model
{
    [TestClass]
    public class ServiceParametersTests : TestBase
    {
        private Dictionary<string, HashSet<string>> inputEntityRelationships;
        private Dictionary<string, HashSet<string>> inputEntityAttributes;

        
        private ServiceParameters systemUnderTest;

        [TestInitialize]
        public void Setup()
        {
            SetupServiceMocks();
            inputEntityRelationships = new Dictionary<string, HashSet<string>>();
            inputEntityAttributes = new Dictionary<string, HashSet<string>>();

            systemUnderTest = new ServiceParameters(ServiceMock.Object, MetadataServiceMock.Object, NotificationServiceMock.Object, ExceptionServicerMock.Object);
        }

        [TestMethod]
        public void CanInstantiate()
        {
            systemUnderTest.OrganizationService.Should().NotBeNull();
            systemUnderTest.MetadataService.Should().NotBeNull();
            systemUnderTest.NotificationService.Should().NotBeNull();
            systemUnderTest.ExceptionService.Should().NotBeNull();
        }

        [TestMethod]
        public void RetrieveSourceEntitiesListShowSystemAttributesIsFalse()
        {
            var showSystemAttributes = false;
            string entityLogicalName = "account_contact";
            SetupMockObjects(entityLogicalName);
            var inputCachedMetadata = new List<EntityMetadata>();

            //var serviceParameters = GenerateMigratorParameters();

            var actual = systemUnderTest.RetrieveSourceEntitiesList(showSystemAttributes, inputCachedMetadata, inputEntityAttributes);

            actual.Count.Should().Be(1);
        }

        [TestMethod]
        public void RetrieveSourceEntitiesListShowSystemAttributesIsTrue()
        {
            var showSystemAttributes = true;
            string entityLogicalName = "account_contact";
            SetupMockObjects(entityLogicalName);
            var inputCachedMetadata = new List<EntityMetadata>();
            //var serviceParameters = GenerateMigratorParameters();

            var actual = systemUnderTest.RetrieveSourceEntitiesList(showSystemAttributes, inputCachedMetadata, inputEntityAttributes);

            actual.Count.Should().Be(1);
        }

        [TestMethod]
        public void PopulateRelationshipActionNoManyToManyRelationships()
        {
            string entityLogicalName = "contact";
            var entityMetadata = new EntityMetadata();

            var migratorServiceParameters = GenerateMigratorParameters();

            MetadataServiceMock.Setup(x => x.RetrieveEntities(It.IsAny<string>(), It.IsAny<IOrganizationService>(), It.IsAny<IExceptionService>()))
                .Returns(entityMetadata)
                .Verifiable();

            var actual = systemUnderTest.PopulateRelationshipAction(entityLogicalName, inputEntityRelationships);

            actual.Count.Should().Be(0);

            ServiceMock.VerifyAll();
            MetadataServiceMock.VerifyAll();
        }

        [TestMethod]
        public void GetAttributeList()
        {
            string entityLogicalName = "contact"; 
            var entityMetadata = new EntityMetadata();
            bool showSystemAttributes = true;
            var serviceParameters = GenerateMigratorParameters();

            MetadataServiceMock.Setup(x => x.RetrieveEntities(It.IsAny<string>(), It.IsAny<IOrganizationService>(), It.IsAny<IExceptionService>()))
                                .Returns(entityMetadata)
                                .Verifiable();

            var actual = systemUnderTest.GetAttributeList(entityLogicalName, showSystemAttributes );

            actual.Should().BeNull();
        }

        [TestMethod]
        public void GetAttributeListMetaDataServiceReturnsEnities()
        {
            string entityLogicalName = "contact"; 
            bool showSystemAttributes = true;

            var serviceParameters = GenerateMigratorParameters();

            var entityMetadata = new EntityMetadata();
            InsertAttributeList(entityMetadata, new List<string> { "contactattnoentity1" });

            MetadataServiceMock.Setup(x => x.RetrieveEntities(It.IsAny<string>(), It.IsAny<IOrganizationService>(), It.IsAny<IExceptionService>()))
                                .Returns(entityMetadata)
                                .Verifiable();

            var actual = systemUnderTest.GetAttributeList(entityLogicalName, showSystemAttributes  );

            actual.Should().NotBeNull();
        }

    }
}