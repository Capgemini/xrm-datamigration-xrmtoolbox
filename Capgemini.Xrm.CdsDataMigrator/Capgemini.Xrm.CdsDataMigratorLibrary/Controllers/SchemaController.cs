using Capgemini.Xrm.CdsDataMigratorLibrary.Core;
using Capgemini.Xrm.CdsDataMigratorLibrary.Forms;
using Capgemini.Xrm.CdsDataMigratorLibrary.Models;
using Capgemini.Xrm.CdsDataMigratorLibrary.Services;
using Capgemini.Xrm.DataMigration.Config;
using Capgemini.Xrm.DataMigration.CrmStore.Config;
using Capgemini.Xrm.DataMigration.Model;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using NuGet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Controllers
{
    public class SchemaController : ControllerBase
    {
        public void SchemaFolderPathAction(INotificationService notificationService, TextBox schemaPathTextBox, bool inputWorkingstate, CollectionParameters collectionParameters, DialogResult dialogResult, SaveFileDialog fileDialog,
    Action<string, bool, INotificationService, Dictionary<string, HashSet<string>>, Dictionary<string, HashSet<string>>> loadSchemaFile
    )
        {
            if (dialogResult == DialogResult.OK)
            {
                schemaPathTextBox.Text = fileDialog.FileName.ToString(CultureInfo.InvariantCulture);

                if (File.Exists(schemaPathTextBox.Text))
                {
                    loadSchemaFile(schemaPathTextBox.Text, inputWorkingstate, notificationService, collectionParameters.EntityAttributes, collectionParameters.EntityRelationships);
                }
            }
            //else if (dialogResult == DialogResult.Cancel)
            //{
            //    schemaPathTextBox.Text = null;
            //}
        }

        public void SaveSchema(ServiceParameters serviceParameters, HashSet<string> inputCheckedEntity, Dictionary<string, HashSet<string>> inputEntityRelationships, Dictionary<string, HashSet<string>> inputEntityAttributes, AttributeTypeMapping inputAttributeMapping, CrmSchemaConfiguration inputCrmSchemaConfiguration, TextBox schemaPathTextBox)
        {
            if (AreCrmEntityFieldsSelected(inputCheckedEntity, inputEntityRelationships, inputEntityAttributes, inputAttributeMapping, serviceParameters))
            {
                CollectCrmEntityFields(inputCheckedEntity, inputCrmSchemaConfiguration, inputEntityRelationships, inputEntityAttributes, inputAttributeMapping, serviceParameters);
                GenerateXMLFile(schemaPathTextBox, inputCrmSchemaConfiguration);
                inputCrmSchemaConfiguration.Entities.Clear();
            }
            else
            {
                serviceParameters.NotificationService.DisplayFeedback("Please select at least one attribute for each selected entity!");
            }
        }

        public void GenerateXMLFile(TextBox tbSchemaPath, CrmSchemaConfiguration schemaConfiguration)
        {
            if (!string.IsNullOrWhiteSpace(tbSchemaPath.Text))
            {
                schemaConfiguration.SaveToFile(tbSchemaPath.Text);
            }
        }
    }
}