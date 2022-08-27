using System;
using System.Collections.Generic;
using Capgemini.Xrm.CdsDataMigratorLibrary.Core;
using Capgemini.Xrm.CdsDataMigratorLibrary.Exceptions;
using Capgemini.Xrm.CdsDataMigratorLibrary.Forms;
using Capgemini.Xrm.CdsDataMigratorLibrary.Tests.Unit.Mocks;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Moq;

namespace Capgemini.Xrm.CdsDataMigrator.Tests.Unit.UserControls
{
    [TestClass]
    public class SchemaWizardTests : TestBase
    {
        private Dictionary<string, HashSet<string>> inputEntityRelationships;
        private bool workingstate;
        private string entityLogicalName;
        private string intersectEntityName;
        private Settings settings;
        private ManyToManyRelationshipMetadata relationship;
        private EntityMetadata entityMetadata;

        [TestInitialize]
        public void Setup()
        {
            entityLogicalName = "contact";
            intersectEntityName = "account_contact";
            settings = new Settings();
            relationship = new ManyToManyRelationshipMetadata
            {
                Entity1LogicalName = "account",
                Entity1IntersectAttribute = "accountid",
                IntersectEntityName = intersectEntityName,
                Entity2LogicalName = "contact",
                Entity2IntersectAttribute = "contactid",
                IsCustomizable = new BooleanManagedProperty() { Value = true }
            };

            entityMetadata = InstantiateEntityMetaData(entityLogicalName);
            workingstate = false;
            SetupServiceMocks();
            inputEntityRelationships = new Dictionary<string, HashSet<string>>();
        }

        [TestMethod]
        public void SchemaGeneratorInstatiation()
        {
            FluentActions.Invoking(() => new SchemaWizard())
                        .Should()
                        .NotThrow();
        }

        [TestMethod]
        public void InitFilter()
        {
            using (var systemUnderTest = new SchemaWizard())
            {
                var entityitem = new System.Windows.Forms.ListViewItem();

                systemUnderTest.OrganizationService = ServiceMock.Object;
                systemUnderTest.MetadataService = MetadataServiceMock.Object;

                FluentActions.Invoking(() => systemUnderTest.InitFilter(entityitem))
                        .Should()
                        .NotThrow();
            }
        }

        [TestMethod]
        public void InitFilterWithListViewtag()
        {
            var entityMetadata = new EntityMetadata
            {
                LogicalName = entityLogicalName,
                DisplayName = new Label
                {
                    UserLocalizedLabel = new LocalizedLabel { Label = "Test" }
                }
            };
            var settings = new Settings();

            using (var systemUnderTest = new SchemaWizard())
            {
                var entityitem = new System.Windows.Forms.ListViewItem
                {
                    Tag = entityMetadata
                };
                systemUnderTest.Settings = settings;

                systemUnderTest.OrganizationService = ServiceMock.Object;
                systemUnderTest.MetadataService = MetadataServiceMock.Object;

                FluentActions.Invoking(() => systemUnderTest.InitFilter(entityitem))
                        .Should()
                        .NotThrow();
            }
        }

        [TestMethod]
        public void InitFilterWithListViewtagAndNoSettings()
        {
            var entityMetadata = new EntityMetadata
            {
                LogicalName = entityLogicalName,
                DisplayName = new Label
                {
                    UserLocalizedLabel = new LocalizedLabel { Label = "Test" }
                }
            };

            using (var systemUnderTest = new SchemaWizard())
            {
                var entityitem = new System.Windows.Forms.ListViewItem
                {
                    Tag = entityMetadata
                };
                systemUnderTest.Settings = null;

                systemUnderTest.OrganizationService = ServiceMock.Object;
                systemUnderTest.MetadataService = MetadataServiceMock.Object;

                FluentActions.Invoking(() => systemUnderTest.InitFilter(entityitem))
                        .Should()
                        .Throw<NullReferenceException>()
                        .WithMessage("Object reference not set to an instance of an object.");
            }
        }

        [TestMethod]
        public void OnConnectionUpdated()
        {
            string entityLogicalName = "account_contact";
            SetupMockObjects(entityLogicalName);

            using (var systemUnderTest = new SchemaWizard())
            {
                systemUnderTest.OrganizationService = ServiceMock.Object;
                systemUnderTest.MetadataService = MetadataServiceMock.Object;

                FluentActions.Invoking(() => systemUnderTest.OnConnectionUpdated(Guid.NewGuid(), "TestOrg"))
                        .Should()
                        .NotThrow();
            }
        }

