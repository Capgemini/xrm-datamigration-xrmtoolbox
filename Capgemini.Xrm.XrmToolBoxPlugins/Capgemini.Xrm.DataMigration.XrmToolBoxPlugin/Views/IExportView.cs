using System;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using XrmToolBox.Extensibility;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Views
{
    public interface IExportView
    {
        event EventHandler SelectExportLocationHandler;

        event EventHandler SelectExportConfigFileHandler;

        event EventHandler SelectSchemaFileHandler;

        event EventHandler ExportDataHandler;

        event EventHandler<RequestConnectionEventArgs> OnConnectionRequested;

        bool FormatJsonSelected { get; set; }

        bool FormatCsvSelected { get; set; }

        string SaveExportLocation { get; set; }

        string ExportConfigFileLocation { get; set; }

        string ExportSchemaFileLocation { get; set; }

        bool ExportInactiveRecordsChecked { get; set; }

        bool MinimizeJsonChecked { get; set; }

        decimal BatchSize { get; set; }
        CrmServiceClient CrmServiceClient { get; set; }

        string ShowFolderBrowserDialog();

        string ShowFileDialog();

    }
}