using Capgemini.Xrm.CdsDataMigratorLibrary.Models;
using Capgemini.Xrm.DataMigration.Config;
using Capgemini.Xrm.DataMigration.Model;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Presenters
{
    public interface IImportFilterFormView
    {
        Dictionary<string, string> EntityFilters { get; }
        CrmSchemaConfiguration SchemaConfiguration { get; }
        IEnumerable<ListBoxItem<CrmEntity>> EntityList { get; set; }
        CrmEntity SelectedEntity { get; set; }
        string FilterText { get; set; }

        void Close();
        DialogResult ShowMessage(string message, string caption, MessageBoxButtons buttons, MessageBoxIcon icon);
    }
}
