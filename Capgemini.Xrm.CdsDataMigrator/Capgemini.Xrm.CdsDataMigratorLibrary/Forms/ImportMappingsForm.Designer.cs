namespace Capgemini.Xrm.CdsDataMigratorLibrary.Forms
{
    partial class ImportMappingsForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnClose = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvMappings = new System.Windows.Forms.DataGridView();
            this.clSourceID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clTargetID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clEntity = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMappings)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(1040, 382);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 28);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.ButtonCloseClick);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(-5, 2);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1145, 74);
            this.panel1.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 36);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(459, 38);
            this.label2.TabIndex = 1;
            this.label2.Text = "The references in this list will be replaced in the source dataset before the \r\nt" +
    "ransfer to the target environment";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 5);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mapping list";
            // 
            // dgvMappings
            // 
            this.dgvMappings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMappings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMappings.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clSourceID,
            this.clTargetID,
            this.clEntity});
            this.dgvMappings.Location = new System.Drawing.Point(15, 85);
            this.dgvMappings.Margin = new System.Windows.Forms.Padding(4);
            this.dgvMappings.Name = "dgvMappings";
            this.dgvMappings.RowHeadersWidth = 51;
            //this.dgvMappings.CurrentCellDirtyStateChanged += new System.EventHandler(this.GridViewMappingsCurrentCellDirtyStateChanged); 
            this.dgvMappings.Size = new System.Drawing.Size(1125, 289);
            this.dgvMappings.TabIndex = 4;
            //this.dgvMappings.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.DataGridViewMappingsCellValidating);
            this.dgvMappings.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvMappings_DataError);
            this.dgvMappings.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.DataGridViewMappingsDefaultValuesNeeded);
            // 
            // clSourceID
            // 
            this.clSourceID.HeaderText = "Source ID";
            this.clSourceID.MinimumWidth = 6;
            this.clSourceID.Name = "clSourceID";
            this.clSourceID.Width = 310;
            // 
            // clTargetID
            // 
            this.clTargetID.HeaderText = "Target ID";
            this.clTargetID.MinimumWidth = 6;
            this.clTargetID.Name = "clTargetID";
            this.clTargetID.Width = 310;
            // 
            // clEntity
            // 
            this.clEntity.HeaderText = "Entity";
            this.clEntity.MinimumWidth = 6;
            this.clEntity.Name = "clEntity";
            this.clEntity.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clEntity.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.clEntity.Width = 170;
            // 
            // MappingList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1157, 421);
            this.ControlBox = false;
            this.Controls.Add(this.dgvMappings);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnClose);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MappingList";
            this.Text = "Mappings";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMappings)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvMappings;
        private System.Windows.Forms.DataGridViewTextBoxColumn clSourceID;
        private System.Windows.Forms.DataGridViewTextBoxColumn clTargetID;
        private System.Windows.Forms.DataGridViewComboBoxColumn clEntity;
    }
}