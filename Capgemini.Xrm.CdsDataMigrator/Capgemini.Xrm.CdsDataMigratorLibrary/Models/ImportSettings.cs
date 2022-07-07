using Capgemini.Xrm.CdsDataMigratorLibrary.Enums;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Models
{
    public class ImportSettings
    {
        public DataFormat DataFormat { get; set; }

        public string SavePath { get; set; }

        public IOrganizationService EnvironmentConnection { get; set; }

        public string ImportConfigPath { get; set; }

        public string SchemaPath { get; set; }

        public bool ImportInactiveRecords { get; set; }

        public bool Minimize { get; set; }

        public int BatchSize { get; set; }
    }
}