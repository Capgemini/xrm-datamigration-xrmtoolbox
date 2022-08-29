using Capgemini.Xrm.CdsDataMigrator.Tests.Unit;
using Capgemini.Xrm.CdsDataMigratorLibrary.UserControls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Microsoft.Xrm.Sdk.Metadata;
using System.Collections.Generic;
using System.Linq;
using Capgemini.Xrm.CdsDataMigratorLibrary.Tests.Unit.Mocks;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Tests.Unit.UserControls
{
    [TestClass]
    public class EntityListViewTests : TestBase
    {
        [TestMethod]
        public void SetEntitiesToNull()
        {
            using (var systemUnderTest = new EntityListView())
            {
                systemUnderTest.Entities = null;

                systemUnderTest.Entities.Should().BeNull();
            }
        }

        [TestMethod]
        public void SetEntitiesToEntityMetadata()
        {
            var entityCount = 10;
            var entityList = new List<EntityMetadata>();

            for (int counter = 0; counter < entityCount; counter++)
            {
                entityList.Add(InstantiateEntityMetaData($"TestEnity{counter}"));
            }


            using (var systemUnderTest = new EntityListView())
            {
                systemUnderTest.Entities = entityList;

                systemUnderTest.Entities.Count.Should().Be(entityCount);
                systemUnderTest.Entities
                        .All(x => x.GetType().Name == "EntityMetadata")
                        .Should()
                        .BeTrue();
            }
        }

        [TestMethod]
        public void SelectedEntitiesWhenEntitiesIsNull()
        {
            using (var systemUnderTest = new EntityListView())
            {
                systemUnderTest.Entities = null;

                var actual = systemUnderTest.SelectedEntities;

                actual.Count.Should().Be(0);
            }
        }


        [TestMethod]
        public void SelectedEntitiesWhenEntitiesIsNotNullAndNoItemSelected()
        {
            var entityCount = 10;
            var entityList = new List<EntityMetadata>();

            for (int counter = 0; counter < entityCount; counter++)
            {
                entityList.Add(InstantiateEntityMetaData($"TestEnity{counter}"));
            }

            using (var systemUnderTest = new EntityListView())
            {
                systemUnderTest.Entities = entityList;

                var actual = systemUnderTest.SelectedEntities;

                actual.Count.Should().Be(0);
            }
        }

        [TestMethod]
        public void SelectedEntitiesWhenEntitiesIsNotNullAndItemSelected()
        {
            var entityCount = 10;
            var nodesToSelect = new List<int> { 3, 7, 9 };
            var entityList = new List<EntityMetadata>();

            for (int counter = 0; counter < entityCount; counter++)
            {
                entityList.Add(InstantiateEntityMetaData($"TestEnity{counter}"));
            }

            using (var systemUnderTest = new MockEntityListView())
            {
                systemUnderTest.Entities = entityList;
                systemUnderTest.SelectTreeViewNodes(nodesToSelect);

                // Act
                var actual = systemUnderTest.SelectedEntities;

                actual.Count.Should().Be(nodesToSelect.Count);
            }
        }
    }
}
