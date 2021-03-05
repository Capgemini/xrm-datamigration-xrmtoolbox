using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Tests
{
    [TestClass]
    public class SchemaGeneratorTests
    {
        [TestMethod]
        public void SchemaGeneratorInstatiation()
        {
            FluentActions.Invoking(() => new SchemaGenerator())
                        .Should()
                        .NotThrow();
        }
    }
}