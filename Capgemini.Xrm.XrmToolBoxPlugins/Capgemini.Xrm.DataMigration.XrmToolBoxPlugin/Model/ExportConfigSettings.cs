using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Model
{
    class ExportConfigSettings
    {
        #region Public Properties
        public String JsonFilePath { get; set; }
        public String JsonFilePathLoad { get; set; }
        public Dictionary<string, string> Filter { get; set; }
        public Boolean FailedValidation { get; set; }
        public String FailedValidationLoadingMessage { get; set; }
        public String FailedValidationMessage { get; set; }
        public String SuccessValidationMessage { get; set; }
        public Boolean FailedValidationLoading { get; set; }
        public String SuccessValidationMessageLoading { get; set; }
        #endregion

        public void Validate()
        {
            FailedValidation = false;
            StringBuilder message = new StringBuilder();
            if (String.IsNullOrEmpty(JsonFilePath) || JsonFilePath == null)
            {
                FailedValidation = true;
                message.AppendLine("Export config file path is empty");
            }
                      FailedValidationMessage = message.ToString();
        }
        public void ValidateSuccess()
        {
            if (FailedValidation == false)
            {
                SuccessValidationMessage = "Successfully created json file";
            }
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
    }
}
