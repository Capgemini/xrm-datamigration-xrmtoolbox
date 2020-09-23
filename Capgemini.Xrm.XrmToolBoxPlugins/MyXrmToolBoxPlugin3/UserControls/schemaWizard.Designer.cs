using System;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin
{
    partial class SchemaGenerator
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
            this.stepWizardControl1 = new AeroWizard.WizardPageContainer();
            this.wizardPage1 = new AeroWizard.WizardPage();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.wizardPage3 = new AeroWizard.WizardPage();
            this.gbRelationship = new System.Windows.Forms.GroupBox();
            this.chkAllRelationships = new System.Windows.Forms.CheckBox();
            this.lvRelationship = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btImportConfigPath = new System.Windows.Forms.Button();
            this.btExportConfigPath = new System.Windows.Forms.Button();
            this.tbImportConfig = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.gbAttributes = new System.Windows.Forms.GroupBox();
            this.chkAllAttributes = new System.Windows.Forms.CheckBox();
            this.lvAttributes = new System.Windows.Forms.ListView();
            this.clAttDisplayName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clAttLogicalName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clAttType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clAttComment = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tbExportConfig = new System.Windows.Forms.TextBox();
            this.gbEntities = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkAllEntities = new System.Windows.Forms.CheckBox();
            this.cbShowSystemAttributes = new Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.UserControls.ToggleCheckBox();
            this.lvEntities = new System.Windows.Forms.ListView();
            this.clEntDisplayName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clEntLogicalName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonConnect = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtRetrieveEntities = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtMappings = new System.Windows.Forms.ToolStripButton();
            this.lookupMappings = new System.Windows.Forms.ToolStripButton();
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
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.loadAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabelConnection = new System.Windows.Forms.ToolStripLabel();
            this.label10 = new System.Windows.Forms.Label();
            this.tbSchemaPath = new System.Windows.Forms.TextBox();
            this.btSchemaFolderPath = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.wizardButtons1 = new Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.UserControls.WizardButtons();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.stepWizardControl1)).BeginInit();
            this.stepWizardControl1.SuspendLayout();
            this.wizardPage1.SuspendLayout();
            this.wizardPage3.SuspendLayout();
            this.gbRelationship.SuspendLayout();
            this.gbAttributes.SuspendLayout();
            this.gbEntities.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // StatusLabel
            // 
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.Location = new System.Drawing.Point(40, 382);
            this.StatusLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(0, 20);
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
            // stepWizardControl1
            // 
            this.stepWizardControl1.BackButton = null;
            this.stepWizardControl1.BackButtonText = "";
            this.stepWizardControl1.CancelButton = null;
            this.stepWizardControl1.CancelButtonText = "";
            this.stepWizardControl1.Controls.Add(this.wizardPage3);
            this.stepWizardControl1.Controls.Add(this.wizardPage1);
            this.stepWizardControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stepWizardControl1.Location = new System.Drawing.Point(0, 0);
            this.stepWizardControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.stepWizardControl1.Name = "stepWizardControl1";
            this.stepWizardControl1.NextButton = null;
            this.stepWizardControl1.Pages.Add(this.wizardPage1);
            this.stepWizardControl1.Pages.Add(this.wizardPage3);
            this.stepWizardControl1.Size = new System.Drawing.Size(1800, 879);
            this.stepWizardControl1.TabIndex = 41;
            // 
            // wizardPage1
            // 
            this.wizardPage1.Controls.Add(this.label1);
            this.wizardPage1.Controls.Add(this.radioButton4);
            this.wizardPage1.Controls.Add(this.radioButton3);
            this.wizardPage1.Controls.Add(this.radioButton2);
            this.wizardPage1.Controls.Add(this.radioButton1);
            this.wizardPage1.Name = "wizardPage1";
            this.wizardPage1.ShowCancel = false;
            this.wizardPage1.Size = new System.Drawing.Size(1800, 879);
            this.wizardPage1.TabIndex = 2;
            this.wizardPage1.Text = "Schema Mode";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(78, 74);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(367, 40);
            this.label1.TabIndex = 4;
            this.label1.Text = "What would you like to do?";
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Checked = true;
            this.radioButton4.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton4.Location = new System.Drawing.Point(86, 248);
            this.radioButton4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(199, 35);
            this.radioButton4.TabIndex = 3;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "All of the above";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton3.Location = new System.Drawing.Point(86, 209);
            this.radioButton3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(414, 35);
            this.radioButton3.TabIndex = 2;
            this.radioButton3.Text = "Generate / Modify Import Config File";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton2.Location = new System.Drawing.Point(86, 172);
            this.radioButton2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(411, 35);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Text = "Generate / Modify Export Config File";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton1.Location = new System.Drawing.Point(86, 134);
            this.radioButton1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(383, 35);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.Text = "Generate / Modify Export Schema";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // wizardPage3
            // 
            this.wizardPage3.Controls.Add(this.gbRelationship);
            this.wizardPage3.Controls.Add(this.btImportConfigPath);
            this.wizardPage3.Controls.Add(this.btExportConfigPath);
            this.wizardPage3.Controls.Add(this.tbImportConfig);
            this.wizardPage3.Controls.Add(this.label9);
            this.wizardPage3.Controls.Add(this.gbAttributes);
            this.wizardPage3.Controls.Add(this.tbExportConfig);
            this.wizardPage3.Controls.Add(this.gbEntities);
            this.wizardPage3.Controls.Add(this.toolStrip2);
            this.wizardPage3.Controls.Add(this.label10);
            this.wizardPage3.Controls.Add(this.tbSchemaPath);
            this.wizardPage3.Controls.Add(this.btSchemaFolderPath);
            this.wizardPage3.Controls.Add(this.label8);
            this.wizardPage3.IsFinishPage = true;
            this.wizardPage3.Name = "wizardPage3";
            this.wizardPage3.ShowCancel = false;
            this.wizardPage3.Size = new System.Drawing.Size(1800, 879);
            this.wizardPage3.TabIndex = 4;
            this.wizardPage3.Text = "Schema Creation";
            // 
            // gbRelationship
            // 
            this.gbRelationship.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbRelationship.Controls.Add(this.chkAllRelationships);
            this.gbRelationship.Controls.Add(this.lvRelationship);
            this.gbRelationship.Enabled = false;
            this.gbRelationship.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbRelationship.Location = new System.Drawing.Point(561, 637);
            this.gbRelationship.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.gbRelationship.Name = "gbRelationship";
            this.gbRelationship.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.gbRelationship.Size = new System.Drawing.Size(1206, 345);
            this.gbRelationship.TabIndex = 103;
            this.gbRelationship.TabStop = false;
            this.gbRelationship.Text = "Available many to many relationships";
            this.gbRelationship.Enter += new System.EventHandler(this.gbRelationship_Enter);
            // 
            // chkAllRelationships
            // 
            this.chkAllRelationships.AutoSize = true;
            this.chkAllRelationships.Checked = true;
            this.chkAllRelationships.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAllRelationships.Location = new System.Drawing.Point(9, 28);
            this.chkAllRelationships.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkAllRelationships.Name = "chkAllRelationships";
            this.chkAllRelationships.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkAllRelationships.Size = new System.Drawing.Size(177, 27);
            this.chkAllRelationships.TabIndex = 3;
            this.chkAllRelationships.Text = "Select/Unselect All";
            this.chkAllRelationships.UseVisualStyleBackColor = true;
            this.chkAllRelationships.CheckedChanged += new System.EventHandler(this.CheckBoxAllRelationshipsCheckedChanged);
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
            this.lvRelationship.Location = new System.Drawing.Point(14, 57);
            this.lvRelationship.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lvRelationship.Name = "lvRelationship";
            this.lvRelationship.Size = new System.Drawing.Size(1190, 327);
            this.lvRelationship.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvRelationship.TabIndex = 64;
            this.lvRelationship.UseCompatibleStateImageBehavior = false;
            this.lvRelationship.View = System.Windows.Forms.View.Details;
            this.lvRelationship.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ListViewRelationshipItemCheck);
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
            // btImportConfigPath
            // 
            this.btImportConfigPath.Enabled = false;
            this.btImportConfigPath.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btImportConfigPath.Location = new System.Drawing.Point(1026, 65);
            this.btImportConfigPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.btImportConfigPath.Name = "btImportConfigPath";
            this.btImportConfigPath.Size = new System.Drawing.Size(39, 40);
            this.btImportConfigPath.TabIndex = 35;
            this.btImportConfigPath.Text = "...";
            this.btImportConfigPath.UseVisualStyleBackColor = true;
            this.btImportConfigPath.Click += new System.EventHandler(this.ButtonImportConfigPathClick);
            // 
            // btExportConfigPath
            // 
            this.btExportConfigPath.Enabled = false;
            this.btExportConfigPath.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btExportConfigPath.Location = new System.Drawing.Point(1026, 105);
            this.btExportConfigPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.btExportConfigPath.Name = "btExportConfigPath";
            this.btExportConfigPath.Size = new System.Drawing.Size(39, 40);
            this.btExportConfigPath.TabIndex = 36;
            this.btExportConfigPath.Text = "...";
            this.btExportConfigPath.UseVisualStyleBackColor = true;
            this.btExportConfigPath.Click += new System.EventHandler(this.btExportConfigPath_Click);
            // 
            // tbImportConfig
            // 
            this.tbImportConfig.Enabled = false;
            this.tbImportConfig.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbImportConfig.Location = new System.Drawing.Point(720, 69);
            this.tbImportConfig.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.tbImportConfig.Name = "tbImportConfig";
            this.tbImportConfig.Size = new System.Drawing.Size(295, 29);
            this.tbImportConfig.TabIndex = 33;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(570, 75);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(117, 23);
            this.label9.TabIndex = 31;
            this.label9.Text = "Import Config";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // gbAttributes
            // 
            this.gbAttributes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbAttributes.Controls.Add(this.chkAllAttributes);
            this.gbAttributes.Controls.Add(this.lvAttributes);
            this.gbAttributes.Enabled = false;
            this.gbAttributes.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbAttributes.Location = new System.Drawing.Point(561, 200);
            this.gbAttributes.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbAttributes.Name = "gbAttributes";
            this.gbAttributes.Padding = new System.Windows.Forms.Padding(4, 5, 4, 0);
            this.gbAttributes.Size = new System.Drawing.Size(1215, 417);
            this.gbAttributes.TabIndex = 96;
            this.gbAttributes.TabStop = false;
            this.gbAttributes.Text = "Available attributes";
            // 
            // chkAllAttributes
            // 
            this.chkAllAttributes.AutoSize = true;
            this.chkAllAttributes.Checked = true;
            this.chkAllAttributes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAllAttributes.Location = new System.Drawing.Point(9, 32);
            this.chkAllAttributes.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkAllAttributes.Name = "chkAllAttributes";
            this.chkAllAttributes.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkAllAttributes.Size = new System.Drawing.Size(177, 27);
            this.chkAllAttributes.TabIndex = 3;
            this.chkAllAttributes.Text = "Select/Unselect All";
            this.chkAllAttributes.UseVisualStyleBackColor = true;
            this.chkAllAttributes.CheckedChanged += new System.EventHandler(this.CheckListAllAttributesCheckedChanged);
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
            this.lvAttributes.Location = new System.Drawing.Point(9, 58);
            this.lvAttributes.Margin = new System.Windows.Forms.Padding(4, 5, 4, 0);
            this.lvAttributes.Name = "lvAttributes";
            this.lvAttributes.Size = new System.Drawing.Size(1195, 356);
            this.lvAttributes.TabIndex = 64;
            this.lvAttributes.UseCompatibleStateImageBehavior = false;
            this.lvAttributes.View = System.Windows.Forms.View.Details;
            this.lvAttributes.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ListViewAttributesColumnClick);
            this.lvAttributes.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ListViewAttributesItemCheck);
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
            // tbExportConfig
            // 
            this.tbExportConfig.Enabled = false;
            this.tbExportConfig.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbExportConfig.Location = new System.Drawing.Point(720, 108);
            this.tbExportConfig.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.tbExportConfig.Name = "tbExportConfig";
            this.tbExportConfig.Size = new System.Drawing.Size(295, 29);
            this.tbExportConfig.TabIndex = 34;
            // 
            // gbEntities
            // 
            this.gbEntities.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gbEntities.Controls.Add(this.label2);
            this.gbEntities.Controls.Add(this.chkAllEntities);
            this.gbEntities.Controls.Add(this.cbShowSystemAttributes);
            this.gbEntities.Controls.Add(this.lvEntities);
            this.gbEntities.Enabled = false;
            this.gbEntities.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.gbEntities.Location = new System.Drawing.Point(24, 60);
            this.gbEntities.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbEntities.Name = "gbEntities";
            this.gbEntities.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbEntities.Size = new System.Drawing.Size(514, 797);
            this.gbEntities.TabIndex = 95;
            this.gbEntities.TabStop = false;
            this.gbEntities.Text = "Available entities";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(226, 51);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(190, 23);
            this.label2.TabIndex = 105;
            this.label2.Text = "Show System Attributes";
            // 
            // chkAllEntities
            // 
            this.chkAllEntities.AutoSize = true;
            this.chkAllEntities.Checked = true;
            this.chkAllEntities.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAllEntities.Location = new System.Drawing.Point(9, 49);
            this.chkAllEntities.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkAllEntities.Name = "chkAllEntities";
            this.chkAllEntities.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkAllEntities.Size = new System.Drawing.Size(177, 27);
            this.chkAllEntities.TabIndex = 65;
            this.chkAllEntities.Text = "Select/Unselect All";
            this.chkAllEntities.UseVisualStyleBackColor = true;
            this.chkAllEntities.CheckedChanged += new System.EventHandler(this.CheckListAllEntitiesCheckedChanged);
            // 
            // cbShowSystemAttributes
            // 
            this.cbShowSystemAttributes.Checked = true;
            this.cbShowSystemAttributes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbShowSystemAttributes.Location = new System.Drawing.Point(428, 43);
            this.cbShowSystemAttributes.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbShowSystemAttributes.Name = "cbShowSystemAttributes";
            this.cbShowSystemAttributes.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cbShowSystemAttributes.Size = new System.Drawing.Size(72, 37);
            this.cbShowSystemAttributes.TabIndex = 104;
            this.cbShowSystemAttributes.Text = "toggleCheckBox1";
            this.cbShowSystemAttributes.UseVisualStyleBackColor = true;
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
            this.lvEntities.Location = new System.Drawing.Point(9, 85);
            this.lvEntities.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lvEntities.MultiSelect = false;
            this.lvEntities.Name = "lvEntities";
            this.lvEntities.Size = new System.Drawing.Size(494, 728);
            this.lvEntities.TabIndex = 64;
            this.lvEntities.UseCompatibleStateImageBehavior = false;
            this.lvEntities.View = System.Windows.Forms.View.Details;
            this.lvEntities.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ListViewEntitiesItemCheck);
            this.lvEntities.SelectedIndexChanged += new System.EventHandler(this.ListViewEntitiesSelectedIndexChanged);
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
            this.toolStrip2.GripMargin = new System.Windows.Forms.Padding(4);
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonConnect,
            this.toolStripSeparator1,
            this.tsbtRetrieveEntities,
            this.toolStripSeparator5,
            this.tsbtMappings,
            this.lookupMappings,
            this.toolStripSeparator4,
            this.tsbtFilters,
            this.toolStripSeparator3,
            this.toolStripDropDownButton1,
            this.toolStripSeparator6,
            this.toolStripDropDownButton2,
            this.toolStripSeparator2,
            this.toolStripLabelConnection});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip2.Size = new System.Drawing.Size(1800, 34);
            this.toolStrip2.TabIndex = 94;
            this.toolStrip2.Text = "toolStrip1";
            // 
            // toolStripButtonConnect
            // 
            this.toolStripButtonConnect.Image = global::Capgemini.Xrm.DataMigration.XrmToolBox.Properties.Resources.CRM;
            this.toolStripButtonConnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonConnect.Name = "toolStripButtonConnect";
            this.toolStripButtonConnect.Size = new System.Drawing.Size(105, 29);
            this.toolStripButtonConnect.Text = "Connect";
            this.toolStripButtonConnect.Click += new System.EventHandler(this.ToolStripButtonConnectClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 34);
            // 
            // tsbtRetrieveEntities
            // 
            this.tsbtRetrieveEntities.Image = global::Capgemini.Xrm.DataMigration.XrmToolBox.Properties.Resources.entities;
            this.tsbtRetrieveEntities.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtRetrieveEntities.Name = "tsbtRetrieveEntities";
            this.tsbtRetrieveEntities.Size = new System.Drawing.Size(159, 29);
            this.tsbtRetrieveEntities.Text = "Refresh Entities";
            this.tsbtRetrieveEntities.Click += new System.EventHandler(this.TabStripButtonRetrieveEntitiesClick);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 34);
            // 
            // tsbtMappings
            // 
            this.tsbtMappings.Image = global::Capgemini.Xrm.DataMigration.XrmToolBox.Properties.Resources.Mapping;
            this.tsbtMappings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtMappings.Name = "tsbtMappings";
            this.tsbtMappings.Size = new System.Drawing.Size(120, 29);
            this.tsbtMappings.Text = "Mappings";
            this.tsbtMappings.Click += new System.EventHandler(this.tsbtMappings_Click);
            // 
            // lookupMappings
            // 
            this.lookupMappings.Image = global::Capgemini.Xrm.DataMigration.XrmToolBox.Properties.Resources.Mapping;
            this.lookupMappings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.lookupMappings.Name = "lookupMappings";
            this.lookupMappings.Size = new System.Drawing.Size(185, 29);
            this.lookupMappings.Text = "Lookup Mappings";
            this.lookupMappings.Click += new System.EventHandler(this.ToolStripButton1Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 34);
            // 
            // tsbtFilters
            // 
            this.tsbtFilters.Image = global::Capgemini.Xrm.DataMigration.XrmToolBox.Properties.Resources.Filter;
            this.tsbtFilters.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtFilters.Name = "tsbtFilters";
            this.tsbtFilters.Size = new System.Drawing.Size(86, 29);
            this.tsbtFilters.Text = "Filters";
            this.tsbtFilters.Click += new System.EventHandler(this.TabStripFiltersClick);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 34);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveSchemaToolStripMenuItem,
            this.saveFiltersToolStripMenuItem,
            this.saveMappingsToolStripMenuItem,
            this.saveAllToolStripMenuItem});
            this.toolStripDropDownButton1.Image = global::Capgemini.Xrm.DataMigration.XrmToolBox.Properties.Resources.Save;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(91, 29);
            this.toolStripDropDownButton1.Text = "Save";
            // 
            // saveSchemaToolStripMenuItem
            // 
            this.saveSchemaToolStripMenuItem.Image = global::Capgemini.Xrm.DataMigration.XrmToolBox.Properties.Resources.Save;
            this.saveSchemaToolStripMenuItem.Name = "saveSchemaToolStripMenuItem";
            this.saveSchemaToolStripMenuItem.Size = new System.Drawing.Size(269, 34);
            this.saveSchemaToolStripMenuItem.Text = "Save Schema";
            this.saveSchemaToolStripMenuItem.Click += new System.EventHandler(this.TbSaveSchemaClick);
            // 
            // saveFiltersToolStripMenuItem
            // 
            this.saveFiltersToolStripMenuItem.Image = global::Capgemini.Xrm.DataMigration.XrmToolBox.Properties.Resources.Save;
            this.saveFiltersToolStripMenuItem.Name = "saveFiltersToolStripMenuItem";
            this.saveFiltersToolStripMenuItem.Size = new System.Drawing.Size(269, 34);
            this.saveFiltersToolStripMenuItem.Text = "Save Export Config";
            this.saveFiltersToolStripMenuItem.Click += new System.EventHandler(this.ToolBarSaveFiltersClick);
            // 
            // saveMappingsToolStripMenuItem
            // 
            this.saveMappingsToolStripMenuItem.Image = global::Capgemini.Xrm.DataMigration.XrmToolBox.Properties.Resources.Save;
            this.saveMappingsToolStripMenuItem.Name = "saveMappingsToolStripMenuItem";
            this.saveMappingsToolStripMenuItem.Size = new System.Drawing.Size(269, 34);
            this.saveMappingsToolStripMenuItem.Text = "Save Import Config";
            this.saveMappingsToolStripMenuItem.Click += new System.EventHandler(this.ToolBarSaveMappingsClick);
            // 
            // saveAllToolStripMenuItem
            // 
            this.saveAllToolStripMenuItem.Image = global::Capgemini.Xrm.DataMigration.XrmToolBox.Properties.Resources.Save;
            this.saveAllToolStripMenuItem.Name = "saveAllToolStripMenuItem";
            this.saveAllToolStripMenuItem.Size = new System.Drawing.Size(269, 34);
            this.saveAllToolStripMenuItem.Text = "Save All";
            this.saveAllToolStripMenuItem.Click += new System.EventHandler(this.SaveAllToolStripMenuItemClick);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 34);
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadSchemaToolStripMenuItem,
            this.loadFiltersToolStripMenuItem,
            this.loadMappingsToolStripMenuItem,
            this.toolStripMenuItem1,
            this.loadAllToolStripMenuItem});
            this.toolStripDropDownButton2.Image = global::Capgemini.Xrm.DataMigration.XrmToolBox.Properties.Resources.CRM;
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(93, 29);
            this.toolStripDropDownButton2.Text = "Load";
            // 
            // loadSchemaToolStripMenuItem
            // 
            this.loadSchemaToolStripMenuItem.Image = global::Capgemini.Xrm.DataMigration.XrmToolBox.Properties.Resources.CRM;
            this.loadSchemaToolStripMenuItem.Name = "loadSchemaToolStripMenuItem";
            this.loadSchemaToolStripMenuItem.Size = new System.Drawing.Size(305, 34);
            this.loadSchemaToolStripMenuItem.Text = "Load Schema";
            this.loadSchemaToolStripMenuItem.Click += new System.EventHandler(this.ToolBarLoadSchemaFileClick);
            // 
            // loadFiltersToolStripMenuItem
            // 
            this.loadFiltersToolStripMenuItem.Image = global::Capgemini.Xrm.DataMigration.XrmToolBox.Properties.Resources.CRM;
            this.loadFiltersToolStripMenuItem.Name = "loadFiltersToolStripMenuItem";
            this.loadFiltersToolStripMenuItem.Size = new System.Drawing.Size(305, 34);
            this.loadFiltersToolStripMenuItem.Text = "Load Filters";
            this.loadFiltersToolStripMenuItem.Click += new System.EventHandler(this.ToolBarLoadFiltersFileClick);
            // 
            // loadMappingsToolStripMenuItem
            // 
            this.loadMappingsToolStripMenuItem.Image = global::Capgemini.Xrm.DataMigration.XrmToolBox.Properties.Resources.CRM;
            this.loadMappingsToolStripMenuItem.Name = "loadMappingsToolStripMenuItem";
            this.loadMappingsToolStripMenuItem.Size = new System.Drawing.Size(305, 34);
            this.loadMappingsToolStripMenuItem.Text = "Load Mappings";
            this.loadMappingsToolStripMenuItem.Click += new System.EventHandler(this.ToolBarLoadMappingsFileClick);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Image = global::Capgemini.Xrm.DataMigration.XrmToolBox.Properties.Resources.CRM;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(305, 34);
            this.toolStripMenuItem1.Text = "Load LookUp Mappings";
            // 
            // loadAllToolStripMenuItem
            // 
            this.loadAllToolStripMenuItem.Image = global::Capgemini.Xrm.DataMigration.XrmToolBox.Properties.Resources.CRM;
            this.loadAllToolStripMenuItem.Name = "loadAllToolStripMenuItem";
            this.loadAllToolStripMenuItem.Size = new System.Drawing.Size(305, 34);
            this.loadAllToolStripMenuItem.Text = "Load All";
            this.loadAllToolStripMenuItem.Click += new System.EventHandler(this.LoadAllToolStripMenuItemClick);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 34);
            // 
            // toolStripLabelConnection
            // 
            this.toolStripLabelConnection.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.toolStripLabelConnection.ForeColor = System.Drawing.Color.Green;
            this.toolStripLabelConnection.Name = "toolStripLabelConnection";
            this.toolStripLabelConnection.Size = new System.Drawing.Size(0, 29);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(572, 114);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(114, 23);
            this.label10.TabIndex = 32;
            this.label10.Text = "Export Config";
            this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tbSchemaPath
            // 
            this.tbSchemaPath.Enabled = false;
            this.tbSchemaPath.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSchemaPath.Location = new System.Drawing.Point(720, 148);
            this.tbSchemaPath.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.tbSchemaPath.Name = "tbSchemaPath";
            this.tbSchemaPath.Size = new System.Drawing.Size(295, 29);
            this.tbSchemaPath.TabIndex = 30;
            // 
            // btSchemaFolderPath
            // 
            this.btSchemaFolderPath.Enabled = false;
            this.btSchemaFolderPath.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btSchemaFolderPath.Location = new System.Drawing.Point(1026, 145);
            this.btSchemaFolderPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.btSchemaFolderPath.Name = "btSchemaFolderPath";
            this.btSchemaFolderPath.Size = new System.Drawing.Size(39, 40);
            this.btSchemaFolderPath.TabIndex = 29;
            this.btSchemaFolderPath.Text = "...";
            this.btSchemaFolderPath.UseVisualStyleBackColor = true;
            this.btSchemaFolderPath.Click += new System.EventHandler(this.ButtonSchemaFolderPathClick);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(570, 155);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(139, 23);
            this.label8.TabIndex = 27;
            this.label8.Text = "Schema File Path";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // wizardButtons1
            // 
            this.wizardButtons1.Container = this.stepWizardControl1;
            this.wizardButtons1.Dock = System.Windows.Forms.DockStyle.Right;
            this.wizardButtons1.Location = new System.Drawing.Point(1420, 0);
            this.wizardButtons1.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.wizardButtons1.Name = "wizardButtons1";
            this.wizardButtons1.ShowExecuteButton = true;
            this.wizardButtons1.Size = new System.Drawing.Size(380, 115);
            this.wizardButtons1.TabIndex = 2;
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
            this.splitContainer1.Size = new System.Drawing.Size(1800, 1000);
            this.splitContainer1.SplitterDistance = 879;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 42;
            // 
            // SchemaGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.StatusLabel);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "SchemaGenerator";
            this.Size = new System.Drawing.Size(1800, 1000);
            ((System.ComponentModel.ISupportInitialize)(this.stepWizardControl1)).EndInit();
            this.stepWizardControl1.ResumeLayout(false);
            this.wizardPage1.ResumeLayout(false);
            this.wizardPage1.PerformLayout();
            this.wizardPage3.ResumeLayout(false);
            this.wizardPage3.PerformLayout();
            this.gbRelationship.ResumeLayout(false);
            this.gbRelationship.PerformLayout();
            this.gbAttributes.ResumeLayout(false);
            this.gbAttributes.PerformLayout();
            this.gbEntities.ResumeLayout(false);
            this.gbEntities.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.FolderBrowserDialog fbExportPath;
        private System.Windows.Forms.FolderBrowserDialog fbSchemaPath;
        private System.Windows.Forms.OpenFileDialog fdSchemaFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
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
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.CheckBox chkAllEntities;
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
        private System.Windows.Forms.ToolStripButton lookupMappings;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private AeroWizard.WizardPage wizardPage1;
        private AeroWizard.WizardPage wizardPage3;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripButton toolStripButtonConnect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabelConnection;
        private AeroWizard.WizardPageContainer stepWizardControl1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private UserControls.WizardButtons wizardButtons1;
        private UserControls.ToggleCheckBox cbShowSystemAttributes;
        private System.Windows.Forms.Label label2;
    }
}
