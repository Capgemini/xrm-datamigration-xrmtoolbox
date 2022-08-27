using Capgemini.Xrm.CdsDataMigrator.Tests.Unit;
using Capgemini.Xrm.CdsDataMigratorLibrary.Core;
using Capgemini.Xrm.CdsDataMigratorLibrary.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Moq;
using System;
using System.Collections.Generic;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Presenters.Tests
{
    [TestClass]
    public class SchemaGeneratorPresenterTests : TestBase
    {
        private Dictionary<string, HashSet<string>> inputEntityRelationships;
        private Dictionary<string, HashSet<string>> inputEntityAttributes;
        private Mock<ISchemaGeneratorView> view;
        private Settings settings;
        private ServiceParameters serviceParameters;

        private SchemaGeneratorPresenter systemUnderTest;

        [TestInitialize]
        public void Setup()
        {
            SetupServiceMocks();

            view = new Mock<ISchemaGeneratorView>();
            settings = new Settings();

            systemUnderTest =
                new SchemaGeneratorPresenter(
                                    view.Object,
                                    ServiceMock.Object,
                                    MetadataServiceMock.Object,
                                    NotificationServiceMock.Object,
                                    ExceptionServicerMock.Object,
                                    settings);

            serviceParameters = new ServiceParameters(ServiceMock.Object, MetadataServiceMock.Object, NotificationServiceMock.Object, ExceptionServicerMock.Object);
        }

        [TestMethod]
        public void SchemaGeneratorPresenter_ParameterBag()
        {
            systemUnderTest.ParameterBag.Should().NotBeNull();
        }

        [TestMethod]
        public void SchemaGeneratorPresenter_View()
        {
            systemUnderTest.View.Should().NotBeNull();
        }

        [TestMethod]
        public void OnConnectionUpdated()
        {
            var connectedOrgFriendlyName = "testOrg";

            var entityResultList = new List<Microsoft.Xrm.Sdk.Metadata.EntityMetadata> { };
            MetadataServiceMock.Setup(a => a.RetrieveEntities(It.IsAny<IOrganizationService>()))
                           .Returns(entityResultList);

            using (var entityList = new System.Windows.Forms.TreeView())
            {
                using (var entityAttributeList = new System.Windows.Forms.ListView())
                {
                    using (var entityRelationshipList = new System.Windows.Forms.ListView())
                    {
                        view.SetupGet(a => a.EntityList).Returns(entityList);
                        view.SetupGet(a => a.EntityAttributeList).Returns(entityAttributeList);
                        view.SetupGet(a => a.EntityRelationshipList).Returns(entityRelationshipList);

                        FluentActions.Awaiting(() =>
                                                    systemUnderTest.OnConnectionUpdated(new Guid(), connectedOrgFriendlyName)
                                                )
                                     .Should()
                                     .NotThrow();
                    }
                }
            }

            view.VerifySet(a => a.CurrentConnection = $"Connected to: {connectedOrgFriendlyName}");
            view.VerifyGet(a => a.ShowSystemAttributes);
            MetadataServiceMock.Verify(a => a.RetrieveEntities(It.IsAny<IOrganizationService>()));
            ServiceMock.Verify();
            NotificationServiceMock.VerifyAll();
            ExceptionServicerMock.VerifyAll();
        }

        [TestMethod]
        public void OnConnectionUpdatedRetrieveEntitiesThrowsAnException()
        {
            var connectedOrgFriendlyName = "testOrg";

            var entityResultList = new List<Microsoft.Xrm.Sdk.Metadata.EntityMetadata> { };
            MetadataServiceMock.Setup(a => a.RetrieveEntities(It.IsAny<IOrganizationService>()))
                               .Throws<Exception>();

            using (var entityList = new System.Windows.Forms.TreeView())
            {
                using (var entityAttributeList = new System.Windows.Forms.ListView())
                {
                    using (var entityRelationshipList = new System.Windows.Forms.ListView())
                    {
                        view.SetupGet(a => a.EntityList).Returns(entityList);
                        view.SetupGet(a => a.EntityAttributeList).Returns(entityAttributeList);
                        view.SetupGet(a => a.EntityRelationshipList).Returns(entityRelationshipList);

                        FluentActions.Awaiting(() =>
                                                    systemUnderTest.OnConnectionUpdated(new Guid(), connectedOrgFriendlyName)
                                                )
                                     .Should()
                                     .Throw<Exception>();
                    }
                }
            }

            view.VerifySet(a => a.CurrentConnection = $"Connected to: {connectedOrgFriendlyName}");
            view.VerifyGet(a => a.ShowSystemAttributes);
            MetadataServiceMock.Verify(a => a.RetrieveEntities(It.IsAny<IOrganizationService>()));
            ServiceMock.Verify();
            NotificationServiceMock.VerifyAll();
            ExceptionServicerMock.VerifyAll();
        }

        [TestMethod]
        public void GetAttributeList()
        {
            var inputEntityLogicalName = "account_contact";

            FluentActions.Invoking(() =>
            systemUnderTest.GetAttributeList(inputEntityLogicalName)
                )
                .Should()
                .NotThrow();
        }

        [TestMethod]
        public void FilterAttributes()
        {
            var entityMetadata = InstantiateEntityMetaData("case");
            bool showSystemAttributes = true;

            FluentActions.Invoking(() =>
            systemUnderTest.FilterAttributes(entityMetadata, showSystemAttributes)
                )
                .Should()
                .NotThrow();
        }

        [TestMethod]
        public void ClearMemory()
        {
            FluentActions.Invoking(() =>
            systemUnderTest.ClearMemory()
                )
                .Should()
                .NotThrow();
        }

        [TestMethod]
        public void LoadSchemaFile()
        {
            string schemaFilePath = "";
            bool working = true;
            var inputEntityAttributes = new Dictionary<string, HashSet<string>>();
            var inputEntityRelationships = new Dictionary<string, HashSet<string>>();

            FluentActions.Invoking(() =>
            systemUnderTest.LoadSchemaFile(schemaFilePath, working, NotificationServiceMock.Object, inputEntityAttributes, inputEntityRelationships)
                )
                .Should()
                .NotThrow();
        }

        [TestMethod]
        public void ClearAllListViews()
        {
            FluentActions.Invoking(() =>
                systemUnderTest.ClearAllListViews()
                )
                .Should()
                .NotThrow();
        }

        [TestMethod]
        public void HandleListViewEntitiesSelectedIndexChanged()
        {
            var inputEntityLogicalName = "account_contact";
            var inputSelectedEntity = new HashSet<string>();

            FluentActions.Awaiting(() =>
                systemUnderTest.HandleListViewEntitiesSelectedIndexChanged(inputEntityRelationships, inputEntityLogicalName, inputSelectedEntity)
                    )
                    .Should()
                    .NotThrow();
        }

        [TestMethod]
        public void ManageWorkingStateTrue()
        {
            FluentActions.Invoking(() =>
systemUnderTest.ManageWorkingState(true)
                )
                .Should()
                .NotThrow();
        }

        [TestMethod]
        public void ManageWorkingStateFalse()
        {
            FluentActions.Invoking(() => systemUnderTest.ManageWorkingState(false))
                        .Should()
                        .NotThrow();
        }

        [TestMethod]
        public void PopulateAttributes()
        {
            string entityLogicalName = "case";

            FluentActions.Invoking(() => systemUnderTest.PopulateAttributes(entityLogicalName, serviceParameters))
                        .Should()
                        .NotThrow();
        }

        [TestMethod]
        public void PopulateRelationship()
        {
            string entityLogicalName = "case";
            var inputEntityRelationships = new Dictionary<string, HashSet<string>>();

            FluentActions.Invoking(() => systemUnderTest.PopulateRelationship(entityLogicalName, inputEntityRelationships, serviceParameters))
                            .Should()
                            .NotThrow();
        }

        [TestMethod]
        public void AddSelectedEntitiesWhenSelectedEntitySetIsNullAndSelectedItemCountIsZero()
        {
            string entityLogicalName = "account_contact";

            inputEntityRelationships = new Dictionary<string, HashSet<string>>();
            var selectedItemsCount = 0;

            FluentActions.Invoking(() => systemUnderTest.AddSelectedEntities(selectedItemsCount, entityLogicalName, null))
                         .Should()
                         .NotThrow();
        }

        [TestMethod]
        public void AddSelectedEntitiesWhenSelectedEntitySetIsNullAndSelectedItemCountIsNotZero()
        {
            string entityLogicalName = "account_contact";

            inputEntityRelationships = new Dictionary<string, HashSet<string>>();
            var selectedItemsCount = 2;

            FluentActions.Invoking(() => systemUnderTest.AddSelectedEntities(selectedItemsCount, entityLogicalName, null))
                         .Should()
                         .Throw<NullReferenceException>();
        }

        [TestMethod]
        public void AddSelectedEntitiesWhenSelectedEntitySetIsNotNullAndSelectedEntityDoesNotContainLogicalName()
        {
            string entityLogicalName = "account_contact";

            inputEntityRelationships = new Dictionary<string, HashSet<string>>();
            var selectedItemsCount = 2;
            var selectedEntity = new HashSet<string>();

            FluentActions.Invoking(() => systemUnderTest.AddSelectedEntities(selectedItemsCount, entityLogicalName, selectedEntity))
                         .Should()
                         .NotThrow();

            selectedEntity.Count.Should().Be(1);
        }

        [TestMethod]
        public void AddSelectedEntitiesWhenSelectedEntitySetIsNotNullAndSelectedEntityContainsLogicalName()
        {
            string entityLogicalName = "account_contact";

            inputEntityRelationships = new Dictionary<string, HashSet<string>>();
            var selectedItemsCount = 2;
            var selectedEntity = new HashSet<string>
            {
                entityLogicalName
            };

            FluentActions.Invoking(() => systemUnderTest.AddSelectedEntities(selectedItemsCount, entityLogicalName, selectedEntity))
                         .Should()
                         .NotThrow();

            selectedEntity.Count.Should().Be(1);
        }

        [TestMethod]
        public void AddSelectedEntitiesWhenSelectedItemCountIsZero()
        {
            string entityLogicalName = "account_contact";

            inputEntityRelationships = new Dictionary<string, HashSet<string>>();
            var selectedItemsCount = 0;
            var selectedEntity = new HashSet<string>();

            FluentActions.Invoking(() => systemUnderTest.AddSelectedEntities(selectedItemsCount, entityLogicalName, selectedEntity))
                         .Should()
                         .NotThrow();

            selectedEntity.Count.Should().Be(0);
        }
    }
}