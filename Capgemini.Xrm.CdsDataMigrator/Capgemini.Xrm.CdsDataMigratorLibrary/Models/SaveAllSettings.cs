﻿using System.Text;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Models
{
    public class SaveAllSettings
    {
        public string SchemaFilePath { get; set; }

        public string ImportFilePath { get; set; }

        public string ExportFilePath { get; set; }

        public string FailedValidationMessage { get; set; }

        public bool FailedValidation { get; set; }

        public string SuccessValidationMessage { get; set; }

        public void Validate()
        {
            var stringBuilder = new StringBuilder();
            FailedValidationMessage = string.Empty;
            FailedValidation = false;
            if (string.IsNullOrEmpty(SchemaFilePath) || SchemaFilePath == null)
            {
                FailedValidation = true;
                stringBuilder.AppendLine("Select schema file path");
            }

            if (string.IsNullOrEmpty(ImportFilePath) || ImportFilePath == null)
            {
                FailedValidation = true;
                stringBuilder.AppendLine("Select import config file path");
            }

            if (string.IsNullOrEmpty(ExportFilePath) || ExportFilePath == null)
            {
                FailedValidation = true;
                stringBuilder.AppendLine("Select export config file path");
            }

            FailedValidationMessage = stringBuilder.ToString();
        }
    }
}