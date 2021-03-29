using System;
using McTools.Xrm.Connection;

namespace Capgemini.Xrm.DataMigration.XrmToolBox.Core
{
    public class ExceptionService : IExceptionService
    {
        public string GetErrorMessage(Exception error, bool returnWithStackTrace)
        {
            return CrmExceptionHelper.GetErrorMessage(error, false);
        }
    }
}