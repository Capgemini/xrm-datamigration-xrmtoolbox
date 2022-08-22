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
    public class AttributeMetadataExtension : MetadataExtensionBase
    {
        public AttributeMetadata[] GetAttributeList(string entityLogicalName, bool showSystemAttributes, ServiceParameters serviceParameters)
        {
            var entitymeta = serviceParameters.MetadataService.RetrieveEntities(entityLogicalName, serviceParameters.OrganizationService, serviceParameters.ExceptionService);

            var attributes = FilterAttributes(entitymeta, showSystemAttributes);

            if (attributes != null)
            {
                attributes = attributes.OrderByDescending(p => p.IsPrimaryId)
                                       .ThenByDescending(p => p.IsPrimaryName)
                                       .ThenByDescending(p => p.IsCustomAttribute != null && p.IsCustomAttribute.Value)
                                       .ThenBy(p => p.IsLogical != null && p.IsLogical.Value)
                                       .ThenBy(p => p.LogicalName).ToArray();
            }

            return attributes;
        }

        public void StoreAttributeIfRequiresKey(string logicalName, ItemCheckEventArgs e, Dictionary<string, HashSet<string>> inputEntityAttributes, string inputEntityLogicalName)
        {
            var attributeSet = new HashSet<string>();
            if (e.CurrentValue.ToString() != "Checked")
            {
                attributeSet.Add(logicalName);
            }

            inputEntityAttributes.Add(inputEntityLogicalName, attributeSet);
        }

        public void StoreAttriubteIfKeyExists(string logicalName, ItemCheckEventArgs e, Dictionary<string, HashSet<string>> inputEntityAttributes, string inputEntityLogicalName)
        {
            var attributeSet = inputEntityAttributes[inputEntityLogicalName];

            if (e.CurrentValue.ToString() == "Checked")
            {
                if (attributeSet.Contains(logicalName))
                {
                    attributeSet.Remove(logicalName);
                }
            }
            else
            {
                attributeSet.Add(logicalName);
            }
        }

        public List<ListViewItem> ProcessAllAttributeMetadata(List<string> unmarkedattributes, AttributeMetadata[] attributes, string inputEntityLogicalName, Dictionary<string, HashSet<string>> inputEntityAttributes)
        {
            List<ListViewItem> sourceAttributesList = new List<ListViewItem>();
            foreach (AttributeMetadata attribute in attributes)
            {
                var name = attribute.DisplayName.UserLocalizedLabel == null ? string.Empty : attribute.DisplayName.UserLocalizedLabel.Label;
                var typename = attribute.AttributeTypeName == null ? string.Empty : attribute.AttributeTypeName.Value;
                var item = new ListViewItem(name);
                AddAttribute(attribute, item, typename);
                InvalidUpdate(attribute, item);
                item.Checked = unmarkedattributes.Contains(attribute.LogicalName);
                UpdateAttributeMetadataCheckBoxes(attribute.LogicalName, item, inputEntityAttributes, inputEntityLogicalName);
                sourceAttributesList.Add(item);
            }

            return sourceAttributesList;
        }

        public AttributeMetadata[] FilterAttributes(EntityMetadata entityMetadata, bool showSystemAttributes)
        {
            AttributeMetadata[] attributes = entityMetadata.Attributes?.ToArray();

            if (attributes != null && !showSystemAttributes)
            {
                attributes = attributes.Where(p => p.IsLogical != null
                                                    && !p.IsLogical.Value
                                                    && p.IsValidForRead != null
                                                    && p.IsValidForRead.Value
                                                    && (p.IsValidForCreate != null && p.IsValidForCreate.Value || p.IsValidForUpdate != null && p.IsValidForUpdate.Value))
                                        .ToArray();
            }

            return attributes;
        }

        public void InvalidUpdate(AttributeMetadata attribute, ListViewItem item)
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

        private void AddAttribute(AttributeMetadata attribute, ListViewItem item, string typename)
        {
            item.Tag = attribute;
            item.SubItems.Add(attribute.LogicalName);
            item.SubItems.Add(typename.EndsWith("Type", StringComparison.Ordinal) ? typename.Substring(0, typename.LastIndexOf("Type", StringComparison.Ordinal)) : typename);
        }
    }
}