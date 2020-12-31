using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Exceptions;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Forms
{
    /// <summary>
    /// Implementation of MappingListLookup.
    /// </summary>
    public partial class MappingListLookup : Form
    {
        private readonly Dictionary<string, Dictionary<string, List<string>>> mappings;
        private readonly IOrganizationService orgService;
        private readonly List<EntityMetadata> metCache;
        private readonly string selctedValue;

        public MappingListLookup(Dictionary<string, Dictionary<string, List<string>>> mappings, IOrganizationService orgService, List<EntityMetadata> metadata, string selectedValue)
        {
            metCache = metadata.ToList();
            selctedValue = selectedValue;
            this.mappings = mappings;
            this.orgService = orgService;
            InitializeComponent();

            Column1.Items.AddRange(metCache.Select(e => e.LogicalName).OrderBy(n => n).ToArray());
        }

        public void RefreshMappingList()
        {
            mappings.Clear();

            foreach (DataGridViewRow row in dgvMappings.Rows)
            {
                if (!row.IsNewRow && row.Cells[0].Value != null && row.Cells[1].Value != null && row.Cells[2].Value != null)
                {
                    var mapKey = row.Cells[0].Value.ToString();

                    if (!mappings.ContainsKey(mapKey))
                    {
                        mappings.Add(mapKey, new Dictionary<string, List<string>>());
                    }

                    var entKey = row.Cells[1].Value.ToString();
                    if (!mappings[mapKey].ContainsKey(entKey))
                    {
                        mappings[mapKey].Add(entKey, new List<string>());
                    }

                    if (mappings[mapKey][entKey].Contains(row.Cells[2].Value.ToString()))
                    {
                        throw new MappingException($"Duplicated entry {mapKey} {entKey} {row.Cells[2].Value.ToString()}");
                    }

                    mappings[mapKey][entKey].Add(row.Cells[2].Value.ToString());
                }
            }
        }

        private void MappingListLoad(object sender, EventArgs e)
        {
            dgvMappings.Rows.Clear();
            dgvMappings.Refresh();

            int rowCount = 0;
            foreach (var m in mappings)
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

        private void DataGridViewMappingsDefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            var blank = new object[] { selctedValue, null, null };
            e.Row.SetValues(blank);

            if (!string.IsNullOrWhiteSpace(selctedValue))
            {
                ValidateEntitytColumn(e.Row.Index, selctedValue);
            }
        }

        private void ValidateLookupColumn(int rowIndex, string newValue)
        {
            var lookup = (LookupAttributeMetadata)((AttributeMetadata[])dgvMappings.Rows[rowIndex].Tag).Single(a => a.LogicalName == newValue);

            var entitymeta = MetadataHelper.RetrieveEntities(lookup.Targets[0], orgService);

            var fields = entitymeta.Attributes.OrderBy(p => p.LogicalName).Select(a => a.LogicalName).ToArray();

            (dgvMappings.Rows[rowIndex].Cells[2] as DataGridViewComboBoxCell).DataSource = fields;
        }

        private void ValidateEntitytColumn(int rowIndex, string newValue)
        {
            var entitymeta = MetadataHelper.RetrieveEntities(newValue, orgService);
            var lookups = entitymeta.Attributes.Where(a => a.AttributeType == AttributeTypeCode.Lookup).OrderBy(p => p.LogicalName).ToArray();
            dgvMappings.Rows[rowIndex].Tag = lookups;

            (dgvMappings.Rows[rowIndex].Cells[1] as DataGridViewComboBoxCell).DataSource = lookups;
            (dgvMappings.Rows[rowIndex].Cells[1] as DataGridViewComboBoxCell).DisplayMember = "LogicalName";
        }

        private void CloseButtonClick(object sender, EventArgs e)
        {
            Close();
        }

        private void GridViewMappingsCurrentCellDirtyStateChanged(object sender, EventArgs e)
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