using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.UserControls
{
    public partial class FileInputSelector : UserControl
    {
        public FileInputSelector()
        {
            InitializeComponent();
        }

        public string Value { 
            get => textBox1.Text;
            set { textBox1.Text = value; openFileDialog1.FileName = value; }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            textBox1.Text = openFileDialog1.FileName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }
    }
}
