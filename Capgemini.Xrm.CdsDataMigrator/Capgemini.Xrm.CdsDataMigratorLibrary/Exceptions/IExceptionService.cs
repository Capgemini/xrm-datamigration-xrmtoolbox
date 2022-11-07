using System;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Exceptions
{
    public interface IExceptionService
    {
        string GetErrorMessage(Exception error, bool returnWithStackTrace);
    }
}