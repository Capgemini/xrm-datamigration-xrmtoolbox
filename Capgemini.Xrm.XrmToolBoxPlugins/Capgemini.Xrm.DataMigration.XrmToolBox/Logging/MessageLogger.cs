using System;
using System.Threading;
using System.Windows.Forms;
using Capgemini.DataMigration.Core;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Logging
{
    public class MessageLogger : ILogger
    {
        private readonly SynchronizationContext syncContext;
        private readonly TextBox messageTextBox;

        public MessageLogger(TextBox messageTextBox, SynchronizationContext syncContext)
        {
            this.messageTextBox = messageTextBox;
            this.syncContext = syncContext;
        }

        public LogLevel LogLevel { get; set; } = LogLevel.Info;

        public void Error(string message)
        {
            WriteLine($"Error:{message}");
        }

        public void Error(string message, Exception ex)
        {
            WriteLine($"Error:{message},Ex:{ex}");
        }

        public void Info(string message)
        {
            if ((int)LogLevel > 1)
            {
                WriteLine($"Info:{message}");
            }
        }

        public void Verbose(string message)
        {
            if ((int)LogLevel > 2)
            {
                WriteLine($"Verbose:{message}");
            }
        }

        public void Warning(string message)
        {
            if (LogLevel > 0)
            {
                WriteLine($"Warning:{message}");
            }
        }

        private void WriteLine(string message)
        {
            syncContext.Send(
                p =>
            {
                messageTextBox.AppendText($"{DateTime.Now} - {message}{Environment.NewLine}");
            }, null);
        }
    }
}