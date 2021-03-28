using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Capgemini.Xrm.DataMigration.XrmToolBox.Core;
using Capgemini.Xrm.DataMigration.XrmToolBox.Services;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Forms;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Moq;

namespace Capgemini.Xrm.CdsDataMigrator.Tests.Unit.UserControls
{
    [TestClass]
    public class SchemaWizardDelegateTests
    {
        private Mock<IOrganizationService> serviceMock;
        private Mock<IMetadataService> metadataServiceMock;
        private Mock<IFeedbackManager> feedbackManagerMock;
        private Mock<IDataMigratorExceptionHelper> dataMigratorExceptionHelperMock;
        private Dictionary<string, HashSet<string>> inputEntityRelationships;

        private SchemaWizardDelegate systemUnderTest;

        [TestInitialize]
        public void Setup()
        {
            serviceMock = new Mock<IOrganizationService>();
            metadataServiceMock = new Mock<IMetadataService>();
            feedbackManagerMock = new Mock<IFeedbackManager>();
            dataMigratorExceptionHelperMock = new Mock<IDataMigratorExceptionHelper>();

            inputEntityRelationships = new Dictionary<string, HashSet<string>>();

            systemUnderTest = new SchemaWizardDelegate();
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
            var inputEntityAttributes = new Dictionary<string, HashSet<string>>();

            var actual = systemUnderTest.RetrieveSourceEntitiesList(showSystemAttributes, serviceMock.Object, metadataServiceMock.Object, inputCachedMetadata, inputEntityAttributes);

            actual.Count.Should().Be(1);
        }

        [TestMethod]
        public void RetrieveSourceEntitiesListShowSystemAttributesIsTrue()
        {
            var showSystemAttributes = true;
            string entityLogicalName = "account_contact";
            SetupMockObjects(entityLogicalName);
            var inputCachedMetadata = new List<EntityMetadata>();
            var inputEntityAttributes = new Dictionary<string, HashSet<string>>();

            var actual = systemUnderTest.RetrieveSourceEntitiesList(showSystemAttributes, serviceMock.Object, metadataServiceMock.Object, inputCachedMetadata, inputEntityAttributes);

            actual.Count.Should().Be(1);
        }

        [TestMethod]
        public void PopulateRelationshipActionNoManyToManyRelationships()
        {
            string entityLogicalName = "contact";
            var entityMetadata = new EntityMetadata();

            metadataServiceMock.Setup(x => x.RetrieveEntities(It.IsAny<string>(), It.IsAny<IOrganizationService>(), It.IsAny<IDataMigratorExceptionHelper>()))
                .Returns(entityMetadata)
                .Verifiable();

            var actual = systemUnderTest.PopulateRelationshipAction(entityLogicalName, serviceMock.Object, metadataServiceMock.Object, inputEntityRelationships, dataMigratorExceptionHelperMock.Object);

            actual.Count.Should().Be(0);

            serviceMock.VerifyAll();
            metadataServiceMock.VerifyAll();
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

            metadataServiceMock.Setup(x => x.RetrieveEntities(It.IsAny<string>(), It.IsAny<IOrganizationService>(), It.IsAny<IDataMigratorExceptionHelper>()))
                .Returns(entityMetadata)
                .Verifiable();

            using (var listView = new System.Windows.Forms.ListView())
            {
                systemUnderTest.PopulateEntitiesListView(items, null, null, listView, feedbackManagerMock.Object);

                var actual = systemUnderTest.PopulateRelationshipAction(entityLogicalName, serviceMock.Object, metadataServiceMock.Object, inputEntityRelationships, dataMigratorExceptionHelperMock.Object);

                actual.Count.Should().BeGreaterThan(0);
            }

            serviceMock.VerifyAll();
            metadataServiceMock.VerifyAll();
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

            feedbackManagerMock.Setup(x => x.DisplayErrorFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()))
                                .Verifiable();
            feedbackManagerMock.Setup(x => x.DisplayWarningFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()))
                               .Verifiable();
            using (var listView = new System.Windows.Forms.ListView())
            {
                FluentActions.Invoking(() => systemUnderTest.PopulateEntitiesListView(items, exception, null, listView, feedbackManagerMock.Object))
                        .Should()
                        .NotThrow();
            }

