using System;
using System.Threading;
using System.Windows.Forms;
using Capgemini.DataMigration.Core;

namespace Capgemini.Xrm.XrmToolBoxPluginBase.Models
{
    public class MessageLogger : ILogger
    {
        private readonly SynchronizationContext _syncContext;
        private readonly RichTextBox _tbMessage;

        public MessageLogger(RichTextBox tbMessage, SynchronizationContext syncContext)
        {
            _tbMessage = tbMessage;
            _syncContext = syncContext;
        }

        public static LogLevel LogLevel { get; set; } = LogLevel.Info;

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
            _syncContext.Send(p =>
            {
                _tbMessage.AppendText($"{DateTime.Now} - {message}{Environment.NewLine}");
            }, null);
        }

    }
}
