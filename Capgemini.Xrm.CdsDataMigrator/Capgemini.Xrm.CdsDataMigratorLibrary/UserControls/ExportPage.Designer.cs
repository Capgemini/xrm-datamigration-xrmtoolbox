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
            this.gbxFetchSettings.Location = new System.Drawing.Point(8, 8);
            this.gbxFetchSettings.Name = "gbxFetchSettings";
            this.gbxFetchSettings.Padding = new System.Windows.Forms.Padding(10);
            this.gbxFetchSettings.Size = new System.Drawing.Size(837, 194);
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
            this.tlpFetchSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpFetchSettings.Location = new System.Drawing.Point(10, 23);
            this.tlpFetchSettings.Name = "tlpFetchSettings";
            this.tlpFetchSettings.RowCount = 5;
            this.tlpFetchSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpFetchSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpFetchSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpFetchSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpFetchSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpFetchSettings.Size = new System.Drawing.Size(817, 161);
            this.tlpFetchSettings.TabIndex = 0;
            // 
            // lblEnvironment
            // 
            this.lblEnvironment.AutoSize = true;
            this.lblEnvironment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEnvironment.Location = new System.Drawing.Point(3, 0);
            this.lblEnvironment.Name = "lblEnvironment";
            this.lblEnvironment.Size = new System.Drawing.Size(128, 31);
            this.lblEnvironment.TabIndex = 7;
            this.lblEnvironment.Text = "Environment";
            this.toolTip.SetToolTip(this.lblEnvironment, "The Dataverse environment you want to export data from.");
            // 
            // lblSchemaFile
            // 
            this.lblSchemaFile.AutoSize = true;
            this.lblSchemaFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSchemaFile.Location = new System.Drawing.Point(3, 31);
            this.lblSchemaFile.Name = "lblSchemaFile";
            this.lblSchemaFile.Size = new System.Drawing.Size(128, 33);
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
            this.lblPageSize.Location = new System.Drawing.Point(3, 64);
            this.lblPageSize.Name = "lblPageSize";
            this.lblPageSize.Size = new System.Drawing.Size(128, 26);
            this.lblPageSize.TabIndex = 1;
            this.lblPageSize.Text = "Page Size";
            this.toolTip.SetToolTip(this.lblPageSize, "The number of records to export in each request.");
            // 
            // lblTopCount
            // 
            this.lblTopCount.AutoSize = true;
            this.lblTopCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTopCount.Location = new System.Drawing.Point(3, 90);
            this.lblTopCount.Name = "lblTopCount";
            this.lblTopCount.Size = new System.Drawing.Size(128, 26);
            this.lblTopCount.TabIndex = 2;
            this.lblTopCount.Text = "Top Count";
            this.toolTip.SetToolTip(this.lblTopCount, "The maxium number of records to export. ");
            // 
            // dataverseEnvironmentSelector1
            // 
            this.dataverseEnvironmentSelector1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataverseEnvironmentSelector1.Location = new System.Drawing.Point(138, 4);
            this.dataverseEnvironmentSelector1.Margin = new System.Windows.Forms.Padding(4);
            this.dataverseEnvironmentSelector1.Name = "dataverseEnvironmentSelector1";
            this.dataverseEnvironmentSelector1.Size = new System.Drawing.Size(675, 23);
            this.dataverseEnvironmentSelector1.TabIndex = 8;
            // 
            // fisSchemaFile
            // 
            this.fisSchemaFile.AutoSize = true;
            this.fisSchemaFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fisSchemaFile.Location = new System.Drawing.Point(138, 35);
            this.fisSchemaFile.Margin = new System.Windows.Forms.Padding(4);
            this.fisSchemaFile.MinimumSize = new System.Drawing.Size(100, 25);
            this.fisSchemaFile.Name = "fisSchemaFile";
            this.fisSchemaFile.Size = new System.Drawing.Size(675, 25);
            this.fisSchemaFile.TabIndex = 5;
            this.fisSchemaFile.Value = "";
            // 
            // nbxPageSize
            // 
            this.nbxPageSize.Location = new System.Drawing.Point(137, 67);
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
            this.nbxPageSize.Size = new System.Drawing.Size(96, 20);
            this.nbxPageSize.TabIndex = 3;
            this.nbxPageSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nbxTopCount
            // 
            this.nbxTopCount.Location = new System.Drawing.Point(137, 93);
            this.nbxTopCount.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.nbxTopCount.Name = "nbxTopCount";
            this.nbxTopCount.Size = new System.Drawing.Size(96, 20);
            this.nbxTopCount.TabIndex = 4;
            // 
            // lblActiveRecords
            // 
            this.lblActiveRecords.AutoSize = true;
            this.lblActiveRecords.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblActiveRecords.Location = new System.Drawing.Point(3, 116);
            this.lblActiveRecords.Name = "lblActiveRecords";
            this.lblActiveRecords.Size = new System.Drawing.Size(128, 45);
            this.lblActiveRecords.TabIndex = 9;
            this.lblActiveRecords.Text = "Only active records?";
            // 
            // tcbActiveRecords
            // 
            this.tcbActiveRecords.AutoSize = true;
            this.tcbActiveRecords.Location = new System.Drawing.Point(137, 119);
            this.tcbActiveRecords.Name = "tcbActiveRecords";
            this.tcbActiveRecords.Padding = new System.Windows.Forms.Padding(4);
            this.tcbActiveRecords.Size = new System.Drawing.Size(52, 25);
            this.tcbActiveRecords.TabIndex = 10;
            this.tcbActiveRecords.Text = "Yes";
            this.tcbActiveRecords.UseVisualStyleBackColor = true;
            // 
            // gbxWriteSettings
            // 
            this.gbxWriteSettings.Controls.Add(this.tlpWriteSettings);
            this.gbxWriteSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxWriteSettings.Location = new System.Drawing.Point(8, 208);
            this.gbxWriteSettings.Name = "gbxWriteSettings";
            this.gbxWriteSettings.Padding = new System.Windows.Forms.Padding(10);
            this.gbxWriteSettings.Size = new System.Drawing.Size(837, 195);
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
            this.tlpWriteSettings.Controls.Add(this.lblBatchSize, 0, 3);
            this.tlpWriteSettings.Controls.Add(this.fplFileTypes, 1, 0);
            this.tlpWriteSettings.Controls.Add(this.fisOutputDirectory, 1, 1);
            this.tlpWriteSettings.Controls.Add(this.nbxBatchSize, 1, 3);
            this.tlpWriteSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpWriteSettings.Location = new System.Drawing.Point(10, 23);
            this.tlpWriteSettings.Name = "tlpWriteSettings";
            this.tlpWriteSettings.RowCount = 5;
            this.tlpWriteSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpWriteSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpWriteSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpWriteSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpWriteSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpWriteSettings.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpWriteSettings.Size = new System.Drawing.Size(817, 162);
            this.tlpWriteSettings.TabIndex = 0;
            // 
            // lblFileType
            // 
            this.lblFileType.AutoSize = true;
            this.lblFileType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFileType.Location = new System.Drawing.Point(3, 0);
            this.lblFileType.Name = "lblFileType";
            this.lblFileType.Size = new System.Drawing.Size(126, 29);
            this.lblFileType.TabIndex = 0;
            this.lblFileType.Text = "File Type";
            this.toolTip.SetToolTip(this.lblFileType, "The type of file the data will be stored in.");
            // 
            // lblOutputDirectory
            // 
            this.lblOutputDirectory.AutoSize = true;
            this.lblOutputDirectory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOutputDirectory.Location = new System.Drawing.Point(3, 29);
            this.lblOutputDirectory.Name = "lblOutputDirectory";
            this.lblOutputDirectory.Size = new System.Drawing.Size(126, 33);
            this.lblOutputDirectory.TabIndex = 1;
            this.lblOutputDirectory.Text = "Output Directory";
            this.toolTip.SetToolTip(this.lblOutputDirectory, "The directory where the data files will be stored.");
            // 
            // lblBatchSize
            // 
            this.lblBatchSize.AutoSize = true;
            this.lblBatchSize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBatchSize.Location = new System.Drawing.Point(3, 62);
            this.lblBatchSize.Name = "lblBatchSize";
            this.lblBatchSize.Size = new System.Drawing.Size(126, 26);
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
            this.fplFileTypes.Location = new System.Drawing.Point(135, 3);
            this.fplFileTypes.Name = "fplFileTypes";
            this.fplFileTypes.Size = new System.Drawing.Size(679, 23);
            this.fplFileTypes.TabIndex = 3;
            // 
            // rbnDataFormatJson
            // 
            this.rbnDataFormatJson.AutoSize = true;
            this.rbnDataFormatJson.Location = new System.Drawing.Point(3, 3);
            this.rbnDataFormatJson.Name = "rbnDataFormatJson";
            this.rbnDataFormatJson.Size = new System.Drawing.Size(53, 17);
            this.rbnDataFormatJson.TabIndex = 0;
            this.rbnDataFormatJson.TabStop = true;
            this.rbnDataFormatJson.Text = "JSON";
            this.rbnDataFormatJson.UseVisualStyleBackColor = true;
            // 
            // rbnDataFormatCsv
            // 
            this.rbnDataFormatCsv.AutoSize = true;
            this.rbnDataFormatCsv.Location = new System.Drawing.Point(62, 3);
            this.rbnDataFormatCsv.Name = "rbnDataFormatCsv";
            this.rbnDataFormatCsv.Size = new System.Drawing.Size(46, 17);
            this.rbnDataFormatCsv.TabIndex = 1;
            this.rbnDataFormatCsv.TabStop = true;
            this.rbnDataFormatCsv.Text = "CSV";
            this.rbnDataFormatCsv.UseVisualStyleBackColor = true;
            // 
            // fisOutputDirectory
            // 
            this.fisOutputDirectory.AutoSize = true;
            this.fisOutputDirectory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fisOutputDirectory.Location = new System.Drawing.Point(136, 33);
            this.fisOutputDirectory.Margin = new System.Windows.Forms.Padding(4);
            this.fisOutputDirectory.MinimumSize = new System.Drawing.Size(100, 25);
            this.fisOutputDirectory.Name = "fisOutputDirectory";
            this.fisOutputDirectory.Size = new System.Drawing.Size(677, 25);
            this.fisOutputDirectory.TabIndex = 7;
            this.fisOutputDirectory.Value = "";
            // 
            // nbxBatchSize
            // 
            this.nbxBatchSize.Dock = System.Windows.Forms.DockStyle.Left;
            this.nbxBatchSize.Location = new System.Drawing.Point(135, 65);
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
            this.nbxBatchSize.Size = new System.Drawing.Size(98, 20);
            this.nbxBatchSize.TabIndex = 6;
            this.nbxBatchSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
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
            this.toolStrip1.Size = new System.Drawing.Size(853, 27);
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbLoad
            // 
            this.tsbLoad.Image = ((System.Drawing.Image)(resources.GetObject("tsbLoad.Image")));
            this.tsbLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLoad.Name = "tsbLoad";
            this.tsbLoad.Size = new System.Drawing.Size(57, 24);
            this.tsbLoad.Text = "Load";
            this.tsbLoad.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // tsbSave
            // 
            this.tsbSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbSave.Image")));
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(55, 24);
            this.tsbSave.Text = "Save";
            this.tsbSave.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // tsbRun
            // 
            this.tsbRun.Image = ((System.Drawing.Image)(resources.GetObject("tsbRun.Image")));
            this.tsbRun.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRun.Name = "tsbRun";
            this.tsbRun.Size = new System.Drawing.Size(52, 24);
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
            this.tlpMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMainLayout.Controls.Add(this.gbxFetchSettings, 0, 0);
            this.tlpMainLayout.Controls.Add(this.gbxWriteSettings, 0, 1);
            this.tlpMainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMainLayout.Location = new System.Drawing.Point(0, 27);
            this.tlpMainLayout.Name = "tlpMainLayout";
            this.tlpMainLayout.Padding = new System.Windows.Forms.Padding(5);
            this.tlpMainLayout.RowCount = 2;
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMainLayout.Size = new System.Drawing.Size(853, 411);
            this.tlpMainLayout.TabIndex = 7;
            // 
            // ExportPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpMainLayout);
            this.Controls.Add(this.toolStrip1);
            this.Name = "ExportPage";
            this.Size = new System.Drawing.Size(853, 438);
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
    }
}
