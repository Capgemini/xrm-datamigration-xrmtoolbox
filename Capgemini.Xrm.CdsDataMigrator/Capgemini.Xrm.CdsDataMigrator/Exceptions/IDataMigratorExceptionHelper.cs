using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capgemini.Xrm.DataMigration.XrmToolBox.Core
{
    public interface IDataMigratorExceptionHelper
    {
        string GetErrorMessage(Exception error, bool returnWithStackTrace);
    }
}