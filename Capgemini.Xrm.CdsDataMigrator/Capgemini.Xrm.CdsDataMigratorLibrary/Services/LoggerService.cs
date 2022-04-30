using System;
using System.Threading;
using System.Windows.Forms;
using Capgemini.Xrm.CdsDataMigratorLibrary.Enums;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Services
{
    public class LoggerService : ILogManager
    {
        private readonly SynchronizationContext syncContext;
        private readonly TextBox messageTextBox;
        private readonly ILogManagerContainer logManagerContainer;

        public LoggerService(TextBox messageTextBox, SynchronizationContext syncContext, ILogManagerContainer logManagerContainer)
        {
            this.messageTextBox = messageTextBox;
            this.syncContext = syncContext;
            this.logManagerContainer = logManagerContainer;
        }

        public LogLevel LogLevel { get; set; } = LogLevel.Info;

        public void LogError(string message)
        {
            WriteLine($"Error:{message}", LogLevel.Error);
        }

        public void LogError(string message, Exception ex)
        {
            WriteLine($"Error:{message},Ex:{ex}", LogLevel.Error);
        }

        public void LogInfo(string message)
        {
            if ((int)LogLevel > 1)
            {
                WriteLine($"Info:{message}", LogLevel.Info);
            }
        }

        public void LogVerbose(string message)
        {
            if ((int)LogLevel > 2)
            {
                WriteLine($"Verbose:{message}", LogLevel.Verbose);
            }
        }

        public void LogWarning(string message)
        {
            if (LogLevel > 0)
            {
                WriteLine($"Warning:{message}", LogLevel.Warning);
            }
        }

        private void WriteLine(string message, LogLevel logLevel)
        {
            syncContext.Send(
                p =>
            {
                var logMessage = $"{DateTime.Now:dd-MMM-yyyy HH:mm:ss} - {message}{Environment.NewLine}";
                messageTextBox.AppendText(logMessage);
                logManagerContainer.WriteLine(logMessage, logLevel);
            }, null);
        }
    }
}