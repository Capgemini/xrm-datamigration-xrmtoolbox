namespace Capgemini.Xrm.ContactTelephoneUpdate
{
    partial class PluginControl
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
            this.lbConDetails = new System.Windows.Forms.Label();
            this.buttonUpdateCRM = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxCrmConnString = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.richTextBoxLog = new System.Windows.Forms.RichTextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbConDetails
            // 
            this.lbConDetails.AutoSize = true;
            this.lbConDetails.Location = new System.Drawing.Point(217, 15);
            this.lbConDetails.Name = "lbConDetails";
            this.lbConDetails.Size = new System.Drawing.Size(0, 15);
            this.lbConDetails.TabIndex = 1;
            // 
            // buttonUpdateCRM
            // 
            this.buttonUpdateCRM.Location = new System.Drawing.Point(536, 138);
            this.buttonUpdateCRM.Name = "buttonUpdateCRM";
            this.buttonUpdateCRM.Size = new System.Drawing.Size(95, 23);
            this.buttonUpdateCRM.TabIndex = 12;
            this.buttonUpdateCRM.Text = "Update CRM";
            this.buttonUpdateCRM.UseVisualStyleBackColor = true;
            this.buttonUpdateCRM.Click += new System.EventHandler(this.buttonUpdateCRM_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Enabled = false;
            this.buttonCancel.Location = new System.Drawing.Point(637, 138);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 13;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxCrmConnString);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.lbConDetails);
            this.groupBox1.Location = new System.Drawing.Point(32, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(680, 113);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Parameters";
            // 
            // textBoxCrmConnString
            // 
            this.textBoxCrmConnString.Location = new System.Drawing.Point(155, 29);
            this.textBoxCrmConnString.Name = "textBoxCrmConnString";
            this.textBoxCrmConnString.Size = new System.Drawing.Size(507, 20);
            this.textBoxCrmConnString.TabIndex = 29;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 15);
            this.label3.TabIndex = 28;
            this.label3.Text = "CRM Connectionstring";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(217, 51);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 15);
            this.label7.TabIndex = 1;
            // 
            // richTextBoxLog
            // 
            this.richTextBoxLog.Location = new System.Drawing.Point(12, 167);
            this.richTextBoxLog.Name = "richTextBoxLog";
            this.richTextBoxLog.ReadOnly = true;
            this.richTextBoxLog.Size = new System.Drawing.Size(694, 232);
            this.richTextBoxLog.TabIndex = 16;
            this.richTextBoxLog.Text = "";
            // 
            // PluginControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.richTextBoxLog);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonUpdateCRM);
            this.Name = "PluginControl";
            this.Size = new System.Drawing.Size(739, 571);
            this.Load += new System.EventHandler(this.PluginControl_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lbConDetails;
        private System.Windows.Forms.Button buttonUpdateCRM;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox richTextBoxLog;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxCrmConnString;
        private System.Windows.Forms.Label label3;
    }
}
