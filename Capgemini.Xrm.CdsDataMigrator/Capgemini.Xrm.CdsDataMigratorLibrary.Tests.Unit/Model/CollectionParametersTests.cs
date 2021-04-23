using System;
using System.Collections.Generic;
using Capgemini.Xrm.CdsDataMigratorLibrary.Core;
using Capgemini.Xrm.CdsDataMigratorLibrary.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;

namespace Capgemini.Xrm.CdsDataMigrator.Tests.Unit.Model
{
    [TestClass]
    public class CollectionParametersTests : TestBase
    {
        [TestMethod]
        public void CanInstantiate()
        {
            Dictionary<string, HashSet<string>> inputEntityAttributes = null;
            Dictionary<string, HashSet<string>> inputEntityRelationships = null;
            Dictionary<string, string> inputFilterQuery = null;
            Dictionary<string, Dictionary<string, List<string>>> inputLookupMaping = null;
            Dictionary<string, Dictionary<Guid, Guid>> inputMapper = null;
            Dictionary<string, List<Item<EntityReference, EntityReference>>> inputMapping = null;

            var systemUndertest = new CollectionParameters(inputEntityAttributes, inputEntityRelationships, inputFilterQuery, inputLookupMaping, inputMapper, inputMapping);

            systemUndertest.EntityAttributes.Should().BeNull();
            systemUndertest.EntityRelationships.Should().BeNull();
            systemUndertest.FilterQuery.Should().BeNull();
            systemUndertest.LookupMaping.Should().BeNull();
            systemUndertest.Mapper.Should().BeNull();
            systemUndertest.Mapping.Should().BeNull();
        }
    }
}