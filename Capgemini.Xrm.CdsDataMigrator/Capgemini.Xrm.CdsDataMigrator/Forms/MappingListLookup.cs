using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Capgemini.Xrm.DataMigration.XrmToolBox.Services;
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
        private readonly IMetadataService metadataService;
        private readonly List<EntityMetadata> metaDataCache;
        private readonly string selctedValue;

        public MappingListLookup(Dictionary<string, Dictionary<string, List<string>>> mappings, IOrganizationService orgService, List<EntityMetadata> metadata, string selectedValue, IMetadataService metadataService)
        {
            metaDataCache = metadata.ToList();
            selctedValue = selectedValue;
            this.mappings = mappings;
            this.orgService = orgService;
            InitializeComponent();

            Column1.Items.AddRange(metaDataCache.Select(e => e.LogicalName).OrderBy(n => n).ToArray());
            this.metadataService = metadataService;
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
                        throw new MappingException($"Duplicated entry {mapKey} {entKey} {row.Cells[2].Value}");
                    }

                    mappings[mapKey][entKey].Add(row.Cells[2].Value.ToString());
                }
            }
        }

        public void LoadMappedItems()
        {
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
                        ValidateLookupColumn(rowCount, m2.Key, (AttributeMetadata[])dgvMappings.Rows[rowCount].Tag);

                        rowCount++;
                    }
                }
            }
        }

        public void ValidateLookupColumn(int rowIndex, string newValue, AttributeMetadata[] allAttributes)
        {
            //var allAttributes = (AttributeMetadata[])dgvMappings.Rows[rowIndex].Tag;
            var attr = allAttributes.SingleOrDefault(a => a.LogicalName == newValue);

            string[] fields = null;
            string logicalName = string.Empty;

            if (attr == null)
            {
                throw new MappingException($"schema logical name {newValue} does not exist in the attribue metadata. Please ensure the field exists with that name and the schema is updated accordingly.");
            }

            if (attr.AttributeType == AttributeTypeCode.Uniqueidentifier)
            {
                logicalName = attr.EntityLogicalName;
                // var entitymeta = metadataService.RetrieveEntities(attr.EntityLogicalName, orgService);
                // fields = entitymeta.Attributes.OrderBy(p => p.LogicalName).Select(a => a.LogicalName).ToArray();
                //(dgvMappings.Rows[rowIndex].Cells[2] as DataGridViewComboBoxCell).DataSource = fields;
            }
            //Temporary fix to support Owner as SystemUser only, needs fixing data migration engine to support OwningUser , OwningTeam or OwningBu
            else if (attr.AttributeType == AttributeTypeCode.Owner)
            {
                logicalName = "systemuser";
                //var entitymeta = metadataService.RetrieveEntities("systemuser", orgService);
                // fields = entitymeta.Attributes.OrderBy(p => p.LogicalName).Select(a => a.LogicalName).ToArray();
                //(dgvMappings.Rows[rowIndex].Cells[2] as DataGridViewComboBoxCell).DataSource = fields;
            }
            else if (attr.AttributeType == AttributeTypeCode.Lookup)
            {
                var lookup = attr as LookupAttributeMetadata;
                logicalName = lookup.Targets[0];
                //var entitymeta = metadataService.RetrieveEntities(lookup.Targets[0], orgService);
                // fields = entitymeta.Attributes.OrderBy(p => p.LogicalName).Select(a => a.LogicalName).ToArray();
                //(dgvMappings.Rows[rowIndex].Cells[2] as DataGridViewComboBoxCell).DataSource = fields;
            }
            else
            {
                throw new MappingException($"schema logical name {newValue}, not supported attribute type: {attr.AttributeType} .");
            }

            var entitymeta = metadataService.RetrieveEntities(logicalName, orgService);
            fields = entitymeta.Attributes.OrderBy(p => p.LogicalName).Select(a => a.LogicalName).ToArray();
            (dgvMappings.Rows[rowIndex].Cells[2] as DataGridViewComboBoxCell).DataSource = fields;
        }

        private void MappingListLoad(object sender, EventArgs e)
        {
            dgvMappings.Rows.Clear();
            dgvMappings.Refresh();
            LoadMappedItems();
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

        private void ValidateEntitytColumn(int rowIndex, string newValue)
        {
            var entitymeta = metadataService.RetrieveEntities(newValue, orgService);
            var lookups = entitymeta.Attributes.Where(a => a.AttributeType == AttributeTypeCode.Lookup || a.AttributeType == AttributeTypeCode.Owner || a.AttributeType == AttributeTypeCode.Uniqueidentifier).OrderBy(p => p.LogicalName).ToArray();

            if (dgvMappings.Rows != null && dgvMappings.Rows.Count > rowIndex && dgvMappings.Rows[rowIndex].Cells.Count > 0)
            {
                dgvMappings.Rows[rowIndex].Tag = lookups;

                (dgvMappings.Rows[rowIndex].Cells[1] as DataGridViewComboBoxCell).DataSource = lookups;
                (dgvMappings.Rows[rowIndex].Cells[1] as DataGridViewComboBoxCell).DisplayMember = "LogicalName";
            }
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
                        ValidateLookupColumn(cell.RowIndex, newValue, (AttributeMetadata[])dgvMappings.Rows[cell.RowIndex].Tag);
                        dgvMappings.CurrentCell = dgvMappings.Rows[cell.RowIndex].Cells[2];
                    }
                    else if (cell.ColumnIndex == 2)
                    {
                        dgvMappings.CurrentCell = dgvMappings.Rows[cell.RowIndex + 1].Cells[0];
                    }
                }
            }
        }

        private void dgvMappings_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //Temporary solution to hide critical failure
            e.Cancel = true;
        }
    }
}