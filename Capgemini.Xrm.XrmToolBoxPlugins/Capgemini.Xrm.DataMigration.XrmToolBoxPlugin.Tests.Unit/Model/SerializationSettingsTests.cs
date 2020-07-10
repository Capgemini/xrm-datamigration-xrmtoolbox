using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Model.Tests
{
    [TestClass]
    public class SerializationSettingsTests
    {
        private SerializationSettings systemUnderTest;

        [TestInitialize]
        public void Setup()
        {
            systemUnderTest = new SerializationSettings();
        }

        [TestMethod]
        public void ValidateAll()
        {
            FluentActions.Invoking(() => systemUnderTest.ValidateAll())
                          .Should()
                          .NotThrow();
        }
    }
}