        [TestMethod]
        public void HandleListViewEntitiesSelectedIndexChanged()
        {
            string inputEntityLogicalName = "account";
            HashSet<string> inputSelectedEntity = new HashSet<string>();
            using (var listView = new System.Windows.Forms.ListView())
            {
                var selectedItems = new System.Windows.Forms.ListView.SelectedListViewItemCollection(listView);
                using (var systemUnderTest = new SchemaWizard())
                {
                    systemUnderTest.OrganizationService = ServiceMock.Object;
                    systemUnderTest.MetadataService = MetadataServiceMock.Object;
                    var serviceParameters = GenerateMigratorParameters();
                    FluentActions.Invoking(() => systemUnderTest.HandleListViewEntitiesSelectedIndexChanged(inputEntityRelationships, inputEntityLogicalName, inputSelectedEntity, selectedItems, serviceParameters))
                            .Should()
                            .NotThrow();
                }
            }
        }

        [TestMethod]
        public void PopulateAttributes()
        {
            InsertAttributeList(entityMetadata, new List<string> { "contactId", "firstname", "lastname" });
            InsertManyToManyRelationshipMetadata(entityMetadata, relationship);

            MetadataServiceMock.Setup(x => x.RetrieveEntities(It.IsAny<string>(), It.IsAny<IOrganizationService>(), It.IsAny<IExceptionService>()))
                              .Returns(entityMetadata)
                              .Verifiable();

            using (var listView = new System.Windows.Forms.ListView())
            {
                var item = new System.Windows.Forms.ListViewItem { };
                listView.Items.Add(item);
                var selectedItem = listView.Items[0];

                using (var systemUnderTest = new SchemaWizard())
                {
                    systemUnderTest.Settings = settings;
                    systemUnderTest.OrganizationService = ServiceMock.Object;
                    systemUnderTest.MetadataService = MetadataServiceMock.Object;

                    var serviceParameters = GenerateMigratorParameters();
                    FluentActions.Awaiting(() => systemUnderTest.PopulateAttributes(entityLogicalName, selectedItem, serviceParameters))
                    .Should()
                    .NotThrow();
                }
            }
        }

        [TestMethod]
        public void PopulateAttributesWithException()
        {
            InsertAttributeList(entityMetadata, new List<string> { "contactId", "firstname", "lastname" });
            InsertManyToManyRelationshipMetadata(entityMetadata, relationship);

            MetadataServiceMock.Setup(x => x.RetrieveEntities(It.IsAny<string>(), It.IsAny<IOrganizationService>(), It.IsAny<IExceptionService>()))
                              .Returns(entityMetadata)
                              .Verifiable();

            using (var listView = new System.Windows.Forms.ListView())
            {
                var item = new System.Windows.Forms.ListViewItem { };
                listView.Items.Add(item);
                var selectedItem = listView.Items[0];

                using (var systemUnderTest = new SchemaWizard())
                {
                    systemUnderTest.OrganizationService = ServiceMock.Object;
                    systemUnderTest.MetadataService = MetadataServiceMock.Object;

                    var serviceParameters = GenerateMigratorParameters();
                    FluentActions.Awaiting(() => systemUnderTest.PopulateAttributes(entityLogicalName, selectedItem, serviceParameters))
                    .Should().Throw<NullReferenceException>();
                }
            }
        }

        [TestMethod]
        public void PopulateRelationship()
        {
            var entityMetadata = InstantiateEntityMetaData(entityLogicalName);
            InsertAttributeList(entityMetadata, new List<string> { "contactId", "firstname", "lastname" });
            InsertManyToManyRelationshipMetadata(entityMetadata, relationship);

            MetadataServiceMock.Setup(x => x.RetrieveEntities(It.IsAny<string>(), It.IsAny<IOrganizationService>(), It.IsAny<IExceptionService>()))
                              .Returns(entityMetadata)
                              .Verifiable();


            using (var listView = new System.Windows.Forms.ListView())
            {
                var item = new System.Windows.Forms.ListViewItem { };
                listView.Items.Add(item);
                var selectedItems = listView.Items[0];

                using (var systemUnderTest = new SchemaWizard())
                {
                    systemUnderTest.OrganizationService = ServiceMock.Object;
                    systemUnderTest.MetadataService = MetadataServiceMock.Object;

                    var serviceParameters = GenerateMigratorParameters();
                    FluentActions.Awaiting(() => systemUnderTest.PopulateRelationship(entityLogicalName, inputEntityRelationships, selectedItems, serviceParameters))
                    .Should()
                    .NotThrow();
                }

            }
        }

        [TestMethod]
        public void ClearMemory()
        {
            using (var systemUnderTest = new SchemaWizard())
            {
                systemUnderTest.OrganizationService = ServiceMock.Object;
                systemUnderTest.MetadataService = MetadataServiceMock.Object;

                FluentActions.Invoking(() => systemUnderTest.ClearMemory())
                             .Should()
                             .NotThrow();
            }
        }

