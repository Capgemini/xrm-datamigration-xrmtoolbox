using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Capgemini.Xrm.DataMigration.XrmToolBox.Services;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
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
        public void HandleListViewEntitiesSelectedIndexChanged()
        {
            string inputEntityLogicalName = "account";
            HashSet<string> inputSelectedEntity = new HashSet<string>();

            using (var listView = new System.Windows.Forms.ListView())
            {
                var selectedItems = new System.Windows.Forms.ListView.SelectedListViewItemCollection(listView);

                using (var systemUnderTest = new SchemaWizard())
                {
                    systemUnderTest.OrganizationService = serviceMock.Object;
                    systemUnderTest.MetadataService = metadataServiceMock.Object;

                    FluentActions.Invoking(() => systemUnderTest.HandleListViewEntitiesSelectedIndexChanged(metadataServiceMock.Object, serviceMock.Object, inputEntityRelationships, inputEntityLogicalName, inputSelectedEntity, selectedItems))
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
                systemUnderTest.OrganizationService = serviceMock.Object;
                systemUnderTest.MetadataService = metadataServiceMock.Object;

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

            feedbackManagerMock.Setup(x => x.DisplayFeedback(It.IsAny<string>()))
                                .Verifiable();

            using (var systemUnderTest = new SchemaWizard())
            {
                systemUnderTest.OrganizationService = serviceMock.Object;
                systemUnderTest.MetadataService = metadataServiceMock.Object;
                systemUnderTest.FeedbackManager = feedbackManagerMock.Object;

                FluentActions.Invoking(() => systemUnderTest.LoadSchemaFile(schemaFilename, workingstate, feedbackManagerMock.Object, inputEntityAttributes, inputEntityRelationships))
                             .Should()
                             .NotThrow();
            }

            feedbackManagerMock.Verify(x => x.DisplayFeedback(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void LoadSchemaFileWithInValidPath()
        {
            string configFilename = "hello.txt";

            var inputEntityAttributes = new Dictionary<string, HashSet<string>>();

            feedbackManagerMock.Setup(x => x.DisplayFeedback(It.IsAny<string>()))
                               .Verifiable();

            using (var systemUnderTest = new SchemaWizard())
            {
                systemUnderTest.OrganizationService = serviceMock.Object;
                systemUnderTest.MetadataService = metadataServiceMock.Object;
                systemUnderTest.FeedbackManager = feedbackManagerMock.Object;

                FluentActions.Invoking(() => systemUnderTest.LoadSchemaFile(configFilename, workingstate, feedbackManagerMock.Object, inputEntityAttributes, inputEntityRelationships))
                             .Should()
                             .NotThrow();
            }

            feedbackManagerMock.Verify(x => x.DisplayFeedback(It.IsAny<string>()), Times.Once);
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
                systemUnderTest.OrganizationService = serviceMock.Object;
                systemUnderTest.MetadataService = metadataServiceMock.Object;
                systemUnderTest.FeedbackManager = feedbackManagerMock.Object;

                FluentActions.Invoking(() => systemUnderTest.LoadSchemaFile(configFilename, workingstate, feedbackManagerMock.Object, inputEntityAttributes, inputEntityRelationships))
                             .Should()
                             .NotThrow();
            }
        }

        [TestMethod]
        public void ManageWorkingStateSetWorkingStateToTrue()
        {
            using (var systemUnderTest = new SchemaWizard())
            {
                systemUnderTest.OrganizationService = serviceMock.Object;
                systemUnderTest.MetadataService = metadataServiceMock.Object;
                systemUnderTest.FeedbackManager = feedbackManagerMock.Object;

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
                systemUnderTest.OrganizationService = serviceMock.Object;
                systemUnderTest.MetadataService = metadataServiceMock.Object;
                systemUnderTest.FeedbackManager = feedbackManagerMock.Object;

                FluentActions.Invoking(() => systemUnderTest.ManageWorkingState(false))
                                 .Should()
                                 .NotThrow();
            }
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

            metadataServiceMock.Setup(x => x.RetrieveEntities(It.IsAny<string>(), It.IsAny<IOrganizationService>()))
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