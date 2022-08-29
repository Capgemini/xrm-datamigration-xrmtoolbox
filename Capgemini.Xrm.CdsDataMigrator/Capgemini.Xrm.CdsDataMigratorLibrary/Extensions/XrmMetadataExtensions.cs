using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Extensions
{
    public static class XrmMetadataExtensions
    {
        public static List<AttributeMetadata> FilterAttributes(this EntityMetadata entityMetadata, bool showSystemAttributes)
        {
            var attributes = entityMetadata.Attributes?.ToList();

            if (attributes != null && !showSystemAttributes)
            {
                attributes = attributes.Where(p => p.IsLogical != null
                                                    && !p.IsLogical.Value
                                                    && p.IsValidForRead != null
                                                    && p.IsValidForRead.Value
                                                    && (p.IsValidForCreate != null && p.IsValidForCreate.Value || p.IsValidForUpdate != null && p.IsValidForUpdate.Value))
                                        .ToList();
            }

            return attributes;
        }

        public static List<ListViewItem> ProcessAllAttributeMetadata(this List<AttributeMetadata> attributes, List<string> unmarkedattributes, string inputEntityLogicalName, Dictionary<string, HashSet<string>> inputEntityAttributes)
        {
            List<ListViewItem> sourceAttributesList = new List<ListViewItem>();
            foreach (AttributeMetadata attribute in attributes)
            {
                var name = attribute.DisplayName.UserLocalizedLabel == null ? string.Empty : attribute.DisplayName.UserLocalizedLabel.Label;
                var typename = attribute.AttributeTypeName == null ? string.Empty : attribute.AttributeTypeName.Value;
                var item = new ListViewItem(name);
                AddAttribute(attribute, item, typename);
                item.InvalidUpdate(attribute);
                item.Checked = unmarkedattributes.Contains(attribute.LogicalName);
                item.UpdateAttributeMetadataCheckBoxes(attribute.LogicalName, inputEntityAttributes, inputEntityLogicalName);
                sourceAttributesList.Add(item);
            }

            return sourceAttributesList;
        }

        private static void AddAttribute(AttributeMetadata attribute, ListViewItem item, string typename)
        {
            item.Tag = attribute;
            item.SubItems.Add(attribute.LogicalName);
            item.SubItems.Add(typename.EndsWith("Type", StringComparison.Ordinal) ? typename.Substring(0, typename.LastIndexOf("Type", StringComparison.Ordinal)) : typename);
        }
    }
}