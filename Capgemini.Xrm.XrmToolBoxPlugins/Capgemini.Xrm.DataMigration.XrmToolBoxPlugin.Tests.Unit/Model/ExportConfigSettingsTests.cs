using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Model.Tests
{
    [TestClass]
    public class ExportConfigSettingsTests
    {
        private ExportConfigSettings systemUnderTest;

        [TestInitialize]
        public void Setup()
        {
            systemUnderTest = new ExportConfigSettings();
        }

        [TestMethod]
        public void Validate()
        {
            FluentActions.Invoking(() => systemUnderTest.Validate())
               .Should()
               .NotThrow();

            systemUnderTest.FailedValidation.Should().BeTrue();
            systemUnderTest.FailedValidationMessage.Contains("Export config file path is empty").Should().BeTrue();
        }

        [TestMethod]
        public void ValidateSuccess()
        {
            systemUnderTest.FailedValidation = false;

            FluentActions.Invoking(() => systemUnderTest.ValidateSuccess())
               .Should()
               .NotThrow();

            systemUnderTest.SuccessValidationMessage.Contains("Successfully created json file").Should().BeTrue();
        }

        [TestMethod]
        public void ValidateLoadingJsonFilePathLoadNull()
        {
            systemUnderTest.JsonFilePathLoad = null;

            FluentActions.Invoking(() => systemUnderTest.ValidateLoading())
               .Should()
               .NotThrow();

            systemUnderTest.FailedValidationLoading.Should().BeTrue();
            systemUnderTest.FailedValidationLoadingMessage.Contains("Json file path is empty").Should().BeTrue();

            systemUnderTest.FailedValidationLoadingMessage.Contains("Json file path is empty").Should().BeTrue();
            systemUnderTest.FailedValidationLoadingMessage.Contains("Loading Success").Should().BeFalse();
            systemUnderTest.SuccessValidationMessageLoading.Should().BeNullOrWhiteSpace();
        }

        [TestMethod]
        public void ValidateLoadingJsonFilePathLoadNotNull()
        {
            systemUnderTest.JsonFilePathLoad = "Test path";

            FluentActions.Invoking(() => systemUnderTest.ValidateLoading())
               .Should()
               .NotThrow();

            systemUnderTest.FailedValidationLoading.Should().BeFalse();
            systemUnderTest.FailedValidationLoadingMessage.Contains("Json file path is empty").Should().BeFalse();
            systemUnderTest.FailedValidationLoadingMessage.Contains("Loading Success").Should().BeTrue();
            systemUnderTest.SuccessValidationMessageLoading.Contains("Loading Success").Should().BeTrue();
        }
    }
}