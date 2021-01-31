using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core.Tests
{
    [TestClass]
    public class EntitySettingsTests
    {
        [TestMethod]
        public void EntitySettingsInstantiation()
        {
            EntitySettings systemUnderTest = new EntitySettings();

            systemUnderTest.UnmarkedAttributes.Should().NotBeNull();
            systemUnderTest.Filter.Should().BeNullOrEmpty();
        }
    }
}