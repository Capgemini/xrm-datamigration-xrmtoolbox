using System;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Exceptions;
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
    }
}