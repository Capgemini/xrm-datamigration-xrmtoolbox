using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Model.Tests
{
    [TestClass]
    public class DeserializationSettingsTests
    {
        private DeserializationSettings systemUnderTest;

        [TestInitialize]
        public void Setup()
        {
            systemUnderTest = new DeserializationSettings();
        }

        [TestMethod]
        public void Validate()
        {
            FluentActions.Invoking(() => systemUnderTest.Validate())
                .Should()
                .NotThrow();

            systemUnderTest.FailedValidation.Should().BeTrue();
            systemUnderTest.FailedValidationMessage.Contains("Enter schema folder path").Should().BeTrue();
        }

        [TestMethod]
        public void ValidateXmlFolderPathNotNullOrEmpty()
        {
            systemUnderTest.XmlFolderPath = "Test";

            FluentActions.Invoking(() => systemUnderTest.Validate())
                .Should()
                .NotThrow();

            systemUnderTest.FailedValidation.Should().BeFalse();
            systemUnderTest.FailedValidationMessage.Should().BeNullOrWhiteSpace();
        }
    }
}