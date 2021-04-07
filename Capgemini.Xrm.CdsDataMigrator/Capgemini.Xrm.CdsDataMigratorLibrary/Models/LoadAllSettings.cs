using System;
using System.Text;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Models
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
            if (string.IsNullOrEmpty(SchemaPath))
            {
                message.AppendLine("Schema file path is empty");
                FailedValidation = true;
            }

            if (string.IsNullOrEmpty(ImportPath))
            {
                message.AppendLine("Import config file path is empty");
                FailedValidation = true;
            }

            if (string.IsNullOrEmpty(ExportPath))
            {
                message.AppendLine("Export config file path is empty");
                FailedValidation = true;
            }

            FailedValidationMessage = message.ToString();
        }
    }
}