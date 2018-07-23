namespace Capgemini.Xrm.SolutionAudit.XrmToolBoxPlugin
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
            this.btGenerate = new System.Windows.Forms.Button();
            this.tbLog = new System.Windows.Forms.TextBox();
            this.tbPublishers = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btExportPath = new System.Windows.Forms.Button();
            this.tbExportFolderPath = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.cbHtml = new System.Windows.Forms.CheckBox();
            this.cbExcel = new System.Windows.Forms.CheckBox();
            this.cbJson = new System.Windows.Forms.CheckBox();
            this.cbXml = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btGenerate
            // 
            this.btGenerate.Location = new System.Drawing.Point(347, 58);
            this.btGenerate.Name = "btGenerate";
            this.btGenerate.Size = new System.Drawing.Size(75, 23);
            this.btGenerate.TabIndex = 0;
            this.btGenerate.Text = "Generate";
            this.btGenerate.UseVisualStyleBackColor = true;
            this.btGenerate.Click += new System.EventHandler(this.btGenerate_Click);
            // 
            // tbLog
            // 
            this.tbLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbLog.Location = new System.Drawing.Point(13, 87);
            this.tbLog.Multiline = true;
            this.tbLog.Name = "tbLog";
            this.tbLog.Size = new System.Drawing.Size(676, 415);
            this.tbLog.TabIndex = 1;
            // 
            // tbPublishers
            // 
            this.tbPublishers.Location = new System.Drawing.Point(180, 7);
            this.tbPublishers.Name = "tbPublishers";
            this.tbPublishers.Size = new System.Drawing.Size(406, 20);
            this.tbPublishers.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Publishers (coma delimited)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(26, 33);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(123, 13);
            this.label7.TabIndex = 37;
            this.label7.Text = "Export report Folder path";
            // 
            // btExportPath
            // 
            this.btExportPath.Location = new System.Drawing.Point(592, 30);
            this.btExportPath.Margin = new System.Windows.Forms.Padding(2);
            this.btExportPath.Name = "btExportPath";
            this.btExportPath.Size = new System.Drawing.Size(26, 19);
            this.btExportPath.TabIndex = 39;
            this.btExportPath.Text = "...";
            this.btExportPath.UseVisualStyleBackColor = true;
            this.btExportPath.Click += new System.EventHandler(this.btExportPath_Click);
            // 
            // tbExportFolderPath
            // 
            this.tbExportFolderPath.Location = new System.Drawing.Point(180, 29);
            this.tbExportFolderPath.Name = "tbExportFolderPath";
            this.tbExportFolderPath.Size = new System.Drawing.Size(406, 20);
            this.tbExportFolderPath.TabIndex = 38;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(518, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 43;
            this.label4.Text = "Loggin Level";
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Error",
            "Warning",
            "Info",
            "Verbose"});
            this.comboBox1.Location = new System.Drawing.Point(592, 58);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(97, 21);
            this.comboBox1.TabIndex = 42;
            this.comboBox1.Text = "Info";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // cbHtml
            // 
            this.cbHtml.AutoSize = true;
            this.cbHtml.Checked = true;
            this.cbHtml.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbHtml.Location = new System.Drawing.Point(29, 62);
            this.cbHtml.Margin = new System.Windows.Forms.Padding(2);
            this.cbHtml.Name = "cbHtml";
            this.cbHtml.Size = new System.Drawing.Size(47, 17);
            this.cbHtml.TabIndex = 44;
            this.cbHtml.Text = "Html";
            this.cbHtml.UseVisualStyleBackColor = true;
            // 
            // cbExcel
            // 
            this.cbExcel.AutoSize = true;
            this.cbExcel.Checked = true;
            this.cbExcel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbExcel.Location = new System.Drawing.Point(80, 62);
            this.cbExcel.Margin = new System.Windows.Forms.Padding(2);
            this.cbExcel.Name = "cbExcel";
            this.cbExcel.Size = new System.Drawing.Size(52, 17);
            this.cbExcel.TabIndex = 45;
            this.cbExcel.Text = "Excel";
            this.cbExcel.UseVisualStyleBackColor = true;
            // 
            // cbJson
            // 
            this.cbJson.AutoSize = true;
            this.cbJson.Checked = true;
            this.cbJson.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbJson.Location = new System.Drawing.Point(136, 62);
            this.cbJson.Margin = new System.Windows.Forms.Padding(2);
            this.cbJson.Name = "cbJson";
            this.cbJson.Size = new System.Drawing.Size(48, 17);
            this.cbJson.TabIndex = 46;
            this.cbJson.Text = "Json";
            this.cbJson.UseVisualStyleBackColor = true;
            // 
            // cbXml
            // 
            this.cbXml.AutoSize = true;
            this.cbXml.Checked = true;
            this.cbXml.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbXml.Location = new System.Drawing.Point(188, 62);
            this.cbXml.Margin = new System.Windows.Forms.Padding(2);
            this.cbXml.Name = "cbXml";
            this.cbXml.Size = new System.Drawing.Size(43, 17);
            this.cbXml.TabIndex = 47;
            this.cbXml.Text = "Xml";
            this.cbXml.UseVisualStyleBackColor = true;
            // 
            // PluginControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbXml);
            this.Controls.Add(this.cbJson);
            this.Controls.Add(this.cbExcel);
            this.Controls.Add(this.cbHtml);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btExportPath);
            this.Controls.Add(this.tbExportFolderPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbPublishers);
            this.Controls.Add(this.tbLog);
            this.Controls.Add(this.btGenerate);
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "PluginControl";
            this.Size = new System.Drawing.Size(708, 518);
            this.ConnectionUpdated += new XrmToolBox.Extensibility.PluginControlBase.ConnectionUpdatedHandler(this.PluginControl_ConnectionUpdated);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btGenerate;
        private System.Windows.Forms.TextBox tbLog;
        private System.Windows.Forms.TextBox tbPublishers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btExportPath;
        private System.Windows.Forms.TextBox tbExportFolderPath;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.CheckBox cbHtml;
        private System.Windows.Forms.CheckBox cbExcel;
        private System.Windows.Forms.CheckBox cbJson;
        private System.Windows.Forms.CheckBox cbXml;
    }
}
