namespace Capgemini.Xrm.CdsDataMigratorLibrary.Forms
{
    partial class ImportFilterForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblSubTitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.flpFooter = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSave = new System.Windows.Forms.Button();
            this.tbxFetchXmlFilter = new System.Windows.Forms.TextBox();
            this.lbxEntityNames = new System.Windows.Forms.ListBox();
            this.pnlHeader.SuspendLayout();
            this.flpFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.AutoSize = true;
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.lblSubTitle);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Margin = new System.Windows.Forms.Padding(4);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(10);
            this.pnlHeader.Size = new System.Drawing.Size(669, 71);
            this.pnlHeader.TabIndex = 7;
            // 
            // lblSubTitle
            // 
            this.lblSubTitle.AutoSize = true;
            this.lblSubTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSubTitle.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubTitle.Location = new System.Drawing.Point(10, 42);
            this.lblSubTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSubTitle.Name = "lblSubTitle";
            this.lblSubTitle.Size = new System.Drawing.Size(595, 19);
            this.lblSubTitle.TabIndex = 1;
            this.lblSubTitle.Text = "Enter the filter section from the FetchXml query created by the \"Advanced Find\" f" +
    "eature in CRM ";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(10, 10);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(173, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "FetchXml Filter";
            // 
            // flpFooter
            // 
            this.flpFooter.AutoSize = true;
            this.flpFooter.Controls.Add(this.btnSave);
            this.flpFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flpFooter.Location = new System.Drawing.Point(0, 311);
            this.flpFooter.Name = "flpFooter";
            this.flpFooter.Padding = new System.Windows.Forms.Padding(10);
            this.flpFooter.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.flpFooter.Size = new System.Drawing.Size(669, 49);
            this.flpFooter.TabIndex = 9;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(571, 13);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tbxFetchXmlFilter
            // 
            this.tbxFetchXmlFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxFetchXmlFilter.Font = new System.Drawing.Font("Courier New", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxFetchXmlFilter.Location = new System.Drawing.Point(249, 71);
            this.tbxFetchXmlFilter.Margin = new System.Windows.Forms.Padding(10);
            this.tbxFetchXmlFilter.Multiline = true;
            this.tbxFetchXmlFilter.Name = "tbxFetchXmlFilter";
            this.tbxFetchXmlFilter.Size = new System.Drawing.Size(420, 240);
            this.tbxFetchXmlFilter.TabIndex = 10;
            this.tbxFetchXmlFilter.TextChanged += new System.EventHandler(this.tbxFilterText_TextChanged);
            // 
            // lbxEntityNames
            // 
            this.lbxEntityNames.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbxEntityNames.FormattingEnabled = true;
            this.lbxEntityNames.ItemHeight = 16;
            this.lbxEntityNames.Location = new System.Drawing.Point(0, 71);
            this.lbxEntityNames.Margin = new System.Windows.Forms.Padding(10);
            this.lbxEntityNames.Name = "lbxEntityNames";
            this.lbxEntityNames.Size = new System.Drawing.Size(249, 240);
            this.lbxEntityNames.TabIndex = 11;
            this.lbxEntityNames.SelectedIndexChanged += new System.EventHandler(this.lbxEntityNames_SelectedIndexChanged);
            // 
            // ExportFilterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(669, 360);
            this.Controls.Add(this.tbxFetchXmlFilter);
            this.Controls.Add(this.lbxEntityNames);
            this.Controls.Add(this.flpFooter);
            this.Controls.Add(this.pnlHeader);
            this.Name = "ExportFilterForm";
            this.Text = "ExportFilterForm";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.flpFooter.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblSubTitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.FlowLayoutPanel flpFooter;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox tbxFetchXmlFilter;
        private System.Windows.Forms.ListBox lbxEntityNames;
    }
}