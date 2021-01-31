using Capgemini.Xrm.DataMigration.XrmToolBox.Enums;
using Microsoft.Xrm.Sdk;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Models
{
    public class ExportSettings
    {
        public ExportSettings()
        {
        }

        public DataFormat DataFormat { get; set; }

        public string SavePath { get; set; }

        public IOrganizationService EnvironmentConnection { get; set; }

        public string ExportConfigPath { get; set; }

        public string SchemaPath { get; set; }

        public bool ExportInactiveRecords { get; set; }

        public bool Minimize { get; set; }

        public int BatchSize { get; set; }
    }
}