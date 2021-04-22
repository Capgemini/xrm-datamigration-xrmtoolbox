using System.Text;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Models
{
    public class FilterSettings
    {
        public string QueryString { get; set; }

        public string EntityName { get; set; }

        public bool WorkingState { get; set; }

        public string FailedValidationMessage { get; set; }

        public bool FailedValidation { get; set; }

        public void ValidateFailure()
        {
            var stringBuilder = new StringBuilder();
            FailedValidationMessage = string.Empty;
            FailedValidation = false;

            if (string.IsNullOrWhiteSpace(QueryString))
            {
                FailedValidation = true;
                stringBuilder.AppendLine("Enter correct Filter Query");
            }

            if (string.IsNullOrWhiteSpace(EntityName))
            {
                FailedValidation = true;
                stringBuilder.AppendLine("Enter correct Entity");
            }

            FailedValidationMessage = stringBuilder.ToString();
        }
    }
}