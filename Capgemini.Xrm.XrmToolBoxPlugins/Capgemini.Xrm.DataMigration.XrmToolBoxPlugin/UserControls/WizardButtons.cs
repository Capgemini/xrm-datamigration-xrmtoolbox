using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.UserControls
{
    public partial class WizardButtons : UserControl
    {

        public AeroWizard.WizardPageContainer Container { get; set; }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool ShowExecuteButton
        {
            get { return button3.Visible; }
            set { button3.Visible = value; }
        }       

        public WizardButtons()
        {
            InitializeComponent();
            button3.Visible = ShowExecuteButton;
        }

        private void Container_SelectedPageChanged(object sender, EventArgs e)
        {
            button3.Enabled = Container.SelectedPage.IsFinishPage;
            button2.Enabled = !Container.SelectedPage.IsFinishPage;
            button1.Enabled = Container.SelectedPage != Container.Pages[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Container.PreviousPage();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Container.NextPage();
        }

        public event EventHandler<EventArgs> OnExecute;

        private void button3_Click(object sender, EventArgs e)
        {
            OnExecute(this,e);
        }

        private void WizardButtons_Load(object sender, EventArgs e)
        {
            Container.SelectedPageChanged += Container_SelectedPageChanged;
        }
    }
}
