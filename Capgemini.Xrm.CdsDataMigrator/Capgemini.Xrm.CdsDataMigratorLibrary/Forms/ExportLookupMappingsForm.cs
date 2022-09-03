using Capgemini.Xrm.CdsDataMigratorLibrary.Presenters;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows.Forms;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Forms
{
    public partial class ExportLookupMappings : Form, IExportLookupMappingsView
    {
        public event EventHandler OnVisible;
        public event EventHandler OnEntityColumnChanged;
        public event EventHandler OnRefFieldChanged;
        public event EventHandler LoadMappedItems;
        public DataGridView LookupMappings { get; set; }

        public ExportLookupMappings()
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterParent;
        }

        #region data mappings

        IEnumerable<string> IExportLookupMappingsView.EntityList
        {
            get => clEntity.Items.Cast<string>();
            set
            {
                clEntity.Items.Clear();
                clEntity.Items.AddRange(value.ToArray());
            }
        }

        [ExcludeFromCodeCoverage]
        AttributeMetadata[] IExportLookupMappingsView.RefFieldLookups
        {
            set
            {
                (dgvMappings.Rows[dgvMappings.CurrentRow.Index].Cells[1] as DataGridViewComboBoxCell).DataSource = value.Select(x => x.LogicalName).OrderBy(n => n).ToArray();
            }
        }

        [ExcludeFromCodeCoverage]
        AttributeMetadata[] IExportLookupMappingsView.MapFieldLookups
        {
            set
            {   
                (dgvMappings.Rows[dgvMappings.CurrentRow.Index].Cells[2] as DataGridViewComboBoxCell).DataSource = value.Select(x => x.LogicalName).OrderBy(n => n).ToArray();
            }
        }

        public List<DataGridViewRow> Mappings
        {
            get
            {
                List<DataGridViewRow> mappings = new List<DataGridViewRow>();
                foreach (DataGridViewRow row in dgvMappings.Rows)
                {
                    mappings.Add(row);
                }
                return mappings;
            }
            // setter needed to load existing mappings. Still needs to be implemented
            set
            {
                dgvMappings.Rows.Clear();
                foreach (DataGridViewRow row in value)
                {
                    dgvMappings.Rows.Add(row);
                }
            }
        }

        #endregion

        #region action mappings

        [ExcludeFromCodeCoverage]
        DialogResult IExportLookupMappingsView.ShowMessage(string message, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return MessageBox.Show(message, caption, buttons, icon);
        }

        #endregion

        #region event mappings

        [ExcludeFromCodeCoverage]
        protected override void OnVisibleChanged(EventArgs e)
        {
            if (Visible)
            {
                this.OnVisible?.Invoke(this, e);
            }

            base.OnVisibleChanged(e);
        }

        [ExcludeFromCodeCoverage]
        private void DataGridViewMappingsDefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            var defaultValues = new object[] { null, null, null };
            e.Row.SetValues(defaultValues);
        }

        [ExcludeFromCodeCoverage]
        private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            LookupMappings = dgvMappings;
            if (dgvMappings.CurrentCell.IsInEditMode)
            {
                dgvMappings.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
            if (dgvMappings.CurrentCell.ColumnIndex == 0)
            {
                this.OnEntityColumnChanged?.Invoke(sender, e);
            }
            if (dgvMappings.CurrentCell.ColumnIndex == 1)
            {
                this.OnRefFieldChanged?.Invoke(sender, e);
            }
        }

        private void MappingListLoad(object sender, EventArgs e)
        {
            dgvMappings.Rows.Clear();
            dgvMappings.Refresh();
            this.LoadMappedItems?.Invoke(sender, e);
        }

        [ExcludeFromCodeCoverage]
        private void ButtonCloseClick(object sender, EventArgs e)
        {
            Close();
        }

        #endregion
    }
}
