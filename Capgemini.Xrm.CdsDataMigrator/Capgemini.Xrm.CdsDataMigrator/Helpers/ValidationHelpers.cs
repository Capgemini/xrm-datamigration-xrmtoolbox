using System;
using System.Windows.Forms;

namespace Capgemini.Xrm.DataMigration.XrmToolBox.Helpers
{
    public static class ValidationHelpers
    {

        public static bool IsTextControlNotEmpty(Control validationLabelName, Control toValidateControlName)
        {

            if (!string.IsNullOrWhiteSpace(toValidateControlName.Text))
            {
                validationLabelName.Visible = false;
            }
            else
            {
                validationLabelName.Visible = true;
            }

            return !validationLabelName.Visible;
        }

    }

}
