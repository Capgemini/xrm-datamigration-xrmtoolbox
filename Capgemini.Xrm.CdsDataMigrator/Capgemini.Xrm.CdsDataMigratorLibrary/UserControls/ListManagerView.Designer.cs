namespace Capgemini.Xrm.CdsDataMigratorLibrary.UserControls
{
    partial class ListManagerView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tlpMainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.listViewItems = new System.Windows.Forms.ListView();
            this.clAttDisplayName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clAttLogicalName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clAttType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clAttComment = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.checkBoxSelectUnselectAll = new System.Windows.Forms.CheckBox();
            this.labelDisplayedItemsName = new System.Windows.Forms.Label();
            this.tlpMainLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMainLayout
            // 
            this.tlpMainLayout.ColumnCount = 1;
            this.tlpMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMainLayout.Controls.Add(this.listViewItems, 0, 2);
            this.tlpMainLayout.Controls.Add(this.checkBoxSelectUnselectAll, 0, 1);
            this.tlpMainLayout.Controls.Add(this.labelDisplayedItemsName, 0, 0);
            this.tlpMainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMainLayout.Location = new System.Drawing.Point(0, 0);
            this.tlpMainLayout.Name = "tlpMainLayout";
            this.tlpMainLayout.RowCount = 3;
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMainLayout.Size = new System.Drawing.Size(915, 233);
            this.tlpMainLayout.TabIndex = 0;
            // 
            // listViewItems
            // 
            this.listViewItems.CheckBoxes = true;
            this.listViewItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clAttDisplayName,
            this.clAttLogicalName,
            this.clAttType,
            this.clAttComment});
            this.listViewItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewItems.FullRowSelect = true;
            this.listViewItems.HideSelection = false;
            this.listViewItems.Location = new System.Drawing.Point(4, 54);
            this.listViewItems.Margin = new System.Windows.Forms.Padding(4, 4, 4, 0);
            this.listViewItems.Name = "listViewItems";
            this.listViewItems.Size = new System.Drawing.Size(907, 179);
            this.listViewItems.TabIndex = 65;
            this.listViewItems.UseCompatibleStateImageBehavior = false;
            this.listViewItems.View = System.Windows.Forms.View.Details;
            this.listViewItems.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewItems_ColumnClick);
            this.listViewItems.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.listViewItems_ItemCheck);
            // 
            // clAttDisplayName
            // 
            this.clAttDisplayName.Text = "Display Name";
            this.clAttDisplayName.Width = 154;
            // 
            // clAttLogicalName
            // 
            this.clAttLogicalName.Text = "Logical Name";
            this.clAttLogicalName.Width = 129;
            // 
            // clAttType
            // 
            this.clAttType.Text = "Type";
            this.clAttType.Width = 138;
            // 
            // clAttComment
            // 
            this.clAttComment.Text = "Comment";
            this.clAttComment.Width = 200;
            // 
            // checkBoxSelectUnselectAll
            // 
            this.checkBoxSelectUnselectAll.AutoSize = true;
            this.checkBoxSelectUnselectAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBoxSelectUnselectAll.Location = new System.Drawing.Point(3, 28);
            this.checkBoxSelectUnselectAll.Name = "checkBoxSelectUnselectAll";
            this.checkBoxSelectUnselectAll.Size = new System.Drawing.Size(909, 19);
            this.checkBoxSelectUnselectAll.TabIndex = 0;
            this.checkBoxSelectUnselectAll.Text = "Select/Unselect All";
            this.checkBoxSelectUnselectAll.UseVisualStyleBackColor = true;
            this.checkBoxSelectUnselectAll.CheckedChanged += new System.EventHandler(this.SelectUnselectAllCheckedChanged);
            // 
            // labelDisplayedItemsName
            // 
            this.labelDisplayedItemsName.AutoSize = true;
            this.labelDisplayedItemsName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelDisplayedItemsName.Location = new System.Drawing.Point(3, 0);
            this.labelDisplayedItemsName.Name = "labelDisplayedItemsName";
            this.labelDisplayedItemsName.Size = new System.Drawing.Size(909, 25);
            this.labelDisplayedItemsName.TabIndex = 66;
            this.labelDisplayedItemsName.Text = "label1";
            // 
            // ListManagerView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpMainLayout);
            this.Name = "ListManagerView";
            this.Size = new System.Drawing.Size(915, 233);
            this.tlpMainLayout.ResumeLayout(false);
            this.tlpMainLayout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMainLayout;
        private System.Windows.Forms.CheckBox checkBoxSelectUnselectAll;
        private System.Windows.Forms.ListView listViewItems;
        private System.Windows.Forms.ColumnHeader clAttDisplayName;
        private System.Windows.Forms.ColumnHeader clAttLogicalName;
        private System.Windows.Forms.ColumnHeader clAttType;
        private System.Windows.Forms.ColumnHeader clAttComment;
        private System.Windows.Forms.Label labelDisplayedItemsName;
    }
}
