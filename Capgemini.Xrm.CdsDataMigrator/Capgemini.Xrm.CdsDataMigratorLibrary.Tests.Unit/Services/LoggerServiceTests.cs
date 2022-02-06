﻿using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using Capgemini.Xrm.CdsDataMigratorLibrary.Enums;
using Capgemini.Xrm.CdsDataMigratorLibrary.Services;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Capgemini.Xrm.CdsDataMigrator.Tests.Unit.Services
{
    [TestClass]
    public class LoggerServiceTests
    {
        private const string Message = "Test message";
        private const string DateFormat = "dd-MMM-yyyy";

        private LoggerService systemUnderTest;
        private Mock<ILogManagerContainer> logManagerContainerMock;

        [TestInitialize]
        public void Setup()
        {
            logManagerContainerMock = new Mock<ILogManagerContainer>();
        }

        [TestMethod]
        public void MessageLogger()
        {
            using (var textBox = new TextBox())
            {
                SynchronizationContext syncContext = SynchronizationContext.Current;

                FluentActions.Invoking(() => systemUnderTest = new LoggerService(textBox, syncContext, logManagerContainerMock.Object))
                            .Should()
                            .NotThrow();

                logManagerContainerMock.VerifyAll();
            }
        }

        [TestMethod]
        public void ErrorWhenVerboseLogLevel()
        {
            var expectedTimeStamp = $"{DateTime.Now.ToString(DateFormat, CultureInfo.InvariantCulture)} ";
            var expectedMessage = $"- Error:{Message}";

            logManagerContainerMock.Setup(x => x.WriteLine(It.IsAny<string>(), LogLevel.Error));

            using (var textBox = new TextBox())
            {
                systemUnderTest = new LoggerService(textBox, SynchronizationContext.Current, logManagerContainerMock.Object)
                {
                    LogLevel = LogLevel.Verbose
                };

                FluentActions.Invoking(() => systemUnderTest.LogError(Message))
                            .Should()
                            .NotThrow();

                textBox.Text.Should().Contain(expectedMessage);
                textBox.Text.Should().Contain(expectedTimeStamp);

                logManagerContainerMock.VerifyAll();
            }
        }

        [Ignore("To be fixed!")]
        [TestMethod]
        public void ErrorLogLevel()
        {
            var expectedTimeStamp = $"{DateTime.Now.ToString(DateFormat, CultureInfo.InvariantCulture)} ";
            var expectedMessage = $"- Error:{Message}";

            logManagerContainerMock.Setup(x => x.WriteLine(It.IsAny<string>(), LogLevel.Error));

            using (var textBox = new TextBox())
            {
                systemUnderTest = new LoggerService(textBox, SynchronizationContext.Current, logManagerContainerMock.Object)
                {
                    LogLevel = LogLevel.Error
                };

                FluentActions.Invoking(() => systemUnderTest.LogError(Message))
                            .Should()
                            .NotThrow();

                textBox.Text.Should().Contain(expectedMessage);
                textBox.Text.Should().Contain(expectedTimeStamp);

                logManagerContainerMock.VerifyAll();
            }
        }

        [Ignore("To be fixed!")]
        [TestMethod]
        public void ErrorWithExceptionParameter()
        {
            var exception = new Exception("Sample exception");
            var expectedTimeStamp = $"{DateTime.Now.ToString(DateFormat, CultureInfo.InvariantCulture)} ";
            var expectedMessage = $"- Error:{Message}";

            logManagerContainerMock.Setup(x => x.WriteLine(It.IsAny<string>(), LogLevel.Error));

            using (var textBox = new TextBox())
            {
                systemUnderTest = new LoggerService(textBox, SynchronizationContext.Current, logManagerContainerMock.Object);

                FluentActions.Invoking(() => systemUnderTest.LogError(Message, exception))
                            .Should()
                            .NotThrow();

                textBox.Text.Should().Contain(expectedMessage);
                textBox.Text.Should().Contain(expectedTimeStamp);
                textBox.Text.Should().Contain(exception.Message);

                logManagerContainerMock.VerifyAll();
            }
        }

        [Ignore("To be fixed!")]
        [TestMethod]
        public void InfoLessThanInfoLogLevel()
        {
            using (var textBox = new TextBox())
            {
                systemUnderTest = new LoggerService(textBox, SynchronizationContext.Current, logManagerContainerMock.Object)
                {
                    LogLevel = LogLevel.Warning
                };

                FluentActions.Invoking(() => systemUnderTest.LogInfo(Message))
                            .Should()
                            .NotThrow();

                textBox.Text.Should().Be(string.Empty);

                logManagerContainerMock.Verify(x => x.WriteLine(It.IsAny<string>(), LogLevel.Info), Times.Never);
            }
        }

        [Ignore("To be fixed!")]
        [TestMethod]
        public void InfoLogLevel()
        {
            var expectedTimeStamp = $"{DateTime.Now.ToString(DateFormat, CultureInfo.InvariantCulture)} ";
            var expectedMessage = $"- Info:{Message}";
            logManagerContainerMock.Setup(x => x.WriteLine(It.IsAny<string>(), LogLevel.Info));

            using (var textBox = new TextBox())
            {
                systemUnderTest = new LoggerService(textBox, SynchronizationContext.Current, logManagerContainerMock.Object)
                {
                    LogLevel = LogLevel.Info
                };

                FluentActions.Invoking(() => systemUnderTest.LogInfo(Message))
                            .Should()
                            .NotThrow();

                textBox.Text.Should().Contain(expectedMessage);
                textBox.Text.Should().Contain(expectedTimeStamp);

                logManagerContainerMock.VerifyAll();
            }
        }

        [Ignore("To be fixed!")]
        [TestMethod]
        public void VerboseLessThanVerboseLogLevel()
        {
            using (var textBox = new TextBox())
            {
                systemUnderTest = new LoggerService(textBox, SynchronizationContext.Current, logManagerContainerMock.Object)
                {
                    LogLevel = LogLevel.Warning
                };

                FluentActions.Invoking(() => systemUnderTest.LogVerbose(Message))
                            .Should()
                            .NotThrow();

                textBox.Text.Should().Be(string.Empty);

                logManagerContainerMock.Verify(x => x.WriteLine(It.IsAny<string>(), LogLevel.Verbose), Times.Never);
            }
        }

        [Ignore("To be fixed!")]
        [TestMethod]
        public void VerboseLogLevel()
        {
            var expectedTimeStamp = $"{DateTime.Now.ToString(DateFormat, CultureInfo.InvariantCulture)} ";
            var expectedMessage = $"- Verbose:{Message}";

            logManagerContainerMock.Setup(x => x.WriteLine(It.IsAny<string>(), LogLevel.Verbose));

            using (var textBox = new TextBox())
            {
                systemUnderTest = new LoggerService(textBox, SynchronizationContext.Current, logManagerContainerMock.Object)
                {
                    LogLevel = LogLevel.Verbose
                };

                FluentActions.Invoking(() => systemUnderTest.LogVerbose(Message))
                            .Should()
                            .NotThrow();

                textBox.Text.Should().Contain(expectedMessage);
                textBox.Text.Should().Contain(expectedTimeStamp);

                logManagerContainerMock.VerifyAll();
            }
        }

        [Ignore("To be fixed!")]
        [TestMethod]
        public void WarningErrorLogLevel()
        {
            using (var textBox = new TextBox())
            {
                systemUnderTest = new LoggerService(textBox, SynchronizationContext.Current, logManagerContainerMock.Object)
                {
                    LogLevel = LogLevel.Error
                };

                FluentActions.Invoking(() => systemUnderTest.LogWarning(Message))
                            .Should()
                            .NotThrow();

                textBox.Text.Should().Be(string.Empty);

                logManagerContainerMock.Verify(x => x.WriteLine(It.IsAny<string>(), LogLevel.Warning), Times.Never);
            }
        }

        [Ignore("To be fixed!")]
        [TestMethod]
        public void WarningWarningLogLevel()
        {
            var expectedTimeStamp = $"{DateTime.Now.ToString(DateFormat, CultureInfo.InvariantCulture)} ";
            var expectedMessage = $"- Warning:{Message}";

            logManagerContainerMock.Setup(x => x.WriteLine(It.IsAny<string>(), LogLevel.Warning));

            using (var textBox = new TextBox())
            {
                systemUnderTest = new LoggerService(textBox, SynchronizationContext.Current, logManagerContainerMock.Object)
                {
                    LogLevel = LogLevel.Warning
                };

                FluentActions.Invoking(() => systemUnderTest.LogWarning(Message))
                            .Should()
                            .NotThrow();

                textBox.Text.Should().Contain(expectedMessage);
                textBox.Text.Should().Contain(expectedTimeStamp);

                logManagerContainerMock.VerifyAll();
            }
        }
    }
}