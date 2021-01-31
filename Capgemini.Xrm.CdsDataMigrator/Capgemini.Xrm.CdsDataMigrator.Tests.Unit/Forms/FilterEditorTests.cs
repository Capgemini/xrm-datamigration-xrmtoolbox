using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Forms.Tests
{
    [TestClass]
    public class FilterEditorTests
    {
        [TestMethod]
        public void FilterEditorInstantiation()
        {
            string currentfilter = string.Empty;

            FluentActions.Invoking(() => new FilterEditor(currentfilter))
             .Should()
             .NotThrow();
        }
    }
}