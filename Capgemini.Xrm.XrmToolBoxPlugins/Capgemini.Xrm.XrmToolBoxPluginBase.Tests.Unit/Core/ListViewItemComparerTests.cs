using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core.Tests
{
    [TestClass]
    public class ListViewItemComparerTests
    {
        private ListViewItemComparer systemUnderTest;

        [TestInitialize]
        public void Setup()
        {
            systemUnderTest = new ListViewItemComparer();
        }

        [TestMethod]
        public void ListViewItemComparerInstantiation()
        {
            FluentActions.Invoking(() => new ListViewItemComparer())
                         .Should()
                         .NotThrow();
        }

        [TestMethod]
        public void ListViewItemComparerInstantiationWithParameters()
        {
            int column = 12;
            SortOrder order = SortOrder.Ascending;

            FluentActions.Invoking(() => new ListViewItemComparer(column, order))
                        .Should()
                        .NotThrow();
        }

        [TestMethod]
        public void CompareDescending()
        {
            ListViewItem firstItem = new ListViewItem();
            ListViewItem secondItem = new ListViewItem();

            firstItem.SubItems.Add(new ListViewItem.ListViewSubItem(firstItem, "Hello"));
            secondItem.SubItems.Add(new ListViewItem.ListViewSubItem(secondItem, "World"));

            systemUnderTest = new ListViewItemComparer(0, SortOrder.Descending);

            var actual = systemUnderTest.Compare(firstItem, secondItem);

            actual.Should().Be(0);
        }

        [TestMethod]
        public void CompareAscending()
        {
            ListViewItem firstItem = new ListViewItem();
            ListViewItem secondItem = new ListViewItem();

            firstItem.SubItems.Add(new ListViewItem.ListViewSubItem(firstItem, "Hello"));
            secondItem.SubItems.Add(new ListViewItem.ListViewSubItem(secondItem, "World"));

            systemUnderTest = new ListViewItemComparer(0, SortOrder.Ascending);

            var actual = systemUnderTest.Compare(firstItem, secondItem);

            actual.Should().Be(0);
        }
    }
}