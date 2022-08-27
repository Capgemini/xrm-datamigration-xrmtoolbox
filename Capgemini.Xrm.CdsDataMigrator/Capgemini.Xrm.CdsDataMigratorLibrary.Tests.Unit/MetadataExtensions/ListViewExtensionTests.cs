using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Capgemini.Xrm.CdsDataMigratorLibrary.Controllers;
using Capgemini.Xrm.CdsDataMigratorLibrary.Core;
using Capgemini.Xrm.CdsDataMigratorLibrary.Exceptions;
using Capgemini.Xrm.CdsDataMigratorLibrary.Extensions;
using Capgemini.Xrm.CdsDataMigratorLibrary.Forms;
using Capgemini.Xrm.DataMigration.Model;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Moq;

namespace Capgemini.Xrm.CdsDataMigrator.Tests.Unit.Controllers
{
    [TestClass]
    public class ListViewExtensionTests : TestBase
    {
        private Dictionary<string, HashSet<string>> inputEntityRelationships;
        private Dictionary<string, HashSet<string>> inputEntityAttributes;

        //private ListViewExtension systemUnderTest;

        [TestInitialize]
        public void Setup()
        {
            SetupServiceMocks();
            inputEntityRelationships = new Dictionary<string, HashSet<string>>();
            inputEntityAttributes = new Dictionary<string, HashSet<string>>();

            //systemUnderTest = new ListViewExtension();
        }

        //[TestMethod]
        //public void UpdateAttributeMetadataCheckBoxesNonExitingsFilterValue()
        //{
        //    string inputEntityLogicalName = "account_contact";

        //    var relationship = new ManyToManyRelationshipMetadata
        //    {
        //        Entity1LogicalName = "account",
        //        Entity1IntersectAttribute = "accountid",
        //        IntersectEntityName = "account_contact",
        //        Entity2LogicalName = "contact",
        //        Entity2IntersectAttribute = "contactid"
        //    };

        //    var entityRelationshipSet = new HashSet<string>() { inputEntityLogicalName };

        //    inputEntityRelationships.Add(inputEntityLogicalName, entityRelationshipSet);

        //    var item = new System.Windows.Forms.ListViewItem("Item1");

        //    FluentActions.Invoking(() => systemUnderTest.UpdateAttributeMetadataCheckBoxes(relationship.IntersectEntityName, item, inputEntityRelationships, "Fake Logical name"))
        //                 .Should()
        //                 .NotThrow();

        //    item.Checked.Should().BeFalse();
        //}

        //[TestMethod]
        //public void UpdateAttributeMetadataCheckBoxesValueDoesNotExistInEntityRelationships()
        //{
        //    string inputEntityLogicalName = "account_contact";

        //    var relationship = new ManyToManyRelationshipMetadata
        //    {
        //        Entity1LogicalName = "account",
        //        Entity1IntersectAttribute = "accountid",
        //        IntersectEntityName = "account_contact",
        //        Entity2LogicalName = "contact",
        //        Entity2IntersectAttribute = "contactid"
        //    };

        //    var entityRelationshipSet = new HashSet<string>() { };

        //    inputEntityRelationships.Add(inputEntityLogicalName, entityRelationshipSet);

        //    var item = new System.Windows.Forms.ListViewItem("Item1");

        //    FluentActions.Invoking(() => systemUnderTest.UpdateAttributeMetadataCheckBoxes(relationship.IntersectEntityName, item, inputEntityRelationships, inputEntityLogicalName))
        //                 .Should()
        //                 .NotThrow();

        //    item.Checked.Should().BeFalse();
        //}

        //[TestMethod]
        //public void UpdateAttributeMetadataCheckBoxesIntersectEntityNameDoesNotExist()
        //{
        //    string inputEntityLogicalName = "account_contact";

