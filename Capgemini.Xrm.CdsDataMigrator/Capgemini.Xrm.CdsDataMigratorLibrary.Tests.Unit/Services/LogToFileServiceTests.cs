using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using Capgemini.Xrm.CdsDataMigratorLibrary.Enums;
using Capgemini.Xrm.CdsDataMigratorLibrary.Services;
using Capgemini.Xrm.CdsDataMigratorLibrary.Tests.Unit.Mocks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Capgemini.Xrm.CdsDataMigrator.Tests.Unit.Services
{
    [TestClass]
    public class LogToFileServiceTests
    {
        private const string Message = "Test message";
        private const string DateFormat = "dd-MMM-yyyy";

        private readonly string expectedTimeStamp = $"{DateTime.Now.ToString(DateFormat, CultureInfo.InvariantCulture)} ";
        private readonly Mock<ILogManagerContainer> logManagerContainerMock = new Mock<ILogManagerContainer>();

        private LogToFileService systemUnderTest;

        [TestInitialize]
        public void Setup()
        {
            SynchronizationContext.SetSynchronizationContext(new TestSynchronizationContext());

            systemUnderTest = new LogToFileService(logManagerContainerMock.Object);
        }

        [TestMethod]
        public void MessageLogger()
        {
            FluentActions.Invoking(() => systemUnderTest = new LogToFileService(logManagerContainerMock.Object))
                        .Should()
                        .NotThrow();
        }

        [TestMethod]
        public void ErrorWhenVerboseLogLevel()
        {
            var expectedMessage = $"- Error:{Message}";

            systemUnderTest.LogLevel = LogLevel.Verbose;
            //};

            FluentActions.Invoking(() =>
            systemUnderTest.LogError(Message))
                        .Should()
                        .NotThrow();

            logManagerContainerMock
                .Verify(x => x.WriteLine(It.Is<string>(s => s.Contains(expectedMessage) && s.Contains(expectedTimeStamp)), LogLevel.Error));
        }

        [TestMethod]
        public void ErrorLogLevel()
        {
            var expectedMessage = $"- Error:{Message}";

            systemUnderTest.LogLevel = LogLevel.Error;

            FluentActions.Invoking(() => systemUnderTest.LogError(Message))
                        .Should()
                        .NotThrow();

            logManagerContainerMock
                .Verify(x => x.WriteLine(
                    It.Is<string>(s => s.Contains(expectedMessage) && s.Contains(expectedTimeStamp)), 
                    LogLevel.Error));
        }

        [TestMethod]
        public void ErrorWithExceptionParameter()
        {
            var exception = new Exception("Sample exception");
            var expectedMessage = $"- Error:{Message}";

            FluentActions.Invoking(() => systemUnderTest.LogError(Message, exception))
                        .Should()
                        .NotThrow();

            logManagerContainerMock
                .Verify(x => x.WriteLine(
                    It.Is<string>(s => s.Contains(expectedMessage) && s.Contains(expectedTimeStamp) && s.Contains(exception.Message)), 
                    LogLevel.Error));
        }

        [TestMethod]
        public void InfoLessThanInfoLogLevel()
        {
            systemUnderTest.LogLevel = LogLevel.Warning;

            FluentActions.Invoking(() => systemUnderTest.LogInfo(Message))
                        .Should()
                        .NotThrow();

            logManagerContainerMock.Verify(x => x.WriteLine(It.IsAny<string>(), LogLevel.Info), Times.Never);
        }

        [TestMethod]
        public void InfoLogLevel()
        {
            var expectedMessage = $"- Info:{Message}";
            systemUnderTest.LogLevel = LogLevel.Info;

            FluentActions.Invoking(() => systemUnderTest.LogInfo(Message))
                        .Should()
                        .NotThrow();

            logManagerContainerMock
                .Verify(x => x.WriteLine(
                    It.Is<string>(s => s.Contains(expectedMessage) && s.Contains(expectedTimeStamp)),
                    LogLevel.Info));
        }

        [TestMethod]
        public void VerboseLessThanVerboseLogLevel()
        {
            systemUnderTest.LogLevel = LogLevel.Warning;

            FluentActions.Invoking(() => systemUnderTest.LogVerbose(Message))
                        .Should()
                        .NotThrow();

            logManagerContainerMock.Verify(x => x.WriteLine(It.IsAny<string>(), LogLevel.Verbose), Times.Never);
        }

        [TestMethod]
        public void VerboseLogLevel()
        {
            var expectedMessage = $"- Verbose:{Message}";

            systemUnderTest.LogLevel = LogLevel.Verbose;

            FluentActions.Invoking(() => systemUnderTest.LogVerbose(Message))
                        .Should()
                        .NotThrow();

            logManagerContainerMock
                .Verify(x => x.WriteLine(
                    It.Is<string>(s => s.Contains(expectedMessage) && s.Contains(expectedTimeStamp)),
                    LogLevel.Verbose));
        }

        [TestMethod]
        public void WarningErrorLogLevel()
        {
            systemUnderTest.LogLevel = LogLevel.Error;

            FluentActions.Invoking(() => systemUnderTest.LogWarning(Message))
                        .Should()
                        .NotThrow();

            logManagerContainerMock.Verify(x => x.WriteLine(It.IsAny<string>(), LogLevel.Warning), Times.Never);
        }

        [TestMethod]
        public void WarningWarningLogLevel()
        {
            var expectedMessage = $"- Warning:{Message}";

            logManagerContainerMock.Setup(x => x.WriteLine(It.IsAny<string>(), LogLevel.Warning));

            systemUnderTest.LogLevel = LogLevel.Warning;

            FluentActions.Invoking(() => systemUnderTest.LogWarning(Message))
                        .Should()
                        .NotThrow();

            logManagerContainerMock
                .Verify(x => x.WriteLine(
                    It.Is<string>(s => s.Contains(expectedMessage) && s.Contains(expectedTimeStamp)),
                    LogLevel.Warning));
        }
    }
}