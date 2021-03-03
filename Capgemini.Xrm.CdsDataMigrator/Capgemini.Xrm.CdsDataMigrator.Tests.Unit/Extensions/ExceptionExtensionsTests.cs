using System;
using System.Diagnostics.CodeAnalysis;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Extensions.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class ExceptionExtensionsTests
    {
        [TestMethod]
        public void ThrowIfNullForNullString()
        {
            string message = "Test message";
            string testVariable = null;

            FluentActions.Invoking(() => testVariable.ThrowIfNull<Exception>(message))
                         .Should()
                         .Throw<Exception>()
                         .WithMessage(message);
        }

        [TestMethod]
        public void ThrowIfNullForNonNullString()
        {
            string message = "Test message";
            string testVariable = "Test Variable";

            FluentActions.Invoking(() => testVariable.ThrowIfNull<Exception>(message))
                         .Should()
                         .NotThrow();
        }

        [TestMethod]
        public void ThrowIfNullForNullObject()
        {
            string message = "Test message";
            EntitySettings testVariable = null;

            FluentActions.Invoking(() => testVariable.ThrowIfNull<Exception>(message))
                         .Should()
                         .Throw<Exception>()
                         .WithMessage(message);
        }

        [TestMethod]
        public void ThrowIfNullForNonNullObject()
        {
            string message = "Test message";
            EntitySettings testVariable = new EntitySettings();

            FluentActions.Invoking(() => testVariable.ThrowIfNull<Exception>(message))
                         .Should()
                         .NotThrow();
        }

        [TestMethod]
        public void ThrowArgumentNullExceptionIfNull()
        {
            string message = "Test message";
            EntitySettings testVariable = null;

            FluentActions.Invoking(() => testVariable.ThrowArgumentNullExceptionIfNull(nameof(testVariable), message))
                         .Should()
                         .Throw<ArgumentNullException>()
                         .Where(a => a.Message.Contains(message) && a.Message.Contains(nameof(testVariable)));
        }

        [TestMethod]
        public void ThrowArgumentNullExceptionIfNullReturnsDefaultMessage()
        {
            string message = "input parameter should not be null!";
            EntitySettings testVariable = null;

            FluentActions.Invoking(() => testVariable.ThrowArgumentNullExceptionIfNull(nameof(testVariable)))
                         .Should()
                         .Throw<ArgumentNullException>()
                         .Where(a => a.Message.Contains(message) && a.Message.Contains(nameof(testVariable)));
        }

        [TestMethod]
        public void ThrowArgumentNullExceptionIfNullSucceeds()
        {
            string message = "Test message";
            EntitySettings testVariable = new EntitySettings();

            FluentActions.Invoking(() => testVariable.ThrowArgumentNullExceptionIfNull(nameof(testVariable), message))
                         .Should()
                         .NotThrow();
        }

        [TestMethod]
        public void ThrowArgumentOutOfRangeExceptionIfTrue()
        {
            string message = "Test message";
            bool testVariable = true;

            FluentActions.Invoking(() => testVariable.ThrowArgumentOutOfRangeExceptionIfTrue(nameof(testVariable), message))
                         .Should()
                         .Throw<ArgumentOutOfRangeException>()
                         .Where(a => a.Message.Contains(message) && a.Message.Contains(nameof(testVariable)));
        }

        [TestMethod]
        public void ThrowArgumentOutOfRangeExceptionIfTrueDoesNotThrow()
        {
            string message = "Test message";
            bool testVariable = false;

            FluentActions.Invoking(() => testVariable.ThrowArgumentOutOfRangeExceptionIfTrue(nameof(testVariable), message))
                         .Should()
                         .NotThrow();
        }
    }
}