using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Capgemini.Xrm.DataMigration.XrmToolBox.Core;
using Capgemini.Xrm.DataMigration.XrmToolBox.Services;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Forms;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Model;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Moq;

namespace Capgemini.Xrm.CdsDataMigrator.Tests.Unit.UserControls
{
    [TestClass]
    public class SchemaWizardDelegateTests : TestBase
    {
        private Dictionary<string, HashSet<string>> inputEntityRelationships;
        private Dictionary<string, HashSet<string>> inputEntityAttributes;
        private bool inputWorkingstate;
        private HashSet<string> inputCheckedEntity;

        private SchemaWizardDelegate systemUnderTest;

        [TestInitialize]
        public void Setup()
        {
            SetupServiceMocks();
            inputEntityRelationships = new Dictionary<string, HashSet<string>>();
            inputEntityAttributes = new Dictionary<string, HashSet<string>>();
            inputWorkingstate = true;
            inputCheckedEntity = new HashSet<string>();

            systemUnderTest = new SchemaWizardDelegate();
        }

        [TestMethod]
        public void StoreEntityDataNullEntityList()
        {
            FluentActions.Invoking(() => systemUnderTest.StoreEntityData(null, inputEntityAttributes, inputEntityRelationships))
                         .Should()
                         .NotThrow();

            inputEntityAttributes.Count.Should().Be(0);
            inputEntityRelationships.Count.Should().Be(0);
        }

        [TestMethod]
        public void StoreEntityDataNoEntities()
        {
            var crmEntity = new List<DataMigration.Model.CrmEntity>();

            FluentActions.Invoking(() => systemUnderTest.StoreEntityData(crmEntity.ToArray(), inputEntityAttributes, inputEntityRelationships))
                         .Should()
                         .NotThrow();

            inputEntityAttributes.Count.Should().Be(0);
            inputEntityRelationships.Count.Should().Be(0);
        }

        [TestMethod]
        public void StoreEntityDataHasEntitiesDuplicateEntityLogicalName()
        {
            var maxAttributes = 6;
            var maxRelationships = 3;

            var crmEntity = new List<DataMigration.Model.CrmEntity>();

            for (int entityCount = 0; entityCount < 5; entityCount++)
            {
                var entity = new DataMigration.Model.CrmEntity
                {
                    Name = "TestEntity"
                };

                for (int attributeCount = 0; attributeCount < maxAttributes; attributeCount++)
                {
                    entity.CrmFields.Add(new Capgemini.Xrm.DataMigration.Model.CrmField { FieldName = $"FieldName{attributeCount}" });
                }

                for (int relationshipCount = 0; relationshipCount < maxRelationships; relationshipCount++)
                {
                    entity.CrmRelationships.Add(new Capgemini.Xrm.DataMigration.Model.CrmRelationship { RelationshipName = $"RelationshipName{relationshipCount}" });
                }

                crmEntity.Add(entity);
            }

            FluentActions.Invoking(() => systemUnderTest.StoreEntityData(crmEntity.ToArray(), inputEntityAttributes, inputEntityRelationships))
                         .Should()
                         .Throw<ArgumentException>();
        }

        [TestMethod]
        public void StoreEntityDataHasEntities()
        {
            var maxAttributes = 6;
            var maxRelationships = 3;
            var maxEntityCount = 5;
            var index = 3;

            var crmEntity = new List<DataMigration.Model.CrmEntity>();

            for (int entityCount = 0; entityCount < maxEntityCount; entityCount++)
            {
                var entity = new DataMigration.Model.CrmEntity
                {
                    Name = $"TestEntity{entityCount}"
                };

                for (int attributeCount = 0; attributeCount < maxAttributes; attributeCount++)
                {
                    entity.CrmFields.Add(new Capgemini.Xrm.DataMigration.Model.CrmField { FieldName = $"FieldName{attributeCount}" });
                }

                for (int relationshipCount = 0; relationshipCount < maxRelationships; relationshipCount++)
                {
                    entity.CrmRelationships.Add(new Capgemini.Xrm.DataMigration.Model.CrmRelationship { RelationshipName = $"RelationshipName{relationshipCount}" });
                }

                crmEntity.Add(entity);
            }

            FluentActions.Invoking(() => systemUnderTest.StoreEntityData(crmEntity.ToArray(), inputEntityAttributes, inputEntityRelationships))
                         .Should()
                         .NotThrow();

            inputEntityAttributes.Count.Should().Be(maxEntityCount);
            inputEntityRelationships.Count.Should().Be(maxEntityCount);

            inputEntityAttributes[$"TestEntity{index}"].Count.Should().Be(maxAttributes);
            inputEntityRelationships[$"TestEntity{index}"].Count.Should().Be(maxRelationships);
        }

