using Capgemini.Xrm.CdsDataMigratorLibrary.UserControls;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.UserControls
{
    partial class ImportWizard
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
            this.wizardPage4 = new AeroWizard.WizardPage();
            this.labelLogLevel = new System.Windows.Forms.Label();
            this.comboBoxLogLevel = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbLogger = new System.Windows.Forms.TextBox();
            this.wizardPage3 = new AeroWizard.WizardPage();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cbIgnoreStatuses = new Capgemini.Xrm.CdsDataMigratorLibrary.UserControls.ToggleCheckBox();
            this.cbIgnoreSystemFields = new Capgemini.Xrm.CdsDataMigratorLibrary.UserControls.ToggleCheckBox();
            this.nudSavePageSize = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.nudMaxThreads = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.ConnectionString = new System.Windows.Forms.Label();
            this.buttonTargetConnectionString = new System.Windows.Forms.Button();
            this.labelTargetConnectionString = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.wizardButtonsImportData = new Capgemini.Xrm.CdsDataMigratorLibrary.UserControls.WizardButtons();
            ((System.ComponentModel.ISupportInitialize)(this.stepWizardControl1)).BeginInit();
            this.stepWizardControl1.SuspendLayout();
            this.wizardPage4.SuspendLayout();
            this.dataFormat.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.wizardPage5.SuspendLayout();
            this.wizardPage2.SuspendLayout();
            this.wizardPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSavePageSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxThreads)).BeginInit();
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
            this.stepWizardControl1.Controls.Add(this.wizardPage2);
            this.stepWizardControl1.Controls.Add(this.wizardPage3);
            this.stepWizardControl1.Controls.Add(this.wizardPage2);
            this.stepWizardControl1.Controls.Add(this.dataFormat);
            this.stepWizardControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stepWizardControl1.Location = new System.Drawing.Point(0, 0);
            this.stepWizardControl1.Margin = new System.Windows.Forms.Padding(2);
            this.stepWizardControl1.Name = "stepWizardControl1";
            this.stepWizardControl1.NextButton = null;
            this.stepWizardControl1.Pages.Add(this.dataFormat);
            this.stepWizardControl1.Pages.Add(this.wizardPage5);
            this.stepWizardControl1.Pages.Add(this.wizardPage2);
            this.stepWizardControl1.Pages.Add(this.wizardPage3);
            this.stepWizardControl1.Pages.Add(this.wizardPage4);
            this.stepWizardControl1.ShowProgressInTaskbarIcon = true;
            this.stepWizardControl1.Size = new System.Drawing.Size(800, 500);
            this.stepWizardControl1.TabIndex = 0;
            // 
            // wizardPage4
            // 
            this.wizardPage4.AllowCancel = false;
            this.wizardPage4.AllowNext = false;
            this.wizardPage4.Controls.Add(this.labelLogLevel);
            this.wizardPage4.Controls.Add(this.comboBoxLogLevel);
            this.wizardPage4.Controls.Add(this.label10);
            this.wizardPage4.Controls.Add(this.tbLogger);
            this.wizardPage4.IsFinishPage = true;
            this.wizardPage4.Name = "wizardPage4";
            this.wizardPage4.ShowCancel = false;
            this.wizardPage4.Size = new System.Drawing.Size(800, 500);
            this.wizardPage4.TabIndex = 5;
            this.wizardPage4.Text = "Perform Import";
            // 
            // labelLogLevel
            // 
            this.labelLogLevel.AutoSize = true;
            this.labelLogLevel.Font = new System.Drawing.Font("Segoe UI", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLogLevel.Location = new System.Drawing.Point(18, 10);
            this.labelLogLevel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelLogLevel.Name = "labelLogLevel";
            this.labelLogLevel.Size = new System.Drawing.Size(105, 30);
            this.labelLogLevel.TabIndex = 16;
            this.labelLogLevel.Text = "Log Level:";
            // 
            // comboBoxLogLevel
            // 
            this.comboBoxLogLevel.FormattingEnabled = true;
            this.comboBoxLogLevel.Location = new System.Drawing.Point(141, 16);
            this.comboBoxLogLevel.Name = "comboBoxLogLevel";
            this.comboBoxLogLevel.Size = new System.Drawing.Size(184, 24);
            this.comboBoxLogLevel.TabIndex = 15;
            this.comboBoxLogLevel.SelectedIndexChanged += new System.EventHandler(this.ComboBoxLogLevelSelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(18, 46);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(51, 25);
            this.label10.TabIndex = 13;
            this.label10.Text = "Logs";
            // 
            // tbLogger
            // 
            this.tbLogger.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbLogger.BackColor = System.Drawing.SystemColors.Control;
            this.tbLogger.Location = new System.Drawing.Point(23, 73);
            this.tbLogger.Margin = new System.Windows.Forms.Padding(2);
            this.tbLogger.Multiline = true;
            this.tbLogger.Name = "tbLogger";
            this.tbLogger.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbLogger.Size = new System.Drawing.Size(751, 415);
            this.tbLogger.TabIndex = 14;
            // 
            // dataFormat
            // 
            this.dataFormat.AllowCancel = false;
            this.dataFormat.AllowNext = false;
            this.dataFormat.Controls.Add(this.groupBox1);
            this.dataFormat.Controls.Add(this.label1);
            this.dataFormat.Controls.Add(this.radioButtonJsonFormat);
            this.dataFormat.Controls.Add(this.radioButtonCSVFormat);
            this.dataFormat.Name = "dataFormat";
            this.dataFormat.NextPage = this.wizardPage5;
            this.dataFormat.ShowCancel = false;
            this.dataFormat.Size = new System.Drawing.Size(800, 500);
            this.dataFormat.TabIndex = 2;
            this.dataFormat.Text = "Select Data Format";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbImportSchema);
            this.groupBox1.Controls.Add(this.btnImportSchema);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.groupBox1.Location = new System.Drawing.Point(35, 141);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(5);
            this.groupBox1.Size = new System.Drawing.Size(762, 84);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Schema file (CSV only)";
            this.groupBox1.Visible = false;
            // 
            // tbImportSchema
            // 
            this.tbImportSchema.Location = new System.Drawing.Point(7, 31);
            this.tbImportSchema.Margin = new System.Windows.Forms.Padding(2);
            this.tbImportSchema.Name = "tbImportSchema";
            this.tbImportSchema.Size = new System.Drawing.Size(695, 32);
            this.tbImportSchema.TabIndex = 4;
            this.tbImportSchema.TextChanged += new System.EventHandler(this.TbImportSchemeTextChanged);
            // 
            // btnImportSchema
            // 
            this.btnImportSchema.Location = new System.Drawing.Point(706, 31);
            this.btnImportSchema.Margin = new System.Windows.Forms.Padding(2);
            this.btnImportSchema.Name = "btnImportSchema";
            this.btnImportSchema.Size = new System.Drawing.Size(46, 33);
            this.btnImportSchema.TabIndex = 3;
            this.btnImportSchema.Text = "...";
            this.btnImportSchema.UseVisualStyleBackColor = true;
            this.btnImportSchema.Click += new System.EventHandler(this.Button3Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(30, 26);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(594, 32);
            this.label1.TabIndex = 2;
            this.label1.Text = "Which format would you like to import the data from?";
            // 
            // radioButtonJsonFormat
            // 
            this.radioButtonJsonFormat.AutoSize = true;
            this.radioButtonJsonFormat.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonJsonFormat.Location = new System.Drawing.Point(35, 78);
            this.radioButtonJsonFormat.Margin = new System.Windows.Forms.Padding(2);
            this.radioButtonJsonFormat.Name = "radioButtonJsonFormat";
            this.radioButtonJsonFormat.Size = new System.Drawing.Size(78, 29);
            this.radioButtonJsonFormat.TabIndex = 1;
            this.radioButtonJsonFormat.TabStop = true;
            this.radioButtonJsonFormat.Text = "JSON";
            this.radioButtonJsonFormat.UseVisualStyleBackColor = true;
            this.radioButtonJsonFormat.CheckedChanged += new System.EventHandler(this.RadioButtonCheckedChanged);
            // 
            // radioButtonCSVFormat
            // 
            this.radioButtonCSVFormat.AutoSize = true;
            this.radioButtonCSVFormat.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonCSVFormat.Location = new System.Drawing.Point(35, 109);
            this.radioButtonCSVFormat.Margin = new System.Windows.Forms.Padding(2);
            this.radioButtonCSVFormat.Name = "radioButtonCSVFormat";
            this.radioButtonCSVFormat.Size = new System.Drawing.Size(67, 29);
            this.radioButtonCSVFormat.TabIndex = 0;
            this.radioButtonCSVFormat.TabStop = true;
            this.radioButtonCSVFormat.Text = "CSV";
            this.radioButtonCSVFormat.UseVisualStyleBackColor = true;
            this.radioButtonCSVFormat.CheckedChanged += new System.EventHandler(this.RadioButton1CheckedChanged);
            // 
            // wizardPage5
            // 
            this.wizardPage5.AllowCancel = false;
            this.wizardPage5.Controls.Add(this.label7);
            this.wizardPage5.Controls.Add(this.label4);
            this.wizardPage5.Controls.Add(this.btLoadImportConfigFile);
            this.wizardPage5.Controls.Add(this.tbImportConfigFile);
            this.wizardPage5.Name = "wizardPage5";
            this.wizardPage5.NextPage = this.wizardPage2;
            this.wizardPage5.ShowCancel = false;
            this.wizardPage5.Size = new System.Drawing.Size(800, 500);
            this.wizardPage5.TabIndex = 6;
            this.wizardPage5.Text = "Import Config";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(30, 26);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(683, 32);
            this.label7.TabIndex = 49;
            this.label7.Text = "Do you have an existing Import Config file? Please supply if so";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(31, 72);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(253, 25);
            this.label4.TabIndex = 46;
            this.label4.Text = "Import Config File (Optional)";
            // 
            // btLoadImportConfigFile
            // 
            this.btLoadImportConfigFile.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btLoadImportConfigFile.Location = new System.Drawing.Point(748, 105);
            this.btLoadImportConfigFile.Margin = new System.Windows.Forms.Padding(1);
            this.btLoadImportConfigFile.Name = "btLoadImportConfigFile";
            this.btLoadImportConfigFile.Size = new System.Drawing.Size(46, 33);
            this.btLoadImportConfigFile.TabIndex = 48;
            this.btLoadImportConfigFile.Text = "...";
            this.btLoadImportConfigFile.UseVisualStyleBackColor = true;
            this.btLoadImportConfigFile.Click += new System.EventHandler(this.LoadImportConfigFileButtonClick);
            // 
            // tbImportConfigFile
            // 
            this.tbImportConfigFile.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbImportConfigFile.Location = new System.Drawing.Point(31, 105);
            this.tbImportConfigFile.Margin = new System.Windows.Forms.Padding(2);
            this.tbImportConfigFile.Name = "tbImportConfigFile";
            this.tbImportConfigFile.Size = new System.Drawing.Size(714, 32);
            this.tbImportConfigFile.TabIndex = 47;
            this.tbImportConfigFile.TextChanged += new System.EventHandler(this.TabImportConfigFileTextChanged);
            // 
            // wizardPage2
            // 
            this.wizardPage2.AllowCancel = false;
            this.wizardPage2.AllowNext = false;
            this.wizardPage2.Controls.Add(this.labelFolderPathValidation);
            this.wizardPage2.Controls.Add(this.tbSourceDataLocation);
            this.wizardPage2.Controls.Add(this.button1);
            this.wizardPage2.Controls.Add(this.label2);
            this.wizardPage2.Name = "wizardPage2";
            this.wizardPage2.NextPage = this.wizardPage3;
            this.wizardPage2.ShowCancel = false;
            this.wizardPage2.Size = new System.Drawing.Size(800, 500);
            this.wizardPage2.TabIndex = 3;
            this.wizardPage2.Text = "Select Source Directory";
            // 
            // labelFolderPathValidation
            // 
            this.labelFolderPathValidation.AutoSize = true;
            this.labelFolderPathValidation.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFolderPathValidation.ForeColor = System.Drawing.Color.Red;
            this.labelFolderPathValidation.Location = new System.Drawing.Point(39, 67);
            this.labelFolderPathValidation.Name = "labelFolderPathValidation";
            this.labelFolderPathValidation.Size = new System.Drawing.Size(170, 15);
            this.labelFolderPathValidation.TabIndex = 10;
            this.labelFolderPathValidation.Text = "Please Provide the folder path";
            this.labelFolderPathValidation.Visible = false;
            // 
            // tbSourceDataLocation
            // 
            this.tbSourceDataLocation.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.tbSourceDataLocation.Location = new System.Drawing.Point(35, 93);
            this.tbSourceDataLocation.Margin = new System.Windows.Forms.Padding(2);
            this.tbSourceDataLocation.Name = "tbSourceDataLocation";
            this.tbSourceDataLocation.Size = new System.Drawing.Size(706, 32);
            this.tbSourceDataLocation.TabIndex = 2;
            this.tbSourceDataLocation.TextChanged += new System.EventHandler(this.TabSourceDataLocationTextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(745, 93);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(46, 33);
            this.button1.TabIndex = 1;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.ImportFolderSelectionButtonClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(30, 26);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(451, 32);
            this.label2.TabIndex = 0;
            this.label2.Text = "What directory contains the source data?";
            // 
            // wizardPage3
            // 
            this.wizardPage3.AllowCancel = false;
            this.wizardPage3.AllowNext = false;
            this.wizardPage3.Controls.Add(this.label9);
            this.wizardPage3.Controls.Add(this.label8);
            this.wizardPage3.Controls.Add(this.cbIgnoreStatuses);
            this.wizardPage3.Controls.Add(this.cbIgnoreSystemFields);
            this.wizardPage3.Controls.Add(this.nudSavePageSize);
            this.wizardPage3.Controls.Add(this.label11);
            this.wizardPage3.Controls.Add(this.nudMaxThreads);
            this.wizardPage3.Controls.Add(this.label5);
            this.wizardPage3.Controls.Add(this.ConnectionString);
            this.wizardPage3.Controls.Add(this.buttonTargetConnectionString);
            this.wizardPage3.Controls.Add(this.labelTargetConnectionString);
            this.wizardPage3.Controls.Add(this.label3);
            this.wizardPage3.Name = "wizardPage3";
            this.wizardPage3.NextPage = this.wizardPage4;
            this.wizardPage3.ShowCancel = false;
            this.wizardPage3.Size = new System.Drawing.Size(800, 500);
            this.wizardPage3.TabIndex = 4;
            this.wizardPage3.Text = "Import Settings";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(31, 258);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(185, 25);
            this.label9.TabIndex = 57;
            this.label9.Text = "Ignore Record Status";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(31, 219);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(183, 25);
            this.label8.TabIndex = 56;
            this.label8.Text = "Ignore System Fields";
            // 
            // cbIgnoreStatuses
            // 
            this.cbIgnoreStatuses.Location = new System.Drawing.Point(268, 258);
            this.cbIgnoreStatuses.Margin = new System.Windows.Forms.Padding(2);
            this.cbIgnoreStatuses.Name = "cbIgnoreStatuses";
            this.cbIgnoreStatuses.Padding = new System.Windows.Forms.Padding(2);
            this.cbIgnoreStatuses.Size = new System.Drawing.Size(75, 31);
            this.cbIgnoreStatuses.TabIndex = 55;
            this.cbIgnoreStatuses.Text = "toggleCheckBox2";
            this.cbIgnoreStatuses.UseVisualStyleBackColor = true;
            // 
            // cbIgnoreSystemFields
            // 
            this.cbIgnoreSystemFields.Location = new System.Drawing.Point(268, 219);
            this.cbIgnoreSystemFields.Margin = new System.Windows.Forms.Padding(2);
            this.cbIgnoreSystemFields.Name = "cbIgnoreSystemFields";
            this.cbIgnoreSystemFields.Padding = new System.Windows.Forms.Padding(2);
            this.cbIgnoreSystemFields.Size = new System.Drawing.Size(75, 31);
            this.cbIgnoreSystemFields.TabIndex = 54;
            this.cbIgnoreSystemFields.Text = "toggleCheckBox1";
            this.cbIgnoreSystemFields.UseVisualStyleBackColor = true;
            // 
            // nudSavePageSize
            // 
            this.nudSavePageSize.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudSavePageSize.Location = new System.Drawing.Point(268, 129);
            this.nudSavePageSize.Margin = new System.Windows.Forms.Padding(2);
            this.nudSavePageSize.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.nudSavePageSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSavePageSize.Name = "nudSavePageSize";
            this.nudSavePageSize.Size = new System.Drawing.Size(75, 32);
            this.nudSavePageSize.TabIndex = 53;
            this.nudSavePageSize.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(31, 174);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(120, 25);
            this.label11.TabIndex = 49;
            this.label11.Text = "Max Threads";
            // 
            // nudMaxThreads
            // 
            this.nudMaxThreads.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudMaxThreads.Location = new System.Drawing.Point(268, 174);
            this.nudMaxThreads.Margin = new System.Windows.Forms.Padding(2);
            this.nudMaxThreads.Name = "nudMaxThreads";
            this.nudMaxThreads.Size = new System.Drawing.Size(75, 32);
            this.nudMaxThreads.TabIndex = 52;
            this.nudMaxThreads.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(31, 129);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(136, 25);
            this.label5.TabIndex = 47;
            this.label5.Text = "Save Page Size";
            // 
            // ConnectionString
            // 
            this.ConnectionString.AutoSize = true;
            this.ConnectionString.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConnectionString.Location = new System.Drawing.Point(31, 88);
            this.ConnectionString.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ConnectionString.Name = "ConnectionString";
            this.ConnectionString.Size = new System.Drawing.Size(220, 25);
            this.ConnectionString.TabIndex = 39;
            this.ConnectionString.Text = "Target Connection String";
            // 
            // buttonTargetConnectionString
            // 
            this.buttonTargetConnectionString.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonTargetConnectionString.Location = new System.Drawing.Point(658, 88);
            this.buttonTargetConnectionString.Margin = new System.Windows.Forms.Padding(2);
            this.buttonTargetConnectionString.Name = "buttonTargetConnectionString";
            this.buttonTargetConnectionString.Size = new System.Drawing.Size(46, 33);
            this.buttonTargetConnectionString.TabIndex = 40;
            this.buttonTargetConnectionString.Text = "...";
            this.buttonTargetConnectionString.UseVisualStyleBackColor = true;
            this.buttonTargetConnectionString.Click += new System.EventHandler(this.ButtonTargetConnectionStringClick);
            // 
            // labelTargetConnectionString
            // 
            this.labelTargetConnectionString.AutoSize = true;
            this.labelTargetConnectionString.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTargetConnectionString.ForeColor = System.Drawing.Color.Green;
            this.labelTargetConnectionString.Location = new System.Drawing.Point(263, 92);
            this.labelTargetConnectionString.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelTargetConnectionString.MinimumSize = new System.Drawing.Size(97, 0);
            this.labelTargetConnectionString.Name = "labelTargetConnectionString";
            this.labelTargetConnectionString.Size = new System.Drawing.Size(97, 25);
            this.labelTargetConnectionString.TabIndex = 41;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(30, 26);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(357, 32);
            this.label3.TabIndex = 0;
            this.label3.Text = "Please set the setting for import";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.wizardButtonsImportData);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.stepWizardControl1);
            this.splitContainer1.Size = new System.Drawing.Size(800, 552);
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 1;
            // 
            // wizardButtons1
            // 
            this.wizardButtonsImportData.Dock = System.Windows.Forms.DockStyle.Right;
            this.wizardButtonsImportData.Location = new System.Drawing.Point(300, 0);
            this.wizardButtonsImportData.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.wizardButtonsImportData.Name = "wizardButtons1";
            this.wizardButtonsImportData.PageContainer = this.stepWizardControl1;
            this.wizardButtonsImportData.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.wizardButtonsImportData.ShowExecuteButton = false;
            this.wizardButtonsImportData.Size = new System.Drawing.Size(500, 50);
            this.wizardButtonsImportData.TabIndex = 0;
            // 
            // ImportWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(800, 552);
            this.Name = "ImportWizard";
            this.Size = new System.Drawing.Size(800, 552);
            ((System.ComponentModel.ISupportInitialize)(this.stepWizardControl1)).EndInit();
            this.stepWizardControl1.ResumeLayout(false);
            this.wizardPage4.ResumeLayout(false);
            this.wizardPage4.PerformLayout();
            this.dataFormat.ResumeLayout(false);
            this.dataFormat.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.wizardPage5.ResumeLayout(false);
            this.wizardPage5.PerformLayout();
            this.wizardPage2.ResumeLayout(false);
            this.wizardPage2.PerformLayout();
            this.wizardPage3.ResumeLayout(false);
            this.wizardPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSavePageSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxThreads)).EndInit();
            this.wizardPage5.ResumeLayout(false);
            this.wizardPage5.PerformLayout();
            this.wizardPage2.ResumeLayout(false);
            this.wizardPage2.PerformLayout();
            this.dataFormat.ResumeLayout(false);
            this.dataFormat.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AeroWizard.WizardPageContainer stepWizardControl1;
        private AeroWizard.WizardPage dataFormat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioButtonJsonFormat;
        private System.Windows.Forms.RadioButton radioButtonCSVFormat;
        private AeroWizard.WizardPage wizardPage2;
        private System.Windows.Forms.TextBox tbSourceDataLocation;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private AeroWizard.WizardPage wizardPage3;
        private System.Windows.Forms.Label label3;
        private AeroWizard.WizardPage wizardPage4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label ConnectionString;
        private System.Windows.Forms.Button buttonTargetConnectionString;
        private System.Windows.Forms.Label labelTargetConnectionString;
        private AeroWizard.WizardPage wizardPage5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btLoadImportConfigFile;
        private System.Windows.Forms.TextBox tbImportConfigFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.NumericUpDown nudSavePageSize;
        private System.Windows.Forms.NumericUpDown nudMaxThreads;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbImportSchema;
        private System.Windows.Forms.Button btnImportSchema;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Capgemini.Xrm.CdsDataMigratorLibrary.UserControls.WizardButtons wizardButtonsImportData;
        private ToggleCheckBox cbIgnoreStatuses;
        private ToggleCheckBox cbIgnoreSystemFields;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbLogger;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label labelFolderPathValidation;
        private System.Windows.Forms.Label labelLogLevel;
        private System.Windows.Forms.ComboBox comboBoxLogLevel;
    }
}
