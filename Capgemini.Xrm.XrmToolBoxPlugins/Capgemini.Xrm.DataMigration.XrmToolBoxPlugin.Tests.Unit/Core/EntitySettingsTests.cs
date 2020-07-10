using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core.Tests
{
    [TestClass]
    public class EntitySettingsTests
    {
        private EntitySettings systemUnderTest;

        [TestInitialize]
        public void SetUp()
        {
            systemUnderTest = new EntitySettings();
        }

        [TestMethod]
        public void EntitySettings()
        {
            systemUnderTest.UnmarkedAttributes.Count.Should().Be(0);
            systemUnderTest.Filter.Should().Be(string.Empty);
        }
    }
}