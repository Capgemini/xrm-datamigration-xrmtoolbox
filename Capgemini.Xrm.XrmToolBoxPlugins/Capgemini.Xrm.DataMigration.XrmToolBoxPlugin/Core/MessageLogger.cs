using Capgemini.DataMigration.Core;
using System;
using System.Threading;
using System.Windows.Forms;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core
{
    public class MessageLogger : ILogger
    {
        private readonly SynchronizationContext _syncContext;
        private readonly TextBox _tbMessage;

        public MessageLogger(TextBox tbMessage, SynchronizationContext syncContext)
        {
            _tbMessage = tbMessage;
            _syncContext = syncContext;
        }

        /// <summary>
        /// LogLevel
        /// 0 - only Errors
        /// 1 - Warnings
        /// 2 - Info
        /// 3 - Verbose
        /// </summary>
        public static int LogLevel { get; set; } = 2;

        public void Error(string message)
        {
            this.WriteLine("Error:" + message);
        }

        public void Error(string message, Exception ex)
        {
            this.WriteLine("Error:" + message + ",Ex:" + ex.ToString());
        }

        public void Info(string message)
        {
            if (LogLevel > 1)
                this.WriteLine("Info:" + message);
        }

        public void Verbose(string message)
        {
            if (LogLevel > 2)
                this.WriteLine("Verbose:" + message);
        }

        public void Warning(string message)
        {
            if (LogLevel > 0)
                this.WriteLine("Warning:" + message);
        }

        private void WriteLine(string message)
        {
            _syncContext.Send(p =>
            {
                _tbMessage.AppendText(string.Format("{0} - {1}{2}", DateTime.Now, message, Environment.NewLine));
            }, null);
        }
    }
}