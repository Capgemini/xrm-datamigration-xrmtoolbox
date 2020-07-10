using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Extensions.Tests
{
    [TestClass]
    public class ExceptionExtensionsTests
    {
        [TestMethod]
        public void ThrowArgumentNullExceptionIfNullTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void ThrowIfNullTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void ThrowArgumentOutOfRangeExceptionIfTrueTest()
        {
            Assert.Fail();
        }
    }
}