
using Capgemini.Xrm.CdsDataMigratorLibrary.Core;
using Capgemini.Xrm.CdsDataMigratorLibrary.Models;
using Capgemini.Xrm.CdsDataMigratorLibrary.Services;
using Capgemini.Xrm.DataMigration.Config;
using Capgemini.Xrm.DataMigration.Model;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

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

        void Close();
        DialogResult ShowMessage(string message, string caption, MessageBoxButtons buttons, MessageBoxIcon icon);

    }
}
