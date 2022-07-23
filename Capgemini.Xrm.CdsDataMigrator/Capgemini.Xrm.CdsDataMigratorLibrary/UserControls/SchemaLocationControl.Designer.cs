namespace Capgemini.Xrm.CdsDataMigratorLibrary.UserControls
{
    partial class SchemaLocationControl
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
            this.label8 = new System.Windows.Forms.Label();
            this.fileInputSelectorSchema = new Capgemini.Xrm.CdsDataMigratorLibrary.UserControls.FileInputSelector();
            this.tplMainLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // tplMainLayout
            // 
            this.tplMainLayout.ColumnCount = 2;
            this.tplMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tplMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tplMainLayout.Controls.Add(this.label8, 0, 0);
            this.tplMainLayout.Controls.Add(this.fileInputSelectorSchema, 1, 0);
            this.tplMainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tplMainLayout.Location = new System.Drawing.Point(0, 0);
            this.tplMainLayout.Name = "tplMainLayout";
            this.tplMainLayout.RowCount = 1;
            this.tplMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tplMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tplMainLayout.Size = new System.Drawing.Size(702, 30);
            this.tplMainLayout.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(4, 0);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(112, 19);
            this.label8.TabIndex = 34;
            this.label8.Text = "Schema File Path";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // fileInputSelectorSchema
            // 
            this.fileInputSelectorSchema.AutoSize = true;
            this.fileInputSelectorSchema.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileInputSelectorSchema.Location = new System.Drawing.Point(164, 4);
            this.fileInputSelectorSchema.Margin = new System.Windows.Forms.Padding(4);
            this.fileInputSelectorSchema.MinimumSize = new System.Drawing.Size(133, 31);
            this.fileInputSelectorSchema.Name = "fileInputSelectorSchema";
            this.fileInputSelectorSchema.Size = new System.Drawing.Size(534, 32);
            this.fileInputSelectorSchema.TabIndex = 37;
            this.fileInputSelectorSchema.Value = "";
            // 
            // SchemaLocationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tplMainLayout);
            this.Name = "SchemaLocationControl";
            this.Size = new System.Drawing.Size(702, 30);
            this.tplMainLayout.ResumeLayout(false);
            this.tplMainLayout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tplMainLayout;
        private FileInputSelector fileInputSelectorSchema;
        private System.Windows.Forms.Label label8;
    }
}
