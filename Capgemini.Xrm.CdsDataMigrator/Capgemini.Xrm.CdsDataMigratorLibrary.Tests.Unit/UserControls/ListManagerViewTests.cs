using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.UserControls.Tests
{
    [TestClass]
    public class ListManagerViewTests
    {
        [TestMethod]
        public void ListManagerView()
        {
            using (var systemUnderTest = new ListManagerView())
            {
                systemUnderTest.ListView.Items.Count.Should().Be(0);
            }
        }

        [TestMethod]
        public void SetDisplayedItemsName()
        {
            var displayName = "Test value";

            using (var systemUnderTest = new ListManagerView())
            {
                systemUnderTest.DisplayedItemsName = displayName;

                systemUnderTest.DisplayedItemsName.Should().Be(displayName);
            }
        }
    }
}