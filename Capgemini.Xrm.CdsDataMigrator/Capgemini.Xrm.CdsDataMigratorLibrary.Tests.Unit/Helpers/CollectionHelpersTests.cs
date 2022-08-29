using Capgemini.Xrm.CdsDataMigrator.Tests.Unit;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Helpers.Tests
{
    [TestClass]
    public class CollectionHelpersTests : TestBase
    {
        private Dictionary<string, HashSet<string>> inputEntityRelationships;

        [TestInitialize]
        public void Setup()
        {
            SetupServiceMocks();
            inputEntityRelationships = new Dictionary<string, HashSet<string>>();
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