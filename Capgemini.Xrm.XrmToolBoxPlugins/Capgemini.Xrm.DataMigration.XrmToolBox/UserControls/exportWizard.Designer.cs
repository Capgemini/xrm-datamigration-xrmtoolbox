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
            this.wizardPage5 = new AeroWizard.WizardPage();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxLogs = new System.Windows.Forms.TextBox();
            this.wizardPage4 = new AeroWizard.WizardPage();
            this.labelSchemaLocationFile = new System.Windows.Forms.Label();
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
            this.wizardPage2 = new AeroWizard.WizardPage();
            this.labelFolderPathValidation = new System.Windows.Forms.Label();
            this.buttonExportLocation = new System.Windows.Forms.Button();
            this.textBoxExportLocation = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.wizardPage3 = new AeroWizard.WizardPage();
            this.buttonExportConfigLocation = new System.Windows.Forms.Button();
            this.textBoxExportConfigLocation = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.wizardPage1 = new AeroWizard.WizardPage();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButtonFormatJson = new System.Windows.Forms.RadioButton();
            this.radioButtonFormatCsv = new System.Windows.Forms.RadioButton();
            this.folderBrowserDialogExportLocation = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialogExportConfigFile = new System.Windows.Forms.OpenFileDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.wizardButtons1 = new Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.UserControls.WizardButtons();
            ((System.ComponentModel.ISupportInitialize)(this.stepWizardControl1)).BeginInit();
            this.stepWizardControl1.SuspendLayout();
            this.wizardPage5.SuspendLayout();
            this.wizardPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBatchSize)).BeginInit();
            this.wizardPage2.SuspendLayout();
            this.wizardPage3.SuspendLayout();
            this.wizardPage1.SuspendLayout();
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
            this.stepWizardControl1.Controls.Add(this.wizardPage1);
            this.stepWizardControl1.Controls.Add(this.wizardPage5);
            this.stepWizardControl1.Controls.Add(this.wizardPage4);
            this.stepWizardControl1.Controls.Add(this.wizardPage2);
            this.stepWizardControl1.Controls.Add(this.wizardPage3);
            this.stepWizardControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stepWizardControl1.Location = new System.Drawing.Point(0, 0);
            this.stepWizardControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.stepWizardControl1.Name = "stepWizardControl1";
            this.stepWizardControl1.NextButton = null;
            this.stepWizardControl1.Pages.Add(this.wizardPage1);
            this.stepWizardControl1.Pages.Add(this.wizardPage2);
            this.stepWizardControl1.Pages.Add(this.wizardPage3);
            this.stepWizardControl1.Pages.Add(this.wizardPage4);
            this.stepWizardControl1.Pages.Add(this.wizardPage5);
            this.stepWizardControl1.Size = new System.Drawing.Size(1936, 825);
            this.stepWizardControl1.TabIndex = 0;
            // 
            // wizardPage5
            // 
            this.wizardPage5.Controls.Add(this.label10);
            this.wizardPage5.Controls.Add(this.textBoxLogs);
            this.wizardPage5.IsFinishPage = true;
            this.wizardPage5.Name = "wizardPage5";
            this.wizardPage5.ShowCancel = false;
            this.wizardPage5.Size = new System.Drawing.Size(1936, 825);
            this.wizardPage5.TabIndex = 6;
            this.wizardPage5.Text = "Page Title";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(60, 28);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(62, 31);
            this.label10.TabIndex = 22;
            this.label10.Text = "Logs";
            // 
            // textBoxLogs
            // 
            this.textBoxLogs.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxLogs.Location = new System.Drawing.Point(66, 62);
            this.textBoxLogs.Multiline = true;
            this.textBoxLogs.Name = "textBoxLogs";
            this.textBoxLogs.ReadOnly = true;
            this.textBoxLogs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxLogs.Size = new System.Drawing.Size(1041, 489);
            this.textBoxLogs.TabIndex = 23;
            // 
            // wizardPage4
            // 
            this.wizardPage4.Controls.Add(this.labelSchemaLocationFile);
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
            this.wizardPage4.Size = new System.Drawing.Size(1936, 825);
            this.wizardPage4.TabIndex = 5;
            this.wizardPage4.Text = "Perform Export";
            this.wizardPage4.Initialize += new System.EventHandler<AeroWizard.WizardPageInitEventArgs>(this.wizardPage4_Initialize);
            // 
            // labelSchemaLocationFile
            // 
            this.labelSchemaLocationFile.AutoSize = true;
            this.labelSchemaLocationFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSchemaLocationFile.ForeColor = System.Drawing.Color.Red;
            this.labelSchemaLocationFile.Location = new System.Drawing.Point(86, 220);
            this.labelSchemaLocationFile.Name = "labelSchemaLocationFile";
            this.labelSchemaLocationFile.Size = new System.Drawing.Size(303, 17);
            this.labelSchemaLocationFile.TabIndex = 62;
            this.labelSchemaLocationFile.Text = "Please Provide the location of your schema file";
            this.labelSchemaLocationFile.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(82, 358);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(182, 31);
            this.label6.TabIndex = 61;
            this.label6.Text = "Minimise (JSON)";
            // 
            // checkBoxMinimize
            // 
            this.checkBoxMinimize.Location = new System.Drawing.Point(338, 356);
            this.checkBoxMinimize.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxMinimize.Name = "checkBoxMinimize";
            this.checkBoxMinimize.Padding = new System.Windows.Forms.Padding(6);
            this.checkBoxMinimize.Size = new System.Drawing.Size(74, 38);
            this.checkBoxMinimize.TabIndex = 60;
            this.checkBoxMinimize.Text = "toggleCheckBox2";
            this.checkBoxMinimize.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(82, 300);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(252, 31);
            this.label9.TabIndex = 59;
            this.label9.Text = "Export Inactive Records";
            // 
            // checkBoxExportInactiveRecords
            // 
            this.checkBoxExportInactiveRecords.Location = new System.Drawing.Point(338, 300);
            this.checkBoxExportInactiveRecords.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxExportInactiveRecords.Name = "checkBoxExportInactiveRecords";
            this.checkBoxExportInactiveRecords.Padding = new System.Windows.Forms.Padding(6);
            this.checkBoxExportInactiveRecords.Size = new System.Drawing.Size(74, 38);
            this.checkBoxExportInactiveRecords.TabIndex = 58;
            this.checkBoxExportInactiveRecords.Text = "toggleCheckBox2";
            this.checkBoxExportInactiveRecords.UseVisualStyleBackColor = true;
            // 
            // labelConnectionString
            // 
            this.labelConnectionString.AutoSize = true;
            this.labelConnectionString.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.labelConnectionString.Location = new System.Drawing.Point(78, 135);
            this.labelConnectionString.Name = "labelConnectionString";
            this.labelConnectionString.Size = new System.Drawing.Size(265, 31);
            this.labelConnectionString.TabIndex = 45;
            this.labelConnectionString.Text = "Target Connection String";
            // 
            // buttonTargetConnectionString
            // 
            this.buttonTargetConnectionString.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonTargetConnectionString.Location = new System.Drawing.Point(746, 135);
            this.buttonTargetConnectionString.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonTargetConnectionString.Name = "buttonTargetConnectionString";
            this.buttonTargetConnectionString.Size = new System.Drawing.Size(51, 52);
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
            this.labelTargetConnectionString.Location = new System.Drawing.Point(381, 135);
            this.labelTargetConnectionString.MinimumSize = new System.Drawing.Size(192, 0);
            this.labelTargetConnectionString.Name = "labelTargetConnectionString";
            this.labelTargetConnectionString.Size = new System.Drawing.Size(192, 31);
            this.labelTargetConnectionString.TabIndex = 44;
            // 
            // labelSchemaFile
            // 
            this.labelSchemaFile.AutoSize = true;
            this.labelSchemaFile.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSchemaFile.Location = new System.Drawing.Point(78, 189);
            this.labelSchemaFile.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSchemaFile.Name = "labelSchemaFile";
            this.labelSchemaFile.Size = new System.Drawing.Size(400, 31);
            this.labelSchemaFile.TabIndex = 17;
            this.labelSchemaFile.Text = "Select the location of your schema file";
            // 
            // buttonSchemaLocation
            // 
            this.buttonSchemaLocation.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSchemaLocation.Location = new System.Drawing.Point(746, 228);
            this.buttonSchemaLocation.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonSchemaLocation.Name = "buttonSchemaLocation";
            this.buttonSchemaLocation.Size = new System.Drawing.Size(51, 52);
            this.buttonSchemaLocation.TabIndex = 16;
            this.buttonSchemaLocation.Text = "...";
            this.buttonSchemaLocation.UseVisualStyleBackColor = true;
            this.buttonSchemaLocation.Click += new System.EventHandler(this.buttonSchemaLocation_Click);
            // 
            // textBoxSchemaLocation
            // 
            this.textBoxSchemaLocation.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSchemaLocation.Location = new System.Drawing.Point(86, 242);
            this.textBoxSchemaLocation.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxSchemaLocation.Name = "textBoxSchemaLocation";
            this.textBoxSchemaLocation.Size = new System.Drawing.Size(596, 37);
            this.textBoxSchemaLocation.TabIndex = 15;
            this.textBoxSchemaLocation.TextChanged += new System.EventHandler(this.textBoxSchemaLocation_TextChanged);
            // 
            // numericUpDownBatchSize
            // 
            this.numericUpDownBatchSize.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownBatchSize.Location = new System.Drawing.Point(338, 412);
            this.numericUpDownBatchSize.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
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
            this.numericUpDownBatchSize.Size = new System.Drawing.Size(130, 37);
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
            this.label5.Location = new System.Drawing.Point(82, 415);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(117, 31);
            this.label5.TabIndex = 12;
            this.label5.Text = "Batch Size";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(78, 74);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(209, 40);
            this.label4.TabIndex = 10;
            this.label4.Text = "Export Settings";
            // 
            // wizardPage2
            // 
            this.wizardPage2.Controls.Add(this.labelFolderPathValidation);
            this.wizardPage2.Controls.Add(this.buttonExportLocation);
            this.wizardPage2.Controls.Add(this.textBoxExportLocation);
            this.wizardPage2.Controls.Add(this.label2);
            this.wizardPage2.Name = "wizardPage2";
            this.wizardPage2.NextPage = this.wizardPage3;
            this.wizardPage2.ShowCancel = false;
            this.wizardPage2.Size = new System.Drawing.Size(1936, 825);
            this.wizardPage2.TabIndex = 3;
            this.wizardPage2.Text = "Select Export Location";
            // 
            // labelFolderPathValidation
            // 
            this.labelFolderPathValidation.AutoSize = true;
            this.labelFolderPathValidation.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFolderPathValidation.ForeColor = System.Drawing.Color.Red;
            this.labelFolderPathValidation.Location = new System.Drawing.Point(104, 118);
            this.labelFolderPathValidation.Name = "labelFolderPathValidation";
            this.labelFolderPathValidation.Size = new System.Drawing.Size(199, 17);
            this.labelFolderPathValidation.TabIndex = 9;
            this.labelFolderPathValidation.Text = "Please Provide the folder path";
            this.labelFolderPathValidation.Visible = false;
            // 
            // buttonExportLocation
            // 
            this.buttonExportLocation.Location = new System.Drawing.Point(676, 138);
            this.buttonExportLocation.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonExportLocation.Name = "buttonExportLocation";
            this.buttonExportLocation.Size = new System.Drawing.Size(51, 52);
            this.buttonExportLocation.TabIndex = 8;
            this.buttonExportLocation.Text = "...";
            this.buttonExportLocation.UseVisualStyleBackColor = true;
            this.buttonExportLocation.Click += new System.EventHandler(this.buttonExportLocation_Click);
            // 
            // textBoxExportLocation
            // 
            this.textBoxExportLocation.Location = new System.Drawing.Point(104, 149);
            this.textBoxExportLocation.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxExportLocation.Name = "textBoxExportLocation";
            this.textBoxExportLocation.Size = new System.Drawing.Size(562, 26);
            this.textBoxExportLocation.TabIndex = 7;
            this.textBoxExportLocation.TextChanged += new System.EventHandler(this.textBoxExportLocation_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(78, 74);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(615, 40);
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
            this.wizardPage3.Size = new System.Drawing.Size(1936, 1028);
            this.wizardPage3.TabIndex = 4;
            this.wizardPage3.Text = "Export Config";
            // 
            // buttonExportConfigLocation
            // 
            this.buttonExportConfigLocation.Location = new System.Drawing.Point(686, 134);
            this.buttonExportConfigLocation.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonExportConfigLocation.Name = "buttonExportConfigLocation";
            this.buttonExportConfigLocation.Size = new System.Drawing.Size(51, 52);
            this.buttonExportConfigLocation.TabIndex = 11;
            this.buttonExportConfigLocation.Text = "...";
            this.buttonExportConfigLocation.UseVisualStyleBackColor = true;
            this.buttonExportConfigLocation.Click += new System.EventHandler(this.buttonExportConfigLocation_Click);
            // 
            // textBoxExportConfigLocation
            // 
            this.textBoxExportConfigLocation.Location = new System.Drawing.Point(78, 145);
            this.textBoxExportConfigLocation.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxExportConfigLocation.Name = "textBoxExportConfigLocation";
            this.textBoxExportConfigLocation.Size = new System.Drawing.Size(596, 26);
            this.textBoxExportConfigLocation.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(78, 74);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(573, 40);
            this.label3.TabIndex = 9;
            this.label3.Text = "Select the location of your export config file";
            // 
            // wizardPage1
            // 
            this.wizardPage1.Controls.Add(this.label1);
            this.wizardPage1.Controls.Add(this.radioButtonFormatJson);
            this.wizardPage1.Controls.Add(this.radioButtonFormatCsv);
            this.wizardPage1.Name = "wizardPage1";
            this.wizardPage1.NextPage = this.wizardPage2;
            this.wizardPage1.ShowCancel = false;
            this.wizardPage1.Size = new System.Drawing.Size(1936, 825);
            this.wizardPage1.TabIndex = 2;
            this.wizardPage1.Text = "Select Data Format";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(78, 74);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(669, 40);
            this.label1.TabIndex = 5;
            this.label1.Text = "Which format would you like to export the data to?";
            // 
            // radioButtonFormatJson
            // 
            this.radioButtonFormatJson.AutoSize = true;
            this.radioButtonFormatJson.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonFormatJson.Location = new System.Drawing.Point(78, 149);
            this.radioButtonFormatJson.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButtonFormatJson.Name = "radioButtonFormatJson";
            this.radioButtonFormatJson.Size = new System.Drawing.Size(93, 35);
            this.radioButtonFormatJson.TabIndex = 4;
            this.radioButtonFormatJson.TabStop = true;
            this.radioButtonFormatJson.Text = "JSON";
            this.radioButtonFormatJson.UseVisualStyleBackColor = true;
            // 
            // radioButtonFormatCsv
            // 
            this.radioButtonFormatCsv.AutoSize = true;
            this.radioButtonFormatCsv.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonFormatCsv.Location = new System.Drawing.Point(78, 194);
            this.radioButtonFormatCsv.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButtonFormatCsv.Name = "radioButtonFormatCsv";
            this.radioButtonFormatCsv.Size = new System.Drawing.Size(79, 35);
            this.radioButtonFormatCsv.TabIndex = 3;
            this.radioButtonFormatCsv.TabStop = true;
            this.radioButtonFormatCsv.Text = "CSV";
            this.radioButtonFormatCsv.UseVisualStyleBackColor = true;
            // 
            // openFileDialogExportConfigFile
            // 
            this.openFileDialogExportConfigFile.FileName = "openFileDialog1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
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
            this.splitContainer1.Size = new System.Drawing.Size(1936, 1145);
            this.splitContainer1.SplitterDistance = 825;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 1;
            // 
            // wizardButtons1
            // 
            this.wizardButtons1.Container = this.stepWizardControl1;
            this.wizardButtons1.Dock = System.Windows.Forms.DockStyle.Right;
            this.wizardButtons1.Location = new System.Drawing.Point(1426, 0);
            this.wizardButtons1.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.wizardButtons1.Name = "wizardButtons1";
            this.wizardButtons1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.wizardButtons1.ShowExecuteButton = false;
            this.wizardButtons1.Size = new System.Drawing.Size(510, 314);
            this.wizardButtons1.TabIndex = 1;
            // 
            // exportWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "exportWizard";
            this.Size = new System.Drawing.Size(1936, 1145);
            ((System.ComponentModel.ISupportInitialize)(this.stepWizardControl1)).EndInit();
            this.stepWizardControl1.ResumeLayout(false);
            this.wizardPage5.ResumeLayout(false);
            this.wizardPage5.PerformLayout();
            this.wizardPage4.ResumeLayout(false);
            this.wizardPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBatchSize)).EndInit();
            this.wizardPage2.ResumeLayout(false);
            this.wizardPage2.PerformLayout();
            this.wizardPage3.ResumeLayout(false);
            this.wizardPage3.PerformLayout();
            this.wizardPage1.ResumeLayout(false);
            this.wizardPage1.PerformLayout();
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
        private System.Windows.Forms.Label label10;
        private WizardButtons wizardButtons1;
        private System.Windows.Forms.Label labelFolderPathValidation;
        private System.Windows.Forms.Label labelSchemaLocationFile;
        private System.Windows.Forms.TextBox textBoxLogs;
    }
}
