using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Model.Tests
{
    [TestClass]
    public class LoadAllSettingsTests
    {
        private LoadAllSettings systemUnderTest;

        [TestInitialize]
        public void Setup()
        {
            systemUnderTest = new LoadAllSettings();
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