using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Model;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.Xrm.CdsDataMigrator.Tests.Unit.Model
{
    [TestClass]
    public class DeserializationSettingsTests
    {
        private DeserializationSettings systemUnderTest;

        [TestInitialize]
        public void TestSetup()
        {
            systemUnderTest = new DeserializationSettings();
        }

        [TestMethod]
        public void ValidateXmlFolderPathNull()
        {
            systemUnderTest.XmlFolderPath = null;

            systemUnderTest.Validate();

            systemUnderTest.FailedValidation.Should().BeTrue();
            systemUnderTest.FailedValidationMessage.Should().Contain("Enter schema folder path");
        }

        [TestMethod]
        public void Validate()
        {
            systemUnderTest.XmlFolderPath = "XmlFolderPath";

            systemUnderTest.Validate();

            systemUnderTest.FailedValidation.Should().BeFalse();
            systemUnderTest.FailedValidationMessage.Should().BeNullOrWhiteSpace();
        }
    }
}