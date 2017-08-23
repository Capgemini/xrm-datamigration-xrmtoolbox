using Capgemini.Xrm.DataMigration.Core.EntitySchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Model
{
    class DeserializationSettings
    {
        #region Public Properties
        public string XmlFolderPath { get; set; }
        public bool FailedValidation { get; set; }
        public string FailedValidationMessage { get; set; }

        #endregion

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
