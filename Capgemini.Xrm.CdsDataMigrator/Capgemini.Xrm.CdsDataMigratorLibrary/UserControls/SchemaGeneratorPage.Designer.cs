namespace Capgemini.Xrm.CdsDataMigratorLibrary.UserControls
{
    partial class SchemaGeneratorPage
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
            this.tlpMainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.tlpDetailsPane = new System.Windows.Forms.TableLayoutPanel();
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
            this.loadAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabelConnection = new System.Windows.Forms.ToolStripLabel();
            this.generatedSchemaLocationControl1 = new Capgemini.Xrm.CdsDataMigratorLibrary.UserControls.GeneratedSchemaLocationControl();
            this.listManagerView1 = new Capgemini.Xrm.CdsDataMigratorLibrary.UserControls.ListManagerView();
            this.listManagerView2 = new Capgemini.Xrm.CdsDataMigratorLibrary.UserControls.ListManagerView();
            this.entityListView1 = new Capgemini.Xrm.CdsDataMigratorLibrary.UserControls.EntityListView();
            this.tlpMainLayout.SuspendLayout();
            this.tlpDetailsPane.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMainLayout
            // 
            this.tlpMainLayout.ColumnCount = 2;
            this.tlpMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 350F));
            this.tlpMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMainLayout.Controls.Add(this.tlpDetailsPane, 1, 1);
            this.tlpMainLayout.Controls.Add(this.entityListView1, 0, 1);
            this.tlpMainLayout.Controls.Add(this.toolStrip2, 0, 0);
            this.tlpMainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMainLayout.Location = new System.Drawing.Point(0, 0);
            this.tlpMainLayout.Name = "tlpMainLayout";
            this.tlpMainLayout.RowCount = 2;
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tlpMainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMainLayout.Size = new System.Drawing.Size(1137, 615);
            this.tlpMainLayout.TabIndex = 0;
            // 
            // tlpDetailsPane
            // 
            this.tlpDetailsPane.ColumnCount = 1;
            this.tlpDetailsPane.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpDetailsPane.Controls.Add(this.generatedSchemaLocationControl1, 0, 0);
            this.tlpDetailsPane.Controls.Add(this.listManagerView1, 0, 1);
            this.tlpDetailsPane.Controls.Add(this.listManagerView2, 0, 2);
            this.tlpDetailsPane.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpDetailsPane.Location = new System.Drawing.Point(353, 38);
            this.tlpDetailsPane.Name = "tlpDetailsPane";
            this.tlpDetailsPane.RowCount = 3;
            this.tlpDetailsPane.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 230F));
            this.tlpDetailsPane.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpDetailsPane.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpDetailsPane.Size = new System.Drawing.Size(781, 574);
            this.tlpDetailsPane.TabIndex = 0;
            // 
            // toolStrip2
            // 
            this.tlpMainLayout.SetColumnSpan(this.toolStrip2, 2);
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtRetrieveEntities,
            this.toolStripSeparator5,
            this.toolStripDropDownButton2,
            this.toolStripSeparator3,
            this.toolStripDropDownButton1,
            this.toolStripSeparator6,
            this.toolStripButtonConnect,
            this.toolStripSeparator1,
            this.tsbtMappings,
            this.lookupMappings,
            this.toolStripSeparator4,
            this.tsbtFilters,
            this.toolStripSeparator2,
            this.toolStripLabelConnection});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip2.Size = new System.Drawing.Size(1137, 31);
            this.toolStrip2.TabIndex = 95;
            this.toolStrip2.Text = "toolStrip1";
            // 
            // toolStripButtonConnect
            // 
            this.toolStripButtonConnect.Image = global::Capgemini.Xrm.CdsDataMigratorLibrary.Properties.Resource.CRM;
            this.toolStripButtonConnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonConnect.Name = "toolStripButtonConnect";
            this.toolStripButtonConnect.Size = new System.Drawing.Size(91, 28);
            this.toolStripButtonConnect.Text = "Connect";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbtRetrieveEntities
            // 
            this.tsbtRetrieveEntities.Image = global::Capgemini.Xrm.CdsDataMigratorLibrary.Properties.Resource.entities;
            this.tsbtRetrieveEntities.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtRetrieveEntities.Name = "tsbtRetrieveEntities";
            this.tsbtRetrieveEntities.Size = new System.Drawing.Size(138, 28);
            this.tsbtRetrieveEntities.Text = "Refresh Entities";
            this.tsbtRetrieveEntities.Click += new System.EventHandler(this.RaiseRefreshEntitiesEvent);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbtMappings
            // 
            this.tsbtMappings.Image = global::Capgemini.Xrm.CdsDataMigratorLibrary.Properties.Resource.Mapping;
            this.tsbtMappings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtMappings.Name = "tsbtMappings";
            this.tsbtMappings.Size = new System.Drawing.Size(103, 28);
            this.tsbtMappings.Text = "Mappings";
            // 
            // lookupMappings
            // 
            this.lookupMappings.Image = global::Capgemini.Xrm.CdsDataMigratorLibrary.Properties.Resource.Mapping;
            this.lookupMappings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.lookupMappings.Name = "lookupMappings";
            this.lookupMappings.Size = new System.Drawing.Size(156, 28);
            this.lookupMappings.Text = "Lookup Mappings";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbtFilters
            // 
            this.tsbtFilters.Image = global::Capgemini.Xrm.CdsDataMigratorLibrary.Properties.Resource.Filter;
            this.tsbtFilters.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtFilters.Name = "tsbtFilters";
            this.tsbtFilters.Size = new System.Drawing.Size(76, 28);
            this.tsbtFilters.Text = "Filters";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveSchemaToolStripMenuItem,
            this.saveFiltersToolStripMenuItem,
            this.saveMappingsToolStripMenuItem,
            this.saveAllToolStripMenuItem});
            this.toolStripDropDownButton1.Image = global::Capgemini.Xrm.CdsDataMigratorLibrary.Properties.Resource.Save;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(78, 28);
            this.toolStripDropDownButton1.Text = "Save";
            // 
            // saveSchemaToolStripMenuItem
            // 
            this.saveSchemaToolStripMenuItem.Image = global::Capgemini.Xrm.CdsDataMigratorLibrary.Properties.Resource.Save;
            this.saveSchemaToolStripMenuItem.Name = "saveSchemaToolStripMenuItem";
            this.saveSchemaToolStripMenuItem.Size = new System.Drawing.Size(220, 26);
            this.saveSchemaToolStripMenuItem.Text = "Save Schema";
            // 
            // saveFiltersToolStripMenuItem
            // 
            this.saveFiltersToolStripMenuItem.Image = global::Capgemini.Xrm.CdsDataMigratorLibrary.Properties.Resource.Save;
            this.saveFiltersToolStripMenuItem.Name = "saveFiltersToolStripMenuItem";
            this.saveFiltersToolStripMenuItem.Size = new System.Drawing.Size(220, 26);
            this.saveFiltersToolStripMenuItem.Text = "Save Export Config";
            // 
            // saveMappingsToolStripMenuItem
            // 
            this.saveMappingsToolStripMenuItem.Image = global::Capgemini.Xrm.CdsDataMigratorLibrary.Properties.Resource.Save;
            this.saveMappingsToolStripMenuItem.Name = "saveMappingsToolStripMenuItem";
            this.saveMappingsToolStripMenuItem.Size = new System.Drawing.Size(220, 26);
            this.saveMappingsToolStripMenuItem.Text = "Save Import Config";
            // 
            // saveAllToolStripMenuItem
            // 
            this.saveAllToolStripMenuItem.Image = global::Capgemini.Xrm.CdsDataMigratorLibrary.Properties.Resource.Save;
            this.saveAllToolStripMenuItem.Name = "saveAllToolStripMenuItem";
            this.saveAllToolStripMenuItem.Size = new System.Drawing.Size(220, 26);
            this.saveAllToolStripMenuItem.Text = "Save All";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadSchemaToolStripMenuItem,
            this.loadFiltersToolStripMenuItem,
            this.loadMappingsToolStripMenuItem,
            this.loadAllToolStripMenuItem});
            this.toolStripDropDownButton2.Image = global::Capgemini.Xrm.CdsDataMigratorLibrary.Properties.Resource.CRM;
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(80, 28);
            this.toolStripDropDownButton2.Text = "Load";
            // 
            // loadSchemaToolStripMenuItem
            // 
            this.loadSchemaToolStripMenuItem.Image = global::Capgemini.Xrm.CdsDataMigratorLibrary.Properties.Resource.CRM;
            this.loadSchemaToolStripMenuItem.Name = "loadSchemaToolStripMenuItem";
            this.loadSchemaToolStripMenuItem.Size = new System.Drawing.Size(320, 26);
            this.loadSchemaToolStripMenuItem.Text = "Load Schema";
            // 
            // loadFiltersToolStripMenuItem
            // 
            this.loadFiltersToolStripMenuItem.Image = global::Capgemini.Xrm.CdsDataMigratorLibrary.Properties.Resource.CRM;
            this.loadFiltersToolStripMenuItem.Name = "loadFiltersToolStripMenuItem";
            this.loadFiltersToolStripMenuItem.Size = new System.Drawing.Size(320, 26);
            this.loadFiltersToolStripMenuItem.Text = "Load Filters and Lookup Mappings";
            // 
            // loadMappingsToolStripMenuItem
            // 
            this.loadMappingsToolStripMenuItem.Image = global::Capgemini.Xrm.CdsDataMigratorLibrary.Properties.Resource.CRM;
            this.loadMappingsToolStripMenuItem.Name = "loadMappingsToolStripMenuItem";
            this.loadMappingsToolStripMenuItem.Size = new System.Drawing.Size(320, 26);
            this.loadMappingsToolStripMenuItem.Text = "Load Guid Id Mappings";
            // 
            // loadAllToolStripMenuItem
            // 
            this.loadAllToolStripMenuItem.Image = global::Capgemini.Xrm.CdsDataMigratorLibrary.Properties.Resource.CRM;
            this.loadAllToolStripMenuItem.Name = "loadAllToolStripMenuItem";
            this.loadAllToolStripMenuItem.Size = new System.Drawing.Size(320, 26);
            this.loadAllToolStripMenuItem.Text = "Load All";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripLabelConnection
            // 
            this.toolStripLabelConnection.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.toolStripLabelConnection.ForeColor = System.Drawing.Color.Green;
            this.toolStripLabelConnection.Name = "toolStripLabelConnection";
            this.toolStripLabelConnection.Size = new System.Drawing.Size(0, 28);
            // 
            // generatedSchemaLocationControl1
            // 
            this.generatedSchemaLocationControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.generatedSchemaLocationControl1.Location = new System.Drawing.Point(3, 3);
            this.generatedSchemaLocationControl1.Name = "generatedSchemaLocationControl1";
            this.generatedSchemaLocationControl1.Size = new System.Drawing.Size(775, 224);
            this.generatedSchemaLocationControl1.TabIndex = 0;
            // 
            // listManagerView1
            // 
            this.listManagerView1.DisplayedItemsName = "Available Attributes";
            this.listManagerView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listManagerView1.Location = new System.Drawing.Point(3, 233);
            this.listManagerView1.Name = "listManagerView1";
            this.listManagerView1.Size = new System.Drawing.Size(775, 166);
            this.listManagerView1.TabIndex = 1;
            // 
            // listManagerView2
            // 
            this.listManagerView2.DisplayedItemsName = "Available Relationships";
            this.listManagerView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listManagerView2.Location = new System.Drawing.Point(3, 405);
            this.listManagerView2.Name = "listManagerView2";
            this.listManagerView2.Size = new System.Drawing.Size(775, 166);
            this.listManagerView2.TabIndex = 2;
            // 
            // entityListView1
            // 
            this.entityListView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.entityListView1.Entities = null;
            this.entityListView1.Location = new System.Drawing.Point(3, 38);
            this.entityListView1.Name = "entityListView1";
            this.entityListView1.Size = new System.Drawing.Size(344, 574);
            this.entityListView1.TabIndex = 1;
            // 
            // SchemaGeneratorPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpMainLayout);
            this.Name = "SchemaGeneratorPage";
            this.Size = new System.Drawing.Size(1137, 615);
            this.tlpMainLayout.ResumeLayout(false);
            this.tlpMainLayout.PerformLayout();
            this.tlpDetailsPane.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMainLayout;
        private System.Windows.Forms.TableLayoutPanel tlpDetailsPane;
        private EntityListView entityListView1;
        private GeneratedSchemaLocationControl generatedSchemaLocationControl1;
        private ListManagerView listManagerView1;
        private ListManagerView listManagerView2;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton toolStripButtonConnect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbtRetrieveEntities;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tsbtMappings;
        private System.Windows.Forms.ToolStripButton lookupMappings;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tsbtFilters;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem saveSchemaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveFiltersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveMappingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;
        private System.Windows.Forms.ToolStripMenuItem loadSchemaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadFiltersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadMappingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabelConnection;
    }
}
