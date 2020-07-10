using System.Windows.Forms;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core.Tests
{
    [TestClass]
    public class ListViewItemComparerTests
    {
        private ListViewItemComparer systemUnderTest;

        [TestMethod]
        public void ListViewItemComparer()
        {
            FluentActions.Invoking(() => systemUnderTest = new ListViewItemComparer())
                         .Should()
                         .NotThrow();
        }

        [TestMethod]
        public void ListViewItemComparerParameters()
        {
            FluentActions.Invoking(() => systemUnderTest = new ListViewItemComparer(1, SortOrder.Ascending))
                         .Should()
                         .NotThrow();
        }

        [TestMethod]
        public void CompareAscending()
        {
            using (var listView = new ListView())
            {
                for (int i = 0; i < 6; i++)
                {
                    listView.Items.Add($"Item{i}");
                }

                systemUnderTest = new ListViewItemComparer(0, SortOrder.Ascending);

                var actual = systemUnderTest.Compare(listView.Items[2], listView.Items[5]);

                actual.Should().Be(-1);
            }
        }

        [TestMethod]
        public void CompareDescending()
        {
            using (var listView = new ListView())
            {
                for (int i = 0; i < 6; i++)
                {
                    listView.Items.Add($"Item{i}");
                }

                systemUnderTest = new ListViewItemComparer(0, SortOrder.Descending);

                var actual = systemUnderTest.Compare(listView.Items[2], listView.Items[5]);

                actual.Should().Be(1);
            }
        }
    }
}