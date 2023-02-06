using Capgemini.Xrm.CdsDataMigrator.Tests.Unit;
using Capgemini.Xrm.CdsDataMigratorLibrary.Core;
using Capgemini.Xrm.CdsDataMigratorLibrary.Exceptions;
using Capgemini.Xrm.CdsDataMigratorLibrary.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Moq;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

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

            inputEntityAttributes = new Dictionary<string, HashSet<string>>();
            inputEntityRelationships = new Dictionary<string, HashSet<string>>();
            view = new Mock<ISchemaGeneratorView>();
            settings = new Settings();

            systemUnderTest = new SchemaGeneratorPresenter(
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

                        FluentActions
                            .Awaiting(() => systemUnderTest.OnConnectionUpdated(new Guid(), connectedOrgFriendlyName))
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

            var entityResultList = new List<EntityMetadata> { };
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

                        FluentActions
                            .Awaiting(() => systemUnderTest.OnConnectionUpdated(new Guid(), connectedOrgFriendlyName))
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
        public void GetAttributeListShowSystemAttributesTrue()
        {
            string entityLogicalName = "case";

            var entityResultList = InstantiateEntityMetaData(entityLogicalName);
            InsertAttributeList(entityResultList, new List<string> { "column1", "column2" });

            MetadataServiceMock.Setup(a => a.RetrieveEntities(It.IsAny<string>(),
                                                              It.IsAny<IOrganizationService>(),
                                                              It.IsAny<IExceptionService>()))
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
                        view.SetupGet(a => a.ShowSystemAttributes).Returns(true);

                        var actual = systemUnderTest.GetAttributeList(entityLogicalName);

                        actual.Count.Should().Be(2);
                    }
                }
            }
        }

        [TestMethod]
        public void GetAttributeListShowSystemAttributesFalse()
        {
            string entityLogicalName = "case";

            var entityResultList = InstantiateEntityMetaData(entityLogicalName);
            InsertAttributeList(entityResultList, new List<string> { "column1", "column2" });

            MetadataServiceMock.Setup(a => a.RetrieveEntities(It.IsAny<string>(),
                                                              It.IsAny<IOrganizationService>(),
                                                              It.IsAny<IExceptionService>()))
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
                        view.SetupGet(a => a.ShowSystemAttributes).Returns(false);

                        var actual = systemUnderTest.GetAttributeList(entityLogicalName);

                        actual.Count.Should().Be(0);
                    }
                }
            }
        }

        [TestMethod]
        public void FilterAttributesShowSystemAttributesTrue()
        {
            var entityLogicalName = "case";
            var entityMetadata = InstantiateEntityMetaData(entityLogicalName);
            InsertAttributeList(entityMetadata, new List<string> { "column1", "column2" });
            bool showSystemAttributes = true;

            var actual = systemUnderTest.FilterAttributes(entityMetadata, showSystemAttributes);

            actual.Count.Should().Be(2);
        }

        [TestMethod]
        public void FilterAttributesShowSystemAttributesFalse()
        {
            var entityLogicalName = "case";
            var entityMetadata = InstantiateEntityMetaData(entityLogicalName);
            InsertAttributeList(entityMetadata, new List<string> { "column1", "column2" });
            bool showSystemAttributes = false;

            var actual = systemUnderTest.FilterAttributes(entityMetadata, showSystemAttributes);

            actual.Count.Should().Be(0);
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
            var logicalName = "case";
            string schemaFilePath = "TestData\\testschemafile.xml";
            bool working = true;
            var inputEntityAttributes = new Dictionary<string, HashSet<string>>();
            var inputEntityRelationships = new Dictionary<string, HashSet<string>>();

            var selectedEntities = new List<EntityMetadata> { InstantiateEntityMetaData(logicalName) };

            view.SetupGet(a => a.SelectedEntities).Returns(selectedEntities);
            view.Setup(a => a.CloseInformationPanel());
            view.Setup(a => a.ShowInformationPanel(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()));

            FluentActions
                .Awaiting(() => systemUnderTest.LoadSchemaFile(
                        schemaFilePath,
                        working,
                        NotificationServiceMock.Object,
                        inputEntityAttributes,
                        inputEntityRelationships))
                .Should()
                .NotThrow();

            view.VerifyGet(a => a.SelectedEntities);
            view.Verify(a => a.CloseInformationPanel());
            view.Verify(a => a.ShowInformationPanel(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()));
            NotificationServiceMock.Verify(a => a.DisplayFeedback(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void LoadSchemaFileThrowsException()
        {
            var logicalName = "case";
            string schemaFilePath = "TestData\\testschemafile.xml";
            bool working = true;
            var inputEntityAttributes = new Dictionary<string, HashSet<string>>();
            var inputEntityRelationships = new Dictionary<string, HashSet<string>>();

            var selectedEntities = new List<EntityMetadata> { InstantiateEntityMetaData(logicalName) };

            view.SetupGet(a => a.SelectedEntities).Returns(selectedEntities);
            view.Setup(a => a.CloseInformationPanel());
            view.Setup(a => a.ShowInformationPanel(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .Throws(new Exception());
            NotificationServiceMock.Setup(a => a.DisplayFeedback(It.IsAny<string>()));

            FluentActions
                .Awaiting(() => systemUnderTest.LoadSchemaFile(
                    schemaFilePath,
                    working,
                    NotificationServiceMock.Object,
                    inputEntityAttributes,
                    inputEntityRelationships))
                .Should()
                .NotThrow();

            view.VerifyGet(a => a.SelectedEntities, Times.Never);
            view.Verify(a => a.CloseInformationPanel());
            view.Verify(a => a.ShowInformationPanel(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()));
            NotificationServiceMock.Verify(a => a.DisplayFeedback(It.IsAny<string>()));
        }

        [TestMethod]
        public void LoadSchemaFileEmptySchemaFilePath()
        {
            var logicalName = "case";
            string schemaFilePath = " ";
            bool working = true;
            var inputEntityAttributes = new Dictionary<string, HashSet<string>>();
            var inputEntityRelationships = new Dictionary<string, HashSet<string>>();

            var selectedEntities = new List<EntityMetadata> { InstantiateEntityMetaData(logicalName) };

            view.SetupGet(a => a.SelectedEntities).Returns(selectedEntities);
            view.Setup(a => a.CloseInformationPanel());
            view.Setup(a => a.ShowInformationPanel(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .Throws(new Exception());
            NotificationServiceMock.Setup(a => a.DisplayFeedback(It.IsAny<string>()));

            FluentActions
                .Awaiting(() => systemUnderTest.LoadSchemaFile(
                    schemaFilePath,
                    working,
                    NotificationServiceMock.Object,
                    inputEntityAttributes,
                    inputEntityRelationships))
                .Should()
                .NotThrow();

            view.VerifyGet(a => a.SelectedEntities, Times.Never);
            view.Verify(a => a.CloseInformationPanel(), Times.Never);
            view.Verify(a => a.ShowInformationPanel(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()), Times.Never);
            NotificationServiceMock.Verify(a => a.DisplayFeedback("Please specify the Schema File to load!"), Times.Once);
        }

        [TestMethod]
        public void ClearAllListViews()
        {
            FluentActions.Invoking(() => systemUnderTest.ClearAllListViews())
                .Should()
                .NotThrow();
        }

        [TestMethod]
        public void HandleListViewEntitiesSelectedIndexChanged()
        {
            var entityLogicalName = "account_contact";
            var inputSelectedEntity = new HashSet<string>();

            var selectedEntities = new List<EntityMetadata> { InstantiateEntityMetaData(entityLogicalName) };

            view.SetupGet(a => a.SelectedEntities).Returns(selectedEntities);

            FluentActions
                .Awaiting(() => systemUnderTest.HandleListViewEntitiesSelectedIndexChanged(
                        inputEntityRelationships,
                        entityLogicalName,
                        inputSelectedEntity))
                .Should()
                .NotThrow();

            view.VerifyGet(a => a.SelectedEntities);
        }

        [TestMethod]
        public void ManageWorkingStateTrue()
        {
            FluentActions.Invoking(() => systemUnderTest.ManageWorkingState(true))
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
        public void PopulateAttributesWithNullViewProperties()
        {
            string entityLogicalName = "case";

            FluentActions.Invoking(() => systemUnderTest.PopulateAttributes(entityLogicalName, serviceParameters))
                        .Should()
                        .NotThrow();
        }

        [TestMethod]
        public void PopulateAttributes()
        {
            string entityLogicalName = "case";

            var entityResultList = InstantiateEntityMetaData(entityLogicalName);
            InsertAttributeList(entityResultList, new List<string> { "column1", "column2" });

            MetadataServiceMock.Setup(a => a.RetrieveEntities(It.IsAny<string>(),
                                                              It.IsAny<IOrganizationService>(),
                                                              It.IsAny<IExceptionService>()))
                           .Returns(entityResultList);

            using (var entityList = new TreeView())
            {
                using (var entityAttributeList = new ListView())
                {
                    using (var entityRelationshipList = new ListView())
                    {
                        view.SetupGet(a => a.EntityList).Returns(entityList);
                        view.SetupGet(a => a.EntityAttributeList).Returns(entityAttributeList);
                        view.SetupGet(a => a.EntityRelationshipList).Returns(entityRelationshipList);

                        FluentActions.Invoking(() => systemUnderTest.PopulateAttributes(entityLogicalName, serviceParameters))
                        .Should()
                        .NotThrow();
                    }
                }
            }

            view.VerifyGet(a => a.ShowSystemAttributes);
            MetadataServiceMock.Verify(a => a.RetrieveEntities(It.IsAny<string>(),
                                                              It.IsAny<IOrganizationService>(),
                                                              It.IsAny<IExceptionService>()));
            ServiceMock.Verify();
            NotificationServiceMock.VerifyAll();
            ExceptionServicerMock.VerifyAll();
        }

        [TestMethod]
        public void PopulateAttributesRetrieveEntitiesThrowsException()
        {
            string entityLogicalName = "case";

            var entityResultList = InstantiateEntityMetaData(entityLogicalName);
            InsertAttributeList(entityResultList, new List<string> { "column1", "column2" });

            MetadataServiceMock.Setup(a => a.RetrieveEntities(It.IsAny<string>(),
                                                              It.IsAny<IOrganizationService>(),
                                                              It.IsAny<IExceptionService>()))
                           .Throws(new Exception());

            using (var entityList = new System.Windows.Forms.TreeView())
            {
                using (var entityAttributeList = new System.Windows.Forms.ListView())
                {
                    using (var entityRelationshipList = new System.Windows.Forms.ListView())
                    {
                        view.SetupGet(a => a.EntityList).Returns(entityList);
                        view.SetupGet(a => a.EntityAttributeList).Returns(entityAttributeList);
                        view.SetupGet(a => a.EntityRelationshipList).Returns(entityRelationshipList);

                        FluentActions.Invoking(() => systemUnderTest.PopulateAttributes(entityLogicalName, serviceParameters))
                        .Should()
                        .NotThrow();
                    }
                }
            }

            view.VerifyGet(a => a.ShowSystemAttributes);
            MetadataServiceMock.Verify(a => a.RetrieveEntities(It.IsAny<string>(),
                                                              It.IsAny<IOrganizationService>(),
                                                              It.IsAny<IExceptionService>()));
            ServiceMock.Verify();
            NotificationServiceMock.VerifyAll();
            ExceptionServicerMock.VerifyAll();
        }

        [TestMethod]
        public void PopulateRelationship()
        {
            string entityLogicalName = "case";
            var inputEntityRelationships = new Dictionary<string, HashSet<string>>();

            var entityMetaData = InstantiateEntityMetaData(entityLogicalName);

            MetadataServiceMock.Setup(a => a.RetrieveEntities(It.IsAny<string>(),
                                                            It.IsAny<IOrganizationService>(),
                                                            It.IsAny<IExceptionService>()))
                                .Returns(entityMetaData);

            using (var entityRelationshipList = new System.Windows.Forms.ListView())
            {
                view.SetupGet(a => a.EntityRelationshipList).Returns(entityRelationshipList);

                FluentActions.Invoking(() => systemUnderTest.PopulateRelationship(entityLogicalName, inputEntityRelationships, serviceParameters))
                            .Should()
                            .NotThrow();
            }

            view.Verify(a => a.EntityRelationshipList);
            MetadataServiceMock.Verify(a => a.RetrieveEntities(It.IsAny<string>(),
                                                            It.IsAny<IOrganizationService>(),
                                                            It.IsAny<IExceptionService>()));
        }

        [TestMethod]
        public void PopulateRelationshipThrowsException()
        {
            string entityLogicalName = "case";
            var inputEntityRelationships = new Dictionary<string, HashSet<string>>();

            var entityMetaData = InstantiateEntityMetaData(entityLogicalName);

            MetadataServiceMock.Setup(a => a.RetrieveEntities(It.IsAny<string>(),
                                                            It.IsAny<IOrganizationService>(),
                                                            It.IsAny<IExceptionService>()))
                                .Throws(new Exception());

            NotificationServiceMock.Setup(a => a.DisplayErrorFeedback(It.IsAny<IWin32Window>(), It.IsAny<string>()));

            using (var entityRelationshipList = new ListView())
            {
                view.SetupGet(a => a.EntityRelationshipList).Returns(entityRelationshipList);

                FluentActions.Invoking(() => systemUnderTest.PopulateRelationship(entityLogicalName, inputEntityRelationships, serviceParameters))
                            .Should()
                            .NotThrow();
            }

            view.Verify(a => a.EntityRelationshipList);
            MetadataServiceMock.Verify(a => a.RetrieveEntities(It.IsAny<string>(),
                                                            It.IsAny<IOrganizationService>(),
                                                            It.IsAny<IExceptionService>()));
            NotificationServiceMock.Verify(a => a.DisplayErrorFeedback(It.IsAny<IWin32Window>(), It.IsAny<string>()));
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

        [TestMethod]
        public void StoreAttributeIfRequiresKeyCurrentValueIsChecked()
        {
            var entityLogicalName = "contact";
            var attributeLogicalName = "contactid";
            var itemCheckEventArgs = new ItemCheckEventArgs(0, CheckState.Unchecked, CheckState.Checked);

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

            var itemCheckEventArgs = new ItemCheckEventArgs(0, CheckState.Checked, CheckState.Unchecked);

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
            var itemCheckEventArgs = new ItemCheckEventArgs(0, CheckState.Unchecked, CheckState.Checked);

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

            var itemCheckEventArgs = new ItemCheckEventArgs(0, CheckState.Unchecked, CheckState.Checked);

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

            var itemCheckEventArgs = new ItemCheckEventArgs(0, CheckState.Unchecked, CheckState.Checked);

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

            var itemCheckEventArgs = new ItemCheckEventArgs(0, CheckState.Checked, CheckState.Unchecked);

            FluentActions.Invoking(() => systemUnderTest.StoreAttriubteIfKeyExists(attributeLogicalName, itemCheckEventArgs, inputEntityAttributes, entityLogicalName))
                         .Should()
                         .NotThrow();

            inputEntityAttributes.Count.Should().Be(1);
            inputEntityAttributes[entityLogicalName].Contains(attributeLogicalName).Should().BeTrue();
        }

        [TestMethod]
        public void LoadSchemaEventHandler()
        {
            string entityLogicalName = "case";
            string schemaFilePath = "TestData\\testschemafile.xml";
            var entityResultList = new List<EntityMetadata> { };
            MetadataServiceMock.Setup(a => a.RetrieveEntities(It.IsAny<IOrganizationService>()))
                           .Returns(entityResultList);

            var selectedEntities = new List<EntityMetadata> { InstantiateEntityMetaData(entityLogicalName) };

            view.SetupGet(a => a.SelectedEntities).Returns(selectedEntities);
            view.Setup(a => a.ShowInformationPanel(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()));

            var e = new MigratorEventArgs<string>(schemaFilePath);

            FluentActions.Invoking(() => systemUnderTest.LoadSchemaEventHandler(this, e))
                        .Should()
                        .NotThrow();
        }
    }
}