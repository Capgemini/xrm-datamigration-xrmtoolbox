using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Capgemini.Xrm.DataMigration.XrmToolBox.Services;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Moq;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Forms;

namespace Capgemini.Xrm.CdsDataMigrator.Tests.Unit.UserControls
{
    [TestClass]
    public class SchemaWizardTests
    {
        private Mock<IOrganizationService> serviceMock;
        private Mock<IMetadataService> metadataServiceMock;
        private Mock<IFeedbackManager> feedbackManagerMock;
        private Dictionary<string, HashSet<string>> inputEntityRelationships;

        private bool workingstate;

        [TestInitialize]
        public void Setup()
        {
            workingstate = false;

            serviceMock = new Mock<IOrganizationService>();
            metadataServiceMock = new Mock<IMetadataService>();
            feedbackManagerMock = new Mock<IFeedbackManager>();

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
        public void OnConnectionUpdated()
        {
            string entityLogicalName = "account_contact";
            SetupMockObjects(entityLogicalName);

            using (var systemUnderTest = new SchemaWizard())
            {
                systemUnderTest.OrganizationService = serviceMock.Object;
                systemUnderTest.MetadataService = metadataServiceMock.Object;

                FluentActions.Invoking(() => systemUnderTest.OnConnectionUpdated(Guid.NewGuid(), "TestOrg"))
                        .Should()
                        .NotThrow();
            }
        }

        [TestMethod]
        public void GetEntityLogicalNameNullListViewItem()
        {
            using (var systemUnderTest = new SchemaWizard())
            {
                systemUnderTest.OrganizationService = serviceMock.Object;
                System.Windows.Forms.ListViewItem entityitem = null;

                var actual = systemUnderTest.GetEntityLogicalName(entityitem);

                actual.Should().BeNull();
            }
        }

        [TestMethod]
        public void GetEntityLogicalNameInstantiatedListViewItemWithNullTag()
        {
            using (var systemUnderTest = new SchemaWizard())
            {
                systemUnderTest.OrganizationService = serviceMock.Object;
                System.Windows.Forms.ListViewItem entityitem = new System.Windows.Forms.ListViewItem();

                var actual = systemUnderTest.GetEntityLogicalName(entityitem);

                actual.Should().BeNull();
            }
        }

        [TestMethod]
        public void GetEntityLogicalNameInstantiatedListViewItemWithEntityMetadataTag()
        {
            var entityMetadata = new EntityMetadata() { LogicalName = "account" };

            using (var systemUnderTest = new SchemaWizard())
            {
                systemUnderTest.OrganizationService = serviceMock.Object;
                System.Windows.Forms.ListViewItem entityitem = new System.Windows.Forms.ListViewItem();
                entityitem.Tag = entityMetadata;

                var actual = systemUnderTest.GetEntityLogicalName(entityitem);

                actual.Should().Be(entityMetadata.LogicalName);
            }
        }

        [TestMethod]
        public void AddSelectedEntitiesWhenSelectedEntitySetIsNullAndSelectedItemCountIsZero()
        {
            //var showSystemAttributes = true;
            string entityLogicalName = "account_contact";
            // SetupMockObjects(entityLogicalName);

            inputEntityRelationships = new Dictionary<string, HashSet<string>>();
            var selectedItemsCount = 0;
            //var selectedEntity = new HashSet<string>();

            using (var systemUnderTest = new SchemaWizard())
            {
                ////AddSelectedEntities(int selectedItemsCount, string inputEntityLogicalName, HashSet<string> inputSelectedEntity)

                FluentActions.Invoking(() => systemUnderTest.AddSelectedEntities(selectedItemsCount, entityLogicalName, null))
                             .Should()
                             .NotThrow();

                //selectedEntity.Count.Should().Be(0);
            }
        }

        [TestMethod]
        public void AddSelectedEntitiesWhenSelectedEntitySetIsNullAndSelectedItemCountIsNotZero()
        {
            //var showSystemAttributes = true;
            string entityLogicalName = "account_contact";
            // SetupMockObjects(entityLogicalName);

            inputEntityRelationships = new Dictionary<string, HashSet<string>>();
            var selectedItemsCount = 2;
            //var selectedEntity = new HashSet<string>();

            using (var systemUnderTest = new SchemaWizard())
            {
                ////AddSelectedEntities(int selectedItemsCount, string inputEntityLogicalName, HashSet<string> inputSelectedEntity)

                FluentActions.Invoking(() => systemUnderTest.AddSelectedEntities(selectedItemsCount, entityLogicalName, null))
                             .Should()
                             .Throw<NullReferenceException>();

                //selectedEntity.Count.Should().Be(0);
            }
        }

        [TestMethod]
        public void AddSelectedEntitiesWhenSelectedEntitySetIsNotNullAndSelectedEntityDoesNotContainLogicalName()
        {
            //var showSystemAttributes = true;
            string entityLogicalName = "account_contact";
            // SetupMockObjects(entityLogicalName);

            inputEntityRelationships = new Dictionary<string, HashSet<string>>();
            var selectedItemsCount = 2;
            var selectedEntity = new HashSet<string>();

            using (var systemUnderTest = new SchemaWizard())
            {
                ////AddSelectedEntities(int selectedItemsCount, string inputEntityLogicalName, HashSet<string> inputSelectedEntity)

                FluentActions.Invoking(() => systemUnderTest.AddSelectedEntities(selectedItemsCount, entityLogicalName, selectedEntity))
                             .Should()
                             .NotThrow();

                selectedEntity.Count.Should().Be(1);
            }
        }

        [TestMethod]
        public void AddSelectedEntitiesWhenSelectedEntitySetIsNotNullAndSelectedEntityContainsLogicalName()
        {
            //var showSystemAttributes = true;
            string entityLogicalName = "account_contact";
            // SetupMockObjects(entityLogicalName);

            inputEntityRelationships = new Dictionary<string, HashSet<string>>();
            var selectedItemsCount = 2;
            var selectedEntity = new HashSet<string>
            {
                entityLogicalName
            };

            using (var systemUnderTest = new SchemaWizard())
            {
                FluentActions.Invoking(() => systemUnderTest.AddSelectedEntities(selectedItemsCount, entityLogicalName, selectedEntity))
                             .Should()
                             .NotThrow();

                selectedEntity.Count.Should().Be(1);
            }
        }

        [TestMethod]
        public void AddSelectedEntitiesWhenSelectedItemCountIsZero()
        {
            //var showSystemAttributes = true;
            string entityLogicalName = "account_contact";
            // SetupMockObjects(entityLogicalName);

            inputEntityRelationships = new Dictionary<string, HashSet<string>>();
            var selectedItemsCount = 0;
            var selectedEntity = new HashSet<string>();

            using (var systemUnderTest = new SchemaWizard())
            {
                ////AddSelectedEntities(int selectedItemsCount, string inputEntityLogicalName, HashSet<string> inputSelectedEntity)

                FluentActions.Invoking(() => systemUnderTest.AddSelectedEntities(selectedItemsCount, entityLogicalName, selectedEntity))
                             .Should()
                             .NotThrow();

                selectedEntity.Count.Should().Be(0);
            }
        }

        [TestMethod]
        public void ProcessListViewEntitiesSelectedIndexChanged()
        {
            var items = new List<System.Windows.Forms.ListViewItem>
            {
                new System.Windows.Forms.ListViewItem("Item1"),
                new System.Windows.Forms.ListViewItem("Item2")
            };

            var entityMetadata = new EntityMetadata() { LogicalName = "account" };
            System.Windows.Forms.ListViewItem entityitem = new System.Windows.Forms.ListViewItem();
            entityitem.Tag = entityMetadata;

            using (var systemUnderTest = new SchemaWizard())
            {
                systemUnderTest.OrganizationService = serviceMock.Object;
                systemUnderTest.MetadataService = metadataServiceMock.Object;
                //systemUnderTest.PopulateEntitiesListView(items, null);
                //systemUnderTest.GetEntityLogicalName();

                FluentActions.Invoking(() => systemUnderTest.ProcessListViewEntitiesSelectedIndexChanged(metadataServiceMock.Object, inputEntityRelationships, entityitem))
                        .Should()
                        .NotThrow();
            }
        }

        [TestMethod]
        public void RetrieveSourceEntitiesListShowSystemAttributesIsFalse()
        {
            var showSystemAttributes = false;
            string entityLogicalName = "account_contact";
            SetupMockObjects(entityLogicalName);

            using (var systemUnderTest = new SchemaWizard())
            {
                systemUnderTest.OrganizationService = serviceMock.Object;
                systemUnderTest.MetadataService = metadataServiceMock.Object;

                var actual = systemUnderTest.RetrieveSourceEntitiesList(showSystemAttributes, serviceMock.Object, metadataServiceMock.Object);

                actual.Count.Should().Be(1);
            }
        }

        [TestMethod]
        public void RetrieveSourceEntitiesListShowSystemAttributesIsTrue()
        {
            var showSystemAttributes = true;
            string entityLogicalName = "account_contact";
            SetupMockObjects(entityLogicalName);

            using (var systemUnderTest = new SchemaWizard())
            {
                systemUnderTest.OrganizationService = serviceMock.Object;
                systemUnderTest.MetadataService = metadataServiceMock.Object;

                var actual = systemUnderTest.RetrieveSourceEntitiesList(showSystemAttributes, serviceMock.Object, metadataServiceMock.Object);

                actual.Count.Should().Be(1);
            }
        }

        [TestMethod]
        public void PopulateRelationshipActionNoManyToManyRelationships()
        {
            string entityLogicalName = "contact";
            var entityMetadata = new EntityMetadata();

            metadataServiceMock.Setup(x => x.RetrieveEntities(It.IsAny<string>(), It.IsAny<IOrganizationService>()))
                .Returns(entityMetadata)
                .Verifiable();

            using (var systemUnderTest = new SchemaWizard())
            {
                systemUnderTest.OrganizationService = serviceMock.Object;
                systemUnderTest.MetadataService = metadataServiceMock.Object;

                var actual = systemUnderTest.PopulateRelationshipAction(entityLogicalName, serviceMock.Object, metadataServiceMock.Object, inputEntityRelationships);

                actual.Count.Should().Be(0);
            }

            serviceMock.VerifyAll();
            metadataServiceMock.VerifyAll();
        }

        [TestMethod]
        public void PopulateRelationshipAction()
        {
            string entityLogicalName = "account_contact";
            var items = new List<System.Windows.Forms.ListViewItem>();
            items.Add(new System.Windows.Forms.ListViewItem("Item1"));
            items.Add(new System.Windows.Forms.ListViewItem("Item2"));

            var entityMetadata = new EntityMetadata();

            //var relationship = new ManyToManyRelationshipMetadata
            //{
            //    Entity1LogicalName = "account",
            //    Entity1IntersectAttribute = "accountid",
            //    IntersectEntityName = "account_contact",
            //    Entity2LogicalName = "contact",
            //    Entity2IntersectAttribute = "contactid"
            //};

            //var manyToManyRelationshipMetadataList = new List<ManyToManyRelationshipMetadata>
            //{
            //    relationship
            //};

            //var field = entityMetadata.GetType().GetRuntimeFields().First(a => a.Name == "_manyToManyRelationships");
            //field.SetValue(entityMetadata, manyToManyRelationshipMetadataList.ToArray());
            InsertManyToManyRelationshipMetadata(entityMetadata);

            metadataServiceMock.Setup(x => x.RetrieveEntities(It.IsAny<string>(), It.IsAny<IOrganizationService>()))
                .Returns(entityMetadata)
                .Verifiable();

            using (var systemUnderTest = new SchemaWizard())
            {
                systemUnderTest.OrganizationService = serviceMock.Object;
                systemUnderTest.MetadataService = metadataServiceMock.Object;
                systemUnderTest.PopulateEntitiesListView(items, null);
                //systemUnderTest.GetEntityLogicalName();

                var actual = systemUnderTest.PopulateRelationshipAction(entityLogicalName, serviceMock.Object, metadataServiceMock.Object, inputEntityRelationships);

                actual.Count.Should().BeGreaterThan(0);
            }

            serviceMock.VerifyAll();
            metadataServiceMock.VerifyAll();
        }

        [TestMethod]
        public void PopulateRelationshipListViewItemSelected()
        {
            var entityLogicalName = Guid.NewGuid().ToString();// "contact";
            System.Windows.Forms.ListViewItem listViewItemSelected = new System.Windows.Forms.ListViewItem();//ListViewItem listViewSelectedItem

            //SetupMockObjects(entityLogicalName);
            var entityMetadata = new EntityMetadata
            {
                LogicalName = entityLogicalName,
                DisplayName = new Label
                {
                    UserLocalizedLabel = new LocalizedLabel { Label = "Test" }
                }
            };

            InsertAttributeList(entityMetadata);
            InsertManyToManyRelationshipMetadata(entityMetadata);

            //var metadataList = new List<EntityMetadata>
            //{
            //    entityMetadata
            //};

            //metadataServiceMock.Setup(x => x.RetrieveEntities(It.IsAny<string>(), It.IsAny<IOrganizationService>()))
            //                    .Returns(entityMetadata)
            //                    .Verifiable();

            //metadataServiceMock.Setup(x => x.RetrieveEntities(It.IsAny<IOrganizationService>()))
            //                    .Returns(metadataList)
            //                    .Verifiable();

            //var entityMetaData = metadataService.RetrieveEntities(inputEntityLogicalName, service);

            //if (entityMetaData != null && entityMetaData.ManyToManyRelationships != null && entityMetaData.ManyToManyRelationships.Any())

            using (var systemUnderTest = new SchemaWizard())
            {
                systemUnderTest.OrganizationService = serviceMock.Object;
                systemUnderTest.MetadataService = metadataServiceMock.Object;

                FluentActions.Invoking(() => systemUnderTest.PopulateRelationship(entityLogicalName, serviceMock.Object, metadataServiceMock.Object, inputEntityRelationships, listViewItemSelected))
                        .Should()
                        .NotThrow();
            }

            serviceMock.VerifyAll();
            //metadataServiceMock.VerifyAll();
        }

        [TestMethod]
        public void PopulateEntitiesListViewWhenThereIsAnException()
        {
            var items = new List<System.Windows.Forms.ListViewItem>();
            items.Add(new System.Windows.Forms.ListViewItem("Item1"));
            items.Add(new System.Windows.Forms.ListViewItem("Item2"));
            Exception exception = new Exception();

            feedbackManagerMock.Setup(x => x.DisplayErrorFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()))
                                .Verifiable();
            feedbackManagerMock.Setup(x => x.DisplayWarningFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()))
                               .Verifiable();

