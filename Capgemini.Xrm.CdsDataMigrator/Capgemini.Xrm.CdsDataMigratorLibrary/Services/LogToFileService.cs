using Capgemini.Xrm.CdsDataMigratorLibrary.Enums;
using System;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Services
{
    public class LogToFileService : AbstractLoggerService
    {
        private readonly ILogManagerContainer logManagerContainer;

        public LogToFileService(ILogManagerContainer logManagerContainer)
        {
            this.logManagerContainer = logManagerContainer;
        }

        protected override void WriteLine(string message, LogLevel logLevel)
        {
            var logMessage = $"{DateTime.Now:dd-MMM-yyyy HH:mm:ss} - {message}{Environment.NewLine}";
            logManagerContainer.WriteLine(logMessage, logLevel);
        }
    }
}