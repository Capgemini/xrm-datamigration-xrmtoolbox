﻿using System.IO;
using System.Text;
using Capgemini.Xrm.DataMigration.CrmStore.Config;
using Microsoft.Xrm.Tooling.Connector;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Models
{
    public class DataMigrationSettings
    {
        public int BatchSize { get; set; }

        public int PageSize { get; set; }

        public int TopCount { get; set; }

        public string SchemaFilePath { get; set; }

        public string JsonFolderPath { get; set; }

        public string SourceConnectionString { get; set; }

        public string TargetConnectionString { get; set; }

        public string SchemaConnectionString { get; set; }

        public string FailedValidationMessage { get; set; }

        public bool FailedValidation { get; private set; }

        public CrmImportConfig ImportConfig { get; internal set; }

        public CrmExporterConfig ExportConfig { get; internal set; }

        public CrmServiceClient TargetServiceClient { get; internal set; }

        public CrmServiceClient SourceServiceClient { get; internal set; }

        public CrmServiceClient SchemaServiceClient { get; internal set; }

        public void ValidateExport()
        {
            var stringBuilder = new StringBuilder();
            FailedValidationMessage = string.Empty;
            FailedValidation = false;
            if (string.IsNullOrWhiteSpace(SchemaFilePath) || SchemaFilePath.Length < 5)
            {
                FailedValidation = true;
                stringBuilder.AppendLine("Enter correct Schema file Path");
            }

            if (string.IsNullOrWhiteSpace(SourceConnectionString) && SourceServiceClient == null)
            {
                FailedValidation = true;
                stringBuilder.AppendLine("Select correct source connection details");
            }

            if (string.IsNullOrWhiteSpace(JsonFolderPath))
            {
                FailedValidation = true;
                stringBuilder.AppendLine("Enter correct Json file Path");
            }
            else if (!Directory.Exists(JsonFolderPath))
            {
                Directory.CreateDirectory(JsonFolderPath);
            }

            if (BatchSize < PageSize)
            {
                FailedValidation = true;
                stringBuilder.AppendLine("Batch Size cannot be less than Page Size");
            }

            FailedValidationMessage = stringBuilder.ToString();
        }

        public void ValidateImport()
        {
            var stringBuilder = new StringBuilder();
            FailedValidationMessage = string.Empty;
            FailedValidation = false;

            if (string.IsNullOrWhiteSpace(TargetConnectionString) && TargetServiceClient == null)
            {
                FailedValidation = true;
                stringBuilder.AppendLine("Select correct target connection details");
            }

            if (string.IsNullOrWhiteSpace(JsonFolderPath))
            {
                FailedValidation = true;
                stringBuilder.AppendLine("Enter correct Json file Path");
            }

            FailedValidationMessage = stringBuilder.ToString();
        }
    }
}