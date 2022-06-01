using System;
using System.Collections.Generic;
using Capgemini.Xrm.CdsDataMigratorLibrary.Exceptions;
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

        [TestInitialize]
        public void Setup()
        {
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
            var entityLogicalName = "account";
            var entityMetadata = new EntityMetadata
            {
                LogicalName = entityLogicalName,
                DisplayName = new Label
                {
                    UserLocalizedLabel = new LocalizedLabel { Label = "Test" }
                }
            };
            var settings = new Capgemini.Xrm.CdsDataMigratorLibrary.Core.Settings();

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
            var entityLogicalName = "account";
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
            var entityLogicalName = "contact";
            var intersectEntityName = "account_contact";

            var settings = new CdsDataMigratorLibrary.Core.Settings();

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

            var entityLogicalName = "account";
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
    }
}