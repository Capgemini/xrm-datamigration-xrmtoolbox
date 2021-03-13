using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.Xrm.CdsDataMigrator.Tests.Unit.UserControls
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

        [Ignore("To be fix!")]
        [TestMethod]
        public void PopulateRelationshipActionTest()
        {
            Assert.Fail();
        }

        [Ignore("To be fix!")]
        [TestMethod]
        public void PopulateRelationshipTest()
        {
            Assert.Fail();
        }
    }
}