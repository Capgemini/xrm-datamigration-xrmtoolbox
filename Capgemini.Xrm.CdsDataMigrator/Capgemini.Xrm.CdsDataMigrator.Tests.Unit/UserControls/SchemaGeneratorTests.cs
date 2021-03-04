using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Tests
{
    [TestClass]
    public class SchemaGeneratorTests
    {
        [Ignore("To be fixed!")]
        [TestMethod]
        public void SchemaGeneratorInstatiation()
        {
            FluentActions.Invoking(() => new SchemaGenerator())
                        .Should()
                        .NotThrow();
        }
    }
}