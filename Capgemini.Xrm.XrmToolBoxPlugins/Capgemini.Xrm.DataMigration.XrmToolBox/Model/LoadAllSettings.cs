using System;
using System.Text;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Model
{
    public class LoadAllSettings
    {
        public string SchemaPath { get; set; }

        public string ImportPath { get; set; }

        public string ExportPath { get; set; }

        public bool FailedValidation { get; set; }

        public string FailedValidationMessage { get; set; }

        public void Validate()
        {
            FailedValidation = false;
            StringBuilder message = new StringBuilder();
            if (string.IsNullOrEmpty(SchemaPath) || SchemaPath == null)
            {
                message.AppendLine("Schema file path is empty");
                FailedValidation = true;
            }

            if (string.IsNullOrEmpty(ImportPath) || ImportPath == null)
            {
                message.AppendLine("Import config file path is empty");
                FailedValidation = true;
            }

            if (string.IsNullOrEmpty(ExportPath) || ExportPath == null)
            {
                message.AppendLine("Export config file path is empty");
                FailedValidation = true;
            }

            FailedValidationMessage = message.ToString();
        }
    }
}