using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Capgemini.Xrm.CdsDataMigratorLibrary.Controllers;
using Capgemini.Xrm.CdsDataMigratorLibrary.Exceptions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Moq;

namespace Capgemini.Xrm.CdsDataMigrator.Tests.Unit.Controllers
{
    [TestClass]
    public class AttributeMetadataExtensionTests : TestBase
    {
        private Dictionary<string, HashSet<string>> inputEntityAttributes;

        private AttributeMetadataExtension systemUnderTest;

        [TestInitialize]
        public void Setup()
        {
            SetupServiceMocks();
            inputEntityAttributes = new Dictionary<string, HashSet<string>>();

            systemUnderTest = new AttributeMetadataExtension();
        }

        [TestMethod]
        public void StoreAttributeIfRequiresKeyCurrentValueIsChecked()
        {
            var entityLogicalName = "contact";
            var attributeLogicalName = "contactid";
            var itemCheckEventArgs = new System.Windows.Forms.ItemCheckEventArgs(0, System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked);

            FluentActions.Invoking(() => systemUnderTest.StoreAttributeIfRequiresKey(attributeLogicalName, itemCheckEventArgs, inputEntityAttributes, entityLogicalName))
                         .Should()
                         .NotThrow();

            inputEntityAttributes.Count.Should().Be(1);
            inputEntityAttributes[entityLogicalName].Count.Should().Be(0);
        }

        [TestMethod]
        public void StoreAttributeIfRequiresKeyCurrentValueIsUnchecked()
        {
            var entityLogicalName = "contact";
            var attributeLogicalName = "contactid";

            var itemCheckEventArgs = new System.Windows.Forms.ItemCheckEventArgs(0, System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked);

            FluentActions.Invoking(() => systemUnderTest.StoreAttributeIfRequiresKey(attributeLogicalName, itemCheckEventArgs, inputEntityAttributes, entityLogicalName))
                         .Should()
                         .NotThrow();

            inputEntityAttributes.Count.Should().Be(1);
            inputEntityAttributes[entityLogicalName].Count.Should().Be(1);
        }

        [TestMethod]
        public void StoreAttriubteIfKeyExistsInputEntityAttributesDoesNotContainEntityLogicalName()
        {
            var entityLogicalName = "contact";
            var attributeLogicalName = "contactid";
            var itemCheckEventArgs = new System.Windows.Forms.ItemCheckEventArgs(0, System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked);

            FluentActions.Invoking(() => systemUnderTest.StoreAttriubteIfKeyExists(attributeLogicalName, itemCheckEventArgs, inputEntityAttributes, entityLogicalName))
                         .Should()
                         .Throw<KeyNotFoundException>();
        }

        [TestMethod]
        public void StoreAttriubteIfKeyExistsCurrentValueIsChecked()
        {
            var entityLogicalName = "contact";
            var attributeLogicalName = "contactid";
            var attributeSet = new HashSet<string>();
            inputEntityAttributes.Add(entityLogicalName, attributeSet);

            var itemCheckEventArgs = new System.Windows.Forms.ItemCheckEventArgs(0, System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked);

            FluentActions.Invoking(() => systemUnderTest.StoreAttriubteIfKeyExists(attributeLogicalName, itemCheckEventArgs, inputEntityAttributes, entityLogicalName))
                         .Should()
                         .NotThrow();

            inputEntityAttributes.Count.Should().Be(1);
            inputEntityAttributes[entityLogicalName].Contains(attributeLogicalName).Should().BeFalse();
        }

        [TestMethod]
        public void StoreAttriubteIfKeyExistsCurrentValueIsCheckedAndAttributeSetAlreadyContainsLogicalName()
        {
            var entityLogicalName = "contact";
            var attributeLogicalName = "contactid";
            var attributeSet = new HashSet<string>() { attributeLogicalName };
            inputEntityAttributes.Add(entityLogicalName, attributeSet);

            var itemCheckEventArgs = new System.Windows.Forms.ItemCheckEventArgs(0, System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked);

            FluentActions.Invoking(() => systemUnderTest.StoreAttriubteIfKeyExists(attributeLogicalName, itemCheckEventArgs, inputEntityAttributes, entityLogicalName))
                         .Should()
                         .NotThrow();

            inputEntityAttributes.Count.Should().Be(1);
            inputEntityAttributes[entityLogicalName].Contains(attributeLogicalName).Should().BeFalse();
        }

