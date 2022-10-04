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
        IEnumerable<string> EntityListDataSource { get; set; }
        string CurrentCell { get; set; }

        void SetMapFieldToNull();
        void SetRefFieldDataSource(AttributeMetadata[] refFieldAttributes);
        void SetMapFieldDataSource(AttributeMetadata[] mapFieldAttributes);
        void SetBothMappingFieldsToNull();
        void Close();
    }
}
