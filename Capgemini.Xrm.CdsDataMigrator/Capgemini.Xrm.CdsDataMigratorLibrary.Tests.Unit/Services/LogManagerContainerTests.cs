using Capgemini.Xrm.CdsDataMigratorLibrary.Enums;
using Capgemini.Xrm.CdsDataMigratorLibrary.Services;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XrmToolBox.Extensibility;

namespace Capgemini.Xrm.CdsDataMigrator.Tests.Unit.Services
{
    [TestClass]
    public class LogManagerContainerTests
    {
        private LogManagerContainer systemUnderTest;
        private readonly string message = "Sample message";

        [Ignore("To be fixed!")]
        [TestMethod]
        public void CanInstantiate()
        {
            FluentActions.Invoking(() => systemUnderTest = new LogManagerContainer(new LogManager(typeof(LogManagerContainerTests))))
                        .Should()
                        .NotThrow();
        }

        [Ignore("To be fixed!")]
        [TestMethod]
        public void WriteLineVerbose()
        {
            systemUnderTest = new LogManagerContainer(new LogManager(typeof(LogManagerContainerTests)));

            FluentActions.Invoking(() => systemUnderTest.WriteLine(message, LogLevel.Verbose))
                        .Should()
                        .NotThrow();
        }
    }
}