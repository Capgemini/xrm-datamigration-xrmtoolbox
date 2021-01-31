using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core.Tests
{
    [TestClass]
    public class OrganisationsTests
    {
        [TestMethod]
        public void OrganisationsInstantiation()
        {
            Organisations systemUnderTest = new Organisations();

            systemUnderTest.Sortcolumns.Count.Should().Be(0);
            systemUnderTest.Mappings.Count.Should().Be(0);
            systemUnderTest.Entities.Count.Should().Be(0);
        }

        [TestMethod]
        public void OrganisationsEntitySettings()
        {
            var key1 = "item1";

            Organisations systemUnderTest = new Organisations();
            var sampleEntitySetting = new EntitySettings();
            sampleEntitySetting.Filter = "Testfilter";

            var item = new Item<string, EntitySettings>(key1, sampleEntitySetting);

            systemUnderTest.Entities.Add(new Item<string, EntitySettings>("item2", new EntitySettings()));
            systemUnderTest.Entities.Add(item);
            systemUnderTest.Entities.Add(new Item<string, EntitySettings>("item3", new EntitySettings()));

            var actual = systemUnderTest[key1];

            actual.Filter.Should().Be("Testfilter");
        }

        [TestMethod]
        public void OrganisationsNpnExistingEntitySettings()
        {
            var key1 = "item1";

            Organisations systemUnderTest = new Organisations();
            var sampleEntitySetting = new EntitySettings();
            sampleEntitySetting.Filter = "Testfilter";

            var item = new Item<string, EntitySettings>(key1, sampleEntitySetting);

            systemUnderTest.Entities.Add(new Item<string, EntitySettings>("item2", new EntitySettings()));
            systemUnderTest.Entities.Add(item);
            systemUnderTest.Entities.Add(new Item<string, EntitySettings>("item3", new EntitySettings()));

            var actual = systemUnderTest["item4"];

            actual.Filter.Should().BeNullOrEmpty();
            actual.UnmarkedAttributes.Count.Should().Be(0);
        }
    }
}