using McTools.Xrm.Connection;
using System;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Exceptions
{
    public class ExceptionService : IExceptionService
    {
        public string GetErrorMessage(Exception error, bool returnWithStackTrace)
        {
            return CrmExceptionHelper.GetErrorMessage(error, false);
        }
    }
}