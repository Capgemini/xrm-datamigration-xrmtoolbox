using Capgemini.Xrm.CdsDataMigratorLibrary.Presenters;
using Capgemini.Xrm.DataMigration.Config;
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

        public ExportLookupMappings()
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterParent;
        }

        #region data mappings

        public CrmSchemaConfiguration SchemaConfiguration { get; set; }

        IEnumerable<string> IExportLookupMappingsView.EntityList
        {
            get => clEntity.Items.Cast<string>();
            set
            {
                clEntity.Items.Clear();
                clEntity.Items.AddRange(value.ToArray());
            }
        }

        public List<DataGridViewRow> LookupMappings
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
            var cell = dgvMappings.CurrentCell;
            if (cell.IsInEditMode)
            {
                dgvMappings.CommitEdit(DataGridViewDataErrorContexts.Commit);
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
