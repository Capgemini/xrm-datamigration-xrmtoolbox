using Capgemini.Xrm.CdsDataMigrator.Tests.Unit;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using System.Collections.Generic;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Extensions.Tests
{
    [TestClass()]
    public class XrmMetadataExtensionsTests : TestBase
    {
        private Dictionary<string, HashSet<string>> inputEntityAttributes;

        [TestInitialize]
        public void Setup()
        {
            SetupServiceMocks();
            inputEntityAttributes = new Dictionary<string, HashSet<string>>();
        }

        [TestMethod]
        public void FilterAttributes()
        {
            var entityMetadata = new EntityMetadata();
            InsertAttributeList(entityMetadata, new List<string> { "contactattnoentity1" });
            bool showSystemAttributes = true;

            var actual = entityMetadata.FilterAttributes(showSystemAttributes);

            actual.Count.Should().Be(entityMetadata.Attributes.Length);
        }

        [TestMethod]
        public void FilterAttributesHideSystemAttributes()
        {
            var entityMetadata = new EntityMetadata();
            InsertAttributeList(entityMetadata, new List<string> { "contactattnoentity1" });
            bool showSystemAttributes = false;

            var actual = entityMetadata.FilterAttributes(showSystemAttributes);

            actual.Count.Should().Be(0);
        }

        [TestMethod]
        public void ProcessAllAttributeMetadata()
        {
            string entityLogicalName = "account_contact";
            List<string> unmarkedattributes = new List<string>();

            var attributeList = new List<AttributeMetadata>()
            {
                new AttributeMetadata
                {
                    LogicalName = "contactattnoentity1",
                    DisplayName = new Label
                    {
                        UserLocalizedLabel = new LocalizedLabel { Label = "Test" }
                    }
                }
            };

            var actual = attributeList.ProcessAllAttributeMetadata(unmarkedattributes, entityLogicalName, inputEntityAttributes);

            actual.Count.Should().BeGreaterThan(0);
        }
    }
}