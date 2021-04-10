using System;
using System.Collections.Generic;
using Capgemini.Xrm.CdsDataMigratorLibrary.Controllers;
using Capgemini.Xrm.CdsDataMigratorLibrary.Core;
using Capgemini.Xrm.CdsDataMigratorLibrary.Exceptions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Moq;

namespace Capgemini.Xrm.CdsDataMigrator.Tests.Unit.Controllers
{
    [TestClass]
    public class SchemaControllerTests : TestBase
    {
        private Dictionary<string, HashSet<string>> inputEntityRelationships;
        private Dictionary<string, HashSet<string>> inputEntityAttributes;
        private bool inputWorkingstate;
        private HashSet<string> inputCheckedEntity;

        private SchemaController systemUnderTest;

        [TestInitialize]
        public void Setup()
        {
            SetupServiceMocks();
            inputEntityRelationships = new Dictionary<string, HashSet<string>>();
            inputEntityAttributes = new Dictionary<string, HashSet<string>>();
            inputWorkingstate = true;
            inputCheckedEntity = new HashSet<string>();

            systemUnderTest = new SchemaController();
        }

        [TestMethod]
        public void SchemaFolderPathActionWithDialogResultCancel()
        {
            using (var fileDialog = new System.Windows.Forms.SaveFileDialog())
            {
                using (var schemaPathTextBox = new System.Windows.Forms.TextBox())
                {
                    var dialogResult = System.Windows.Forms.DialogResult.Cancel;

                    FluentActions.Invoking(() => systemUnderTest.SchemaFolderPathAction(NotificationServiceMock.Object, schemaPathTextBox, inputWorkingstate, inputEntityAttributes, inputEntityRelationships, dialogResult, fileDialog, (x1, x2, x3, x4, x5) => { }))
                                 .Should()
                                 .NotThrow();

                    schemaPathTextBox.Text.Should().BeEmpty();
                }
            }
        }

        [TestMethod]
        public void SchemaFolderPathActionWithDialogResultOk()
        {
            var filename = "TestData\\usersettingsschema.xml";

            using (var fileDialog = new System.Windows.Forms.SaveFileDialog())
            {
                fileDialog.FileName = filename;

                using (var schemaPathTextBox = new System.Windows.Forms.TextBox())
                {
                    var dialogResult = System.Windows.Forms.DialogResult.OK;

                    FluentActions.Invoking(() => systemUnderTest.SchemaFolderPathAction(NotificationServiceMock.Object, schemaPathTextBox, inputWorkingstate, inputEntityAttributes, inputEntityRelationships, dialogResult, fileDialog, (x1, x2, x3, x4, x5) => { }))
                                 .Should()
                                 .NotThrow();

                    schemaPathTextBox.Text.Should().Be(filename);
                }
            }
        }

        [TestMethod]
        public void SchemaFolderPathActionWithDialogResultOkAndNonExistingFile()
        {
            var filename = "TestData\\nonexistingfile.xml";

            using (var fileDialog = new System.Windows.Forms.SaveFileDialog())
            {
                fileDialog.FileName = filename;

                using (var schemaPathTextBox = new System.Windows.Forms.TextBox())
                {
                    var dialogResult = System.Windows.Forms.DialogResult.OK;

                    FluentActions.Invoking(() => systemUnderTest.SchemaFolderPathAction(NotificationServiceMock.Object, schemaPathTextBox, inputWorkingstate, inputEntityAttributes, inputEntityRelationships, dialogResult, fileDialog, (x1, x2, x3, x4, x5) => { }))
                                 .Should()
                                 .NotThrow();

                    schemaPathTextBox.Text.Should().Be(filename);
                }
            }
        }

        [TestMethod]
        public void SaveSchemaNoEntityAttributeSelected()
        {
            NotificationServiceMock.Setup(x => x.DisplayFeedback("Please select at least one attribute for each selected entity!"))
                                   .Verifiable();

            using (var schemaPathTextBox = new System.Windows.Forms.TextBox())
            {
                var serviceParameters = GenerateMigratorParameters();

                var inputAttributeMapping = new AttributeTypeMapping();
                var inputCrmSchemaConfiguration = new DataMigration.Config.CrmSchemaConfiguration();

                FluentActions.Invoking(() => systemUnderTest.SaveSchema(serviceParameters, inputCheckedEntity, inputEntityRelationships, inputEntityAttributes, inputAttributeMapping, inputCrmSchemaConfiguration, schemaPathTextBox))
                             .Should()
                             .NotThrow();
            }

            NotificationServiceMock.Verify(x => x.DisplayFeedback("Please select at least one attribute for each selected entity!"), Times.Once);
        }

        [TestMethod]
        public void SaveSchemaEntityAttributeSelected()
        {
            NotificationServiceMock.Setup(x => x.DisplayFeedback("Please select at least one attribute for each selected entity!"))
                                   .Verifiable();
            var entityLogicalName = "contact";
            var entityMetadata = InstantiateEntityMetaData(entityLogicalName);
            InsertAttributeList(entityMetadata, new List<string> { "contactId", "firstname", "lastname" });

            inputCheckedEntity.Add(entityLogicalName);

            var attributeSet = new HashSet<string>() { "contactId", "firstname", "lastname" };
            inputEntityAttributes.Add(entityLogicalName, attributeSet);
            var inputAttributeMapping = new AttributeTypeMapping();
            var serviceParameters = GenerateMigratorParameters();

            MetadataServiceMock.Setup(x => x.RetrieveEntities(It.IsAny<string>(), It.IsAny<IOrganizationService>(), It.IsAny<IExceptionService>()))
                                .Returns(entityMetadata)
                                .Verifiable();

            using (var schemaPathTextBox = new System.Windows.Forms.TextBox())
            {
                var inputCrmSchemaConfiguration = new DataMigration.Config.CrmSchemaConfiguration();

                FluentActions.Invoking(() => systemUnderTest.SaveSchema(serviceParameters, inputCheckedEntity, inputEntityRelationships, inputEntityAttributes, inputAttributeMapping, inputCrmSchemaConfiguration, schemaPathTextBox))
                             .Should()
                             .NotThrow();
            }

            MetadataServiceMock.VerifyAll();
            NotificationServiceMock.Verify(x => x.DisplayFeedback("Please select at least one attribute for each selected entity!"), Times.Never);
        }

        [TestMethod]
        public void GenerateXMLFileEmptyFilePath()
        {
            using (var tbSchemaPath = new System.Windows.Forms.TextBox())
            {
                var schemaConfiguration = new Capgemini.Xrm.DataMigration.Config.CrmSchemaConfiguration();

                FluentActions.Invoking(() => systemUnderTest.GenerateXMLFile(tbSchemaPath, schemaConfiguration))
                             .Should()
                             .NotThrow();
            }
        }

        [TestMethod]
        public void GenerateXMLFile()
        {
            using (var tbSchemaPath = new System.Windows.Forms.TextBox())
            {
                tbSchemaPath.Text = $"{Guid.NewGuid()}.json";

                var schemaConfiguration = new Capgemini.Xrm.DataMigration.Config.CrmSchemaConfiguration();

                FluentActions.Invoking(() => systemUnderTest.GenerateXMLFile(tbSchemaPath, schemaConfiguration))
                             .Should()
                             .NotThrow();
            }
        }
    }
}