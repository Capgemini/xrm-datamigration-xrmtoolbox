using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core.Tests
{
    [TestClass]
    public class ItemTests
    {
        private Item<string, string> systemUnderTest;

        [TestMethod]
        public void ItemWithParameters()
        {
            var key = "Key";
            var value = "some value";

            systemUnderTest = new Item<string, string>(key, value);

            systemUnderTest.Key.Should().Be(key);
            systemUnderTest.Value.Should().Be(value);
        }
    }
}