        [TestMethod]
        public void StoreAttriubteIfKeyExistsCurrentValueIsUnchecked()
        {
            var entityLogicalName = "contact";
            var attributeLogicalName = "contactid";
            var attributeSet = new HashSet<string>();
            inputEntityAttributes.Add(entityLogicalName, attributeSet);

            var itemCheckEventArgs = new System.Windows.Forms.ItemCheckEventArgs(0, System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked);

            FluentActions.Invoking(() => systemUnderTest.StoreAttriubteIfKeyExists(attributeLogicalName, itemCheckEventArgs, inputEntityAttributes, entityLogicalName))
                         .Should()
                         .NotThrow();

            inputEntityAttributes.Count.Should().Be(1);
            inputEntityAttributes[entityLogicalName].Contains(attributeLogicalName).Should().BeTrue();
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

            var actual = systemUnderTest.ProcessAllAttributeMetadata(unmarkedattributes, attributeList.ToArray(), entityLogicalName, inputEntityAttributes);

            actual.Count.Should().BeGreaterThan(0);
        }

        [TestMethod]
        public void FilterAttributes()
        {
            var entityMetadata = new EntityMetadata();
            InsertAttributeList(entityMetadata, new List<string> { "contactattnoentity1" });
            bool showSystemAttributes = true;

            var actual = systemUnderTest.FilterAttributes(entityMetadata, showSystemAttributes);

            actual.Length.Should().Be(entityMetadata.Attributes.Length);
        }

        [TestMethod]
        public void FilterAttributesHideSystemAttributes()
        {
            var entityMetadata = new EntityMetadata();
            InsertAttributeList(entityMetadata, new List<string> { "contactattnoentity1" });
            bool showSystemAttributes = false;

            var actual = systemUnderTest.FilterAttributes(entityMetadata, showSystemAttributes);

            actual.Length.Should().Be(0);
        }

        [TestMethod]
        public void InvalidUpdateIsValidForCreate()
        {
            AttributeMetadata attribute = new AttributeMetadata
            {
                LogicalName = "contactattnoentity1",
                IsValidForCreate = (bool?)true
            };
            var item = new System.Windows.Forms.ListViewItem("Item1");

            FluentActions.Invoking(() => systemUnderTest.InvalidUpdate(attribute, item))
                         .Should()
                         .NotThrow();

            item.ToolTipText.Should().NotContain("Not createable, ");
        }

        [TestMethod]
        public void InvalidUpdateIsLogicalFalse()
        {
            var attribute = new AttributeMetadata
            {
                LogicalName = "contactattnoentity1"
            };
            var isLogicalEntityField = attribute.GetType().GetRuntimeFields().First(a => a.Name == "_isLogical");
            isLogicalEntityField.SetValue(attribute, (bool?)false);

            var item = new System.Windows.Forms.ListViewItem("Item1");

            FluentActions.Invoking(() => systemUnderTest.InvalidUpdate(attribute, item))
                         .Should()
                         .NotThrow();

            item.ToolTipText.Should().NotContain("Logical attribute, ");
        }

        [TestMethod]
        public void InvalidUpdateIsLogicalTrue()
        {
            var attribute = new AttributeMetadata
            {
                LogicalName = "contactattnoentity1"
            };
            var isLogicalEntityField = attribute.GetType().GetRuntimeFields().First(a => a.Name == "_isLogical");
            isLogicalEntityField.SetValue(attribute, (bool?)true);

            var item = new System.Windows.Forms.ListViewItem("Item1");

            FluentActions.Invoking(() => systemUnderTest.InvalidUpdate(attribute, item))
                         .Should()
                         .NotThrow();

            item.ToolTipText.Should().Contain("Logical attribute, ");
        }

        [TestMethod]
        public void InvalidUpdateIsValidForReadTrue()
        {
            var attribute = new AttributeMetadata
            {
                LogicalName = "contactattnoentity1"
            };
            var isLogicalEntityField = attribute.GetType().GetRuntimeFields().First(a => a.Name == "_validForRead");
            isLogicalEntityField.SetValue(attribute, (bool?)true);

            var item = new System.Windows.Forms.ListViewItem("Item1");

            FluentActions.Invoking(() => systemUnderTest.InvalidUpdate(attribute, item))
                         .Should()
                         .NotThrow();

            item.ToolTipText.Should().NotContain("Not readable, ");
        }

