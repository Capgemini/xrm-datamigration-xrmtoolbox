using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Model.Tests
{
    [TestClass]
    public class ImportConfigSettingscsTests
    {
        private ImportConfigSettingscs systemUnderTest;

        [TestInitialize]
        public void TestSetup()
        {
            systemUnderTest = new ImportConfigSettingscs();
        }

        [TestMethod]
        public void ValidateAllNullJsonFilePath()
        {
            systemUnderTest.JsonFilePath = null;

            systemUnderTest.ValidateAll();

            systemUnderTest.FailedValidation.Should().BeTrue();
            systemUnderTest.FailedValidationLoading.Should().BeFalse();
            systemUnderTest.FailedValidationMessage.Should().Contain("Import config file path is empty");
            systemUnderTest.SuccessValidationMessage.Should().NotContain("Successfully created json file");
        }

        [TestMethod]
        public void ValidateAll()
        {
            systemUnderTest.JsonFilePath = "JsonFilePath";

            systemUnderTest.ValidateAll();

            systemUnderTest.FailedValidation.Should().BeFalse();
            systemUnderTest.FailedValidationLoading.Should().BeFalse();
            systemUnderTest.FailedValidationMessage.Should().NotContain("Import config file path is empty");
            systemUnderTest.SuccessValidationMessage.Should().Contain("Successfully created json file");
        }

        [TestMethod]
        public void ValidateLoadingNullJsonFilePathLoad()
        {
            systemUnderTest.JsonFilePathLoad = null;

            systemUnderTest.ValidateLoading();

            systemUnderTest.FailedValidationLoading.Should().BeTrue();
            systemUnderTest.FailedValidation.Should().BeFalse();
            systemUnderTest.FailedValidationLoadingMessage.Should().Contain("Json file path is empty");
            systemUnderTest.SuccessValidationMessageLoading.Should().NotContain("Loading Success");
        }

        [TestMethod]
        public void ValidateLoading()
        {
            systemUnderTest.JsonFilePathLoad = "JsonFilePathLoad";

            systemUnderTest.ValidateLoading();

            systemUnderTest.FailedValidationLoading.Should().BeFalse();
            systemUnderTest.FailedValidation.Should().BeFalse();
            systemUnderTest.FailedValidationLoadingMessage.Should().NotContain("Json file path is empty");
            systemUnderTest.SuccessValidationMessageLoading.Should().Contain("Loading Success");
        }
    }
}