using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Capgemini.Xrm.CdsDataMigratorLibrary.Exceptions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.Xrm.CdsDataMigrator.Tests.Unit.Exceptions
{
    [TestClass]
    public class OrganizationalServiceExceptionTests
    {
        private OrganizationalServiceException systemUnderTest;

        [TestInitialize]
        public void Setup()
        {
            systemUnderTest = new OrganizationalServiceException();
        }

        [TestMethod]
        public void OrganizationalServiceExceptionDefaultConstructor()
        {
            FluentActions.Invoking(() => systemUnderTest = new OrganizationalServiceException())
                 .Should()
                 .NotThrow();
        }

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
        public void OrganizationalServiceExceptionnWithMessageAndInnerException()
        {
            var message = "Test message";

            FluentActions.Invoking(() => systemUnderTest = new OrganizationalServiceException(message, new Exception()))
                 .Should()
                 .NotThrow();

            Assert.AreEqual(message, systemUnderTest.Message);
            Assert.IsNotNull(systemUnderTest.InnerException);
        }

        [TestMethod]
        public void OrganizationalServiceExceptionSerialization()
        {
            var message = "Test message";

            var exception = new OrganizationalServiceException(message);

            var actual = SerializeToBytes(exception);

            actual.Length.Should().BeGreaterThan(0);
        }

        [TestMethod]
        public void OrganizationalServiceExceptionDeserialization()
        {
            var message = "Test message";

            var exception = new OrganizationalServiceException(message);
            var bytes = SerializeToBytes(exception);
            bytes.Length.Should().BeGreaterThan(0);

            var actual = DeserializeFromBytes(bytes);

            actual.Message.Should().Be(message);
            actual.InnerException.Should().BeNull();
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