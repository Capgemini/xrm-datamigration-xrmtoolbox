using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.UserControls
{
    [ExcludeFromCodeCoverage]
    public partial class FolderInputSelector : UserControl
    {
        public FolderInputSelector()
        {
            InitializeComponent();
        }

        public string Value
        {
            get => tbxInput.Text;
            set { tbxInput.Text = value; folderBrowserDialog.SelectedPath = value; }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.ShowDialog();
            tbxInput.Text = folderBrowserDialog.SelectedPath;
        }
    }
}
