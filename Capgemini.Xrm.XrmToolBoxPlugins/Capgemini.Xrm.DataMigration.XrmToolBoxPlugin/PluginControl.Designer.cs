using System;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin
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
            this.BatchSizeLabel = new System.Windows.Forms.Label();
            this.PageSizeLabel = new System.Windows.Forms.Label();
            this.TopCountLabel = new System.Windows.Forms.Label();
            this.topCount = new System.Windows.Forms.NumericUpDown();
            this.batchSize = new System.Windows.Forms.NumericUpDown();
            this.pageSize = new System.Windows.Forms.NumericUpDown();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbSchemaFilePath = new System.Windows.Forms.TextBox();
            this.ConnectionString = new System.Windows.Forms.Label();
            this.tbFolderPath = new System.Windows.Forms.TextBox();
            this.labelJsonFilePath = new System.Windows.Forms.Label();
            this.buttonTargetConnectionString = new System.Windows.Forms.Button();
            this.labelTargetConnectionString = new System.Windows.Forms.Label();
            this.labelSourceConnectionString = new System.Windows.Forms.Label();
            this.buttonSourceConnectionString = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btSeFile = new System.Windows.Forms.Button();
            this.btFolderPath = new System.Windows.Forms.Button();
            this.fbExportPath = new System.Windows.Forms.FolderBrowserDialog();
            this.fbSchemaPath = new System.Windows.Forms.FolderBrowserDialog();
            this.fdSchemaFile = new System.Windows.Forms.OpenFileDialog();
            this.tbMessage = new System.Windows.Forms.TextBox();
            this.cbOnlyActive = new System.Windows.Forms.CheckBox();
            this.cbIgnoreSystemFields = new System.Windows.Forms.CheckBox();
            this.btLoadImportConfigFile = new System.Windows.Forms.Button();
            this.tbImportConfigFile = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbIgnoreStatuses = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbMinJson = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btExportConfigFile = new System.Windows.Forms.Button();
            this.tbExportConfigFile = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.nudMaxThreads = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.nudSavePageSize = new System.Windows.Forms.NumericUpDown();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageDataMigration = new System.Windows.Forms.TabPage();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btExport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.btImport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btClearLog = new System.Windows.Forms.Button();
            this.tabPageSchema = new System.Windows.Forms.TabPage();
            this.gbRelationship = new System.Windows.Forms.GroupBox();
            this.chkAllRelationships = new System.Windows.Forms.CheckBox();
            this.lvRelationship = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gbEnvironments = new System.Windows.Forms.GroupBox();
            this.cbShowSystemAttributes = new System.Windows.Forms.CheckBox();
            this.btExportConfigPath = new System.Windows.Forms.Button();
            this.btImportConfigPath = new System.Windows.Forms.Button();
            this.tbExportConfig = new System.Windows.Forms.TextBox();
            this.tbImportConfig = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tbSchemaPath = new System.Windows.Forms.TextBox();
            this.btSchemaFolderPath = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.lbSchema = new System.Windows.Forms.Label();
            this.labelSchemaConnectionString = new System.Windows.Forms.Label();
            this.gbAttributes = new System.Windows.Forms.GroupBox();
            this.chkAllAttributes = new System.Windows.Forms.CheckBox();
            this.lvAttributes = new System.Windows.Forms.ListView();
            this.clAttDisplayName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clAttLogicalName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clAttType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clAttComment = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gbEntities = new System.Windows.Forms.GroupBox();
            this.chkAllEntities = new System.Windows.Forms.CheckBox();
            this.lvEntities = new System.Windows.Forms.ListView();
            this.clEntDisplayName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clEntLogicalName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.tsbCloseThisTab = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtRetrieveEntities = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtMappings = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtFilters = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.saveSchemaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFiltersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveMappingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.loadSchemaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadFiltersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadMappingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.topCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.batchSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pageSize)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxThreads)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSavePageSize)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabPageDataMigration.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.tabPageSchema.SuspendLayout();
            this.gbRelationship.SuspendLayout();
            this.gbEnvironments.SuspendLayout();
            this.gbAttributes.SuspendLayout();
            this.gbEntities.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // BatchSizeLabel
            // 
            this.BatchSizeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BatchSizeLabel.AutoSize = true;
            this.BatchSizeLabel.Location = new System.Drawing.Point(117, 134);
            this.BatchSizeLabel.Name = "BatchSizeLabel";
            this.BatchSizeLabel.Size = new System.Drawing.Size(58, 13);
            this.BatchSizeLabel.TabIndex = 1;
            this.BatchSizeLabel.Text = "Batch Size";
            // 
            // PageSizeLabel
            // 
            this.PageSizeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PageSizeLabel.AutoSize = true;
            this.PageSizeLabel.Location = new System.Drawing.Point(311, 106);
            this.PageSizeLabel.Name = "PageSizeLabel";
            this.PageSizeLabel.Size = new System.Drawing.Size(55, 13);
            this.PageSizeLabel.TabIndex = 2;
            this.PageSizeLabel.Text = "Page Size";
            // 
            // TopCountLabel
            // 
            this.TopCountLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TopCountLabel.AutoSize = true;
            this.TopCountLabel.Location = new System.Drawing.Point(312, 127);
            this.TopCountLabel.Name = "TopCountLabel";
            this.TopCountLabel.Size = new System.Drawing.Size(57, 13);
            this.TopCountLabel.TabIndex = 3;
            this.TopCountLabel.Text = "Top Count";
            // 
            // topCount
            // 
            this.topCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.topCount.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.topCount.Location = new System.Drawing.Point(397, 125);
            this.topCount.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.topCount.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.topCount.Name = "topCount";
            this.topCount.Size = new System.Drawing.Size(94, 20);
            this.topCount.TabIndex = 9;
            this.topCount.Value = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            // 
            // batchSize
            // 
            this.batchSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.batchSize.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.batchSize.Location = new System.Drawing.Point(192, 127);
            this.batchSize.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.batchSize.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.batchSize.Name = "batchSize";
            this.batchSize.Size = new System.Drawing.Size(94, 20);
            this.batchSize.TabIndex = 9;
            this.batchSize.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            // 
            // pageSize
            // 
            this.pageSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pageSize.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.pageSize.Location = new System.Drawing.Point(397, 99);
            this.pageSize.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.pageSize.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.pageSize.Name = "pageSize";
            this.pageSize.Size = new System.Drawing.Size(94, 20);
            this.pageSize.TabIndex = 9;
            this.pageSize.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.Location = new System.Drawing.Point(27, 248);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(0, 13);
            this.StatusLabel.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Schema File Path";
            // 
            // tbSchemaFilePath
            // 
            this.tbSchemaFilePath.Location = new System.Drawing.Point(173, 74);
            this.tbSchemaFilePath.Name = "tbSchemaFilePath";
            this.tbSchemaFilePath.Size = new System.Drawing.Size(285, 20);
            this.tbSchemaFilePath.TabIndex = 12;
            // 
            // ConnectionString
            // 
            this.ConnectionString.AutoSize = true;
            this.ConnectionString.Location = new System.Drawing.Point(28, 27);
            this.ConnectionString.Name = "ConnectionString";
            this.ConnectionString.Size = new System.Drawing.Size(125, 13);
            this.ConnectionString.TabIndex = 14;
            this.ConnectionString.Text = "Target Connection String";
            // 
            // tbFolderPath
            // 
            this.tbFolderPath.Location = new System.Drawing.Point(180, 202);
            this.tbFolderPath.Name = "tbFolderPath";
            this.tbFolderPath.Size = new System.Drawing.Size(367, 20);
            this.tbFolderPath.TabIndex = 17;
            // 
            // labelJsonFilePath
            // 
            this.labelJsonFilePath.AutoSize = true;
            this.labelJsonFilePath.Location = new System.Drawing.Point(14, 204);
            this.labelJsonFilePath.Name = "labelJsonFilePath";
            this.labelJsonFilePath.Size = new System.Drawing.Size(160, 13);
            this.labelJsonFilePath.TabIndex = 18;
            this.labelJsonFilePath.Text = "Json Extracted Data Folder Path";
            // 
            // buttonTargetConnectionString
            // 
            this.buttonTargetConnectionString.Location = new System.Drawing.Point(470, 27);
            this.buttonTargetConnectionString.Name = "buttonTargetConnectionString";
            this.buttonTargetConnectionString.Size = new System.Drawing.Size(26, 20);
            this.buttonTargetConnectionString.TabIndex = 19;
            this.buttonTargetConnectionString.Text = "...";
            this.buttonTargetConnectionString.UseVisualStyleBackColor = true;
            this.buttonTargetConnectionString.Click += new System.EventHandler(this.buttonTargetConnectionString_Click);
            // 
            // labelTargetConnectionString
            // 
            this.labelTargetConnectionString.AutoSize = true;
            this.labelTargetConnectionString.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTargetConnectionString.ForeColor = System.Drawing.Color.Green;
            this.labelTargetConnectionString.Location = new System.Drawing.Point(180, 27);
            this.labelTargetConnectionString.MinimumSize = new System.Drawing.Size(170, 0);
            this.labelTargetConnectionString.Name = "labelTargetConnectionString";
            this.labelTargetConnectionString.Size = new System.Drawing.Size(170, 13);
            this.labelTargetConnectionString.TabIndex = 20;
            // 
            // labelSourceConnectionString
            // 
            this.labelSourceConnectionString.AutoSize = true;
            this.labelSourceConnectionString.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSourceConnectionString.ForeColor = System.Drawing.Color.Green;
            this.labelSourceConnectionString.Location = new System.Drawing.Point(170, 22);
            this.labelSourceConnectionString.MinimumSize = new System.Drawing.Size(170, 0);
            this.labelSourceConnectionString.Name = "labelSourceConnectionString";
            this.labelSourceConnectionString.Size = new System.Drawing.Size(170, 13);
            this.labelSourceConnectionString.TabIndex = 23;
            // 
            // buttonSourceConnectionString
            // 
            this.buttonSourceConnectionString.Location = new System.Drawing.Point(465, 16);
            this.buttonSourceConnectionString.Name = "buttonSourceConnectionString";
            this.buttonSourceConnectionString.Size = new System.Drawing.Size(26, 24);
            this.buttonSourceConnectionString.TabIndex = 22;
            this.buttonSourceConnectionString.Text = "...";
            this.buttonSourceConnectionString.UseVisualStyleBackColor = true;
            this.buttonSourceConnectionString.Click += new System.EventHandler(this.buttonSourceConnectionString_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "Source Connection String";
            // 
            // btSeFile
            // 
            this.btSeFile.Location = new System.Drawing.Point(465, 74);
            this.btSeFile.Margin = new System.Windows.Forms.Padding(2);
            this.btSeFile.Name = "btSeFile";
            this.btSeFile.Size = new System.Drawing.Size(26, 19);
            this.btSeFile.TabIndex = 24;
            this.btSeFile.Text = "...";
            this.btSeFile.UseVisualStyleBackColor = true;
            this.btSeFile.Click += new System.EventHandler(this.btSeFile_Click);
            // 
            // btFolderPath
            // 
            this.btFolderPath.Location = new System.Drawing.Point(552, 202);
            this.btFolderPath.Margin = new System.Windows.Forms.Padding(2);
            this.btFolderPath.Name = "btFolderPath";
            this.btFolderPath.Size = new System.Drawing.Size(26, 20);
            this.btFolderPath.TabIndex = 25;
            this.btFolderPath.Text = "...";
            this.btFolderPath.UseVisualStyleBackColor = true;
            this.btFolderPath.Click += new System.EventHandler(this.btFolderPath_Click);
            // 
            // fdSchemaFile
            // 
            this.fdSchemaFile.FileName = "openFileDialog1";
            // 
            // tbMessage
            // 
            this.tbMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMessage.Location = new System.Drawing.Point(6, 228);
            this.tbMessage.Multiline = true;
            this.tbMessage.Name = "tbMessage";
            this.tbMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbMessage.Size = new System.Drawing.Size(1030, 340);
            this.tbMessage.TabIndex = 26;
            this.tbMessage.Text = "  ";
            this.tbMessage.WordWrap = false;
            // 
            // cbOnlyActive
            // 
            this.cbOnlyActive.AutoSize = true;
            this.cbOnlyActive.Location = new System.Drawing.Point(27, 103);
            this.cbOnlyActive.Margin = new System.Windows.Forms.Padding(2);
            this.cbOnlyActive.Name = "cbOnlyActive";
            this.cbOnlyActive.Size = new System.Drawing.Size(148, 17);
            this.cbOnlyActive.TabIndex = 29;
            this.cbOnlyActive.Text = "Export only active records";
            this.cbOnlyActive.UseVisualStyleBackColor = true;
            // 
            // cbIgnoreSystemFields
            // 
            this.cbIgnoreSystemFields.AutoSize = true;
            this.cbIgnoreSystemFields.Checked = true;
            this.cbIgnoreSystemFields.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIgnoreSystemFields.Location = new System.Drawing.Point(66, 108);
            this.cbIgnoreSystemFields.Margin = new System.Windows.Forms.Padding(2);
            this.cbIgnoreSystemFields.Name = "cbIgnoreSystemFields";
            this.cbIgnoreSystemFields.Size = new System.Drawing.Size(123, 17);
            this.cbIgnoreSystemFields.TabIndex = 30;
            this.cbIgnoreSystemFields.Text = "Ignore System Fields";
            this.cbIgnoreSystemFields.UseVisualStyleBackColor = true;
            // 
            // btLoadImportConfigFile
            // 
            this.btLoadImportConfigFile.Location = new System.Drawing.Point(470, 52);
            this.btLoadImportConfigFile.Margin = new System.Windows.Forms.Padding(2);
            this.btLoadImportConfigFile.Name = "btLoadImportConfigFile";
            this.btLoadImportConfigFile.Size = new System.Drawing.Size(26, 19);
            this.btLoadImportConfigFile.TabIndex = 33;
            this.btLoadImportConfigFile.Text = "...";
            this.btLoadImportConfigFile.UseVisualStyleBackColor = true;
            this.btLoadImportConfigFile.Click += new System.EventHandler(this.tbLoadImportConfigFile_Click);
            // 
            // tbImportConfigFile
            // 
            this.tbImportConfigFile.Location = new System.Drawing.Point(168, 51);
            this.tbImportConfigFile.Name = "tbImportConfigFile";
            this.tbImportConfigFile.Size = new System.Drawing.Size(285, 20);
            this.tbImportConfigFile.TabIndex = 32;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 13);
            this.label2.TabIndex = 31;
            this.label2.Text = "Import Config File (Optional)";
            // 
            // cbIgnoreStatuses
            // 
            this.cbIgnoreStatuses.AutoSize = true;
            this.cbIgnoreStatuses.Checked = true;
            this.cbIgnoreStatuses.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIgnoreStatuses.Location = new System.Drawing.Point(66, 130);
            this.cbIgnoreStatuses.Margin = new System.Windows.Forms.Padding(2);
            this.cbIgnoreStatuses.Name = "cbIgnoreStatuses";
            this.cbIgnoreStatuses.Size = new System.Drawing.Size(100, 17);
            this.cbIgnoreStatuses.TabIndex = 34;
            this.cbIgnoreStatuses.Text = "Ignore Statuses";
            this.cbIgnoreStatuses.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbMinJson);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btExportConfigFile);
            this.groupBox1.Controls.Add(this.BatchSizeLabel);
            this.groupBox1.Controls.Add(this.PageSizeLabel);
            this.groupBox1.Controls.Add(this.tbExportConfigFile);
            this.groupBox1.Controls.Add(this.TopCountLabel);
            this.groupBox1.Controls.Add(this.topCount);
            this.groupBox1.Controls.Add(this.batchSize);
            this.groupBox1.Controls.Add(this.pageSize);
            this.groupBox1.Controls.Add(this.cbOnlyActive);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbSchemaFilePath);
            this.groupBox1.Controls.Add(this.btSeFile);
            this.groupBox1.Controls.Add(this.buttonSourceConnectionString);
            this.groupBox1.Controls.Add(this.labelSourceConnectionString);
            this.groupBox1.Location = new System.Drawing.Point(11, 35);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(506, 159);
            this.groupBox1.TabIndex = 37;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "EXPORT";
            // 
            // cbMinJson
            // 
            this.cbMinJson.AutoSize = true;
            this.cbMinJson.Location = new System.Drawing.Point(192, 102);
            this.cbMinJson.Margin = new System.Windows.Forms.Padding(2);
            this.cbMinJson.Name = "cbMinJson";
            this.cbMinJson.Size = new System.Drawing.Size(97, 17);
            this.cbMinJson.TabIndex = 37;
            this.cbMinJson.Text = "Minimize JSON";
            this.cbMinJson.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 52);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(137, 13);
            this.label7.TabIndex = 34;
            this.label7.Text = "Export Config File (Optional)";
            // 
            // btExportConfigFile
            // 
            this.btExportConfigFile.Location = new System.Drawing.Point(465, 49);
            this.btExportConfigFile.Margin = new System.Windows.Forms.Padding(2);
            this.btExportConfigFile.Name = "btExportConfigFile";
            this.btExportConfigFile.Size = new System.Drawing.Size(26, 19);
            this.btExportConfigFile.TabIndex = 36;
            this.btExportConfigFile.Text = "...";
            this.btExportConfigFile.UseVisualStyleBackColor = true;
            this.btExportConfigFile.Click += new System.EventHandler(this.btExportConfigFile_Click);
            // 
            // tbExportConfigFile
            // 
            this.tbExportConfigFile.Location = new System.Drawing.Point(173, 48);
            this.tbExportConfigFile.Name = "tbExportConfigFile";
            this.tbExportConfigFile.Size = new System.Drawing.Size(285, 20);
            this.tbExportConfigFile.TabIndex = 35;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.nudMaxThreads);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.nudSavePageSize);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.ConnectionString);
            this.groupBox2.Controls.Add(this.buttonTargetConnectionString);
            this.groupBox2.Controls.Add(this.cbIgnoreStatuses);
            this.groupBox2.Controls.Add(this.labelTargetConnectionString);
            this.groupBox2.Controls.Add(this.btLoadImportConfigFile);
            this.groupBox2.Controls.Add(this.cbIgnoreSystemFields);
            this.groupBox2.Controls.Add(this.tbImportConfigFile);
            this.groupBox2.Location = new System.Drawing.Point(529, 35);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(507, 159);
            this.groupBox2.TabIndex = 38;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "IMPORT";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(327, 82);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 13);
            this.label6.TabIndex = 37;
            this.label6.Text = "Max Threads";
            // 
            // nudMaxThreads
            // 
            this.nudMaxThreads.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudMaxThreads.Location = new System.Drawing.Point(402, 80);
            this.nudMaxThreads.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.nudMaxThreads.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMaxThreads.Name = "nudMaxThreads";
            this.nudMaxThreads.Size = new System.Drawing.Size(94, 20);
            this.nudMaxThreads.TabIndex = 38;
            this.nudMaxThreads.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(63, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 13);
            this.label5.TabIndex = 35;
            this.label5.Text = "Save Page Size";
            // 
            // nudSavePageSize
            // 
            this.nudSavePageSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudSavePageSize.Increment = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nudSavePageSize.Location = new System.Drawing.Point(168, 79);
            this.nudSavePageSize.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudSavePageSize.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nudSavePageSize.Name = "nudSavePageSize";
            this.nudSavePageSize.Size = new System.Drawing.Size(94, 20);
            this.nudSavePageSize.TabIndex = 36;
            this.nudSavePageSize.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageDataMigration);
            this.tabControl.Controls.Add(this.tabPageSchema);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.MinimumSize = new System.Drawing.Size(1000, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1050, 600);
            this.tabControl.TabIndex = 40;
            this.tabControl.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl_Selected);
            // 
            // tabPageDataMigration
            // 
            this.tabPageDataMigration.Controls.Add(this.toolStrip3);
            this.tabPageDataMigration.Controls.Add(this.label4);
            this.tabPageDataMigration.Controls.Add(this.comboBox1);
            this.tabPageDataMigration.Controls.Add(this.tbMessage);
            this.tabPageDataMigration.Controls.Add(this.btClearLog);
            this.tabPageDataMigration.Controls.Add(this.groupBox1);
            this.tabPageDataMigration.Controls.Add(this.groupBox2);
            this.tabPageDataMigration.Controls.Add(this.tbFolderPath);
            this.tabPageDataMigration.Controls.Add(this.labelJsonFilePath);
            this.tabPageDataMigration.Controls.Add(this.btFolderPath);
            this.tabPageDataMigration.Location = new System.Drawing.Point(4, 22);
            this.tabPageDataMigration.Name = "tabPageDataMigration";
            this.tabPageDataMigration.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDataMigration.Size = new System.Drawing.Size(1042, 574);
            this.tabPageDataMigration.TabIndex = 0;
            this.tabPageDataMigration.Text = "Data Migration";
            this.tabPageDataMigration.UseVisualStyleBackColor = true;
            // 
            // toolStrip3
            // 
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton2,
            this.toolStripSeparator2,
            this.btExport,
            this.toolStripSeparator7,
            this.btImport,
            this.toolStripSeparator8});
            this.toolStrip3.Location = new System.Drawing.Point(3, 3);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(1036, 25);
            this.toolStrip3.TabIndex = 42;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Properties.Resources.Close;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "Close";
            this.toolStripButton2.Click += new System.EventHandler(this.tsbCloseThisTab_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btExport
            // 
            this.btExport.Image = global::Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Properties.Resources.export;
            this.btExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btExport.Name = "btExport";
            this.btExport.Size = new System.Drawing.Size(60, 22);
            this.btExport.Text = "Export";
            this.btExport.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // btImport
            // 
            this.btImport.Image = global::Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Properties.Resources.Import;
            this.btImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btImport.Name = "btImport";
            this.btImport.Size = new System.Drawing.Size(63, 22);
            this.btImport.Text = "Import";
            this.btImport.Click += new System.EventHandler(this.importButton_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(653, 206);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 41;
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
            this.comboBox1.Location = new System.Drawing.Point(727, 201);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(97, 21);
            this.comboBox1.TabIndex = 40;
            this.comboBox1.Text = "Info";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // btClearLog
            // 
            this.btClearLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btClearLog.Location = new System.Drawing.Point(861, 200);
            this.btClearLog.Margin = new System.Windows.Forms.Padding(2);
            this.btClearLog.Name = "btClearLog";
            this.btClearLog.Size = new System.Drawing.Size(90, 23);
            this.btClearLog.TabIndex = 39;
            this.btClearLog.Text = "Clear Log";
            this.btClearLog.UseVisualStyleBackColor = true;
            this.btClearLog.Click += new System.EventHandler(this.btClearLog_Click);
            // 
            // tabPageSchema
            // 
            this.tabPageSchema.Controls.Add(this.gbRelationship);
            this.tabPageSchema.Controls.Add(this.gbEnvironments);
            this.tabPageSchema.Controls.Add(this.gbAttributes);
            this.tabPageSchema.Controls.Add(this.gbEntities);
            this.tabPageSchema.Controls.Add(this.toolStrip2);
            this.tabPageSchema.Location = new System.Drawing.Point(4, 22);
            this.tabPageSchema.Name = "tabPageSchema";
            this.tabPageSchema.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSchema.Size = new System.Drawing.Size(1042, 574);
            this.tabPageSchema.TabIndex = 1;
            this.tabPageSchema.Text = "Schema";
            this.tabPageSchema.UseVisualStyleBackColor = true;
            // 
            // gbRelationship
            // 
            this.gbRelationship.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbRelationship.Controls.Add(this.chkAllRelationships);
            this.gbRelationship.Controls.Add(this.lvRelationship);
            this.gbRelationship.Enabled = false;
            this.gbRelationship.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.gbRelationship.Location = new System.Drawing.Point(363, 443);
            this.gbRelationship.Name = "gbRelationship";
            this.gbRelationship.Size = new System.Drawing.Size(677, 136);
            this.gbRelationship.TabIndex = 103;
            this.gbRelationship.TabStop = false;
            this.gbRelationship.Text = "Available many to many relationships";
            // 
            // chkAllRelationships
            // 
            this.chkAllRelationships.AutoSize = true;
            this.chkAllRelationships.Checked = true;
            this.chkAllRelationships.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAllRelationships.Location = new System.Drawing.Point(6, 21);
            this.chkAllRelationships.Name = "chkAllRelationships";
            this.chkAllRelationships.Size = new System.Drawing.Size(120, 17);
            this.chkAllRelationships.TabIndex = 3;
            this.chkAllRelationships.Text = "Select/Unselect All";
            this.chkAllRelationships.UseVisualStyleBackColor = true;
            this.chkAllRelationships.CheckedChanged += new System.EventHandler(this.chkAllRelationships_CheckedChanged);
            // 
            // lvRelationship
            // 
            this.lvRelationship.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvRelationship.CheckBoxes = true;
            this.lvRelationship.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lvRelationship.FullRowSelect = true;
            this.lvRelationship.HideSelection = false;
            this.lvRelationship.Location = new System.Drawing.Point(6, 38);
            this.lvRelationship.Name = "lvRelationship";
            this.lvRelationship.Size = new System.Drawing.Size(665, 91);
            this.lvRelationship.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvRelationship.TabIndex = 64;
            this.lvRelationship.UseCompatibleStateImageBehavior = false;
            this.lvRelationship.View = System.Windows.Forms.View.Details;
            this.lvRelationship.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvRelationship_ItemCheck);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Relationship name";
            this.columnHeader1.Width = 154;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Related Entity Name";
            this.columnHeader2.Width = 129;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Target Entity";
            this.columnHeader3.Width = 138;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Target Entity Primary Key";
            this.columnHeader4.Width = 200;
            // 
            // gbEnvironments
            // 
            this.gbEnvironments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbEnvironments.Controls.Add(this.cbShowSystemAttributes);
            this.gbEnvironments.Controls.Add(this.btExportConfigPath);
            this.gbEnvironments.Controls.Add(this.btImportConfigPath);
            this.gbEnvironments.Controls.Add(this.tbExportConfig);
            this.gbEnvironments.Controls.Add(this.tbImportConfig);
            this.gbEnvironments.Controls.Add(this.label10);
            this.gbEnvironments.Controls.Add(this.label9);
            this.gbEnvironments.Controls.Add(this.tbSchemaPath);
            this.gbEnvironments.Controls.Add(this.btSchemaFolderPath);
            this.gbEnvironments.Controls.Add(this.label8);
            this.gbEnvironments.Controls.Add(this.lbSchema);
            this.gbEnvironments.Controls.Add(this.labelSchemaConnectionString);
            this.gbEnvironments.Location = new System.Drawing.Point(17, 47);
            this.gbEnvironments.Name = "gbEnvironments";
            this.gbEnvironments.Size = new System.Drawing.Size(1021, 112);
            this.gbEnvironments.TabIndex = 102;
            this.gbEnvironments.TabStop = false;
            this.gbEnvironments.Text = "Environments";
            this.gbEnvironments.Enter += new System.EventHandler(this.gbEnvironments_Enter);
            // 
            // cbShowSystemAttributes
            // 
            this.cbShowSystemAttributes.AutoSize = true;
            this.cbShowSystemAttributes.Location = new System.Drawing.Point(17, 55);
            this.cbShowSystemAttributes.Name = "cbShowSystemAttributes";
            this.cbShowSystemAttributes.Size = new System.Drawing.Size(137, 17);
            this.cbShowSystemAttributes.TabIndex = 37;
            this.cbShowSystemAttributes.Text = "Show System Attributes";
            this.cbShowSystemAttributes.UseVisualStyleBackColor = true;
            // 
            // btExportConfigPath
            // 
            this.btExportConfigPath.Enabled = false;
            this.btExportConfigPath.Location = new System.Drawing.Point(906, 47);
            this.btExportConfigPath.Name = "btExportConfigPath";
            this.btExportConfigPath.Size = new System.Drawing.Size(26, 24);
            this.btExportConfigPath.TabIndex = 36;
            this.btExportConfigPath.Text = "...";
            this.btExportConfigPath.UseVisualStyleBackColor = true;
            this.btExportConfigPath.Click += new System.EventHandler(this.btExportConfigPath_Click);
            // 
            // btImportConfigPath
            // 
            this.btImportConfigPath.Enabled = false;
            this.btImportConfigPath.Location = new System.Drawing.Point(906, 74);
            this.btImportConfigPath.Name = "btImportConfigPath";
            this.btImportConfigPath.Size = new System.Drawing.Size(26, 24);
            this.btImportConfigPath.TabIndex = 35;
            this.btImportConfigPath.Text = "...";
            this.btImportConfigPath.UseVisualStyleBackColor = true;
            this.btImportConfigPath.Click += new System.EventHandler(this.btImportConfigPath_Click);
            // 
            // tbExportConfig
            // 
            this.tbExportConfig.Enabled = false;
            this.tbExportConfig.Location = new System.Drawing.Point(533, 48);
            this.tbExportConfig.Name = "tbExportConfig";
            this.tbExportConfig.Size = new System.Drawing.Size(367, 20);
            this.tbExportConfig.TabIndex = 34;
            // 
            // tbImportConfig
            // 
            this.tbImportConfig.Enabled = false;
            this.tbImportConfig.Location = new System.Drawing.Point(533, 74);
            this.tbImportConfig.Name = "tbImportConfig";
            this.tbImportConfig.Size = new System.Drawing.Size(367, 20);
            this.tbImportConfig.TabIndex = 33;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(425, 49);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(70, 13);
            this.label10.TabIndex = 32;
            this.label10.Text = "Export Config";
            this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(425, 77);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 13);
            this.label9.TabIndex = 31;
            this.label9.Text = "Import Config";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tbSchemaPath
            // 
            this.tbSchemaPath.Enabled = false;
            this.tbSchemaPath.Location = new System.Drawing.Point(533, 22);
            this.tbSchemaPath.Name = "tbSchemaPath";
            this.tbSchemaPath.Size = new System.Drawing.Size(367, 20);
            this.tbSchemaPath.TabIndex = 30;
            // 
            // btSchemaFolderPath
            // 
            this.btSchemaFolderPath.Enabled = false;
            this.btSchemaFolderPath.Location = new System.Drawing.Point(906, 20);
            this.btSchemaFolderPath.Name = "btSchemaFolderPath";
            this.btSchemaFolderPath.Size = new System.Drawing.Size(26, 24);
            this.btSchemaFolderPath.TabIndex = 29;
            this.btSchemaFolderPath.Text = "...";
            this.btSchemaFolderPath.UseVisualStyleBackColor = true;
            this.btSchemaFolderPath.Click += new System.EventHandler(this.btSchemaFolderPath_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(423, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(90, 13);
            this.label8.TabIndex = 27;
            this.label8.Text = "Schema File Path";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lbSchema
            // 
            this.lbSchema.AutoSize = true;
            this.lbSchema.Location = new System.Drawing.Point(14, 25);
            this.lbSchema.Name = "lbSchema";
            this.lbSchema.Size = new System.Drawing.Size(133, 13);
            this.lbSchema.TabIndex = 24;
            this.lbSchema.Text = "Schema Connection String";
            // 
            // labelSchemaConnectionString
            // 
            this.labelSchemaConnectionString.AutoSize = true;
            this.labelSchemaConnectionString.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSchemaConnectionString.ForeColor = System.Drawing.Color.Green;
            this.labelSchemaConnectionString.Location = new System.Drawing.Point(153, 27);
            this.labelSchemaConnectionString.MinimumSize = new System.Drawing.Size(170, 0);
            this.labelSchemaConnectionString.Name = "labelSchemaConnectionString";
            this.labelSchemaConnectionString.Size = new System.Drawing.Size(170, 13);
            this.labelSchemaConnectionString.TabIndex = 26;
            // 
            // gbAttributes
            // 
            this.gbAttributes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbAttributes.Controls.Add(this.chkAllAttributes);
            this.gbAttributes.Controls.Add(this.lvAttributes);
            this.gbAttributes.Enabled = false;
            this.gbAttributes.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.gbAttributes.Location = new System.Drawing.Point(363, 165);
            this.gbAttributes.Name = "gbAttributes";
            this.gbAttributes.Size = new System.Drawing.Size(681, 272);
            this.gbAttributes.TabIndex = 96;
            this.gbAttributes.TabStop = false;
            this.gbAttributes.Text = "Available attributes";
            // 
            // chkAllAttributes
            // 
            this.chkAllAttributes.AutoSize = true;
            this.chkAllAttributes.Checked = true;
            this.chkAllAttributes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAllAttributes.Location = new System.Drawing.Point(6, 21);
            this.chkAllAttributes.Name = "chkAllAttributes";
            this.chkAllAttributes.Size = new System.Drawing.Size(120, 17);
            this.chkAllAttributes.TabIndex = 3;
            this.chkAllAttributes.Text = "Select/Unselect All";
            this.chkAllAttributes.UseVisualStyleBackColor = true;
            this.chkAllAttributes.CheckedChanged += new System.EventHandler(this.chkAllAttributes_CheckedChanged);
            // 
            // lvAttributes
            // 
            this.lvAttributes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvAttributes.CheckBoxes = true;
            this.lvAttributes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clAttDisplayName,
            this.clAttLogicalName,
            this.clAttType,
            this.clAttComment});
            this.lvAttributes.FullRowSelect = true;
            this.lvAttributes.HideSelection = false;
            this.lvAttributes.Location = new System.Drawing.Point(6, 38);
            this.lvAttributes.Name = "lvAttributes";
            this.lvAttributes.Size = new System.Drawing.Size(669, 228);
            this.lvAttributes.TabIndex = 64;
            this.lvAttributes.UseCompatibleStateImageBehavior = false;
            this.lvAttributes.View = System.Windows.Forms.View.Details;
            this.lvAttributes.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvAttributes_ColumnClick);
            this.lvAttributes.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvAttributes_ItemCheck);
            // 
            // clAttDisplayName
            // 
            this.clAttDisplayName.Text = "Display Name";
            this.clAttDisplayName.Width = 154;
            // 
            // clAttLogicalName
            // 
            this.clAttLogicalName.Text = "Logical Name";
            this.clAttLogicalName.Width = 129;
            // 
            // clAttType
            // 
            this.clAttType.Text = "Type";
            this.clAttType.Width = 138;
            // 
            // clAttComment
            // 
            this.clAttComment.Text = "Comment";
            this.clAttComment.Width = 200;
            // 
            // gbEntities
            // 
            this.gbEntities.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gbEntities.Controls.Add(this.chkAllEntities);
            this.gbEntities.Controls.Add(this.lvEntities);
            this.gbEntities.Enabled = false;
            this.gbEntities.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.gbEntities.Location = new System.Drawing.Point(14, 164);
            this.gbEntities.Name = "gbEntities";
            this.gbEntities.Size = new System.Drawing.Size(343, 415);
            this.gbEntities.TabIndex = 95;
            this.gbEntities.TabStop = false;
            this.gbEntities.Text = "Available entities";
            // 
            // chkAllEntities
            // 
            this.chkAllEntities.AutoSize = true;
            this.chkAllEntities.Checked = true;
            this.chkAllEntities.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAllEntities.Location = new System.Drawing.Point(6, 21);
            this.chkAllEntities.Name = "chkAllEntities";
            this.chkAllEntities.Size = new System.Drawing.Size(120, 17);
            this.chkAllEntities.TabIndex = 65;
            this.chkAllEntities.Text = "Select/Unselect All";
            this.chkAllEntities.UseVisualStyleBackColor = true;
            this.chkAllEntities.CheckedChanged += new System.EventHandler(this.chkAllEntities_CheckedChanged);
            // 
            // lvEntities
            // 
            this.lvEntities.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvEntities.CheckBoxes = true;
            this.lvEntities.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clEntDisplayName,
            this.clEntLogicalName});
            this.lvEntities.FullRowSelect = true;
            this.lvEntities.HideSelection = false;
            this.lvEntities.Location = new System.Drawing.Point(6, 38);
            this.lvEntities.MultiSelect = false;
            this.lvEntities.Name = "lvEntities";
            this.lvEntities.Size = new System.Drawing.Size(331, 370);
            this.lvEntities.TabIndex = 64;
            this.lvEntities.UseCompatibleStateImageBehavior = false;
            this.lvEntities.View = System.Windows.Forms.View.Details;
            this.lvEntities.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvEntities_ItemCheck);
            this.lvEntities.SelectedIndexChanged += new System.EventHandler(this.lvEntities_SelectedIndexChanged);
            // 
            // clEntDisplayName
            // 
            this.clEntDisplayName.Text = "Display Name";
            this.clEntDisplayName.Width = 162;
            // 
            // clEntLogicalName
            // 
            this.clEntLogicalName.Text = "Logical Name";
            this.clEntLogicalName.Width = 187;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbCloseThisTab,
            this.toolStripSeparator1,
            this.tsbtRetrieveEntities,
            this.toolStripSeparator5,
            this.tsbtMappings,
            this.toolStripSeparator4,
            this.tsbtFilters,
            this.toolStripSeparator3,
            this.toolStripDropDownButton1,
            this.toolStripSeparator6,
            this.toolStripDropDownButton2});
            this.toolStrip2.Location = new System.Drawing.Point(3, 3);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(1036, 25);
            this.toolStrip2.TabIndex = 94;
            this.toolStrip2.Text = "toolStrip1";
            // 
            // tsbCloseThisTab
            // 
            this.tsbCloseThisTab.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCloseThisTab.Image = global::Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Properties.Resources.Close;
            this.tsbCloseThisTab.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCloseThisTab.Name = "tsbCloseThisTab";
            this.tsbCloseThisTab.Size = new System.Drawing.Size(23, 22);
            this.tsbCloseThisTab.Text = "Close";
            this.tsbCloseThisTab.Click += new System.EventHandler(this.tsbCloseThisTab_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbtRetrieveEntities
            // 
            this.tsbtRetrieveEntities.Image = global::Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Properties.Resources.entities;
            this.tsbtRetrieveEntities.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtRetrieveEntities.Name = "tsbtRetrieveEntities";
            this.tsbtRetrieveEntities.Size = new System.Drawing.Size(107, 22);
            this.tsbtRetrieveEntities.Text = "Refresh Entities";
            this.tsbtRetrieveEntities.Click += new System.EventHandler(this.tsbtRetrieveEntities_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbtMappings
            // 
            this.tsbtMappings.Image = global::Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Properties.Resources.Mapping;
            this.tsbtMappings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtMappings.Name = "tsbtMappings";
            this.tsbtMappings.Size = new System.Drawing.Size(80, 22);
            this.tsbtMappings.Text = "Mappings";
            this.tsbtMappings.Click += new System.EventHandler(this.tsbtMappings_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbtFilters
            // 
            this.tsbtFilters.Image = global::Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Properties.Resources.Filter;
            this.tsbtFilters.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtFilters.Name = "tsbtFilters";
            this.tsbtFilters.Size = new System.Drawing.Size(58, 22);
            this.tsbtFilters.Text = "Filters";
            this.tsbtFilters.Click += new System.EventHandler(this.tsbtFilters_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveSchemaToolStripMenuItem,
            this.saveFiltersToolStripMenuItem,
            this.saveMappingsToolStripMenuItem,
            this.saveAllToolStripMenuItem});
            this.toolStripDropDownButton1.Image = global::Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Properties.Resources.Save;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(60, 22);
            this.toolStripDropDownButton1.Text = "Save";
            // 
            // saveSchemaToolStripMenuItem
            // 
            this.saveSchemaToolStripMenuItem.Image = global::Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Properties.Resources.Save;
            this.saveSchemaToolStripMenuItem.Name = "saveSchemaToolStripMenuItem";
            this.saveSchemaToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.saveSchemaToolStripMenuItem.Text = "Save Schema";
            this.saveSchemaToolStripMenuItem.Click += new System.EventHandler(this.tbSaveSchema_Click);
            // 
            // saveFiltersToolStripMenuItem
            // 
            this.saveFiltersToolStripMenuItem.Image = global::Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Properties.Resources.Save;
            this.saveFiltersToolStripMenuItem.Name = "saveFiltersToolStripMenuItem";
            this.saveFiltersToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.saveFiltersToolStripMenuItem.Text = "Save Export Config";
            this.saveFiltersToolStripMenuItem.Click += new System.EventHandler(this.tbSaveFilters_Click);
            // 
            // saveMappingsToolStripMenuItem
            // 
            this.saveMappingsToolStripMenuItem.Image = global::Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Properties.Resources.Save;
            this.saveMappingsToolStripMenuItem.Name = "saveMappingsToolStripMenuItem";
            this.saveMappingsToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.saveMappingsToolStripMenuItem.Text = "Save Import Config";
            this.saveMappingsToolStripMenuItem.Click += new System.EventHandler(this.tbSaveMappings_Click);
            // 
            // saveAllToolStripMenuItem
            // 
            this.saveAllToolStripMenuItem.Image = global::Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Properties.Resources.Save;
            this.saveAllToolStripMenuItem.Name = "saveAllToolStripMenuItem";
            this.saveAllToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.saveAllToolStripMenuItem.Text = "Save All";
            this.saveAllToolStripMenuItem.Click += new System.EventHandler(this.saveAllToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadSchemaToolStripMenuItem,
            this.loadFiltersToolStripMenuItem,
            this.loadMappingsToolStripMenuItem,
            this.loadAllToolStripMenuItem});
            this.toolStripDropDownButton2.Image = global::Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Properties.Resources.CRM;
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(62, 22);
            this.toolStripDropDownButton2.Text = "Load";
            // 
            // loadSchemaToolStripMenuItem
            // 
            this.loadSchemaToolStripMenuItem.Image = global::Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Properties.Resources.CRM;
            this.loadSchemaToolStripMenuItem.Name = "loadSchemaToolStripMenuItem";
            this.loadSchemaToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.loadSchemaToolStripMenuItem.Text = "Load Schema";
            this.loadSchemaToolStripMenuItem.Click += new System.EventHandler(this.tbLoadSchemaFile_Click);
            // 
            // loadFiltersToolStripMenuItem
            // 
            this.loadFiltersToolStripMenuItem.Image = global::Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Properties.Resources.CRM;
            this.loadFiltersToolStripMenuItem.Name = "loadFiltersToolStripMenuItem";
            this.loadFiltersToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.loadFiltersToolStripMenuItem.Text = "Load Filters";
            this.loadFiltersToolStripMenuItem.Click += new System.EventHandler(this.tbLoadFiltersFile_Click);
            // 
            // loadMappingsToolStripMenuItem
            // 
            this.loadMappingsToolStripMenuItem.Image = global::Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Properties.Resources.CRM;
            this.loadMappingsToolStripMenuItem.Name = "loadMappingsToolStripMenuItem";
            this.loadMappingsToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.loadMappingsToolStripMenuItem.Text = "Load Mappings";
            this.loadMappingsToolStripMenuItem.Click += new System.EventHandler(this.tbLoadMappingsFile_Click);
            // 
            // loadAllToolStripMenuItem
            // 
            this.loadAllToolStripMenuItem.Image = global::Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Properties.Resources.CRM;
            this.loadAllToolStripMenuItem.Name = "loadAllToolStripMenuItem";
            this.loadAllToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.loadAllToolStripMenuItem.Text = "Load All";
            this.loadAllToolStripMenuItem.Click += new System.EventHandler(this.loadAllToolStripMenuItem_Click);
            // 
            // PluginControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.StatusLabel);
            this.MinimumSize = new System.Drawing.Size(1050, 600);
            this.Name = "PluginControl";
            this.Size = new System.Drawing.Size(1050, 600);
            ((System.ComponentModel.ISupportInitialize)(this.topCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.batchSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pageSize)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxThreads)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSavePageSize)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabPageDataMigration.ResumeLayout(false);
            this.tabPageDataMigration.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.tabPageSchema.ResumeLayout(false);
            this.tabPageSchema.PerformLayout();
            this.gbRelationship.ResumeLayout(false);
            this.gbRelationship.PerformLayout();
            this.gbEnvironments.ResumeLayout(false);
            this.gbEnvironments.PerformLayout();
            this.gbAttributes.ResumeLayout(false);
            this.gbAttributes.PerformLayout();
            this.gbEntities.ResumeLayout(false);
            this.gbEntities.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label BatchSizeLabel;
        private System.Windows.Forms.Label PageSizeLabel;
        private System.Windows.Forms.Label TopCountLabel;
        private System.Windows.Forms.NumericUpDown topCount;
        private System.Windows.Forms.NumericUpDown batchSize;
        private System.Windows.Forms.NumericUpDown pageSize;
        private System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbSchemaFilePath;
        private System.Windows.Forms.Label ConnectionString;
        private System.Windows.Forms.TextBox tbFolderPath;
        private System.Windows.Forms.Label labelJsonFilePath;
        private System.Windows.Forms.Button buttonTargetConnectionString;
        private System.Windows.Forms.Label labelTargetConnectionString;
        private System.Windows.Forms.Label labelSourceConnectionString;
        private System.Windows.Forms.Button buttonSourceConnectionString;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btSeFile;
        private System.Windows.Forms.Button btFolderPath;
        private System.Windows.Forms.FolderBrowserDialog fbExportPath;
        private System.Windows.Forms.FolderBrowserDialog fbSchemaPath;
        private System.Windows.Forms.OpenFileDialog fdSchemaFile;
        private System.Windows.Forms.TextBox tbMessage;
        private System.Windows.Forms.CheckBox cbOnlyActive;
        private System.Windows.Forms.CheckBox cbIgnoreSystemFields;
        private System.Windows.Forms.Button btLoadImportConfigFile;
        private System.Windows.Forms.TextBox tbImportConfigFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbIgnoreStatuses;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudSavePageSize;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nudMaxThreads;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageDataMigration;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button btClearLog;
        private System.Windows.Forms.TabPage tabPageSchema;
        private System.Windows.Forms.GroupBox gbAttributes;
        private System.Windows.Forms.CheckBox chkAllAttributes;
        private System.Windows.Forms.ListView lvAttributes;
        private System.Windows.Forms.ColumnHeader clAttDisplayName;
        private System.Windows.Forms.ColumnHeader clAttLogicalName;
        private System.Windows.Forms.ColumnHeader clAttType;
        private System.Windows.Forms.ColumnHeader clAttComment;
        private System.Windows.Forms.GroupBox gbEntities;
        private System.Windows.Forms.ListView lvEntities;
        private System.Windows.Forms.ColumnHeader clEntDisplayName;
        private System.Windows.Forms.ColumnHeader clEntLogicalName;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton tsbtRetrieveEntities;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tsbtMappings;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tsbtFilters;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripButton btExport;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton btImport;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbCloseThisTab;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.GroupBox gbEnvironments;
        private System.Windows.Forms.Label lbSchema;
        private System.Windows.Forms.Label labelSchemaConnectionString;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.CheckBox chkAllEntities;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btExportConfigFile;
        private System.Windows.Forms.TextBox tbExportConfigFile;
        private System.Windows.Forms.CheckBox cbMinJson;
        private System.Windows.Forms.TextBox tbSchemaPath;
        private System.Windows.Forms.Button btSchemaFolderPath;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox gbRelationship;
        private System.Windows.Forms.CheckBox chkAllRelationships;
        private System.Windows.Forms.ListView lvRelationship;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button btExportConfigPath;
        private System.Windows.Forms.Button btImportConfigPath;
        private System.Windows.Forms.TextBox tbExportConfig;
        private System.Windows.Forms.TextBox tbImportConfig;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem saveSchemaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveFiltersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveMappingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;
        private System.Windows.Forms.ToolStripMenuItem loadSchemaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadFiltersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadMappingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadAllToolStripMenuItem;
        private System.Windows.Forms.CheckBox cbShowSystemAttributes;
    }
}
