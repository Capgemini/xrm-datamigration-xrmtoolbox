using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capgemini.Xrm.DataMigration.XrmToolBox.Core
{
    public interface IExceptionService
    {
        string GetErrorMessage(Exception error, bool returnWithStackTrace);
    }
}