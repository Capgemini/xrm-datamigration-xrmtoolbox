using Capgemini.Xrm.CdsDataMigratorLibrary.Exceptions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Capgemini.Xrm.CdsDataMigrator.Tests.Unit.Exceptions
{
    [TestClass]
    public class OrganizationalServiceExceptionTests
    {
        private OrganizationalServiceException systemUnderTest;

        [TestMethod]
        public void OrganizationalServiceExceptionConstructWithMessageParameter()
        {
            var message = "Test message";

            FluentActions.Invoking(() => systemUnderTest = new OrganizationalServiceException(message))
                 .Should()
                 .NotThrow();

            Assert.AreEqual(message, systemUnderTest.Message);
        }

        [TestMethod]
        public void OrganizationalServiceExceptionSerialization()
        {
            var message = "Test message";

            var exception = new OrganizationalServiceException(message);

            var actual = SerializeToBytes(exception);

            actual.Length.Should().BeGreaterThan(0);
        }

        private static byte[] SerializeToBytes(OrganizationalServiceException e)
        {
            using (var stream = new MemoryStream())
            {
                new BinaryFormatter().Serialize(stream, e);
                return stream.GetBuffer();
            }
        }

        private static OrganizationalServiceException DeserializeFromBytes(byte[] bytes)
        {
            using (var stream = new MemoryStream(bytes))
            {
                return (OrganizationalServiceException)new BinaryFormatter().Deserialize(stream);
            }
        }
    }
}