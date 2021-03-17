﻿using System;
using System.Windows.Forms;

namespace Capgemini.Xrm.DataMigration.XrmToolBox.Services
{
    public class FeedbackManager : IFeedbackManager
    {
        public void DisplayErrorFeedback(string message)
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

    public interface IFeedbackManager
    {
        void DisplayErrorFeedback(string message);

        void DisplayErrorFeedback(IWin32Window owner, string message);

        void DisplayWarningFeedback(IWin32Window owner, string message);
    }
}