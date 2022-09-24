using Capgemini.Xrm.CdsDataMigratorLibrary.Models;
using Capgemini.Xrm.DataMigration.Config;
using Capgemini.Xrm.DataMigration.Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Presenters
{
    public interface IExportFilterFormView
    {
        Dictionary<string, string> EntityFilters { get; }
        CrmSchemaConfiguration SchemaConfiguration { get; }
        IEnumerable<ListBoxItem<CrmEntity>> EntityList { get; set; }
        CrmEntity SelectedEntity { get; set; }
        string FilterText { get; set; }

        event EventHandler OnVisible;
        event EventHandler OnEntitySelected;
        event EventHandler OnFilterTextChanged;
    }
}
