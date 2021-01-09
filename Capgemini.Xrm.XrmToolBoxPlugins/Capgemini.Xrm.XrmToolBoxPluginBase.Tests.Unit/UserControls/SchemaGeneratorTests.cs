using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Tests
{
    [ExcludeFromCodeCoverage]
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

        [TestMethod]
        public void OnConnectionUpdated()
        {
            Assert.Fail();
        }
    }
}