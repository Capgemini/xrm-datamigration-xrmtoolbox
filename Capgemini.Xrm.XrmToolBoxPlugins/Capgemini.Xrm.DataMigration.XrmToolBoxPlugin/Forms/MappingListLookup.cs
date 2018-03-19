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
        private readonly string _selctedValue;

        public MappingListLookup(Dictionary<string, Dictionary<string, List<string>>> mappings, IOrganizationService orgService, List<EntityMetadata> metadata, string selectedValue)
        {
            this._metCache = metadata.ToList();
            this._selctedValue = selectedValue;
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
                        var mapKey = row.Cells[0].Value.ToString();

                        if (!_mappings.ContainsKey(mapKey))
                        {
                            _mappings.Add(mapKey, new Dictionary<string, List<string>>());
                        }

                        var entKey = row.Cells[1].Value.ToString();
                        if (!_mappings[mapKey].ContainsKey(entKey))
                        {
                            _mappings[mapKey].Add(entKey, new List<string>());
                        }

                        if (_mappings[mapKey][entKey].Contains(row.Cells[2].Value.ToString()))
                            throw new Exception($"Duplicated entry {mapKey} {entKey} {row.Cells[2].Value.ToString()}");

                        _mappings[mapKey][entKey].Add(row.Cells[2].Value.ToString());
                                             
                    }
                }
            }
        }

        private void MappingListLoad(object sender, EventArgs e)
        {
            dgvMappings.Rows.Clear();
            dgvMappings.Refresh();
            //Add mappings
            int rowCount = 0;
            foreach (var m in _mappings)
            {
                foreach (var m2 in m.Value)
                {
                    foreach (var m2Value in m2.Value)
                    {
                        var vals = new object[] { m.Key, m2.Key, m2Value };
                        dgvMappings.Rows.Add(vals);

                        ValidateEntitytColumn(rowCount, m.Key);
                        ValidateLookupColumn(rowCount, m2.Key);

                        rowCount++;
                    }
                   
                }
            }

        }

        private void dgvMappings_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            var blank = new object[] { _selctedValue, null, null };
            e.Row.SetValues(blank);

            if (!string.IsNullOrWhiteSpace(_selctedValue))
                ValidateEntitytColumn(e.Row.Index, _selctedValue);
        }

        private void ValidateLookupColumn(int rowIndex, string newValue)
        {
            var lookup = (LookupAttributeMetadata)((AttributeMetadata[])dgvMappings.Rows[rowIndex].Tag).Single(a => a.LogicalName == newValue);

            var entitymeta = MetadataHelper.RetrieveEntities(lookup.Targets[0], _orgService);

            var fields = entitymeta.Attributes.OrderBy(p=>p.LogicalName).Select(a => a.LogicalName).ToArray();

            (dgvMappings.Rows[rowIndex].Cells[2] as DataGridViewComboBoxCell).DataSource = fields;
        }

        private void ValidateEntitytColumn(int rowIndex, string newValue)
        {
            var entitymeta = MetadataHelper.RetrieveEntities(newValue, _orgService);
            var lookups = entitymeta.Attributes.Where(a => a.AttributeType == AttributeTypeCode.Lookup).OrderBy(p=>p.LogicalName).ToArray();
            dgvMappings.Rows[rowIndex].Tag = lookups;

            (dgvMappings.Rows[rowIndex].Cells[1] as DataGridViewComboBoxCell).DataSource = lookups;
            (dgvMappings.Rows[rowIndex].Cells[1] as DataGridViewComboBoxCell).DisplayMember = "LogicalName";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvMappings_SelectionChanged(object sender, EventArgs e)
        {
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