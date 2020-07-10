namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.UserControls
{
    partial class exportWizard
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
            this.stepWizardControl1 = new AeroWizard.WizardPageContainer();
            this.wizardPage1 = new AeroWizard.WizardPage();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButtonFormatJson = new System.Windows.Forms.RadioButton();
            this.radioButtonFormatCsv = new System.Windows.Forms.RadioButton();
            this.wizardPage2 = new AeroWizard.WizardPage();
            this.buttonExportLocation = new System.Windows.Forms.Button();
            this.textBoxExportLocation = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.wizardPage3 = new AeroWizard.WizardPage();
            this.buttonExportConfigLocation = new System.Windows.Forms.Button();
            this.textBoxExportConfigLocation = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.wizardPage4 = new AeroWizard.WizardPage();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBoxMinimize = new Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.UserControls.ToggleCheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.checkBoxExportInactiveRecords = new Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.UserControls.ToggleCheckBox();
            this.labelConnectionString = new System.Windows.Forms.Label();
            this.buttonTargetConnectionString = new System.Windows.Forms.Button();
            this.labelTargetConnectionString = new System.Windows.Forms.Label();
            this.labelSchemaFile = new System.Windows.Forms.Label();
            this.buttonSchemaLocation = new System.Windows.Forms.Button();
            this.textBoxSchemaLocation = new System.Windows.Forms.TextBox();
            this.numericUpDownBatchSize = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.wizardPage5 = new AeroWizard.WizardPage();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxLogs = new System.Windows.Forms.TextBox();
            this.folderBrowserDialogExportLocation = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialogExportConfigFile = new System.Windows.Forms.OpenFileDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.wizardButtons1 = new Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.UserControls.WizardButtons();
            ((System.ComponentModel.ISupportInitialize)(this.stepWizardControl1)).BeginInit();
            this.stepWizardControl1.SuspendLayout();
            this.wizardPage1.SuspendLayout();
            this.wizardPage2.SuspendLayout();
            this.wizardPage3.SuspendLayout();
            this.wizardPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBatchSize)).BeginInit();
            this.wizardPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // stepWizardControl1
            // 
            this.stepWizardControl1.BackButton = null;
            this.stepWizardControl1.BackButtonText = "";
            this.stepWizardControl1.CancelButton = null;
            this.stepWizardControl1.CancelButtonText = "";
            this.stepWizardControl1.Controls.Add(this.wizardPage5);
            this.stepWizardControl1.Controls.Add(this.wizardPage4);
            this.stepWizardControl1.Controls.Add(this.wizardPage3);
            this.stepWizardControl1.Controls.Add(this.wizardPage2);
            this.stepWizardControl1.Controls.Add(this.wizardPage1);
            this.stepWizardControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stepWizardControl1.Location = new System.Drawing.Point(0, 0);
            this.stepWizardControl1.Name = "stepWizardControl1";
            this.stepWizardControl1.NextButton = null;
            this.stepWizardControl1.Pages.Add(this.wizardPage1);
            this.stepWizardControl1.Pages.Add(this.wizardPage2);
            this.stepWizardControl1.Pages.Add(this.wizardPage3);
            this.stepWizardControl1.Pages.Add(this.wizardPage4);
            this.stepWizardControl1.Pages.Add(this.wizardPage5);
            this.stepWizardControl1.Size = new System.Drawing.Size(1291, 668);
            this.stepWizardControl1.TabIndex = 0;
            // 
            // wizardPage1
            // 
            this.wizardPage1.Controls.Add(this.label1);
            this.wizardPage1.Controls.Add(this.radioButtonFormatJson);
            this.wizardPage1.Controls.Add(this.radioButtonFormatCsv);
            this.wizardPage1.Name = "wizardPage1";
            this.wizardPage1.NextPage = this.wizardPage2;
            this.wizardPage1.ShowCancel = false;
            this.wizardPage1.Size = new System.Drawing.Size(1291, 668);
            this.wizardPage1.TabIndex = 2;
            this.wizardPage1.Text = "Select Data Format";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(52, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(440, 25);
            this.label1.TabIndex = 5;
            this.label1.Text = "Which format would you like to export the data to?";
            // 
            // radioButtonFormatJson
            // 
            this.radioButtonFormatJson.AutoSize = true;
            this.radioButtonFormatJson.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonFormatJson.Location = new System.Drawing.Point(52, 97);
            this.radioButtonFormatJson.Name = "radioButtonFormatJson";
            this.radioButtonFormatJson.Size = new System.Drawing.Size(62, 24);
            this.radioButtonFormatJson.TabIndex = 4;
            this.radioButtonFormatJson.TabStop = true;
            this.radioButtonFormatJson.Text = "JSON";
            this.radioButtonFormatJson.UseVisualStyleBackColor = true;
            // 
            // radioButtonFormatCsv
            // 
            this.radioButtonFormatCsv.AutoSize = true;
            this.radioButtonFormatCsv.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonFormatCsv.Location = new System.Drawing.Point(52, 120);
            this.radioButtonFormatCsv.Name = "radioButtonFormatCsv";
            this.radioButtonFormatCsv.Size = new System.Drawing.Size(53, 24);
            this.radioButtonFormatCsv.TabIndex = 3;
            this.radioButtonFormatCsv.TabStop = true;
            this.radioButtonFormatCsv.Text = "CSV";
            this.radioButtonFormatCsv.UseVisualStyleBackColor = true;
            // 
            // wizardPage2
            // 
            this.wizardPage2.Controls.Add(this.buttonExportLocation);
            this.wizardPage2.Controls.Add(this.textBoxExportLocation);
            this.wizardPage2.Controls.Add(this.label2);
            this.wizardPage2.Name = "wizardPage2";
            this.wizardPage2.NextPage = this.wizardPage3;
            this.wizardPage2.ShowCancel = false;
            this.wizardPage2.Size = new System.Drawing.Size(1291, 668);
            this.wizardPage2.TabIndex = 3;
            this.wizardPage2.Text = "Select Export Location";
            // 
            // buttonExportLocation
            // 
            this.buttonExportLocation.Location = new System.Drawing.Point(451, 89);
            this.buttonExportLocation.Name = "buttonExportLocation";
            this.buttonExportLocation.Size = new System.Drawing.Size(34, 34);
            this.buttonExportLocation.TabIndex = 8;
            this.buttonExportLocation.Text = "...";
            this.buttonExportLocation.UseVisualStyleBackColor = true;
            this.buttonExportLocation.Click += new System.EventHandler(this.buttonExportLocation_Click);
            // 
            // textBoxExportLocation
            // 
            this.textBoxExportLocation.Location = new System.Drawing.Point(69, 97);
            this.textBoxExportLocation.Name = "textBoxExportLocation";
            this.textBoxExportLocation.Size = new System.Drawing.Size(376, 20);
            this.textBoxExportLocation.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(52, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(404, 25);
            this.label2.TabIndex = 6;
            this.label2.Text = "Where do you want to save the exported data?";
            // 
            // wizardPage3
            // 
            this.wizardPage3.Controls.Add(this.buttonExportConfigLocation);
            this.wizardPage3.Controls.Add(this.textBoxExportConfigLocation);
            this.wizardPage3.Controls.Add(this.label3);
            this.wizardPage3.Name = "wizardPage3";
            this.wizardPage3.NextPage = this.wizardPage4;
            this.wizardPage3.ShowCancel = false;
            this.wizardPage3.Size = new System.Drawing.Size(1291, 668);
            this.wizardPage3.TabIndex = 4;
            this.wizardPage3.Text = "Export Config";
            // 
            // buttonExportConfigLocation
            // 
            this.buttonExportConfigLocation.Location = new System.Drawing.Point(457, 87);
            this.buttonExportConfigLocation.Name = "buttonExportConfigLocation";
            this.buttonExportConfigLocation.Size = new System.Drawing.Size(34, 34);
            this.buttonExportConfigLocation.TabIndex = 11;
            this.buttonExportConfigLocation.Text = "...";
            this.buttonExportConfigLocation.UseVisualStyleBackColor = true;
            this.buttonExportConfigLocation.Click += new System.EventHandler(this.buttonExportConfigLocation_Click);
            // 
            // textBoxExportConfigLocation
            // 
            this.textBoxExportConfigLocation.Location = new System.Drawing.Point(52, 94);
            this.textBoxExportConfigLocation.Name = "textBoxExportConfigLocation";
            this.textBoxExportConfigLocation.Size = new System.Drawing.Size(399, 20);
            this.textBoxExportConfigLocation.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(52, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(380, 25);
            this.label3.TabIndex = 9;
            this.label3.Text = "Select the location of your export config file";
            // 
            // wizardPage4
            // 
            this.wizardPage4.Controls.Add(this.label6);
            this.wizardPage4.Controls.Add(this.checkBoxMinimize);
            this.wizardPage4.Controls.Add(this.label9);
            this.wizardPage4.Controls.Add(this.checkBoxExportInactiveRecords);
            this.wizardPage4.Controls.Add(this.labelConnectionString);
            this.wizardPage4.Controls.Add(this.buttonTargetConnectionString);
            this.wizardPage4.Controls.Add(this.labelTargetConnectionString);
            this.wizardPage4.Controls.Add(this.labelSchemaFile);
            this.wizardPage4.Controls.Add(this.buttonSchemaLocation);
            this.wizardPage4.Controls.Add(this.textBoxSchemaLocation);
            this.wizardPage4.Controls.Add(this.numericUpDownBatchSize);
            this.wizardPage4.Controls.Add(this.label5);
            this.wizardPage4.Controls.Add(this.label4);
            this.wizardPage4.Name = "wizardPage4";
            this.wizardPage4.ShowCancel = false;
            this.wizardPage4.Size = new System.Drawing.Size(1291, 668);
            this.wizardPage4.TabIndex = 5;
            this.wizardPage4.Text = "Perform Export";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(55, 230);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(118, 20);
            this.label6.TabIndex = 61;
            this.label6.Text = "Minimise (JSON)";
            // 
            // checkBoxMinimize
            // 
            this.checkBoxMinimize.Location = new System.Drawing.Point(225, 229);
            this.checkBoxMinimize.Name = "checkBoxMinimize";
            this.checkBoxMinimize.Padding = new System.Windows.Forms.Padding(4);
            this.checkBoxMinimize.Size = new System.Drawing.Size(49, 24);
            this.checkBoxMinimize.TabIndex = 60;
            this.checkBoxMinimize.Text = "toggleCheckBox2";
            this.checkBoxMinimize.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(55, 192);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(164, 20);
            this.label9.TabIndex = 59;
            this.label9.Text = "Export Inactive Records";
            // 
            // checkBoxExportInactiveRecords
            // 
            this.checkBoxExportInactiveRecords.Location = new System.Drawing.Point(225, 192);
            this.checkBoxExportInactiveRecords.Name = "checkBoxExportInactiveRecords";
            this.checkBoxExportInactiveRecords.Padding = new System.Windows.Forms.Padding(4);
            this.checkBoxExportInactiveRecords.Size = new System.Drawing.Size(49, 24);
            this.checkBoxExportInactiveRecords.TabIndex = 58;
            this.checkBoxExportInactiveRecords.Text = "toggleCheckBox2";
            this.checkBoxExportInactiveRecords.UseVisualStyleBackColor = true;
            // 
            // labelConnectionString
            // 
            this.labelConnectionString.AutoSize = true;
            this.labelConnectionString.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.labelConnectionString.Location = new System.Drawing.Point(52, 88);
            this.labelConnectionString.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelConnectionString.Name = "labelConnectionString";
            this.labelConnectionString.Size = new System.Drawing.Size(172, 20);
            this.labelConnectionString.TabIndex = 45;
            this.labelConnectionString.Text = "Target Connection String";
            // 
            // buttonTargetConnectionString
            // 
            this.buttonTargetConnectionString.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonTargetConnectionString.Location = new System.Drawing.Point(497, 88);
            this.buttonTargetConnectionString.Margin = new System.Windows.Forms.Padding(2);
            this.buttonTargetConnectionString.Name = "buttonTargetConnectionString";
            this.buttonTargetConnectionString.Size = new System.Drawing.Size(34, 34);
            this.buttonTargetConnectionString.TabIndex = 43;
            this.buttonTargetConnectionString.Text = "...";
            this.buttonTargetConnectionString.UseVisualStyleBackColor = true;
            this.buttonTargetConnectionString.Click += new System.EventHandler(this.buttonTargetConnectionString_Click);
            // 
            // labelTargetConnectionString
            // 
            this.labelTargetConnectionString.AutoSize = true;
            this.labelTargetConnectionString.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTargetConnectionString.ForeColor = System.Drawing.Color.Green;
            this.labelTargetConnectionString.Location = new System.Drawing.Point(254, 88);
            this.labelTargetConnectionString.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelTargetConnectionString.MinimumSize = new System.Drawing.Size(128, 0);
            this.labelTargetConnectionString.Name = "labelTargetConnectionString";
            this.labelTargetConnectionString.Size = new System.Drawing.Size(128, 20);
            this.labelTargetConnectionString.TabIndex = 44;
            // 
            // labelSchemaFile
            // 
            this.labelSchemaFile.AutoSize = true;
            this.labelSchemaFile.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSchemaFile.Location = new System.Drawing.Point(52, 123);
            this.labelSchemaFile.Name = "labelSchemaFile";
            this.labelSchemaFile.Size = new System.Drawing.Size(262, 20);
            this.labelSchemaFile.TabIndex = 17;
            this.labelSchemaFile.Text = "Select the location of your schema file";
            // 
            // buttonSchemaLocation
            // 
            this.buttonSchemaLocation.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSchemaLocation.Location = new System.Drawing.Point(497, 147);
            this.buttonSchemaLocation.Name = "buttonSchemaLocation";
            this.buttonSchemaLocation.Size = new System.Drawing.Size(34, 34);
            this.buttonSchemaLocation.TabIndex = 16;
            this.buttonSchemaLocation.Text = "...";
            this.buttonSchemaLocation.UseVisualStyleBackColor = true;
            this.buttonSchemaLocation.Click += new System.EventHandler(this.buttonSchemaLocation_Click);
            // 
            // textBoxSchemaLocation
            // 
            this.textBoxSchemaLocation.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSchemaLocation.Location = new System.Drawing.Point(56, 154);
            this.textBoxSchemaLocation.Name = "textBoxSchemaLocation";
            this.textBoxSchemaLocation.Size = new System.Drawing.Size(399, 27);
            this.textBoxSchemaLocation.TabIndex = 15;
            // 
            // numericUpDownBatchSize
            // 
            this.numericUpDownBatchSize.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownBatchSize.Location = new System.Drawing.Point(225, 263);
            this.numericUpDownBatchSize.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numericUpDownBatchSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownBatchSize.Name = "numericUpDownBatchSize";
            this.numericUpDownBatchSize.Size = new System.Drawing.Size(87, 27);
            this.numericUpDownBatchSize.TabIndex = 13;
            this.numericUpDownBatchSize.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(55, 265);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 20);
            this.label5.TabIndex = 12;
            this.label5.Text = "Batch Size";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(52, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(138, 25);
            this.label4.TabIndex = 10;
            this.label4.Text = "Export Settings";
            // 
            // wizardPage5
            // 
            this.wizardPage5.Controls.Add(this.label10);
            this.wizardPage5.Controls.Add(this.textBoxLogs);
            this.wizardPage5.IsFinishPage = true;
            this.wizardPage5.Name = "wizardPage5";
            this.wizardPage5.ShowCancel = false;
            this.wizardPage5.Size = new System.Drawing.Size(1291, 668);
            this.wizardPage5.TabIndex = 6;
            this.wizardPage5.Text = "Page Title";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Top;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(0, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(40, 20);
            this.label10.TabIndex = 22;
            this.label10.Text = "Logs";
            // 
            // textBoxLogs
            // 
            this.textBoxLogs.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBoxLogs.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxLogs.Location = new System.Drawing.Point(0, 31);
            this.textBoxLogs.Multiline = true;
            this.textBoxLogs.Name = "textBoxLogs";
            this.textBoxLogs.ReadOnly = true;
            this.textBoxLogs.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxLogs.Size = new System.Drawing.Size(1291, 637);
            this.textBoxLogs.TabIndex = 21;
            // 
            // openFileDialogExportConfigFile
            // 
            this.openFileDialogExportConfigFile.FileName = "openFileDialog1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.stepWizardControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.wizardButtons1);
            this.splitContainer1.Panel2MinSize = 0;
            this.splitContainer1.Size = new System.Drawing.Size(1291, 744);
            this.splitContainer1.SplitterDistance = 668;
            this.splitContainer1.TabIndex = 1;
            // 
            // wizardButtons1
            // 
            this.wizardButtons1.Container = this.stepWizardControl1;
            this.wizardButtons1.Dock = System.Windows.Forms.DockStyle.Right;
            this.wizardButtons1.Location = new System.Drawing.Point(1038, 0);
            this.wizardButtons1.Name = "wizardButtons1";
            this.wizardButtons1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.wizardButtons1.ShowExecuteButton = false;
            this.wizardButtons1.Size = new System.Drawing.Size(253, 72);
            this.wizardButtons1.TabIndex = 1;
            // 
            // exportWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "exportWizard";
            this.Size = new System.Drawing.Size(1291, 744);
            ((System.ComponentModel.ISupportInitialize)(this.stepWizardControl1)).EndInit();
            this.stepWizardControl1.ResumeLayout(false);
            this.wizardPage1.ResumeLayout(false);
            this.wizardPage1.PerformLayout();
            this.wizardPage2.ResumeLayout(false);
            this.wizardPage2.PerformLayout();
            this.wizardPage3.ResumeLayout(false);
            this.wizardPage3.PerformLayout();
            this.wizardPage4.ResumeLayout(false);
            this.wizardPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBatchSize)).EndInit();
            this.wizardPage5.ResumeLayout(false);
            this.wizardPage5.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AeroWizard.WizardPageContainer stepWizardControl1;
        private AeroWizard.WizardPage wizardPage1;
        private AeroWizard.WizardPage wizardPage2;
        private AeroWizard.WizardPage wizardPage3;
        private AeroWizard.WizardPage wizardPage4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioButtonFormatJson;
        private System.Windows.Forms.RadioButton radioButtonFormatCsv;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonExportLocation;
        private System.Windows.Forms.TextBox textBoxExportLocation;
        private System.Windows.Forms.Button buttonExportConfigLocation;
        private System.Windows.Forms.TextBox textBoxExportConfigLocation;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericUpDownBatchSize;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogExportLocation;
        private System.Windows.Forms.OpenFileDialog openFileDialogExportConfigFile;
        private System.Windows.Forms.Label labelSchemaFile;
        private System.Windows.Forms.Button buttonSchemaLocation;
        private System.Windows.Forms.TextBox textBoxSchemaLocation;
        private System.Windows.Forms.Label labelConnectionString;
        private System.Windows.Forms.Button buttonTargetConnectionString;
        private System.Windows.Forms.Label labelTargetConnectionString;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label6;
        private Xrm.DataMigration.XrmToolBoxPlugin.UserControls.ToggleCheckBox checkBoxMinimize;
        private System.Windows.Forms.Label label9;
        private Xrm.DataMigration.XrmToolBoxPlugin.UserControls.ToggleCheckBox checkBoxExportInactiveRecords;
        private AeroWizard.WizardPage wizardPage5;
        private System.Windows.Forms.TextBox textBoxLogs;
        private System.Windows.Forms.Label label10;
        private WizardButtons wizardButtons1;
    }
}