        //    var relationship = new ManyToManyRelationshipMetadata
        //    {
        //        Entity1LogicalName = "account",
        //        Entity1IntersectAttribute = "accountid",
        //        IntersectEntityName = "account_contact2",
        //        Entity2LogicalName = "contact",
        //        Entity2IntersectAttribute = "contactid"
        //    };

        //    var entityRelationshipSet = new HashSet<string>() { inputEntityLogicalName };

        //    inputEntityRelationships.Add(inputEntityLogicalName, entityRelationshipSet);

        //    var item = new System.Windows.Forms.ListViewItem("Item1");

        //    FluentActions.Invoking(() => systemUnderTest.UpdateAttributeMetadataCheckBoxes(relationship.IntersectEntityName, item, inputEntityRelationships, inputEntityLogicalName))
        //                 .Should()
        //                 .NotThrow();

        //    item.Checked.Should().BeFalse();
        //}

        //[TestMethod]
        //public void UpdateAttributeMetadataCheckBoxesIntersectEntityNameExist()
        //{
        //    string inputEntityLogicalName = "account_contact";

        //    var relationship = new ManyToManyRelationshipMetadata
        //    {
        //        Entity1LogicalName = "account",
        //        Entity1IntersectAttribute = "accountid",
        //        IntersectEntityName = "account_contact",
        //        Entity2LogicalName = "contact",
        //        Entity2IntersectAttribute = "contactid"
        //    };

        //    var entityRelationshipSet = new HashSet<string>() { inputEntityLogicalName };

        //    inputEntityRelationships.Add(inputEntityLogicalName, entityRelationshipSet);

        //    var item = new System.Windows.Forms.ListViewItem("Item1");

        //    FluentActions.Invoking(() => systemUnderTest.UpdateAttributeMetadataCheckBoxes(relationship.IntersectEntityName, item, inputEntityRelationships, inputEntityLogicalName))
        //                 .Should()
        //                 .NotThrow();

        //    item.Checked.Should().BeTrue();
        //}

        //[TestMethod]
        //public void UpdateCheckBoxesRelationshipNullEntityLogicalName()
        //{
        //    string inputEntityLogicalName = "account";

        //    var relationship = new ManyToManyRelationshipMetadata
        //    {
        //        Entity1LogicalName = "account",
        //        Entity1IntersectAttribute = "accountid",
        //        IntersectEntityName = "account_contact",
        //        Entity2LogicalName = "contact",
        //        Entity2IntersectAttribute = "contactid"
        //    };

        //    var entityRelationshipSet = new HashSet<string>() { inputEntityLogicalName };

        //    inputEntityRelationships.Add(inputEntityLogicalName, entityRelationshipSet);

        //    var item = new System.Windows.Forms.ListViewItem("Item1");

        //    FluentActions.Invoking(() => systemUnderTest.UpdateAttributeMetadataCheckBoxes(relationship.IntersectEntityName, item, inputEntityRelationships, null))
        //                 .Should()
        //                 .Throw<ArgumentNullException>();
        //}

        //[TestMethod]
        //public void UpdateCheckBoxesRelationshipNonExistingEntityLogicalName()
        //{
        //    string inputEntityLogicalName = "account";

        //    var relationship = new ManyToManyRelationshipMetadata
        //    {
        //        Entity1LogicalName = "account",
        //        Entity1IntersectAttribute = "accountid",
        //        IntersectEntityName = "account_contact",
        //        Entity2LogicalName = "contact",
        //        Entity2IntersectAttribute = "contactid"
        //    };

        //    var entityRelationshipSet = new HashSet<string>() { inputEntityLogicalName };

        //    inputEntityRelationships.Add(inputEntityLogicalName, entityRelationshipSet);

        //    var item = new System.Windows.Forms.ListViewItem("Item1");

        //    FluentActions.Invoking(() => systemUnderTest.UpdateAttributeMetadataCheckBoxes(relationship.IntersectEntityName, item, inputEntityRelationships, "Random Text"))
        //                 .Should()
        //                 .NotThrow();
        //}

       }
}