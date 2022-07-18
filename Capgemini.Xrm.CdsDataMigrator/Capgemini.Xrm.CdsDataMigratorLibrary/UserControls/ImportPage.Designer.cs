namespace Capgemini.Xrm.CdsDataMigratorLibrary.UserControls
{
    partial class ImportPage
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportPage));
            this.gbxFetchSettings = new System.Windows.Forms.GroupBox();
            this.tlpFetchSettings = new System.Windows.Forms.TableLayoutPanel();
            this.lblFileType = new System.Windows.Forms.Label();
            this.fplFileTypes = new System.Windows.Forms.FlowLayoutPanel();
            this.rbnDataFormatJson = new System.Windows.Forms.RadioButton();
            this.rbnDataFormatCsv = new System.Windows.Forms.RadioButton();
            this.tbImportSchema = new System.Windows.Forms.TextBox();
            this.btnImportSchema = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblEnvironment = new System.Windows.Forms.Label();
            this.lblSchemaFile = new System.Windows.Forms.Label();
            this.lblPageSize = new System.Windows.Forms.Label();
            this.lblTopCount = new System.Windows.Forms.Label();
            this.nbxPageSize = new System.Windows.Forms.NumericUpDown();
            this.nbxTopCount = new System.Windows.Forms.NumericUpDown();
            this.lblIgnoreRecordStatus = new System.Windows.Forms.Label();
            this.lblIgnoreSystemFields = new System.Windows.Forms.Label();
            this.gbxWriteSettings = new System.Windows.Forms.GroupBox();
            this.tlpWriteSettings = new System.Windows.Forms.TableLayoutPanel();
            this.dataverseEnvironmentSelector1 = new Capgemini.Xrm.CdsDataMigratorLibrary.UserControls.DataverseEnvironmentSelector();
            this.tcbIgnoreSystemFields = new Capgemini.Xrm.CdsDataMigratorLibrary.UserControls.ToggleCheckBox();
            this.tcbIgnoreRecordStatus = new Capgemini.Xrm.CdsDataMigratorLibrary.UserControls.ToggleCheckBox();
            this.lblOutputDirectory = new System.Windows.Forms.Label();
            this.tbxFileNamePrefix = new System.Windows.Forms.TextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbLoad = new System.Windows.Forms.ToolStripButton();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.tsbRun = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.tlpMainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.fisSchemaFile = new Capgemini.Xrm.CdsDataMigratorLibrary.UserControls.FileInputSelector();
            this.fisOutputDirectory = new Capgemini.Xrm.CdsDataMigratorLibrary.UserControls.FolderInputSelector();
            this.gbxFetchSettings.SuspendLayout();
            this.tlpFetchSettings.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nbxPageSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nbxTopCount)).BeginInit();
            this.gbxWriteSettings.SuspendLayout();
            this.tlpWriteSettings.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tlpMainLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxFetchSettings
            // 
            this.gbxFetchSettings.AutoSize = true;
            this.gbxFetchSettings.Controls.Add(this.tlpFetchSettings);
            this.gbxFetchSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxFetchSettings.Location = new System.Drawing.Point(11, 10);
            this.gbxFetchSettings.Margin = new System.Windows.Forms.Padding(4);
            this.gbxFetchSettings.Name = "gbxFetchSettings";
            this.gbxFetchSettings.Padding = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.gbxFetchSettings.Size = new System.Drawing.Size(1304, 306);
            this.gbxFetchSettings.TabIndex = 0;
            this.gbxFetchSettings.TabStop = false;
            this.gbxFetchSettings.Text = "Fetch settings (from file)";
            // 
            // tlpFetchSettings
            // 
            this.tlpFetchSettings.AutoScroll = true;
            this.tlpFetchSettings.AutoSize = true;
            this.tlpFetchSettings.ColumnCount = 2;
            this.tlpFetchSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.45274F));
            this.tlpFetchSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 83.54726F));

            this.tlpFetchSettings.Controls.Add(this.lblOutputDirectory, 0, 0);
            this.tlpFetchSettings.Controls.Add(this.fisOutputDirectory, 1, 0);
            this.tlpFetchSettings.Controls.Add(this.lblFileType, 0, 1);
            this.tlpFetchSettings.Controls.Add(this.fplFileTypes, 1, 1);
            this.tlpFetchSettings.Controls.Add(this.groupBox1, 1, 2);
            this.tlpFetchSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpFetchSettings.Location = new System.Drawing.Point(13, 27);
            this.tlpFetchSettings.Margin = new System.Windows.Forms.Padding(4);
            this.tlpFetchSettings.Name = "tlpFetchSettings";
            this.tlpFetchSettings.RowCount = 8;
            this.tlpFetchSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpFetchSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpFetchSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpFetchSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpFetchSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpFetchSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tlpFetchSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tlpFetchSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.tlpFetchSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpFetchSettings.Size = new System.Drawing.Size(1278, 267);
            this.tlpFetchSettings.TabIndex = 0;
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
            // 
            // 
            // lblFileType
            // 
            this.lblFileType.AutoSize = true;
            this.lblFileType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFileType.Location = new System.Drawing.Point(4, 0);
            this.lblFileType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFileType.Name = "lblFileType";
            this.lblFileType.Size = new System.Drawing.Size(253, 45);
            this.lblFileType.TabIndex = 0;
            this.lblFileType.Text = "File Type";
            this.toolTip.SetToolTip(this.lblFileType, "The type of file the data will be stored in.");
            // 
            // fplFileTypes
            // 
            this.fplFileTypes.AutoSize = true;
            this.fplFileTypes.Controls.Add(this.rbnDataFormatJson);
            this.fplFileTypes.Controls.Add(this.rbnDataFormatCsv);
            this.fplFileTypes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fplFileTypes.Location = new System.Drawing.Point(268, 5);
            this.fplFileTypes.Margin = new System.Windows.Forms.Padding(4);
            this.fplFileTypes.Name = "fplFileTypes";
            this.fplFileTypes.Size = new System.Drawing.Size(1325, 35);
            this.fplFileTypes.TabIndex = 3;
            // 
            // rbnDataFormatJson
            // 
            this.rbnDataFormatJson.AutoSize = true;
            this.rbnDataFormatJson.Location = new System.Drawing.Point(4, 4);
            this.rbnDataFormatJson.Margin = new System.Windows.Forms.Padding(4);
            this.rbnDataFormatJson.Name = "rbnDataFormatJson";
            this.rbnDataFormatJson.Size = new System.Drawing.Size(64, 20);
            this.rbnDataFormatJson.TabIndex = 0;
            this.rbnDataFormatJson.TabStop = true;
            this.rbnDataFormatJson.Text = "JSON";
            this.rbnDataFormatJson.UseVisualStyleBackColor = true;
            this.rbnDataFormatJson.CheckedChanged += new System.EventHandler(this.RadioButtonCheckedChanged);
            // 
            // rbnDataFormatCsv
            // 
            this.rbnDataFormatCsv.AutoSize = true;
            this.rbnDataFormatCsv.Location = new System.Drawing.Point(76, 4);
            this.rbnDataFormatCsv.Margin = new System.Windows.Forms.Padding(4);
            this.rbnDataFormatCsv.Name = "rbnDataFormatCsv";
            this.rbnDataFormatCsv.Size = new System.Drawing.Size(55, 20);
            this.rbnDataFormatCsv.TabIndex = 1;
            this.rbnDataFormatCsv.TabStop = true;
            this.rbnDataFormatCsv.Text = "CSV";
            this.rbnDataFormatCsv.UseVisualStyleBackColor = true;
            this.rbnDataFormatCsv.CheckedChanged += new System.EventHandler(this.RadioButton1CheckedChanged);
            // 
            // lblEnvironment
            // 
            this.lblEnvironment.AutoSize = true;
            this.lblEnvironment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEnvironment.Location = new System.Drawing.Point(4, 0);
            this.lblEnvironment.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEnvironment.Name = "lblEnvironment";
            this.lblEnvironment.Size = new System.Drawing.Size(199, 28);
            this.lblEnvironment.TabIndex = 7;
            this.lblEnvironment.Text = "Environment";
            this.toolTip.SetToolTip(this.lblEnvironment, "The Dataverse environment you want to export data from.");
            // 
            // lblPageSize
            // 
            this.lblPageSize.AutoSize = true;
            this.lblPageSize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPageSize.Location = new System.Drawing.Point(4, 28);
            this.lblPageSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPageSize.Name = "lblPageSize";
            this.lblPageSize.Size = new System.Drawing.Size(199, 30);
            this.lblPageSize.TabIndex = 1;
            this.lblPageSize.Text = "Save Page Size";
            this.toolTip.SetToolTip(this.lblPageSize, "The number of records to Import in each request.");
            // 
            // lblTopCount
            // 
            this.lblTopCount.AutoSize = true;
            this.lblTopCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTopCount.Location = new System.Drawing.Point(4, 58);
            this.lblTopCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTopCount.Name = "lblTopCount";
            this.lblTopCount.Size = new System.Drawing.Size(199, 28);
            this.lblTopCount.TabIndex = 2;
            this.lblTopCount.Text = "Max Threads";
            this.toolTip.SetToolTip(this.lblTopCount, "The maxium number of records to Import. ");
            // 
            // nbxPageSize
            // 
            this.nbxPageSize.Location = new System.Drawing.Point(211, 32);
            this.nbxPageSize.Margin = new System.Windows.Forms.Padding(4);
            this.nbxPageSize.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.nbxPageSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nbxPageSize.Name = "nbxPageSize";
            this.nbxPageSize.Size = new System.Drawing.Size(128, 22);
            this.nbxPageSize.TabIndex = 3;
            this.nbxPageSize.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            // 
            // nbxTopCount
            // 
            this.nbxTopCount.Location = new System.Drawing.Point(210, 61);
            this.nbxTopCount.Name = "nbxTopCount";
            this.nbxTopCount.Size = new System.Drawing.Size(129, 22);
            this.nbxTopCount.TabIndex = 9;
            this.nbxTopCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblIgnoreRecordStatus
            // 
            this.lblIgnoreRecordStatus.AutoSize = true;
            this.lblIgnoreRecordStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIgnoreRecordStatus.Location = new System.Drawing.Point(4, 124);
            this.lblIgnoreRecordStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblIgnoreRecordStatus.Name = "lblIgnoreRecordStatus";
            this.lblIgnoreRecordStatus.Size = new System.Drawing.Size(199, 38);
            this.lblIgnoreRecordStatus.TabIndex = 9;
            this.lblIgnoreRecordStatus.Text = "Ignore Record Status";
            // 
            // lblIgnoreSystemFields
            // 
            this.lblIgnoreSystemFields.AutoSize = true;
            this.lblIgnoreSystemFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblIgnoreSystemFields.Location = new System.Drawing.Point(4, 86);
            this.lblIgnoreSystemFields.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblIgnoreSystemFields.Name = "lblIgnoreSystemFields";
            this.lblIgnoreSystemFields.Size = new System.Drawing.Size(199, 38);
            this.lblIgnoreSystemFields.TabIndex = 9;
            this.lblIgnoreSystemFields.Text = "Ignore System Fields";
            this.toolTip.SetToolTip(this.lblIgnoreSystemFields, "Restricts the Import based on records status.");
            // 
            // gbxWriteSettings
            // 
            this.gbxWriteSettings.Controls.Add(this.tlpWriteSettings);
            this.gbxWriteSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxWriteSettings.Location = new System.Drawing.Point(11, 324);
            this.gbxWriteSettings.Margin = new System.Windows.Forms.Padding(4);
            this.gbxWriteSettings.Name = "gbxWriteSettings";
            this.gbxWriteSettings.Padding = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.gbxWriteSettings.Size = new System.Drawing.Size(1304, 307);
            this.gbxWriteSettings.TabIndex = 1;
            this.gbxWriteSettings.TabStop = false;
            this.gbxWriteSettings.Text = "Write settings (to Dataverse)";
            // 
            // tlpWriteSettings
            // 
            this.tlpWriteSettings.AutoScroll = true;
            this.tlpWriteSettings.AutoSize = true;
            this.tlpWriteSettings.ColumnCount = 2;
            this.tlpWriteSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.21937F));
            this.tlpWriteSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 83.78063F));
            this.tlpWriteSettings.Controls.Add(this.lblEnvironment, 0, 0);
            this.tlpWriteSettings.Controls.Add(this.dataverseEnvironmentSelector1, 1, 0);
            this.tlpWriteSettings.Controls.Add(this.lblPageSize, 0, 1);
            this.tlpWriteSettings.Controls.Add(this.nbxPageSize, 1, 1);
            this.tlpWriteSettings.Controls.Add(this.lblTopCount, 0, 2);
            this.tlpWriteSettings.Controls.Add(this.nbxTopCount, 1, 2);
            this.tlpWriteSettings.Controls.Add(this.lblIgnoreSystemFields, 0, 3);
            this.tlpWriteSettings.Controls.Add(this.tcbIgnoreSystemFields, 1, 3);
            this.tlpWriteSettings.Controls.Add(this.lblIgnoreRecordStatus, 0, 4);
            this.tlpWriteSettings.Controls.Add(this.tcbIgnoreRecordStatus, 1, 4);
            this.tlpWriteSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpWriteSettings.Location = new System.Drawing.Point(13, 27);
            this.tlpWriteSettings.Margin = new System.Windows.Forms.Padding(4);
            this.tlpWriteSettings.Name = "tlpWriteSettings";
            this.tlpWriteSettings.RowCount = 7;
            this.tlpWriteSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpWriteSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpWriteSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpWriteSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpWriteSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpWriteSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpWriteSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpWriteSettings.Size = new System.Drawing.Size(1278, 268);
            this.tlpWriteSettings.TabIndex = 0;
            // 
            // dataverseEnvironmentSelector1
            // 
            this.dataverseEnvironmentSelector1.ConnectionUpdatedScope = Capgemini.Xrm.CdsDataMigratorLibrary.UserControls.DataverseEnvironmentSelector.Scope.Local;
            this.dataverseEnvironmentSelector1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataverseEnvironmentSelector1.Location = new System.Drawing.Point(212, 5);
            this.dataverseEnvironmentSelector1.Margin = new System.Windows.Forms.Padding(5);
            this.dataverseEnvironmentSelector1.Name = "dataverseEnvironmentSelector1";
            this.dataverseEnvironmentSelector1.Size = new System.Drawing.Size(1061, 18);
            this.dataverseEnvironmentSelector1.TabIndex = 8;
            // 
            // tcbIgnoreSystemFields
            // 
            this.tcbIgnoreSystemFields.AutoSize = true;
            this.tcbIgnoreSystemFields.Location = new System.Drawing.Point(211, 90);
            this.tcbIgnoreSystemFields.Margin = new System.Windows.Forms.Padding(4);
            this.tcbIgnoreSystemFields.Name = "tcbIgnoreSystemFields";
            this.tcbIgnoreSystemFields.Padding = new System.Windows.Forms.Padding(5);
            this.tcbIgnoreSystemFields.Size = new System.Drawing.Size(63, 30);
            this.tcbIgnoreSystemFields.TabIndex = 10;
            this.tcbIgnoreSystemFields.Text = "Yes";
            this.tcbIgnoreSystemFields.UseVisualStyleBackColor = true;
            // 
            // tcbIgnoreRecordStatus
            // 
            this.tcbIgnoreRecordStatus.AutoSize = true;
            this.tcbIgnoreRecordStatus.Location = new System.Drawing.Point(211, 128);
            this.tcbIgnoreRecordStatus.Margin = new System.Windows.Forms.Padding(4);
            this.tcbIgnoreRecordStatus.Name = "tcbIgnoreRecordStatus";
            this.tcbIgnoreRecordStatus.Padding = new System.Windows.Forms.Padding(5);
            this.tcbIgnoreRecordStatus.Size = new System.Drawing.Size(63, 30);
            this.tcbIgnoreRecordStatus.TabIndex = 10;
            this.tcbIgnoreRecordStatus.Text = "Yes";
            this.tcbIgnoreRecordStatus.UseVisualStyleBackColor = true;
            // 
            // lblOutputDirectory
            // 
            this.lblOutputDirectory.AutoSize = true;
            this.lblOutputDirectory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOutputDirectory.Location = new System.Drawing.Point(4, 36);
            this.lblOutputDirectory.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOutputDirectory.Name = "lblOutputDirectory";
            this.lblOutputDirectory.Size = new System.Drawing.Size(199, 41);
            this.lblOutputDirectory.TabIndex = 1;
            this.lblOutputDirectory.Text = "Source data Directory";
            this.toolTip.SetToolTip(this.lblOutputDirectory, "The directory where the data files will be stored.");
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbLoad,
            this.tsbSave,
            this.tsbRun,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1326, 27);
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbLoad
            // 
            this.tsbLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLoad.Name = "tsbLoad";
            this.tsbLoad.Size = new System.Drawing.Size(46, 24);
            this.tsbLoad.Text = "Load";
            this.tsbLoad.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // tsbSave
            // 
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(44, 24);
            this.tsbSave.Text = "Save";
            this.tsbSave.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // tsbRun
            // 
            this.tsbRun.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRun.Name = "tsbRun";
            this.tsbRun.Size = new System.Drawing.Size(38, 24);
            this.tsbRun.Text = "Run";
            this.tsbRun.Click += new System.EventHandler(this.runButton_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(29, 24);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // tlpMainLayout
            // 
            this.tlpMainLayout.ColumnCount = 1;
            this.tlpMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tlpMainLayout.Controls.Add(this.gbxFetchSettings, 0, 0);
            this.tlpMainLayout.Controls.Add(this.gbxWriteSettings, 0, 1);
            this.tlpMainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMainLayout.Location = new System.Drawing.Point(0, 27);
            this.tlpMainLayout.Margin = new System.Windows.Forms.Padding(4);
            this.tlpMainLayout.Name = "tlpMainLayout";
            this.tlpMainLayout.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.tlpMainLayout.RowCount = 2;
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMainLayout.Size = new System.Drawing.Size(1326, 641);
            this.tlpMainLayout.TabIndex = 7;
            // 
            // fisOutputDirectory
            // 
            this.fisOutputDirectory.AutoSize = true;
            this.fisOutputDirectory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fisOutputDirectory.Location = new System.Drawing.Point(212, 41);
            this.fisOutputDirectory.Margin = new System.Windows.Forms.Padding(5);
            this.fisOutputDirectory.MinimumSize = new System.Drawing.Size(133, 31);
            this.fisOutputDirectory.Name = "fisOutputDirectory";
            this.fisOutputDirectory.Size = new System.Drawing.Size(1061, 31);
            this.fisOutputDirectory.TabIndex = 7;
            this.fisOutputDirectory.Value = "";
            // 
            // ImportPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpMainLayout);
            this.Controls.Add(this.toolStrip1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ImportPage";
            this.Size = new System.Drawing.Size(1326, 668);
            this.gbxFetchSettings.ResumeLayout(false);
            this.gbxFetchSettings.PerformLayout();
            this.tlpFetchSettings.ResumeLayout(false);
            this.tlpFetchSettings.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.fplFileTypes.ResumeLayout(false);
            this.fplFileTypes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nbxPageSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nbxTopCount)).EndInit();
            this.gbxWriteSettings.ResumeLayout(false);
            this.gbxWriteSettings.PerformLayout();
            this.tlpWriteSettings.ResumeLayout(false);
            this.tlpWriteSettings.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tlpMainLayout.ResumeLayout(false);
            this.tlpMainLayout.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxFetchSettings;
        private System.Windows.Forms.GroupBox gbxWriteSettings;
        private System.Windows.Forms.TableLayoutPanel tlpWriteSettings;
        private System.Windows.Forms.Label lblFileType;
        private System.Windows.Forms.Label lblOutputDirectory;
        private System.Windows.Forms.FlowLayoutPanel fplFileTypes;
        private System.Windows.Forms.RadioButton rbnDataFormatJson;
        private System.Windows.Forms.RadioButton rbnDataFormatCsv;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbImportSchema;
        private System.Windows.Forms.Button btnImportSchema;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbLoad;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.ToolStripButton tsbRun;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Label lblBatchSize;
        private System.Windows.Forms.NumericUpDown nbxBatchSize;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private FolderInputSelector fisOutputDirectory;
        private System.Windows.Forms.TableLayoutPanel tlpMainLayout;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.TextBox tbxFileNamePrefix;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.TableLayoutPanel tlpFetchSettings;
        private ToggleCheckBox tcbIgnoreStatuses;
        private System.Windows.Forms.Label lblEnvironment;
        private System.Windows.Forms.Label lblSchemaFile;
        private System.Windows.Forms.Label lblPageSize;
        private System.Windows.Forms.Label lblTopCount;
        private System.Windows.Forms.Label lblIgnoreSystemFields;
        private System.Windows.Forms.Label lblIgnoreRecordStatus;
        private DataverseEnvironmentSelector dataverseEnvironmentSelector1;
        private FileInputSelector fisSchemaFile;
        private System.Windows.Forms.NumericUpDown nbxPageSize;
        private System.Windows.Forms.NumericUpDown nbxTopCount;
        private ToggleCheckBox tcbIgnoreSystemFields;
        private ToggleCheckBox tcbIgnoreRecordStatus;
    }
}
