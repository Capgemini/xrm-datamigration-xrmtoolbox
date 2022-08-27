using Capgemini.Xrm.CdsDataMigratorLibrary.Core;
using Capgemini.Xrm.CdsDataMigratorLibrary.Extensions;
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
        }
}