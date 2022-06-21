using Capgemini.Xrm.CdsDataMigratorLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Services
{
    abstract public class AbstractLoggerService : ILogManager
    {
        public LogLevel LogLevel { get; set; } = LogLevel.Verbose;

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

        abstract protected void WriteLine(string message, LogLevel logLevel);
    }
}
