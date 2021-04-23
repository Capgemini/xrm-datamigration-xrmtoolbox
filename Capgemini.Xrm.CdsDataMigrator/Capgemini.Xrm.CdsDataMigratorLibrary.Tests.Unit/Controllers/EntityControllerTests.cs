using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Capgemini.Xrm.CdsDataMigratorLibrary.Controllers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk.Metadata;
using Moq;

namespace Capgemini.Xrm.CdsDataMigrator.Tests.Unit.Controllers
{
    [TestClass]
    public class EntityControllerTests : TestBase
    {
        private Dictionary<string, HashSet<string>> inputEntityRelationships;
        private Dictionary<string, HashSet<string>> inputEntityAttributes;

        private EntityController systemUnderTest;

        [TestInitialize]
        public void Setup()
        {
            SetupServiceMocks();
            inputEntityRelationships = new Dictionary<string, HashSet<string>>();
            inputEntityAttributes = new Dictionary<string, HashSet<string>>();

            systemUnderTest = new EntityController();
        }

        [TestMethod]
        public void StoreEntityDataNullEntityList()
        {
            FluentActions.Invoking(() => systemUnderTest.StoreEntityData(null, inputEntityAttributes, inputEntityRelationships))
                         .Should()
                         .NotThrow();

            inputEntityAttributes.Count.Should().Be(0);
            inputEntityRelationships.Count.Should().Be(0);
        }

        [TestMethod]
        public void StoreEntityDataNoEntities()
        {
            var crmEntity = new List<DataMigration.Model.CrmEntity>();

            FluentActions.Invoking(() => systemUnderTest.StoreEntityData(crmEntity.ToArray(), inputEntityAttributes, inputEntityRelationships))
                         .Should()
                         .NotThrow();

            inputEntityAttributes.Count.Should().Be(0);
            inputEntityRelationships.Count.Should().Be(0);
        }

        [TestMethod]
        public void StoreEntityDataHasEntitiesDuplicateEntityLogicalName()
        {
            var maxAttributes = 6;
            var maxRelationships = 3;

            var crmEntity = new List<DataMigration.Model.CrmEntity>();

            for (int entityCount = 0; entityCount < 5; entityCount++)
            {
                var entity = new DataMigration.Model.CrmEntity
                {
                    Name = "TestEntity"
                };

                for (int attributeCount = 0; attributeCount < maxAttributes; attributeCount++)
                {
                    entity.CrmFields.Add(new Capgemini.Xrm.DataMigration.Model.CrmField { FieldName = $"FieldName{attributeCount}" });
                }

                for (int relationshipCount = 0; relationshipCount < maxRelationships; relationshipCount++)
                {
                    entity.CrmRelationships.Add(new Capgemini.Xrm.DataMigration.Model.CrmRelationship { RelationshipName = $"RelationshipName{relationshipCount}" });
                }

                crmEntity.Add(entity);
            }

            FluentActions.Invoking(() => systemUnderTest.StoreEntityData(crmEntity.ToArray(), inputEntityAttributes, inputEntityRelationships))
                         .Should()
                         .Throw<ArgumentException>();
        }

        [TestMethod]
        public void StoreEntityDataHasEntities()
        {
            var maxAttributes = 6;
            var maxRelationships = 3;
            var maxEntityCount = 5;
            var index = 3;

            var crmEntity = new List<DataMigration.Model.CrmEntity>();

            for (int entityCount = 0; entityCount < maxEntityCount; entityCount++)
            {
                var entity = new DataMigration.Model.CrmEntity
                {
                    Name = $"TestEntity{entityCount}"
                };

                for (int attributeCount = 0; attributeCount < maxAttributes; attributeCount++)
                {
                    entity.CrmFields.Add(new Capgemini.Xrm.DataMigration.Model.CrmField { FieldName = $"FieldName{attributeCount}" });
                }

                for (int relationshipCount = 0; relationshipCount < maxRelationships; relationshipCount++)
                {
                    entity.CrmRelationships.Add(new Capgemini.Xrm.DataMigration.Model.CrmRelationship { RelationshipName = $"RelationshipName{relationshipCount}" });
                }

                crmEntity.Add(entity);
            }

            FluentActions.Invoking(() => systemUnderTest.StoreEntityData(crmEntity.ToArray(), inputEntityAttributes, inputEntityRelationships))
                         .Should()
                         .NotThrow();

            inputEntityAttributes.Count.Should().Be(maxEntityCount);
            inputEntityRelationships.Count.Should().Be(maxEntityCount);

            inputEntityAttributes[$"TestEntity{index}"].Count.Should().Be(maxAttributes);
            inputEntityRelationships[$"TestEntity{index}"].Count.Should().Be(maxRelationships);
        }

