using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Model.Tests
{
    [TestClass]
    public class FilterSettingsTests
    {
        private FilterSettings systemUnderTest;

        [TestInitialize]
        public void Setup()
        {
            systemUnderTest = new FilterSettings();
        }

        [TestMethod]
        public void ValidateFailure()
        {
            FluentActions.Invoking(() => systemUnderTest.ValidateFailure())
                          .Should()
                          .NotThrow();
        }
    }
}