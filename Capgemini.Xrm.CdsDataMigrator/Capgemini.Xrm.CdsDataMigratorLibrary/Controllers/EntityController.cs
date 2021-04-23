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
    public class EntityController : ControllerBase
    {
        public List<ListViewItem> RetrieveSourceEntitiesList(bool showSystemAttributes, List<EntityMetadata> inputCachedMetadata, Dictionary<string, HashSet<string>> inputEntityAttributes, ServiceParameters serviceParameters)
        {
            var sourceList = serviceParameters.MetadataService.RetrieveEntities(serviceParameters.OrganizationService);

            if (!showSystemAttributes)
            {
                sourceList = sourceList.Where(p => !p.IsLogicalEntity.Value && !p.IsIntersect.Value).ToList();
            }

            if (sourceList != null)
            {
                inputCachedMetadata = sourceList.OrderBy(p => p.IsLogicalEntity.Value).ThenBy(p => p.IsIntersect.Value).ThenByDescending(p => p.IsCustomEntity.Value).ThenBy(p => p.LogicalName).ToList();
            }

            var sourceEntitiesList = new List<ListViewItem>();

            foreach (EntityMetadata entity in inputCachedMetadata)
            {
                var name = entity.DisplayName.UserLocalizedLabel == null ? string.Empty : entity.DisplayName.UserLocalizedLabel.Label;
                var item = new ListViewItem(name)
                {
                    Tag = entity
                };
                item.SubItems.Add(entity.LogicalName);
                IsInvalidForCustomization(entity, item);
                UpdateCheckBoxesEntities(entity, item, inputEntityAttributes);

                sourceEntitiesList.Add(item);
            }

            return sourceEntitiesList;
        }

        public void IsInvalidForCustomization(EntityMetadata entity, ListViewItem item)
        {
            if (entity != null)
            {
                if (entity.IsCustomEntity != null && entity.IsCustomEntity.Value)
                {
                    item.ForeColor = Color.DarkGreen;
                }

                if (entity.IsIntersect != null && entity.IsIntersect.Value)
                {
                    item.ForeColor = Color.Red;
                    item.ToolTipText = "Intersect Entity, ";
                }

                if (entity.IsLogicalEntity != null && entity.IsLogicalEntity.Value)
                {
                    item.ForeColor = Color.Red;
                    item.ToolTipText = "Logical Entity";
                }
            }
        }

        public void AddSelectedEntities(int selectedItemsCount, string inputEntityLogicalName, HashSet<string> inputSelectedEntity)
        {
            if (selectedItemsCount > 0 &&
                !(
                    string.IsNullOrEmpty(inputEntityLogicalName) &&
                    inputSelectedEntity.Contains(inputEntityLogicalName)
                  )
                )
            {
                inputSelectedEntity.Add(inputEntityLogicalName);
            }
        }

        public void PopulateEntitiesListView(List<ListViewItem> items, Exception exception, IWin32Window owner, ListView listView, INotificationService notificationService)
        {
            if (exception != null)
            {
                notificationService.DisplayErrorFeedback(owner, $"An error occured: {exception.Message}");
            }
            else
            {
                if (items != null && items.Count > 0)
                {
                    listView.Items.AddRange(items.ToArray());
                }
                else
                {
                    notificationService.DisplayWarningFeedback(owner, "The system does not contain any entities");
                }
            }
        }

        public string GetEntityLogicalName(ListViewItem entityitem)
        {
            string logicalName = null;
            if (entityitem != null && entityitem.Tag != null)
            {
                var entity = (EntityMetadata)entityitem.Tag;
                logicalName = entity.LogicalName;
            }
            return logicalName;
        }

        public void StoreEntityData(CrmEntity[] crmEntity, Dictionary<string, HashSet<string>> inputEntityAttributes, Dictionary<string, HashSet<string>> inputEntityRelationships)
        {
            inputEntityAttributes.Clear();
            inputEntityRelationships.Clear();
            if (crmEntity != null)
            {
                foreach (var entities in crmEntity)
                {
                    var logicalName = entities.Name;
                    var attributeSet = new HashSet<string>();
                    var relationShipSet = new HashSet<string>();
                    ExtractAttributes(entities, attributeSet);
                    ExtractRelationships(entities, relationShipSet);

                    inputEntityAttributes.Add(logicalName, attributeSet);
                    inputEntityRelationships.Add(logicalName, relationShipSet);
                }
            }
        }

        private void UpdateCheckBoxesEntities(EntityMetadata entity, ListViewItem item, Dictionary<string, HashSet<string>> inputEntityAttributes)
        {
            item.Checked |= inputEntityAttributes.ContainsKey(entity.LogicalName);
        }

        private static void ExtractRelationships(CrmEntity entities, HashSet<string> relationShipSet)
        {
            if (entities.CrmRelationships != null)
            {
                foreach (var relationship in entities.CrmRelationships)
                {
                    relationShipSet.Add(relationship.RelationshipName);
                }
            }
        }

        private static void ExtractAttributes(CrmEntity entities, HashSet<string> attributeSet)
        {
            if (entities.CrmFields != null)
            {
                foreach (var attributes in entities.CrmFields)
                {
                    attributeSet.Add(attributes.FieldName);
                }
            }
        }
    }
}