        [TestMethod]
        public void StoreEntityDataHasEntitiesWithNoAttributesAndRelationships()
        {
            var maxAttributes = 0;
            var maxRelationships = 0;
            var maxEntityCount = 5;
            var index = 3;

            var crmEntity = new List<DataMigration.Model.CrmEntity>();

            for (int entityCount = 0; entityCount < maxEntityCount; entityCount++)
            {
                var entity = new DataMigration.Model.CrmEntity
                {
                    Name = $"TestEntity{entityCount}"
                };

                crmEntity.Add(entity);
            }

            FluentActions.Invoking(() => systemUnderTest.StoreEntityData(crmEntity.ToArray(), inputEntityAttributes, inputEntityRelationships))
                         .Should()
                         .NotThrow();

            inputEntityAttributes.Count.Should().Be(maxEntityCount);
            inputEntityRelationships.Count.Should().Be(maxEntityCount);

            inputEntityAttributes[$"TestEntity{index}"].Count.Should().Be(maxAttributes);
            inputEntityRelationships[$"TestEntity{index}"].Count.Should().Be(maxRelationships);
        }

        [TestMethod]
        public void GetEntityLogicalNameNullListViewItem()
        {
            System.Windows.Forms.ListViewItem entityitem = null;

            var actual = systemUnderTest.GetEntityLogicalName(entityitem);

            actual.Should().BeNull();
        }

        [TestMethod]
        public void GetEntityLogicalNameInstantiatedListViewItemWithNullTag()
        {
            var entityitem = new System.Windows.Forms.ListViewItem
            {
                Tag = null
            };
            var actual = systemUnderTest.GetEntityLogicalName(entityitem);

            actual.Should().BeNull();
        }

        [TestMethod]
        public void GetEntityLogicalNameInstantiatedListViewItemWithEntityMetadataTag()
        {
            var entityMetadata = new EntityMetadata() { LogicalName = "account" };

            var entityitem = new System.Windows.Forms.ListViewItem
            {
                Tag = entityMetadata
            };

            var actual = systemUnderTest.GetEntityLogicalName(entityitem);

            actual.Should().Be(entityMetadata.LogicalName);
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
        public void RetrieveSourceEntitiesListShowSystemAttributesIsFalse()
        {
            var showSystemAttributes = false;
            string entityLogicalName = "account_contact";
            SetupMockObjects(entityLogicalName);
            var inputCachedMetadata = new List<EntityMetadata>();

            var serviceParameters = GenerateMigratorParameters();

            var actual = systemUnderTest.RetrieveSourceEntitiesList(showSystemAttributes, inputCachedMetadata, inputEntityAttributes, serviceParameters);

            actual.Count.Should().Be(1);
        }

        [TestMethod]
        public void RetrieveSourceEntitiesListShowSystemAttributesIsTrue()
        {
            var showSystemAttributes = true;
            string entityLogicalName = "account_contact";
            SetupMockObjects(entityLogicalName);
            var inputCachedMetadata = new List<EntityMetadata>();
            var serviceParameters = GenerateMigratorParameters();

            var actual = systemUnderTest.RetrieveSourceEntitiesList(showSystemAttributes, inputCachedMetadata, inputEntityAttributes, serviceParameters);

            actual.Count.Should().Be(1);
        }

        [TestMethod]
        public void PopulateEntitiesListViewWhenThereIsAnException()
        {
            var items = new List<System.Windows.Forms.ListViewItem>
            {
                new System.Windows.Forms.ListViewItem("Item1"),
                new System.Windows.Forms.ListViewItem("Item2")
            };
            Exception exception = new Exception();

            NotificationServiceMock.Setup(x => x.DisplayErrorFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()))
                                .Verifiable();
            NotificationServiceMock.Setup(x => x.DisplayWarningFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()))
                               .Verifiable();
            using (var listView = new System.Windows.Forms.ListView())
            {
                FluentActions.Invoking(() => systemUnderTest.PopulateEntitiesListView(items, exception, null, listView, NotificationServiceMock.Object))
                        .Should()
                        .NotThrow();
            }

