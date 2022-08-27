using Capgemini.Xrm.CdsDataMigratorLibrary.Core;
using Capgemini.Xrm.CdsDataMigratorLibrary.Services;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Extensions
{
    public static class ListViewItemExtensions
    {
        public static void IsInvalidForCustomization(this ListViewItem item, EntityMetadata entity)
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

        public static string GetEntityLogicalName(this ListViewItem entityitem)
        {
            string logicalName = null;
            if (entityitem != null && entityitem.Tag != null)
            {
                var entity = (EntityMetadata)entityitem.Tag;
                logicalName = entity.LogicalName;
            }
            return logicalName;
        }

        public static void PopulateEntitiesListView(this List<ListViewItem> items, Exception exception, IWin32Window owner, ListView listView, INotificationService notificationService)
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

        public static void SetListViewSorting(this ListView listview, int column, string inputOrganisationId, Core.Settings settings)
        {
            var setting = settings[inputOrganisationId].Sortcolumns.FirstOrDefault(s => s.Key == listview.Name);
            if (setting == null)
            {
                setting = new Item<string, int>(listview.Name, -1);
                settings[inputOrganisationId].Sortcolumns.Add(setting);
            }

            if (setting.Value != column)
            {
                setting.Value = column;
                listview.Sorting = SortOrder.Ascending;
            }
            else
            {
                if (listview.Sorting == SortOrder.Ascending)
                {
                    listview.Sorting = SortOrder.Descending;
                }
                else
                {
                    listview.Sorting = SortOrder.Ascending;
                }
            }

            listview.ListViewItemSorter = new ListViewItemComparer(column, listview.Sorting);
        }

        public static void OnPopulateCompletedAction(this ListView listView, RunWorkerCompletedEventArgs e, INotificationService notificationService, IWin32Window owner, bool showSystemAttributes)
        {
            if (e?.Error != null)
            {
                notificationService.DisplayErrorFeedback(owner, $"An error occured: {e.Error.Message}");
            }
            else
            {
                var items = e.Result as List<ListViewItem>;
                if (showSystemAttributes)
                {
                    items = items.Where(x => ((AttributeMetadata)x.Tag)?.IsCustomAttribute == true).ToList();
                }

                if (items != null)
                {
                    listView.Items.AddRange(items.ToArray());
                }
            }
        }

        public static void UpdateAttributeMetadataCheckBoxes(this ListViewItem item, string predicate, Dictionary<string, HashSet<string>> inputEntityRelationships, string inputEntityLogicalName)
        {
            if (inputEntityRelationships.ContainsKey(inputEntityLogicalName))
            {
                foreach (string attr in inputEntityRelationships[inputEntityLogicalName])
                {
                    item.Checked |= attr.Equals(predicate, StringComparison.InvariantCulture);
                }
            }
        }

        public static void InvalidUpdate(this ListViewItem item, AttributeMetadata attribute)
        {
            item.ToolTipText = string.Empty;

            if (attribute.IsValidForCreate != null && !attribute.IsValidForCreate.Value)
            {
                item.ForeColor = Color.Gray;
                item.ToolTipText = "Not createable, ";
            }

            CheckForPrimaryIdAndName(attribute, item);

            CheckForVirtual(attribute, item);

            if (attribute.IsLogical != null && attribute.IsLogical.Value)
            {
                item.ForeColor = Color.Red;
                item.ToolTipText += "Logical attribute, ";
            }

            if (attribute.IsValidForCreate != null && !attribute.IsValidForCreate.Value &&
                attribute.IsValidForUpdate != null && !attribute.IsValidForUpdate.Value)
            {
                item.ForeColor = Color.Red;
            }

            if (attribute.IsValidForRead != null && !attribute.IsValidForRead.Value)
            {
                item.ForeColor = Color.Red;
                item.ToolTipText += "Not readable, ";
            }

            if (attribute.Description != null && attribute.Description.LocalizedLabels.Count > 0)
            {
                item.ToolTipText += attribute.Description.LocalizedLabels.First().Label;
            }

            if (!string.IsNullOrWhiteSpace(attribute.DeprecatedVersion))
            {
                item.ForeColor = Color.Red;
                item.ToolTipText += "DeprecatedVersion:" + attribute.DeprecatedVersion;
            }

            item.SubItems.Add(item.ToolTipText);
        }

        private static void CheckForVirtual(AttributeMetadata attribute, ListViewItem item)
        {
            if (attribute.AttributeType == AttributeTypeCode.Virtual || attribute.AttributeType == AttributeTypeCode.ManagedProperty)
            {
                item.ForeColor = Color.Red;
                item.ToolTipText += "Virtual or managed property, ";
            }
        }

        private static void CheckForPrimaryIdAndName(AttributeMetadata attribute, ListViewItem item)
        {
            if (attribute.IsValidForUpdate != null && !attribute.IsValidForUpdate.Value)
            {
                item.ForeColor = Color.Gray;
                item.ToolTipText += "Not updateable, ";
            }

            if (attribute.IsCustomAttribute != null && attribute.IsCustomAttribute.Value)
            {
                item.ForeColor = Color.DarkGreen;
            }

            if (attribute.IsPrimaryId != null && attribute.IsPrimaryId.Value)
            {
                item.ForeColor = Color.DarkBlue;
            }

            if (attribute.IsPrimaryName != null && attribute.IsPrimaryName.Value)
            {
                item.ForeColor = Color.DarkBlue;
            }
        }
    }
}