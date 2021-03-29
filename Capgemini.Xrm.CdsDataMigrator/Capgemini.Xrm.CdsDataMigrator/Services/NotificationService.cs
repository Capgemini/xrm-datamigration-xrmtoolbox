using System;
using System.Windows.Forms;

namespace Capgemini.Xrm.CdsDataMigrator.Services
{
    public class NotificationService : INotificationService
    {
        public void DisplayFeedback(string message)
        {
            MessageBox.Show(message);
        }

        public void DisplayErrorFeedback(IWin32Window owner, string message)
        {
            MessageBox.Show(owner, message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void DisplayWarningFeedback(IWin32Window owner, string message)
        {
            MessageBox.Show(owner, message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}