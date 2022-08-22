using System.Collections.Generic;
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
    public class RelationshipMetadataExtensionTests : TestBase
    {
        private Dictionary<string, HashSet<string>> inputEntityRelationships;

        private RelationshipMetadataExtension systemUnderTest;

        [TestInitialize]
        public void Setup()
        {
            SetupServiceMocks();
            inputEntityRelationships = new Dictionary<string, HashSet<string>>();

            systemUnderTest = new RelationshipMetadataExtension();
        }

        [TestMethod]
        public void StoreRelationshipIfRequiresKeyCurrentValueUnchecked()
        {
            var relationshipLogicalName = "contact_account";
            var inputEntityLogicalName = "contact";

            var itemCheckEventArgs = new System.Windows.Forms.ItemCheckEventArgs(0, System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked);

            FluentActions.Invoking(() => systemUnderTest.StoreRelationshipIfRequiresKey(relationshipLogicalName, itemCheckEventArgs, inputEntityLogicalName, inputEntityRelationships))
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

            FluentActions.Invoking(() => systemUnderTest.StoreRelationshipIfRequiresKey(relationshipLogicalName, itemCheckEventArgs, inputEntityLogicalName, inputEntityRelationships))
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

            FluentActions.Invoking(() => systemUnderTest.StoreRelationshipIfKeyExists(relationshipLogicalName, itemCheckEventArgs, inputEntityLogicalName, inputEntityRelationships))
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

            FluentActions.Invoking(() => systemUnderTest.StoreRelationshipIfKeyExists(relationshipLogicalName, itemCheckEventArgs, inputEntityLogicalName, inputEntityRelationships))
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

            FluentActions.Invoking(() => systemUnderTest.StoreRelationshipIfKeyExists(relationshipLogicalName, itemCheckEventArgs, inputEntityLogicalName, inputEntityRelationships))
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

            FluentActions.Invoking(() => systemUnderTest.StoreRelationshipIfKeyExists(relationshipLogicalName, itemCheckEventArgs, inputEntityLogicalName, inputEntityRelationships))
                         .Should()
                         .NotThrow();

            inputEntityRelationships[inputEntityLogicalName].Contains(relationshipLogicalName).Should().BeTrue();
        }

        [TestMethod]
        public void PopulateRelationshipActionNoManyToManyRelationships()
        {
            string entityLogicalName = "contact";
            var entityMetadata = new EntityMetadata();

            var migratorServiceParameters = GenerateMigratorParameters();

            MetadataServiceMock.Setup(x => x.RetrieveEntities(It.IsAny<string>(), It.IsAny<IOrganizationService>(), It.IsAny<IExceptionService>()))
                .Returns(entityMetadata)
                .Verifiable();

            var actual = systemUnderTest.PopulateRelationshipAction(entityLogicalName, inputEntityRelationships, migratorServiceParameters);

            actual.Count.Should().Be(0);

            ServiceMock.VerifyAll();
            MetadataServiceMock.VerifyAll();
        }

        [TestMethod]
        public void PopulateRelationshipAction()
        {
            string entityLogicalName = "account_contact";
            var items = new List<System.Windows.Forms.ListViewItem>
            {
                new System.Windows.Forms.ListViewItem("Item1"),
                new System.Windows.Forms.ListViewItem("Item2")
            };

            var entityMetadata = new EntityMetadata();

            var relationship = new ManyToManyRelationshipMetadata
            {
                Entity1LogicalName = "account",
                Entity1IntersectAttribute = "accountid",
                IntersectEntityName = "account_contact",
                Entity2LogicalName = "contact",
                Entity2IntersectAttribute = "contactid"
            };

            InsertManyToManyRelationshipMetadata(entityMetadata, relationship);

            var migratorServiceParameters = GenerateMigratorParameters();

            MetadataServiceMock.Setup(x => x.RetrieveEntities(It.IsAny<string>(), It.IsAny<IOrganizationService>(), It.IsAny<IExceptionService>()))
                .Returns(entityMetadata)
                .Verifiable();

            using (var listView = new System.Windows.Forms.ListView())
            {
                var controller = new EntityMetadataExtension();
                controller.PopulateEntitiesListView(items, null, null, listView, NotificationServiceMock.Object);

                var actual = systemUnderTest.PopulateRelationshipAction(entityLogicalName, inputEntityRelationships, migratorServiceParameters);

                actual.Count.Should().BeGreaterThan(0);
            }

            ServiceMock.VerifyAll();
            MetadataServiceMock.VerifyAll();
        }
    }
}