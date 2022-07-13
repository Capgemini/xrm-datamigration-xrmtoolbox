namespace Capgemini.Xrm.CdsDataMigratorLibrary.UserControls
{
    partial class EntityListView
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
            this.tplMainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxShowSystemAttributes = new System.Windows.Forms.CheckBox();
            this.checkBoxSelectUnselectAll = new System.Windows.Forms.CheckBox();
            this.treeViewEntities = new System.Windows.Forms.TreeView();
            this.tplMainLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // tplMainLayout
            // 
            this.tplMainLayout.ColumnCount = 2;
            this.tplMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.00001F));
            this.tplMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.99999F));
            this.tplMainLayout.Controls.Add(this.label1, 0, 0);
            this.tplMainLayout.Controls.Add(this.checkBoxShowSystemAttributes, 1, 1);
            this.tplMainLayout.Controls.Add(this.checkBoxSelectUnselectAll, 0, 1);
            this.tplMainLayout.Controls.Add(this.treeViewEntities, 0, 2);
            this.tplMainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tplMainLayout.Location = new System.Drawing.Point(0, 0);
            this.tplMainLayout.Name = "tplMainLayout";
            this.tplMainLayout.RowCount = 3;
            this.tplMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tplMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tplMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tplMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tplMainLayout.Size = new System.Drawing.Size(400, 500);
            this.tplMainLayout.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Available entities";
            // 
            // checkBoxShowSystemAttributes
            // 
            this.checkBoxShowSystemAttributes.AutoSize = true;
            this.checkBoxShowSystemAttributes.Location = new System.Drawing.Point(203, 33);
            this.checkBoxShowSystemAttributes.Name = "checkBoxShowSystemAttributes";
            this.checkBoxShowSystemAttributes.Size = new System.Drawing.Size(168, 20);
            this.checkBoxShowSystemAttributes.TabIndex = 109;
            this.checkBoxShowSystemAttributes.Text = "Show System Attributes";
            this.checkBoxShowSystemAttributes.UseVisualStyleBackColor = true;
            // 
            // checkBoxSelectUnselectAll
            // 
            this.checkBoxSelectUnselectAll.AutoSize = true;
            this.checkBoxSelectUnselectAll.Location = new System.Drawing.Point(3, 33);
            this.checkBoxSelectUnselectAll.Name = "checkBoxSelectUnselectAll";
            this.checkBoxSelectUnselectAll.Size = new System.Drawing.Size(142, 20);
            this.checkBoxSelectUnselectAll.TabIndex = 110;
            this.checkBoxSelectUnselectAll.Text = "Select/Unselect All";
            this.checkBoxSelectUnselectAll.UseVisualStyleBackColor = true;
            // 
            // treeView1
            // 
            this.treeViewEntities.CheckBoxes = true;
            this.tplMainLayout.SetColumnSpan(this.treeViewEntities, 2);
            this.treeViewEntities.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewEntities.Location = new System.Drawing.Point(3, 63);
            this.treeViewEntities.Name = "treeView1";
            this.treeViewEntities.Size = new System.Drawing.Size(394, 434);
            this.treeViewEntities.TabIndex = 111;
            // 
            // EntityListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tplMainLayout);
            this.Name = "EntityListView";
            this.Size = new System.Drawing.Size(400, 500);
            this.tplMainLayout.ResumeLayout(false);
            this.tplMainLayout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tplMainLayout;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxShowSystemAttributes;
        private System.Windows.Forms.CheckBox checkBoxSelectUnselectAll;
        private System.Windows.Forms.TreeView treeViewEntities;
    }
}