            NotificationServiceMock.Verify(x => x.DisplayErrorFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()), Times.Once);
            NotificationServiceMock.Verify(x => x.DisplayWarningFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void PopulateEntitiesListViewWhenThereIsNoException()
        {
            var items = new List<System.Windows.Forms.ListViewItem>();
            Exception exception = null;

            NotificationServiceMock.Setup(x => x.DisplayWarningFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()))
                                .Verifiable();
            using (var listView = new System.Windows.Forms.ListView())
            {
                FluentActions.Invoking(() => systemUnderTest.PopulateEntitiesListView(items, exception, null, listView, NotificationServiceMock.Object))
                        .Should()
                        .NotThrow();
            }

            NotificationServiceMock.Verify(x => x.DisplayErrorFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()), Times.Never);
            NotificationServiceMock.Verify(x => x.DisplayWarningFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void PopulateEntitiesListViewWhenThereAreListItems()
        {
            var items = new List<System.Windows.Forms.ListViewItem>
            {
                new System.Windows.Forms.ListViewItem("Item1"),
                new System.Windows.Forms.ListViewItem("Item2")
            };
            Exception exception = null;

            NotificationServiceMock.Setup(x => x.DisplayErrorFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()))
                                .Verifiable();

            using (var listView = new System.Windows.Forms.ListView())
            {
                FluentActions.Invoking(() => systemUnderTest.PopulateEntitiesListView(items, exception, null, listView, NotificationServiceMock.Object))
                        .Should()
                        .NotThrow();
            }

            NotificationServiceMock.Verify(x => x.DisplayErrorFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()), Times.Never);
            NotificationServiceMock.Verify(x => x.DisplayWarningFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void IsInvalidForCustomizationIsCustomEntity()
        {
            EntityMetadata entity = new EntityMetadata
            {
                LogicalName = "contactattnoentity1"
            };
            var isLogicalEntityField = entity.GetType().GetRuntimeFields().First(a => a.Name == "_isCustomEntity");
            isLogicalEntityField.SetValue(entity, (bool?)true);

            var item = new System.Windows.Forms.ListViewItem("Item1");

            FluentActions.Invoking(() => systemUnderTest.IsInvalidForCustomization(entity, item))
                           .Should()
                           .NotThrow();

            item.ForeColor.Should().Be(System.Drawing.Color.DarkGreen);
        }

        [TestMethod]
        public void IsInvalidForCustomizationIsIntersect()
        {
            EntityMetadata entity = new EntityMetadata
            {
                LogicalName = "contactattnoentity1",
            };
            var isLogicalEntityField = entity.GetType().GetRuntimeFields().First(a => a.Name == "_isIntersect");
            isLogicalEntityField.SetValue(entity, (bool?)true);

            var item = new System.Windows.Forms.ListViewItem("Item1");

            FluentActions.Invoking(() => systemUnderTest.IsInvalidForCustomization(entity, item))
                         .Should()
                         .NotThrow();

            item.ForeColor.Should().Be(System.Drawing.Color.Red);
            item.ToolTipText.Should().Contain("Intersect Entity, ");
        }

        [TestMethod]
        public void IsInvalidForCustomizationIsIntersectNull()
        {
            EntityMetadata entity = new EntityMetadata
            {
                LogicalName = "contactattnoentity1",
            };

            var item = new System.Windows.Forms.ListViewItem("Item1");

            FluentActions.Invoking(() => systemUnderTest.IsInvalidForCustomization(entity, item))
                         .Should()
                         .NotThrow();
            item.ForeColor.Should().NotBe(System.Drawing.Color.Red);
            item.ToolTipText.Should().NotContain("Intersect Entity, ");
        }

        [TestMethod]
        public void IsInvalidForCustomizationIsLogicalEntity()
        {
            EntityMetadata entity = new EntityMetadata
            {
                LogicalName = "contactattnoentity1"
            };
            var isLogicalEntityField = entity.GetType().GetRuntimeFields().First(a => a.Name == "_isLogicalEntity");
            isLogicalEntityField.SetValue(entity, (bool?)true);

            var item = new System.Windows.Forms.ListViewItem("Item1");

            FluentActions.Invoking(() => systemUnderTest.IsInvalidForCustomization(entity, item))
                         .Should()
                         .NotThrow();

            item.ForeColor.Should().Be(System.Drawing.Color.Red);
            item.ToolTipText.Should().Contain("Logical Entity");
        }

        [TestMethod]
        public void IsInvalidForCustomizationIsLogicalEntityNull()
        {
            EntityMetadata entity = new EntityMetadata
            {
                LogicalName = "contactattnoentity1",
            };

            var item = new System.Windows.Forms.ListViewItem("Item1");

            FluentActions.Invoking(() => systemUnderTest.IsInvalidForCustomization(entity, item))
                         .Should()
                         .NotThrow();

            item.ForeColor.Should().NotBe(System.Drawing.Color.Red);
            item.ToolTipText.Should().NotContain("Logical Entity");
        }
    }
}