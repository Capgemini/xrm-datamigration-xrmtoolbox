using System;
using System.Collections.Generic;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Forms;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;

namespace Capgemini.Xrm.CdsDataMigrator.Tests.Unit.Forms
{
    [TestClass]
    public class MappingListTests
    {
        private List<Item<EntityReference, EntityReference>> mappings;

        [TestInitialize]
        public void Setup()
        {
            mappings = new List<Item<EntityReference, EntityReference>>();
        }

        [TestMethod]
        public void MappingListInstantiation()
        {
            FluentActions.Invoking(() => new MappingList(mappings))
                         .Should()
                         .NotThrow();
        }

        [TestMethod]
        public void GetMappingListEmptyEntityLogicalName()
        {
            string entityLogicalName = string.Empty;
            using (var systemUnderTest = new MappingList(mappings))
            {
                var actual = systemUnderTest.GetMappingList(entityLogicalName);

                actual.Count.Should().Be(0);
            }
        }

        [TestMethod]
        public void GetMappingListPopulatedEntityLogicalName()
        {
            var entityLogicalName = "Acoount";
            var entityRef = new EntityReference(entityLogicalName);

            var mapping = new Item<EntityReference, EntityReference>(entityRef, entityRef);
            mappings.Add(mapping);

            using (var systemUnderTest = new MappingList(mappings))
            {
                systemUnderTest.PopulateMappingGrid();

                var actual = systemUnderTest.GetMappingList(entityLogicalName);

                actual.Count.Should().Be(1);
            }
        }

        [TestMethod]
        public void PopulateMappingGrid()
        {
            var entityLogicalName = "Acoount";
            var entityRef = new EntityReference(entityLogicalName);

            var mapping = new Item<EntityReference, EntityReference>(entityRef, entityRef);
            mappings.Add(mapping);

            using (var systemUnderTest = new MappingList(mappings))
            {
                FluentActions.Invoking(() => systemUnderTest.PopulateMappingGrid())
                             .Should()
                             .NotThrow();
            }
        }

        [TestMethod]
        public void GetGuidMappingList()
        {
            var totalEntityReferences = 5;

            for (int i = 0; i < totalEntityReferences; i++)
            {
                var entityRef = new EntityReference($"TestEntity{i}", Guid.NewGuid());
                var mapping = new Item<EntityReference, EntityReference>(entityRef, entityRef);
                mappings.Add(mapping);
            }

            using (var systemUnderTest = new MappingList(mappings))
            {
                systemUnderTest.PopulateMappingGrid();

                var actual = systemUnderTest.GetGuidMappingList();

                actual.Count.Should().Be(totalEntityReferences);
            }
        }

        [TestMethod]
        public void PerformMappingsCellValidationFormatedValueIsNotNullForClEntityColumn()
        {
            for (int i = 0; i < 5; i++)
            {
                var entityRef = new EntityReference($"TestEntity{i}", Guid.NewGuid());
                var mapping = new Item<EntityReference, EntityReference>(entityRef, entityRef);
                mappings.Add(mapping);
            }

            var column = $"clEntity";
            var entityRef1 = new EntityReference($"clEntity", Guid.NewGuid());
            mappings.Add(new Item<EntityReference, EntityReference>(entityRef1, entityRef1));

            using (var systemUnderTest = new MappingList(mappings))
            {
                systemUnderTest.PopulateMappingGrid();

                var actual = systemUnderTest.PerformMappingsCellValidation(column, entityRef1.Id, 6, 0);

                actual.Should().BeFalse();
            }
        }

        [TestMethod]
        public void PerformMappingsCellValidationFormatedValueIsNullForClEntityColumn()
        {
            for (int i = 0; i < 5; i++)
            {
                var entityRef = new EntityReference($"TestEntity{i}", Guid.NewGuid());
                var mapping = new Item<EntityReference, EntityReference>(entityRef, entityRef);
                mappings.Add(mapping);
            }

            var column = $"clEntity";
            var entityRef1 = new EntityReference($"clEntity", Guid.NewGuid());
            mappings.Add(new Item<EntityReference, EntityReference>(entityRef1, entityRef1));

            using (var systemUnderTest = new MappingList(mappings))
            {
                systemUnderTest.PopulateMappingGrid();

                var actual = systemUnderTest.PerformMappingsCellValidation(column, null, 6, 0);

                actual.Should().BeTrue();
            }
        }

        [TestMethod]
        public void PerformMappingsCellValidationFormatedValueIsNotNullForIdColumn()
        {
            for (int i = 0; i < 5; i++)
            {
                var entityRef = new EntityReference($"TestEntity{i}", Guid.NewGuid());
                var mapping = new Item<EntityReference, EntityReference>(entityRef, entityRef);
                mappings.Add(mapping);
            }

            var column = $"Id";

            using (var systemUnderTest = new MappingList(mappings))
            {
                systemUnderTest.PopulateMappingGrid();

                var actual = systemUnderTest.PerformMappingsCellValidation(column, mappings[0].Value.Id, 6, 0);

                actual.Should().BeFalse();
            }
        }

        [TestMethod]
        public void PerformMappingsCellValidationFormatedValueIsNullForIdColumn()
        {
            for (int i = 0; i < 5; i++)
            {
                var entityRef = new EntityReference($"TestEntity{i}", Guid.NewGuid());
                var mapping = new Item<EntityReference, EntityReference>(entityRef, entityRef);
                mappings.Add(mapping);
            }

            var column = $"Id";

            using (var systemUnderTest = new MappingList(mappings))
            {
                systemUnderTest.PopulateMappingGrid();

                var actual = systemUnderTest.PerformMappingsCellValidation(column, null, 6, 0);

                actual.Should().BeTrue();
            }
        }
    }
}