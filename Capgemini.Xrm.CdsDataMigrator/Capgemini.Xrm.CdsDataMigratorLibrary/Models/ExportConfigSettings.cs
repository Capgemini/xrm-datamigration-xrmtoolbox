using System;
using System.Collections.Generic;
using System.Text;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Models
{
    public class ExportConfigSettings
    {
        public string JsonFilePath { get; set; }

        public string JsonFilePathLoad { get; set; }

        public Dictionary<string, string> Filter { get; private set; } = new Dictionary<string, string>();

        public bool FailedValidation { get; set; }

        public string FailedValidationLoadingMessage { get; set; }

        public string FailedValidationMessage { get; set; }

        public string SuccessValidationMessage { get; set; }

        public bool FailedValidationLoading { get; set; }

        public string SuccessValidationMessageLoading { get; set; }

        public void Validate()
        {
            FailedValidation = false;
            StringBuilder message = new StringBuilder();
            if (string.IsNullOrEmpty(JsonFilePath))
            {
                FailedValidation = true;
                message.AppendLine("Export config file path is empty");
            }

            FailedValidationMessage = message.ToString();
        }

        public void ValidateSuccess()
        {
            if (!FailedValidation)
            {
                SuccessValidationMessage = "Successfully created json file";
            }
        }

        public void ValidateLoading()
        {
            FailedValidationLoading = false;
            StringBuilder message = new StringBuilder();
            if (string.IsNullOrEmpty(JsonFilePathLoad))
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
    }
}