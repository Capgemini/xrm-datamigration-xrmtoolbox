using System;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Services
{
    public interface INotifier
    {
        void ShowError(Exception error);

        void ShowSuccess(string message);
    }
}
