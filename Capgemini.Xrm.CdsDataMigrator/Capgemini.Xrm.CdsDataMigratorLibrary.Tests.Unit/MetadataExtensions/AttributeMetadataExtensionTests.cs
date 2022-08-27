using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Capgemini.Xrm.CdsDataMigratorLibrary.Controllers;
using Capgemini.Xrm.CdsDataMigratorLibrary.Exceptions;
using Capgemini.Xrm.CdsDataMigratorLibrary.Extensions;
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
  }
}