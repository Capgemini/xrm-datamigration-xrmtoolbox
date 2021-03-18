using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capgemini.Xrm.DataMigration.XrmToolBox.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using FluentAssertions;
using Moq;

namespace Capgemini.Xrm.DataMigration.XrmToolBox.Services.Tests
{
    [TestClass]
    public class EntityRepositoryServiceTests
    {
        private Mock<IOrganizationService> serviceMock;

        private MetadataService systemUnderTest;

        [TestInitialize]
        public void Setup()
        {
            serviceMock = new Mock<IOrganizationService>();
        }

        [TestMethod]
        public void EntityRepositoryServiceTest()
        {
            var systemUnderTest = new EntityRepositoryService(serviceMock.Object);

            systemUnderTest.Should().NotBeNull();
        }

        [TestMethod]
        public void InstantiateEntityRepository()
        {
            var systemUnderTest = new EntityRepositoryService(serviceMock.Object);

            var actual = systemUnderTest.InstantiateEntityRepository(false);

            actual.Should().NotBeNull();
        }

        [TestMethod]
        public void InstantiateEntityRepositoryCloneConnection()
        {
            var systemUnderTest = new EntityRepositoryService(serviceMock.Object);

            FluentActions.Invoking(() => systemUnderTest.InstantiateEntityRepository(true))
                         .Should()
                         .Throw<InvalidCastException>();
        }
    }
}