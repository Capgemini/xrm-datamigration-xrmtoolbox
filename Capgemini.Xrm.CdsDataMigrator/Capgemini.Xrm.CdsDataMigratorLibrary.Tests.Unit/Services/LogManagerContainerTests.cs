using Capgemini.Xrm.CdsDataMigratorLibrary.Enums;
using Capgemini.Xrm.CdsDataMigratorLibrary.Services;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XrmToolBox.Extensibility;

namespace Capgemini.Xrm.CdsDataMigrator.Tests.Unit.Services
{
    [Ignore("To be fixed!")]
    [TestClass]
    public class LogManagerContainerTests
    {
        private LogManagerContainer systemUnderTest;
        private readonly string message = "Sample message";

        [TestMethod]
        public void CanInstantiate()
        {
            FluentActions.Invoking(() => systemUnderTest = new LogManagerContainer(new LogManager(typeof(LogManagerContainerTests))))
                        .Should()
                        .NotThrow();
        }

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