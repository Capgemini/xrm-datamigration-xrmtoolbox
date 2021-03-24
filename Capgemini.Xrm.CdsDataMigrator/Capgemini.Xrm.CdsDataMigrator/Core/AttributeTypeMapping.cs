using Capgemini.Xrm.DataMigration.XrmToolBox.Services;
using System.Windows;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core
{
    public class AttributeTypeMapping
    {
        public string AttributeMetadataType { get; set; }

        public string AttributeMetadataTypeResult { get; set; }

        public void GetMapping(IFeedbackManager feedbackManager)
        {
            AttributeMetadataTypeResult = GetAttributeMetadataTypeResult(AttributeMetadataType, feedbackManager);
        }

        private static string GetAttributeMetadataTypeResult(string input, IFeedbackManager feedbackManager)
        {
            var result = "Unknown";

            switch (input)
            {
                case "StringType":
                    result = "string";
                    break;

                case "UniqueidentifierType":
                    result = "guid";
                    break;

                case "PicklistType":
                    result = "optionsetvalue";
                    break;

                case "MoneyType":
                    result = "money";
                    break;

                case "BooleanType":
                    result = "bool";
                    break;

                case "LookupType":
                    result = "entityreference";
                    break;

                case "IntegerType":
                    result = "integer";
                    break;

                case "DateTimeType":
                    result = "datetime";
                    break;

                case "DoubleType":
                    result = "double";
                    break;

                case "DecimalType":
                    result = "decimal";
                    break;

                case "MemoType":
                    result = "memo";
                    break;

                case "ImageType":
                    result = "image";
                    break;

                case "EntityName":
                    result = "entityname";
                    break;

                case "StateType":
                    result = "state";
                    break;

                case "StatusType":
                    result = "status";
                    break;

                case "OwnerType":
                case "Owner":
                    result = "entityreference";
                    break;

                default:
                    feedbackManager.DisplayFeedback($"Missing mapping for {input}");
                    break;
            }

            return result;
        }
    }
}