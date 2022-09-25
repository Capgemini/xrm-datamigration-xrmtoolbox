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
    public partial class ImportMappingsForm : Form, IImportMappingsFormView
    {
        public event EventHandler OnVisible;

        public ImportMappingsForm()
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterParent;
        }

        #region data mappings

        public CrmSchemaConfiguration SchemaConfiguration { get; set; }

        IEnumerable<string> IImportMappingsFormView.EntityListDataSource
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
            var defaultValues = new object[] { clEntity.Items[0], Guid.Empty.ToString(), Guid.Empty.ToString() };
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
