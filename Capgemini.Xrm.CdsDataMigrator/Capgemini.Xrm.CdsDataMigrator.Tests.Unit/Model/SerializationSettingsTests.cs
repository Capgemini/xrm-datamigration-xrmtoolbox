using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Model;
using Capgemini.Xrm.DataMigration.Model;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.Xrm.CdsDataMigrator.Tests.Unit.Model
{
    [TestClass]
    public class SerializationSettingsTests
    {
        private SerializationSettings systemUnderTest;

        [TestInitialize]
        public void TestSetup()
        {
            systemUnderTest = new SerializationSettings();
        }

        [TestMethod]
        public void SerializationSettingsInstantiation()
        {
            FluentActions.Invoking(() => new SerializationSettings())
                        .Should()
                        .NotThrow();
        }

        [TestMethod]
        public void ValidateAllNullXmlFilePath()
        {
            systemUnderTest.XmlFilePath = null;

            systemUnderTest.ValidateAll();

            systemUnderTest.FailedValidation.Should().BeTrue();
            systemUnderTest.SuccessValidationMessage.Should().BeNull();
            systemUnderTest.FailedValidationMessage.Should().Contain("Select file path");
            systemUnderTest.FailedValidationMessage.Should().Contain("Select entity");
        }

        [TestMethod]
        public void ValidateAllXmlFilePath()
        {
            systemUnderTest.XmlFilePath = "XmlFilePath";

            systemUnderTest.ValidateAll();

            systemUnderTest.FailedValidation.Should().BeTrue();
            systemUnderTest.SuccessValidationMessage.Should().NotContain("Successfully created XML file");
            systemUnderTest.FailedValidationMessage.Should().NotContain("Select file path");
            systemUnderTest.FailedValidationMessage.Should().Contain("Select entity");
        }

        [TestMethod]
        public void ValidateAllXmlFilePathEntityContainsItems()
        {
            systemUnderTest.XmlFilePath = "XmlFilePath";
            systemUnderTest.Entity.Add(new CrmEntity());

            systemUnderTest.ValidateAll();

            systemUnderTest.FailedValidation.Should().BeFalse();
            systemUnderTest.SuccessValidationMessage.Should().Contain("Successfully created XML file");
            systemUnderTest.FailedValidationMessage.Should().NotContain("Select file path");
            systemUnderTest.FailedValidationMessage.Should().NotContain("Select entity");
        }
    }
}