        [TestMethod]
        public void InvalidUpdateIsValidForReadFalse()
        {
            var attribute = new AttributeMetadata
            {
                LogicalName = "contactattnoentity1"
            };
            var isLogicalEntityField = attribute.GetType().GetRuntimeFields().First(a => a.Name == "_validForRead");
            isLogicalEntityField.SetValue(attribute, (bool?)false);

            var item = new System.Windows.Forms.ListViewItem("Item1");

            FluentActions.Invoking(() => systemUnderTest.InvalidUpdate(attribute, item))
                         .Should()
                         .NotThrow();

            item.ToolTipText.Should().Contain("Not readable, ");
        }

        [TestMethod]
        public void InvalidUpdateIsValidForCreateAndIsValidForUpdateFalse()
        {
            var attribute = new AttributeMetadata
            {
                LogicalName = "contactattnoentity1",
                IsValidForCreate = (bool?)false,
                IsValidForUpdate = (bool?)false
            };

            var item = new System.Windows.Forms.ListViewItem("Item1");

            FluentActions.Invoking(() => systemUnderTest.InvalidUpdate(attribute, item))
                         .Should()
                         .NotThrow();

            item.ForeColor.Should().Be(System.Drawing.Color.Red);
        }

        [TestMethod]
        public void InvalidUpdateIsValidForCreateAndIsValidForUpdateTrue()
        {
            var attribute = new AttributeMetadata
            {
                LogicalName = "contactattnoentity1",
                IsValidForCreate = (bool?)true,
                IsValidForUpdate = (bool?)true
            };

            var item = new System.Windows.Forms.ListViewItem("Item1");

            FluentActions.Invoking(() => systemUnderTest.InvalidUpdate(attribute, item))
                         .Should()
                         .NotThrow();

            item.ForeColor.Should().NotBe(System.Drawing.Color.Red);
        }

        [TestMethod]
        public void InvalidUpdateDeprecatedVersionNull()
        {
            var attribute = new AttributeMetadata
            {
                LogicalName = "contactattnoentity1"
            };
            var isLogicalEntityField = attribute.GetType().GetRuntimeFields().First(a => a.Name == "_deprecatedVersion");
            isLogicalEntityField.SetValue(attribute, null);

            var item = new System.Windows.Forms.ListViewItem("Item1");

            FluentActions.Invoking(() => systemUnderTest.InvalidUpdate(attribute, item))
                         .Should()
                         .NotThrow();

            item.ToolTipText.Should().NotContain("DeprecatedVersion:");
        }

        [TestMethod]
        public void InvalidUpdateDeprecatedVersionContainsValue()
        {
            var attribute = new AttributeMetadata
            {
                LogicalName = "contactattnoentity1"
            };
            var isLogicalEntityField = attribute.GetType().GetRuntimeFields().First(a => a.Name == "_deprecatedVersion");
            isLogicalEntityField.SetValue(attribute, "1.0.0.0");

            var item = new System.Windows.Forms.ListViewItem("Item1");

            FluentActions.Invoking(() => systemUnderTest.InvalidUpdate(attribute, item))
                         .Should()
                         .NotThrow();

            item.ToolTipText.Should().Contain("DeprecatedVersion:");
        }

        [TestMethod]
        public void GetAttributeList()
        {
            string entityLogicalName = "contact";

            AttributeMetadata[] attributes = null;
            var entityMetadata = new EntityMetadata();
            bool showSystemAttributes = true;
            var serviceParameters = GenerateMigratorParameters();

            MetadataServiceMock.Setup(x => x.RetrieveEntities(It.IsAny<string>(), It.IsAny<IOrganizationService>(), It.IsAny<IExceptionService>()))
                                .Returns(entityMetadata)
                                .Verifiable();

            attributes = systemUnderTest.GetAttributeList(entityLogicalName, showSystemAttributes, serviceParameters);

            attributes.Should().BeNull();
        }

        [TestMethod]
        public void GetAttributeListMetaDataServiceReturnsEnities()
        {
            string entityLogicalName = "contact";

            AttributeMetadata[] attributes = null;
            bool showSystemAttributes = true;

            var serviceParameters = GenerateMigratorParameters();

            var entityMetadata = new EntityMetadata();
            InsertAttributeList(entityMetadata, new List<string> { "contactattnoentity1" });

            MetadataServiceMock.Setup(x => x.RetrieveEntities(It.IsAny<string>(), It.IsAny<IOrganizationService>(), It.IsAny<IExceptionService>()))
                                .Returns(entityMetadata)
                                .Verifiable();

            attributes = systemUnderTest.GetAttributeList(entityLogicalName, showSystemAttributes, serviceParameters);

            attributes.Should().NotBeNull();
        }
    }
}