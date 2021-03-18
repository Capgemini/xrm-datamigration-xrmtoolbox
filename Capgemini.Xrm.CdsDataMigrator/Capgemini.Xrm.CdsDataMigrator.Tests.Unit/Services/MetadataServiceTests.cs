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

namespace Capgemini.Xrm.DataMigration.XrmToolBox.Services.Tests
{
    //    [TestClass]
    //    public class MetadataServiceTests
    //    {
    //        private Mock<IOrganizationService> serviceMock;

    //        private MetadataService systemUnderTest;

    //        [TestInitialize]
    //        public void Setup()

    //        {
    //            serviceMock = new Mock<IOrganizationService>();

    //            systemUnderTest = new MetadataService();
    //        }

    //    }

    [TestClass]
    public class MetadataServiceTests
    {
        private Mock<IOrganizationService> serviceMock;
        private List<EntityMetadata> metaDataList;
        private RetrieveAllEntitiesResponse entityResponse;

        private MetadataService systemUnderTest;

        [TestInitialize]
        public void Setup()
        {
            serviceMock = new Mock<IOrganizationService>();

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

            serviceMock.Setup(a => a.Execute(It.IsAny<OrganizationRequest>()))
                       .Returns(response);

            var actual = systemUnderTest.RetrieveEntities(serviceMock.Object);

            serviceMock.VerifyAll();
            actual.Should().NotBeNull();
        }

        [TestMethod]
        public void RetrieveEntitiesResponseWithMetaData()
        {
            serviceMock.Setup(a => a.Execute(It.IsAny<OrganizationRequest>()))
                       .Returns(entityResponse);

            var actual = systemUnderTest.RetrieveEntities(serviceMock.Object);

            serviceMock.VerifyAll();
            actual.Count.Should().Be(2);
        }

        //[Ignore("To be fixed!")]
        [TestMethod]
        public void RetrieveEntitiesInputLogicalNameNullService()
        {
            string logicalName = "account";

            FluentActions.Invoking(() => systemUnderTest.RetrieveEntities(logicalName, null))
                .Should()
                .Throw<ArgumentNullException>();
        }

        //[Ignore("To be fixed!")]
        [TestMethod]
        public void RetrieveEntitiesInputLogicalNameNotExistAndServiceNotNull()
        {
            string logicalName = Guid.NewGuid().ToString();// "account";

            var response = new RetrieveEntityResponse();

            serviceMock.Setup(a => a.Execute(It.IsAny<OrganizationRequest>())).Returns(response);

            var actual = systemUnderTest.RetrieveEntities(logicalName, serviceMock.Object);

            serviceMock.Verify(a => a.Execute(It.IsAny<OrganizationRequest>()), Times.Once);
            actual.Should().BeNull();
        }

        [TestMethod]
        public void RetrieveEntitiesInputLogicalNameExistsAndServiceNotNull()
        {
            string logicalName = "account";

            var response = new RetrieveEntityResponse();

            serviceMock.Setup(a => a.Execute(It.IsAny<OrganizationRequest>())).Returns(response);

            systemUnderTest.RetrieveEntities(logicalName, serviceMock.Object);

            var actual = systemUnderTest.RetrieveEntities(logicalName, serviceMock.Object);

            serviceMock.Verify(a => a.Execute(It.IsAny<OrganizationRequest>()));
            actual.Should().BeNull();
        }

        [TestMethod]
        public void RetrieveEntitiesOrgServiceReturnsEntityList()
        {
            var entityLogicalName = "testentity";
            var entityMetadata = new EntityMetadata
            {
                LogicalName = entityLogicalName,
                DisplayName = new Label
                {
                    UserLocalizedLabel = new LocalizedLabel { Label = "Test" }
                }
            };

            var response = new RetrieveAllEntitiesResponse
            {
                Results = new ParameterCollection
            {
                { "EntityMetadata", new List<EntityMetadata> { entityMetadata }.ToArray() }
            }
            };

            serviceMock.Setup(x => x.Execute(It.IsAny<RetrieveAllEntitiesRequest>()))
                       .Returns(response)
                       .Verifiable();

            var actual = systemUnderTest.RetrieveEntities(serviceMock.Object);

            actual.Count.Should().Be(1);
            serviceMock.VerifyAll();
        }

        [TestMethod]
        public void RetrieveEntitiesOrgServiceReturnsEmptyEntityList()
        {
            var response = new RetrieveAllEntitiesResponse
            {
                Results = new ParameterCollection
            {
                { "EntityMetadata", new List<EntityMetadata> { }.ToArray() }
            }
            };

            serviceMock.Setup(x => x.Execute(It.IsAny<RetrieveAllEntitiesRequest>()))
                       .Returns(response)
                       .Verifiable();

            var actual = systemUnderTest.RetrieveEntities(serviceMock.Object);

            actual.Count.Should().Be(0);
            serviceMock.VerifyAll();
        }

        [TestMethod]
        public void RetrieveEntitiesOrgServiceReturnsNullEntityList()
        {
            var response = new RetrieveAllEntitiesResponse
            {
                Results = new ParameterCollection
            {
                { "EntityMetadata", null }
            }
            };

            serviceMock.Setup(x => x.Execute(It.IsAny<RetrieveAllEntitiesRequest>()))
                       .Returns(response)
                       .Verifiable();

            var actual = systemUnderTest.RetrieveEntities(serviceMock.Object);

            actual.Count.Should().Be(0);
            serviceMock.VerifyAll();
        }

        [TestMethod]
        public void RetrieveEntitiesOrgServiceReturnsAnEntity()
        {
            var entityLogicalName = "testentity";

            var entityMetadata = new EntityMetadata
            {
                LogicalName = entityLogicalName,
                DisplayName = new Label
                {
                    UserLocalizedLabel = new LocalizedLabel { Label = "Test" }
                }
            };

            var response = new RetrieveEntityResponse
            {
                Results = new ParameterCollection
            {
                { "EntityMetadata", entityMetadata }
            }
            };

            serviceMock.Setup(x => x.Execute(It.IsAny<RetrieveEntityRequest>()))
                    .Returns(response)
                    .Verifiable();

            var actual = systemUnderTest.RetrieveEntities(entityLogicalName, serviceMock.Object);

            actual.LogicalName.Should().Be(entityLogicalName);
            serviceMock.VerifyAll();
        }

        [TestMethod]
        public void RetrieveEntitiesOrgServiceDoesNotReturnAnEntity()
        {
            var entityLogicalName = Guid.NewGuid().ToString();

            var response = new RetrieveEntityResponse
            {
                Results = new ParameterCollection
            {
                { "EntityMetadata", null }
            }
            };

            serviceMock.Setup(x => x.Execute(It.IsAny<RetrieveEntityRequest>()))
                    .Returns(response)
                    .Verifiable();

            var actual = systemUnderTest.RetrieveEntities(entityLogicalName, serviceMock.Object);

            actual.Should().BeNull();
            serviceMock.VerifyAll();
        }
    }
}