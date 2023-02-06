using Capgemini.Xrm.DataMigration.Config;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Presenters
{
    public interface IImportMappingsFormView
    {
        CrmSchemaConfiguration SchemaConfiguration { get; }
        event EventHandler OnVisible;

        IEnumerable<string> EntityListDataSource { get; set; }
        List<DataGridViewRow> Mappings { get; set; }

        void Close();
    }
}
