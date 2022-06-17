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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.nbxPageSize = new System.Windows.Forms.NumericUpDown();
            this.nbxTopCount = new System.Windows.Forms.NumericUpDown();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.gbxWriteSettings = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.nbxBatchSize = new System.Windows.Forms.NumericUpDown();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbLoad = new System.Windows.Forms.ToolStripButton();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.tsbRun = new System.Windows.Forms.ToolStripButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.gbxFetchSettings.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nbxPageSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nbxTopCount)).BeginInit();
            this.gbxWriteSettings.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nbxBatchSize)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxFetchSettings
            // 
            this.gbxFetchSettings.AutoSize = true;
            this.gbxFetchSettings.Controls.Add(this.tableLayoutPanel1);
            this.gbxFetchSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxFetchSettings.Location = new System.Drawing.Point(0, 27);
            this.gbxFetchSettings.Name = "gbxFetchSettings";
            this.gbxFetchSettings.Padding = new System.Windows.Forms.Padding(10);
            this.gbxFetchSettings.Size = new System.Drawing.Size(863, 201);
            this.gbxFetchSettings.TabIndex = 0;
            this.gbxFetchSettings.TabStop = false;
            this.gbxFetchSettings.Text = "Fetch settings (from Dataverse)";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.45274F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 83.54726F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.nbxPageSize, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.nbxTopCount, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBox1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(10, 23);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(843, 168);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Schema File";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 26);
            this.label2.TabIndex = 1;
            this.label2.Text = "Page Size";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(132, 26);
            this.label3.TabIndex = 2;
            this.label3.Text = "Top Count";
            // 
            // nbxPageSize
            // 
            this.nbxPageSize.Location = new System.Drawing.Point(141, 29);
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
            this.nbxPageSize.Size = new System.Drawing.Size(235, 20);
            this.nbxPageSize.TabIndex = 3;
            this.nbxPageSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nbxTopCount
            // 
            this.nbxTopCount.Location = new System.Drawing.Point(141, 55);
            this.nbxTopCount.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.nbxTopCount.Name = "nbxTopCount";
            this.nbxTopCount.Size = new System.Drawing.Size(235, 20);
            this.nbxTopCount.TabIndex = 4;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(141, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(349, 20);
            this.textBox1.TabIndex = 5;
            // 
            // gbxWriteSettings
            // 
            this.gbxWriteSettings.Controls.Add(this.tableLayoutPanel2);
            this.gbxWriteSettings.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gbxWriteSettings.Location = new System.Drawing.Point(0, 228);
            this.gbxWriteSettings.Name = "gbxWriteSettings";
            this.gbxWriteSettings.Padding = new System.Windows.Forms.Padding(10);
            this.gbxWriteSettings.Size = new System.Drawing.Size(863, 210);
            this.gbxWriteSettings.TabIndex = 1;
            this.gbxWriteSettings.TabStop = false;
            this.gbxWriteSettings.Text = "Write settings (to files)";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.21937F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 83.78063F));
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label5, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel1, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label6, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.nbxBatchSize, 1, 3);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(10, 23);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(843, 177);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(130, 29);
            this.label4.TabIndex = 0;
            this.label4.Text = "File Type";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(3, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(130, 25);
            this.label5.TabIndex = 1;
            this.label5.Text = "Output Directory";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.radioButton1);
            this.flowLayoutPanel1.Controls.Add(this.radioButton2);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(139, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(701, 23);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(3, 3);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(85, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "radioButton1";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(94, 3);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(85, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "radioButton2";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(139, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(701, 19);
            this.panel1.TabIndex = 4;
            // 
            // textBox2
            // 
            this.textBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox2.Location = new System.Drawing.Point(0, 0);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(654, 20);
            this.textBox2.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.Dock = System.Windows.Forms.DockStyle.Right;
            this.button1.Location = new System.Drawing.Point(654, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(47, 19);
            this.button1.TabIndex = 3;
            this.button1.Text = "Select";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(3, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(130, 26);
            this.label6.TabIndex = 5;
            this.label6.Text = "Batch Size";
            // 
            // nbxBatchSize
            // 
            this.nbxBatchSize.Dock = System.Windows.Forms.DockStyle.Left;
            this.nbxBatchSize.Location = new System.Drawing.Point(139, 57);
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
            this.nbxBatchSize.Size = new System.Drawing.Size(122, 20);
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
            this.toolStrip1.Size = new System.Drawing.Size(863, 27);
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
            // ExportPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbxFetchSettings);
            this.Controls.Add(this.gbxWriteSettings);
            this.Controls.Add(this.toolStrip1);
            this.Name = "ExportPage";
            this.Size = new System.Drawing.Size(863, 438);
            this.gbxFetchSettings.ResumeLayout(false);
            this.gbxFetchSettings.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nbxPageSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nbxTopCount)).EndInit();
            this.gbxWriteSettings.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nbxBatchSize)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxFetchSettings;
        private System.Windows.Forms.GroupBox gbxWriteSettings;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nbxPageSize;
        private System.Windows.Forms.NumericUpDown nbxTopCount;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbLoad;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.ToolStripButton tsbRun;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nbxBatchSize;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}
