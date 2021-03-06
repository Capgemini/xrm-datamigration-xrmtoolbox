﻿using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using Capgemini.Xrm.CdsDataMigratorLibrary.Enums;
using Capgemini.Xrm.CdsDataMigratorLibrary.Services;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.Xrm.CdsDataMigrator.Tests.Unit.Services
{
    [TestClass]
    public class LoggerServiceTests
    {
        private const string Message = "Test message";
        private const string DateFormat = "dd-MMM-yyyy";

        private LoggerService systemUnderTest;

        [TestMethod]
        public void MessageLogger()
        {
            using (var textBox = new TextBox())
            {
                SynchronizationContext syncContext = SynchronizationContext.Current;

                FluentActions.Invoking(() => systemUnderTest = new LoggerService(textBox, syncContext))
                            .Should()
                            .NotThrow();
            }
        }

        [TestMethod]
        public void ErrorWhenVerboseLogLevel()
        {
            var expectedTimeStamp = $"{DateTime.Now.ToString(DateFormat, CultureInfo.InvariantCulture)} ";
            var expectedMessage = $"- Error:{Message}";

            using (var textBox = new TextBox())
            {
                systemUnderTest = new LoggerService(textBox, SynchronizationContext.Current)
                {
                    LogLevel = LogLevel.Verbose
                };

                FluentActions.Invoking(() => systemUnderTest.LogError(Message))
                            .Should()
                            .NotThrow();

                textBox.Text.Should().Contain(expectedMessage);
                textBox.Text.Should().Contain(expectedTimeStamp);
            }
        }

        [TestMethod]
        public void ErrorLogLevel()
        {
            var expectedTimeStamp = $"{DateTime.Now.ToString(DateFormat, CultureInfo.InvariantCulture)} ";
            var expectedMessage = $"- Error:{Message}";

            using (var textBox = new TextBox())
            {
                systemUnderTest = new LoggerService(textBox, SynchronizationContext.Current)
                {
                    LogLevel = LogLevel.Error
                };

                FluentActions.Invoking(() => systemUnderTest.LogError(Message))
                            .Should()
                            .NotThrow();

                textBox.Text.Should().Contain(expectedMessage);
                textBox.Text.Should().Contain(expectedTimeStamp);
            }
        }

        [TestMethod]
        public void ErrorWithExceptionParameter()
        {
            var exception = new Exception("Sample exception");
            var expectedTimeStamp = $"{DateTime.Now.ToString(DateFormat, CultureInfo.InvariantCulture)} ";
            var expectedMessage = $"- Error:{Message}";

            using (var textBox = new TextBox())
            {
                systemUnderTest = new LoggerService(textBox, SynchronizationContext.Current);

                FluentActions.Invoking(() => systemUnderTest.LogError(Message, exception))
                            .Should()
                            .NotThrow();

                textBox.Text.Should().Contain(expectedMessage);
                textBox.Text.Should().Contain(expectedTimeStamp);
                textBox.Text.Should().Contain(exception.Message);
            }
        }

        [TestMethod]
        public void InfoLessThanInfoLogLevel()
        {
            using (var textBox = new TextBox())
            {
                systemUnderTest = new LoggerService(textBox, SynchronizationContext.Current)
                {
                    LogLevel = LogLevel.Warning
                };

                FluentActions.Invoking(() => systemUnderTest.LogInfo(Message))
                            .Should()
                            .NotThrow();

                textBox.Text.Should().Be(string.Empty);
            }
        }

        [TestMethod]
        public void InfoLogLevel()
        {
            var expectedTimeStamp = $"{DateTime.Now.ToString(DateFormat, CultureInfo.InvariantCulture)} ";
            var expectedMessage = $"- Info:{Message}";

            using (var textBox = new TextBox())
            {
                systemUnderTest = new LoggerService(textBox, SynchronizationContext.Current)
                {
                    LogLevel = LogLevel.Info
                };

                FluentActions.Invoking(() => systemUnderTest.LogInfo(Message))
                            .Should()
                            .NotThrow();

                textBox.Text.Should().Contain(expectedMessage);
                textBox.Text.Should().Contain(expectedTimeStamp);
            }
        }

        [TestMethod]
        public void VerboseLessThanVerboseLogLevel()
        {
            using (var textBox = new TextBox())
            {
                systemUnderTest = new LoggerService(textBox, SynchronizationContext.Current)
                {
                    LogLevel = LogLevel.Warning
                };

                FluentActions.Invoking(() => systemUnderTest.LogVerbose(Message))
                            .Should()
                            .NotThrow();

                textBox.Text.Should().Be(string.Empty);
            }
        }

        [TestMethod]
        public void VerboseLogLevel()
        {
            var expectedTimeStamp = $"{DateTime.Now.ToString(DateFormat, CultureInfo.InvariantCulture)} ";
            var expectedMessage = $"- Verbose:{Message}";

            using (var textBox = new TextBox())
            {
                systemUnderTest = new LoggerService(textBox, SynchronizationContext.Current)
                {
                    LogLevel = LogLevel.Verbose
                };

                FluentActions.Invoking(() => systemUnderTest.LogVerbose(Message))
                            .Should()
                            .NotThrow();

                textBox.Text.Should().Contain(expectedMessage);
                textBox.Text.Should().Contain(expectedTimeStamp);
            }
        }

        [TestMethod]
        public void WarningErrorLogLevel()
        {
            using (var textBox = new TextBox())
            {
                systemUnderTest = new LoggerService(textBox, SynchronizationContext.Current)
                {
                    LogLevel = LogLevel.Error
                };

                FluentActions.Invoking(() => systemUnderTest.LogWarning(Message))
                            .Should()
                            .NotThrow();

                textBox.Text.Should().Be(string.Empty);
            }
        }

        [TestMethod]
        public void WarningWarningLogLevel()
        {
            var expectedTimeStamp = $"{DateTime.Now.ToString(DateFormat, CultureInfo.InvariantCulture)} ";
            var expectedMessage = $"- Warning:{Message}";

            using (var textBox = new TextBox())
            {
                systemUnderTest = new LoggerService(textBox, SynchronizationContext.Current)
                {
                    LogLevel = LogLevel.Warning
                };

                FluentActions.Invoking(() => systemUnderTest.LogWarning(Message))
                            .Should()
                            .NotThrow();

                textBox.Text.Should().Contain(expectedMessage);
                textBox.Text.Should().Contain(expectedTimeStamp);
            }
        }
    }
}