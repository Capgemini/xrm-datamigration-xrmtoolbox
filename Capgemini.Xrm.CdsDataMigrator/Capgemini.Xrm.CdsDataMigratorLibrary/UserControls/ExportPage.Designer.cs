﻿namespace Capgemini.Xrm.CdsDataMigratorLibrary.UserControls
{
    partial class ExportPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportPage));
            this.gbxFetchSettings = new System.Windows.Forms.GroupBox();
            this.tlpFetchSettings = new System.Windows.Forms.TableLayoutPanel();
            this.lblEnvironment = new System.Windows.Forms.Label();
            this.lblSchemaFile = new System.Windows.Forms.Label();
            this.lblPageSize = new System.Windows.Forms.Label();
            this.lblTopCount = new System.Windows.Forms.Label();
            this.dataverseEnvironmentSelector1 = new Capgemini.Xrm.CdsDataMigratorLibrary.UserControls.DataverseEnvironmentSelector();
            this.fisSchemaFile = new Capgemini.Xrm.CdsDataMigratorLibrary.UserControls.FileInputSelector();
            this.nbxPageSize = new System.Windows.Forms.NumericUpDown();
            this.nbxTopCount = new System.Windows.Forms.NumericUpDown();
            this.lblActiveRecords = new System.Windows.Forms.Label();
            this.tcbActiveRecords = new Capgemini.Xrm.CdsDataMigratorLibrary.UserControls.ToggleCheckBox();
            this.btnFetchXmlFilters = new System.Windows.Forms.Button();
            this.lblFetchXmlFilters = new System.Windows.Forms.Label();
            this.btnUpdateLookupMappings = new System.Windows.Forms.Button();
            this.lblUpdateLookupMappings = new System.Windows.Forms.Label();
            this.gbxWriteSettings = new System.Windows.Forms.GroupBox();
            this.tlpWriteSettings = new System.Windows.Forms.TableLayoutPanel();
            this.lblFileType = new System.Windows.Forms.Label();
            this.lblOutputDirectory = new System.Windows.Forms.Label();
            this.lblBatchSize = new System.Windows.Forms.Label();
            this.fplFileTypes = new System.Windows.Forms.FlowLayoutPanel();
            this.rbnDataFormatJson = new System.Windows.Forms.RadioButton();
            this.rbnDataFormatCsv = new System.Windows.Forms.RadioButton();
            this.fisOutputDirectory = new Capgemini.Xrm.CdsDataMigratorLibrary.UserControls.FolderInputSelector();
            this.nbxBatchSize = new System.Windows.Forms.NumericUpDown();
            this.lblOneEntityPerBatch = new System.Windows.Forms.Label();
            this.tcbOneEntityPerBatch = new Capgemini.Xrm.CdsDataMigratorLibrary.UserControls.ToggleCheckBox();
            this.lblSeparateFilesPerEntity = new System.Windows.Forms.Label();
            this.tcbSeparateFilesPerEntity = new Capgemini.Xrm.CdsDataMigratorLibrary.UserControls.ToggleCheckBox();
            this.lblFileNamePrefix = new System.Windows.Forms.Label();
            this.tbxFileNamePrefix = new System.Windows.Forms.TextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbLoad = new System.Windows.Forms.ToolStripButton();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.tsbRun = new System.Windows.Forms.ToolStripButton();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.tlpMainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.gbxFetchSettings.SuspendLayout();
            this.tlpFetchSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nbxPageSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nbxTopCount)).BeginInit();
            this.gbxWriteSettings.SuspendLayout();
            this.tlpWriteSettings.SuspendLayout();
            this.fplFileTypes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nbxBatchSize)).BeginInit();
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
            this.gbxFetchSettings.Size = new System.Drawing.Size(1340, 310);
            this.gbxFetchSettings.TabIndex = 0;
            this.gbxFetchSettings.TabStop = false;
            this.gbxFetchSettings.Text = "Fetch settings (from Dataverse)";
            // 
            // tlpFetchSettings
            // 
            this.tlpFetchSettings.AutoScroll = true;
            this.tlpFetchSettings.AutoSize = true;
            this.tlpFetchSettings.ColumnCount = 2;
            this.tlpFetchSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.45274F));
            this.tlpFetchSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 83.54726F));
            this.tlpFetchSettings.Controls.Add(this.lblEnvironment, 0, 0);
            this.tlpFetchSettings.Controls.Add(this.lblSchemaFile, 0, 1);
            this.tlpFetchSettings.Controls.Add(this.lblPageSize, 0, 2);
            this.tlpFetchSettings.Controls.Add(this.lblTopCount, 0, 3);
            this.tlpFetchSettings.Controls.Add(this.dataverseEnvironmentSelector1, 1, 0);
            this.tlpFetchSettings.Controls.Add(this.fisSchemaFile, 1, 1);
            this.tlpFetchSettings.Controls.Add(this.nbxPageSize, 1, 2);
            this.tlpFetchSettings.Controls.Add(this.nbxTopCount, 1, 3);
            this.tlpFetchSettings.Controls.Add(this.lblActiveRecords, 0, 4);
            this.tlpFetchSettings.Controls.Add(this.tcbActiveRecords, 1, 4);
            this.tlpFetchSettings.Controls.Add(this.btnFetchXmlFilters, 1, 5);
            this.tlpFetchSettings.Controls.Add(this.lblFetchXmlFilters, 0, 5);
            this.tlpFetchSettings.Controls.Add(this.btnUpdateLookupMappings, 1, 6);
            this.tlpFetchSettings.Controls.Add(this.lblUpdateLookupMappings, 0, 6);
            this.tlpFetchSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpFetchSettings.Location = new System.Drawing.Point(13, 27);
            this.tlpFetchSettings.Margin = new System.Windows.Forms.Padding(4);
            this.tlpFetchSettings.Name = "tlpFetchSettings";
            this.tlpFetchSettings.RowCount = 6;
            this.tlpFetchSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpFetchSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpFetchSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpFetchSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpFetchSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpFetchSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tlpFetchSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 14F));
            this.tlpFetchSettings.Size = new System.Drawing.Size(1314, 271);
            this.tlpFetchSettings.TabIndex = 0;
            // 
            // lblEnvironment
            // 
            this.lblEnvironment.AutoSize = true;
            this.lblEnvironment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEnvironment.Location = new System.Drawing.Point(4, 0);
            this.lblEnvironment.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEnvironment.Name = "lblEnvironment";
            this.lblEnvironment.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.lblEnvironment.Size = new System.Drawing.Size(208, 34);
            this.lblEnvironment.TabIndex = 7;
            this.lblEnvironment.Text = "Environment";
            this.toolTip.SetToolTip(this.lblEnvironment, "The Dataverse environment you want to export data from.");
            // 
            // lblSchemaFile
            // 
            this.lblSchemaFile.AutoSize = true;
            this.lblSchemaFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSchemaFile.Location = new System.Drawing.Point(4, 35);
            this.lblSchemaFile.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSchemaFile.Name = "lblSchemaFile";
            this.lblSchemaFile.Size = new System.Drawing.Size(208, 40);
            this.lblSchemaFile.TabIndex = 0;
            this.lblSchemaFile.Text = "Schema File";
            this.toolTip.SetToolTip(this.lblSchemaFile, "The path to a Schema file that defines the entities and columns to export. This c" +
        "an be generated from the Schema tab or using Microsoft\'s Configuration exporter." +
        "");
            // 
            // lblPageSize
            // 
            this.lblPageSize.AutoSize = true;
            this.lblPageSize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPageSize.Location = new System.Drawing.Point(4, 75);
            this.lblPageSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPageSize.Name = "lblPageSize";
            this.lblPageSize.Size = new System.Drawing.Size(208, 30);
            this.lblPageSize.TabIndex = 1;
            this.lblPageSize.Text = "Page Size";
            this.toolTip.SetToolTip(this.lblPageSize, "The number of records to export in each request.");
            // 
            // lblTopCount
            // 
            this.lblTopCount.AutoSize = true;
            this.lblTopCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTopCount.Location = new System.Drawing.Point(4, 104);
            this.lblTopCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTopCount.Name = "lblTopCount";
            this.lblTopCount.Size = new System.Drawing.Size(208, 30);
            this.lblTopCount.TabIndex = 2;
            this.lblTopCount.Text = "Top Count";
            this.toolTip.SetToolTip(this.lblTopCount, "The maxium number of records to export. ");
            // 
            // dataverseEnvironmentSelector1
            // 
            this.dataverseEnvironmentSelector1.ConnectionUpdatedScope = Capgemini.Xrm.CdsDataMigratorLibrary.UserControls.DataverseEnvironmentSelector.Scope.Global;
            this.dataverseEnvironmentSelector1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataverseEnvironmentSelector1.Location = new System.Drawing.Point(221, 5);
            this.dataverseEnvironmentSelector1.Margin = new System.Windows.Forms.Padding(5);
            this.dataverseEnvironmentSelector1.MinimumSize = new System.Drawing.Size(0, 30);
            this.dataverseEnvironmentSelector1.Name = "dataverseEnvironmentSelector1";
            this.dataverseEnvironmentSelector1.Size = new System.Drawing.Size(1088, 30);
            this.dataverseEnvironmentSelector1.TabIndex = 8;
            // 
            // fisSchemaFile
            // 
            this.fisSchemaFile.AutoSize = true;
            this.fisSchemaFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fisSchemaFile.Location = new System.Drawing.Point(221, 39);
            this.fisSchemaFile.Margin = new System.Windows.Forms.Padding(5);
            this.fisSchemaFile.MinimumSize = new System.Drawing.Size(133, 30);
            this.fisSchemaFile.Name = "fisSchemaFile";
            this.fisSchemaFile.Size = new System.Drawing.Size(1088, 30);
            this.fisSchemaFile.TabIndex = 5;
            this.fisSchemaFile.Value = "";
            // 
            // nbxPageSize
            // 
            this.nbxPageSize.Location = new System.Drawing.Point(275, 98);
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
            this.nbxPageSize.Size = new System.Drawing.Size(250, 22);
            this.nbxPageSize.TabIndex = 3;
            this.nbxPageSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nbxTopCount
            // 
            this.nbxTopCount.Location = new System.Drawing.Point(275, 135);
            this.nbxTopCount.Margin = new System.Windows.Forms.Padding(4);
            this.nbxTopCount.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.nbxTopCount.Name = "nbxTopCount";
            this.nbxTopCount.Size = new System.Drawing.Size(250, 22);
            this.nbxTopCount.TabIndex = 4;
            // 
            // lblActiveRecords
            // 
            this.lblActiveRecords.AutoSize = true;
            this.lblActiveRecords.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblActiveRecords.Location = new System.Drawing.Point(4, 135);
            this.lblActiveRecords.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblActiveRecords.Name = "lblActiveRecords";
            this.lblActiveRecords.Size = new System.Drawing.Size(208, 38);
            this.lblActiveRecords.TabIndex = 9;
            this.lblActiveRecords.Text = "Only active records?";
            this.toolTip.SetToolTip(this.lblActiveRecords, "Restricts the export based on records status.");
            // 
            // tcbActiveRecords
            // 
            this.tcbActiveRecords.AutoSize = true;
            this.tcbActiveRecords.Location = new System.Drawing.Point(220, 139);
            this.tcbActiveRecords.Margin = new System.Windows.Forms.Padding(4);
            this.tcbActiveRecords.Name = "tcbActiveRecords";
            this.tcbActiveRecords.Padding = new System.Windows.Forms.Padding(5);
            this.tcbActiveRecords.Size = new System.Drawing.Size(63, 30);
            this.tcbActiveRecords.TabIndex = 10;
            this.tcbActiveRecords.Text = "Yes";
            this.tcbActiveRecords.UseVisualStyleBackColor = true;
            // 
            // btnFetchXmlFilters
            // 
            this.btnFetchXmlFilters.Location = new System.Drawing.Point(220, 175);
            this.btnFetchXmlFilters.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnFetchXmlFilters.Name = "btnFetchXmlFilters";
            this.btnFetchXmlFilters.Size = new System.Drawing.Size(80, 27);
            this.btnFetchXmlFilters.TabIndex = 11;
            this.btnFetchXmlFilters.Text = "Edit";
            this.btnFetchXmlFilters.UseVisualStyleBackColor = true;
            this.btnFetchXmlFilters.Click += new System.EventHandler(this.btnFetchXmlFilters_Click);
            // 
            // lblFetchXmlFilters
            // 
            this.lblFetchXmlFilters.AutoSize = true;
            this.lblFetchXmlFilters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFetchXmlFilters.Location = new System.Drawing.Point(4, 172);
            this.lblFetchXmlFilters.Name = "lblFetchXmlFilters";
            this.lblFetchXmlFilters.Size = new System.Drawing.Size(210, 31);
            this.lblFetchXmlFilters.TabIndex = 12;
            this.lblFetchXmlFilters.Text = "FetchXml Filters";
            // 
            // btnUpdateLookupMappings
            // 
            this.btnUpdateLookupMappings.Location = new System.Drawing.Point(220, 205);
            this.btnUpdateLookupMappings.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnUpdateLookupMappings.Name = "btnUpdateLookupMappings";
            this.btnUpdateLookupMappings.Size = new System.Drawing.Size(80, 27);
            this.btnUpdateLookupMappings.TabIndex = 11;
            this.btnUpdateLookupMappings.Text = "Edit";
            this.btnUpdateLookupMappings.UseVisualStyleBackColor = true;
            this.btnUpdateLookupMappings.Click += new System.EventHandler(this.btnUpdateLookupMappings_Click);
            // 
            // lblUpdateLookupMappings
            // 
            this.lblUpdateLookupMappings.AutoSize = true;
            this.lblUpdateLookupMappings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblUpdateLookupMappings.Location = new System.Drawing.Point(4, 203);
            this.lblUpdateLookupMappings.Name = "lblUpdateLookupMappings";
            this.lblUpdateLookupMappings.Size = new System.Drawing.Size(210, 68);
            this.lblUpdateLookupMappings.TabIndex = 12;
            this.lblUpdateLookupMappings.Text = "Lookup mappings";
            // 
            // gbxWriteSettings
            // 
            this.gbxWriteSettings.Controls.Add(this.tlpWriteSettings);
            this.gbxWriteSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxWriteSettings.Location = new System.Drawing.Point(11, 328);
            this.gbxWriteSettings.Margin = new System.Windows.Forms.Padding(4);
            this.gbxWriteSettings.Name = "gbxWriteSettings";
            this.gbxWriteSettings.Padding = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.gbxWriteSettings.Size = new System.Drawing.Size(1340, 311);
            this.gbxWriteSettings.TabIndex = 1;
            this.gbxWriteSettings.TabStop = false;
            this.gbxWriteSettings.Text = "Write settings (to files)";
            // 
            // tlpWriteSettings
            // 
            this.tlpWriteSettings.AutoScroll = true;
            this.tlpWriteSettings.AutoSize = true;
            this.tlpWriteSettings.ColumnCount = 2;
            this.tlpWriteSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.21937F));
            this.tlpWriteSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 83.78063F));
            this.tlpWriteSettings.Controls.Add(this.lblFileType, 0, 0);
            this.tlpWriteSettings.Controls.Add(this.lblOutputDirectory, 0, 1);
            this.tlpWriteSettings.Controls.Add(this.lblBatchSize, 0, 4);
            this.tlpWriteSettings.Controls.Add(this.fplFileTypes, 1, 0);
            this.tlpWriteSettings.Controls.Add(this.fisOutputDirectory, 1, 1);
            this.tlpWriteSettings.Controls.Add(this.nbxBatchSize, 1, 4);
            this.tlpWriteSettings.Controls.Add(this.lblOneEntityPerBatch, 0, 5);
            this.tlpWriteSettings.Controls.Add(this.tcbOneEntityPerBatch, 1, 5);
            this.tlpWriteSettings.Controls.Add(this.lblSeparateFilesPerEntity, 0, 6);
            this.tlpWriteSettings.Controls.Add(this.tcbSeparateFilesPerEntity, 1, 6);
            this.tlpWriteSettings.Controls.Add(this.lblFileNamePrefix, 0, 3);
            this.tlpWriteSettings.Controls.Add(this.tbxFileNamePrefix, 1, 3);
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
            this.tlpWriteSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpWriteSettings.Size = new System.Drawing.Size(1314, 272);
            this.tlpWriteSettings.TabIndex = 0;
            // 
            // lblFileType
            // 
            this.lblFileType.AutoSize = true;
            this.lblFileType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFileType.Location = new System.Drawing.Point(4, 0);
            this.lblFileType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFileType.Name = "lblFileType";
            this.lblFileType.Size = new System.Drawing.Size(205, 36);
            this.lblFileType.TabIndex = 0;
            this.lblFileType.Text = "File Type";
            this.toolTip.SetToolTip(this.lblFileType, "The type of file the data will be stored in.");
            // 
            // lblOutputDirectory
            // 
            this.lblOutputDirectory.AutoSize = true;
            this.lblOutputDirectory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOutputDirectory.Location = new System.Drawing.Point(4, 36);
            this.lblOutputDirectory.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOutputDirectory.Name = "lblOutputDirectory";
            this.lblOutputDirectory.Size = new System.Drawing.Size(205, 40);
            this.lblOutputDirectory.TabIndex = 1;
            this.lblOutputDirectory.Text = "Output Directory";
            this.toolTip.SetToolTip(this.lblOutputDirectory, "The directory where the data files will be stored.");
            // 
            // lblBatchSize
            // 
            this.lblBatchSize.AutoSize = true;
            this.lblBatchSize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBatchSize.Location = new System.Drawing.Point(4, 106);
            this.lblBatchSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBatchSize.Name = "lblBatchSize";
            this.lblBatchSize.Size = new System.Drawing.Size(205, 30);
            this.lblBatchSize.TabIndex = 5;
            this.lblBatchSize.Text = "Batch Size";
            this.toolTip.SetToolTip(this.lblBatchSize, "The maximum number of records to store in each data file.");
            // 
            // fplFileTypes
            // 
            this.fplFileTypes.AutoSize = true;
            this.fplFileTypes.Controls.Add(this.rbnDataFormatJson);
            this.fplFileTypes.Controls.Add(this.rbnDataFormatCsv);
            this.fplFileTypes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fplFileTypes.Location = new System.Drawing.Point(217, 4);
            this.fplFileTypes.Margin = new System.Windows.Forms.Padding(4);
            this.fplFileTypes.Name = "fplFileTypes";
            this.fplFileTypes.Size = new System.Drawing.Size(1093, 28);
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
            // 
            // fisOutputDirectory
            // 
            this.fisOutputDirectory.AutoSize = true;
            this.fisOutputDirectory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fisOutputDirectory.Location = new System.Drawing.Point(218, 41);
            this.fisOutputDirectory.Margin = new System.Windows.Forms.Padding(5);
            this.fisOutputDirectory.MinimumSize = new System.Drawing.Size(133, 30);
            this.fisOutputDirectory.Name = "fisOutputDirectory";
            this.fisOutputDirectory.Size = new System.Drawing.Size(1091, 30);
            this.fisOutputDirectory.TabIndex = 7;
            this.fisOutputDirectory.Value = "";
            // 
            // nbxBatchSize
            // 
            this.nbxBatchSize.Dock = System.Windows.Forms.DockStyle.Left;
            this.nbxBatchSize.Location = new System.Drawing.Point(217, 110);
            this.nbxBatchSize.Margin = new System.Windows.Forms.Padding(4);
            this.nbxBatchSize.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nbxBatchSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nbxBatchSize.Name = "nbxBatchSize";
            this.nbxBatchSize.Size = new System.Drawing.Size(131, 22);
            this.nbxBatchSize.TabIndex = 6;
            this.nbxBatchSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblOneEntityPerBatch
            // 
            this.lblOneEntityPerBatch.AutoSize = true;
            this.lblOneEntityPerBatch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOneEntityPerBatch.Location = new System.Drawing.Point(4, 136);
            this.lblOneEntityPerBatch.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOneEntityPerBatch.Name = "lblOneEntityPerBatch";
            this.lblOneEntityPerBatch.Size = new System.Drawing.Size(205, 38);
            this.lblOneEntityPerBatch.TabIndex = 8;
            this.lblOneEntityPerBatch.Text = "One entity per batch?";
            this.toolTip.SetToolTip(this.lblOneEntityPerBatch, "Only applies of SeperateFilesPerEntity is false. If this is false then Export fil" +
        "es are created upto the BatchSize value containing data from one or more entitie" +
        "s.");
            // 
            // tcbOneEntityPerBatch
            // 
            this.tcbOneEntityPerBatch.AutoSize = true;
            this.tcbOneEntityPerBatch.Location = new System.Drawing.Point(217, 140);
            this.tcbOneEntityPerBatch.Margin = new System.Windows.Forms.Padding(4);
            this.tcbOneEntityPerBatch.Name = "tcbOneEntityPerBatch";
            this.tcbOneEntityPerBatch.Padding = new System.Windows.Forms.Padding(5);
            this.tcbOneEntityPerBatch.Size = new System.Drawing.Size(63, 30);
            this.tcbOneEntityPerBatch.TabIndex = 9;
            this.tcbOneEntityPerBatch.Text = "Yes";
            this.tcbOneEntityPerBatch.UseVisualStyleBackColor = true;
            // 
            // lblSeparateFilesPerEntity
            // 
            this.lblSeparateFilesPerEntity.AutoSize = true;
            this.lblSeparateFilesPerEntity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSeparateFilesPerEntity.Location = new System.Drawing.Point(4, 174);
            this.lblSeparateFilesPerEntity.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSeparateFilesPerEntity.Name = "lblSeparateFilesPerEntity";
            this.lblSeparateFilesPerEntity.Size = new System.Drawing.Size(205, 98);
            this.lblSeparateFilesPerEntity.TabIndex = 10;
            this.lblSeparateFilesPerEntity.Text = "Separate files per entity?";
            this.toolTip.SetToolTip(this.lblSeparateFilesPerEntity, "Ensures that each entity type will be saved to a seperate file, otherwise there m" +
        "ight be multiple entities types in one file basedon the setting of OneEntityPerB" +
        "atch.");
            // 
            // tcbSeparateFilesPerEntity
            // 
            this.tcbSeparateFilesPerEntity.AutoSize = true;
            this.tcbSeparateFilesPerEntity.Location = new System.Drawing.Point(217, 178);
            this.tcbSeparateFilesPerEntity.Margin = new System.Windows.Forms.Padding(4);
            this.tcbSeparateFilesPerEntity.Name = "tcbSeparateFilesPerEntity";
            this.tcbSeparateFilesPerEntity.Padding = new System.Windows.Forms.Padding(5);
            this.tcbSeparateFilesPerEntity.Size = new System.Drawing.Size(63, 30);
            this.tcbSeparateFilesPerEntity.TabIndex = 11;
            this.tcbSeparateFilesPerEntity.Text = "Yes";
            this.tcbSeparateFilesPerEntity.UseVisualStyleBackColor = true;
            // 
            // lblFileNamePrefix
            // 
            this.lblFileNamePrefix.AutoSize = true;
            this.lblFileNamePrefix.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFileNamePrefix.Location = new System.Drawing.Point(4, 76);
            this.lblFileNamePrefix.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFileNamePrefix.Name = "lblFileNamePrefix";
            this.lblFileNamePrefix.Size = new System.Drawing.Size(205, 30);
            this.lblFileNamePrefix.TabIndex = 12;
            this.lblFileNamePrefix.Text = "File name prefix";
            this.toolTip.SetToolTip(this.lblFileNamePrefix, "Defines the common prefix for all exported files.");
            // 
            // tbxFileNamePrefix
            // 
            this.tbxFileNamePrefix.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxFileNamePrefix.Location = new System.Drawing.Point(217, 80);
            this.tbxFileNamePrefix.Margin = new System.Windows.Forms.Padding(4);
            this.tbxFileNamePrefix.Name = "tbxFileNamePrefix";
            this.tbxFileNamePrefix.Size = new System.Drawing.Size(1093, 22);
            this.tbxFileNamePrefix.TabIndex = 13;
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbLoad,
            this.tsbSave,
            this.tsbRun});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1362, 27);
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbLoad
            // 
            this.tsbLoad.Image = ((System.Drawing.Image)(resources.GetObject("tsbLoad.Image")));
            this.tsbLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLoad.Name = "tsbLoad";
            this.tsbLoad.Size = new System.Drawing.Size(66, 24);
            this.tsbLoad.Text = "Load";
            this.tsbLoad.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // tsbSave
            // 
            this.tsbSave.Image = global::Capgemini.Xrm.CdsDataMigratorLibrary.Properties.Resource.Save;
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(64, 24);
            this.tsbSave.Text = "Save";
            this.tsbSave.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // tsbRun
            // 
            this.tsbRun.Image = ((System.Drawing.Image)(resources.GetObject("tsbRun.Image")));
            this.tsbRun.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRun.Name = "tsbRun";
            this.tsbRun.Size = new System.Drawing.Size(58, 24);
            this.tsbRun.Text = "Run";
            this.tsbRun.Click += new System.EventHandler(this.runButton_Click);
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
            this.tlpMainLayout.Size = new System.Drawing.Size(1362, 649);
            this.tlpMainLayout.TabIndex = 7;
            // 
            // ExportPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpMainLayout);
            this.Controls.Add(this.toolStrip1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ExportPage";
            this.Size = new System.Drawing.Size(1362, 676);
            this.gbxFetchSettings.ResumeLayout(false);
            this.gbxFetchSettings.PerformLayout();
            this.tlpFetchSettings.ResumeLayout(false);
            this.tlpFetchSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nbxPageSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nbxTopCount)).EndInit();
            this.gbxWriteSettings.ResumeLayout(false);
            this.gbxWriteSettings.PerformLayout();
            this.tlpWriteSettings.ResumeLayout(false);
            this.tlpWriteSettings.PerformLayout();
            this.fplFileTypes.ResumeLayout(false);
            this.fplFileTypes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nbxBatchSize)).EndInit();
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
        private System.Windows.Forms.TableLayoutPanel tlpFetchSettings;
        private System.Windows.Forms.Label lblSchemaFile;
        private System.Windows.Forms.Label lblPageSize;
        private System.Windows.Forms.Label lblTopCount;
        private System.Windows.Forms.NumericUpDown nbxPageSize;
        private System.Windows.Forms.NumericUpDown nbxTopCount;
        private System.Windows.Forms.TableLayoutPanel tlpWriteSettings;
        private System.Windows.Forms.Label lblFileType;
        private System.Windows.Forms.Label lblOutputDirectory;
        private System.Windows.Forms.FlowLayoutPanel fplFileTypes;
        private System.Windows.Forms.RadioButton rbnDataFormatJson;
        private System.Windows.Forms.RadioButton rbnDataFormatCsv;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbLoad;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.ToolStripButton tsbRun;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Label lblBatchSize;
        private System.Windows.Forms.NumericUpDown nbxBatchSize;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private FileInputSelector fisSchemaFile;
        private FolderInputSelector fisOutputDirectory;
        private System.Windows.Forms.Label lblEnvironment;
        private DataverseEnvironmentSelector dataverseEnvironmentSelector1;
        private System.Windows.Forms.TableLayoutPanel tlpMainLayout;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label lblActiveRecords;
        private ToggleCheckBox tcbActiveRecords;
        private System.Windows.Forms.Label lblOneEntityPerBatch;
        private ToggleCheckBox tcbOneEntityPerBatch;
        private System.Windows.Forms.Label lblSeparateFilesPerEntity;
        private ToggleCheckBox tcbSeparateFilesPerEntity;
        private System.Windows.Forms.Label lblFileNamePrefix;
        private System.Windows.Forms.TextBox tbxFileNamePrefix;
        private System.Windows.Forms.Button btnFetchXmlFilters;
        private System.Windows.Forms.Label lblFetchXmlFilters;
        private System.Windows.Forms.Button btnUpdateLookupMappings;
        private System.Windows.Forms.Label lblUpdateLookupMappings;
    }
}
