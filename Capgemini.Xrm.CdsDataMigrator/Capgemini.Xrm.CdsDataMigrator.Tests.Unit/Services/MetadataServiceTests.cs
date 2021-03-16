using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capgemini.Xrm.DataMigration.XrmToolBox.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Moq;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using System.Reflection;
using FluentAssertions;
using McTools.Xrm.Connection;

namespace Capgemini.Xrm.DataMigration.XrmToolBox.Services.Tests
{
    [TestClass]
    public class MetadataServiceTests
    {
        private Mock<IOrganizationService> serviceMock;

        private MetadataService systemUnderTest;

        [TestInitialize]
        public void Setup()

        {
            serviceMock = new Mock<IOrganizationService>();

            systemUnderTest = new MetadataService();
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