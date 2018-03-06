using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Forms
{
    public partial class MappingListLookup : Form
    {
        private Dictionary<string, Dictionary<string, List<string>>> _mappings;
        private IOrganizationService _orgService;
        private readonly List<EntityMetadata> _metCache;

        public MappingListLookup(Dictionary<string, Dictionary<string, List<string>>> mappings, IOrganizationService orgService, List<EntityMetadata> metadata)
        {
            this._metCache = metadata.ToList();

            this._mappings = mappings;
            _orgService = orgService;
            InitializeComponent();

            this.Column1.Items.AddRange(_metCache.Select(e => e.LogicalName).OrderBy(n => n).ToArray());
        }

        public void RefreshMappingList()
        {
            _mappings.Clear();

            foreach (DataGridViewRow row in dgvMappings.Rows)
            {
                if (!row.IsNewRow)
                {
                    if (row.Cells[0].Value != null && row.Cells[1].Value != null && row.Cells[2].Value != null)
                    {
                        if (!_mappings.ContainsKey(row.Cells[0].Value.ToString()))
                        {
                            _mappings.Add(row.Cells[0].Value.ToString(), new Dictionary<string, List<string>>());
                        }
                        _mappings[row.Cells[0].Value.ToString()].Add(row.Cells[1].Value.ToString(), new List<string> { row.Cells[2].Value.ToString() });
                    }
                }
            }
        }

        private void MappingListLoad(object sender, EventArgs e)
        {
            //  dgvMappings.DataSource = _mappings;
            dgvMappings.Rows.Clear();
            dgvMappings.Refresh();
            //Add mappings
            int rowCount = 0;
            foreach (var m in _mappings)
            {
                foreach (var m2 in m.Value)
                {
                    var vals = new object[3] { m.Key.ToString(), m2.Key.ToString(), m2.Value[0].ToString() };
                    dgvMappings.Rows.Add(vals);

                    ValidateEntitytColumn(rowCount, m.Key.ToString());
                    ValidateLookupColumn(rowCount, m2.Key.ToString());

                    rowCount++;
                }
            }
        }

        private void dgvMappings_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            var blank = new object[3] { null, null, null };
            e.Row.SetValues(blank);
        }

        private void dgvMappings_CellValidating(object sender, DataGridViewCellValidatingEventArgs arg)
        {
            //if (!String.IsNullOrWhiteSpace(arg.FormattedValue.ToString()))
            //{
            //    if (arg.ColumnIndex == 0)
            //    {
            //        var newValue = arg.FormattedValue.ToString();
            //        dgvMappings.Rows[arg.RowIndex].Cells[1].Value = null;
            //        dgvMappings.Rows[arg.RowIndex].Cells[2].Value = null;
            //        ValidateEntitytColumn(arg.RowIndex, newValue);
            //    }
            //    else if (arg.ColumnIndex == 1)
            //    {
            //        dgvMappings.Rows[arg.RowIndex].Cells[2].Value = null;
            //        var newValue = arg.FormattedValue.ToString();
            //        ValidateLookupColumn(arg.RowIndex, newValue);

            //    }
            //    else if (arg.ColumnIndex == 2)
            //    {
            //    }
            //}
        }

        private void ValidateLookupColumn(int rowIndex, string newValue)
        {
            var lookup = (LookupAttributeMetadata)((AttributeMetadata[])dgvMappings.Rows[rowIndex].Tag).Single(a => a.SchemaName == newValue);

            var entitymeta = MetadataHelper.RetrieveEntities(lookup.Targets[0], _orgService);

            var fields = entitymeta.Attributes.Select(a => a.LogicalName).ToArray();

            (dgvMappings.Rows[rowIndex].Cells[2] as DataGridViewComboBoxCell).DataSource = fields;
        }

        private void ValidateEntitytColumn(int rowIndex, string newValue)
        {
            var entitymeta = MetadataHelper.RetrieveEntities(newValue, _orgService);
            var lookups = entitymeta.Attributes.Where(a => a.AttributeType == AttributeTypeCode.Lookup).ToArray();
            dgvMappings.Rows[rowIndex].Tag = lookups;

            (dgvMappings.Rows[rowIndex].Cells[1] as DataGridViewComboBoxCell).DataSource = lookups;
            (dgvMappings.Rows[rowIndex].Cells[1] as DataGridViewComboBoxCell).DisplayMember = "SchemaName";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvMappings_SelectionChanged(object sender, EventArgs e)
        {
        }

        private void dgvMappings_CellValueChanged(object sender, DataGridViewCellEventArgs arg)
        {
            //if (arg.ColumnIndex == 0)
            //{
            //    DataGridViewComboBoxCell cell = dgvMappings.Rows[arg.RowIndex].Cells[0] as DataGridViewComboBoxCell;

            //    var newValue = cell.Value;
            //    if (newValue != null)
            //        ValidateEntitytColumn(arg.RowIndex, newValue.ToString());

            //}
            //else if (arg.ColumnIndex == 1)
            //{
            //    DataGridViewComboBoxCell cell = dgvMappings.Rows[arg.RowIndex].Cells[1] as DataGridViewComboBoxCell;

            //    var newValue = cell.Value;
            //    if (newValue != null)
            //        ValidateLookupColumn(arg.RowIndex, newValue.ToString());
            //}
            //else if (arg.ColumnIndex == 2)
            //{
            //}
        }

        private void dgvMappings_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            var cell = dgvMappings.CurrentCell;

            if (cell.IsInEditMode)
            {
                dgvMappings.CommitEdit(DataGridViewDataErrorContexts.Commit);

                if (cell.FormattedValue != null && !string.IsNullOrWhiteSpace(cell.FormattedValue.ToString()))
                {
                    if (cell.ColumnIndex == 0)
                    {
                        var newValue = cell.FormattedValue.ToString();
                        dgvMappings.Rows[cell.RowIndex].Cells[1].Value = null;
                        dgvMappings.Rows[cell.RowIndex].Cells[2].Value = null;
                        ValidateEntitytColumn(cell.RowIndex, newValue);
                        dgvMappings.CurrentCell = dgvMappings.Rows[cell.RowIndex].Cells[1];
                    }
                    else if (cell.ColumnIndex == 1)
                    {
                        dgvMappings.Rows[cell.RowIndex].Cells[2].Value = null;
                        var newValue = cell.FormattedValue.ToString();
                        ValidateLookupColumn(cell.RowIndex, newValue);
                        dgvMappings.CurrentCell = dgvMappings.Rows[cell.RowIndex].Cells[2];
                    }
                    else if (cell.ColumnIndex == 2)
                    {
                        dgvMappings.CurrentCell = dgvMappings.Rows[cell.RowIndex + 1].Cells[0];
                    }
                }
            }
        }
    }
}