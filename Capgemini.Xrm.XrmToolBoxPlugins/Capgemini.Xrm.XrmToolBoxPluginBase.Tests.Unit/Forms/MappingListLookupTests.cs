using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Forms.Tests
{
    [TestClass]
    public class MappingListLookupTests
    {
        private Dictionary<string, Dictionary<string, List<string>>> mappings;
        private IOrganizationService orgService;
        private List<EntityMetadata> metadata;
        private string selectedValue;

        [TestInitialize]
        public void Setup()
        {
            mappings = new Dictionary<string, Dictionary<string, List<string>>>();
            orgService = null;
            metadata = new List<EntityMetadata>();
            selectedValue = string.Empty;
        }

        [TestMethod]
        public void MappingListLookupInstantiation()
        {
            FluentActions.Invoking(() => new MappingListLookup(
                                                                mappings,
                                                                orgService,
                                                                metadata,
                                                                selectedValue))
                         .Should()
                         .NotThrow();
        }

        [TestMethod]
        public void RefreshMappingList()
        {
            using (var systemUnderTest = new MappingListLookup(mappings, orgService, metadata, selectedValue))
            {
                FluentActions.Invoking(() => systemUnderTest.RefreshMappingList())
                             .Should()
                             .NotThrow();
            }
        }
    }
}