        [TestMethod]
        public void StoreEntityDataHasEntitiesWithNoAttributesAndRelationships()
        {
            var maxAttributes = 0;
            var maxRelationships = 0;
            var maxEntityCount = 5;
            var index = 3;

            var crmEntity = new List<DataMigration.Model.CrmEntity>();

            for (int entityCount = 0; entityCount < maxEntityCount; entityCount++)
            {
                var entity = new DataMigration.Model.CrmEntity
                {
                    Name = $"TestEntity{entityCount}"
                };

                crmEntity.Add(entity);
            }

            FluentActions.Invoking(() => systemUnderTest.StoreEntityData(crmEntity.ToArray(), inputEntityAttributes, inputEntityRelationships))
                         .Should()
                         .NotThrow();

            inputEntityAttributes.Count.Should().Be(maxEntityCount);
            inputEntityRelationships.Count.Should().Be(maxEntityCount);

            inputEntityAttributes[$"TestEntity{index}"].Count.Should().Be(maxAttributes);
            inputEntityRelationships[$"TestEntity{index}"].Count.Should().Be(maxRelationships);
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

                var inputAttributeMapping = new DataMigration.XrmToolBoxPlugin.Core.AttributeTypeMapping();
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

            var attributeSet = new HashSet<string>() { "contactId", "firstname", "lastname" };
            inputEntityAttributes.Add(entityLogicalName, attributeSet);
            var inputAttributeMapping = new DataMigration.XrmToolBoxPlugin.Core.AttributeTypeMapping();
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
        public void SetListViewSortingWithEmptySettings()
        {
            using (var listview = new System.Windows.Forms.ListView())
            {
                int column = 0;
                var inputOrganisationId = Guid.NewGuid().ToString();
                var settings = new Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core.Settings();

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
                var settings = new Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core.Settings();

                var item = new KeyValuePair<Guid, Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core.Organisations>(inputOrganisationId, new DataMigration.XrmToolBoxPlugin.Core.Organisations());
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
                var settings = new Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core.Settings();

                var org = new DataMigration.XrmToolBoxPlugin.Core.Organisations();
                var listItem = new DataMigration.XrmToolBoxPlugin.Core.Item<string, int>(inputOrganisationId.ToString(), 1);
                org.Sortcolumns.Add(listItem);

                var item = new KeyValuePair<Guid, Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core.Organisations>(inputOrganisationId, org);
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

                var settings = new Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core.Settings();

                var org = new DataMigration.XrmToolBoxPlugin.Core.Organisations();
                var listItem = new DataMigration.XrmToolBoxPlugin.Core.Item<string, int>(inputOrganisationId.ToString(), 1);
                org.Sortcolumns.Add(listItem);

                var item = new KeyValuePair<Guid, Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core.Organisations>(inputOrganisationId, org);
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

                var settings = new Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core.Settings();

                var org = new DataMigration.XrmToolBoxPlugin.Core.Organisations();
                var listItem = new DataMigration.XrmToolBoxPlugin.Core.Item<string, int>(inputOrganisationId.ToString(), 1);
                org.Sortcolumns.Add(listItem);

                var item = new KeyValuePair<Guid, Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core.Organisations>(inputOrganisationId, org);
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

                var settings = new Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core.Settings();

                var org = new DataMigration.XrmToolBoxPlugin.Core.Organisations();
                var listItem = new DataMigration.XrmToolBoxPlugin.Core.Item<string, int>(inputOrganisationId.ToString(), 1);
                org.Sortcolumns.Add(listItem);

                var item = new KeyValuePair<Guid, Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core.Organisations>(inputOrganisationId, org);
                settings.Organisations.Add(item);

                FluentActions.Invoking(() => systemUnderTest.SetListViewSorting(listview, column, inputOrganisationId.ToString(), settings))
                             .Should()
                             .NotThrow();

                listview.ListViewItemSorter.Should().NotBeNull();
                listview.Sorting.Should().Be(System.Windows.Forms.SortOrder.Ascending);
            }
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

        [TestMethod]
        public void StoreAttributeIfRequiresKeyCurrentValueIsChecked()
        {
            var entityLogicalName = "contact";
            var attributeLogicalName = "contactid";
            var itemCheckEventArgs = new System.Windows.Forms.ItemCheckEventArgs(0, System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked);

            FluentActions.Invoking(() => systemUnderTest.StoreAttributeIfRequiresKey(attributeLogicalName, itemCheckEventArgs, inputEntityAttributes, entityLogicalName))
                         .Should()
                         .NotThrow();

            inputEntityAttributes.Count.Should().Be(1);
            inputEntityAttributes[entityLogicalName].Count.Should().Be(0);
        }

        [TestMethod]
        public void StoreAttributeIfRequiresKeyCurrentValueIsUnchecked()
        {
            var entityLogicalName = "contact";
            var attributeLogicalName = "contactid";

            var itemCheckEventArgs = new System.Windows.Forms.ItemCheckEventArgs(0, System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked);

            FluentActions.Invoking(() => systemUnderTest.StoreAttributeIfRequiresKey(attributeLogicalName, itemCheckEventArgs, inputEntityAttributes, entityLogicalName))
                         .Should()
                         .NotThrow();

            inputEntityAttributes.Count.Should().Be(1);
            inputEntityAttributes[entityLogicalName].Count.Should().Be(1);
        }

        [TestMethod]
        public void StoreAttriubteIfKeyExistsInputEntityAttributesDoesNotContainEntityLogicalName()
        {
            var entityLogicalName = "contact";
            var attributeLogicalName = "contactid";
            var itemCheckEventArgs = new System.Windows.Forms.ItemCheckEventArgs(0, System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked);

            FluentActions.Invoking(() => systemUnderTest.StoreAttriubteIfKeyExists(attributeLogicalName, itemCheckEventArgs, inputEntityAttributes, entityLogicalName))
                         .Should()
                         .Throw<KeyNotFoundException>();
        }

        [TestMethod]
        public void StoreAttriubteIfKeyExistsCurrentValueIsChecked()
        {
            var entityLogicalName = "contact";
            var attributeLogicalName = "contactid";
            var attributeSet = new HashSet<string>();
            inputEntityAttributes.Add(entityLogicalName, attributeSet);

            var itemCheckEventArgs = new System.Windows.Forms.ItemCheckEventArgs(0, System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked);

            FluentActions.Invoking(() => systemUnderTest.StoreAttriubteIfKeyExists(attributeLogicalName, itemCheckEventArgs, inputEntityAttributes, entityLogicalName))
                         .Should()
                         .NotThrow();

            inputEntityAttributes.Count.Should().Be(1);
            inputEntityAttributes[entityLogicalName].Contains(attributeLogicalName).Should().BeFalse();
        }

        [TestMethod]
        public void StoreAttriubteIfKeyExistsCurrentValueIsCheckedAndAttributeSetAlreadyContainsLogicalName()
        {
            var entityLogicalName = "contact";
            var attributeLogicalName = "contactid";
            var attributeSet = new HashSet<string>() { attributeLogicalName };
            inputEntityAttributes.Add(entityLogicalName, attributeSet);

            var itemCheckEventArgs = new System.Windows.Forms.ItemCheckEventArgs(0, System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked);

            FluentActions.Invoking(() => systemUnderTest.StoreAttriubteIfKeyExists(attributeLogicalName, itemCheckEventArgs, inputEntityAttributes, entityLogicalName))
                         .Should()
                         .NotThrow();

            inputEntityAttributes.Count.Should().Be(1);
            inputEntityAttributes[entityLogicalName].Contains(attributeLogicalName).Should().BeFalse();
        }

        [TestMethod]
        public void StoreAttriubteIfKeyExistsCurrentValueIsUnchecked()
        {
            var entityLogicalName = "contact";
            var attributeLogicalName = "contactid";
            var attributeSet = new HashSet<string>();
            inputEntityAttributes.Add(entityLogicalName, attributeSet);

            var itemCheckEventArgs = new System.Windows.Forms.ItemCheckEventArgs(0, System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked);

            FluentActions.Invoking(() => systemUnderTest.StoreAttriubteIfKeyExists(attributeLogicalName, itemCheckEventArgs, inputEntityAttributes, entityLogicalName))
                         .Should()
                         .NotThrow();

            inputEntityAttributes.Count.Should().Be(1);
            inputEntityAttributes[entityLogicalName].Contains(attributeLogicalName).Should().BeTrue();
        }

        [TestMethod]
        public void StoreRelationshipIfRequiresKeyCurrentValueUnchecked()
        {
            var relationshipLogicalName = "contact_account";
            var inputEntityLogicalName = "contact";

            var itemCheckEventArgs = new System.Windows.Forms.ItemCheckEventArgs(0, System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked);

            FluentActions.Invoking(() => systemUnderTest.StoreRelationshipIfRequiresKey(relationshipLogicalName, itemCheckEventArgs, inputEntityLogicalName, inputEntityRelationships))
                         .Should()
                         .NotThrow();

            inputEntityRelationships[inputEntityLogicalName].Contains(relationshipLogicalName).Should().BeTrue();
        }

        [TestMethod]
        public void StoreRelationshipIfRequiresKeyCurrentValueChecked()
        {
            var relationshipLogicalName = "contact_account";
            var inputEntityLogicalName = "contact";

            var itemCheckEventArgs = new System.Windows.Forms.ItemCheckEventArgs(0, System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked);

            FluentActions.Invoking(() => systemUnderTest.StoreRelationshipIfRequiresKey(relationshipLogicalName, itemCheckEventArgs, inputEntityLogicalName, inputEntityRelationships))
                         .Should()
                         .NotThrow();

            inputEntityRelationships[inputEntityLogicalName].Contains(relationshipLogicalName).Should().BeFalse();
        }

        [TestMethod]
        public void StoreRelationshipIfKeyExistsInputEntityRelationshipsDoesNotContainEntityLogicalName()
        {
            var relationshipLogicalName = "contact_account";
            var inputEntityLogicalName = "contact";

            var itemCheckEventArgs = new System.Windows.Forms.ItemCheckEventArgs(0, System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked);

            FluentActions.Invoking(() => systemUnderTest.StoreRelationshipIfKeyExists(relationshipLogicalName, itemCheckEventArgs, inputEntityLogicalName, inputEntityRelationships))
                         .Should()
                         .Throw<KeyNotFoundException>();
        }

        [TestMethod]
        public void StoreRelationshipIfKeyExistsCurrentValueIsChecked()
        {
            var relationshipLogicalName = "contact_account";
            var inputEntityLogicalName = "contact";

            var relationshipSet = new HashSet<string>();
            inputEntityRelationships.Add(inputEntityLogicalName, relationshipSet);

            var itemCheckEventArgs = new System.Windows.Forms.ItemCheckEventArgs(0, System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked);

            FluentActions.Invoking(() => systemUnderTest.StoreRelationshipIfKeyExists(relationshipLogicalName, itemCheckEventArgs, inputEntityLogicalName, inputEntityRelationships))
                         .Should()
                         .NotThrow();

            inputEntityRelationships[inputEntityLogicalName].Contains(relationshipLogicalName).Should().BeFalse();
        }

        [TestMethod]
        public void StoreRelationshipIfKeyExistsCurrentValueIsCheckedAndRelationshipSetAlreadyContainsLogicalName()
        {
            var relationshipLogicalName = "contact_account";
            var inputEntityLogicalName = "contact";

            var relationshipSet = new HashSet<string>() { relationshipLogicalName };
            inputEntityRelationships.Add(inputEntityLogicalName, relationshipSet);

            var itemCheckEventArgs = new System.Windows.Forms.ItemCheckEventArgs(0, System.Windows.Forms.CheckState.Unchecked, System.Windows.Forms.CheckState.Checked);

            FluentActions.Invoking(() => systemUnderTest.StoreRelationshipIfKeyExists(relationshipLogicalName, itemCheckEventArgs, inputEntityLogicalName, inputEntityRelationships))
                         .Should()
                         .NotThrow();

            inputEntityRelationships[inputEntityLogicalName].Contains(relationshipLogicalName).Should().BeFalse();
        }

        [TestMethod]
        public void StoreRelationshipIfKeyExistsCurrentValueIsUnchecked()
        {
            var relationshipLogicalName = "contact_account";
            var inputEntityLogicalName = "contact";

            var relationshipSet = new HashSet<string>();
            inputEntityRelationships.Add(inputEntityLogicalName, relationshipSet);

            var itemCheckEventArgs = new System.Windows.Forms.ItemCheckEventArgs(0, System.Windows.Forms.CheckState.Checked, System.Windows.Forms.CheckState.Unchecked);

            FluentActions.Invoking(() => systemUnderTest.StoreRelationshipIfKeyExists(relationshipLogicalName, itemCheckEventArgs, inputEntityLogicalName, inputEntityRelationships))
                         .Should()
                         .NotThrow();

            inputEntityRelationships[inputEntityLogicalName].Contains(relationshipLogicalName).Should().BeTrue();
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

            var serviceParameters = GenerateMigratorParameters();

            var actual = systemUnderTest.RetrieveSourceEntitiesList(showSystemAttributes, inputCachedMetadata, inputEntityAttributes, serviceParameters);

            actual.Count.Should().Be(1);
        }

        [TestMethod]
        public void RetrieveSourceEntitiesListShowSystemAttributesIsTrue()
        {
            var showSystemAttributes = true;
            string entityLogicalName = "account_contact";
            SetupMockObjects(entityLogicalName);
            var inputCachedMetadata = new List<EntityMetadata>();
            var serviceParameters = GenerateMigratorParameters();

            var actual = systemUnderTest.RetrieveSourceEntitiesList(showSystemAttributes, inputCachedMetadata, inputEntityAttributes, serviceParameters);

            actual.Count.Should().Be(1);
        }

        [TestMethod]
        public void PopulateRelationshipActionNoManyToManyRelationships()
        {
            string entityLogicalName = "contact";
            var entityMetadata = new EntityMetadata();

            var migratorServiceParameters = GenerateMigratorParameters();

            MetadataServiceMock.Setup(x => x.RetrieveEntities(It.IsAny<string>(), It.IsAny<IOrganizationService>(), It.IsAny<IExceptionService>()))
                .Returns(entityMetadata)
                .Verifiable();

            var actual = systemUnderTest.PopulateRelationshipAction(entityLogicalName, inputEntityRelationships, migratorServiceParameters);

            actual.Count.Should().Be(0);

            ServiceMock.VerifyAll();
            MetadataServiceMock.VerifyAll();
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

            var migratorServiceParameters = GenerateMigratorParameters();

            MetadataServiceMock.Setup(x => x.RetrieveEntities(It.IsAny<string>(), It.IsAny<IOrganizationService>(), It.IsAny<IExceptionService>()))
                .Returns(entityMetadata)
                .Verifiable();

            using (var listView = new System.Windows.Forms.ListView())
            {
                systemUnderTest.PopulateEntitiesListView(items, null, null, listView, NotificationServiceMock.Object);

                var actual = systemUnderTest.PopulateRelationshipAction(entityLogicalName, inputEntityRelationships, migratorServiceParameters);

                actual.Count.Should().BeGreaterThan(0);
            }

            ServiceMock.VerifyAll();
            MetadataServiceMock.VerifyAll();
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

            NotificationServiceMock.Setup(x => x.DisplayErrorFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()))
                                .Verifiable();
            NotificationServiceMock.Setup(x => x.DisplayWarningFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()))
                               .Verifiable();
            using (var listView = new System.Windows.Forms.ListView())
            {
                FluentActions.Invoking(() => systemUnderTest.PopulateEntitiesListView(items, exception, null, listView, NotificationServiceMock.Object))
                        .Should()
                        .NotThrow();
            }

            NotificationServiceMock.Verify(x => x.DisplayErrorFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()), Times.Once);
            NotificationServiceMock.Verify(x => x.DisplayWarningFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void PopulateEntitiesListViewWhenThereIsNoException()
        {
            var items = new List<System.Windows.Forms.ListViewItem>();
            Exception exception = null;

            NotificationServiceMock.Setup(x => x.DisplayWarningFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()))
                                .Verifiable();
            using (var listView = new System.Windows.Forms.ListView())
            {
                FluentActions.Invoking(() => systemUnderTest.PopulateEntitiesListView(items, exception, null, listView, NotificationServiceMock.Object))
                        .Should()
                        .NotThrow();
            }

            NotificationServiceMock.Verify(x => x.DisplayErrorFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()), Times.Never);
            NotificationServiceMock.Verify(x => x.DisplayWarningFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()), Times.Once);
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

