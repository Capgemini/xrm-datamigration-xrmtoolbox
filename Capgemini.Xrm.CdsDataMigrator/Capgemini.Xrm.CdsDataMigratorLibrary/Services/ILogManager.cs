using Capgemini.DataMigration.Core;
using Capgemini.Xrm.CdsDataMigratorLibrary.Enums;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Services
{
    public interface ILogManager : ILogger
    {
        LogLevel LogLevel { get; set; }
    }
}