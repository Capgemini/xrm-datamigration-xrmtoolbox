using Capgemini.Xrm.CdsDataMigratorLibrary.Enums;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Services
{
    public interface ILogManagerContainer
    {
        void WriteLine(string message, LogLevel logLevel);
    }
}