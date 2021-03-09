using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Forms;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;

namespace Capgemini.Xrm.CdsDataMigrator.Tests.Unit.Forms
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
            orgService = null;

            selectedValue = string.Empty;

            mappings = new Dictionary<string, Dictionary<string, List<string>>>();
            var values = new Dictionary<string, List<string>>
            {
                { "samplekey", new List<string>() { "contactattnoentity1" } }
            };
            mappings.Add(selectedValue, values);

            var entityMetadata = new EntityMetadata();
            var attributeList = new List<AttributeMetadata>()
            {
                new AttributeMetadata{ LogicalName = "contactattnoentity1" }
            };
            entityMetadata.LogicalName = "contact";

            var field = entityMetadata.GetType().GetRuntimeFields().First(a => a.Name == "_attributes");
            field.SetValue(entityMetadata, attributeList.ToArray());

            metadata = new List<EntityMetadata>
            {
                entityMetadata
            };
        }

        [TestMethod]
        public void MappingListLookupInstantiation()
        {
            FluentActions.Invoking(() => new MappingListLookup(
                                                                mappings,
                                                                orgService,
                                                                new List<EntityMetadata>(),
                                                                selectedValue))
                         .Should()
                         .NotThrow();
        }

        [TestMethod]
        public void MappingListLookupInstantiationNoMetaDataLogicalNameFails()
        {
            var entityMetadata = new EntityMetadata();
            var attributeList = new List<AttributeMetadata>()
            {
                new AttributeMetadata{ LogicalName = "contactattnoentity1" }
            };

            var field = entityMetadata.GetType().GetRuntimeFields().First(a => a.Name == "_attributes");
            field.SetValue(entityMetadata, attributeList.ToArray());
            metadata = new List<EntityMetadata>
            {
                entityMetadata
            };

            FluentActions.Invoking(() => new MappingListLookup(
                                                                mappings,
                                                                orgService,
                                                                metadata,
                                                                selectedValue))
                         .Should()
                         .Throw<InvalidOperationException>()
                         .WithMessage("One or more items in the collection are null.");
        }

        [TestMethod]
        public void MappingListLookupInstantiationWithMetaDataLogicalNameSucceeds()
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

        [Ignore("Must fix this!")]
        [TestMethod]
        public void LoadMappedItems()
        {
            using (var systemUnderTest = new MappingListLookup(mappings, orgService, metadata, selectedValue))
            {
                FluentActions.Invoking(() => systemUnderTest.LoadMappedItems())
                             .Should()
                             .NotThrow();
            }
        }
    }
}