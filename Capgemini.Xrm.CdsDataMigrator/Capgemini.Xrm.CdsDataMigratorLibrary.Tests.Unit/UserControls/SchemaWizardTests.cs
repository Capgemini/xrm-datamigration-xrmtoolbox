using System;
using System.Collections.Generic;
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
    }
}