using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Model.Tests
{
    [TestClass]
    public class RecordCountModelTests
    {
        private RecordCountModel systemUnderTest;

        [TestMethod]
        public void RecordCountModelInstantiation()
        {
            FluentActions.Invoking(() => systemUnderTest = new RecordCountModel())
                        .Should()
                        .NotThrow();

            systemUnderTest.RecordCount.Should().Be(0);
        }
    }
}