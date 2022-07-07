using System;
using Microsoft.Xrm.Sdk;
using XrmToolBox.Extensibility;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Presenters
{
    public interface IImportView
    {
        event EventHandler SelectImportLocationHandler;

        event EventHandler SelectImportConfigFileHandler;

        event EventHandler SelectSchemaFileHandler;

        event EventHandler ImportDataHandler;

        event EventHandler CancelHandler;

        event EventHandler<RequestConnectionEventArgs> OnConnectionRequested;

        bool FormatJsonSelected { get; set; }

        bool FormatCsvSelected { get; set; }

        string SaveImportLocation { get; set; }

        string ImportConfigFileLocation { get; set; }

        string ImportSchemaFileLocation { get; set; }

        bool ImportInactiveRecordsChecked { get; set; }

        bool MinimizeJsonChecked { get; set; }

        decimal BatchSize { get; set; }

        IOrganizationService OrganizationService { get; set; }

        string ShowFolderBrowserDialog();

        string ShowFileDialog();
    }
}