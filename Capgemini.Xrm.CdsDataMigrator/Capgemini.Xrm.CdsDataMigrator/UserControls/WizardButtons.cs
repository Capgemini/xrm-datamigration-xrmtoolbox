﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.UserControls
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

        private void Container_SelectedPageChanged(object sender, EventArgs e)
        {
            btnExecute.Visible = PageContainer.SelectedPage.IsFinishPage;

            if (PageContainer.SelectedPage.IsFinishPage)
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

        private void button1_Click(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
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

        private void button3_Click(object sender, EventArgs e)
        {
            btnExecute.Enabled = false;
            OnExecute?.Invoke(this, e);
            btnExecute.Enabled = true;
        }

        private void WizardButtons_Load(object sender, EventArgs e)
        {
            PageContainer.SelectedPageChanged += Container_SelectedPageChanged;
        }

        public event EventHandler<EventArgs> OnExecute;

        public event EventHandler<EventArgs> OnCustomNextNavigation;

        public event EventHandler<EventArgs> OnCustomPreviousNavigation;
    }
}