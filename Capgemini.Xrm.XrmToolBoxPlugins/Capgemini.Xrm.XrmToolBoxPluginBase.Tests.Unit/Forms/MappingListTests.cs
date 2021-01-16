using System.Collections.Generic;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Forms.Tests
{
    [TestClass]
    public class MappingListTests
    {
        private List<Item<EntityReference, EntityReference>> mappings;

        private MappingList systemUnderTest;

        [TestInitialize]
        public void Setup()
        {
            mappings = new List<Item<EntityReference, EntityReference>>();

            systemUnderTest = new MappingList(mappings);
        }

        [TestMethod]
        public void MappingListInstantiation()
        {
            var mappings = new List<Item<EntityReference, EntityReference>>();

            FluentActions.Invoking(() => new MappingList(mappings))
                         .Should()
                         .NotThrow();
        }

        [TestMethod]
        public void GetMappingListEmptyEntityLogicalName()
        {
            string entityLogicalName = string.Empty;

            var actual = systemUnderTest.GetMappingList(entityLogicalName);

            actual.Count.Should().Be(0);
        }

        [TestMethod]
        public void GetGuidMappingList()
        {
            var actual = systemUnderTest.GetGuidMappingList();

            actual.Count.Should().Be(0);
        }
    }
}