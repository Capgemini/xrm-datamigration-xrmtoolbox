using Microsoft.Xrm.Sdk;
using System;
using XrmToolBox.Extensibility;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Presenters
{
    public interface IExportView
    {
        event EventHandler SelectExportLocationHandler;

        event EventHandler SelectExportConfigFileHandler;

        event EventHandler SelectSchemaFileHandler;

        event EventHandler ExportDataHandler;

        event EventHandler CancelHandler;

        event EventHandler<RequestConnectionEventArgs> OnConnectionRequested;

        bool FormatJsonSelected { get; set; }

        bool FormatCsvSelected { get; set; }

        string SaveExportLocation { get; set; }

        string ExportConfigFileLocation { get; set; }

        string ExportSchemaFileLocation { get; set; }

        bool ExportInactiveRecordsChecked { get; set; }

        bool MinimizeJsonChecked { get; set; }

        decimal BatchSize { get; set; }

        IOrganizationService OrganizationService { get; set; }

        string ShowFolderBrowserDialog();

        string ShowFileDialog();
    }
}