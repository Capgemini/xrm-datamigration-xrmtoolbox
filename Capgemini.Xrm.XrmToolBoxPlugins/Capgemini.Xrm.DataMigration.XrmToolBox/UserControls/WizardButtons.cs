using System;
using System.ComponentModel;
using System.Drawing;
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
            button3.Visible = Container.SelectedPage.IsFinishPage;

            button3.BackColor = Color.Green;

            button2.Enabled = !Container.SelectedPage.IsFinishPage;
            DisableBackButtonIfNotRequired();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (OnCustomPreviousNavigation != null)
            {
                OnCustomPreviousNavigation(this, e);
            }
            else
            {
                Container.PreviousPage();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (OnCustomNextNavigation != null)
            {
                OnCustomNextNavigation(this, e);
            }
            else
            {
                Container.NextPage();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OnExecute?.Invoke(this, e);
        }

        private void WizardButtons_Load(object sender, EventArgs e)
        {
            Container.SelectedPageChanged += Container_SelectedPageChanged;
            DisableBackButtonIfNotRequired();
        }

        public event EventHandler<EventArgs> OnExecute;

        public event EventHandler<EventArgs> OnCustomNextNavigation;

        public event EventHandler<EventArgs> OnCustomPreviousNavigation;

        private void DisableBackButtonIfNotRequired()
        { 
            if (button1 != null && Container.SelectedPage != null && Container.Pages.Count > 0)
            {
                button1.Enabled = Container.SelectedPage != Container.Pages[0];
            }
        }
    }
}