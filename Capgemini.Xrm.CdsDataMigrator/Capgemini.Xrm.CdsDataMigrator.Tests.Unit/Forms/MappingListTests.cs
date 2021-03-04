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
        public void GetGuidMappingList()
        {
            using (var systemUnderTest = new MappingList(mappings))
            {
                var actual = systemUnderTest.GetGuidMappingList();

                actual.Count.Should().Be(0);
            }
        }
    }
}