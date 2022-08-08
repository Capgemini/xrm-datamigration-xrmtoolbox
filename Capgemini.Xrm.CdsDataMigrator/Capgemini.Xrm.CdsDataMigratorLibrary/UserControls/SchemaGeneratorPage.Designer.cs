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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SchemaGeneratorPage));
            this.tlpMainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.tlpDetailsPane = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.tsbtRetrieveEntities = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtLoadSchema = new System.Windows.Forms.ToolStripButton();
            this.tsbSaveSchema = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtCurrentConnection = new System.Windows.Forms.ToolStripLabel();
            this.schemaLocationControl1 = new Capgemini.Xrm.CdsDataMigratorLibrary.UserControls.SchemaLocationControl();
            this.lmvAttributes = new Capgemini.Xrm.CdsDataMigratorLibrary.UserControls.ListManagerView();
            this.lmvRelationships = new Capgemini.Xrm.CdsDataMigratorLibrary.UserControls.ListManagerView();
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
            this.tlpDetailsPane.Controls.Add(this.schemaLocationControl1, 0, 0);
            this.tlpDetailsPane.Controls.Add(this.lmvAttributes, 0, 1);
            this.tlpDetailsPane.Controls.Add(this.lmvRelationships, 0, 2);
            this.tlpDetailsPane.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpDetailsPane.Location = new System.Drawing.Point(353, 38);
            this.tlpDetailsPane.Name = "tlpDetailsPane";
            this.tlpDetailsPane.RowCount = 3;
            this.tlpDetailsPane.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
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
            this.tsbtLoadSchema,
            this.tsbSaveSchema,
            this.toolStripSeparator1,
            this.tsbtCurrentConnection});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.toolStrip2.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip2.Size = new System.Drawing.Size(1137, 31);
            this.toolStrip2.TabIndex = 95;
            this.toolStrip2.Text = "toolStrip1";
            // 
            // tsbtRetrieveEntities
            // 
            this.tsbtRetrieveEntities.Image = global::Capgemini.Xrm.CdsDataMigratorLibrary.Properties.Resource.entities;
            this.tsbtRetrieveEntities.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtRetrieveEntities.Name = "tsbtRetrieveEntities";
            this.tsbtRetrieveEntities.Size = new System.Drawing.Size(138, 28);
            this.tsbtRetrieveEntities.Text = "Refresh Entities";
            this.tsbtRetrieveEntities.Click += new System.EventHandler(this.RefreshEntitiesButtonClick);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbtLoadSchema
            // 
            this.tsbtLoadSchema.Image = ((System.Drawing.Image)(resources.GetObject("tsbtLoadSchema.Image")));
            this.tsbtLoadSchema.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtLoadSchema.Name = "tsbtLoadSchema";
            this.tsbtLoadSchema.Size = new System.Drawing.Size(126, 28);
            this.tsbtLoadSchema.Text = "Load Schema";
            this.tsbtLoadSchema.Click += new System.EventHandler(this.LoadSchemaButtonClick);
            // 
            // tsbSaveSchema
            // 
            this.tsbSaveSchema.Image = global::Capgemini.Xrm.CdsDataMigratorLibrary.Properties.Resource.Save;
            this.tsbSaveSchema.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSaveSchema.Name = "tsbSaveSchema";
            this.tsbSaveSchema.Size = new System.Drawing.Size(124, 28);
            this.tsbSaveSchema.Text = "Save Schema";
            this.tsbSaveSchema.Click += new System.EventHandler(this.SaveSchemaButtonClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbtCurrentConnection
            // 
            this.tsbtCurrentConnection.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbtCurrentConnection.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.tsbtCurrentConnection.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.tsbtCurrentConnection.Name = "tsbtCurrentConnection";
            this.tsbtCurrentConnection.Size = new System.Drawing.Size(0, 28);
            // 
            // schemaLocationControl1
            // 
            this.schemaLocationControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.schemaLocationControl1.Location = new System.Drawing.Point(3, 3);
            this.schemaLocationControl1.Name = "schemaLocationControl1";
            this.schemaLocationControl1.Size = new System.Drawing.Size(775, 29);
            this.schemaLocationControl1.TabIndex = 0;
            // 
            // lmvAttributes
            // 
            this.lmvAttributes.DisplayedItemsName = "Available Attributes";
            this.lmvAttributes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lmvAttributes.Location = new System.Drawing.Point(3, 38);
            this.lmvAttributes.Name = "lmvAttributes";
            this.lmvAttributes.Size = new System.Drawing.Size(775, 263);
            this.lmvAttributes.TabIndex = 1;
            // 
            // lmvRelationships
            // 
            this.lmvRelationships.DisplayedItemsName = "Available Relationships";
            this.lmvRelationships.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lmvRelationships.Location = new System.Drawing.Point(3, 307);
            this.lmvRelationships.Name = "lmvRelationships";
            this.lmvRelationships.Size = new System.Drawing.Size(775, 264);
            this.lmvRelationships.TabIndex = 2;
            // 
            // entityListView1
            // 
            this.entityListView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.entityListView1.Entities = null;
            this.entityListView1.Location = new System.Drawing.Point(3, 38);
            this.entityListView1.Name = "entityListView1";
            this.entityListView1.ShowSystemEntities = false;
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
        private SchemaLocationControl schemaLocationControl1;
        private ListManagerView lmvAttributes;
        private ListManagerView lmvRelationships;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton tsbtRetrieveEntities;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tsbtLoadSchema;
        private System.Windows.Forms.ToolStripButton tsbSaveSchema;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel tsbtCurrentConnection;
    }
}
