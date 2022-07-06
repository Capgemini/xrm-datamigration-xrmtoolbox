using Capgemini.Xrm.CdsDataMigratorLibrary.Models;
using Capgemini.Xrm.CdsDataMigratorLibrary.Presenters;
using Capgemini.Xrm.DataMigration.Config;
using Capgemini.Xrm.DataMigration.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows.Forms;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Forms
{
    public partial class ExportFilterForm : Form, IExportFilterFormView
    {
        private readonly IExportFilterFormPresenter presenter;

        public ExportFilterForm(IExportFilterFormPresenter presenter = null)
        {
            InitializeComponent();

            this.presenter = presenter ?? new ExportFilterFormPresenter(this);

            StartPosition = FormStartPosition.CenterParent;
        }

        #region data mappings

        public Dictionary<string, string> EntityFilters { get; set; } = new Dictionary<string, string>();

        public CrmSchemaConfiguration SchemaConfiguration { get; set; }

        IEnumerable<ListBoxItem<CrmEntity>> IExportFilterFormView.EntityList
        {
            get => lbxEntityNames.Items.Cast<ListBoxItem<CrmEntity>>();
            set
            {
                lbxEntityNames.Items.Clear();
                lbxEntityNames.Items.AddRange(value.ToArray());
            }
        }

        CrmEntity IExportFilterFormView.SelectedEntity
        {
            get => ((ListBoxItem<CrmEntity>)lbxEntityNames.SelectedItem).Item;
            set => lbxEntityNames.SelectedItem = ((IExportFilterFormView)this).EntityList.First(x => x.Item == value);
        }

        string IExportFilterFormView.FilterText
        {
            get => tbxFetchXmlFilter.Text;
            set => tbxFetchXmlFilter.Text = value;
        }

        #endregion

        #region action mappings

        DialogResult IExportFilterFormView.ShowMessage(string message, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return MessageBox.Show(message, caption, buttons, icon);
        }

        #endregion

        #region event mappings

        protected override void OnVisibleChanged(EventArgs e)
        {
            if (Visible)
            {
                presenter.OnVisible();
            }

            base.OnVisibleChanged(e);
        }

        private void lbxEntityNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            presenter.OnEntitySelected();
        }

        private void tbxFilterText_TextChanged(object sender, EventArgs e)
        {
            presenter.UpdateFilterForEntity();
        }

        [ExcludeFromCodeCoverage]
        private void btnSave_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion
    }
}
