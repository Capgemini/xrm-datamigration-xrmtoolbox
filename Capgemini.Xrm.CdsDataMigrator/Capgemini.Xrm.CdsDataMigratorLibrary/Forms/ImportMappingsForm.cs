using Capgemini.Xrm.CdsDataMigratorLibrary.Presenters;
using Capgemini.Xrm.DataMigration.Config;
using System;
using System.Collections.Generic;
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

        IEnumerable<string> IImportMappingsFormView.EntityList
        {
            // Check the get method
            get => clEntity.Items.Cast<string>();
            set
            {
                clEntity.Items.Clear();
                clEntity.Items.AddRange(value.ToArray());
            }
        }

        #endregion

        #region action mappings

        [ExcludeFromCodeCoverage]
        DialogResult IImportMappingsFormView.ShowMessage(string message, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return MessageBox.Show(message, caption, buttons, icon);
        }

        #endregion

        #region event mappings

        protected override void OnVisibleChanged(EventArgs e)
        {
            if (Visible)
            {
                this.OnVisible?.Invoke(this, e);
            }

            base.OnVisibleChanged(e);
        }
        
        private void DataGridViewMappingsDefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {

            var defaultValues = new object[] { Guid.Empty.ToString(), Guid.Empty.ToString(), clEntity.Items[0] };
            e.Row.SetValues(defaultValues);
        }
        
        private void ButtonCloseClick(object sender, EventArgs e)
        {
            Close();
        }

        private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            var cell = dgvMappings.CurrentCell;

            if (cell.IsInEditMode)
            {
                dgvMappings.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        #endregion
    }
}
