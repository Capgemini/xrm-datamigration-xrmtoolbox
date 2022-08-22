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
    public class RelationshipMetadataExtension : MetadataExtensionBase
    {
        public List<ListViewItem> PopulateRelationshipAction(string inputEntityLogicalName, Dictionary<string, HashSet<string>> inputEntityRelationships, ServiceParameters migratorParameters)
        {
            var entityMetaData = migratorParameters.MetadataService.RetrieveEntities(inputEntityLogicalName, migratorParameters.OrganizationService, migratorParameters.ExceptionService);
            var sourceAttributesList = new List<ListViewItem>();
            if (entityMetaData != null && entityMetaData.ManyToManyRelationships != null && entityMetaData.ManyToManyRelationships.Any())
            {
                foreach (var relationship in entityMetaData.ManyToManyRelationships)
                {
                    var item = new ListViewItem(relationship.IntersectEntityName);
                    AddRelationship(relationship, item, sourceAttributesList);
                    UpdateAttributeMetadataCheckBoxes(relationship.IntersectEntityName, item, inputEntityRelationships, inputEntityLogicalName);
                }
            }

            return sourceAttributesList;
        }

        public void StoreRelationshipIfRequiresKey(string logicalName, ItemCheckEventArgs e, string inputEntityLogicalName, Dictionary<string, HashSet<string>> inputEntityRelationships)
        {
            var relationshipSet = new HashSet<string>();
            if (e.CurrentValue.ToString() != "Checked")
            {
                relationshipSet.Add(logicalName);
            }

            inputEntityRelationships.Add(inputEntityLogicalName, relationshipSet);
        }

        public void StoreRelationshipIfKeyExists(string logicalName, ItemCheckEventArgs e, string inputEntityLogicalName, Dictionary<string, HashSet<string>> inputEntityRelationships)
        {
            var relationshipSet = inputEntityRelationships[inputEntityLogicalName];

            if (e.CurrentValue.ToString() == "Checked")
            {
                if (relationshipSet.Contains(logicalName))
                {
                    relationshipSet.Remove(logicalName);
                }
            }
            else
            {
                relationshipSet.Add(logicalName);
            }
        }
    }
}