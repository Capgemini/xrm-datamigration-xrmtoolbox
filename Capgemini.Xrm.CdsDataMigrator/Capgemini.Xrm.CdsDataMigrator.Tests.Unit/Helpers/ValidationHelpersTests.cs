using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;

namespace Capgemini.Xrm.DataMigration.XrmToolBox.Helpers.Tests
{
    [TestClass]
    public class ValidationHelpersTests
    {
        [TestMethod]
        public void IsTextControlNotEmptyForEmptyString()
        {
            using (Control validationLabelName = new Label())
            {
                using (Control toValidateControlName = new TextBox())
                {
                    toValidateControlName.Text = string.Empty;

                    var actual = ValidationHelpers.IsTextControlNotEmpty(validationLabelName, toValidateControlName);

                    actual.Should().BeFalse();
                }
            }
        }

        [TestMethod]
        public void IsTextControlNotEmpty()
        {
            using (Control validationLabelName = new Label())
            {
                using (Control toValidateControlName = new TextBox())
                {
                    toValidateControlName.Text = "Sample Text";

                    var actual = ValidationHelpers.IsTextControlNotEmpty(validationLabelName, toValidateControlName);

                    actual.Should().BeTrue();
                }
            }
        }
    }
}