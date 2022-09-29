using Capgemini.Xrm.DataMigration.Config;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Presenters
{
    public interface IImportMappingsFormView
    {
        CrmSchemaConfiguration SchemaConfiguration { get; }
        IEnumerable<string> EntityListDataSource { get; set; }
        List<DataGridViewRow> Mappings { get; set; }

        event EventHandler OnVisible;
        
        void Close();
    }
}
