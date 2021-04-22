using Capgemini.Xrm.CdsDataMigratorLibrary.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.Xrm.CdsDataMigrator.Tests.Unit.Model
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
            systemUnderTest.EntityName.Should().BeNullOrEmpty();
        }
    }
}