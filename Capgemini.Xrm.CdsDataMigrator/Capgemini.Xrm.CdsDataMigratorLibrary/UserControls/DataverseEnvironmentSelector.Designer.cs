﻿namespace Capgemini.Xrm.CdsDataMigratorLibrary.UserControls
{
    partial class DataverseEnvironmentSelector
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
            this.btnSelect = new System.Windows.Forms.Button();
            this.lblConnectionName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.btnSelect.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSelect.Location = new System.Drawing.Point(180, 0);
            this.btnSelect.Name = "button1";
            this.btnSelect.Size = new System.Drawing.Size(22, 34);
            this.btnSelect.TabIndex = 2;
            this.btnSelect.Text = "...";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // label1
            // 
            this.lblConnectionName.AutoSize = true;
            this.lblConnectionName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblConnectionName.Location = new System.Drawing.Point(0, 0);
            this.lblConnectionName.Name = "label1";
            this.lblConnectionName.Padding = new System.Windows.Forms.Padding(0, 5, 5, 5);
            this.lblConnectionName.Size = new System.Drawing.Size(121, 18);
            this.lblConnectionName.TabIndex = 3;
            this.lblConnectionName.Text = "Please select an environment";
            // 
            // DataverseEnvironmentSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblConnectionName);
            this.Controls.Add(this.btnSelect);
            this.Name = "DataverseEnvironmentSelector";
            this.Size = new System.Drawing.Size(202, 34);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Label lblConnectionName;
    }
}