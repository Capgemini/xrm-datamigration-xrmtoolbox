using Capgemini.Xrm.CdsDataMigratorLibrary.Core;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.Xrm.CdsDataMigrator.Tests.Unit.Core
{
    [TestClass]
    public class EntitySettingsTests
    {
        [TestMethod]
        public void EntitySettingsInstantiation()
        {
            var systemUnderTest = new EntitySettings();

            systemUnderTest.UnmarkedAttributes.Should().NotBeNull();
            systemUnderTest.Filter.Should().BeNullOrEmpty();
        }
    }
}