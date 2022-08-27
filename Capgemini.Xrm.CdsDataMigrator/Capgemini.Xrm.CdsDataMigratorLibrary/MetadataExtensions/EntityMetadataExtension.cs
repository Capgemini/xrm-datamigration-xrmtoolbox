//using Capgemini.Xrm.CdsDataMigratorLibrary.Core;
//using Capgemini.Xrm.CdsDataMigratorLibrary.Extensions;
//using Capgemini.Xrm.CdsDataMigratorLibrary.Forms;
//using Capgemini.Xrm.CdsDataMigratorLibrary.Models;
//using Capgemini.Xrm.CdsDataMigratorLibrary.Services;
//using Capgemini.Xrm.DataMigration.Config;
//using Capgemini.Xrm.DataMigration.CrmStore.Config;
//using Capgemini.Xrm.DataMigration.Model;
//using Microsoft.Xrm.Sdk;
//using Microsoft.Xrm.Sdk.Metadata;
//using NuGet;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Drawing;
//using System.Globalization;
//using System.IO;
//using System.Linq;
//using System.Windows.Forms;

//namespace Capgemini.Xrm.CdsDataMigratorLibrary.Controllers
//{
//    public class EntityMetadataExtension : MetadataExtensionBase
//    {
//        public void AddSelectedEntities(int selectedItemsCount, string inputEntityLogicalName, HashSet<string> inputSelectedEntity)
//        {
//            if (selectedItemsCount > 0 &&
//                !(
//                    string.IsNullOrEmpty(inputEntityLogicalName) &&
//                    inputSelectedEntity.Contains(inputEntityLogicalName)
//                  )
//                )
//            {
//                inputSelectedEntity.Add(inputEntityLogicalName);
//            }
//        }

        
        

        

        

//        //private void UpdateCheckBoxesEntities(EntityMetadata entity, ListViewItem item, Dictionary<string, HashSet<string>> inputEntityAttributes)
//        //{
//        //    item.Checked |= inputEntityAttributes.ContainsKey(entity.LogicalName);
//        //}

//        private static void ExtractRelationships(CrmEntity entities, HashSet<string> relationShipSet)
//        {
//            if (entities.CrmRelationships != null)
//            {
//                foreach (var relationship in entities.CrmRelationships)
//                {
//                    relationShipSet.Add(relationship.RelationshipName);
//                }
//            }
//        }

//        private static void ExtractAttributes(CrmEntity entities, HashSet<string> attributeSet)
//        {
//            if (entities.CrmFields != null)
//            {
//                foreach (var attributes in entities.CrmFields)
//                {
//                    attributeSet.Add(attributes.FieldName);
//                }
//            }
//        }
//    }
//}