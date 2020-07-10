using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Model.Tests
{
    [TestClass]
    public class SaveAllSettingsTests
    {
        private SaveAllSettings systemUnderTest;

        [TestInitialize]
        public void Setup()
        {
            systemUnderTest = new SaveAllSettings();
        }

        [TestMethod]
        public void Validate()
        {
            FluentActions.Invoking(() => systemUnderTest.Validate())
                          .Should()
                          .NotThrow();
        }
    }
}