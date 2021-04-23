using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.UserControls
{
    public partial class WizardButtons : UserControl
    {
        public AeroWizard.WizardPageContainer PageContainer { get; set; }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool ShowExecuteButton
        {
            get { return btnExecute.Visible; }
            set { btnExecute.Visible = value; }
        }

        public WizardButtons()
        {
            InitializeComponent();
            btnExecute.Visible = ShowExecuteButton;
            btnBack.Enabled = false;
        }

        public void HandleSelectedPageChanged(bool isFinishPage)
        {
            btnExecute.Visible = isFinishPage;

            if (isFinishPage)
            {
                btnExecute.BackColor = Color.Green;
            }
            else
            {
                btnExecute.BackColor = SystemColors.ControlDarkDark;
            }

            btnNext.Enabled = PageContainer.SelectedPage != PageContainer.Pages[PageContainer.Pages.Count - 1];
            btnBack.Enabled = PageContainer.SelectedPage != PageContainer.Pages[0];
        }

        public void ExecutePreviousButtonClick(EventArgs e)
        {
            if (OnCustomPreviousNavigation != null)
            {
                OnCustomPreviousNavigation(this, e);
            }
            else
            {
                PageContainer.PreviousPage();
            }
        }

        public void ExecuteNextButtonClick(EventArgs e)
        {
            if (OnCustomNextNavigation != null)
            {
                OnCustomNextNavigation(this, e);
            }
            else
            {
                PageContainer.NextPage();
            }
        }

        public void ExecuteAction(EventArgs e)
        {
            btnExecute.Enabled = false;
            OnExecute?.Invoke(this, e);
            btnExecute.Enabled = true;
        }

        private void Container_SelectedPageChanged(object sender, EventArgs e)
        {
            HandleSelectedPageChanged(PageContainer.SelectedPage.IsFinishPage);
        }

        private void Button1Click(object sender, EventArgs e)
        {
            ExecutePreviousButtonClick(e);
        }

        private void Button2Click(object sender, EventArgs e)
        {
            ExecuteNextButtonClick(e);
        }

        private void Button3Click(object sender, EventArgs e)
        {
            ExecuteAction(e);
        }

        private void WizardButtonsLoadHandler(object sender, EventArgs e)
        {
            PageContainer.SelectedPageChanged += Container_SelectedPageChanged;
        }

        public event EventHandler<EventArgs> OnExecute;

        public event EventHandler<EventArgs> OnCustomNextNavigation;

        public event EventHandler<EventArgs> OnCustomPreviousNavigation;
    }
}