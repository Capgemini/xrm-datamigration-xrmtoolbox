using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.UserControls
{
    [ExcludeFromCodeCoverage]
    public partial class FileInputSelector : UserControl
    {
        public FileInputSelector()
        {
            InitializeComponent();
        }

        public string Value { 
            get => tbxInput.Text;
            set { tbxInput.Text = value; openFileDialog.FileName = value; }
        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            tbxInput.Text = openFileDialog.FileName;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
        }
    }
}
