using System.Windows.Forms;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Services
{
    public interface INotificationService
    {
        void DisplayFeedback(string message);

        void DisplayErrorFeedback(IWin32Window owner, string message);

        void DisplayWarningFeedback(IWin32Window owner, string message);
    }
}