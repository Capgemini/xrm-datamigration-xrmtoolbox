using System.Collections.Generic;
using System.Linq;
using Microsoft.Xrm.Sdk;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Core
{
    public class Organisations
    {
        public Organisations()
        {
            Sortcolumns = new List<Item<string, int>>();
            Mappings = new List<Item<EntityReference, EntityReference>>();
            Entities = new List<Item<string, EntitySettings>>();
        }

        public List<Item<string, int>> Sortcolumns { get; }

        public List<Item<EntityReference, EntityReference>> Mappings { get; }

        public List<Item<string, EntitySettings>> Entities { get; }

        public EntitySettings this[string logicalname]
        {
            get
            {
                if (!Entities.Any(o => o.Key == logicalname))
                {
                    Entities.Add(new Item<string, EntitySettings>(logicalname, new EntitySettings()));
                }

                return Entities.Where(o => o.Key == logicalname).Select(o => o.Value).FirstOrDefault();
            }
        }
    }
}