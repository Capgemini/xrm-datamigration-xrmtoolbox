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
            this.generatedSchemaLocationControl1 = new Capgemini.Xrm.CdsDataMigratorLibrary.UserControls.GeneratedSchemaLocationControl();
            this.listManagerView1 = new Capgemini.Xrm.CdsDataMigratorLibrary.UserControls.ListManagerView();
            this.listManagerView2 = new Capgemini.Xrm.CdsDataMigratorLibrary.UserControls.ListManagerView();
            this.entityListView1 = new Capgemini.Xrm.CdsDataMigratorLibrary.UserControls.EntityListView();
            this.tlpMainLayout.SuspendLayout();
            this.tlpDetailsPane.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMainLayout
            // 
            this.tlpMainLayout.ColumnCount = 2;
            this.tlpMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tlpMainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMainLayout.Controls.Add(this.tlpDetailsPane, 1, 0);
            this.tlpMainLayout.Controls.Add(this.entityListView1, 0, 0);
            this.tlpMainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMainLayout.Location = new System.Drawing.Point(0, 0);
            this.tlpMainLayout.Name = "tlpMainLayout";
            this.tlpMainLayout.RowCount = 1;
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
            this.tlpDetailsPane.Location = new System.Drawing.Point(303, 3);
            this.tlpDetailsPane.Name = "tlpDetailsPane";
            this.tlpDetailsPane.RowCount = 3;
            this.tlpDetailsPane.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 230F));
            this.tlpDetailsPane.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpDetailsPane.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpDetailsPane.Size = new System.Drawing.Size(831, 609);
            this.tlpDetailsPane.TabIndex = 0;
            // 
            // generatedSchemaLocationControl1
            // 
            this.generatedSchemaLocationControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.generatedSchemaLocationControl1.Location = new System.Drawing.Point(3, 3);
            this.generatedSchemaLocationControl1.Name = "generatedSchemaLocationControl1";
            this.generatedSchemaLocationControl1.Size = new System.Drawing.Size(825, 224);
            this.generatedSchemaLocationControl1.TabIndex = 0;
            // 
            // listManagerView1
            // 
            this.listManagerView1.DisplayedItemsName = "Available Attributes";
            this.listManagerView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listManagerView1.Location = new System.Drawing.Point(3, 233);
            this.listManagerView1.Name = "listManagerView1";
            this.listManagerView1.Size = new System.Drawing.Size(825, 183);
            this.listManagerView1.TabIndex = 1;
            // 
            // listManagerView2
            // 
            this.listManagerView2.DisplayedItemsName = "Available Relationships";
            this.listManagerView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listManagerView2.Location = new System.Drawing.Point(3, 422);
            this.listManagerView2.Name = "listManagerView2";
            this.listManagerView2.Size = new System.Drawing.Size(825, 184);
            this.listManagerView2.TabIndex = 2;
            // 
            // entityListView1
            // 
            this.entityListView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.entityListView1.Location = new System.Drawing.Point(3, 3);
            this.entityListView1.Name = "entityListView1";
            this.entityListView1.Size = new System.Drawing.Size(294, 609);
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
            this.tlpDetailsPane.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMainLayout;
        private System.Windows.Forms.TableLayoutPanel tlpDetailsPane;
        private EntityListView entityListView1;
        private GeneratedSchemaLocationControl generatedSchemaLocationControl1;
        private ListManagerView listManagerView1;
        private ListManagerView listManagerView2;
    }
}
