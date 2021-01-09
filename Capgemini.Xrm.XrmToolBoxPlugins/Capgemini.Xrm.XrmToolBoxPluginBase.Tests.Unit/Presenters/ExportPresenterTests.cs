using System.Diagnostics.CodeAnalysis;
using Capgemini.DataMigration.Core;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Views;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Presenters.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class ExportPresenterTests
    {
        private IExportView exportView;

        [TestInitialize]
        public void TestSetup()
        {
            exportView = new Mock<IExportView>().Object;
        }

        [TestMethod]
        [Ignore("TODO: fix")]
        public void ExportPresenterIntantiation()
        {
            Assert.Fail();
        }
    }
}