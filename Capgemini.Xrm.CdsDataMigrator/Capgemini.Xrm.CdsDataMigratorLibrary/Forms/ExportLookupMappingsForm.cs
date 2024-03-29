﻿using Capgemini.Xrm.CdsDataMigratorLibrary.Presenters;
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
        private bool CurrentCellIsUpdated;
        public event EventHandler OnVisible;
        public event EventHandler OnEntityColumnChanged;
        public event EventHandler OnRefFieldChanged;
        [ExcludeFromCodeCoverage]
        public string CurrentCell { get; set; }
        public string CurrentRowEntityName { get; set; }

        public ExportLookupMappings()
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterParent;
        }

        #region data mappings

        IEnumerable<string> IExportLookupMappingsView.EntityListDataSource
        {
            get => clEntity.Items.Cast<string>();
            set
            {
                clEntity.Items.Clear();
                clEntity.Items.AddRange(value.ToArray());
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
        public void SetMapFieldToNull()
        {
            dgvMappings.Rows[dgvMappings.CurrentCell.RowIndex].Cells[2].Value = null;
        }

        [ExcludeFromCodeCoverage]
        public void SetRefFieldDataSource(AttributeMetadata[] refFieldAttributes)
        {
            (dgvMappings.Rows[dgvMappings.CurrentRow.Index].Cells[1] as DataGridViewComboBoxCell).DataSource = refFieldAttributes.Select(x => x.LogicalName).OrderBy(n => n).ToArray();
        }

        [ExcludeFromCodeCoverage]
        public void SetMapFieldDataSource(AttributeMetadata[] mapFieldAttributes)
        {
            (dgvMappings.Rows[dgvMappings.CurrentRow.Index].Cells[2] as DataGridViewComboBoxCell).DataSource = mapFieldAttributes.Select(x => x.LogicalName).OrderBy(n => n).ToArray();
        }

        [ExcludeFromCodeCoverage]
        public void SetBothMappingFieldsToNull()
        {
            dgvMappings.Rows[dgvMappings.CurrentCell.RowIndex].Cells[1].Value = null;
            dgvMappings.Rows[dgvMappings.CurrentCell.RowIndex].Cells[2].Value = null;
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
            CurrentCellIsUpdated = false;
            CurrentCell = (string)dgvMappings.CurrentCell.Value;
            if (dgvMappings.CurrentCell.IsInEditMode)
            {
                dgvMappings.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
            if (dgvMappings.CurrentCell.ColumnIndex == 0)
            {
                this.OnEntityColumnChanged?.Invoke(sender, e);
                dgvMappings.CurrentCell = dgvMappings.CurrentRow.Cells[1];
                CurrentCellIsUpdated = true;
            }
            if (dgvMappings.CurrentCell.ColumnIndex == 1 && !CurrentCellIsUpdated)
            {
                this.CurrentRowEntityName = (string)dgvMappings.CurrentRow.Cells[0].Value;
                this.OnRefFieldChanged?.Invoke(sender, e);
                dgvMappings.CurrentCell = dgvMappings.CurrentRow.Cells[2];
                CurrentCellIsUpdated = true;
            }
        }

        [ExcludeFromCodeCoverage]
        private void ButtonCloseClick(object sender, EventArgs e)
        {
            Close();
        }

        #endregion
    }
}
