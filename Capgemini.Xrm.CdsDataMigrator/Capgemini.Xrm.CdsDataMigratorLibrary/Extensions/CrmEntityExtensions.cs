using Capgemini.Xrm.DataMigration.Model;
using System.Collections.Generic;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Extensions
{
    public static class CrmEntityExtensions
    {
        public static void StoreEntityData(this List<CrmEntity> crmEntity, Dictionary<string, HashSet<string>> inputEntityAttributes, Dictionary<string, HashSet<string>> inputEntityRelationships)
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
