using System.Collections.Generic;
using System.Windows.Forms;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Helpers
{
    public static class CollectionHelpers
    {
        public static void StoreRelationshipIfRequiresKey(string logicalName, ItemCheckEventArgs e, string inputEntityLogicalName, Dictionary<string, HashSet<string>> inputEntityRelationships)
        {
            var relationshipSet = new HashSet<string>();
            if (e.CurrentValue.ToString() != "Checked")
            {
                relationshipSet.Add(logicalName);
            }

            inputEntityRelationships.Add(inputEntityLogicalName, relationshipSet);
        }

        public static void StoreRelationshipIfKeyExists(string logicalName, ItemCheckEventArgs e, string inputEntityLogicalName, Dictionary<string, HashSet<string>> inputEntityRelationships)
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