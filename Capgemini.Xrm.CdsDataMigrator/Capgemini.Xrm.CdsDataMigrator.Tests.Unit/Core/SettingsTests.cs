using System;
using System.Collections.Generic;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;

namespace Capgemini.Xrm.CdsDataMigrator.Tests.Unit.Core
{
    [TestClass]
    public class SettingsTests
    {
        [TestMethod]
        public void GetOrganisations()
        {
            var systemUnderTest = new Settings();

            var actual = systemUnderTest.Organisations;

            actual.Count.Should().Be(0);
        }

        [TestMethod]
        public void IndexerForOrganisationsWithoutItems()
        {
            var systemUnderTest = new Settings();
            var key = Guid.NewGuid();

            var actual = systemUnderTest[key.ToString()];

            actual.Entities.Count.Should().Be(0);
            actual.Mappings.Count.Should().Be(0);
            actual.Sortcolumns.Count.Should().Be(0);
        }

        [TestMethod]
        public void IndexerForOrganisationsWithItems()
        {
            var systemUnderTest = new Settings();
            var organisation = new Organisations();

            for (var counter = 0; counter < 4; counter++)
            {
                var entityRef = new EntityReference($"TestEntity{counter}", Guid.NewGuid());
                var mapping = new Item<EntityReference, EntityReference>(entityRef, entityRef);
                organisation.Mappings.Add(mapping);
            }

            systemUnderTest.Organisations.Add(new KeyValuePair<Guid, Organisations>(Guid.NewGuid(), new Organisations() { }));
            systemUnderTest.Organisations.Add(new KeyValuePair<Guid, Organisations>(organisation.Mappings[1].Key.Id, organisation));
            systemUnderTest.Organisations.Add(new KeyValuePair<Guid, Organisations>(Guid.NewGuid(), new Organisations() { }));

            var actual = systemUnderTest[organisation.Mappings[1].Key.Id.ToString()];

            actual.Entities.Count.Should().Be(0);
            actual.Mappings.Count.Should().Be(organisation.Mappings.Count);
            actual.Sortcolumns.Count.Should().Be(0);
        }
    }
}