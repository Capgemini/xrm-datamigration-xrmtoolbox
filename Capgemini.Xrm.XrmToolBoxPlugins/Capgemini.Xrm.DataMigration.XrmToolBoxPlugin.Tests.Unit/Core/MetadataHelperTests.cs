using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Moq;
using System.Collections.Generic;
using FluentAssertions;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core.Tests
{
    [TestClass]
    public class MetadataHelperTests
    {
        private Mock<IOrganizationService> organizationServiceMock;

        [TestInitialize]
        public void Setup()
        {
            organizationServiceMock = new Mock<IOrganizationService>();
        }

        [TestMethod]
        public void RetrieveEntities()
        {
            var response = GenerateResponse(out List<EntityMetadata> entityMetadata);

            organizationServiceMock.Setup(a => a.Execute(It.IsAny<RetrieveAllEntitiesRequest>()))
                                   .Returns(response);

            var actual = MetadataHelper.RetrieveEntities(organizationServiceMock.Object);

            actual.Count.Should().Be(entityMetadata.Count);
        }

        [TestMethod]
        public void RetrieveEntitiesEntityNotAlreadyInCache()
        {
            var logicalName = "TestEntity1234";

            var metadata = new EntityMetadata
            {
                DisplayName = new Label(logicalName, 234)
                {
                    UserLocalizedLabel = new LocalizedLabel(logicalName, 234)
                }
            };

            var retrieveEntityResponse = new RetrieveEntityResponse
            {
                Results = new ParameterCollection
                    {
                        new KeyValuePair<string, object>("EntityMetadata", metadata)
                    }
            };

            organizationServiceMock.Setup(a => a.Execute(It.IsAny<RetrieveEntityRequest>()))
                                   .Returns(retrieveEntityResponse);

            var actual = MetadataHelper.RetrieveEntities(logicalName, organizationServiceMock.Object);

            actual.DisplayName.UserLocalizedLabel.Label.Should().Be(logicalName);
        }

        private static RetrieveAllEntitiesResponse GenerateResponse(out List<EntityMetadata> entityMetadata)
        {
            var response = new RetrieveAllEntitiesResponse();
            entityMetadata = new List<EntityMetadata>();
            for (int i = 0; i < 5; i++)
            {
                var metaData = new EntityMetadata
                {
                    DisplayName = new Label($"TestEntity{i}", 234)
                    {
                        UserLocalizedLabel = new LocalizedLabel($"TestEntity{i}", 234)
                    }
                };

                entityMetadata.Add(metaData);
            }

            response.Results = new ParameterCollection
            {
                new KeyValuePair<string, object>("EntityMetadata", entityMetadata.ToArray())
            };

            return response;
        }
    }
}