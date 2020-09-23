﻿using System;
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

            if (Container.SelectedPage.IsFinishPage)
            {
                button3.BackColor = Color.DarkGray;
            }
            else
            {
                button3.BackColor = SystemColors.ControlDarkDark;
            }

            button2.Enabled = !Container.SelectedPage.IsFinishPage;
            button1.Enabled = Container.SelectedPage != Container.Pages[0];
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
            OnExecute(this, e);
        }

        private void WizardButtons_Load(object sender, EventArgs e)
        {
            Container.SelectedPageChanged += Container_SelectedPageChanged;
        }

        public event EventHandler<EventArgs> OnExecute;

        public event EventHandler<EventArgs> OnCustomNextNavigation;

        public event EventHandler<EventArgs> OnCustomPreviousNavigation;
    }
}