            NotificationServiceMock.Setup(x => x.DisplayErrorFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()))
                                .Verifiable();

            using (var listView = new System.Windows.Forms.ListView())
            {
                FluentActions.Invoking(() => systemUnderTest.PopulateEntitiesListView(items, exception, null, listView, NotificationServiceMock.Object))
                        .Should()
                        .NotThrow();
            }

            NotificationServiceMock.Verify(x => x.DisplayErrorFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()), Times.Never);
            NotificationServiceMock.Verify(x => x.DisplayWarningFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void LoadExportConfigFileWithEmptyExportConfigPath()
        {
            string exportConfigFilename = string.Empty;
            var inputFilterQuery = new Dictionary<string, string>();
            var inputLookupMaping = new Dictionary<string, Dictionary<string, List<string>>>();

            NotificationServiceMock.Setup(x => x.DisplayFeedback(It.IsAny<string>()))
                                .Verifiable();

            using (System.Windows.Forms.TextBox exportConfigTextBox = new System.Windows.Forms.TextBox())
            {
                exportConfigTextBox.Text = exportConfigFilename;

                FluentActions.Invoking(() => systemUnderTest.LoadExportConfigFile(NotificationServiceMock.Object, exportConfigTextBox, inputFilterQuery, inputLookupMaping))
                             .Should()
                             .NotThrow();
            }

            NotificationServiceMock.Verify(x => x.DisplayFeedback(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void LoadExportConfigFileWithInValidExportConfigPath()
        {
            string exportConfigFilename = "hello.txt";
            var inputFilterQuery = new Dictionary<string, string>();
            var inputLookupMaping = new Dictionary<string, Dictionary<string, List<string>>>();

            NotificationServiceMock.Setup(x => x.DisplayFeedback("Invalid Export Config File"))
                                .Verifiable();

            using (System.Windows.Forms.TextBox exportConfigTextBox = new System.Windows.Forms.TextBox())
            {
                exportConfigTextBox.Text = exportConfigFilename;

                FluentActions.Invoking(() => systemUnderTest.LoadExportConfigFile(NotificationServiceMock.Object, exportConfigTextBox, inputFilterQuery, inputLookupMaping))
                             .Should()
                             .NotThrow();
            }

            NotificationServiceMock.Verify(x => x.DisplayFeedback("Invalid Export Config File"), Times.Once);
        }

        [TestMethod]
        public void LoadExportConfigFileWithValidExportConfigPath()
        {
            string exportConfigFilename = "TestData\\ExportConfig.json";
            var inputFilterQuery = new Dictionary<string, string>();
            var inputLookupMaping = new Dictionary<string, Dictionary<string, List<string>>>();

            NotificationServiceMock.Setup(x => x.DisplayFeedback("Filters and Lookup Mappings loaded from Export Config File"))
                                .Verifiable();

            using (System.Windows.Forms.TextBox exportConfigTextBox = new System.Windows.Forms.TextBox())
            {
                exportConfigTextBox.Text = exportConfigFilename;

                FluentActions.Invoking(() => systemUnderTest.LoadExportConfigFile(NotificationServiceMock.Object, exportConfigTextBox, inputFilterQuery, inputLookupMaping))
                             .Should()
                             .NotThrow();
            }

            NotificationServiceMock.Verify(x => x.DisplayFeedback("Filters and Lookup Mappings loaded from Export Config File"), Times.Once);
        }

        [TestMethod]
        public void LoadExportConfigFileThrowsException()
        {
            string exportConfigFilename = "TestData\\ExportConfig.json";
            var exception = new Exception("TestException here!");
            var inputFilterQuery = new Dictionary<string, string>();
            var inputLookupMaping = new Dictionary<string, Dictionary<string, List<string>>>();

            NotificationServiceMock.Setup(x => x.DisplayFeedback("Filters and Lookup Mappings loaded from Export Config File"))
                               .Throws(exception);

            NotificationServiceMock.Setup(x => x.DisplayFeedback($"Load Correct Export Config file, error:{exception.Message}"))
                                .Verifiable();

            using (System.Windows.Forms.TextBox exportConfigTextBox = new System.Windows.Forms.TextBox())
            {
                exportConfigTextBox.Text = exportConfigFilename;

                FluentActions.Invoking(() => systemUnderTest.LoadExportConfigFile(NotificationServiceMock.Object, exportConfigTextBox, inputFilterQuery, inputLookupMaping))
                             .Should()
                             .NotThrow();
            }

            NotificationServiceMock.Verify(x => x.DisplayFeedback($"Load Correct Export Config file, error:{exception.Message}"), Times.Once);
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

            NotificationServiceMock.Setup(x => x.DisplayErrorFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()))
                                .Verifiable();
            using (var listView = new System.Windows.Forms.ListView())
            {
                FluentActions.Invoking(() => systemUnderTest.OnPopulateCompletedAction(eventArgs, NotificationServiceMock.Object, null, listView))
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
            var result = new List<System.Windows.Forms.ListViewItem>();

            var eventArgs = new System.ComponentModel.RunWorkerCompletedEventArgs(result, exception, cancelled);

            NotificationServiceMock.Setup(x => x.DisplayErrorFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()))
                                .Verifiable();
            using (var listView = new System.Windows.Forms.ListView())
            {
                FluentActions.Invoking(() => systemUnderTest.OnPopulateCompletedAction(eventArgs, NotificationServiceMock.Object, null, listView))
                             .Should()
                             .NotThrow();
            }

            NotificationServiceMock.Verify(x => x.DisplayErrorFeedback(It.IsAny<System.Windows.Forms.IWin32Window>(), It.IsAny<string>()), Times.Never);
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
            var serviceParameters = GenerateMigratorParameters();

            MetadataServiceMock.Setup(x => x.RetrieveEntities(It.IsAny<string>(), It.IsAny<IOrganizationService>(), It.IsAny<IExceptionService>()))
                                .Returns(entityMetadata)
                                .Verifiable();

            attributes = systemUnderTest.GetAttributeList(entityLogicalName, showSystemAttributes, serviceParameters);

            attributes.Should().BeNull();
        }

        [TestMethod]
        public void GetAttributeListMetaDataServiceReturnsEnities()
        {
            string entityLogicalName = "contact";

            AttributeMetadata[] attributes = null;
            bool showSystemAttributes = true;

            var serviceParameters = GenerateMigratorParameters();

            var entityMetadata = new EntityMetadata();
            InsertAttributeList(entityMetadata, new List<string> { "contactattnoentity1" });

            MetadataServiceMock.Setup(x => x.RetrieveEntities(It.IsAny<string>(), It.IsAny<IOrganizationService>(), It.IsAny<IExceptionService>()))
                                .Returns(entityMetadata)
                                .Verifiable();

            attributes = systemUnderTest.GetAttributeList(entityLogicalName, showSystemAttributes, serviceParameters);

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
            var inputMapping = new Dictionary<string, List<Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core.Item<EntityReference, EntityReference>>>();
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

            var mappingItem = new List<DataMigration.XrmToolBoxPlugin.Core.Item<EntityReference, EntityReference>>
            {
                new DataMigration.XrmToolBoxPlugin.Core.Item<EntityReference, EntityReference>(entityReference, entityReference)
            };

            var inputMapping = new Dictionary<string, List<Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core.Item<EntityReference, EntityReference>>>
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
            Dictionary<string, string> inputFilterQuery = new Dictionary<string, string>();

            inputFilterQuery.Add(inputEntityLogicalName, inputEntityLogicalName);

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
        public void LoadImportConfigFileWithNoImportConfig()
        {
            var inputMapper = new Dictionary<string, Dictionary<Guid, Guid>>();
            var inputMapping = new Dictionary<string, List<Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core.Item<EntityReference, EntityReference>>>();

            NotificationServiceMock.Setup(x => x.DisplayFeedback(It.IsAny<string>()))
                                .Verifiable();

            using (var importConfig = new System.Windows.Forms.TextBox())
            {
                FluentActions.Invoking(() => systemUnderTest.LoadImportConfigFile(NotificationServiceMock.Object, importConfig, inputMapper, inputMapping))
                                 .Should()
                                 .NotThrow();
            }

            NotificationServiceMock.Verify(x => x.DisplayFeedback(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void LoadImportConfigFileWithInvalidImportConfigFilePath()
        {
            var inputMapper = new Dictionary<string, Dictionary<Guid, Guid>>();
            var inputMapping = new Dictionary<string, List<Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core.Item<EntityReference, EntityReference>>>();

            NotificationServiceMock.Setup(x => x.DisplayFeedback("Invalid Import Config File"))
                                .Verifiable();

            using (var importConfig = new System.Windows.Forms.TextBox())
            {
                importConfig.Text = "Hello.txt";

                FluentActions.Invoking(() => systemUnderTest.LoadImportConfigFile(NotificationServiceMock.Object, importConfig, inputMapper, inputMapping))
                                .Should()
                                .NotThrow();
            }

            NotificationServiceMock.Verify(x => x.DisplayFeedback(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void LoadImportConfigFileWithValidImportConfigFilePathButNoMigrationConfig()
        {
            var inputMapper = new Dictionary<string, Dictionary<Guid, Guid>>();
            var inputMapping = new Dictionary<string, List<Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core.Item<EntityReference, EntityReference>>>();

            NotificationServiceMock.Setup(x => x.DisplayFeedback("Invalid Import Config File"))
                                .Verifiable();

            using (var importConfig = new System.Windows.Forms.TextBox())
            {
                importConfig.Text = "TestData\\ImportConfig.json";

                FluentActions.Invoking(() => systemUnderTest.LoadImportConfigFile(NotificationServiceMock.Object, importConfig, inputMapper, inputMapping))
                                 .Should()
                                 .NotThrow();
            }

            NotificationServiceMock.Verify(x => x.DisplayFeedback(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void LoadImportConfigFileWithValidImportConfigFilePathAndMigrationConfig()
        {
            var inputMapper = new Dictionary<string, Dictionary<Guid, Guid>>();
            var inputMapping = new Dictionary<string, List<Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core.Item<EntityReference, EntityReference>>>();

            NotificationServiceMock.Setup(x => x.DisplayFeedback("Guid Id Mappings loaded from Import Config File"))
                                .Verifiable();

            using (var importConfig = new System.Windows.Forms.TextBox())
            {
                importConfig.Text = "TestData/ImportConfig2.json";

                FluentActions.Invoking(() => systemUnderTest.LoadImportConfigFile(NotificationServiceMock.Object, importConfig, inputMapper, inputMapping))
                                 .Should()
                                 .NotThrow();
            }

            NotificationServiceMock.Verify(x => x.DisplayFeedback(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void LoadImportConfigFileHandleException()
        {
            var inputMapper = new Dictionary<string, Dictionary<Guid, Guid>>();
            var inputMapping = new Dictionary<string, List<Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core.Item<EntityReference, EntityReference>>>();

            NotificationServiceMock.Setup(x => x.DisplayFeedback("Guid Id Mappings loaded from Import Config File"))
                .Throws<Exception>();

            using (var importConfig = new System.Windows.Forms.TextBox())
            {
                importConfig.Text = "TestData/ImportConfig2.json";

                FluentActions.Invoking(() => systemUnderTest.LoadImportConfigFile(NotificationServiceMock.Object, importConfig, inputMapper, inputMapping))
                                 .Should()
                                 .NotThrow();
            }

            NotificationServiceMock.Verify(x => x.DisplayFeedback(It.IsAny<string>()), Times.Exactly(2));
        }

        [TestMethod]
        public void AreCrmEntityFieldsSelectedCheckedEntityCountIsZero()
        {
            var inputAttributeMapping = new DataMigration.XrmToolBoxPlugin.Core.AttributeTypeMapping();
            var serviceParameters = GenerateMigratorParameters();

            var actual = systemUnderTest.AreCrmEntityFieldsSelected(inputCheckedEntity, inputEntityRelationships, inputEntityAttributes, inputAttributeMapping, serviceParameters);

            actual.Should().BeFalse();
        }

        [TestMethod]
        public void AreCrmEntityFieldsSelected()
        {
            var entityLogicalName = "contact";
            inputCheckedEntity.Add(entityLogicalName);
            var entityMetadata = InstantiateEntityMetaData(entityLogicalName);
            InsertAttributeList(entityMetadata, new List<string> { "contactId", "firstname", "lastname" });

            var attributeSet = new HashSet<string>() { "contactId", "firstname", "lastname" };
            inputEntityAttributes.Add(entityLogicalName, attributeSet);
            var inputAttributeMapping = new DataMigration.XrmToolBoxPlugin.Core.AttributeTypeMapping();
            var serviceParameters = GenerateMigratorParameters();

            MetadataServiceMock.Setup(x => x.RetrieveEntities(It.IsAny<string>(), It.IsAny<IOrganizationService>(), It.IsAny<IExceptionService>()))
                                .Returns(entityMetadata)
                                .Verifiable();

            var actual = systemUnderTest.AreCrmEntityFieldsSelected(inputCheckedEntity, inputEntityRelationships, inputEntityAttributes, inputAttributeMapping, serviceParameters);

            actual.Should().BeTrue();
            MetadataServiceMock.VerifyAll();
        }

        [TestMethod]
        public void CollectCrmEntityFieldsCheckedEntityCountIsZero()
        {
            var inputAttributeMapping = new DataMigration.XrmToolBoxPlugin.Core.AttributeTypeMapping();
            var serviceParameters = GenerateMigratorParameters();

            var schemaConfiguration = new Capgemini.Xrm.DataMigration.Config.CrmSchemaConfiguration();

            FluentActions.Invoking(() => systemUnderTest.CollectCrmEntityFields(inputCheckedEntity, schemaConfiguration, inputEntityRelationships, inputEntityAttributes, inputAttributeMapping, serviceParameters))
                                .Should()
                                .NotThrow();
        }

        [TestMethod]
        public void CollectCrmEntityFields()
        {
            var entityLogicalName = "contact";
            inputCheckedEntity.Add(entityLogicalName);
            var attributeSet = new HashSet<string>() { "contactId", "firstname", "lastname" };
            inputEntityAttributes.Add(entityLogicalName, attributeSet);
            var inputAttributeMapping = new DataMigration.XrmToolBoxPlugin.Core.AttributeTypeMapping();

            var schemaConfiguration = new Capgemini.Xrm.DataMigration.Config.CrmSchemaConfiguration();

            var entityMetadata = InstantiateEntityMetaData(entityLogicalName);
            InsertAttributeList(entityMetadata, new List<string> { "contactId", "firstname", "lastname" });

            var serviceParameters = GenerateMigratorParameters();

            MetadataServiceMock.Setup(x => x.RetrieveEntities(It.IsAny<string>(), It.IsAny<IOrganizationService>(), It.IsAny<IExceptionService>()))
                                .Returns(entityMetadata)
                                .Verifiable();

            FluentActions.Invoking(() => systemUnderTest.CollectCrmEntityFields(inputCheckedEntity, schemaConfiguration, inputEntityRelationships, inputEntityAttributes, inputAttributeMapping, serviceParameters))
                                 .Should()
                                 .NotThrow();

            MetadataServiceMock.VerifyAll();
        }

        [TestMethod]
        public void StoreCrmEntityData()
        {
            var entityLogicalName = "contact";
            var attributeSet = new HashSet<string>() { "contactId", "firstname", "lastname" };
            inputEntityAttributes.Add(entityLogicalName, attributeSet);
            var inputAttributeMapping = new DataMigration.XrmToolBoxPlugin.Core.AttributeTypeMapping();

            var entityMetadata = InstantiateEntityMetaData(entityLogicalName);
            InsertAttributeList(entityMetadata, new List<string> { "contactId", "firstname", "lastname" });

            var crmEntity = new Capgemini.Xrm.DataMigration.Model.CrmEntity();
            var crmEntityList = new List<Capgemini.Xrm.DataMigration.Model.CrmEntity>();

            FluentActions.Invoking(() => systemUnderTest.StoreCrmEntityData(crmEntity, entityMetadata, crmEntityList, inputEntityRelationships, inputEntityAttributes, inputAttributeMapping, NotificationServiceMock.Object))
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

            FluentActions.Invoking(() => systemUnderTest.CollectCrmAttributesFields(entityMetadata, crmEntity, inputEntityAttributes, inputAttributeMapping, NotificationServiceMock.Object))
                                 .Should()
                                 .NotThrow();

            crmEntity.CrmFields.Count.Should().Be(3);
        }

        [TestMethod]
        public void CollectCrmAttributesFieldsWithNoInputEntityAttributes()
        {
            var entityLogicalName = "contact";

            var inputAttributeMapping = new DataMigration.XrmToolBoxPlugin.Core.AttributeTypeMapping();

            var entityMetadata = InstantiateEntityMetaData(entityLogicalName);
            InsertAttributeList(entityMetadata, new List<string> { "contactId", "firstname", "lastname" });

            var crmEntity = new Capgemini.Xrm.DataMigration.Model.CrmEntity
            {
                Name = entityLogicalName,
                DisplayName = entityLogicalName
            };

            FluentActions.Invoking(() => systemUnderTest.CollectCrmAttributesFields(entityMetadata, crmEntity, inputEntityAttributes, inputAttributeMapping, NotificationServiceMock.Object))
                                 .Should()
                                 .NotThrow();

            crmEntity.CrmFields.Count.Should().Be(0);
        }

        [TestMethod]
        public void StoreAttributeMetadataWithNullInputEntityAttributes()
        {
            var entityLogicalName = "contact";
            inputEntityAttributes = null;
            var inputAttributeMapping = new DataMigration.XrmToolBoxPlugin.Core.AttributeTypeMapping();

            string primaryAttribute = "contactId";
            var entityMetadata = InstantiateEntityMetaData(entityLogicalName);
            InsertAttributeList(entityMetadata, new List<string> { primaryAttribute, "firstname", "lastname" });

            var crmFieldList = new List<Capgemini.Xrm.DataMigration.Model.CrmField>();

            FluentActions.Invoking(() => systemUnderTest.StoreAttributeMetadata(entityMetadata.Attributes[0], entityMetadata, primaryAttribute, crmFieldList, inputEntityAttributes, inputAttributeMapping, NotificationServiceMock.Object))
                                 .Should()
                                 .NotThrow();

            crmFieldList.Count.Should().Be(0);
        }

        [TestMethod]
        public void StoreAttributeMetadataWithNoInputEntityAttributes()
        {
            var entityLogicalName = "contact";

            var inputAttributeMapping = new DataMigration.XrmToolBoxPlugin.Core.AttributeTypeMapping();

            string primaryAttribute = "contactId";
            var entityMetadata = InstantiateEntityMetaData(entityLogicalName);
            InsertAttributeList(entityMetadata, new List<string> { primaryAttribute, "firstname", "lastname" });

            var crmFieldList = new List<Capgemini.Xrm.DataMigration.Model.CrmField>();

            FluentActions.Invoking(() => systemUnderTest.StoreAttributeMetadata(entityMetadata.Attributes[0], entityMetadata, primaryAttribute, crmFieldList, inputEntityAttributes, inputAttributeMapping, NotificationServiceMock.Object))
                                 .Should()
                                 .NotThrow();

            crmFieldList.Count.Should().Be(0);
        }

        [TestMethod]
        public void StoreAttributeMetadata()
        {
            var entityLogicalName = "contact";
            var attributeSet = new HashSet<string>() { "contactId", "firstname", "lastname" };
            inputEntityAttributes.Add(entityLogicalName, attributeSet);
            var inputAttributeMapping = new DataMigration.XrmToolBoxPlugin.Core.AttributeTypeMapping();

            string primaryAttribute = "contactId";
            var entityMetadata = InstantiateEntityMetaData(entityLogicalName);
            InsertAttributeList(entityMetadata, new List<string> { primaryAttribute, "firstname", "lastname" });

            var crmFieldList = new List<Capgemini.Xrm.DataMigration.Model.CrmField>();

            FluentActions.Invoking(() => systemUnderTest.StoreAttributeMetadata(entityMetadata.Attributes[0], entityMetadata, primaryAttribute, crmFieldList, inputEntityAttributes, inputAttributeMapping, NotificationServiceMock.Object))
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

            NotificationServiceMock.Setup(x => x.DisplayFeedback("The supplied attribute is null. Expecting an Entity Reference!"))
                              .Verifiable();

            FluentActions.Invoking(() => systemUnderTest.StoreLookUpAttribute(attribute, crmField, NotificationServiceMock.Object))
                             .Should()
                             .NotThrow();
            NotificationServiceMock.Verify(x => x.DisplayFeedback("The supplied attribute is null. Expecting an Entity Reference!"), Times.Never);
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

            NotificationServiceMock.Setup(x => x.DisplayFeedback("The supplied attribute is null. Expecting an Entity Reference!"))
                               .Verifiable();

            FluentActions.Invoking(() => systemUnderTest.StoreLookUpAttribute(attribute, crmField, NotificationServiceMock.Object))
                             .Should()
                             .NotThrow();

            NotificationServiceMock.Verify(x => x.DisplayFeedback("The supplied attribute is null. Expecting an Entity Reference!"), Times.Once);
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

            NotificationServiceMock.Setup(x => x.DisplayFeedback("The supplied attribute is null. Expecting an Entity Reference!"))
                               .Verifiable();

            FluentActions.Invoking(() => systemUnderTest.StoreLookUpAttribute(null, crmField, NotificationServiceMock.Object))
                             .Should()
                             .NotThrow();

            NotificationServiceMock.Verify(x => x.DisplayFeedback("The supplied attribute is null. Expecting an Entity Reference!"), Times.Once);
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

            NotificationServiceMock.Setup(x => x.DisplayFeedback("The supplied attribute is null. Expecting an Entity Reference!"))
                               .Verifiable();

            FluentActions.Invoking(() => systemUnderTest.StoreLookUpAttribute(attribute, crmField, NotificationServiceMock.Object))
                             .Should()
                             .NotThrow();

            NotificationServiceMock.Verify(x => x.DisplayFeedback("The supplied attribute is null. Expecting an Entity Reference!"), Times.Never);
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

            NotificationServiceMock.Setup(x => x.DisplayFeedback("The supplied attribute is null. Expecting an Entity Reference!"))
                               .Verifiable();

            FluentActions.Invoking(() => systemUnderTest.StoreLookUpAttribute(attribute, crmField, NotificationServiceMock.Object))
                             .Should()
                             .NotThrow();

            NotificationServiceMock.Verify(x => x.DisplayFeedback("The supplied attribute is null. Expecting an Entity Reference!"), Times.Never);
            crmField.LookupType.Should().BeNull();
        }
    }
}