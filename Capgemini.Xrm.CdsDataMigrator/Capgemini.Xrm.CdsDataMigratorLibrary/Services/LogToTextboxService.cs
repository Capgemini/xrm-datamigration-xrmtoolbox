using System;
using System.Threading;
using System.Windows.Forms;
using Capgemini.Xrm.CdsDataMigratorLibrary.Enums;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Services
{
    public class LogToTextboxService : AbstractLoggerService
    {
        private readonly SynchronizationContext syncContext;
        private readonly TextBox messageTextBox;
        private readonly ILogManagerContainer logManagerContainer;

        public LogToTextboxService(TextBox messageTextBox, SynchronizationContext syncContext, ILogManagerContainer logManagerContainer)
        {
            this.messageTextBox = messageTextBox;
            this.syncContext = syncContext;
            this.logManagerContainer = logManagerContainer;
        }

        protected override void WriteLine(string message, LogLevel logLevel)
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