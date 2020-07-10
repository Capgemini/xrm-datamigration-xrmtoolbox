using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Model.Tests
{
    [TestClass]
    public class RecordCountModelTests
    {
        [TestMethod]
        public void RecordCountModel()
        {
            FluentActions.Invoking(() => new RecordCountModel())
                          .Should()
                          .NotThrow();
        }
    }
}