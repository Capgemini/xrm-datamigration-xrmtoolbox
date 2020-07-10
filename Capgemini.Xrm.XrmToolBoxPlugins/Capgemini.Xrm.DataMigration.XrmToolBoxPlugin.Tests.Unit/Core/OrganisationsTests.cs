using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core.Tests
{
    [TestClass]
    public class OrganisationsTests
    {
        private Organisations systemUndertest;

        [TestMethod]
        public void Organisations()
        {
            FluentActions.Invoking(() => systemUndertest = new Organisations())
                            .Should()
                            .NotThrow();

            systemUndertest.Sortcolumns.Should().NotBeNull();
            systemUndertest.Mappings.Should().NotBeNull();
            systemUndertest.Entities.Should().NotBeNull();
        }

        [TestMethod]
        public void OrganisationsIndexer()
        {
            systemUndertest = new Organisations();

            for (int i = 0; i < 5; i++)
            {
                systemUndertest.Entities.Add(new Item<string, EntitySettings>($"testentity{i}", new EntitySettings()));
            }

            var actual = systemUndertest["testentity3"];

            actual.Should().NotBeNull();
        }
    }
}