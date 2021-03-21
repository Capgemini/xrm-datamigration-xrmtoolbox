using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Forms;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.Xrm.CdsDataMigrator.Tests.Unit.Forms
{
    [TestClass]
    public class FilterEditorTests
    {
        [TestMethod]
        public void FilterEditorInstantiation()
        {
            string currentfilter = string.Empty;

            FluentActions.Invoking(() => new FilterEditor(currentfilter, System.Windows.Forms.FormStartPosition.CenterParent))
             .Should()
             .NotThrow();
        }

        [TestMethod]
        public void FilterAppliesTrim()
        {
            string currentfilter = " TestValue  ";

            using (var systemUnderTest = new FilterEditor(currentfilter, System.Windows.Forms.FormStartPosition.CenterParent))
            {
                systemUnderTest.Filter.Should().Be("TestValue");
                systemUnderTest.QueryString.Should().BeNullOrEmpty();
            }
        }

        [TestMethod]
        public void FilterZeroLengthFilter()
        {
            string currentfilter = string.Empty;

            using (var systemUnderTest = new FilterEditor(currentfilter, System.Windows.Forms.FormStartPosition.CenterParent))
            {
                systemUnderTest.Filter.Should().Be(currentfilter);
                systemUnderTest.QueryString.Should().BeNullOrEmpty();
            }
        }

        [TestMethod]
        public void FilterContainsNoLessThanCharacter()
        {
            string currentfilter = "TestValue";

            using (var systemUnderTest = new FilterEditor(currentfilter, System.Windows.Forms.FormStartPosition.CenterParent))
            {
                systemUnderTest.Filter.Should().Be(currentfilter);
                systemUnderTest.QueryString.Should().BeNullOrEmpty();
            }
        }

        [TestMethod]
        public void FilterContainsFetchXml()
        {
            var currentfilter = "< filter type =\"and\" > < condition attribute =\"sw_appointmentstatus\" operator=\"eq\" value=\"266880017\" /></ filter >";

            using (var systemUnderTest = new FilterEditor(currentfilter, System.Windows.Forms.FormStartPosition.CenterParent))
            {
                systemUnderTest.Filter.Should().Be(currentfilter);
            }
        }
    }
}