using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Model
{
    class LoadAllSettings
    {
        public String SchemaPath { get; set; } 
        public String ImportPath { get; set; } 
        public String ExportPath { get; set; }
        public Boolean FailedValidation { get; set; }
        public String FailedValidationMessage { get; set; }


        public void Validate()
        {
            FailedValidation = false;
            StringBuilder message = new StringBuilder();
            if (String.IsNullOrEmpty(SchemaPath) || SchemaPath == null)
            {
                message.AppendLine("Schema file path is empty");
                FailedValidation = true;
            }
            if (String.IsNullOrEmpty(ImportPath) || ImportPath == null)
            {
                message.AppendLine("Import config file path is empty");
                FailedValidation = true;
            }
            if (String.IsNullOrEmpty(ExportPath) || ExportPath == null)
            {
                message.AppendLine("Export config file path is empty");
                FailedValidation = true;
            }

            FailedValidationMessage = message.ToString();
        
        }
    }
}
