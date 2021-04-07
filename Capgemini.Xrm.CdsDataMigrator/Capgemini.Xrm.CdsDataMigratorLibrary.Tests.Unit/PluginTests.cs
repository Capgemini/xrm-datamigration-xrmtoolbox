using Capgemini.Xrm.DataMigration.XrmToolBox;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyXrmToolBoxPlugin3;

namespace Capgemini.Xrm.CdsDataMigrator.Tests.Unit
{
    [TestClass]
    public class PluginTests
    {
        [Ignore("To be fixed!")]
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