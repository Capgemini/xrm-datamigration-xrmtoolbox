using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Capgemini.Xrm.CdsDataMigratorLibrary.Core;
using Microsoft.Xrm.Sdk;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Forms
{
    /// <summary>
    /// Implementation of MappingList.
    /// </summary>
    public partial class MappingList : Form
    {
        private readonly List<Item<EntityReference, EntityReference>> mappings;

        public MappingList(List<Item<EntityReference, EntityReference>> mappings)
        {
            this.mappings = mappings;
            InitializeComponent();
        }

        public List<Item<EntityReference, EntityReference>> GetMappingList(string entityLogicalName)
        {
            var list = new List<Item<EntityReference, EntityReference>>();

            foreach (DataGridViewRow m in dgvMappings.Rows)
            {
                if (!m.IsNewRow)
                {
                    var sourceid = Guid.Parse((string)m.Cells[0].Value);
                    var targetid = Guid.Parse((string)m.Cells[1].Value);
                    list.Add(new Item<EntityReference, EntityReference>(new EntityReference(entityLogicalName, sourceid), new EntityReference(entityLogicalName, targetid)));
                }
            }

            return list;
        }

        public Dictionary<Guid, Guid> GetGuidMappingList()
        {
            var dictionary = new Dictionary<Guid, Guid>();
            foreach (DataGridViewRow m in dgvMappings.Rows)
            {
                if (!m.IsNewRow)
                {
                    var sourceid = Guid.Parse((string)m.Cells[0].Value);
                    var targetid = Guid.Parse((string)m.Cells[1].Value);
                    dictionary.Add(sourceid, targetid);
                }
            }

            return dictionary;
        }

        public void PopulateMappingGrid()
        {
            foreach (var m in mappings)
            {
                var vals = new object[2] { m.Key.Id.ToString(), m.Value.Id.ToString() };
                dgvMappings.Rows.Add(vals);
            }
        }

        public bool PerformMappingsCellValidation(string column, object formattedValue, int rowIndex, int columnIndex)
        {
            // Abort validation if cell is not in the CompanyName column.
            if (column.Equals("clEntity", StringComparison.InvariantCulture))
            {
                if (formattedValue == null)
                {
                    dgvMappings.Rows[rowIndex].ErrorText = "Entity must not be empty";
                    return true;
                }
            }
            else if (formattedValue == null || !Guid.TryParse(formattedValue.ToString(), out Guid dummy))
            {
                // Check on valid GUID
                dgvMappings.Rows[rowIndex].ErrorText = $"{dgvMappings.Columns[columnIndex].HeaderText} is not a valid GUID";
                return true;
            }

            return false;
        }

        private void MappingListLoad(object sender, EventArgs e)
        {
            PopulateMappingGrid();
        }

        private void DataGridViewMappingsDefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            var blank = new object[2] { Guid.Empty.ToString(), Guid.Empty.ToString() };
            e.Row.SetValues(blank);
        }

        private void DataGridViewMappingsCellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            var column = dgvMappings.Columns[e.ColumnIndex].Name;
            e.Cancel = PerformMappingsCellValidation(column, e.FormattedValue, e.RowIndex, e.ColumnIndex);
        }

        private void ButtonCloseClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}