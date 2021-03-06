﻿using System.Text;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Models
{
    public class DeserializationSettings
    {
        public string XmlFolderPath { get; set; }

        public bool FailedValidation { get; set; }

        public string FailedValidationMessage { get; set; }

        public void Validate()
        {
            FailedValidation = false;
            var stringBuilder = new StringBuilder();
            if (string.IsNullOrWhiteSpace(XmlFolderPath) || XmlFolderPath == null)
            {
                stringBuilder.AppendLine("Enter schema folder path");
                FailedValidation = true;
            }

            FailedValidationMessage = stringBuilder.ToString();
        }
    }
}