using System.Windows.Forms;

namespace Capgemini.Xrm.DataMigration.XrmToolBox.Services
{
    public interface IFeedbackManager
    {
        void DisplayFeedback(string message);

        void DisplayErrorFeedback(IWin32Window owner, string message);

        void DisplayWarningFeedback(IWin32Window owner, string message);
    }
}