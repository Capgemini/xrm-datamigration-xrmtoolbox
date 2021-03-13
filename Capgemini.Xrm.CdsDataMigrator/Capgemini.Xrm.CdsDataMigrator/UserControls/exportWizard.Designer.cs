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
            this.format.SuspendLayout();
            this.exportConfig.SuspendLayout();
            this.exportLocation.SuspendLayout();
            this.executeExport.SuspendLayout();
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
            this.stepWizardControl1.Controls.Add(this.executeExport);
            this.stepWizardControl1.Controls.Add(this.exportConfig);
            this.stepWizardControl1.Controls.Add(this.exportLocation);
            this.stepWizardControl1.Controls.Add(this.format);
            this.stepWizardControl1.Controls.Add(this.wizardPage5);
            this.stepWizardControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stepWizardControl1.Location = new System.Drawing.Point(0, 0);
            this.stepWizardControl1.Margin = new System.Windows.Forms.Padding(7);
            this.stepWizardControl1.Name = "stepWizardControl1";
            this.stepWizardControl1.NextButton = null;
            this.stepWizardControl1.Pages.Add(this.format);
            this.stepWizardControl1.Pages.Add(this.exportConfig);
            this.stepWizardControl1.Pages.Add(this.exportLocation);
            this.stepWizardControl1.Pages.Add(this.executeExport);
            this.stepWizardControl1.Pages.Add(this.wizardPage5);
            this.stepWizardControl1.Size = new System.Drawing.Size(1400, 720);
            this.stepWizardControl1.TabIndex = 0;
            // 
            // format
            // 
            this.format.Controls.Add(this.label1);
            this.format.Controls.Add(this.radioButtonFormatJson);
            this.format.Controls.Add(this.radioButtonFormatCsv);
            this.format.Name = "format";
            this.format.NextPage = this.exportConfig;
            this.format.ShowCancel = false;
            this.format.Size = new System.Drawing.Size(1400, 720);
            this.format.TabIndex = 2;
            this.format.Text = "Select Data Format";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(63, 41);
            this.label1.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(992, 59);
            this.label1.TabIndex = 5;
            this.label1.Text = "Which format would you like to export the data to?";
            // 
            // radioButtonFormatJson
            // 
            this.radioButtonFormatJson.AutoSize = true;
            this.radioButtonFormatJson.Checked = true;
            this.radioButtonFormatJson.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonFormatJson.Location = new System.Drawing.Point(73, 138);
            this.radioButtonFormatJson.Margin = new System.Windows.Forms.Padding(7);
            this.radioButtonFormatJson.Name = "radioButtonFormatJson";
            this.radioButtonFormatJson.Size = new System.Drawing.Size(132, 50);
            this.radioButtonFormatJson.TabIndex = 4;
            this.radioButtonFormatJson.TabStop = true;
            this.radioButtonFormatJson.Text = "JSON";
            this.radioButtonFormatJson.UseVisualStyleBackColor = true;
            // 
            // radioButtonFormatCsv
            // 
            this.radioButtonFormatCsv.AutoSize = true;
            this.radioButtonFormatCsv.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonFormatCsv.Location = new System.Drawing.Point(73, 202);
            this.radioButtonFormatCsv.Margin = new System.Windows.Forms.Padding(7);
            this.radioButtonFormatCsv.Name = "radioButtonFormatCsv";
            this.radioButtonFormatCsv.Size = new System.Drawing.Size(111, 50);
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
            this.exportConfig.Size = new System.Drawing.Size(1400, 720);
            this.exportConfig.TabIndex = 4;
            this.exportConfig.Text = "Export Config";
            // 
            // buttonExportConfigLocation
            // 
            this.buttonExportConfigLocation.Location = new System.Drawing.Point(1005, 152);
            this.buttonExportConfigLocation.Margin = new System.Windows.Forms.Padding(7);
            this.buttonExportConfigLocation.Name = "buttonExportConfigLocation";
            this.buttonExportConfigLocation.Size = new System.Drawing.Size(80, 60);
            this.buttonExportConfigLocation.TabIndex = 11;
            this.buttonExportConfigLocation.Text = "...";
            this.buttonExportConfigLocation.UseVisualStyleBackColor = true;
            this.buttonExportConfigLocation.Click += new System.EventHandler(this.buttonExportConfigLocation_Click);
            // 
            // textBoxExportConfigLocation
            // 
            this.textBoxExportConfigLocation.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.textBoxExportConfigLocation.Location = new System.Drawing.Point(51, 152);
            this.textBoxExportConfigLocation.Margin = new System.Windows.Forms.Padding(7);
            this.textBoxExportConfigLocation.Name = "textBoxExportConfigLocation";
            this.textBoxExportConfigLocation.Size = new System.Drawing.Size(940, 52);
            this.textBoxExportConfigLocation.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(41, 49);
            this.label3.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1051, 59);
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
            this.exportLocation.Size = new System.Drawing.Size(1400, 720);
            this.exportLocation.TabIndex = 3;
            this.exportLocation.Text = "Select Export Location";
            // 
            // labelFolderPathValidation
            // 
            this.labelFolderPathValidation.AutoSize = true;
            this.labelFolderPathValidation.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFolderPathValidation.ForeColor = System.Drawing.Color.Red;
            this.labelFolderPathValidation.Location = new System.Drawing.Point(61, 136);
            this.labelFolderPathValidation.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.labelFolderPathValidation.Name = "labelFolderPathValidation";
            this.labelFolderPathValidation.Size = new System.Drawing.Size(301, 25);
            this.labelFolderPathValidation.TabIndex = 9;
            this.labelFolderPathValidation.Text = "Please Provide the folder path";
            this.labelFolderPathValidation.Visible = false;
            // 
            // buttonExportLocation
            // 
            this.buttonExportLocation.Location = new System.Drawing.Point(987, 168);
            this.buttonExportLocation.Margin = new System.Windows.Forms.Padding(7);
            this.buttonExportLocation.Name = "buttonExportLocation";
            this.buttonExportLocation.Size = new System.Drawing.Size(80, 60);
            this.buttonExportLocation.TabIndex = 8;
            this.buttonExportLocation.Text = "...";
            this.buttonExportLocation.UseVisualStyleBackColor = true;
            this.buttonExportLocation.Click += new System.EventHandler(this.buttonExportLocation_Click);
            // 
            // textBoxExportLocation
            // 
            this.textBoxExportLocation.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.textBoxExportLocation.Location = new System.Drawing.Point(66, 168);
            this.textBoxExportLocation.Margin = new System.Windows.Forms.Padding(7);
            this.textBoxExportLocation.Name = "textBoxExportLocation";
            this.textBoxExportLocation.Size = new System.Drawing.Size(907, 52);
            this.textBoxExportLocation.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(56, 53);
            this.label2.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(912, 59);
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
            this.executeExport.Size = new System.Drawing.Size(1400, 720);
            this.executeExport.TabIndex = 5;
            this.executeExport.Text = "Perform Export";
            // 
            // labelExportConnectionValidation
            // 
            this.labelExportConnectionValidation.AutoSize = true;
            this.labelExportConnectionValidation.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelExportConnectionValidation.ForeColor = System.Drawing.Color.Red;
            this.labelExportConnectionValidation.Location = new System.Drawing.Point(69, 146);
            this.labelExportConnectionValidation.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.labelExportConnectionValidation.Name = "labelExportConnectionValidation";
            this.labelExportConnectionValidation.Size = new System.Drawing.Size(461, 25);
            this.labelExportConnectionValidation.TabIndex = 63;
            this.labelExportConnectionValidation.Text = "Please Provide the location of your schema file";
            this.labelExportConnectionValidation.Visible = false;
            // 
            // labelSchemaLocationFileValidation
            // 
            this.labelSchemaLocationFileValidation.AutoSize = true;
            this.labelSchemaLocationFileValidation.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSchemaLocationFileValidation.ForeColor = System.Drawing.Color.Red;
            this.labelSchemaLocationFileValidation.Location = new System.Drawing.Point(69, 294);
            this.labelSchemaLocationFileValidation.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.labelSchemaLocationFileValidation.Name = "labelSchemaLocationFileValidation";
            this.labelSchemaLocationFileValidation.Size = new System.Drawing.Size(461, 25);
            this.labelSchemaLocationFileValidation.TabIndex = 62;
            this.labelSchemaLocationFileValidation.Text = "Please Provide the location of your schema file";
            this.labelSchemaLocationFileValidation.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(64, 495);
            this.label6.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(265, 46);
            this.label6.TabIndex = 61;
            this.label6.Text = "Minimise (JSON)";
            // 
            // checkBoxMinimize
            // 
            this.checkBoxMinimize.Location = new System.Drawing.Point(461, 490);
            this.checkBoxMinimize.Margin = new System.Windows.Forms.Padding(7);
            this.checkBoxMinimize.Name = "checkBoxMinimize";
            this.checkBoxMinimize.Padding = new System.Windows.Forms.Padding(9);
            this.checkBoxMinimize.Size = new System.Drawing.Size(114, 56);
            this.checkBoxMinimize.TabIndex = 60;
            this.checkBoxMinimize.Text = "toggleCheckBox2";
            this.checkBoxMinimize.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(64, 410);
            this.label9.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(369, 46);
            this.label9.TabIndex = 59;
            this.label9.Text = "Export Inactive Records";
            // 
            // checkBoxExportInactiveRecords
            // 
            this.checkBoxExportInactiveRecords.Location = new System.Drawing.Point(461, 410);
            this.checkBoxExportInactiveRecords.Margin = new System.Windows.Forms.Padding(7);
            this.checkBoxExportInactiveRecords.Name = "checkBoxExportInactiveRecords";
            this.checkBoxExportInactiveRecords.Padding = new System.Windows.Forms.Padding(9);
            this.checkBoxExportInactiveRecords.Size = new System.Drawing.Size(114, 56);
            this.checkBoxExportInactiveRecords.TabIndex = 58;
            this.checkBoxExportInactiveRecords.Text = "toggleCheckBox2";
            this.checkBoxExportInactiveRecords.UseVisualStyleBackColor = true;
            // 
            // labelConnectionString
            // 
            this.labelConnectionString.AutoSize = true;
            this.labelConnectionString.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.labelConnectionString.Location = new System.Drawing.Point(57, 171);
            this.labelConnectionString.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.labelConnectionString.Name = "labelConnectionString";
            this.labelConnectionString.Size = new System.Drawing.Size(391, 46);
            this.labelConnectionString.TabIndex = 45;
            this.labelConnectionString.Text = "Target Connection String";
            // 
            // buttonTargetConnectionString
            // 
            this.buttonTargetConnectionString.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonTargetConnectionString.Location = new System.Drawing.Point(1096, 171);
            this.buttonTargetConnectionString.Margin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            this.buttonTargetConnectionString.Name = "buttonTargetConnectionString";
            this.buttonTargetConnectionString.Size = new System.Drawing.Size(80, 60);
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
            this.labelTargetConnectionString.Location = new System.Drawing.Point(458, 171);
            this.labelTargetConnectionString.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.labelTargetConnectionString.MinimumSize = new System.Drawing.Size(299, 0);
            this.labelTargetConnectionString.Name = "labelTargetConnectionString";
            this.labelTargetConnectionString.Size = new System.Drawing.Size(299, 46);
            this.labelTargetConnectionString.TabIndex = 44;
            // 
            // labelSchemaFile
            // 
            this.labelSchemaFile.AutoSize = true;
            this.labelSchemaFile.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSchemaFile.Location = new System.Drawing.Point(57, 249);
            this.labelSchemaFile.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.labelSchemaFile.Name = "labelSchemaFile";
            this.labelSchemaFile.Size = new System.Drawing.Size(589, 46);
            this.labelSchemaFile.TabIndex = 17;
            this.labelSchemaFile.Text = "Select the location of your schema file";
            // 
            // buttonSchemaLocation
            // 
            this.buttonSchemaLocation.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSchemaLocation.Location = new System.Drawing.Point(1096, 325);
            this.buttonSchemaLocation.Margin = new System.Windows.Forms.Padding(7);
            this.buttonSchemaLocation.Name = "buttonSchemaLocation";
            this.buttonSchemaLocation.Size = new System.Drawing.Size(80, 60);
            this.buttonSchemaLocation.TabIndex = 16;
            this.buttonSchemaLocation.Text = "...";
            this.buttonSchemaLocation.UseVisualStyleBackColor = true;
            this.buttonSchemaLocation.Click += new System.EventHandler(this.buttonSchemaLocation_Click);
            // 
            // textBoxSchemaLocation
            // 
            this.textBoxSchemaLocation.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSchemaLocation.Location = new System.Drawing.Point(69, 325);
            this.textBoxSchemaLocation.Margin = new System.Windows.Forms.Padding(7);
            this.textBoxSchemaLocation.Name = "textBoxSchemaLocation";
            this.textBoxSchemaLocation.Size = new System.Drawing.Size(1013, 52);
            this.textBoxSchemaLocation.TabIndex = 15;
            // 
            // numericUpDownBatchSize
            // 
            this.numericUpDownBatchSize.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownBatchSize.Location = new System.Drawing.Point(461, 573);
            this.numericUpDownBatchSize.Margin = new System.Windows.Forms.Padding(7);
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
            this.numericUpDownBatchSize.Size = new System.Drawing.Size(203, 52);
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
            this.label5.Location = new System.Drawing.Point(64, 577);
            this.label5.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(171, 46);
            this.label5.TabIndex = 12;
            this.label5.Text = "Batch Size";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(55, 44);
            this.label4.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(311, 59);
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
            this.wizardPage5.ShowNext = false;
            this.wizardPage5.Size = new System.Drawing.Size(1400, 720);
            this.wizardPage5.TabIndex = 6;
            this.wizardPage5.Text = "Results";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(31, 9);
            this.label10.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(90, 46);
            this.label10.TabIndex = 22;
            this.label10.Text = "Logs";
            // 
            // textBoxLogs
            // 
            this.textBoxLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxLogs.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxLogs.Location = new System.Drawing.Point(39, 72);
            this.textBoxLogs.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.textBoxLogs.Multiline = true;
            this.textBoxLogs.Name = "textBoxLogs";
            this.textBoxLogs.ReadOnly = true;
            this.textBoxLogs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxLogs.Size = new System.Drawing.Size(1321, 623);
            this.textBoxLogs.TabIndex = 23;
            // 
            // openFileDialogExportConfigFile
            // 
            this.openFileDialogExportConfigFile.FileName = "openFileDialog1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(7);
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
            this.splitContainer1.Size = new System.Drawing.Size(1400, 1000);
            this.splitContainer1.SplitterDistance = 720;
            this.splitContainer1.SplitterWidth = 9;
            this.splitContainer1.TabIndex = 1;
            // 
            // wizardButtons1
            // 
            this.wizardButtons1.PageContainer = this.stepWizardControl1;
            this.wizardButtons1.Dock = System.Windows.Forms.DockStyle.Right;
            this.wizardButtons1.Location = new System.Drawing.Point(692, 0);
            this.wizardButtons1.Margin = new System.Windows.Forms.Padding(9, 11, 9, 11);
            this.wizardButtons1.Name = "wizardButtons1";
            this.wizardButtons1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.wizardButtons1.ShowExecuteButton = false;
            this.wizardButtons1.Size = new System.Drawing.Size(708, 271);
            this.wizardButtons1.TabIndex = 1;
            // 
            // exportWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(7);
            this.MinimumSize = new System.Drawing.Size(1400, 1000);
            this.Name = "exportWizard";
            this.Size = new System.Drawing.Size(1400, 1000);
            ((System.ComponentModel.ISupportInitialize)(this.stepWizardControl1)).EndInit();
            this.stepWizardControl1.ResumeLayout(false);
            this.format.ResumeLayout(false);
            this.format.PerformLayout();
            this.exportConfig.ResumeLayout(false);
            this.exportConfig.PerformLayout();
            this.exportLocation.ResumeLayout(false);
            this.exportLocation.PerformLayout();
            this.executeExport.ResumeLayout(false);
            this.executeExport.PerformLayout();
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
        private Xrm.DataMigration.XrmToolBoxPlugin.UserControls.ToggleCheckBox checkBoxMinimize;
        private System.Windows.Forms.Label label9;
        private Xrm.DataMigration.XrmToolBoxPlugin.UserControls.ToggleCheckBox checkBoxExportInactiveRecords;
        private AeroWizard.WizardPage wizardPage5;
        private System.Windows.Forms.Label label10;
        private WizardButtons wizardButtons1;
        private System.Windows.Forms.Label labelFolderPathValidation;
        private System.Windows.Forms.Label labelSchemaLocationFileValidation;
        private System.Windows.Forms.TextBox textBoxLogs;
        private System.Windows.Forms.Label labelExportConnectionValidation;
    }
}
