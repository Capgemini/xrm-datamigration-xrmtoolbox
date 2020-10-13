using System.Collections.Generic;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core
{
    public class EntitySettings
    {
        public EntitySettings()
        {
            UnmarkedAttributes = new List<string>();
            Filter = string.Empty;
        }

        public List<string> UnmarkedAttributes { get; private set; }

        public string Filter { get; set; }
    }
}