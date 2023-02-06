using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Models.Tests
{
    [TestClass]
    public class MigratorEventArgsTests
    {
        [TestMethod]
        public void MigratorEventArgs()
        {
            MigratorEventArgs<string> migratorEventArgs = null;
            var input = "MigratorEventArgs";

            FluentActions.Invoking(() => migratorEventArgs = new MigratorEventArgs<string>(input))
                        .Should()
                        .NotThrow();

            migratorEventArgs.Input.Should().Be(input);
        }
    }
}