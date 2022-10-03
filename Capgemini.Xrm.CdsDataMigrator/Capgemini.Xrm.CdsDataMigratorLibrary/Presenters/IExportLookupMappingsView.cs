using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Presenters
{
    public interface IExportLookupMappingsView
    {
        event EventHandler OnVisible;
        event EventHandler OnEntityColumnChanged;
        event EventHandler OnRefFieldChanged;

        string CurrentRowEntityName { get; set; }
        List<string> MappingCells { set; }
        IEnumerable<string> EntityListDataSource { get; set; }
        AttributeMetadata[] SetRefFieldDataSource { set; }
        AttributeMetadata[] SetMapFieldDataSource { set; }
        string CurrentCell { get; set; }

        void SetMapFieldToNull();
        void Close();
    }
}
