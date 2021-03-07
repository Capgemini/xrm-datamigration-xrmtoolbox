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

            FluentActions.Invoking(() => new FilterEditor(currentfilter))
             .Should()
             .NotThrow();
        }
    }
}