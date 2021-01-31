using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Model.Tests
{
    [TestClass]
    public class FilterSettingsTests
    {
        private FilterSettings systemUnderTest;

        [TestInitialize]
        public void TestSetup()
        {
            systemUnderTest = new FilterSettings();
        }

        [TestMethod]
        public void ValidateFailureForEmptyQueryString()
        {
            systemUnderTest.QueryString = string.Empty;

            systemUnderTest.ValidateFailure();

            FluentActions.Invoking(() => systemUnderTest.ValidateFailure())
                            .Should()
                            .NotThrow();

            systemUnderTest.FailedValidation.Should().BeTrue();
            systemUnderTest.FailedValidationMessage.Should().Contain("Enter correct Filter Query");
        }
    }
}