using System;
using System.Collections.Generic;
using System.Text;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Model
{
    internal class ImportConfigSettingscs
    {
        #region Public Properties

        public String JsonFilePath { get; set; }
        public String JsonFilePathLoad { get; set; }
        public Dictionary<string, Dictionary<Guid, Guid>> Mappings { get; set; }
        public Boolean FailedValidation { get; set; }
        public Boolean FailedValidationLoading { get; set; }
        public String FailedValidationLoadingMessage { get; set; }
        public String FailedValidationMessage { get; set; }
        public String SuccessValidationMessage { get; set; }
        public String SuccessValidationMessageLoading { get; set; }

        #endregion Public Properties

        #region Public Methods

        public void ValidateAll()
        {
            ValidateFailure();
            ValidateSuccessss();
        }

        public void ValidateLoading()
        {
            FailedValidationLoading = false;
            StringBuilder message = new StringBuilder();
            if (String.IsNullOrEmpty(JsonFilePathLoad) || JsonFilePathLoad == null)
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

        #endregion Public Methods

        #region Private  Methods

        private void ValidateFailure()
        {
            FailedValidation = false;
            StringBuilder message = new StringBuilder();
            if (String.IsNullOrEmpty(JsonFilePath) || JsonFilePath == null)
            {
                message.AppendLine("Import config file path is empty");
                FailedValidation = true;
            }

            FailedValidationMessage = message.ToString();
        }

        private void ValidateSuccessss()
        {
            if (FailedValidation == false)
            {
                SuccessValidationMessage = "Successfully created json file";
            }
        }

        #endregion Private  Methods
    }
}