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

namespace Capgemini.Xrm.CdsDataMigrator.Tests.Unit.UserControls
{
    [TestClass]
    public class SchemaWizardTests
    {
        private Mock<IOrganizationService> serviceMock;
        private Mock<IMetadataService> metadataServiceMock;
        private Mock<IFeedbackManager> feedbackManagerMock;
        private Dictionary<string, HashSet<string>> inputEntityRelationships;

        [TestInitialize]
        public void Setup()
        {
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
            var items = new List<System.Windows.Forms.ListViewItem>();
            items.Add(new System.Windows.Forms.ListViewItem("Item1"));
            items.Add(new System.Windows.Forms.ListViewItem("Item2"));

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
        public void RetrieveSourceEntitiesListShowSystemAttributes()
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
        public void PopulateRelationship()
        {
            string entityLogicalName = "contact";

            using (var systemUnderTest = new SchemaWizard())
            {
                systemUnderTest.OrganizationService = serviceMock.Object;
                systemUnderTest.MetadataService = metadataServiceMock.Object;

                FluentActions.Invoking(() => systemUnderTest.PopulateRelationship(entityLogicalName, serviceMock.Object, metadataServiceMock.Object, inputEntityRelationships))
                        .Should()
                        .NotThrow();
            }

            serviceMock.VerifyAll();
            metadataServiceMock.VerifyAll();
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

                FluentActions.Invoking(() => systemUnderTest.LoadSchemaFile(schemaFilename))
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

                FluentActions.Invoking(() => systemUnderTest.LoadSchemaFile(configFilename))
                             .Should()
                             .NotThrow();
            }

            feedbackManagerMock.Verify(x => x.DisplayFeedback(It.IsAny<string>()), Times.Once);
        }

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
            metadataServiceMock.Setup(x => x.RetrieveEntities(It.IsAny<IOrganizationService>()))
                                .Returns(metadataList)
                                .Verifiable();

            using (var systemUnderTest = new SchemaWizard())
            {
                systemUnderTest.OrganizationService = serviceMock.Object;
                systemUnderTest.MetadataService = metadataServiceMock.Object;
                systemUnderTest.FeedbackManager = feedbackManagerMock.Object;

                FluentActions.Invoking(() => systemUnderTest.LoadSchemaFile(configFilename))
                             .Should()
                             .NotThrow();
            }

            metadataServiceMock.VerifyAll();
            //feedbackManagerMock.Verify(x => x.DisplayFeedback("Filters and Lookup Mappings loaded from Export Config File"), Times.Once);
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