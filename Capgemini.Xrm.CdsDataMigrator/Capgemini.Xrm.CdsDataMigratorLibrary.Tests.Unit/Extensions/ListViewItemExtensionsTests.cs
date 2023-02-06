using Capgemini.Xrm.CdsDataMigrator.Tests.Unit;
using Capgemini.Xrm.CdsDataMigratorLibrary.Core;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk.Metadata;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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
        public void SetListViewSortingWithEmptySettings()
        {
            using (var listview = new System.Windows.Forms.ListView())
            {
                int column = 0;
                var inputOrganisationId = Guid.NewGuid().ToString();
                var settings = new Settings();

                FluentActions.Invoking(() => listview.SetListViewSorting(column, inputOrganisationId, settings))
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

                FluentActions.Invoking(() => listview.SetListViewSorting(column, inputOrganisationId.ToString(), settings))
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

                FluentActions.Invoking(() => listview.SetListViewSorting(column, inputOrganisationId.ToString(), settings))
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

                FluentActions.Invoking(() => listview.SetListViewSorting(column, inputOrganisationId.ToString(), settings))
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

                FluentActions.Invoking(() => listview.SetListViewSorting(column, inputOrganisationId.ToString(), settings))
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

                FluentActions.Invoking(() => listview.SetListViewSorting(column, inputOrganisationId.ToString(), settings))
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
                FluentActions.Invoking(() => listView.OnPopulateCompletedAction(eventArgs, NotificationServiceMock.Object, null, showSystemAttributes))
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
            isCustomAttributeField.SetValue(attribute, true);
            item.Tag = attribute;
            result.Add(item);

            var eventArgs = new System.ComponentModel.RunWorkerCompletedEventArgs(result, exception, cancelled);

            NotificationServiceMock.Setup(x => x.DisplayErrorFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()))
                                .Verifiable();
            using (var listView = new System.Windows.Forms.ListView())
            {
                FluentActions.Invoking(() => listView.OnPopulateCompletedAction(eventArgs, NotificationServiceMock.Object, null, showSystemAttributes))
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
            isCustomAttributeField.SetValue(attribute, false);
            item.Tag = attribute;
            result.Add(item);

            var eventArgs = new System.ComponentModel.RunWorkerCompletedEventArgs(result, exception, cancelled);

            NotificationServiceMock.Setup(x => x.DisplayErrorFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()))
                                .Verifiable();
            using (var listView = new System.Windows.Forms.ListView())
            {
                FluentActions.Invoking(() => listView.OnPopulateCompletedAction(eventArgs, NotificationServiceMock.Object, null, showSystemAttributes))
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
            isCustomAttributeField.SetValue(customAttribute, false);

            var secondIsCustomAttributeField = nonCustomAttribute.GetType().GetRuntimeFields().First(a => a.Name == "_isCustomAttribute");
            secondIsCustomAttributeField.SetValue(customAttribute, true);

            nonCustomItem.Tag = nonCustomAttribute;
            customTtem.Tag = customAttribute;
            result.Add(nonCustomItem);
            result.Add(customTtem);

            var eventArgs = new System.ComponentModel.RunWorkerCompletedEventArgs(result, exception, cancelled);

            NotificationServiceMock.Setup(x => x.DisplayErrorFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()))
                                .Verifiable();
            using (var listView = new System.Windows.Forms.ListView())
            {
                FluentActions.Invoking(() => listView.OnPopulateCompletedAction(eventArgs, NotificationServiceMock.Object, null, showSystemAttributes))
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
                IsValidForCreate = true
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

            FluentActions.Invoking(() => item.InvalidUpdate(attribute))
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

            FluentActions.Invoking(() => item.InvalidUpdate(attribute))
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
            isLogicalEntityField.SetValue(attribute, true);

            var item = new System.Windows.Forms.ListViewItem("Item1");

            FluentActions.Invoking(() => item.InvalidUpdate(attribute))
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

            FluentActions.Invoking(() => item.InvalidUpdate(attribute))
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
                IsValidForCreate = false,
                IsValidForUpdate = false
            };

            var item = new System.Windows.Forms.ListViewItem("Item1");

            FluentActions.Invoking(() => item.InvalidUpdate(attribute))
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
                IsValidForCreate = true,
                IsValidForUpdate = true
            };

            var item = new System.Windows.Forms.ListViewItem("Item1");

            FluentActions.Invoking(() => item.InvalidUpdate(attribute))
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

            FluentActions.Invoking(() => item.InvalidUpdate(attribute))
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

        [TestMethod]
        public void UpdateAttributeMetadataCheckBoxesNonExitingsFilterValue()
        {
            string inputEntityLogicalName = "account_contact";

            var relationship = new ManyToManyRelationshipMetadata
            {
                Entity1LogicalName = "account",
                Entity1IntersectAttribute = "accountid",
                IntersectEntityName = "account_contact",
                Entity2LogicalName = "contact",
                Entity2IntersectAttribute = "contactid"
            };

            var entityRelationshipSet = new HashSet<string>() { inputEntityLogicalName };

            inputEntityRelationships.Add(inputEntityLogicalName, entityRelationshipSet);

            var item = new System.Windows.Forms.ListViewItem("Item1");

            FluentActions.Invoking(() => item.UpdateAttributeMetadataCheckBoxes(relationship.IntersectEntityName, inputEntityRelationships, "Fake Logical name"))
                         .Should()
                         .NotThrow();

            item.Checked.Should().BeFalse();
        }

        [TestMethod]
        public void UpdateAttributeMetadataCheckBoxesValueDoesNotExistInEntityRelationships()
        {
            string inputEntityLogicalName = "account_contact";

            var relationship = new ManyToManyRelationshipMetadata
            {
                Entity1LogicalName = "account",
                Entity1IntersectAttribute = "accountid",
                IntersectEntityName = "account_contact",
                Entity2LogicalName = "contact",
                Entity2IntersectAttribute = "contactid"
            };

            var entityRelationshipSet = new HashSet<string>() { };

            inputEntityRelationships.Add(inputEntityLogicalName, entityRelationshipSet);

            var item = new System.Windows.Forms.ListViewItem("Item1");

            FluentActions.Invoking(() => item.UpdateAttributeMetadataCheckBoxes(relationship.IntersectEntityName, inputEntityRelationships, inputEntityLogicalName))
                         .Should()
                         .NotThrow();

            item.Checked.Should().BeFalse();
        }

        [TestMethod]
        public void UpdateAttributeMetadataCheckBoxesIntersectEntityNameDoesNotExist()
        {
            string inputEntityLogicalName = "account_contact";

            var relationship = new ManyToManyRelationshipMetadata
            {
                Entity1LogicalName = "account",
                Entity1IntersectAttribute = "accountid",
                IntersectEntityName = "account_contact2",
                Entity2LogicalName = "contact",
                Entity2IntersectAttribute = "contactid"
            };

            var entityRelationshipSet = new HashSet<string>() { inputEntityLogicalName };

            inputEntityRelationships.Add(inputEntityLogicalName, entityRelationshipSet);

            var item = new System.Windows.Forms.ListViewItem("Item1");

            FluentActions.Invoking(() => item.UpdateAttributeMetadataCheckBoxes(relationship.IntersectEntityName, inputEntityRelationships, inputEntityLogicalName))
                         .Should()
                         .NotThrow();

            item.Checked.Should().BeFalse();
        }

        [TestMethod]
        public void UpdateAttributeMetadataCheckBoxesIntersectEntityNameExist()
        {
            string inputEntityLogicalName = "account_contact";

            var relationship = new ManyToManyRelationshipMetadata
            {
                Entity1LogicalName = "account",
                Entity1IntersectAttribute = "accountid",
                IntersectEntityName = "account_contact",
                Entity2LogicalName = "contact",
                Entity2IntersectAttribute = "contactid"
            };

            var entityRelationshipSet = new HashSet<string>() { inputEntityLogicalName };

            inputEntityRelationships.Add(inputEntityLogicalName, entityRelationshipSet);

            var item = new System.Windows.Forms.ListViewItem("Item1");

            FluentActions.Invoking(() => item.UpdateAttributeMetadataCheckBoxes(relationship.IntersectEntityName, inputEntityRelationships, inputEntityLogicalName))
                         .Should()
                         .NotThrow();

            item.Checked.Should().BeTrue();
        }

        [TestMethod]
        public void UpdateCheckBoxesRelationshipNullEntityLogicalName()
        {
            string inputEntityLogicalName = "account";

            var relationship = new ManyToManyRelationshipMetadata
            {
                Entity1LogicalName = "account",
                Entity1IntersectAttribute = "accountid",
                IntersectEntityName = "account_contact",
                Entity2LogicalName = "contact",
                Entity2IntersectAttribute = "contactid"
            };

            var entityRelationshipSet = new HashSet<string>() { inputEntityLogicalName };

            inputEntityRelationships.Add(inputEntityLogicalName, entityRelationshipSet);

            var item = new System.Windows.Forms.ListViewItem("Item1");

            FluentActions.Invoking(() => item.UpdateAttributeMetadataCheckBoxes(relationship.IntersectEntityName, inputEntityRelationships, null))
                         .Should()
                         .Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void UpdateCheckBoxesRelationshipNonExistingEntityLogicalName()
        {
            string inputEntityLogicalName = "account";

            var relationship = new ManyToManyRelationshipMetadata
            {
                Entity1LogicalName = "account",
                Entity1IntersectAttribute = "accountid",
                IntersectEntityName = "account_contact",
                Entity2LogicalName = "contact",
                Entity2IntersectAttribute = "contactid"
            };

            var entityRelationshipSet = new HashSet<string>() { inputEntityLogicalName };

            inputEntityRelationships.Add(inputEntityLogicalName, entityRelationshipSet);

            var item = new System.Windows.Forms.ListViewItem("Item1");

            FluentActions.Invoking(() => item.UpdateAttributeMetadataCheckBoxes(relationship.IntersectEntityName, inputEntityRelationships, "Random Text"))
                         .Should()
                         .NotThrow();
        }
    }
}