using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core;
using Microsoft.Xrm.Sdk;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Forms
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

        private void MappingListLoad(object sender, EventArgs e)
        {
            // Add mappings
            foreach (var m in mappings)
            {
                var vals = new object[2] { m.Key.Id.ToString(), m.Value.Id.ToString() };
                dgvMappings.Rows.Add(vals);
            }
        }

        private void DataGridViewMappingsDefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            var blank = new object[2] { Guid.Empty.ToString(), Guid.Empty.ToString() };
            e.Row.SetValues(blank);
        }

        private void DataGridViewMappingsCellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string column = dgvMappings.Columns[e.ColumnIndex].Name;

            // Abort validation if cell is not in the CompanyName column.
            if (column.Equals("clEntity", StringComparison.InvariantCulture))
            {
                if (e.FormattedValue == null)
                {
                    dgvMappings.Rows[e.RowIndex].ErrorText = "Entity must not be empty";
                    e.Cancel = true;
                }
            }
            else if (!Guid.TryParse(e.FormattedValue.ToString(), out Guid dummy))
            {
                // Check on valid GUID
                dgvMappings.Rows[e.RowIndex].ErrorText = string.Format("{0} is not a valid GUID", dgvMappings.Columns[e.ColumnIndex].HeaderText);
                e.Cancel = true;
            }
        }

        private void ButtonCloseClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}