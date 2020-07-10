using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Model.Tests
{
    [TestClass]
    public class ImportConfigSettingscsTests
    {
        private ImportConfigSettingscs systemUnderTest;

        [TestInitialize]
        public void Setup()
        {
            systemUnderTest = new ImportConfigSettingscs();
        }

        [TestMethod]
        public void ValidateAll()
        {
            FluentActions.Invoking(() => systemUnderTest.ValidateAll())
                          .Should()
                          .NotThrow();
        }

        [TestMethod]
        public void ValidateLoading()
        {
            FluentActions.Invoking(() => systemUnderTest.ValidateLoading())
                          .Should()
                          .NotThrow();
        }
    }
}