            feedbackManagerMock.Verify(x => x.DisplayErrorFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()), Times.Once);
            feedbackManagerMock.Verify(x => x.DisplayWarningFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void PopulateEntitiesListViewWhenThereIsNoException()
        {
            var items = new List<System.Windows.Forms.ListViewItem>();
            Exception exception = null;

            feedbackManagerMock.Setup(x => x.DisplayWarningFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()))
                                .Verifiable();
            using (var listView = new System.Windows.Forms.ListView())
            {
                FluentActions.Invoking(() => systemUnderTest.PopulateEntitiesListView(items, exception, null, listView, feedbackManagerMock.Object))
                        .Should()
                        .NotThrow();
            }

            feedbackManagerMock.Verify(x => x.DisplayErrorFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()), Times.Never);
            feedbackManagerMock.Verify(x => x.DisplayWarningFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()), Times.Once);
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

            feedbackManagerMock.Setup(x => x.DisplayErrorFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()))
                                .Verifiable();

            using (var listView = new System.Windows.Forms.ListView())
            {
                FluentActions.Invoking(() => systemUnderTest.PopulateEntitiesListView(items, exception, null, listView, feedbackManagerMock.Object))
                        .Should()
                        .NotThrow();
            }

            feedbackManagerMock.Verify(x => x.DisplayErrorFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()), Times.Never);
            feedbackManagerMock.Verify(x => x.DisplayWarningFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void LoadExportConfigFileWithEmptyExportConfigPath()
        {
            string exportConfigFilename = string.Empty;
            var inputFilterQuery = new Dictionary<string, string>();
            var inputLookupMaping = new Dictionary<string, Dictionary<string, List<string>>>();

            feedbackManagerMock.Setup(x => x.DisplayFeedback(It.IsAny<string>()))
                                .Verifiable();

            using (System.Windows.Forms.TextBox exportConfigTextBox = new System.Windows.Forms.TextBox())
            {
                exportConfigTextBox.Text = exportConfigFilename;

                FluentActions.Invoking(() => systemUnderTest.LoadExportConfigFile(feedbackManagerMock.Object, exportConfigTextBox, inputFilterQuery, inputLookupMaping))
                             .Should()
                             .NotThrow();
            }

            feedbackManagerMock.Verify(x => x.DisplayFeedback(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void LoadExportConfigFileWithInValidExportConfigPath()
        {
            string exportConfigFilename = "hello.txt";
            var inputFilterQuery = new Dictionary<string, string>();
            var inputLookupMaping = new Dictionary<string, Dictionary<string, List<string>>>();

            feedbackManagerMock.Setup(x => x.DisplayFeedback("Invalid Export Config File"))
                                .Verifiable();

            using (System.Windows.Forms.TextBox exportConfigTextBox = new System.Windows.Forms.TextBox())
            {
                exportConfigTextBox.Text = exportConfigFilename;

                FluentActions.Invoking(() => systemUnderTest.LoadExportConfigFile(feedbackManagerMock.Object, exportConfigTextBox, inputFilterQuery, inputLookupMaping))
                             .Should()
                             .NotThrow();
            }

            feedbackManagerMock.Verify(x => x.DisplayFeedback("Invalid Export Config File"), Times.Once);
        }

        [TestMethod]
        public void LoadExportConfigFileWithValidExportConfigPath()
        {
            string exportConfigFilename = "TestData\\ExportConfig.json";
            var inputFilterQuery = new Dictionary<string, string>();
            var inputLookupMaping = new Dictionary<string, Dictionary<string, List<string>>>();

            feedbackManagerMock.Setup(x => x.DisplayFeedback("Filters and Lookup Mappings loaded from Export Config File"))
                                .Verifiable();

            using (System.Windows.Forms.TextBox exportConfigTextBox = new System.Windows.Forms.TextBox())
            {
                exportConfigTextBox.Text = exportConfigFilename;

                FluentActions.Invoking(() => systemUnderTest.LoadExportConfigFile(feedbackManagerMock.Object, exportConfigTextBox, inputFilterQuery, inputLookupMaping))
                             .Should()
                             .NotThrow();
            }

            feedbackManagerMock.Verify(x => x.DisplayFeedback("Filters and Lookup Mappings loaded from Export Config File"), Times.Once);
        }

        [TestMethod]
        public void LoadExportConfigFileThrowsException()
        {
            string exportConfigFilename = "TestData\\ExportConfig.json";
            var exception = new Exception("TestException here!");
            var inputFilterQuery = new Dictionary<string, string>();
            var inputLookupMaping = new Dictionary<string, Dictionary<string, List<string>>>();

            feedbackManagerMock.Setup(x => x.DisplayFeedback("Filters and Lookup Mappings loaded from Export Config File"))
                               .Throws(exception);

            feedbackManagerMock.Setup(x => x.DisplayFeedback($"Load Correct Export Config file, error:{exception.Message}"))
                                .Verifiable();

            using (System.Windows.Forms.TextBox exportConfigTextBox = new System.Windows.Forms.TextBox())
            {
                exportConfigTextBox.Text = exportConfigFilename;

                FluentActions.Invoking(() => systemUnderTest.LoadExportConfigFile(feedbackManagerMock.Object, exportConfigTextBox, inputFilterQuery, inputLookupMaping))
                             .Should()
                             .NotThrow();
            }

            feedbackManagerMock.Verify(x => x.DisplayFeedback($"Load Correct Export Config file, error:{exception.Message}"), Times.Once);
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
        public void ProcessAllAttributeMetadata()
        {
            string entityLogicalName = "account_contact";
            List<string> unmarkedattributes = new List<string>();

            var attributeList = new List<AttributeMetadata>()
            {
                new AttributeMetadata
                {
                    LogicalName = "contactattnoentity1",
                    DisplayName = new Label
                    {
                        UserLocalizedLabel = new LocalizedLabel { Label = "Test" }
                    }
                }
            };

            Dictionary<string, HashSet<string>> inputEntityAttributes = new Dictionary<string, HashSet<string>>();

            var actual = systemUnderTest.ProcessAllAttributeMetadata(unmarkedattributes, attributeList.ToArray(), entityLogicalName, inputEntityAttributes);

            actual.Count.Should().BeGreaterThan(0);
        }

        [TestMethod]
        public void OnPopulateRelationshipCompletedActionWithException()
        {
            Exception exception = new Exception();
            bool cancelled = false;
            var result = new List<System.Windows.Forms.ListViewItem>();

            var eventArgs = new System.ComponentModel.RunWorkerCompletedEventArgs(result, exception, cancelled);

            feedbackManagerMock.Setup(x => x.DisplayErrorFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()))
                                .Verifiable();
            using (var listView = new System.Windows.Forms.ListView())
            {
                FluentActions.Invoking(() => systemUnderTest.OnPopulateCompletedAction(eventArgs, feedbackManagerMock.Object, null, listView))
             .Should()
             .NotThrow();
            }

            feedbackManagerMock.Verify(x => x.DisplayErrorFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void OnPopulateRelationshipCompletedActionWithoutException()
        {
            Exception exception = null;
            bool cancelled = false;
            var result = new List<System.Windows.Forms.ListViewItem>();

            var eventArgs = new System.ComponentModel.RunWorkerCompletedEventArgs(result, exception, cancelled);

            feedbackManagerMock.Setup(x => x.DisplayErrorFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()))
                                .Verifiable();
            using (var listView = new System.Windows.Forms.ListView())
            {
                FluentActions.Invoking(() => systemUnderTest.OnPopulateCompletedAction(eventArgs, feedbackManagerMock.Object, null, listView))
                             .Should()
                             .NotThrow();
            }

            feedbackManagerMock.Verify(x => x.DisplayErrorFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void FilterAttributes()
        {
            var entityMetadata = new EntityMetadata();
            InsertAttributeList(entityMetadata, new List<string> { "contactattnoentity1" });
            bool showSystemAttributes = true;

            var actual = systemUnderTest.FilterAttributes(entityMetadata, showSystemAttributes);

            actual.Length.Should().Be(entityMetadata.Attributes.Length);
        }

        [TestMethod]
        public void FilterAttributesHideSystemAttributes()
        {
            var entityMetadata = new EntityMetadata();
            InsertAttributeList(entityMetadata, new List<string> { "contactattnoentity1" });
            bool showSystemAttributes = false;

            var actual = systemUnderTest.FilterAttributes(entityMetadata, showSystemAttributes);

            actual.Length.Should().Be(0);
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

            FluentActions.Invoking(() => systemUnderTest.InvalidUpdate(attribute, item))
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

            FluentActions.Invoking(() => systemUnderTest.InvalidUpdate(attribute, item))
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

            FluentActions.Invoking(() => systemUnderTest.InvalidUpdate(attribute, item))
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

            FluentActions.Invoking(() => systemUnderTest.InvalidUpdate(attribute, item))
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

            FluentActions.Invoking(() => systemUnderTest.InvalidUpdate(attribute, item))
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

            FluentActions.Invoking(() => systemUnderTest.InvalidUpdate(attribute, item))
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

            FluentActions.Invoking(() => systemUnderTest.InvalidUpdate(attribute, item))
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

            FluentActions.Invoking(() => systemUnderTest.InvalidUpdate(attribute, item))
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

            FluentActions.Invoking(() => systemUnderTest.InvalidUpdate(attribute, item))
                         .Should()
                         .NotThrow();

            item.ToolTipText.Should().Contain("DeprecatedVersion:");
        }

        [TestMethod]
        public void GetAttributeList()
        {
            string entityLogicalName = "contact";

            AttributeMetadata[] attributes = null;
            var entityMetadata = new EntityMetadata();
            bool showSystemAttributes = true;

            metadataServiceMock.Setup(x => x.RetrieveEntities(It.IsAny<string>(), It.IsAny<IOrganizationService>(), It.IsAny<IDataMigratorExceptionHelper>()))
                                .Returns(entityMetadata)
                                .Verifiable();

            attributes = systemUnderTest.GetAttributeList(entityLogicalName, serviceMock.Object, metadataServiceMock.Object, showSystemAttributes, dataMigratorExceptionHelperMock.Object);

            attributes.Should().BeNull();
        }

        [TestMethod]
        public void GetAttributeListMetaDataServiceReturnsEnities()
        {
            string entityLogicalName = "contact";

            AttributeMetadata[] attributes = null;
            bool showSystemAttributes = true;

            var entityMetadata = new EntityMetadata();
            InsertAttributeList(entityMetadata, new List<string> { "contactattnoentity1" });

            metadataServiceMock.Setup(x => x.RetrieveEntities(It.IsAny<string>(), It.IsAny<IOrganizationService>(), It.IsAny<IDataMigratorExceptionHelper>()))
                                .Returns(entityMetadata)
                                .Verifiable();

            attributes = systemUnderTest.GetAttributeList(entityLogicalName, serviceMock.Object, metadataServiceMock.Object, showSystemAttributes, dataMigratorExceptionHelperMock.Object);

            attributes.Should().NotBeNull();
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

        [TestMethod]
        public void HandleMappingControlItemClickNoListViewItemSelected()
        {
            string inputEntityLogicalName = "contact";
            bool listViewItemIsSelected = false;
            var inputMapping = new Dictionary<string, List<Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core.Item<EntityReference, EntityReference>>>();
            var inputMapper = new Dictionary<string, Dictionary<Guid, Guid>>();

            feedbackManagerMock.Setup(x => x.DisplayFeedback(It.IsAny<string>()))
                               .Verifiable();

            FluentActions.Invoking(() => systemUnderTest.HandleMappingControlItemClick(feedbackManagerMock.Object, inputEntityLogicalName, listViewItemIsSelected, inputMapping, inputMapper, null))
                         .Should()
                         .NotThrow();

            feedbackManagerMock.Verify(x => x.DisplayFeedback(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void HandleMappingControlItemClickListViewItemSelectedIsTrueAndMappingsDoesNotContainEntityLogicalName()
        {
            string inputEntityLogicalName = "contact";
            bool listViewItemIsSelected = true;
            var inputMapping = new Dictionary<string, List<Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core.Item<EntityReference, EntityReference>>>();
            var inputMapper = new Dictionary<string, Dictionary<Guid, Guid>>();

            feedbackManagerMock.Setup(x => x.DisplayFeedback(It.IsAny<string>()))
                               .Verifiable();

            FluentActions.Invoking(() => systemUnderTest.HandleMappingControlItemClick(feedbackManagerMock.Object, inputEntityLogicalName, listViewItemIsSelected, inputMapping, inputMapper, null))
                         .Should()
                         .NotThrow();

            feedbackManagerMock.Verify(x => x.DisplayFeedback(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void HandleMappingControlItemClickListViewItemSelectedIsTrueAndFilterContainsEntityLogicalName()
        {
            string inputEntityLogicalName = "contact";
            bool listViewItemIsSelected = true;

            var entityReference = new EntityReference(inputEntityLogicalName, Guid.NewGuid());

            var mappingItem = new List<DataMigration.XrmToolBoxPlugin.Core.Item<EntityReference, EntityReference>>
            {
                new DataMigration.XrmToolBoxPlugin.Core.Item<EntityReference, EntityReference>(entityReference, entityReference)
            };

            var inputMapping = new Dictionary<string, List<Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core.Item<EntityReference, EntityReference>>>
            {
                { inputEntityLogicalName, mappingItem }
            };

            var inputMapper = new Dictionary<string, Dictionary<Guid, Guid>>();

            feedbackManagerMock.Setup(x => x.DisplayFeedback(It.IsAny<string>()))
                               .Verifiable();

            FluentActions.Invoking(() => systemUnderTest.HandleMappingControlItemClick(feedbackManagerMock.Object, inputEntityLogicalName, listViewItemIsSelected, inputMapping, inputMapper, null))
                         .Should()
                         .NotThrow();
            feedbackManagerMock.Verify(x => x.DisplayFeedback(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void ProcessFilterQueryNoListViewItemSelected()
        {
            string inputEntityLogicalName = "contact";
            bool listViewItemIsSelected = false;
            Dictionary<string, string> inputFilterQuery = new Dictionary<string, string>();

            feedbackManagerMock.Setup(x => x.DisplayFeedback(It.IsAny<string>()))
                               .Verifiable();

            using (var filterDialog = new FilterEditor(null, System.Windows.Forms.FormStartPosition.CenterParent))
            {
                FluentActions.Invoking(() => systemUnderTest.ProcessFilterQuery(feedbackManagerMock.Object, null, inputEntityLogicalName, listViewItemIsSelected, inputFilterQuery, filterDialog))
                         .Should()
                         .NotThrow();
            }

            feedbackManagerMock.Verify(x => x.DisplayFeedback(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void ProcessFilterQueryListViewItemSelectedIsTrueAndFilterDoesNotContainEntityLogicalName()
        {
            string inputEntityLogicalName = "contact";
            bool listViewItemIsSelected = true;
            Dictionary<string, string> inputFilterQuery = new Dictionary<string, string>();

            feedbackManagerMock.Setup(x => x.DisplayFeedback(It.IsAny<string>()))
                               .Verifiable();

            using (var filterDialog = new FilterEditor(null, System.Windows.Forms.FormStartPosition.CenterParent))
            {
                filterDialog.QueryString = "< filter type =\"and\" > < condition attribute =\"sw_appointmentstatus\" operator=\"eq\" value=\"266880017\" /></ filter >";

                FluentActions.Invoking(() => systemUnderTest.ProcessFilterQuery(feedbackManagerMock.Object, null, inputEntityLogicalName, listViewItemIsSelected, inputFilterQuery, filterDialog))
                         .Should()
                         .NotThrow();
            }

            feedbackManagerMock.Verify(x => x.DisplayFeedback(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void ProcessFilterQueryListViewItemSelectedIsTrueAndFilterContainEntityLogicalName()
        {
            string inputEntityLogicalName = "contact";
            bool listViewItemIsSelected = true;
            Dictionary<string, string> inputFilterQuery = new Dictionary<string, string>();

            inputFilterQuery.Add(inputEntityLogicalName, inputEntityLogicalName);

            feedbackManagerMock.Setup(x => x.DisplayFeedback(It.IsAny<string>()))
                               .Verifiable();

            var currentfilter = inputFilterQuery[inputEntityLogicalName];

            using (var filterDialog = new FilterEditor(currentfilter, System.Windows.Forms.FormStartPosition.CenterParent))
            {
                filterDialog.QueryString = "< filter type =\"and\" > < condition attribute =\"sw_appointmentstatus\" operator=\"eq\" value=\"266880017\" /></ filter >";

                FluentActions.Invoking(() => systemUnderTest.ProcessFilterQuery(feedbackManagerMock.Object, null, inputEntityLogicalName, listViewItemIsSelected, inputFilterQuery, filterDialog))
                         .Should()
                         .NotThrow();
            }

            feedbackManagerMock.Verify(x => x.DisplayFeedback(It.IsAny<string>()), Times.Never);
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

            feedbackManagerMock.Setup(x => x.DisplayFeedback(It.IsAny<string>()))
                               .Verifiable();

            var currentfilter = inputFilterQuery[inputEntityLogicalName];

            using (var filterDialog = new FilterEditor(currentfilter, System.Windows.Forms.FormStartPosition.CenterParent))
            {
                filterDialog.QueryString = string.Empty;

                FluentActions.Invoking(() => systemUnderTest.ProcessFilterQuery(feedbackManagerMock.Object, null, inputEntityLogicalName, listViewItemIsSelected, inputFilterQuery, filterDialog))
                         .Should()
                         .NotThrow();
            }

            feedbackManagerMock.Verify(x => x.DisplayFeedback(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void LoadImportConfigFileWithNoImportConfig()
        {
            var inputMapper = new Dictionary<string, Dictionary<Guid, Guid>>();
            var inputMapping = new Dictionary<string, List<Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core.Item<EntityReference, EntityReference>>>();

            feedbackManagerMock.Setup(x => x.DisplayFeedback(It.IsAny<string>()))
                                .Verifiable();

            using (var importConfig = new System.Windows.Forms.TextBox())
            {
                FluentActions.Invoking(() => systemUnderTest.LoadImportConfigFile(feedbackManagerMock.Object, importConfig, inputMapper, inputMapping))
                                 .Should()
                                 .NotThrow();
            }

            feedbackManagerMock.Verify(x => x.DisplayFeedback(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void LoadImportConfigFileWithInvalidImportConfigFilePath()
        {
            var inputMapper = new Dictionary<string, Dictionary<Guid, Guid>>();
            var inputMapping = new Dictionary<string, List<Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core.Item<EntityReference, EntityReference>>>();

            feedbackManagerMock.Setup(x => x.DisplayFeedback("Invalid Import Config File"))
                                .Verifiable();

            using (var importConfig = new System.Windows.Forms.TextBox())
            {
                importConfig.Text = "Hello.txt";

                FluentActions.Invoking(() => systemUnderTest.LoadImportConfigFile(feedbackManagerMock.Object, importConfig, inputMapper, inputMapping))
                                .Should()
                                .NotThrow();
            }

            feedbackManagerMock.Verify(x => x.DisplayFeedback(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void LoadImportConfigFileWithValidImportConfigFilePathButNoMigrationConfig()
        {
            var inputMapper = new Dictionary<string, Dictionary<Guid, Guid>>();
            var inputMapping = new Dictionary<string, List<Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core.Item<EntityReference, EntityReference>>>();

            feedbackManagerMock.Setup(x => x.DisplayFeedback("Invalid Import Config File"))
                                .Verifiable();

            using (var importConfig = new System.Windows.Forms.TextBox())
            {
                importConfig.Text = "TestData\\ImportConfig.json";

                FluentActions.Invoking(() => systemUnderTest.LoadImportConfigFile(feedbackManagerMock.Object, importConfig, inputMapper, inputMapping))
                                 .Should()
                                 .NotThrow();
            }

            feedbackManagerMock.Verify(x => x.DisplayFeedback(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void LoadImportConfigFileWithValidImportConfigFilePathAndMigrationConfig()
        {
            var inputMapper = new Dictionary<string, Dictionary<Guid, Guid>>();
            var inputMapping = new Dictionary<string, List<Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core.Item<EntityReference, EntityReference>>>();

            feedbackManagerMock.Setup(x => x.DisplayFeedback("Guid Id Mappings loaded from Import Config File"))
                                .Verifiable();

            using (var importConfig = new System.Windows.Forms.TextBox())
            {
                importConfig.Text = "TestData/ImportConfig2.json";

                FluentActions.Invoking(() => systemUnderTest.LoadImportConfigFile(feedbackManagerMock.Object, importConfig, inputMapper, inputMapping))
                                 .Should()
                                 .NotThrow();
            }

            feedbackManagerMock.Verify(x => x.DisplayFeedback(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void LoadImportConfigFileHandleException()
        {
            var inputMapper = new Dictionary<string, Dictionary<Guid, Guid>>();
            var inputMapping = new Dictionary<string, List<Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core.Item<EntityReference, EntityReference>>>();

            feedbackManagerMock.Setup(x => x.DisplayFeedback("Guid Id Mappings loaded from Import Config File"))
                .Throws<Exception>();

            using (var importConfig = new System.Windows.Forms.TextBox())
            {
                importConfig.Text = "TestData/ImportConfig2.json";

                FluentActions.Invoking(() => systemUnderTest.LoadImportConfigFile(feedbackManagerMock.Object, importConfig, inputMapper, inputMapping))
                                 .Should()
                                 .NotThrow();
            }

            feedbackManagerMock.Verify(x => x.DisplayFeedback(It.IsAny<string>()), Times.Exactly(2));
        }

        [TestMethod]
        public void AreCrmEntityFieldsSelectedCheckedEntityCountIsZero()
        {
            var inputCheckedEntity = new HashSet<string>();
            var inputEntityAttributes = new Dictionary<string, HashSet<string>>();
            var inputAttributeMapping = new DataMigration.XrmToolBoxPlugin.Core.AttributeTypeMapping();

            var actual = systemUnderTest.AreCrmEntityFieldsSelected(metadataServiceMock.Object, inputCheckedEntity, serviceMock.Object, inputEntityRelationships, inputEntityAttributes, inputAttributeMapping, feedbackManagerMock.Object, dataMigratorExceptionHelperMock.Object);

            actual.Should().BeFalse();
        }

        [TestMethod]
        public void AreCrmEntityFieldsSelected()
        {
            var entityLogicalName = "contact";
            var inputCheckedEntity = new HashSet<string> { entityLogicalName };
            var entityMetadata = InstantiateEntityMetaData(entityLogicalName);
            InsertAttributeList(entityMetadata, new List<string> { "contactId", "firstname", "lastname" });

            var inputEntityAttributes = new Dictionary<string, HashSet<string>>();
            var attributeSet = new HashSet<string>() { "contactId", "firstname", "lastname" };
            inputEntityAttributes.Add(entityLogicalName, attributeSet);
            var inputAttributeMapping = new DataMigration.XrmToolBoxPlugin.Core.AttributeTypeMapping();

            metadataServiceMock.Setup(x => x.RetrieveEntities(It.IsAny<string>(), It.IsAny<IOrganizationService>(), It.IsAny<IDataMigratorExceptionHelper>()))
                                .Returns(entityMetadata)
                                .Verifiable();

            var actual = systemUnderTest.AreCrmEntityFieldsSelected(metadataServiceMock.Object, inputCheckedEntity, serviceMock.Object, inputEntityRelationships, inputEntityAttributes, inputAttributeMapping, feedbackManagerMock.Object, dataMigratorExceptionHelperMock.Object);

            actual.Should().BeTrue();
            metadataServiceMock.VerifyAll();
        }

        [TestMethod]
        public void CollectCrmEntityFieldsCheckedEntityCountIsZero()
        {
            var inputCheckedEntity = new HashSet<string>();
            var inputEntityAttributes = new Dictionary<string, HashSet<string>>();
            var inputAttributeMapping = new DataMigration.XrmToolBoxPlugin.Core.AttributeTypeMapping();
            var schemaConfiguration = new Capgemini.Xrm.DataMigration.Config.CrmSchemaConfiguration();

            FluentActions.Invoking(() => systemUnderTest.CollectCrmEntityFields(metadataServiceMock.Object, serviceMock.Object, inputCheckedEntity, schemaConfiguration, inputEntityRelationships, inputEntityAttributes, inputAttributeMapping, feedbackManagerMock.Object, dataMigratorExceptionHelperMock.Object))
                                .Should()
                                .NotThrow();
        }

        [TestMethod]
        public void CollectCrmEntityFields()
        {
            var entityLogicalName = "contact";
            var inputCheckedEntity = new HashSet<string> { entityLogicalName };
            var inputEntityAttributes = new Dictionary<string, HashSet<string>>();
            var attributeSet = new HashSet<string>() { "contactId", "firstname", "lastname" };
            inputEntityAttributes.Add(entityLogicalName, attributeSet);
            var inputAttributeMapping = new DataMigration.XrmToolBoxPlugin.Core.AttributeTypeMapping();

            var schemaConfiguration = new Capgemini.Xrm.DataMigration.Config.CrmSchemaConfiguration();

            var entityMetadata = InstantiateEntityMetaData(entityLogicalName);
            InsertAttributeList(entityMetadata, new List<string> { "contactId", "firstname", "lastname" });
            metadataServiceMock.Setup(x => x.RetrieveEntities(It.IsAny<string>(), It.IsAny<IOrganizationService>(), It.IsAny<IDataMigratorExceptionHelper>()))
                                .Returns(entityMetadata)
                                .Verifiable();

            FluentActions.Invoking(() => systemUnderTest.CollectCrmEntityFields(metadataServiceMock.Object, serviceMock.Object, inputCheckedEntity, schemaConfiguration, inputEntityRelationships, inputEntityAttributes, inputAttributeMapping, feedbackManagerMock.Object, dataMigratorExceptionHelperMock.Object))
                                 .Should()
                                 .NotThrow();

            metadataServiceMock.VerifyAll();
        }

        [TestMethod]
        public void StoreCrmEntityData()
        {
            var entityLogicalName = "contact";
            var inputEntityAttributes = new Dictionary<string, HashSet<string>>();
            var attributeSet = new HashSet<string>() { "contactId", "firstname", "lastname" };
            inputEntityAttributes.Add(entityLogicalName, attributeSet);
            var inputAttributeMapping = new DataMigration.XrmToolBoxPlugin.Core.AttributeTypeMapping();

            var entityMetadata = InstantiateEntityMetaData(entityLogicalName);
            InsertAttributeList(entityMetadata, new List<string> { "contactId", "firstname", "lastname" });

            var crmEntity = new Capgemini.Xrm.DataMigration.Model.CrmEntity();
            var crmEntityList = new List<Capgemini.Xrm.DataMigration.Model.CrmEntity>();

            FluentActions.Invoking(() => systemUnderTest.StoreCrmEntityData(crmEntity, entityMetadata, crmEntityList, inputEntityRelationships, inputEntityAttributes, inputAttributeMapping, feedbackManagerMock.Object))
                                 .Should()
                                 .NotThrow();

            crmEntityList.Count.Should().Be(1);
        }

        [TestMethod]
        public void CollectCrmEntityRelationShipNoInputEntityRelationships()
        {
            var entityLogicalName = "contact";

            var entityMetadata = InstantiateEntityMetaData(entityLogicalName);
            InsertAttributeList(entityMetadata, new List<string> { "contactId", "firstname", "lastname" });

            var crmEntity = new Capgemini.Xrm.DataMigration.Model.CrmEntity();

            FluentActions.Invoking(() => systemUnderTest.CollectCrmEntityRelationShip(entityMetadata, crmEntity, inputEntityRelationships))
                                 .Should()
                                 .NotThrow();

            crmEntity.CrmRelationships.Count.Should().Be(0);
        }

        [TestMethod]
        public void CollectCrmEntityRelationShipWithInputEntityRelationships()
        {
            var entityLogicalName = "contact";
            var intersectEntityName = "account_contact";

            var relationship = new ManyToManyRelationshipMetadata
            {
                Entity1LogicalName = "account",
                Entity1IntersectAttribute = "accountid",
                IntersectEntityName = intersectEntityName,
                Entity2LogicalName = "contact",
                Entity2IntersectAttribute = "contactid",
                IsCustomizable = new BooleanManagedProperty() { Value = true }
            };

            var entityRelationshipSet = new HashSet<string>() { intersectEntityName };

            inputEntityRelationships.Add(entityLogicalName, entityRelationshipSet);

            var entityMetadata = InstantiateEntityMetaData(entityLogicalName);
            InsertAttributeList(entityMetadata, new List<string> { "contactId", "firstname", "lastname" });
            InsertManyToManyRelationshipMetadata(entityMetadata, relationship);

            var crmEntity = new Capgemini.Xrm.DataMigration.Model.CrmEntity();

            FluentActions.Invoking(() => systemUnderTest.CollectCrmEntityRelationShip(entityMetadata, crmEntity, inputEntityRelationships))
                                .Should()
                                .NotThrow();
            crmEntity.CrmRelationships.Count.Should().Be(1);
        }

        [TestMethod]
        public void StoreCrmEntityRelationShipData()
        {
            var entityLogicalName = "contact";
            var intersectEntityName = "account_contact";

            var relationship = new ManyToManyRelationshipMetadata
            {
                Entity1LogicalName = "account",
                Entity1IntersectAttribute = "accountid",
                IntersectEntityName = intersectEntityName,
                Entity2LogicalName = "contact",
                Entity2IntersectAttribute = "contactid",
                IsCustomizable = new BooleanManagedProperty() { Value = true }
            };

            var relationshipList = new List<Capgemini.Xrm.DataMigration.Model.CrmRelationship>();

            var crmEntity = new Capgemini.Xrm.DataMigration.Model.CrmEntity
            {
                Name = entityLogicalName,
                DisplayName = entityLogicalName
            };

            FluentActions.Invoking(() => systemUnderTest.StoreCrmEntityRelationShipData(crmEntity, relationship, relationshipList))
                                 .Should()
                                 .NotThrow();

            relationshipList.Count.Should().Be(1);
            relationshipList[0].RelatedEntityName.Should().Be(relationship.IntersectEntityName);
            relationshipList[0].RelationshipName.Should().Be(relationship.IntersectEntityName);
            relationshipList[0].IsReflexive.Should().Be(relationship.IsCustomizable.Value);
            relationshipList[0].ManyToMany.Should().BeTrue();
        }

        [TestMethod]
        public void CollectCrmAttributesFields()
        {
            var entityLogicalName = "contact";

            var inputEntityAttributes = new Dictionary<string, HashSet<string>>();
            var attributeSet = new HashSet<string>() { "contactId", "firstname", "lastname" };
            inputEntityAttributes.Add(entityLogicalName, attributeSet);
            var inputAttributeMapping = new DataMigration.XrmToolBoxPlugin.Core.AttributeTypeMapping();

            var entityMetadata = InstantiateEntityMetaData(entityLogicalName);
            InsertAttributeList(entityMetadata, new List<string> { "contactId", "firstname", "lastname" });

            var crmEntity = new Capgemini.Xrm.DataMigration.Model.CrmEntity
            {
                Name = entityLogicalName,
                DisplayName = entityLogicalName
            };

            FluentActions.Invoking(() => systemUnderTest.CollectCrmAttributesFields(entityMetadata, crmEntity, inputEntityAttributes, inputAttributeMapping, feedbackManagerMock.Object))
                                 .Should()
                                 .NotThrow();

            crmEntity.CrmFields.Count.Should().Be(3);
        }

        [TestMethod]
        public void CollectCrmAttributesFieldsWithNoInputEntityAttributes()
        {
            var entityLogicalName = "contact";

            var inputEntityAttributes = new Dictionary<string, HashSet<string>>();

            var inputAttributeMapping = new DataMigration.XrmToolBoxPlugin.Core.AttributeTypeMapping();

            var entityMetadata = InstantiateEntityMetaData(entityLogicalName);
            InsertAttributeList(entityMetadata, new List<string> { "contactId", "firstname", "lastname" });

            var crmEntity = new Capgemini.Xrm.DataMigration.Model.CrmEntity
            {
                Name = entityLogicalName,
                DisplayName = entityLogicalName
            };

            FluentActions.Invoking(() => systemUnderTest.CollectCrmAttributesFields(entityMetadata, crmEntity, inputEntityAttributes, inputAttributeMapping, feedbackManagerMock.Object))
                                 .Should()
                                 .NotThrow();

            crmEntity.CrmFields.Count.Should().Be(0);
        }

        [TestMethod]
        public void StoreAttributeMetadataWithNullInputEntityAttributes()
        {
            var entityLogicalName = "contact";
            Dictionary<string, HashSet<string>> inputEntityAttributes = null;
            var inputAttributeMapping = new DataMigration.XrmToolBoxPlugin.Core.AttributeTypeMapping();

            string primaryAttribute = "contactId";
            var entityMetadata = InstantiateEntityMetaData(entityLogicalName);
            InsertAttributeList(entityMetadata, new List<string> { primaryAttribute, "firstname", "lastname" });

            var crmFieldList = new List<Capgemini.Xrm.DataMigration.Model.CrmField>();

            FluentActions.Invoking(() => systemUnderTest.StoreAttributeMetadata(entityMetadata.Attributes[0], entityMetadata, primaryAttribute, crmFieldList, inputEntityAttributes, inputAttributeMapping, feedbackManagerMock.Object))
                                 .Should()
                                 .NotThrow();

            crmFieldList.Count.Should().Be(0);
        }

        [TestMethod]
        public void StoreAttributeMetadataWithNoInputEntityAttributes()
        {
            var entityLogicalName = "contact";

            var inputEntityAttributes = new Dictionary<string, HashSet<string>>();
            var inputAttributeMapping = new DataMigration.XrmToolBoxPlugin.Core.AttributeTypeMapping();

            string primaryAttribute = "contactId";
            var entityMetadata = InstantiateEntityMetaData(entityLogicalName);
            InsertAttributeList(entityMetadata, new List<string> { primaryAttribute, "firstname", "lastname" });

            var crmFieldList = new List<Capgemini.Xrm.DataMigration.Model.CrmField>();

            FluentActions.Invoking(() => systemUnderTest.StoreAttributeMetadata(entityMetadata.Attributes[0], entityMetadata, primaryAttribute, crmFieldList, inputEntityAttributes, inputAttributeMapping, feedbackManagerMock.Object))
                                 .Should()
                                 .NotThrow();

            crmFieldList.Count.Should().Be(0);
        }

        [TestMethod]
        public void StoreAttributeMetadata()
        {
            var entityLogicalName = "contact";

            var inputEntityAttributes = new Dictionary<string, HashSet<string>>();
            var attributeSet = new HashSet<string>() { "contactId", "firstname", "lastname" };
            inputEntityAttributes.Add(entityLogicalName, attributeSet);
            var inputAttributeMapping = new DataMigration.XrmToolBoxPlugin.Core.AttributeTypeMapping();

            string primaryAttribute = "contactId";
            var entityMetadata = InstantiateEntityMetaData(entityLogicalName);
            InsertAttributeList(entityMetadata, new List<string> { primaryAttribute, "firstname", "lastname" });

            var crmFieldList = new List<Capgemini.Xrm.DataMigration.Model.CrmField>();

            FluentActions.Invoking(() => systemUnderTest.StoreAttributeMetadata(entityMetadata.Attributes[0], entityMetadata, primaryAttribute, crmFieldList, inputEntityAttributes, inputAttributeMapping, feedbackManagerMock.Object))
                                 .Should()
                                 .NotThrow();

            crmFieldList.Count.Should().Be(1);
        }

        [TestMethod]
        public void StoreAttributePrimaryKey()
        {
            string primaryAttribute = "contactId";
            var crmField = new Capgemini.Xrm.DataMigration.Model.CrmField() { PrimaryKey = false, FieldName = primaryAttribute };

            FluentActions.Invoking(() => systemUnderTest.StoreAttributePrimaryKey(primaryAttribute, crmField))
                                 .Should()
                                 .NotThrow();

            crmField.PrimaryKey.Should().BeTrue();
        }

        [TestMethod]
        public void StoreAttributePrimaryKeyWithWrongPrimaryAttributeName()
        {
            string primaryAttribute = "contactId";
            var crmField = new Capgemini.Xrm.DataMigration.Model.CrmField() { PrimaryKey = false, FieldName = "contactId2" };

            FluentActions.Invoking(() => systemUnderTest.StoreAttributePrimaryKey(primaryAttribute, crmField))
                               .Should()
                               .NotThrow();

            crmField.PrimaryKey.Should().BeFalse();
        }

        [TestMethod]
        public void StoreLookUpAttributeNotEntityReference()
        {
            var attributeLogicalName = "contactId";
            string primaryAttribute = "contactId";
            var crmField = new Capgemini.Xrm.DataMigration.Model.CrmField()
            {
                PrimaryKey = false,
                FieldName = primaryAttribute,
                FieldType = "string"
            };

            var attribute = new AttributeMetadata
            {
                LogicalName = attributeLogicalName,
                DisplayName = new Label
                {
                    UserLocalizedLabel = new LocalizedLabel { Label = attributeLogicalName }
                }
            };

            var attributeTypeName = attribute.GetType().GetRuntimeFields().First(a => a.Name == "_attributeTypeDisplayName");
            attributeTypeName.SetValue(attribute, new AttributeTypeDisplayName { Value = attributeLogicalName });

            feedbackManagerMock.Setup(x => x.DisplayFeedback("The supplied attribute is null. Expecting an Entity Reference!"))
                              .Verifiable();

            FluentActions.Invoking(() => systemUnderTest.StoreLookUpAttribute(attribute, crmField, feedbackManagerMock.Object))
                             .Should()
                             .NotThrow();
            feedbackManagerMock.Verify(x => x.DisplayFeedback("The supplied attribute is null. Expecting an Entity Reference!"), Times.Never);
        }

        [TestMethod]
        public void StoreLookUpAttributeNotLookupAttributeMetadata()
        {
            var attributeLogicalName = "contactId";
            string primaryAttribute = "contactId";
            var crmField = new Capgemini.Xrm.DataMigration.Model.CrmField()
            {
                PrimaryKey = false,
                FieldName = primaryAttribute,
                FieldType = "entityreference"
            };

            var attribute = new AttributeMetadata
            {
                LogicalName = attributeLogicalName,
                DisplayName = new Label
                {
                    UserLocalizedLabel = new LocalizedLabel { Label = attributeLogicalName }
                }
            };

            var attributeTypeName = attribute.GetType().GetRuntimeFields().First(a => a.Name == "_attributeTypeDisplayName");
            attributeTypeName.SetValue(attribute, new AttributeTypeDisplayName { Value = attributeLogicalName });

            feedbackManagerMock.Setup(x => x.DisplayFeedback("The supplied attribute is null. Expecting an Entity Reference!"))
                               .Verifiable();

            FluentActions.Invoking(() => systemUnderTest.StoreLookUpAttribute(attribute, crmField, feedbackManagerMock.Object))
                             .Should()
                             .NotThrow();

            feedbackManagerMock.Verify(x => x.DisplayFeedback("The supplied attribute is null. Expecting an Entity Reference!"), Times.Once);
        }

        [TestMethod]
        public void StoreLookUpAttributeLookupAttributeMetadataIsNull()
        {
            string primaryAttribute = "contactId";
            var crmField = new Capgemini.Xrm.DataMigration.Model.CrmField()
            {
                PrimaryKey = false,
                FieldName = primaryAttribute,
                FieldType = "entityreference"
            };

            feedbackManagerMock.Setup(x => x.DisplayFeedback("The supplied attribute is null. Expecting an Entity Reference!"))
                               .Verifiable();

            FluentActions.Invoking(() => systemUnderTest.StoreLookUpAttribute(null, crmField, feedbackManagerMock.Object))
                             .Should()
                             .NotThrow();

            feedbackManagerMock.Verify(x => x.DisplayFeedback("The supplied attribute is null. Expecting an Entity Reference!"), Times.Once);
        }

        [TestMethod]
        public void StoreLookUpAttributeWithLookupAttributeMetadata()
        {
            var attributeLogicalName = "contactId";
            string primaryAttribute = "contactId";
            var crmField = new Capgemini.Xrm.DataMigration.Model.CrmField()
            {
                PrimaryKey = false,
                FieldName = primaryAttribute,
                FieldType = "entityreference"
            };

            var attribute = new LookupAttributeMetadata
            {
                LogicalName = attributeLogicalName,
                DisplayName = new Label
                {
                    UserLocalizedLabel = new LocalizedLabel { Label = attributeLogicalName }
                }
            };
            attribute.Targets = new List<string> { "Test" }.ToArray();

            feedbackManagerMock.Setup(x => x.DisplayFeedback("The supplied attribute is null. Expecting an Entity Reference!"))
                               .Verifiable();

            FluentActions.Invoking(() => systemUnderTest.StoreLookUpAttribute(attribute, crmField, feedbackManagerMock.Object))
                             .Should()
                             .NotThrow();

            feedbackManagerMock.Verify(x => x.DisplayFeedback("The supplied attribute is null. Expecting an Entity Reference!"), Times.Never);
            crmField.LookupType.Should().Be("Test");
        }

        [TestMethod]
        public void StoreLookUpAttributeWithLookupAttributeMetadataButEmptyTargetList()
        {
            var attributeLogicalName = "contactId";
            string primaryAttribute = "contactId";
            var crmField = new Capgemini.Xrm.DataMigration.Model.CrmField()
            {
                PrimaryKey = false,
                FieldName = primaryAttribute,
                FieldType = "entityreference"
            };

            var attribute = new LookupAttributeMetadata
            {
                LogicalName = attributeLogicalName,
                DisplayName = new Label
                {
                    UserLocalizedLabel = new LocalizedLabel { Label = attributeLogicalName }
                }
            };

            feedbackManagerMock.Setup(x => x.DisplayFeedback("The supplied attribute is null. Expecting an Entity Reference!"))
                               .Verifiable();

            FluentActions.Invoking(() => systemUnderTest.StoreLookUpAttribute(attribute, crmField, feedbackManagerMock.Object))
                             .Should()
                             .NotThrow();

            feedbackManagerMock.Verify(x => x.DisplayFeedback("The supplied attribute is null. Expecting an Entity Reference!"), Times.Never);
            crmField.LookupType.Should().BeNull();
        }

        private static void InsertManyToManyRelationshipMetadata(EntityMetadata entityMetadata, ManyToManyRelationshipMetadata relationship)
        {
            var manyToManyRelationshipMetadataList = new List<ManyToManyRelationshipMetadata>
            {
                relationship
            };

            var field = entityMetadata.GetType().GetRuntimeFields().First(a => a.Name == "_manyToManyRelationships");
            field.SetValue(entityMetadata, manyToManyRelationshipMetadataList.ToArray());
        }

        private static void InsertAttributeList(EntityMetadata entityMetadata, List<string> attributeLogicalNames)
        {
            var attributeList = new List<AttributeMetadata>();

            foreach (var item in attributeLogicalNames)
            {
                var attribute = new AttributeMetadata
                {
                    LogicalName = item,
                    DisplayName = new Label
                    {
                        UserLocalizedLabel = new LocalizedLabel { Label = item }
                    }
                };

                var attributeTypeName = attribute.GetType().GetRuntimeFields().First(a => a.Name == "_attributeTypeDisplayName");
                attributeTypeName.SetValue(attribute, new AttributeTypeDisplayName { Value = item });

                attributeList.Add(attribute);
            }

            var field = entityMetadata.GetType().GetRuntimeFields().First(a => a.Name == "_attributes");
            field.SetValue(entityMetadata, attributeList.ToArray());

            var isIntersectField = entityMetadata.GetType().GetRuntimeFields().First(a => a.Name == "_isIntersect");
            isIntersectField.SetValue(entityMetadata, (bool?)false);

            var isLogicalEntityField = entityMetadata.GetType().GetRuntimeFields().First(a => a.Name == "_isLogicalEntity");
            isLogicalEntityField.SetValue(entityMetadata, (bool?)false);

            var isCustomEntityField = entityMetadata.GetType().GetRuntimeFields().First(a => a.Name == "_isCustomEntity");
            isCustomEntityField.SetValue(entityMetadata, (bool?)true);
        }

        private void SetupMockObjects(string entityLogicalName)
        {
            var entityMetadata = InstantiateEntityMetaData(entityLogicalName);

            InsertAttributeList(entityMetadata, new List<string> { "contactattnoentity1" });

            var metadataList = new List<EntityMetadata>
            {
                entityMetadata
            };

            metadataServiceMock.Setup(x => x.RetrieveEntities(It.IsAny<string>(), It.IsAny<IOrganizationService>(), It.IsAny<IDataMigratorExceptionHelper>()))
                                .Returns(entityMetadata)
                                .Verifiable();

            metadataServiceMock.Setup(x => x.RetrieveEntities(It.IsAny<IOrganizationService>()))
                                .Returns(metadataList)
                                .Verifiable();
        }

        private EntityMetadata InstantiateEntityMetaData(string logicalName)
        {
            var entityMetadata = new EntityMetadata
            {
                LogicalName = logicalName,
                DisplayName = new Label
                {
                    UserLocalizedLabel = new LocalizedLabel { Label = logicalName }
                }
            };

            return entityMetadata;
        }
    }
}