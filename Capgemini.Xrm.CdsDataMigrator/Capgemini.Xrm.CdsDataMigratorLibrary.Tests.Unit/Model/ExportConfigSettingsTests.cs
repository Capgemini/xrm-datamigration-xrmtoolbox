using Capgemini.Xrm.CdsDataMigratorLibrary.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.Xrm.CdsDataMigrator.Tests.Unit.Model
{
    [TestClass]
    public class ExportConfigSettingsTests
    {
        private ExportConfigSettings systemUnderTest;

        [TestInitialize]
        public void TestSetup()
        {
            systemUnderTest = new ExportConfigSettings();
        }

        [TestMethod]
        public void ValidateNullJsonFilePath()
        {
            systemUnderTest.JsonFilePath = null;

            systemUnderTest.Validate();

            systemUnderTest.FailedValidation.Should().BeTrue();
            systemUnderTest.FailedValidationLoading.Should().BeFalse();
            systemUnderTest.FailedValidationMessage.Should().Contain("Export config file path is empty");
            systemUnderTest.SuccessValidationMessageLoading.Should().BeNullOrWhiteSpace();
        }

        [TestMethod]
        public void Validate()
        {
            systemUnderTest.JsonFilePath = "JsonFilePath";

            systemUnderTest.Validate();

            systemUnderTest.FailedValidation.Should().BeFalse();
            systemUnderTest.FailedValidationLoading.Should().BeFalse();
            systemUnderTest.FailedValidationMessage.Should().NotContain("Export config file path is empty");
            systemUnderTest.SuccessValidationMessageLoading.Should().BeNullOrWhiteSpace();
        }

        [TestMethod]
        public void ValidateSuccessFailedValidationIsFalse()
        {
            systemUnderTest.FailedValidation = false;

            systemUnderTest.ValidateSuccess();

            systemUnderTest.FailedValidation.Should().BeFalse();
            systemUnderTest.FailedValidationLoading.Should().BeFalse();
            systemUnderTest.FailedValidationMessage.Should().NotContain("Export config file path is empty");
            systemUnderTest.SuccessValidationMessage.Should().Contain("Successfully created json file");
            systemUnderTest.SuccessValidationMessageLoading.Should().BeNullOrWhiteSpace();
        }

        [TestMethod]
        public void ValidateSuccessFailedValidationIsTrueAndJsonFilePathIsNull()
        {
            systemUnderTest.JsonFilePath = null;
            systemUnderTest.FailedValidation = true;

            systemUnderTest.ValidateSuccess();

            systemUnderTest.FailedValidation.Should().BeTrue();
            systemUnderTest.FailedValidationLoading.Should().BeFalse();
            systemUnderTest.FailedValidationMessage.Should().NotContain("Export config file path is empty");
            systemUnderTest.SuccessValidationMessage.Should().BeNullOrWhiteSpace();
            systemUnderTest.SuccessValidationMessageLoading.Should().BeNullOrWhiteSpace();
        }

        [TestMethod]
        public void ValidateSuccessFailedValidationIsTrueAndJsonFilePathIsNotEmpty()
        {
            systemUnderTest.JsonFilePath = "JsonFilePath";
            systemUnderTest.FailedValidation = true;

            systemUnderTest.ValidateSuccess();

            systemUnderTest.FailedValidation.Should().BeTrue();
            systemUnderTest.FailedValidationLoading.Should().BeFalse();
            systemUnderTest.FailedValidationMessage.Should().NotContain("Export config file path is empty");
            systemUnderTest.SuccessValidationMessage.Should().BeNullOrWhiteSpace();
            systemUnderTest.SuccessValidationMessageLoading.Should().BeNullOrWhiteSpace();
        }

        [TestMethod]
        public void ValidateLoadingJsonFilePathLoadNull()
        {
            systemUnderTest.JsonFilePathLoad = null;

            systemUnderTest.ValidateLoading();

            systemUnderTest.FailedValidation.Should().BeFalse();
            systemUnderTest.FailedValidationLoading.Should().BeTrue();
            systemUnderTest.FailedValidationMessage.Should().BeNullOrWhiteSpace();
            systemUnderTest.FailedValidationLoadingMessage.Should().Contain("Json file path is empty");
            systemUnderTest.SuccessValidationMessageLoading.Should().BeNullOrEmpty();
        }

        [TestMethod]
        public void ValidateLoadingJsonFilePathLoadNotEmpty()
        {
            systemUnderTest.JsonFilePathLoad = "JsonFilePathLoad";

            systemUnderTest.ValidateLoading();

            systemUnderTest.FailedValidation.Should().BeFalse();
            systemUnderTest.FailedValidationLoading.Should().BeFalse();
            systemUnderTest.FailedValidationMessage.Should().BeNullOrWhiteSpace();
            systemUnderTest.FailedValidationLoadingMessage.Should().Contain("Loading Success");
            systemUnderTest.SuccessValidationMessageLoading.Should().Contain("Loading Success");
        }
    }
}