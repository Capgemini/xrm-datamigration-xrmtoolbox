using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.Xrm.Sdk.Metadata;
using Capgemini.Xrm.CdsDataMigrator.Tests.Unit;
using System.Reflection;
using Moq;
using Capgemini.Xrm.CdsDataMigratorLibrary.Core;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Extensions.Tests
{
    [TestClass]
    public class ListViewItemExtensionsTests : TestBase
    {
        private Dictionary<string, HashSet<string>> inputEntityRelationships;
        private Dictionary<string, HashSet<string>> inputEntityAttributes;


        [TestInitialize]
        public void Setup()
        {
            inputEntityRelationships = new Dictionary<string, HashSet<string>>();
            inputEntityAttributes = new Dictionary<string, HashSet<string>>();

            SetupServiceMocks();
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

            FluentActions.Invoking(() => item.IsInvalidForCustomization(entity))
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

            FluentActions.Invoking(() => item.IsInvalidForCustomization(entity))
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

            FluentActions.Invoking(() => item.IsInvalidForCustomization(entity))
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

            FluentActions.Invoking(() => item.IsInvalidForCustomization(entity))
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

            FluentActions.Invoking(() => item.IsInvalidForCustomization(entity))
                         .Should()
                         .NotThrow();

            item.ForeColor.Should().NotBe(System.Drawing.Color.Red);
            item.ToolTipText.Should().NotContain("Logical Entity");
        }


        [TestMethod]
        public void GetEntityLogicalNameNullListViewItem()
        {
            System.Windows.Forms.ListViewItem entityitem = null;

            var actual = entityitem.GetEntityLogicalName();

            actual.Should().BeNull();
        }

        [TestMethod]
        public void GetEntityLogicalNameInstantiatedListViewItemWithNullTag()
        {
            var entityitem = new System.Windows.Forms.ListViewItem
            {
                Tag = null
            };
            var actual = entityitem.GetEntityLogicalName();

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

            var actual = entityitem.GetEntityLogicalName();

            actual.Should().Be(entityMetadata.LogicalName);
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
                FluentActions.Invoking(() => items.PopulateEntitiesListView(exception, null, listView, NotificationServiceMock.Object))
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
                FluentActions.Invoking(() => items.PopulateEntitiesListView(exception, null, listView, NotificationServiceMock.Object))
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
                FluentActions.Invoking(() => items.PopulateEntitiesListView(exception, null, listView, NotificationServiceMock.Object))
                        .Should()
                        .NotThrow();
            }

            NotificationServiceMock.Verify(x => x.DisplayErrorFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()), Times.Never);
            NotificationServiceMock.Verify(x => x.DisplayWarningFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void SetListViewSortingWithEmptySettings()
        {
            using (var listview = new System.Windows.Forms.ListView())
            {
                int column = 0;
                var inputOrganisationId = Guid.NewGuid().ToString();
                var settings = new Settings();

                FluentActions.Invoking(() => listview.SetListViewSorting( column, inputOrganisationId, settings))
                             .Should()
                             .NotThrow();

                listview.ListViewItemSorter.Should().NotBeNull();
                listview.Sorting.Should().Be(System.Windows.Forms.SortOrder.Ascending);
            }
        }

        [TestMethod]
        public void SetListViewSortingWithSettingsContainingOrganisationIdAndListViewHasNoName()
        {
            using (var listview = new System.Windows.Forms.ListView())
            {
                int column = 0;
                var inputOrganisationId = Guid.NewGuid();
                var settings = new Settings();

                var item = new KeyValuePair<Guid, Organisations>(inputOrganisationId, new Organisations());
                settings.Organisations.Add(item);

                FluentActions.Invoking(() => listview.SetListViewSorting(  column, inputOrganisationId.ToString(), settings))
                             .Should()
                             .NotThrow();

                listview.ListViewItemSorter.Should().NotBeNull();
                listview.Sorting.Should().Be(System.Windows.Forms.SortOrder.Ascending);
            }
        }

        [TestMethod]
        public void SetListViewSortingWithSettingsContainsOrganisationIdAndListItemValueIsNotEqualToInputColumn()
        {
            using (var listview = new System.Windows.Forms.ListView())
            {
                int column = 0;
                var inputOrganisationId = Guid.NewGuid();
                listview.Name = inputOrganisationId.ToString();
                var settings = new Settings();

                var org = new Organisations();
                var listItem = new Item<string, int>(inputOrganisationId.ToString(), 1);
                org.Sortcolumns.Add(listItem);

                var item = new KeyValuePair<Guid, Organisations>(inputOrganisationId, org);
                settings.Organisations.Add(item);

                FluentActions.Invoking(() => listview.SetListViewSorting( column, inputOrganisationId.ToString(), settings))
                             .Should()
                             .NotThrow();

                listview.ListViewItemSorter.Should().NotBeNull();
                listview.Sorting.Should().Be(System.Windows.Forms.SortOrder.Ascending);
            }
        }

        [TestMethod]
        public void SetListViewSortingWithSettingsContainsOrganisationIdAndListItemValueIsEqualToInputColumnAndSortOrderAscending()
        {
            using (var listview = new System.Windows.Forms.ListView())
            {
                int column = 1;
                var inputOrganisationId = Guid.NewGuid();
                listview.Name = inputOrganisationId.ToString();
                listview.Sorting = System.Windows.Forms.SortOrder.Ascending;

                var settings = new Settings();

                var org = new Organisations();
                var listItem = new Item<string, int>(inputOrganisationId.ToString(), 1);
                org.Sortcolumns.Add(listItem);

                var item = new KeyValuePair<Guid, Organisations>(inputOrganisationId, org);
                settings.Organisations.Add(item);

                FluentActions.Invoking(() => listview.SetListViewSorting(  column, inputOrganisationId.ToString(), settings))
                             .Should()
                             .NotThrow();

                listview.ListViewItemSorter.Should().NotBeNull();
                listview.Sorting.Should().Be(System.Windows.Forms.SortOrder.Descending);
            }
        }

        [TestMethod]
        public void SetListViewSortingWithSettingsContainsOrganisationIdAndListItemValueIsEqualToInputColumnAndSortOrderNone()
        {
            using (var listview = new System.Windows.Forms.ListView())
            {
                int column = 1;
                var inputOrganisationId = Guid.NewGuid();
                listview.Name = inputOrganisationId.ToString();
                listview.Sorting = System.Windows.Forms.SortOrder.None;

                var settings = new Settings();

                var org = new Organisations();
                var listItem = new Item<string, int>(inputOrganisationId.ToString(), 1);
                org.Sortcolumns.Add(listItem);

                var item = new KeyValuePair<Guid, Organisations>(inputOrganisationId, org);
                settings.Organisations.Add(item);

                FluentActions.Invoking(() => listview.SetListViewSorting(  column, inputOrganisationId.ToString(), settings))
                             .Should()
                             .NotThrow();

                listview.ListViewItemSorter.Should().NotBeNull();
                listview.Sorting.Should().Be(System.Windows.Forms.SortOrder.Ascending);
            }
        }

        [TestMethod]
        public void SetListViewSortingWithSettingsContainsOrganisationIdAndListItemValueIsEqualToInputColumnAndSortOrderDescending()
        {
            using (var listview = new System.Windows.Forms.ListView())
            {
                int column = 1;
                var inputOrganisationId = Guid.NewGuid();
                listview.Name = inputOrganisationId.ToString();
                listview.Sorting = System.Windows.Forms.SortOrder.Descending;

                var settings = new Settings();

                var org = new Organisations();
                var listItem = new Item<string, int>(inputOrganisationId.ToString(), 1);
                org.Sortcolumns.Add(listItem);

                var item = new KeyValuePair<Guid, Organisations>(inputOrganisationId, org);
                settings.Organisations.Add(item);

                FluentActions.Invoking(() => listview.SetListViewSorting(  column, inputOrganisationId.ToString(), settings))
                             .Should()
                             .NotThrow();

                listview.ListViewItemSorter.Should().NotBeNull();
                listview.Sorting.Should().Be(System.Windows.Forms.SortOrder.Ascending);
            }
        }

        [TestMethod]
        public void OnPopulateRelationshipCompletedActionWithException()
        {
            Exception exception = new Exception();
            bool cancelled = false;
            bool showSystemAttributes = false;
            var result = new List<System.Windows.Forms.ListViewItem>();

            var eventArgs = new System.ComponentModel.RunWorkerCompletedEventArgs(result, exception, cancelled);

            NotificationServiceMock.Setup(x => x.DisplayErrorFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()))
                                .Verifiable();
            using (var listView = new System.Windows.Forms.ListView())
            {
                FluentActions.Invoking(() => listView.OnPopulateCompletedAction(eventArgs, NotificationServiceMock.Object, null, showSystemAttributes))
             .Should()
             .NotThrow();
            }

            NotificationServiceMock.Verify(x => x.DisplayErrorFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void OnPopulateRelationshipCompletedActionWithoutException()
        {
            Exception exception = null;
            bool cancelled = false;
            bool showSystemAttributes = false;
            var result = new List<System.Windows.Forms.ListViewItem>();

            var eventArgs = new System.ComponentModel.RunWorkerCompletedEventArgs(result, exception, cancelled);

            NotificationServiceMock.Setup(x => x.DisplayErrorFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()))
                                .Verifiable();
            using (var listView = new System.Windows.Forms.ListView())
            {
                FluentActions.Invoking(() => listView.OnPopulateCompletedAction(eventArgs, NotificationServiceMock.Object, null,  showSystemAttributes))
                             .Should()
                             .NotThrow();
            }

            NotificationServiceMock.Verify(x => x.DisplayErrorFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void OnPopulateRelationshipCompletedActionWithoutExceptionAndOnlySystemAttributes()
        {
            Exception exception = null;
            bool cancelled = false;
            bool showSystemAttributes = true;
            var result = new List<System.Windows.Forms.ListViewItem>();
            System.Windows.Forms.ListViewItem item = new System.Windows.Forms.ListViewItem { };
            var attribute = new AttributeMetadata { };
            var isCustomAttributeField = attribute.GetType().GetRuntimeFields().First(a => a.Name == "_isCustomAttribute");
            isCustomAttributeField.SetValue(attribute, (bool?)true);
            item.Tag = attribute;
            result.Add(item);

            var eventArgs = new System.ComponentModel.RunWorkerCompletedEventArgs(result, exception, cancelled);

            NotificationServiceMock.Setup(x => x.DisplayErrorFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()))
                                .Verifiable();
            using (var listView = new System.Windows.Forms.ListView())
            {
                FluentActions.Invoking(() => listView.OnPopulateCompletedAction(eventArgs, NotificationServiceMock.Object, null,   showSystemAttributes))
                             .Should()
                             .NotThrow();

                listView.Items.Count.Should().Be(1);
            }

            NotificationServiceMock.Verify(x => x.DisplayErrorFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void OnPopulateRelationshipCompletedActionWithoutExceptionAndOnlySystemAttributesWithZeroSystemAttributes()
        {
            Exception exception = null;
            bool cancelled = false;
            bool showSystemAttributes = true;
            var result = new List<System.Windows.Forms.ListViewItem>();
            System.Windows.Forms.ListViewItem item = new System.Windows.Forms.ListViewItem { };
            var attribute = new AttributeMetadata { };
            var isCustomAttributeField = attribute.GetType().GetRuntimeFields().First(a => a.Name == "_isCustomAttribute");
            isCustomAttributeField.SetValue(attribute, (bool?)false);
            item.Tag = attribute;
            result.Add(item);

            var eventArgs = new System.ComponentModel.RunWorkerCompletedEventArgs(result, exception, cancelled);

            NotificationServiceMock.Setup(x => x.DisplayErrorFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()))
                                .Verifiable();
            using (var listView = new System.Windows.Forms.ListView())
            {
                FluentActions.Invoking(() => listView.OnPopulateCompletedAction(eventArgs, NotificationServiceMock.Object, null,   showSystemAttributes))
                             .Should()
                             .NotThrow();

                listView.Items.Count.Should().Be(0);
            }

            NotificationServiceMock.Verify(x => x.DisplayErrorFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void OnPopulateRelationshipCompletedActionWithoutExceptionAndAllAttributesWithTwoItems()
        {
            Exception exception = null;
            bool cancelled = false;
            bool showSystemAttributes = false;
            var result = new List<System.Windows.Forms.ListViewItem>();
            System.Windows.Forms.ListViewItem nonCustomItem = new System.Windows.Forms.ListViewItem { };
            System.Windows.Forms.ListViewItem customTtem = new System.Windows.Forms.ListViewItem { };
            var nonCustomAttribute = new AttributeMetadata();
            var customAttribute = new AttributeMetadata();

            var isCustomAttributeField = nonCustomAttribute.GetType().GetRuntimeFields().First(a => a.Name == "_isCustomAttribute");
            isCustomAttributeField.SetValue(customAttribute, (bool?)false);

            var secondIsCustomAttributeField = nonCustomAttribute.GetType().GetRuntimeFields().First(a => a.Name == "_isCustomAttribute");
            secondIsCustomAttributeField.SetValue(customAttribute, (bool?)true);

            nonCustomItem.Tag = nonCustomAttribute;
            customTtem.Tag = customAttribute;
            result.Add(nonCustomItem);
            result.Add(customTtem);

            var eventArgs = new System.ComponentModel.RunWorkerCompletedEventArgs(result, exception, cancelled);

            NotificationServiceMock.Setup(x => x.DisplayErrorFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()))
                                .Verifiable();
            using (var listView = new System.Windows.Forms.ListView())
            {
                FluentActions.Invoking(() => listView.OnPopulateCompletedAction(eventArgs, NotificationServiceMock.Object, null,   showSystemAttributes))
                             .Should()
                             .NotThrow();

                listView.Items.Count.Should().Be(2);
            }

            NotificationServiceMock.Verify(x => x.DisplayErrorFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()), Times.Never);
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

            FluentActions.Invoking(() => item.InvalidUpdate(attribute))
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

            FluentActions.Invoking(() => item.InvalidUpdate(attribute ))
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

            FluentActions.Invoking(() => item.InvalidUpdate(attribute ))
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

            FluentActions.Invoking(() => item.InvalidUpdate(attribute ))
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

            FluentActions.Invoking(() => item.InvalidUpdate(attribute ))
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

            FluentActions.Invoking(() => item.InvalidUpdate(attribute ))
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

            FluentActions.Invoking(() => item.InvalidUpdate(attribute ))
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

            FluentActions.Invoking(() => item.InvalidUpdate(attribute ))
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

            FluentActions.Invoking(() => item.InvalidUpdate(attribute))
                         .Should()
                         .NotThrow();

            item.ToolTipText.Should().Contain("DeprecatedVersion:");
        }

    }
}