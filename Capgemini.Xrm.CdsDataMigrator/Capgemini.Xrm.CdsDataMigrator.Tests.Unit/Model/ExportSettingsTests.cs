using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Models.Tests
{
    [TestClass]
    public class ExportSettingsTests
    {
        [TestMethod]
        public void CanInstantiate()
        {
            var systemUndertest = new ExportSettings();

            systemUndertest.DataFormat.Should().BeNullOrEmpty();
            systemUndertest.SavePath.Should().BeNullOrEmpty();
            systemUndertest.EnvironmentConnection.Should().BeNull();
            systemUndertest.ExportConfigPath.Should().BeNullOrEmpty();
            systemUndertest.SchemaPath.Should().BeNullOrEmpty();
            systemUndertest.ExportInactiveRecords.Should().BeFalse();
            systemUndertest.Minimize.Should().BeFalse();
            systemUndertest.BatchSize.Should().Be(0);
        }

        [TestMethod]
        public void CanInstantiateWithValue()
        {
            var testString = "test";

            var systemUndertest = new ExportSettings
            {
                DataFormat = testString,
                SavePath = testString,
                EnvironmentConnection = null,
                ExportConfigPath = testString,
                SchemaPath = testString,
                ExportInactiveRecords = true,
                Minimize = true,
                BatchSize = 5
            };

            systemUndertest.DataFormat.Should().Be(testString);
            systemUndertest.SavePath.Should().Be(testString);
            systemUndertest.EnvironmentConnection.Should().BeNull();
            systemUndertest.ExportConfigPath.Should().Be(testString);
            systemUndertest.SchemaPath.Should().Be(testString);
            systemUndertest.ExportInactiveRecords.Should().BeTrue();
            systemUndertest.Minimize.Should().BeTrue();
            systemUndertest.BatchSize.Should().Be(5);
        }
    }
}