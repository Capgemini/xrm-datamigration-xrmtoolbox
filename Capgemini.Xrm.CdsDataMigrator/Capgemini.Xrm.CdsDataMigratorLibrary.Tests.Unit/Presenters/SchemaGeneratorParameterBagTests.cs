using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Presenters.Tests
{
    [TestClass]
    public class SchemaGeneratorParameterBagTests
    {
        private SchemaGeneratorParameterBag systemUnderTest;

        [TestMethod]
        public void SchemaGeneratorParameterBag_CachedMetadata()
        {
            systemUnderTest = new SchemaGeneratorParameterBag();

            systemUnderTest.CachedMetadata.Count.Should().Be(0);
        }

        [TestMethod]
        public void SchemaGeneratorParameterBag_AttributeMapping()
        {
            systemUnderTest = new SchemaGeneratorParameterBag();

            systemUnderTest.AttributeMapping.Should().NotBeNull();
        }

        [TestMethod]
        public void SchemaGeneratorParameterBag_EntityAttributes()
        {
            systemUnderTest = new SchemaGeneratorParameterBag();

            systemUnderTest.EntityAttributes.Count.Should().Be(0);
        }

        [TestMethod]
        public void SchemaGeneratorParameterBag_EntityRelationships()
        {
            systemUnderTest = new SchemaGeneratorParameterBag();

            systemUnderTest.EntityRelationships.Count.Should().Be(0);
        }

        [TestMethod]
        public void SchemaGeneratorParameterBag_CheckedEntity()
        {
            systemUnderTest = new SchemaGeneratorParameterBag();

            systemUnderTest.CheckedEntity.Count.Should().Be(0);
        }

        [TestMethod]
        public void SchemaGeneratorParameterBag_SelectedEntity()
        {
            systemUnderTest = new SchemaGeneratorParameterBag();


            systemUnderTest.SelectedEntity.Count.Should().Be(0);
        }

        [TestMethod]
        public void SchemaGeneratorParameterBag_CheckedRelationship()
        {
            systemUnderTest = new SchemaGeneratorParameterBag();

            systemUnderTest.CheckedRelationship.Count.Should().Be(0);
        }
    }
}