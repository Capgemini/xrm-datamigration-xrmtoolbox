using System;
using System.Collections.Generic;
using System.Text;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Model
{
    public class ImportConfigSettingscs
    {
        public string JsonFilePath { get; set; }

        public string JsonFilePathLoad { get; set; }

        public Dictionary<string, Dictionary<Guid, Guid>> Mappings { get; private set; } = new Dictionary<string, Dictionary<Guid, Guid>>();

        public bool FailedValidation { get; set; }

        public bool FailedValidationLoading { get; set; }

        public string FailedValidationLoadingMessage { get; set; }

        public string FailedValidationMessage { get; set; }

        public string SuccessValidationMessage { get; set; }

        public string SuccessValidationMessageLoading { get; set; }

        public void ValidateAll()
        {
            ValidateFailure();
            ValidateSuccessss();
        }

        public void ValidateLoading()
        {
            FailedValidationLoading = false;
            StringBuilder message = new StringBuilder();
            if (string.IsNullOrEmpty(JsonFilePathLoad) || JsonFilePathLoad == null)
            {
                FailedValidationLoading = true;
                message.AppendLine("Json file path is empty");
            }
            else
            {
                message.AppendLine("Loading Success");
                SuccessValidationMessageLoading = message.ToString();
            }

            FailedValidationLoadingMessage = message.ToString();
        }

        private void ValidateFailure()
        {
            FailedValidation = false;
            StringBuilder message = new StringBuilder();
            if (string.IsNullOrEmpty(JsonFilePath) || JsonFilePath == null)
            {
                message.AppendLine("Import config file path is empty");
                FailedValidation = true;
            }

            FailedValidationMessage = message.ToString();
        }

        private void ValidateSuccessss()
        {
            if (!FailedValidation)
            {
                SuccessValidationMessage = "Successfully created json file";
            }
        }
    }
}