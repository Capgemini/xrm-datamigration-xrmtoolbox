
using Capgemini.Xrm.CdsDataMigratorLibrary.Models;
using Capgemini.Xrm.DataMigration.Config;
using Capgemini.Xrm.DataMigration.Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Presenters
{
    public interface IImportMappingsFormView
    {   
        CrmSchemaConfiguration SchemaConfiguration { get; }
        event EventHandler OnVisible;
        IEnumerable<ListBoxItem<CrmEntity>> EntityList { get; set; }

        void Close();
        DialogResult ShowMessage(string message, string caption, MessageBoxButtons buttons, MessageBoxIcon icon);
        
    }
}