        [TestMethod]
        public void LoadSchemaFileWithEmptyExportConfigPath()
        {
            string schemaFilename = string.Empty;
            var inputEntityAttributes = new Dictionary<string, HashSet<string>>();

            NotificationServiceMock.Setup(x => x.DisplayFeedback(It.IsAny<string>()))
                                .Verifiable();

            using (var systemUnderTest = new SchemaWizard())
            {
                systemUnderTest.OrganizationService = ServiceMock.Object;
                systemUnderTest.MetadataService = MetadataServiceMock.Object;
                systemUnderTest.NotificationService = NotificationServiceMock.Object;

                FluentActions.Invoking(() => systemUnderTest.LoadSchemaFile(schemaFilename, workingstate, NotificationServiceMock.Object, inputEntityAttributes, inputEntityRelationships))
                             .Should()
                             .NotThrow();
            }

            NotificationServiceMock.Verify(x => x.DisplayFeedback(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void LoadSchemaFileWithInValidPath()
        {
            string configFilename = "hello.txt";

            var inputEntityAttributes = new Dictionary<string, HashSet<string>>();

            NotificationServiceMock.Setup(x => x.DisplayFeedback(It.IsAny<string>()))
                               .Verifiable();

            using (var systemUnderTest = new SchemaWizard())
            {
                systemUnderTest.OrganizationService = ServiceMock.Object;
                systemUnderTest.MetadataService = MetadataServiceMock.Object;
                systemUnderTest.NotificationService = NotificationServiceMock.Object;

                FluentActions.Invoking(() => systemUnderTest.LoadSchemaFile(configFilename, workingstate, NotificationServiceMock.Object, inputEntityAttributes, inputEntityRelationships))
                             .Should()
                             .NotThrow();
            }

            NotificationServiceMock.Verify(x => x.DisplayFeedback(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void LoadSchemaFileWithValidPath()
        {
            string configFilename = "TestData\\testschemafile.xml";

            var entityMetadata = new EntityMetadata
            {
                LogicalName = entityLogicalName,
                DisplayName = new Label
                {
                    UserLocalizedLabel = new LocalizedLabel { Label = "Test" }
                }
            };

            InsertAttributeList(entityMetadata, new List<string> { "contactattnoentity1" });

            var inputEntityAttributes = new Dictionary<string, HashSet<string>>();

            workingstate = true;

            using (var systemUnderTest = new SchemaWizard())
            {
                systemUnderTest.OrganizationService = ServiceMock.Object;
                systemUnderTest.MetadataService = MetadataServiceMock.Object;
                systemUnderTest.NotificationService = NotificationServiceMock.Object;

                FluentActions.Invoking(() => systemUnderTest.LoadSchemaFile(configFilename, workingstate, NotificationServiceMock.Object, inputEntityAttributes, inputEntityRelationships))
                             .Should()
                             .NotThrow();
            }
        }

        [TestMethod]
        public void ManageWorkingStateSetWorkingStateToTrue()
        {
            using (var systemUnderTest = new SchemaWizard())
            {
                systemUnderTest.OrganizationService = ServiceMock.Object;
                systemUnderTest.MetadataService = MetadataServiceMock.Object;
                systemUnderTest.NotificationService = NotificationServiceMock.Object;

                FluentActions.Invoking(() => systemUnderTest.ManageWorkingState(true))
                                 .Should()
                                 .NotThrow();
            }
        }

        [TestMethod]
        public void ManageWorkingStateSetWorkingStateToFalse()
        {
            using (var systemUnderTest = new SchemaWizard())
            {
                systemUnderTest.OrganizationService = ServiceMock.Object;
                systemUnderTest.MetadataService = MetadataServiceMock.Object;
                systemUnderTest.NotificationService = NotificationServiceMock.Object;

                FluentActions.Invoking(() => systemUnderTest.ManageWorkingState(false))
                                 .Should()
                                 .NotThrow();
            }
        }

        [TestMethod]
        public void InvokeRadioButton1CheckedChanged()
        {
            using (var systemUnderTest = new MockupForSchemaWizard())
            {
                systemUnderTest.OrganizationService = ServiceMock.Object;
                systemUnderTest.MetadataService = MetadataServiceMock.Object;
                systemUnderTest.NotificationService = NotificationServiceMock.Object;

                FluentActions.Invoking(() => systemUnderTest.InvokeRadioButton1CheckedChanged(new EventArgs()))
                                 .Should()
                                 .NotThrow();
            }
        }

        [TestMethod]
        public void InvokeRadioButton2CheckedChanged()
        {
            using (var systemUnderTest = new MockupForSchemaWizard())
            {
                systemUnderTest.OrganizationService = ServiceMock.Object;
                systemUnderTest.MetadataService = MetadataServiceMock.Object;
                systemUnderTest.NotificationService = NotificationServiceMock.Object;

                FluentActions.Invoking(() => systemUnderTest.InvokeRadioButton2CheckedChanged(new EventArgs()))
                                 .Should()
                                 .NotThrow();
            }
        }

        [TestMethod]
        public void InvokeRadioButton3CheckedChanged()
        {
            using (var systemUnderTest = new MockupForSchemaWizard())
            {
                systemUnderTest.OrganizationService = ServiceMock.Object;
                systemUnderTest.MetadataService = MetadataServiceMock.Object;
                systemUnderTest.NotificationService = NotificationServiceMock.Object;

                FluentActions.Invoking(() => systemUnderTest.InvokeRadioButton3CheckedChanged(new EventArgs()))
                                 .Should()
                                 .NotThrow();
            }
        }

        [TestMethod]
        public void RadioButton4CheckedChanged()
        {
            using (var systemUnderTest = new MockupForSchemaWizard())
            {
                systemUnderTest.OrganizationService = ServiceMock.Object;
                systemUnderTest.MetadataService = MetadataServiceMock.Object;
                systemUnderTest.NotificationService = NotificationServiceMock.Object;

                FluentActions.Invoking(() => systemUnderTest.RadioButton4CheckedChanged(new EventArgs()))
                                 .Should()
                                 .NotThrow();
            }
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

            using (var systemUnderTest = new SchemaWizard())
            {
                FluentActions.Invoking(() => systemUnderTest.HandleMappingControlItemClick(NotificationServiceMock.Object, inputEntityLogicalName, listViewItemIsSelected, inputMapping, inputMapper, null))
                         .Should()
                         .NotThrow();
            }
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
            using (var systemUnderTest = new SchemaWizard())
            {
                FluentActions.Invoking(() => systemUnderTest.HandleMappingControlItemClick(NotificationServiceMock.Object, inputEntityLogicalName, listViewItemIsSelected, inputMapping, inputMapper, null))
                         .Should()
                         .NotThrow();
            }
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

            using (var systemUnderTest = new SchemaWizard())
            {
                FluentActions.Invoking(() => systemUnderTest.HandleMappingControlItemClick(NotificationServiceMock.Object, inputEntityLogicalName, listViewItemIsSelected, inputMapping, inputMapper, null))
                         .Should()
                         .NotThrow();
            }

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

            using (var systemUnderTest = new MockupForSchemaWizard())
            {
                using (var filterDialog = new FilterEditor(null, System.Windows.Forms.FormStartPosition.CenterParent))
                {
                    FluentActions.Invoking(() => systemUnderTest.ProcessFilterQuery(NotificationServiceMock.Object, null, inputEntityLogicalName, listViewItemIsSelected, inputFilterQuery, filterDialog))
                             .Should()
                             .NotThrow();
                }
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


            using (var systemUnderTest = new MockupForSchemaWizard())
            {
                using (var filterDialog = new FilterEditor(null, System.Windows.Forms.FormStartPosition.CenterParent))
                {
                    filterDialog.QueryString = "< filter type =\"and\" > < condition attribute =\"sw_appointmentstatus\" operator=\"eq\" value=\"266880017\" /></ filter >";

                    FluentActions.Invoking(() => systemUnderTest.ProcessFilterQuery(NotificationServiceMock.Object, null, inputEntityLogicalName, listViewItemIsSelected, inputFilterQuery, filterDialog))
                             .Should()
                             .NotThrow();
                }
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


            using (var systemUnderTest = new MockupForSchemaWizard())
            {
                using (var filterDialog = new FilterEditor(currentfilter, System.Windows.Forms.FormStartPosition.CenterParent))
                {
                    filterDialog.QueryString = "< filter type =\"and\" > < condition attribute =\"sw_appointmentstatus\" operator=\"eq\" value=\"266880017\" /></ filter >";

                    FluentActions.Invoking(() => systemUnderTest.ProcessFilterQuery(NotificationServiceMock.Object, null, inputEntityLogicalName, listViewItemIsSelected, inputFilterQuery, filterDialog))
                             .Should()
                             .NotThrow();
                }
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


            using (var systemUnderTest = new MockupForSchemaWizard())
            {
                using (var filterDialog = new FilterEditor(currentfilter, System.Windows.Forms.FormStartPosition.CenterParent))
                {
                    filterDialog.QueryString = string.Empty;

                    FluentActions.Invoking(() => systemUnderTest.ProcessFilterQuery(NotificationServiceMock.Object, null, inputEntityLogicalName, listViewItemIsSelected, inputFilterQuery, filterDialog))
                             .Should()
                             .NotThrow();
                }
            }

            NotificationServiceMock.Verify(x => x.DisplayFeedback(It.IsAny<string>()), Times.Never);
        }

    }
}