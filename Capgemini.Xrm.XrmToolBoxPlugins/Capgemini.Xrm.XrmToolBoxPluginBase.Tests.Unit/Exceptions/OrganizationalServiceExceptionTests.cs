using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Exceptions.Tests
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
    }
}