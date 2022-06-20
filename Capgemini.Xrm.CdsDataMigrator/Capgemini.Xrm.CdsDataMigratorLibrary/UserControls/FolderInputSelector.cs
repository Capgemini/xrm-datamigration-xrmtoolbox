using System;
using System.Windows.Forms;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.UserControls
{
    public partial class FolderInputSelector : UserControl
    {
        public FolderInputSelector()
        {
            InitializeComponent();
        }

        public string Value
        {
            get => textBox1.Text;
            set { textBox1.Text = value; folderBrowserDialog1.SelectedPath = value; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            textBox1.Text = folderBrowserDialog1.SelectedPath;
        }
    }
}