            using (var systemUnderTest = new SchemaWizard())
            {
                systemUnderTest.OrganizationService = serviceMock.Object;
                systemUnderTest.MetadataService = metadataServiceMock.Object;
                systemUnderTest.FeedbackManager = feedbackManagerMock.Object;

                FluentActions.Invoking(() => systemUnderTest.PopulateEntitiesListView(items, exception))
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

            using (var systemUnderTest = new SchemaWizard())
            {
                systemUnderTest.OrganizationService = serviceMock.Object;
                systemUnderTest.MetadataService = metadataServiceMock.Object;
                systemUnderTest.FeedbackManager = feedbackManagerMock.Object;

                FluentActions.Invoking(() => systemUnderTest.PopulateEntitiesListView(items, exception))
                        .Should()
                        .NotThrow();
            }

            feedbackManagerMock.Verify(x => x.DisplayErrorFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()), Times.Never);
            feedbackManagerMock.Verify(x => x.DisplayWarningFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void PopulateEntitiesListViewWhenThereAreListItems()
        {
            var items = new List<System.Windows.Forms.ListViewItem>();
            items.Add(new System.Windows.Forms.ListViewItem("Item1"));
            items.Add(new System.Windows.Forms.ListViewItem("Item2"));
            Exception exception = null;

            feedbackManagerMock.Setup(x => x.DisplayErrorFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()))
                                .Verifiable();

            using (var systemUnderTest = new SchemaWizard())
            {
                systemUnderTest.OrganizationService = serviceMock.Object;
                systemUnderTest.MetadataService = metadataServiceMock.Object;
                systemUnderTest.FeedbackManager = feedbackManagerMock.Object;

                FluentActions.Invoking(() => systemUnderTest.PopulateEntitiesListView(items, exception))
                        .Should()
                        .NotThrow();
            }

            feedbackManagerMock.Verify(x => x.DisplayErrorFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()), Times.Never);
            feedbackManagerMock.Verify(x => x.DisplayWarningFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()), Times.Never);
        }

        [Ignore("Will fix")]
        [TestMethod]
        public void RefreshEntitiesUsedefaultParamater()
        {
            var entityLogicalName = "contact";
            var entityMetadata = new EntityMetadata
            {
                LogicalName = entityLogicalName,
                DisplayName = new Label
                {
                    UserLocalizedLabel = new LocalizedLabel { Label = "Test" }
                }
            };

            InsertAttributeList(entityMetadata);

            var metadataList = new List<EntityMetadata>
            {
                entityMetadata
            };
            metadataServiceMock.Setup(x => x.RetrieveEntities(It.IsAny<IOrganizationService>()))
                                .Returns(metadataList)
                                .Verifiable();

            using (var systemUnderTest = new SchemaWizard())
            {
                FluentActions.Invoking(() => systemUnderTest.RefreshEntities())
                             .Should()
                             .NotThrow();
            }

            metadataServiceMock.VerifyAll();
        }

        [Ignore("Will fix")]
        [TestMethod]
        public void RefreshEntitiesSetParamaterToTrue()
        {
            var entityLogicalName = "contact";
            var entityMetadata = new EntityMetadata
            {
                LogicalName = entityLogicalName,
                DisplayName = new Label
                {
                    UserLocalizedLabel = new LocalizedLabel { Label = "Test" }
                }
            };

            InsertAttributeList(entityMetadata);

            var metadataList = new List<EntityMetadata>
            {
                entityMetadata
            };
            metadataServiceMock.Setup(x => x.RetrieveEntities(It.IsAny<IOrganizationService>()))
                                .Returns(metadataList)
                                .Verifiable();

            using (var systemUnderTest = new SchemaWizard())
            {
                systemUnderTest.OrganizationService = serviceMock.Object;
                systemUnderTest.MetadataService = metadataServiceMock.Object;

                FluentActions.Invoking(() => systemUnderTest.RefreshEntities(true))
                             .Should()
                             .NotThrow();
            }

            metadataServiceMock.VerifyAll();
        }

        [TestMethod]
        public void ClearMemory()
        {
            using (var systemUnderTest = new SchemaWizard())
            {
                systemUnderTest.OrganizationService = serviceMock.Object;
                systemUnderTest.MetadataService = metadataServiceMock.Object;

                FluentActions.Invoking(() => systemUnderTest.ClearMemory())
                             .Should()
                             .NotThrow();
            }
        }

        [TestMethod]
        public void LoadExportConfigFileWithEmptyExportConfigPath()
        {
            string exportConfigFilename = string.Empty;

            feedbackManagerMock.Setup(x => x.DisplayFeedback(It.IsAny<string>()))
                                .Verifiable();

            using (var systemUnderTest = new SchemaWizard())
            {
                systemUnderTest.OrganizationService = serviceMock.Object;
                systemUnderTest.MetadataService = metadataServiceMock.Object;
                systemUnderTest.FeedbackManager = feedbackManagerMock.Object;

                FluentActions.Invoking(() => systemUnderTest.LoadExportConfigFile(exportConfigFilename))
                             .Should()
                             .NotThrow();
            }

            feedbackManagerMock.Verify(x => x.DisplayFeedback(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void LoadExportConfigFileWithInValidExportConfigPath()
        {
            string exportConfigFilename = "hello.txt";

            feedbackManagerMock.Setup(x => x.DisplayFeedback("Invalid Export Config File"))
                                .Verifiable();

            using (var systemUnderTest = new SchemaWizard())
            {
                systemUnderTest.OrganizationService = serviceMock.Object;
                systemUnderTest.MetadataService = metadataServiceMock.Object;
                systemUnderTest.FeedbackManager = feedbackManagerMock.Object;

                FluentActions.Invoking(() => systemUnderTest.LoadExportConfigFile(exportConfigFilename))
                             .Should()
                             .NotThrow();
            }

            feedbackManagerMock.Verify(x => x.DisplayFeedback("Invalid Export Config File"), Times.Once);
        }

        [TestMethod]
        public void LoadExportConfigFileWithValidExportConfigPath()
        {
            string exportConfigFilename = "TestData\\ExportConfig.json";

            feedbackManagerMock.Setup(x => x.DisplayFeedback("Filters and Lookup Mappings loaded from Export Config File"))
                                .Verifiable();

            using (var systemUnderTest = new SchemaWizard())
            {
                systemUnderTest.OrganizationService = serviceMock.Object;
                systemUnderTest.MetadataService = metadataServiceMock.Object;
                systemUnderTest.FeedbackManager = feedbackManagerMock.Object;

                FluentActions.Invoking(() => systemUnderTest.LoadExportConfigFile(exportConfigFilename))
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

            feedbackManagerMock.Setup(x => x.DisplayFeedback("Filters and Lookup Mappings loaded from Export Config File"))
                               .Throws(exception);

            feedbackManagerMock.Setup(x => x.DisplayFeedback($"Load Correct Export Config file, error:{exception.Message}"))
                                .Verifiable();

            using (var systemUnderTest = new SchemaWizard())
            {
                systemUnderTest.OrganizationService = serviceMock.Object;
                systemUnderTest.MetadataService = metadataServiceMock.Object;
                systemUnderTest.FeedbackManager = feedbackManagerMock.Object;

                FluentActions.Invoking(() => systemUnderTest.LoadExportConfigFile(exportConfigFilename))
                             .Should()
                             .NotThrow();
            }

            feedbackManagerMock.Verify(x => x.DisplayFeedback($"Load Correct Export Config file, error:{exception.Message}"), Times.Once);
        }

        [TestMethod]
        public void LoadSchemaFileWithEmptyExportConfigPath()
        {
            string schemaFilename = string.Empty;

            feedbackManagerMock.Setup(x => x.DisplayFeedback(It.IsAny<string>()))
                                .Verifiable();

            using (var systemUnderTest = new SchemaWizard())
            {
                systemUnderTest.OrganizationService = serviceMock.Object;
                systemUnderTest.MetadataService = metadataServiceMock.Object;
                systemUnderTest.FeedbackManager = feedbackManagerMock.Object;

                FluentActions.Invoking(() => systemUnderTest.LoadSchemaFile(schemaFilename, workingstate))
                             .Should()
                             .NotThrow();
            }

            feedbackManagerMock.Verify(x => x.DisplayFeedback(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void LoadSchemaFileWithInValidPath()
        {
            string configFilename = "hello.txt";

            feedbackManagerMock.Setup(x => x.DisplayFeedback(It.IsAny<string>()))
                               .Verifiable();

            using (var systemUnderTest = new SchemaWizard())
            {
                systemUnderTest.OrganizationService = serviceMock.Object;
                systemUnderTest.MetadataService = metadataServiceMock.Object;
                systemUnderTest.FeedbackManager = feedbackManagerMock.Object;

                FluentActions.Invoking(() => systemUnderTest.LoadSchemaFile(configFilename, workingstate))
                             .Should()
                             .NotThrow();
            }

            feedbackManagerMock.Verify(x => x.DisplayFeedback(It.IsAny<string>()), Times.Once);
        }

        //[Ignore("Will fix")]
        [TestMethod]
        public void LoadSchemaFileWithValidPath()
        {
            string configFilename = "TestData\\testschemafile.xml";
            //feedbackManagerMock.Setup(x => x.DisplayFeedback("Filters and Lookup Mappings loaded from Export Config File"))
            //                   .Verifiable();
            //var sourceList = metadataService.RetrieveEntities(organizationService);

            var entityLogicalName = "account";
            var entityMetadata = new EntityMetadata
            {
                LogicalName = entityLogicalName,
                DisplayName = new Label
                {
                    UserLocalizedLabel = new LocalizedLabel { Label = "Test" }
                }
            };

            InsertAttributeList(entityMetadata);

            var metadataList = new List<EntityMetadata>
            {
                entityMetadata
            };

            //var sourceList = metadataService.RetrieveEntities(organizationService);
            //metadataServiceMock.Setup(x => x.RetrieveEntities(It.IsAny<IOrganizationService>()))
            //                    .Returns(metadataList)
            //                   .Verifiable();

            workingstate = true;

            using (var systemUnderTest = new SchemaWizard())
            {
                systemUnderTest.OrganizationService = serviceMock.Object;
                systemUnderTest.MetadataService = metadataServiceMock.Object;
                systemUnderTest.FeedbackManager = feedbackManagerMock.Object;

                FluentActions.Invoking(() => systemUnderTest.LoadSchemaFile(configFilename, workingstate))
                             .Should()
                             .NotThrow();
            }
            /*
             Moq.MockException: The following setups on mock 'Mock<Capgemini.Xrm.DataMigration.XrmToolBox.Services.IMetadataService:0000001e>' were not matched:
    IMetadataService x => x.RetrieveEntities(It.IsAny<IOrganizationService>())
             */
            //metadataServiceMock.Verify(x => x.RetrieveEntities(It.IsAny<IOrganizationService>()), Times.AtLeastOnce);

            //metadataServiceMock.VerifyAll();

            //feedbackManagerMock.Verify(x => x.DisplayFeedback("Filters and Lookup Mappings loaded from Export Config File"), Times.Once);
        }

        //[TestMethod]
        //public void AsyncRunnerCompleteAttributeOperationExceptionIsNull()
        //{
        //    //List<ListViewItem> items, Exception exception
        //    var items = new List<System.Windows.Forms.ListViewItem>();
        //    items.Add(new System.Windows.Forms.ListViewItem("Item1"));
        //    items.Add(new System.Windows.Forms.ListViewItem("Item2"));
        //    Exception exception = null;

        //    feedbackManagerMock.Setup(x => x.DisplayErrorFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()))
        //        .Verifiable();

        //    using (var systemUnderTest = new SchemaWizard())
        //    {
        //        systemUnderTest.OrganizationService = serviceMock.Object;
        //        systemUnderTest.MetadataService = metadataServiceMock.Object;
        //        systemUnderTest.FeedbackManager = feedbackManagerMock.Object;

        //        //AsyncRunnerCompleteAttributeOperation(List<ListViewItem> items, Exception exception)
        //        FluentActions.Invoking(() => systemUnderTest.AsyncRunnerCompleteAttributeOperation(items, exception))
        //                     .Should()
        //                     .NotThrow();
        //    }

        //    feedbackManagerMock.Verify(x => x.DisplayErrorFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()), Times.Never);
        //}

        //[TestMethod]
        //public void AsyncRunnerCompleteAttributeOperationExceptionIsNotNull()
        //{
        //    //List<ListViewItem> items, Exception exception
        //    var items = new List<System.Windows.Forms.ListViewItem>();
        //    items.Add(new System.Windows.Forms.ListViewItem("Item1"));
        //    items.Add(new System.Windows.Forms.ListViewItem("Item2"));
        //    Exception exception = new Exception();

        //    feedbackManagerMock.Setup(x => x.DisplayErrorFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()))
        //        .Verifiable();

        //    using (var systemUnderTest = new SchemaWizard())
        //    {
        //        systemUnderTest.OrganizationService = serviceMock.Object;
        //        systemUnderTest.MetadataService = metadataServiceMock.Object;
        //        systemUnderTest.FeedbackManager = feedbackManagerMock.Object;

        //        //AsyncRunnerCompleteAttributeOperation(List<ListViewItem> items, Exception exception)
        //        FluentActions.Invoking(() => systemUnderTest.AsyncRunnerCompleteAttributeOperation(items, exception))
        //                     .Should()
        //                     .NotThrow();
        //    }

        //    feedbackManagerMock.VerifyAll();
        //}

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

            using (var systemUnderTest = new SchemaWizard())
            {
                FluentActions.Invoking(() => systemUnderTest.UpdateAttributeMetadataCheckBoxes(relationship.IntersectEntityName, item, inputEntityRelationships, "Fake Logical name"))
                             .Should()
                             .NotThrow();
            }

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

            using (var systemUnderTest = new SchemaWizard())
            {
                FluentActions.Invoking(() => systemUnderTest.UpdateAttributeMetadataCheckBoxes(relationship.IntersectEntityName, item, inputEntityRelationships, inputEntityLogicalName))
                             .Should()
                             .NotThrow();
            }

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

            using (var systemUnderTest = new SchemaWizard())
            {
                FluentActions.Invoking(() => systemUnderTest.UpdateAttributeMetadataCheckBoxes(relationship.IntersectEntityName, item, inputEntityRelationships, inputEntityLogicalName))
                             .Should()
                             .NotThrow();
            }

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

            using (var systemUnderTest = new SchemaWizard())
            {
                FluentActions.Invoking(() => systemUnderTest.UpdateAttributeMetadataCheckBoxes(relationship.IntersectEntityName, item, inputEntityRelationships, inputEntityLogicalName))
                             .Should()
                             .NotThrow();
            }

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

            using (var systemUnderTest = new SchemaWizard())
            {
                FluentActions.Invoking(() => systemUnderTest.UpdateAttributeMetadataCheckBoxes(relationship.IntersectEntityName, item, inputEntityRelationships, null))
                             .Should()
                             .Throw<ArgumentNullException>();
            }
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

            using (var systemUnderTest = new SchemaWizard())
            {
                FluentActions.Invoking(() => systemUnderTest.UpdateAttributeMetadataCheckBoxes(relationship.IntersectEntityName, item, inputEntityRelationships, "Random Text"))
                             .Should()
                             .NotThrow();
            }
        }

        [TestMethod]
        public void ProcessAllAttributeMetadata()
        {
            string entityLogicalName = "account_contact";
            List<string> unmarkedattributes = new List<string>();
            //ist<System.Windows.Forms.ListViewItem> sourceAttributesList = new List<System.Windows.Forms.ListViewItem>();
            //AttributeMetadata[] attributes;

            var attributeList = new List<AttributeMetadata>()
            {
                new AttributeMetadata {
                    LogicalName = "contactattnoentity1" ,
                    DisplayName = new Label
                    {
                        UserLocalizedLabel = new LocalizedLabel { Label = "Test" }
                    }
                }
            };

            /*
              var name = attribute.DisplayName.UserLocalizedLabel == null ? string.Empty : attribute.DisplayName.UserLocalizedLabel.Label;
                var typename = attribute.AttributeTypeName == null ? string.Empty : attribute.AttributeTypeName.Value;
             */

            using (var systemUnderTest = new SchemaWizard())
            {
                //ProcessAllAttributeMetadata(List<string> unmarkedattributes, List<ListViewItem> sourceAttributesList, AttributeMetadata[] attributes)
                var actual = systemUnderTest.ProcessAllAttributeMetadata(unmarkedattributes, attributeList.ToArray(), entityLogicalName);

                actual.Count.Should().BeGreaterThan(0);
            }
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

            using (var systemUnderTest = new SchemaWizard())
            {
                systemUnderTest.OrganizationService = serviceMock.Object;
                systemUnderTest.MetadataService = metadataServiceMock.Object;
                systemUnderTest.FeedbackManager = feedbackManagerMock.Object;

                FluentActions.Invoking(() => systemUnderTest.OnPopulateCompletedAction(eventArgs))
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

            using (var systemUnderTest = new SchemaWizard())
            {
                systemUnderTest.OrganizationService = serviceMock.Object;
                systemUnderTest.MetadataService = metadataServiceMock.Object;
                systemUnderTest.FeedbackManager = feedbackManagerMock.Object;

                FluentActions.Invoking(() => systemUnderTest.OnPopulateCompletedAction(eventArgs))
                             .Should()
                             .NotThrow();
            }

            feedbackManagerMock.Verify(x => x.DisplayErrorFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void FilterAttributes()
        {
            var entityMetadata = new EntityMetadata();
            InsertAttributeList(entityMetadata);
            bool showSystemAttributes = true;

            using (var systemUnderTest = new SchemaWizard())
            {
                systemUnderTest.OrganizationService = serviceMock.Object;
                systemUnderTest.MetadataService = metadataServiceMock.Object;
                systemUnderTest.FeedbackManager = feedbackManagerMock.Object;

                var actual = systemUnderTest.FilterAttributes(entityMetadata, showSystemAttributes);

                actual.Length.Should().Be(entityMetadata.Attributes.Length);
            }
        }

        [TestMethod]
        public void FilterAttributesHideSystemAttributes()
        {
            var entityMetadata = new EntityMetadata();
            InsertAttributeList(entityMetadata);
            bool showSystemAttributes = false;

            using (var systemUnderTest = new SchemaWizard())
            {
                systemUnderTest.OrganizationService = serviceMock.Object;
                systemUnderTest.MetadataService = metadataServiceMock.Object;
                systemUnderTest.FeedbackManager = feedbackManagerMock.Object;

                var actual = systemUnderTest.FilterAttributes(entityMetadata, showSystemAttributes);

                actual.Length.Should().Be(0);
            }
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

            using (var systemUnderTest = new SchemaWizard())
            {
                FluentActions.Invoking(() => systemUnderTest.InvalidUpdate(attribute, item))
                             .Should()
                             .NotThrow();
            }

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

            using (var systemUnderTest = new SchemaWizard())
            {
                FluentActions.Invoking(() => systemUnderTest.InvalidUpdate(attribute, item))
                             .Should()
                             .NotThrow();
            }

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

            using (var systemUnderTest = new SchemaWizard())
            {
                FluentActions.Invoking(() => systemUnderTest.InvalidUpdate(attribute, item))
                             .Should()
                             .NotThrow();
            }

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

            using (var systemUnderTest = new SchemaWizard())
            {
                FluentActions.Invoking(() => systemUnderTest.InvalidUpdate(attribute, item))
                             .Should()
                             .NotThrow();
            }

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

            using (var systemUnderTest = new SchemaWizard())
            {
                FluentActions.Invoking(() => systemUnderTest.InvalidUpdate(attribute, item))
                             .Should()
                             .NotThrow();
            }

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

            using (var systemUnderTest = new SchemaWizard())
            {
                FluentActions.Invoking(() => systemUnderTest.InvalidUpdate(attribute, item))
                             .Should()
                             .NotThrow();
            }

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

            using (var systemUnderTest = new SchemaWizard())
            {
                FluentActions.Invoking(() => systemUnderTest.InvalidUpdate(attribute, item))
                             .Should()
                             .NotThrow();
            }

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

            using (var systemUnderTest = new SchemaWizard())
            {
                FluentActions.Invoking(() => systemUnderTest.InvalidUpdate(attribute, item))
                             .Should()
                             .NotThrow();
            }

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

            using (var systemUnderTest = new SchemaWizard())
            {
                FluentActions.Invoking(() => systemUnderTest.InvalidUpdate(attribute, item))
                             .Should()
                             .NotThrow();
            }

            item.ToolTipText.Should().Contain("DeprecatedVersion:");
        }

        [TestMethod]
        public void PopulateAttributeList()
        {
            string entityLogicalName = "contact";
            List<string> unmarkedattributes = null;
            AttributeMetadata[] attributes = null;
            var entityMetadata = new EntityMetadata();

            metadataServiceMock.Setup(x => x.RetrieveEntities(It.IsAny<string>(), It.IsAny<IOrganizationService>()))
                                .Returns(entityMetadata)
                                .Verifiable();

            using (var systemUnderTest = new SchemaWizard())
            {
                systemUnderTest.Settings = new Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core.Settings();

                attributes = systemUnderTest.PopulateAttributeList(entityLogicalName, serviceMock.Object, metadataServiceMock.Object, unmarkedattributes);
            }

            unmarkedattributes.Should().BeNull();
            attributes.Should().BeNull();
        }

        [TestMethod]
        public void PopulateAttributeListMetaDataServiceReturnsEnities()
        {
            string entityLogicalName = "contact";
            List<string> unmarkedattributes = null;
            AttributeMetadata[] attributes = null;

            var entityMetadata = new EntityMetadata();
            InsertAttributeList(entityMetadata);

            metadataServiceMock.Setup(x => x.RetrieveEntities(It.IsAny<string>(), It.IsAny<IOrganizationService>()))
                                .Returns(entityMetadata)
                                .Verifiable();

            using (var systemUnderTest = new SchemaWizard())
            {
                systemUnderTest.Settings = new Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core.Settings();

                attributes = systemUnderTest.PopulateAttributeList(entityLogicalName, serviceMock.Object, metadataServiceMock.Object, unmarkedattributes);
            }

            unmarkedattributes.Should().BeNull();
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

            using (var systemUnderTest = new SchemaWizard())
            {
                FluentActions.Invoking(() => systemUnderTest.IsInvalidForCustomization(entity, item))
                             .Should()
                             .NotThrow();
            }

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

            using (var systemUnderTest = new SchemaWizard())
            {
                FluentActions.Invoking(() => systemUnderTest.IsInvalidForCustomization(entity, item))
                             .Should()
                             .NotThrow();
            }

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
            //var isLogicalEntityField = entity.GetType().GetRuntimeFields().First(a => a.Name == "_isIntersect");
            //isLogicalEntityField.SetValue(entity, null);

            var item = new System.Windows.Forms.ListViewItem("Item1");

            using (var systemUnderTest = new SchemaWizard())
            {
                FluentActions.Invoking(() => systemUnderTest.IsInvalidForCustomization(entity, item))
                             .Should()
                             .NotThrow();
            }

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

            using (var systemUnderTest = new SchemaWizard())
            {
                FluentActions.Invoking(() => systemUnderTest.IsInvalidForCustomization(entity, item))
                             .Should()
                             .NotThrow();
            }

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
            //var isLogicalEntityField = entity.GetType().GetRuntimeFields().First(a => a.Name == "_isIntersect");
            //isLogicalEntityField.SetValue(entity, null);

            var item = new System.Windows.Forms.ListViewItem("Item1");

            using (var systemUnderTest = new SchemaWizard())
            {
                FluentActions.Invoking(() => systemUnderTest.IsInvalidForCustomization(entity, item))
                             .Should()
                             .NotThrow();
            }

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

            using (var systemUnderTest = new SchemaWizard())
            {
                systemUnderTest.FeedbackManager = feedbackManagerMock.Object;

                FluentActions.Invoking(() => systemUnderTest.HandleMappingControlItemClick(inputEntityLogicalName, listViewItemIsSelected, inputMapping, inputMapper, true))
                             .Should()
                             .NotThrow();
            }

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

            using (var systemUnderTest = new SchemaWizard())
            {
                systemUnderTest.Settings = new Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core.Settings();
                systemUnderTest.FeedbackManager = feedbackManagerMock.Object;

                FluentActions.Invoking(() => systemUnderTest.HandleMappingControlItemClick(inputEntityLogicalName, listViewItemIsSelected, inputMapping, inputMapper, true))
                             .Should()
                             .NotThrow();
            }

            feedbackManagerMock.Verify(x => x.DisplayFeedback(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void HandleMappingControlItemClickListViewItemSelectedIsTrueAndFilterContainsEntityLogicalName()
        {
            string inputEntityLogicalName = "contact";
            bool listViewItemIsSelected = true;

            var entityReference = new EntityReference(inputEntityLogicalName, Guid.NewGuid());

            var mappingItem = new List<DataMigration.XrmToolBoxPlugin.Core.Item<EntityReference, EntityReference>>();
            mappingItem.Add(new DataMigration.XrmToolBoxPlugin.Core.Item<EntityReference, EntityReference>(entityReference, entityReference));

            var inputMapping = new Dictionary<string, List<Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core.Item<EntityReference, EntityReference>>>();
            inputMapping.Add(inputEntityLogicalName, mappingItem);

            var inputMapper = new Dictionary<string, Dictionary<Guid, Guid>>();

            feedbackManagerMock.Setup(x => x.DisplayFeedback(It.IsAny<string>()))
                               .Verifiable();

            using (var systemUnderTest = new SchemaWizard())
            {
                systemUnderTest.Settings = new Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core.Settings();
                systemUnderTest.FeedbackManager = feedbackManagerMock.Object;

                FluentActions.Invoking(() => systemUnderTest.HandleMappingControlItemClick(inputEntityLogicalName, listViewItemIsSelected, inputMapping, inputMapper, true))
                             .Should()
                             .NotThrow();
            }

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

            using (var systemUnderTest = new SchemaWizard())
            {
                systemUnderTest.FeedbackManager = feedbackManagerMock.Object;

                using (var filterDialog = new FilterEditor(null, System.Windows.Forms.FormStartPosition.CenterParent, true))
                {
                    FluentActions.Invoking(() => systemUnderTest.ProcessFilterQuery(inputEntityLogicalName, listViewItemIsSelected, inputFilterQuery, filterDialog))
                             .Should()
                             .NotThrow();
                }
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

            using (var systemUnderTest = new SchemaWizard())
            {
                systemUnderTest.Settings = new Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core.Settings();
                systemUnderTest.FeedbackManager = feedbackManagerMock.Object;

                using (var filterDialog = new FilterEditor(null, System.Windows.Forms.FormStartPosition.CenterParent, true))
                {
                    filterDialog.QueryString = "< filter type =\"and\" > < condition attribute =\"sw_appointmentstatus\" operator=\"eq\" value=\"266880017\" /></ filter >";

                    FluentActions.Invoking(() => systemUnderTest.ProcessFilterQuery(inputEntityLogicalName, listViewItemIsSelected, inputFilterQuery, filterDialog))
                             .Should()
                             .NotThrow();
                }
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

            using (var systemUnderTest = new SchemaWizard())
            {
                systemUnderTest.Settings = new Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core.Settings();
                systemUnderTest.FeedbackManager = feedbackManagerMock.Object;

                var currentfilter = inputFilterQuery[inputEntityLogicalName];

                using (var filterDialog = new FilterEditor(currentfilter, System.Windows.Forms.FormStartPosition.CenterParent, true))
                {
                    filterDialog.QueryString = "< filter type =\"and\" > < condition attribute =\"sw_appointmentstatus\" operator=\"eq\" value=\"266880017\" /></ filter >";

                    FluentActions.Invoking(() => systemUnderTest.ProcessFilterQuery(inputEntityLogicalName, listViewItemIsSelected, inputFilterQuery, filterDialog))
                             .Should()
                             .NotThrow();
                }
            }

            feedbackManagerMock.Verify(x => x.DisplayFeedback(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void ProcessFilterQueryListViewFilterDialogQueryStringIsEmpty()
        {
            string inputEntityLogicalName = "contact";
            bool listViewItemIsSelected = true;
            Dictionary<string, string> inputFilterQuery = new Dictionary<string, string>();

            inputFilterQuery.Add(inputEntityLogicalName, inputEntityLogicalName);

            feedbackManagerMock.Setup(x => x.DisplayFeedback(It.IsAny<string>()))
                               .Verifiable();

            using (var systemUnderTest = new SchemaWizard())
            {
                systemUnderTest.Settings = new Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core.Settings();
                systemUnderTest.FeedbackManager = feedbackManagerMock.Object;

                var currentfilter = inputFilterQuery[inputEntityLogicalName];

                using (var filterDialog = new FilterEditor(currentfilter, System.Windows.Forms.FormStartPosition.CenterParent, true))
                {
                    filterDialog.QueryString = string.Empty;

                    FluentActions.Invoking(() => systemUnderTest.ProcessFilterQuery(inputEntityLogicalName, listViewItemIsSelected, inputFilterQuery, filterDialog))
                             .Should()
                             .NotThrow();
                }
            }

            feedbackManagerMock.Verify(x => x.DisplayFeedback(It.IsAny<string>()), Times.Never);
        }

        private void SetupMockObjects(string entityLogicalName)
        {
            var entityMetadata = new EntityMetadata
            {
                LogicalName = entityLogicalName,
                DisplayName = new Label
                {
                    UserLocalizedLabel = new LocalizedLabel { Label = "Test" }
                }
            };

            InsertAttributeList(entityMetadata);

            var metadataList = new List<EntityMetadata>
            {
                entityMetadata
            };

            metadataServiceMock.Setup(x => x.RetrieveEntities(It.IsAny<string>(), It.IsAny<IOrganizationService>()))
                                .Returns(entityMetadata)
                                .Verifiable();

            metadataServiceMock.Setup(x => x.RetrieveEntities(It.IsAny<IOrganizationService>()))
                                .Returns(metadataList)
                                .Verifiable();
        }

        private static void InsertManyToManyRelationshipMetadata(EntityMetadata entityMetadata)
        {
            var relationship = new ManyToManyRelationshipMetadata
            {
                Entity1LogicalName = "account",
                Entity1IntersectAttribute = "accountid",
                IntersectEntityName = "account_contact",
                Entity2LogicalName = "contact",
                Entity2IntersectAttribute = "contactid"
            };

            var manyToManyRelationshipMetadataList = new List<ManyToManyRelationshipMetadata>
            {
                relationship
            };

            var field = entityMetadata.GetType().GetRuntimeFields().First(a => a.Name == "_manyToManyRelationships");
            field.SetValue(entityMetadata, manyToManyRelationshipMetadataList.ToArray());
        }

        private static void InsertAttributeList(EntityMetadata entityMetadata)
        {
            var attributeList = new List<AttributeMetadata>()
            {
                new AttributeMetadata { LogicalName = "contactattnoentity1" }
            };

            var field = entityMetadata.GetType().GetRuntimeFields().First(a => a.Name == "_attributes");
            field.SetValue(entityMetadata, attributeList.ToArray());

            var isIntersectField = entityMetadata.GetType().GetRuntimeFields().First(a => a.Name == "_isIntersect");
            isIntersectField.SetValue(entityMetadata, (bool?)false);

            var isLogicalEntityField = entityMetadata.GetType().GetRuntimeFields().First(a => a.Name == "_isLogicalEntity");
            isLogicalEntityField.SetValue(entityMetadata, (bool?)false);

            var isCustomEntityField = entityMetadata.GetType().GetRuntimeFields().First(a => a.Name == "_isCustomEntity");
            isCustomEntityField.SetValue(entityMetadata, (bool?)true);
        }
    }
}