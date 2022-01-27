using Capgemini.Xrm.CdsDataMigratorLibrary.UserControls;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.UserControls
{
    partial class ExportWizard
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
            this.wizardPage5 = new AeroWizard.WizardPage();
            this.labelLogLevel = new System.Windows.Forms.Label();
            this.comboBoxLogLevel = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxLogs = new System.Windows.Forms.TextBox();
            this.format = new AeroWizard.WizardPage();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButtonFormatJson = new System.Windows.Forms.RadioButton();
            this.radioButtonFormatCsv = new System.Windows.Forms.RadioButton();
            this.exportConfig = new AeroWizard.WizardPage();
            this.buttonExportConfigLocation = new System.Windows.Forms.Button();
            this.textBoxExportConfigLocation = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.exportLocation = new AeroWizard.WizardPage();
            this.labelFolderPathValidation = new System.Windows.Forms.Label();
            this.buttonExportLocation = new System.Windows.Forms.Button();
            this.textBoxExportLocation = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.executeExport = new AeroWizard.WizardPage();
            this.labelExportConnectionValidation = new System.Windows.Forms.Label();
            this.labelSchemaLocationFileValidation = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBoxMinimize = new Capgemini.Xrm.CdsDataMigratorLibrary.UserControls.ToggleCheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.checkBoxExportInactiveRecords = new Capgemini.Xrm.CdsDataMigratorLibrary.UserControls.ToggleCheckBox();
            this.labelConnectionString = new System.Windows.Forms.Label();
            this.buttonTargetConnectionString = new System.Windows.Forms.Button();
            this.labelTargetConnectionString = new System.Windows.Forms.Label();
            this.labelSchemaFile = new System.Windows.Forms.Label();
            this.buttonSchemaLocation = new System.Windows.Forms.Button();
            this.textBoxSchemaLocation = new System.Windows.Forms.TextBox();
            this.numericUpDownBatchSize = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.folderBrowserDialogExportLocation = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialogExportConfigFile = new System.Windows.Forms.OpenFileDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.wizardButtons1 = new Capgemini.Xrm.CdsDataMigratorLibrary.UserControls.WizardButtons();
            ((System.ComponentModel.ISupportInitialize)(this.stepWizardControl1)).BeginInit();
            this.stepWizardControl1.SuspendLayout();
            this.wizardPage5.SuspendLayout();
            this.format.SuspendLayout();
            this.exportConfig.SuspendLayout();
            this.exportLocation.SuspendLayout();
            this.executeExport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBatchSize)).BeginInit();
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
            this.stepWizardControl1.Controls.Add(this.exportLocation);
            this.stepWizardControl1.Controls.Add(this.exportConfig);
            this.stepWizardControl1.Controls.Add(this.wizardPage5);
            this.stepWizardControl1.Controls.Add(this.executeExport);
            this.stepWizardControl1.Controls.Add(this.format);
            this.stepWizardControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stepWizardControl1.Location = new System.Drawing.Point(0, 0);
            this.stepWizardControl1.Margin = new System.Windows.Forms.Padding(4);
            this.stepWizardControl1.Name = "stepWizardControl1";
            this.stepWizardControl1.NextButton = null;
            this.stepWizardControl1.Pages.Add(this.format);
            this.stepWizardControl1.Pages.Add(this.exportConfig);
            this.stepWizardControl1.Pages.Add(this.exportLocation);
            this.stepWizardControl1.Pages.Add(this.executeExport);
            this.stepWizardControl1.Pages.Add(this.wizardPage5);
            this.stepWizardControl1.Size = new System.Drawing.Size(800, 501);
            this.stepWizardControl1.TabIndex = 0;
            // 
            // wizardPage5
            // 
            this.wizardPage5.Controls.Add(this.labelLogLevel);
            this.wizardPage5.Controls.Add(this.comboBoxLogLevel);
            this.wizardPage5.Controls.Add(this.label10);
            this.wizardPage5.Controls.Add(this.textBoxLogs);
            this.wizardPage5.IsFinishPage = true;
            this.wizardPage5.Name = "wizardPage5";
            this.wizardPage5.ShowCancel = false;
            this.wizardPage5.ShowNext = false;
            this.wizardPage5.Size = new System.Drawing.Size(800, 501);
            this.wizardPage5.TabIndex = 6;
            this.wizardPage5.Text = "Results";
            // 
            // labelLogLevel
            // 
            this.labelLogLevel.AutoSize = true;
            this.labelLogLevel.Font = new System.Drawing.Font("Segoe UI", 12.25F);
            this.labelLogLevel.Location = new System.Drawing.Point(17, 17);
            this.labelLogLevel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelLogLevel.Name = "labelLogLevel";
            this.labelLogLevel.Size = new System.Drawing.Size(105, 30);
            this.labelLogLevel.TabIndex = 25;
            this.labelLogLevel.Text = "Log Level:";
            // 
            // comboBoxLogLevel
            // 
            this.comboBoxLogLevel.FormattingEnabled = true;
            this.comboBoxLogLevel.Location = new System.Drawing.Point(131, 21);
            this.comboBoxLogLevel.Name = "comboBoxLogLevel";
            this.comboBoxLogLevel.Size = new System.Drawing.Size(184, 24);
            this.comboBoxLogLevel.TabIndex = 24;
            this.comboBoxLogLevel.SelectedIndexChanged += new System.EventHandler(this.ComboBoxLogLevelSelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(17, 47);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(51, 25);
            this.label10.TabIndex = 22;
            this.label10.Text = "Logs";
            // 
            // textBoxLogs
            // 
            this.textBoxLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxLogs.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxLogs.Location = new System.Drawing.Point(22, 74);
            this.textBoxLogs.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxLogs.Multiline = true;
            this.textBoxLogs.Name = "textBoxLogs";
            this.textBoxLogs.ReadOnly = true;
            this.textBoxLogs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxLogs.Size = new System.Drawing.Size(757, 416);
            this.textBoxLogs.TabIndex = 23;
            // 
            // format
            // 
            this.format.Controls.Add(this.label1);
            this.format.Controls.Add(this.radioButtonFormatJson);
            this.format.Controls.Add(this.radioButtonFormatCsv);
            this.format.Name = "format";
            this.format.NextPage = this.exportConfig;
            this.format.ShowCancel = false;
            this.format.Size = new System.Drawing.Size(800, 501);
            this.format.TabIndex = 2;
            this.format.Text = "Select Data Format";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(36, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(562, 32);
            this.label1.TabIndex = 5;
            this.label1.Text = "Which format would you like to export the data to?";
            // 
            // radioButtonFormatJson
            // 
            this.radioButtonFormatJson.AutoSize = true;
            this.radioButtonFormatJson.Checked = true;
            this.radioButtonFormatJson.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonFormatJson.Location = new System.Drawing.Point(42, 76);
            this.radioButtonFormatJson.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonFormatJson.Name = "radioButtonFormatJson";
            this.radioButtonFormatJson.Size = new System.Drawing.Size(78, 29);
            this.radioButtonFormatJson.TabIndex = 4;
            this.radioButtonFormatJson.TabStop = true;
            this.radioButtonFormatJson.Text = "JSON";
            this.radioButtonFormatJson.UseVisualStyleBackColor = true;
            // 
            // radioButtonFormatCsv
            // 
            this.radioButtonFormatCsv.AutoSize = true;
            this.radioButtonFormatCsv.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonFormatCsv.Location = new System.Drawing.Point(42, 111);
            this.radioButtonFormatCsv.Margin = new System.Windows.Forms.Padding(4);
            this.radioButtonFormatCsv.Name = "radioButtonFormatCsv";
            this.radioButtonFormatCsv.Size = new System.Drawing.Size(67, 29);
            this.radioButtonFormatCsv.TabIndex = 3;
            this.radioButtonFormatCsv.Text = "CSV";
            this.radioButtonFormatCsv.UseVisualStyleBackColor = true;
            // 
            // exportConfig
            // 
            this.exportConfig.Controls.Add(this.buttonExportConfigLocation);
            this.exportConfig.Controls.Add(this.textBoxExportConfigLocation);
            this.exportConfig.Controls.Add(this.label3);
            this.exportConfig.Name = "exportConfig";
            this.exportConfig.NextPage = this.exportLocation;
            this.exportConfig.ShowCancel = false;
            this.exportConfig.Size = new System.Drawing.Size(800, 501);
            this.exportConfig.TabIndex = 4;
            this.exportConfig.Text = "Export Config";
            // 
            // buttonExportConfigLocation
            // 
            this.buttonExportConfigLocation.Location = new System.Drawing.Point(735, 84);
            this.buttonExportConfigLocation.Margin = new System.Windows.Forms.Padding(4);
            this.buttonExportConfigLocation.Name = "buttonExportConfigLocation";
            this.buttonExportConfigLocation.Size = new System.Drawing.Size(46, 33);
            this.buttonExportConfigLocation.TabIndex = 11;
            this.buttonExportConfigLocation.Text = "...";
            this.buttonExportConfigLocation.UseVisualStyleBackColor = true;
            this.buttonExportConfigLocation.Click += new System.EventHandler(this.ButtonExportConfigLocationClick);
            // 
            // textBoxExportConfigLocation
            // 
            this.textBoxExportConfigLocation.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.textBoxExportConfigLocation.Location = new System.Drawing.Point(29, 84);
            this.textBoxExportConfigLocation.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxExportConfigLocation.Name = "textBoxExportConfigLocation";
            this.textBoxExportConfigLocation.Size = new System.Drawing.Size(698, 32);
            this.textBoxExportConfigLocation.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(23, 27);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(599, 32);
            this.label3.TabIndex = 9;
            this.label3.Text = "Select the location of your export config file (Optional)";
            // 
            // exportLocation
            // 
            this.exportLocation.Controls.Add(this.labelFolderPathValidation);
            this.exportLocation.Controls.Add(this.buttonExportLocation);
            this.exportLocation.Controls.Add(this.textBoxExportLocation);
            this.exportLocation.Controls.Add(this.label2);
            this.exportLocation.Name = "exportLocation";
            this.exportLocation.NextPage = this.executeExport;
            this.exportLocation.ShowCancel = false;
            this.exportLocation.Size = new System.Drawing.Size(800, 501);
            this.exportLocation.TabIndex = 3;
            this.exportLocation.Text = "Select Export Location";
            // 
            // labelFolderPathValidation
            // 
            this.labelFolderPathValidation.AutoSize = true;
            this.labelFolderPathValidation.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFolderPathValidation.ForeColor = System.Drawing.Color.Red;
            this.labelFolderPathValidation.Location = new System.Drawing.Point(35, 75);
            this.labelFolderPathValidation.Name = "labelFolderPathValidation";
            this.labelFolderPathValidation.Size = new System.Drawing.Size(170, 15);
            this.labelFolderPathValidation.TabIndex = 9;
            this.labelFolderPathValidation.Text = "Please Provide the folder path";
            this.labelFolderPathValidation.Visible = false;
            // 
            // buttonExportLocation
            // 
            this.buttonExportLocation.Location = new System.Drawing.Point(745, 93);
            this.buttonExportLocation.Margin = new System.Windows.Forms.Padding(4);
            this.buttonExportLocation.Name = "buttonExportLocation";
            this.buttonExportLocation.Size = new System.Drawing.Size(46, 33);
            this.buttonExportLocation.TabIndex = 8;
            this.buttonExportLocation.Text = "...";
            this.buttonExportLocation.UseVisualStyleBackColor = true;
            this.buttonExportLocation.Click += new System.EventHandler(this.ButtonExportLocationClick);
            // 
            // textBoxExportLocation
            // 
            this.textBoxExportLocation.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.textBoxExportLocation.Location = new System.Drawing.Point(38, 93);
            this.textBoxExportLocation.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxExportLocation.Name = "textBoxExportLocation";
            this.textBoxExportLocation.Size = new System.Drawing.Size(699, 32);
            this.textBoxExportLocation.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(32, 29);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(516, 32);
            this.label2.TabIndex = 6;
            this.label2.Text = "Where do you want to save the exported data?";
            // 
            // executeExport
            // 
            this.executeExport.Controls.Add(this.labelExportConnectionValidation);
            this.executeExport.Controls.Add(this.labelSchemaLocationFileValidation);
            this.executeExport.Controls.Add(this.label6);
            this.executeExport.Controls.Add(this.checkBoxMinimize);
            this.executeExport.Controls.Add(this.label9);
            this.executeExport.Controls.Add(this.checkBoxExportInactiveRecords);
            this.executeExport.Controls.Add(this.labelConnectionString);
            this.executeExport.Controls.Add(this.buttonTargetConnectionString);
            this.executeExport.Controls.Add(this.labelTargetConnectionString);
            this.executeExport.Controls.Add(this.labelSchemaFile);
            this.executeExport.Controls.Add(this.buttonSchemaLocation);
            this.executeExport.Controls.Add(this.textBoxSchemaLocation);
            this.executeExport.Controls.Add(this.numericUpDownBatchSize);
            this.executeExport.Controls.Add(this.label5);
            this.executeExport.Controls.Add(this.label4);
            this.executeExport.Name = "executeExport";
            this.executeExport.NextPage = this.wizardPage5;
            this.executeExport.ShowCancel = false;
            this.executeExport.Size = new System.Drawing.Size(800, 501);
            this.executeExport.TabIndex = 5;
            this.executeExport.Text = "Perform Export";
            // 
            // labelExportConnectionValidation
            // 
            this.labelExportConnectionValidation.AutoSize = true;
            this.labelExportConnectionValidation.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelExportConnectionValidation.ForeColor = System.Drawing.Color.Red;
            this.labelExportConnectionValidation.Location = new System.Drawing.Point(39, 81);
            this.labelExportConnectionValidation.Name = "labelExportConnectionValidation";
            this.labelExportConnectionValidation.Size = new System.Drawing.Size(260, 15);
            this.labelExportConnectionValidation.TabIndex = 63;
            this.labelExportConnectionValidation.Text = "Please Provide the location of your schema file";
            this.labelExportConnectionValidation.Visible = false;
            // 
            // labelSchemaLocationFileValidation
            // 
            this.labelSchemaLocationFileValidation.AutoSize = true;
            this.labelSchemaLocationFileValidation.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSchemaLocationFileValidation.ForeColor = System.Drawing.Color.Red;
            this.labelSchemaLocationFileValidation.Location = new System.Drawing.Point(39, 162);
            this.labelSchemaLocationFileValidation.Name = "labelSchemaLocationFileValidation";
            this.labelSchemaLocationFileValidation.Size = new System.Drawing.Size(260, 15);
            this.labelSchemaLocationFileValidation.TabIndex = 62;
            this.labelSchemaLocationFileValidation.Text = "Please Provide the location of your schema file";
            this.labelSchemaLocationFileValidation.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(37, 273);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(151, 25);
            this.label6.TabIndex = 61;
            this.label6.Text = "Minimise (JSON)";
            // 
            // checkBoxMinimize
            // 
            this.checkBoxMinimize.Location = new System.Drawing.Point(263, 270);
            this.checkBoxMinimize.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxMinimize.Name = "checkBoxMinimize";
            this.checkBoxMinimize.Padding = new System.Windows.Forms.Padding(5);
            this.checkBoxMinimize.Size = new System.Drawing.Size(65, 31);
            this.checkBoxMinimize.TabIndex = 60;
            this.checkBoxMinimize.Text = "toggleCheckBox2";
            this.checkBoxMinimize.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(37, 226);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(207, 25);
            this.label9.TabIndex = 59;
            this.label9.Text = "Export Inactive Records";
            // 
            // checkBoxExportInactiveRecords
            // 
            this.checkBoxExportInactiveRecords.Location = new System.Drawing.Point(263, 226);
            this.checkBoxExportInactiveRecords.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxExportInactiveRecords.Name = "checkBoxExportInactiveRecords";
            this.checkBoxExportInactiveRecords.Padding = new System.Windows.Forms.Padding(5);
            this.checkBoxExportInactiveRecords.Size = new System.Drawing.Size(65, 31);
            this.checkBoxExportInactiveRecords.TabIndex = 58;
            this.checkBoxExportInactiveRecords.Text = "toggleCheckBox2";
            this.checkBoxExportInactiveRecords.UseVisualStyleBackColor = true;
            // 
            // labelConnectionString
            // 
            this.labelConnectionString.AutoSize = true;
            this.labelConnectionString.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.labelConnectionString.Location = new System.Drawing.Point(33, 94);
            this.labelConnectionString.Name = "labelConnectionString";
            this.labelConnectionString.Size = new System.Drawing.Size(220, 25);
            this.labelConnectionString.TabIndex = 45;
            this.labelConnectionString.Text = "Target Connection String";
            // 
            // buttonTargetConnectionString
            // 
            this.buttonTargetConnectionString.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonTargetConnectionString.Location = new System.Drawing.Point(626, 94);
            this.buttonTargetConnectionString.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.buttonTargetConnectionString.Name = "buttonTargetConnectionString";
            this.buttonTargetConnectionString.Size = new System.Drawing.Size(46, 33);
            this.buttonTargetConnectionString.TabIndex = 43;
            this.buttonTargetConnectionString.Text = "...";
            this.buttonTargetConnectionString.UseVisualStyleBackColor = true;
            this.buttonTargetConnectionString.Click += new System.EventHandler(this.ButtonTargetConnectionStringClick);
            // 
            // labelTargetConnectionString
            // 
            this.labelTargetConnectionString.AutoSize = true;
            this.labelTargetConnectionString.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTargetConnectionString.ForeColor = System.Drawing.Color.Green;
            this.labelTargetConnectionString.Location = new System.Drawing.Point(262, 94);
            this.labelTargetConnectionString.MinimumSize = new System.Drawing.Size(171, 0);
            this.labelTargetConnectionString.Name = "labelTargetConnectionString";
            this.labelTargetConnectionString.Size = new System.Drawing.Size(171, 25);
            this.labelTargetConnectionString.TabIndex = 44;
            // 
            // labelSchemaFile
            // 
            this.labelSchemaFile.AutoSize = true;
            this.labelSchemaFile.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSchemaFile.Location = new System.Drawing.Point(33, 137);
            this.labelSchemaFile.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSchemaFile.Name = "labelSchemaFile";
            this.labelSchemaFile.Size = new System.Drawing.Size(332, 25);
            this.labelSchemaFile.TabIndex = 17;
            this.labelSchemaFile.Text = "Select the location of your schema file";
            // 
            // buttonSchemaLocation
            // 
            this.buttonSchemaLocation.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSchemaLocation.Location = new System.Drawing.Point(742, 179);
            this.buttonSchemaLocation.Margin = new System.Windows.Forms.Padding(4);
            this.buttonSchemaLocation.Name = "buttonSchemaLocation";
            this.buttonSchemaLocation.Size = new System.Drawing.Size(46, 33);
            this.buttonSchemaLocation.TabIndex = 16;
            this.buttonSchemaLocation.Text = "...";
            this.buttonSchemaLocation.UseVisualStyleBackColor = true;
            this.buttonSchemaLocation.Click += new System.EventHandler(this.ButtonSchemaLocationClick);
            // 
            // textBoxSchemaLocation
            // 
            this.textBoxSchemaLocation.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSchemaLocation.Location = new System.Drawing.Point(39, 179);
            this.textBoxSchemaLocation.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxSchemaLocation.Name = "textBoxSchemaLocation";
            this.textBoxSchemaLocation.Size = new System.Drawing.Size(695, 32);
            this.textBoxSchemaLocation.TabIndex = 15;
            // 
            // numericUpDownBatchSize
            // 
            this.numericUpDownBatchSize.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownBatchSize.Location = new System.Drawing.Point(263, 316);
            this.numericUpDownBatchSize.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDownBatchSize.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDownBatchSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownBatchSize.Name = "numericUpDownBatchSize";
            this.numericUpDownBatchSize.Size = new System.Drawing.Size(116, 32);
            this.numericUpDownBatchSize.TabIndex = 13;
            this.numericUpDownBatchSize.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(37, 318);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 25);
            this.label5.TabIndex = 12;
            this.label5.Text = "Batch Size";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(31, 24);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(175, 32);
            this.label4.TabIndex = 10;
            this.label4.Text = "Export Settings";
            // 
            // openFileDialogExportConfigFile
            // 
            this.openFileDialogExportConfigFile.FileName = "openFileDialog1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.wizardButtons1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.stepWizardControl1);
            this.splitContainer1.Size = new System.Drawing.Size(800, 552);
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 1;
            // 
            // wizardButtons1
            // 
            this.wizardButtons1.Dock = System.Windows.Forms.DockStyle.Right;
            this.wizardButtons1.Location = new System.Drawing.Point(300, 0);
            this.wizardButtons1.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.wizardButtons1.Name = "wizardButtons1";
            this.wizardButtons1.PageContainer = this.stepWizardControl1;
            this.wizardButtons1.ShowExecuteButton = false;
            this.wizardButtons1.Size = new System.Drawing.Size(500, 50);
            this.wizardButtons1.TabIndex = 1;
            // 
            // ExportWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(800, 552);
            this.Name = "ExportWizard";
            this.Size = new System.Drawing.Size(800, 552);
            ((System.ComponentModel.ISupportInitialize)(this.stepWizardControl1)).EndInit();
            this.stepWizardControl1.ResumeLayout(false);
            this.wizardPage5.ResumeLayout(false);
            this.wizardPage5.PerformLayout();
            this.format.ResumeLayout(false);
            this.format.PerformLayout();
            this.exportConfig.ResumeLayout(false);
            this.exportConfig.PerformLayout();
            this.exportLocation.ResumeLayout(false);
            this.exportLocation.PerformLayout();
            this.executeExport.ResumeLayout(false);
            this.executeExport.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBatchSize)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AeroWizard.WizardPageContainer stepWizardControl1;
        private AeroWizard.WizardPage format;
        private AeroWizard.WizardPage exportLocation;
        private AeroWizard.WizardPage exportConfig;
        private AeroWizard.WizardPage executeExport;
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
        private ToggleCheckBox checkBoxMinimize;
        private System.Windows.Forms.Label label9;
        private ToggleCheckBox checkBoxExportInactiveRecords;
        private AeroWizard.WizardPage wizardPage5;
        private System.Windows.Forms.Label label10;
        private WizardButtons wizardButtons1;
        private System.Windows.Forms.Label labelFolderPathValidation;
        private System.Windows.Forms.Label labelSchemaLocationFileValidation;
        private System.Windows.Forms.TextBox textBoxLogs;
        private System.Windows.Forms.Label labelExportConnectionValidation;
        private System.Windows.Forms.Label labelLogLevel;
        private System.Windows.Forms.ComboBox comboBoxLogLevel;
    }
}
