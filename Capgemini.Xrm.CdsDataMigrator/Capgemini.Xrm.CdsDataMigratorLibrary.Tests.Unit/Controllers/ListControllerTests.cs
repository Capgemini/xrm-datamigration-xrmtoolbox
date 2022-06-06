using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Capgemini.Xrm.CdsDataMigratorLibrary.Controllers;
using Capgemini.Xrm.CdsDataMigratorLibrary.Core;
using Capgemini.Xrm.CdsDataMigratorLibrary.Exceptions;
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
    public class ListControllerTests : TestBase
    {
        private Dictionary<string, HashSet<string>> inputEntityRelationships;
        private Dictionary<string, HashSet<string>> inputEntityAttributes;

        private ListController systemUnderTest;

        [TestInitialize]
        public void Setup()
        {
            SetupServiceMocks();
            inputEntityRelationships = new Dictionary<string, HashSet<string>>();
            inputEntityAttributes = new Dictionary<string, HashSet<string>>();

            systemUnderTest = new ListController();
        }

        [TestMethod]
        public void SetListViewSortingWithEmptySettings()
        {
            using (var listview = new System.Windows.Forms.ListView())
            {
                int column = 0;
                var inputOrganisationId = Guid.NewGuid().ToString();
                var settings = new Settings();

                FluentActions.Invoking(() => systemUnderTest.SetListViewSorting(listview, column, inputOrganisationId, settings))
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

                FluentActions.Invoking(() => systemUnderTest.SetListViewSorting(listview, column, inputOrganisationId.ToString(), settings))
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

                FluentActions.Invoking(() => systemUnderTest.SetListViewSorting(listview, column, inputOrganisationId.ToString(), settings))
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

                FluentActions.Invoking(() => systemUnderTest.SetListViewSorting(listview, column, inputOrganisationId.ToString(), settings))
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

                FluentActions.Invoking(() => systemUnderTest.SetListViewSorting(listview, column, inputOrganisationId.ToString(), settings))
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

                FluentActions.Invoking(() => systemUnderTest.SetListViewSorting(listview, column, inputOrganisationId.ToString(), settings))
                             .Should()
                             .NotThrow();

                listview.ListViewItemSorter.Should().NotBeNull();
                listview.Sorting.Should().Be(System.Windows.Forms.SortOrder.Ascending);
            }
        }

        [TestMethod]
        public void OpenMappingForm()
        {
            var serviceParameters = GenerateMigratorParameters();

            string entityLogicalName = "contact";
            var inputCachedMetadata = new List<EntityMetadata>();
            var inputLookupMaping = new Dictionary<string, Dictionary<string, List<string>>>();

            FluentActions.Invoking(() => systemUnderTest.OpenMappingForm(serviceParameters, null, inputCachedMetadata, inputLookupMaping, entityLogicalName))
                         .Should()
                         .NotThrow();
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

            FluentActions.Invoking(() => systemUnderTest.UpdateAttributeMetadataCheckBoxes(relationship.IntersectEntityName, item, inputEntityRelationships, "Fake Logical name"))
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

            FluentActions.Invoking(() => systemUnderTest.UpdateAttributeMetadataCheckBoxes(relationship.IntersectEntityName, item, inputEntityRelationships, inputEntityLogicalName))
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

            FluentActions.Invoking(() => systemUnderTest.UpdateAttributeMetadataCheckBoxes(relationship.IntersectEntityName, item, inputEntityRelationships, inputEntityLogicalName))
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

            FluentActions.Invoking(() => systemUnderTest.UpdateAttributeMetadataCheckBoxes(relationship.IntersectEntityName, item, inputEntityRelationships, inputEntityLogicalName))
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

            FluentActions.Invoking(() => systemUnderTest.UpdateAttributeMetadataCheckBoxes(relationship.IntersectEntityName, item, inputEntityRelationships, null))
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

            FluentActions.Invoking(() => systemUnderTest.UpdateAttributeMetadataCheckBoxes(relationship.IntersectEntityName, item, inputEntityRelationships, "Random Text"))
                         .Should()
                         .NotThrow();
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
                FluentActions.Invoking(() => systemUnderTest.OnPopulateCompletedAction(eventArgs, NotificationServiceMock.Object, null, listView, showSystemAttributes))
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
                FluentActions.Invoking(() => systemUnderTest.OnPopulateCompletedAction(eventArgs, NotificationServiceMock.Object, null, listView, showSystemAttributes))
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
                FluentActions.Invoking(() => systemUnderTest.OnPopulateCompletedAction(eventArgs, NotificationServiceMock.Object, null, listView, showSystemAttributes))
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
                FluentActions.Invoking(() => systemUnderTest.OnPopulateCompletedAction(eventArgs, NotificationServiceMock.Object, null, listView, showSystemAttributes))
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
                FluentActions.Invoking(() => systemUnderTest.OnPopulateCompletedAction(eventArgs, NotificationServiceMock.Object, null, listView, showSystemAttributes))
                             .Should()
                             .NotThrow();

                listView.Items.Count.Should().Be(2);
            }

            NotificationServiceMock.Verify(x => x.DisplayErrorFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void HandleMappingControlItemClickNoListViewItemSelected()
        {
            string inputEntityLogicalName = "contact";
            bool listViewItemIsSelected = false;
            var inputMapping = new Dictionary<string, List<Item<EntityReference, EntityReference>>>();
            var inputMapper = new Dictionary<string, Dictionary<Guid, Guid>>();

            NotificationServiceMock.Setup(x => x.DisplayFeedback(It.IsAny<string>()))
                               .Verifiable();

            FluentActions.Invoking(() => systemUnderTest.HandleMappingControlItemClick(NotificationServiceMock.Object, inputEntityLogicalName, listViewItemIsSelected, inputMapping, inputMapper, null))
                         .Should()
                         .NotThrow();

            NotificationServiceMock.Verify(x => x.DisplayFeedback(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void HandleMappingControlItemClickListViewItemSelectedIsTrueAndMappingsDoesNotContainEntityLogicalName()
        {
            string inputEntityLogicalName = "contact";
            bool listViewItemIsSelected = true;
            var inputMapping = new Dictionary<string, List<Item<EntityReference, EntityReference>>>();
            var inputMapper = new Dictionary<string, Dictionary<Guid, Guid>>();

            NotificationServiceMock.Setup(x => x.DisplayFeedback(It.IsAny<string>()))
                               .Verifiable();

            FluentActions.Invoking(() => systemUnderTest.HandleMappingControlItemClick(NotificationServiceMock.Object, inputEntityLogicalName, listViewItemIsSelected, inputMapping, inputMapper, null))
                         .Should()
                         .NotThrow();

            NotificationServiceMock.Verify(x => x.DisplayFeedback(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void HandleMappingControlItemClickListViewItemSelectedIsTrueAndFilterContainsEntityLogicalName()
        {
            string inputEntityLogicalName = "contact";
            bool listViewItemIsSelected = true;

            var entityReference = new EntityReference(inputEntityLogicalName, Guid.NewGuid());

            var mappingItem = new List<Item<EntityReference, EntityReference>>
            {
                new Item<EntityReference, EntityReference>(entityReference, entityReference)
            };

            var inputMapping = new Dictionary<string, List<Item<EntityReference, EntityReference>>>
            {
                { inputEntityLogicalName, mappingItem }
            };

            var inputMapper = new Dictionary<string, Dictionary<Guid, Guid>>();

            NotificationServiceMock.Setup(x => x.DisplayFeedback(It.IsAny<string>()))
                               .Verifiable();

            FluentActions.Invoking(() => systemUnderTest.HandleMappingControlItemClick(NotificationServiceMock.Object, inputEntityLogicalName, listViewItemIsSelected, inputMapping, inputMapper, null))
                         .Should()
                         .NotThrow();
            NotificationServiceMock.Verify(x => x.DisplayFeedback(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void ProcessFilterQueryNoListViewItemSelected()
        {
            string inputEntityLogicalName = "contact";
            bool listViewItemIsSelected = false;
            Dictionary<string, string> inputFilterQuery = new Dictionary<string, string>();

            NotificationServiceMock.Setup(x => x.DisplayFeedback(It.IsAny<string>()))
                               .Verifiable();

            using (var filterDialog = new FilterEditor(null, System.Windows.Forms.FormStartPosition.CenterParent))
            {
                FluentActions.Invoking(() => systemUnderTest.ProcessFilterQuery(NotificationServiceMock.Object, null, inputEntityLogicalName, listViewItemIsSelected, inputFilterQuery, filterDialog))
                         .Should()
                         .NotThrow();
            }

            NotificationServiceMock.Verify(x => x.DisplayFeedback(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void ProcessFilterQueryListViewItemSelectedIsTrueAndFilterDoesNotContainEntityLogicalName()
        {
            string inputEntityLogicalName = "contact";
            bool listViewItemIsSelected = true;
            Dictionary<string, string> inputFilterQuery = new Dictionary<string, string>();

            NotificationServiceMock.Setup(x => x.DisplayFeedback(It.IsAny<string>()))
                               .Verifiable();

            using (var filterDialog = new FilterEditor(null, System.Windows.Forms.FormStartPosition.CenterParent))
            {
                filterDialog.QueryString = "< filter type =\"and\" > < condition attribute =\"sw_appointmentstatus\" operator=\"eq\" value=\"266880017\" /></ filter >";

                FluentActions.Invoking(() => systemUnderTest.ProcessFilterQuery(NotificationServiceMock.Object, null, inputEntityLogicalName, listViewItemIsSelected, inputFilterQuery, filterDialog))
                         .Should()
                         .NotThrow();
            }

            NotificationServiceMock.Verify(x => x.DisplayFeedback(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void ProcessFilterQueryListViewItemSelectedIsTrueAndFilterContainEntityLogicalName()
        {
            string inputEntityLogicalName = "contact";
            bool listViewItemIsSelected = true;
            var inputFilterQuery = new Dictionary<string, string>
            {
                { inputEntityLogicalName, inputEntityLogicalName }
            };

            NotificationServiceMock.Setup(x => x.DisplayFeedback(It.IsAny<string>()))
                               .Verifiable();

            var currentfilter = inputFilterQuery[inputEntityLogicalName];

            using (var filterDialog = new FilterEditor(currentfilter, System.Windows.Forms.FormStartPosition.CenterParent))
            {
                filterDialog.QueryString = "< filter type =\"and\" > < condition attribute =\"sw_appointmentstatus\" operator=\"eq\" value=\"266880017\" /></ filter >";

                FluentActions.Invoking(() => systemUnderTest.ProcessFilterQuery(NotificationServiceMock.Object, null, inputEntityLogicalName, listViewItemIsSelected, inputFilterQuery, filterDialog))
                         .Should()
                         .NotThrow();
            }

            NotificationServiceMock.Verify(x => x.DisplayFeedback(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void ProcessFilterQueryListViewFilterDialogQueryStringIsEmpty()
        {
            string inputEntityLogicalName = "contact";
            bool listViewItemIsSelected = true;
            Dictionary<string, string> inputFilterQuery = new Dictionary<string, string>
            {
                { inputEntityLogicalName, inputEntityLogicalName }
            };

            NotificationServiceMock.Setup(x => x.DisplayFeedback(It.IsAny<string>()))
                               .Verifiable();

            var currentfilter = inputFilterQuery[inputEntityLogicalName];

            using (var filterDialog = new FilterEditor(currentfilter, System.Windows.Forms.FormStartPosition.CenterParent))
            {
                filterDialog.QueryString = string.Empty;

                FluentActions.Invoking(() => systemUnderTest.ProcessFilterQuery(NotificationServiceMock.Object, null, inputEntityLogicalName, listViewItemIsSelected, inputFilterQuery, filterDialog))
                         .Should()
                         .NotThrow();
            }

            NotificationServiceMock.Verify(x => x.DisplayFeedback(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void StoreCrmEntityData()
        {
            var entityLogicalName = "contact";
            var attributeSet = new HashSet<string>() { "contactId", "firstname", "lastname" };
            inputEntityAttributes.Add(entityLogicalName, attributeSet);
            var inputAttributeMapping = new AttributeTypeMapping();

            var entityMetadata = InstantiateEntityMetaData(entityLogicalName);
            InsertAttributeList(entityMetadata, new List<string> { "contactId", "firstname", "lastname" });

            var crmEntity = new CrmEntity();
            var crmEntityList = new List<CrmEntity>();

            FluentActions.Invoking(() => systemUnderTest.StoreCrmEntityData(crmEntity, entityMetadata, crmEntityList, inputEntityRelationships, inputEntityAttributes, inputAttributeMapping, NotificationServiceMock.Object))
                                 .Should()
                                 .NotThrow();

            crmEntityList.Count.Should().Be(1);
        }
    }
}