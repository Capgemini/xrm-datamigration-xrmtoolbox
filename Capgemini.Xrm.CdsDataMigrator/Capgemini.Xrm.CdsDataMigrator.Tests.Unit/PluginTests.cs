using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyXrmToolBoxPlugin3.Tests
{
    [TestClass]
    public class PluginTests
    {
        [TestMethod]
        public void GetControl()
        {
            var systemUnderTest = new Plugin();

            var actual = systemUnderTest.GetControl();

            actual.Should().NotBeNull();
            actual.Should().BeOfType<MyPluginControl>();
        }

        [TestMethod]
        public void PluginInstantiation()
        {
            FluentActions.Invoking(() => new Plugin())
                         .Should()
                         .NotThrow();
        }
    }
}