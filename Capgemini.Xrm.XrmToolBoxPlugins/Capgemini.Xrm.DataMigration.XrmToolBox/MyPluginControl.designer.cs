namespace MyXrmToolBoxPlugin3
{
    partial class MyPluginControl
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
            this.StatusLabel = new System.Windows.Forms.Label();
            this.fbExportPath = new System.Windows.Forms.FolderBrowserDialog();
            this.fbSchemaPath = new System.Windows.Forms.FolderBrowserDialog();
            this.fdSchemaFile = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.toolStripButtonSchemaConfig = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonDataImport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonDataExport = new System.Windows.Forms.ToolStripButton();
            this.toolStripPluginOptionMenu = new System.Windows.Forms.ToolStrip();
            this.SchemaGeneratorWizard = new Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.SchemaGenerator();
            this.DataImportWizard = new Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.UserControls.importWizard();
            this.DataExportWizard = new Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.UserControls.exportWizard();
            this.toolStripPluginOptionMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StatusLabel.Location = new System.Drawing.Point(0, 0);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(0, 13);
            this.StatusLabel.TabIndex = 16;
            // 
            // fdSchemaFile
            // 
            this.fdSchemaFile.FileName = "openFileDialog1";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // toolStripButtonSchemaConfig
            // 
            this.toolStripButtonSchemaConfig.Image = global::Capgemini.Xrm.DataMigration.XrmToolBox.Properties.Resources.entities;
            this.toolStripButtonSchemaConfig.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonSchemaConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSchemaConfig.Name = "toolStripButtonSchemaConfig";
            this.toolStripButtonSchemaConfig.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.toolStripButtonSchemaConfig.Size = new System.Drawing.Size(135, 25);
            this.toolStripButtonSchemaConfig.Text = "Schema Config";
            this.toolStripButtonSchemaConfig.Click += new System.EventHandler(this.toolStripButtonSchemaConfig_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 28);
            // 
            // toolStripButtonDataImport
            // 
            this.toolStripButtonDataImport.Image = global::Capgemini.Xrm.DataMigration.XrmToolBox.Properties.Resources.Import;
            this.toolStripButtonDataImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDataImport.Name = "toolStripButtonDataImport";
            this.toolStripButtonDataImport.Size = new System.Drawing.Size(117, 25);
            this.toolStripButtonDataImport.Text = "Data Import";
            this.toolStripButtonDataImport.Click += new System.EventHandler(this.toolStripButtonDataImport_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 28);
            // 
            // toolStripButtonDataExport
            // 
            this.toolStripButtonDataExport.Image = global::Capgemini.Xrm.DataMigration.XrmToolBox.Properties.Resources.export;
            this.toolStripButtonDataExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDataExport.Name = "toolStripButtonDataExport";
            this.toolStripButtonDataExport.Size = new System.Drawing.Size(114, 25);
            this.toolStripButtonDataExport.Text = "Data Export";
            this.toolStripButtonDataExport.Click += new System.EventHandler(this.toolStripButtonDataExport_Click);
            // 
            // toolStripPluginOptionMenu
            // 
            this.toolStripPluginOptionMenu.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.toolStripPluginOptionMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripPluginOptionMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonSchemaConfig,
            this.toolStripSeparator1,
            this.toolStripButtonDataImport,
            this.toolStripSeparator2,
            this.toolStripButtonDataExport});
            this.toolStripPluginOptionMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripPluginOptionMenu.Name = "toolStripPluginOptionMenu";
            this.toolStripPluginOptionMenu.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStripPluginOptionMenu.Size = new System.Drawing.Size(1372, 28);
            this.toolStripPluginOptionMenu.TabIndex = 4;
            this.toolStripPluginOptionMenu.Text = "toolStrip1";
            // 
            // SchemaGeneratorWizard
            // 
            this.SchemaGeneratorWizard.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.SchemaGeneratorWizard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SchemaGeneratorWizard.Location = new System.Drawing.Point(0, 0);
            this.SchemaGeneratorWizard.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.SchemaGeneratorWizard.Name = "SchemaGeneratorWizard";
            this.SchemaGeneratorWizard.Size = new System.Drawing.Size(1372, 600);
            this.SchemaGeneratorWizard.TabIndex = 0;
            // 
            // DataImportWizard
            // 
            this.DataImportWizard.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.DataImportWizard.CrmServiceClient = null;
            this.DataImportWizard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataImportWizard.Location = new System.Drawing.Point(0, 0);
            this.DataImportWizard.Name = "DataImportWizard";
            this.DataImportWizard.Size = new System.Drawing.Size(1372, 600);
            this.DataImportWizard.TabIndex = 47;
            this.DataImportWizard.TargetConnectionString = null;
            // 
            // DataExportWizard
            // 
            this.DataExportWizard.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.DataExportWizard.BatchSize = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.DataExportWizard.CrmServiceClient = null;
            this.DataExportWizard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataExportWizard.ExportConfigFileLocation = "";
            this.DataExportWizard.ExportInactiveRecordsChecked = false;
            this.DataExportWizard.ExportSchemaFileLocation = "";
            this.DataExportWizard.FormatCsvSelected = false;
            this.DataExportWizard.FormatJsonSelected = true;
            this.DataExportWizard.Location = new System.Drawing.Point(0, 0);
            this.DataExportWizard.Margin = new System.Windows.Forms.Padding(0);
            this.DataExportWizard.MinimizeJsonChecked = false;
            this.DataExportWizard.Name = "DataExportWizard";
            this.DataExportWizard.SaveExportLocation = "";
            this.DataExportWizard.Size = new System.Drawing.Size(1372, 600);
            this.DataExportWizard.TabIndex = 49;
            // 
            // MyPluginControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStripPluginOptionMenu);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.SchemaGeneratorWizard);
            this.Controls.Add(this.DataImportWizard);
            this.Controls.Add(this.DataExportWizard);
            this.MinimumSize = new System.Drawing.Size(1050, 600);
            this.Name = "MyPluginControl";
            this.Size = new System.Drawing.Size(1372, 600);
            this.toolStripPluginOptionMenu.ResumeLayout(false);
            this.toolStripPluginOptionMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.FolderBrowserDialog fbExportPath;
        private System.Windows.Forms.FolderBrowserDialog fbSchemaPath;
        private System.Windows.Forms.OpenFileDialog fdSchemaFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.UserControls.importWizard DataImportWizard;
        private Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.UserControls.exportWizard DataExportWizard;
        private Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.SchemaGenerator SchemaGeneratorWizard;
        private System.Windows.Forms.ToolStripButton toolStripButtonSchemaConfig;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonDataImport;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButtonDataExport;
        private System.Windows.Forms.ToolStrip toolStripPluginOptionMenu;
    }
}
