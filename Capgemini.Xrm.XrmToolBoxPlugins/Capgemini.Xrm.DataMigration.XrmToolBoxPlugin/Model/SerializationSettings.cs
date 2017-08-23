using Capgemini.Xrm.DataMigration.Core.EntitySchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Model
{
    class SerializationSettings
    {
        #region Public Properties
        public String XmlFilePath { get; set; }
        public CrmEntity[] crmEntity { get; set; }
        public string FailedValidationMessage { get; set; }
        public bool FailedValidation { get; set; }
        public string SuccessValidationMessage { get; set; }
        #endregion

        #region Public Methods 
        private void ValidateFailure()
        {
            var stringBuilder = new StringBuilder();
            FailedValidationMessage = "";
            FailedValidation = false;
            if (string.IsNullOrEmpty(XmlFilePath) || XmlFilePath == null)
            {
                FailedValidation = true;
                stringBuilder.AppendLine("Select file path");
            }
            if (crmEntity == null)
            {
                FailedValidation = true;
                stringBuilder.AppendLine("Select entity");
            }

            FailedValidationMessage = stringBuilder.ToString();
        }
        private void ValidateSuccess()
        {
            if (FailedValidation == false)
            {
                SuccessValidationMessage = "Successfully created XML file";
            }
        }
        public void ValidateAll()
        {
            ValidateFailure();
            ValidateSuccess();
        }

        #endregion
    }
}
