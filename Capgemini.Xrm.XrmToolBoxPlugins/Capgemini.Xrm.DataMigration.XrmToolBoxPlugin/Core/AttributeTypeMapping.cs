using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core
{
    class AttributeTypeMapping
    {
        #region Public Properties
        public string AttributeMetadataType { get; set; }
        public string AttributeMetadataTypeResult { get; set; }
        #endregion

        #region Public Methods
        public void GetMapping()
        {
            AttributeMetadataTypeResult = "Unknown";

            if (AttributeMetadataType.Equals("StringType"))
            {
                AttributeMetadataTypeResult = "string";
            }
            else if (AttributeMetadataType.Equals("UniqueidentifierType"))
            {
                AttributeMetadataTypeResult = "guid";
            }
            else if (AttributeMetadataType.Equals("PicklistType"))
            {
                AttributeMetadataTypeResult = "optionsetvalue";
            }
            else if (AttributeMetadataType.Equals("MoneyType"))
            {
                AttributeMetadataTypeResult = "money";
            }
            else if (AttributeMetadataType.Equals("BooleanType"))
            {
                AttributeMetadataTypeResult = "bool";
            }
            else if (AttributeMetadataType.Equals("LookupType"))
            {
                AttributeMetadataTypeResult = "entityreference";
            }
            else if (AttributeMetadataType.Equals("IntegerType"))
            {
                AttributeMetadataTypeResult = "integer";
            }
            else if (AttributeMetadataType.Equals("DateTimeType"))
            {
                AttributeMetadataTypeResult = "datetime";
            }
            else if (AttributeMetadataType.Equals("DoubleType"))
            {
                AttributeMetadataTypeResult = "double";
            }
            else if (AttributeMetadataType.Equals("DecimalType"))
            {
                AttributeMetadataTypeResult = "decimal";
            }
            else if (AttributeMetadataType.Equals("MemoType"))
            {
                AttributeMetadataTypeResult = "memo";
            }
            else if (AttributeMetadataType.Equals("ImageType"))
            {
                AttributeMetadataTypeResult = "image";
            }
            else if (AttributeMetadataType.Equals("EntityName"))
            {
                AttributeMetadataTypeResult = "entityname";
            }
            else if (AttributeMetadataType.Equals("StateType"))
            {
                AttributeMetadataTypeResult = "state";
            }
            else if (AttributeMetadataType.Equals("StatusType"))
            {
                AttributeMetadataTypeResult = "status";
            }
            else if (AttributeMetadataType.Equals("Owner"))
            {
                AttributeMetadataTypeResult = "entityreference";
            }
            else
                MessageBox.Show("Missing mapping for " + AttributeMetadataType);
        }
        #endregion
    }
}
