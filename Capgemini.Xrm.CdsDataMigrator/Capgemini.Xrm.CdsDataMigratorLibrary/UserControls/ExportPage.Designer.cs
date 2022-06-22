namespace Capgemini.Xrm.CdsDataMigratorLibrary.UserControls
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportPage));
            this.gbxFetchSettings = new System.Windows.Forms.GroupBox();
            this.tlpFetchSettings = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.nbxPageSize = new System.Windows.Forms.NumericUpDown();
            this.nbxTopCount = new System.Windows.Forms.NumericUpDown();
            this.fisSchemaFile = new Capgemini.Xrm.CdsDataMigratorLibrary.UserControls.FileInputSelector();
            this.label7 = new System.Windows.Forms.Label();
            this.dataverseEnvironmentSelector1 = new Capgemini.Xrm.CdsDataMigratorLibrary.UserControls.DataverseEnvironmentSelector();
            this.gbxWriteSettings = new System.Windows.Forms.GroupBox();
            this.tlpWriteSettings = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.rbnDataFormatJson = new System.Windows.Forms.RadioButton();
            this.rbnDataFormatCsv = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.nbxBatchSize = new System.Windows.Forms.NumericUpDown();
            this.fisOutputDirectory = new Capgemini.Xrm.CdsDataMigratorLibrary.UserControls.FolderInputSelector();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbLoad = new System.Windows.Forms.ToolStripButton();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.tsbRun = new System.Windows.Forms.ToolStripButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.tlpMainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.gbxFetchSettings.SuspendLayout();
            this.tlpFetchSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nbxPageSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nbxTopCount)).BeginInit();
            this.gbxWriteSettings.SuspendLayout();
            this.tlpWriteSettings.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
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
            this.gbxFetchSettings.Padding = new System.Windows.Forms.Padding(10, 10, 10, 10);
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
            this.tlpFetchSettings.Controls.Add(this.label1, 0, 1);
            this.tlpFetchSettings.Controls.Add(this.label2, 0, 2);
            this.tlpFetchSettings.Controls.Add(this.label3, 0, 3);
            this.tlpFetchSettings.Controls.Add(this.nbxPageSize, 1, 2);
            this.tlpFetchSettings.Controls.Add(this.nbxTopCount, 1, 3);
            this.tlpFetchSettings.Controls.Add(this.fisSchemaFile, 1, 1);
            this.tlpFetchSettings.Controls.Add(this.label7, 0, 0);
            this.tlpFetchSettings.Controls.Add(this.dataverseEnvironmentSelector1, 1, 0);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 33);
            this.label1.TabIndex = 0;
            this.label1.Text = "Schema File";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 26);
            this.label2.TabIndex = 1;
            this.label2.Text = "Page Size";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 26);
            this.label3.TabIndex = 2;
            this.label3.Text = "Top Count";
            // 
            // nbxPageSize
            // 
            this.nbxPageSize.Location = new System.Drawing.Point(134, 67);
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
            this.nbxTopCount.Location = new System.Drawing.Point(134, 93);
            this.nbxTopCount.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.nbxTopCount.Name = "nbxTopCount";
            this.nbxTopCount.Size = new System.Drawing.Size(96, 20);
            this.nbxTopCount.TabIndex = 4;
            // 
            // fisSchemaFile
            // 
            this.fisSchemaFile.AutoSize = true;
            this.fisSchemaFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fisSchemaFile.Location = new System.Drawing.Point(135, 35);
            this.fisSchemaFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.fisSchemaFile.MinimumSize = new System.Drawing.Size(100, 25);
            this.fisSchemaFile.Name = "fisSchemaFile";
            this.fisSchemaFile.Size = new System.Drawing.Size(661, 25);
            this.fisSchemaFile.TabIndex = 5;
            this.fisSchemaFile.Value = "";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(3, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(125, 31);
            this.label7.TabIndex = 7;
            this.label7.Text = "Environment";
            // 
            // dataverseEnvironmentSelector1
            // 
            this.dataverseEnvironmentSelector1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataverseEnvironmentSelector1.Location = new System.Drawing.Point(135, 4);
            this.dataverseEnvironmentSelector1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataverseEnvironmentSelector1.Name = "dataverseEnvironmentSelector1";
            this.dataverseEnvironmentSelector1.Size = new System.Drawing.Size(661, 23);
            this.dataverseEnvironmentSelector1.TabIndex = 8;
            // 
            // gbxWriteSettings
            // 
            this.gbxWriteSettings.Controls.Add(this.tlpWriteSettings);
            this.gbxWriteSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxWriteSettings.Location = new System.Drawing.Point(8, 208);
            this.gbxWriteSettings.Name = "gbxWriteSettings";
            this.gbxWriteSettings.Padding = new System.Windows.Forms.Padding(10, 10, 10, 10);
            this.gbxWriteSettings.Size = new System.Drawing.Size(837, 195);
            this.gbxWriteSettings.TabIndex = 1;
            this.gbxWriteSettings.TabStop = false;
            this.gbxWriteSettings.Text = "Write settings (to files)";
            // 
            // tlpWriteSettings
            // 
            this.tlpWriteSettings.AutoScroll = true;
            this.tlpWriteSettings.ColumnCount = 2;
            this.tlpWriteSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.21937F));
            this.tlpWriteSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 83.78063F));
            this.tlpWriteSettings.Controls.Add(this.label4, 0, 0);
            this.tlpWriteSettings.Controls.Add(this.label5, 0, 1);
            this.tlpWriteSettings.Controls.Add(this.flowLayoutPanel1, 1, 0);
            this.tlpWriteSettings.Controls.Add(this.label6, 0, 3);
            this.tlpWriteSettings.Controls.Add(this.nbxBatchSize, 1, 3);
            this.tlpWriteSettings.Controls.Add(this.fisOutputDirectory, 1, 1);
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
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(126, 29);
            this.label4.TabIndex = 0;
            this.label4.Text = "File Type";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(3, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(126, 33);
            this.label5.TabIndex = 1;
            this.label5.Text = "Output Directory";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.rbnDataFormatJson);
            this.flowLayoutPanel1.Controls.Add(this.rbnDataFormatCsv);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(135, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(679, 23);
            this.flowLayoutPanel1.TabIndex = 3;
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
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(3, 62);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(126, 26);
            this.label6.TabIndex = 5;
            this.label6.Text = "Batch Size";
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
            // fisOutputDirectory
            // 
            this.fisOutputDirectory.AutoSize = true;
            this.fisOutputDirectory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fisOutputDirectory.Location = new System.Drawing.Point(136, 33);
            this.fisOutputDirectory.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.fisOutputDirectory.MinimumSize = new System.Drawing.Size(100, 25);
            this.fisOutputDirectory.Name = "fisOutputDirectory";
            this.fisOutputDirectory.Size = new System.Drawing.Size(677, 25);
            this.fisOutputDirectory.TabIndex = 7;
            this.fisOutputDirectory.Value = "";
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
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
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
            this.tlpWriteSettings.ResumeLayout(false);
            this.tlpWriteSettings.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nbxPageSize;
        private System.Windows.Forms.NumericUpDown nbxTopCount;
        private System.Windows.Forms.TableLayoutPanel tlpWriteSettings;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.RadioButton rbnDataFormatJson;
        private System.Windows.Forms.RadioButton rbnDataFormatCsv;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbLoad;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.ToolStripButton tsbRun;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nbxBatchSize;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private FileInputSelector fisSchemaFile;
        private FolderInputSelector fisOutputDirectory;
        private System.Windows.Forms.Label label7;
        private DataverseEnvironmentSelector dataverseEnvironmentSelector1;
        private System.Windows.Forms.TableLayoutPanel tlpMainLayout;
    }
}
