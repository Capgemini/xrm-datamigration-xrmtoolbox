using System.Collections.Generic;
using System.Text;
using Capgemini.Xrm.DataMigration.Model;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Model
{
    public class SerializationSettings
    {
        public string XmlFilePath { get; set; }

        public List<CrmEntity> Entity { get; private set; } = new List<CrmEntity>();

        public string FailedValidationMessage { get; set; }

        public bool FailedValidation { get; set; }

        public string SuccessValidationMessage { get; set; }

        public void ValidateAll()
        {
            ValidateFailure();
            ValidateSuccess();
        }

        private void ValidateFailure()
        {
            var stringBuilder = new StringBuilder();
            FailedValidationMessage = string.Empty;
            FailedValidation = false;

            if (string.IsNullOrEmpty(XmlFilePath) || XmlFilePath == null)
            {
                FailedValidation = true;
                stringBuilder.AppendLine("Select file path");
            }

            if (Entity.Count == 0)
            {
                FailedValidation = true;
                stringBuilder.AppendLine("Select entity");
            }

            FailedValidationMessage = stringBuilder.ToString();
        }

        private void ValidateSuccess()
        {
            if (!FailedValidation)
            {
                SuccessValidationMessage = "Successfully created XML file";
            }
        }
    }
}