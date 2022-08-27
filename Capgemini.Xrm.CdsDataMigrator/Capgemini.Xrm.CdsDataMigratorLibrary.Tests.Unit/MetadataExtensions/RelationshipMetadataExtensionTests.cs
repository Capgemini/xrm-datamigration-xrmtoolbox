using System.Collections.Generic;
using Capgemini.Xrm.CdsDataMigratorLibrary.Controllers;
using Capgemini.Xrm.CdsDataMigratorLibrary.Exceptions;
using Capgemini.Xrm.CdsDataMigratorLibrary.Extensions;
using Capgemini.Xrm.CdsDataMigratorLibrary.Helpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Moq;

namespace Capgemini.Xrm.CdsDataMigrator.Tests.Unit.Controllers
{
    [TestClass]
    public class RelationshipMetadataExtensionTests : TestBase
    {
        private Dictionary<string, HashSet<string>> inputEntityRelationships;

        //private RelationshipMetadataExtension systemUnderTest;

        [TestInitialize]
        public void Setup()
        {
            SetupServiceMocks();
            inputEntityRelationships = new Dictionary<string, HashSet<string>>();

           // systemUnderTest = new RelationshipMetadataExtension();
        }

        [TestMethod]
        public void StoreRelationshipIfRequiresKeyCurrentValueUnchecked()
        {
            var relationshipLogicalName = "contact_account";
            var inputEntityLogicalName = "contact";

            var itemCheckEventArgs = new System.Windows.Forms.ItemCheckEventArgs(0, System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked);

            FluentActions.Invoking(() => CollectionHelpers.StoreRelationshipIfRequiresKey(relationshipLogicalName, itemCheckEventArgs, inputEntityLogicalName, inputEntityRelationships))
                         .Should()
                         .NotThrow();

            inputEntityRelationships[inputEntityLogicalName].Contains(relationshipLogicalName).Should().BeTrue();
        }

        [TestMethod]
        public void StoreRelationshipIfRequiresKeyCurrentValueChecked()
        {
            var relationshipLogicalName = "contact_account";
            var inputEntityLogicalName = "contact";

            var itemCheckEventArgs = new System.Windows.Forms.ItemCheckEventArgs(0, System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked);

            FluentActions.Invoking(() => CollectionHelpers.StoreRelationshipIfRequiresKey(relationshipLogicalName, itemCheckEventArgs, inputEntityLogicalName, inputEntityRelationships))
                         .Should()
                         .NotThrow();

            inputEntityRelationships[inputEntityLogicalName].Contains(relationshipLogicalName).Should().BeFalse();
        }

        [TestMethod]
        public void StoreRelationshipIfKeyExistsInputEntityRelationshipsDoesNotContainEntityLogicalName()
        {
            var relationshipLogicalName = "contact_account";
            var inputEntityLogicalName = "contact";

            var itemCheckEventArgs = new System.Windows.Forms.ItemCheckEventArgs(0, System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked);

            FluentActions.Invoking(() => CollectionHelpers.StoreRelationshipIfKeyExists(relationshipLogicalName, itemCheckEventArgs, inputEntityLogicalName, inputEntityRelationships))
                         .Should()
                         .Throw<KeyNotFoundException>();
        }

        [TestMethod]
        public void StoreRelationshipIfKeyExistsCurrentValueIsChecked()
        {
            var relationshipLogicalName = "contact_account";
            var inputEntityLogicalName = "contact";

            var relationshipSet = new HashSet<string>();
            inputEntityRelationships.Add(inputEntityLogicalName, relationshipSet);

            var itemCheckEventArgs = new System.Windows.Forms.ItemCheckEventArgs(0, System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked);

            FluentActions.Invoking(() => CollectionHelpers.StoreRelationshipIfKeyExists(relationshipLogicalName, itemCheckEventArgs, inputEntityLogicalName, inputEntityRelationships))
                         .Should()
                         .NotThrow();

            inputEntityRelationships[inputEntityLogicalName].Contains(relationshipLogicalName).Should().BeFalse();
        }

        [TestMethod]
        public void StoreRelationshipIfKeyExistsCurrentValueIsCheckedAndRelationshipSetAlreadyContainsLogicalName()
        {
            var relationshipLogicalName = "contact_account";
            var inputEntityLogicalName = "contact";

            var relationshipSet = new HashSet<string>() { relationshipLogicalName };
            inputEntityRelationships.Add(inputEntityLogicalName, relationshipSet);

            var itemCheckEventArgs = new System.Windows.Forms.ItemCheckEventArgs(0, System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked);

            FluentActions.Invoking(() => CollectionHelpers.StoreRelationshipIfKeyExists(relationshipLogicalName, itemCheckEventArgs, inputEntityLogicalName, inputEntityRelationships))
                         .Should()
                         .NotThrow();

            inputEntityRelationships[inputEntityLogicalName].Contains(relationshipLogicalName).Should().BeFalse();
        }

        [TestMethod]
        public void StoreRelationshipIfKeyExistsCurrentValueIsUnchecked()
        {
            var relationshipLogicalName = "contact_account";
            var inputEntityLogicalName = "contact";

            var relationshipSet = new HashSet<string>();
            inputEntityRelationships.Add(inputEntityLogicalName, relationshipSet);

            var itemCheckEventArgs = new System.Windows.Forms.ItemCheckEventArgs(0, System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked);

            FluentActions.Invoking(() => CollectionHelpers.StoreRelationshipIfKeyExists(relationshipLogicalName, itemCheckEventArgs, inputEntityLogicalName, inputEntityRelationships))
                         .Should()
                         .NotThrow();

            inputEntityRelationships[inputEntityLogicalName].Contains(relationshipLogicalName).Should().BeTrue();
        }
 }
}