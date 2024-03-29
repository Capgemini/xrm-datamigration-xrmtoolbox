﻿namespace Capgemini.Xrm.CdsDataMigratorLibrary
{
    partial class CdsMigratorPluginControl
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
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripPluginOptionMenu = new System.Windows.Forms.ToolStrip();
            this.tsbSchemaManager = new System.Windows.Forms.ToolStripButton();
            this.tsbShowImportPage = new System.Windows.Forms.ToolStripButton();
            this.tsbShowExportPage = new System.Windows.Forms.ToolStripButton();
            this.sgpManageSchema = new Capgemini.Xrm.CdsDataMigratorLibrary.UserControls.SchemaGeneratorPage();
            this.exportPage1 = new Capgemini.Xrm.CdsDataMigratorLibrary.UserControls.ExportPage();
            this.importPage1 = new Capgemini.Xrm.CdsDataMigratorLibrary.UserControls.ImportPage();
            this.toolStripPluginOptionMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StatusLabel.Location = new System.Drawing.Point(0, 0);
            this.StatusLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(0, 16);
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 44);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 44);
            // 
            // toolStripPluginOptionMenu
            // 
            this.toolStripPluginOptionMenu.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.toolStripPluginOptionMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripPluginOptionMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSchemaManager,
            this.toolStripSeparator1,
            this.tsbShowImportPage,
            this.toolStripSeparator2,
            this.tsbShowExportPage});
            this.toolStripPluginOptionMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripPluginOptionMenu.Name = "toolStripPluginOptionMenu";
            this.toolStripPluginOptionMenu.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.toolStripPluginOptionMenu.Size = new System.Drawing.Size(1829, 44);
            this.toolStripPluginOptionMenu.TabIndex = 5;
            this.toolStripPluginOptionMenu.Text = "toolStrip1";
            // 
            // tsbSchemaManager
            // 
            this.tsbSchemaManager.Image = global::Capgemini.Xrm.CdsDataMigratorLibrary.Properties.Resource.entities;
            this.tsbSchemaManager.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSchemaManager.Name = "tsbSchemaManager";
            this.tsbSchemaManager.Size = new System.Drawing.Size(219, 41);
            this.tsbSchemaManager.Text = "Schema Config";
            this.tsbSchemaManager.Click += new System.EventHandler(this.ShowSchemaManager);
            // 
            // tsbShowImportPage
            // 
            this.tsbShowImportPage.Image = global::Capgemini.Xrm.CdsDataMigratorLibrary.Properties.Resource.Import;
            this.tsbShowImportPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbShowImportPage.Name = "tsbShowImportPage";
            this.tsbShowImportPage.Size = new System.Drawing.Size(194, 41);
            this.tsbShowImportPage.Text = "Data Import";
            this.tsbShowImportPage.Click += new System.EventHandler(this.ShowImportPage_Click);
            // 
            // tsbShowExportPage
            // 
            this.tsbShowExportPage.Image = global::Capgemini.Xrm.CdsDataMigratorLibrary.Properties.Resource.export;
            this.tsbShowExportPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbShowExportPage.Name = "tsbShowExportPage";
            this.tsbShowExportPage.Size = new System.Drawing.Size(191, 41);
            this.tsbShowExportPage.Text = "Data Export";
            this.tsbShowExportPage.Click += new System.EventHandler(this.ShowExportPage_Click);
            // 
            // sgpManageSchema
            // 
            this.sgpManageSchema.CurrentConnection = "";
            this.sgpManageSchema.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sgpManageSchema.EntityMetadataList = null;
            this.sgpManageSchema.EntityRelationships = null;
            this.sgpManageSchema.Location = new System.Drawing.Point(0, 55);
            this.sgpManageSchema.Name = "sgpManageSchema";
            this.sgpManageSchema.ShowSystemAttributes = false;
            this.sgpManageSchema.Size = new System.Drawing.Size(1829, 683);
            this.sgpManageSchema.TabIndex = 51;
            // 
            // exportPage1
            // 
            this.exportPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.exportPage1.Location = new System.Drawing.Point(0, 0);
            this.exportPage1.Margin = new System.Windows.Forms.Padding(5);
            this.exportPage1.Name = "exportPage1";
            this.exportPage1.Size = new System.Drawing.Size(1829, 738);
            this.exportPage1.TabIndex = 50;
            // 
            // importPage1
            // 
            this.importPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.importPage1.Location = new System.Drawing.Point(0, 0);
            this.importPage1.Margin = new System.Windows.Forms.Padding(5);
            this.importPage1.Name = "importPage1";
            this.importPage1.Size = new System.Drawing.Size(1829, 738);
            this.importPage1.TabIndex = 0;
            // 
            // CdsMigratorPluginControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.sgpManageSchema);
            this.Controls.Add(this.toolStripPluginOptionMenu);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.exportPage1);
            this.Controls.Add(this.importPage1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1400, 738);
            this.Name = "CdsMigratorPluginControl";
            this.Size = new System.Drawing.Size(1463, 590);
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
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStrip toolStripPluginOptionMenu;
        private UserControls.ExportPage exportPage1;
        private UserControls.ImportPage importPage1;
        private System.Windows.Forms.ToolStripButton tsbShowExportPage;
        private UserControls.SchemaGeneratorPage sgpManageSchema;
        private System.Windows.Forms.ToolStripButton tsbSchemaManager;
        private System.Windows.Forms.ToolStripButton tsbShowImportPage;
    }
}
