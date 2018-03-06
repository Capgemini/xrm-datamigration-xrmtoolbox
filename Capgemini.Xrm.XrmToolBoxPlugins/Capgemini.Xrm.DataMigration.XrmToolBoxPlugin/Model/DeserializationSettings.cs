using System.Text;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Model
{
    internal class DeserializationSettings
    {
        #region Public Properties
        public string XmlFolderPath { get; set; }
        public bool FailedValidation { get; set; }
        public string FailedValidationMessage { get; set; }

        #endregion Public Properties

        #region

        public void Validate()
        {
            FailedValidation = false;
            StringBuilder stringBuilder = new StringBuilder();
            if (string.IsNullOrWhiteSpace(XmlFolderPath) || XmlFolderPath == null)
            {
                stringBuilder.AppendLine("Enter schema folder path");
                FailedValidation = true;
            }
            FailedValidationMessage = stringBuilder.ToString();
        }

        #endregion
    }
}