using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using FluentAssertions;
using Capgemini.Xrm.CdsDataMigrator.Tests.Unit;
using Capgemini.Xrm.CdsDataMigratorLibrary.Core;
using Capgemini.Xrm.DataMigration.Model;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Extensions.Tests
{
    [TestClass]
    public class CrmEntityExtensionsTests : TestBase
    {
        private Dictionary<string, HashSet<string>> inputEntityRelationships;
        private Dictionary<string, HashSet<string>> inputEntityAttributes;

        [TestInitialize]
        public void Setup()
        {
            inputEntityRelationships = new Dictionary<string, HashSet<string>>();
            inputEntityAttributes = new Dictionary<string, HashSet<string>>();
        }

        [TestMethod]
        public void StoreEntityDataNullEntityList()
        {
            List<DataMigration.Model.CrmEntity> crmEntity = null;

            FluentActions.Invoking(() => crmEntity.StoreEntityData(inputEntityAttributes, inputEntityRelationships))
                         .Should()
                         .NotThrow();

            inputEntityAttributes.Count.Should().Be(0);
            inputEntityRelationships.Count.Should().Be(0);
        }

        [TestMethod]
        public void StoreEntityDataNoEntities()
        {
            var crmEntity = new List<DataMigration.Model.CrmEntity>();

            FluentActions.Invoking(() => crmEntity.StoreEntityData(inputEntityAttributes, inputEntityRelationships))
                         .Should()
                         .NotThrow();

            inputEntityAttributes.Count.Should().Be(0);
            inputEntityRelationships.Count.Should().Be(0);
        }

        [TestMethod]
        public void StoreEntityDataHasEntitiesDuplicateEntityLogicalName()
        {
            var maxAttributes = 6;
            var maxRelationships = 3;

            var crmEntity = new List<DataMigration.Model.CrmEntity>();

            for (int entityCount = 0; entityCount < 5; entityCount++)
            {
                var entity = new DataMigration.Model.CrmEntity
                {
                    Name = "TestEntity"
                };

                for (int attributeCount = 0; attributeCount < maxAttributes; attributeCount++)
                {
                    entity.CrmFields.Add(new Capgemini.Xrm.DataMigration.Model.CrmField { FieldName = $"FieldName{attributeCount}" });
                }

                for (int relationshipCount = 0; relationshipCount < maxRelationships; relationshipCount++)
                {
                    entity.CrmRelationships.Add(new Capgemini.Xrm.DataMigration.Model.CrmRelationship { RelationshipName = $"RelationshipName{relationshipCount}" });
                }

                crmEntity.Add(entity);
            }

            FluentActions.Invoking(() => crmEntity.StoreEntityData(inputEntityAttributes, inputEntityRelationships))
                         .Should()
                         .Throw<ArgumentException>();
        }

        [TestMethod]
        public void StoreEntityDataHasEntities()
        {
            var maxAttributes = 6;
            var maxRelationships = 3;
            var maxEntityCount = 5;
            var index = 3;

            var crmEntity = new List<DataMigration.Model.CrmEntity>();

            for (int entityCount = 0; entityCount < maxEntityCount; entityCount++)
            {
                var entity = new DataMigration.Model.CrmEntity
                {
                    Name = $"TestEntity{entityCount}"
                };

                for (int attributeCount = 0; attributeCount < maxAttributes; attributeCount++)
                {
                    entity.CrmFields.Add(new Capgemini.Xrm.DataMigration.Model.CrmField { FieldName = $"FieldName{attributeCount}" });
                }

                for (int relationshipCount = 0; relationshipCount < maxRelationships; relationshipCount++)
                {
                    entity.CrmRelationships.Add(new Capgemini.Xrm.DataMigration.Model.CrmRelationship { RelationshipName = $"RelationshipName{relationshipCount}" });
                }

                crmEntity.Add(entity);
            }

            FluentActions.Invoking(() => crmEntity.StoreEntityData(inputEntityAttributes, inputEntityRelationships))
                         .Should()
                         .NotThrow();

            inputEntityAttributes.Count.Should().Be(maxEntityCount);
            inputEntityRelationships.Count.Should().Be(maxEntityCount);

            inputEntityAttributes[$"TestEntity{index}"].Count.Should().Be(maxAttributes);
            inputEntityRelationships[$"TestEntity{index}"].Count.Should().Be(maxRelationships);
        }

        [TestMethod]
        public void StoreEntityDataHasEntitiesWithNoAttributesAndRelationships()
        {
            var maxAttributes = 0;
            var maxRelationships = 0;
            var maxEntityCount = 5;
            var index = 3;

            var crmEntity = new List<DataMigration.Model.CrmEntity>();

            for (int entityCount = 0; entityCount < maxEntityCount; entityCount++)
            {
                var entity = new DataMigration.Model.CrmEntity
                {
                    Name = $"TestEntity{entityCount}"
                };

                crmEntity.Add(entity);
            }

            FluentActions.Invoking(() => crmEntity.StoreEntityData(inputEntityAttributes, inputEntityRelationships))
                         .Should()
                         .NotThrow();

            inputEntityAttributes.Count.Should().Be(maxEntityCount);
            inputEntityRelationships.Count.Should().Be(maxEntityCount);

            inputEntityAttributes[$"TestEntity{index}"].Count.Should().Be(maxAttributes);
            inputEntityRelationships[$"TestEntity{index}"].Count.Should().Be(maxRelationships);
        }

    }
}