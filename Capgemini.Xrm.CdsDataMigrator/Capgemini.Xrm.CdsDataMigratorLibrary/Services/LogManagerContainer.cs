using Capgemini.Xrm.CdsDataMigratorLibrary.Enums;
using System.Diagnostics.CodeAnalysis;
using XrmToolBox.Extensibility;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Services
{
    [ExcludeFromCodeCoverage]
    public class LogManagerContainer : ILogManagerContainer
    {
        private readonly LogManager xrmToolBoxLogManager;

        public LogManagerContainer(LogManager logManager)
        {
            xrmToolBoxLogManager = logManager;
        }

        public void WriteLine(string message, LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Error:
                    xrmToolBoxLogManager.LogError(message);
                    break;

                case LogLevel.Warning:
                    xrmToolBoxLogManager.LogWarning(message);
                    break;

                case LogLevel.Info:
                case LogLevel.Verbose:
                    xrmToolBoxLogManager.LogInfo(message);
                    break;
            }
        }
    }
}