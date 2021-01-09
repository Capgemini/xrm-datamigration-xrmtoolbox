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
        // private Capgemini.DataMigration.Core.ILogger logger;

        [TestInitialize]
        public void TestSetup()
        {
            exportView = new Mock<IExportView>().Object;
            /// TODO: need to fix this
            // logger = new Mock<ILogger>().Object;
        }

        [TestMethod]
        [Ignore("Will fix this!!!")]
        public void ExportPresenterIntantiation()
        {
            /// TODO: need to fix this
            // FluentActions.Invoking(() => new ExportPresenter(exportView, logger))
            //            .Should()
            //            .NotThrow();
        }
    }
}