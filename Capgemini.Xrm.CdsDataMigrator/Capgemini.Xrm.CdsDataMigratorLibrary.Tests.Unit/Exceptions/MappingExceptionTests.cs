using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Capgemini.Xrm.CdsDataMigratorLibrary.Exceptions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.Xrm.CdsDataMigrator.Tests.Unit.Exceptions
{
    [TestClass]
    public class MappingExceptionTests
    {
        private MappingException systemUnderTest;

        [TestInitialize]
        public void Setup()
        {
            systemUnderTest = new MappingException();
        }

        [TestMethod]
        public void MappingExceptionDefaultConstructor()
        {
            FluentActions.Invoking(() => systemUnderTest = new MappingException())
                 .Should()
                 .NotThrow();
        }

        [TestMethod]
        public void MappingExceptionWithMessage()
        {
            var message = "Test message";

            FluentActions.Invoking(() => systemUnderTest = new MappingException(message))
                 .Should()
                 .NotThrow();

            Assert.AreEqual(message, systemUnderTest.Message);
        }

        [TestMethod]
        public void MappingExceptionWithMessageAndException()
        {
            var message = "Test message";

            FluentActions.Invoking(() => systemUnderTest = new MappingException(message, new Exception()))
                 .Should()
                 .NotThrow();

            Assert.AreEqual(message, systemUnderTest.Message);
            Assert.IsNotNull(systemUnderTest.InnerException);
        }

        [TestMethod]
        public void MappingExceptionSerialization()
        {
            var message = "Test message";

            var exception = new MappingException(message);

            var actual = SerializeToBytes(exception);

            actual.Length.Should().BeGreaterThan(0);
        }

        [TestMethod]
        public void MappingExceptionDeserialization()
        {
            var message = "Test message";

            var exception = new MappingException(message);
            var bytes = SerializeToBytes(exception);
            bytes.Length.Should().BeGreaterThan(0);

            var actual = DeserializeFromBytes(bytes);

            actual.Message.Should().Be(message);
            actual.InnerException.Should().BeNull();
        }

        private static byte[] SerializeToBytes(MappingException e)
        {
            using (var stream = new MemoryStream())
            {
                new BinaryFormatter().Serialize(stream, e);
                return stream.GetBuffer();
            }
        }

        private static MappingException DeserializeFromBytes(byte[] bytes)
        {
            using (var stream = new MemoryStream(bytes))
            {
                return (MappingException)new BinaryFormatter().Deserialize(stream);
            }
        }
    }
}