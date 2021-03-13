using System;
using System.Collections.Generic;
using Capgemini.Xrm.DataMigration.XrmToolBox.Services;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Moq;

namespace Capgemini.Xrm.CdsDataMigrator.Tests.Unit.Core
{
    [TestClass]
    public class MetadataServiceTests
    {
        private Mock<IOrganizationService> mockService;
        private List<EntityMetadata> metaDataList;
        private RetrieveAllEntitiesResponse entityResponse;

        private MetadataService systemUnderTest;

        [TestInitialize]
        public void Setup()
        {
            mockService = new Mock<IOrganizationService>();

            systemUnderTest = new MetadataService();

            metaDataList = new List<EntityMetadata>
            {
                new EntityMetadata { DisplayName = new Label("Test1", 0) { UserLocalizedLabel = new LocalizedLabel("Test1", 0) } },
                new EntityMetadata { DisplayName = new Label("Test2", 0) { UserLocalizedLabel = new LocalizedLabel("Test2", 0) } }
            };

            entityResponse = new RetrieveAllEntitiesResponse();
            entityResponse.Results.Add(new KeyValuePair<string, object>("EntityMetadata", metaDataList.ToArray()));
        }

        [TestMethod]
        public void RetrieveEntitiesNullOrganizationService()
        {
            var actual = systemUnderTest.RetrieveEntities(null);

            actual.Should().BeOfType<List<EntityMetadata>>();
            actual.Count.Should().Be(0);
        }

        [TestMethod]
        public void RetrieveEntitiesEmptyResponse()
        {
            var response = new RetrieveAllEntitiesResponse();

            mockService.Setup(a => a.Execute(It.IsAny<OrganizationRequest>()))
                       .Returns(response);

            var actual = systemUnderTest.RetrieveEntities(mockService.Object);

            mockService.VerifyAll();
            actual.Should().NotBeNull();
        }

        [TestMethod]
        public void RetrieveEntitiesResponseWithMetaData()
        {
            mockService.Setup(a => a.Execute(It.IsAny<OrganizationRequest>()))
                       .Returns(entityResponse);

            var actual = systemUnderTest.RetrieveEntities(mockService.Object);

            mockService.VerifyAll();
            actual.Count.Should().Be(2);
        }

        [Ignore("To be fixed!")]
        [TestMethod]
        public void RetrieveEntitiesInputLogicalNameNullService()
        {
            string logicalName = "account";

            FluentActions.Invoking(() => systemUnderTest.RetrieveEntities(logicalName, null))
                .Should()
                .Throw<ArgumentNullException>();
        }

        [Ignore("To be fixed!")]
        [TestMethod]
        public void RetrieveEntitiesInputLogicalNameNotExistAndServiceNotNull()
        {
            string logicalName = "account";

            var response = new RetrieveEntityResponse();

            mockService.Setup(a => a.Execute(It.IsAny<OrganizationRequest>())).Returns(response);

            var actual = systemUnderTest.RetrieveEntities(logicalName, mockService.Object);

            mockService.Verify(a => a.Execute(It.IsAny<OrganizationRequest>()), Times.Once);
            actual.Should().BeNull();
        }
    }
}