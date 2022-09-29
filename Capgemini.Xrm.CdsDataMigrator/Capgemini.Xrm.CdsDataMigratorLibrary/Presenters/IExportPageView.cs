using Capgemini.Xrm.CdsDataMigratorLibrary.Enums;
using Capgemini.Xrm.DataMigration.Config;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Presenters
{
    public interface IExportPageView
    {
        string CrmMigrationToolSchemaPath { get; set; }
        CrmSchemaConfiguration SchemaConfiguration { get; set; }
        IOrganizationService Service { get; }
        
        //TODO: List<string> ExcludedFields { get; set; }
        //TODO: string FetchXMLFolderPath { get; set; }
        Dictionary<string, string> CrmMigrationToolSchemaFilters { get; set; }
        int PageSize { get; set; }
        int BatchSize { get; set; }
        int TopCount { get; set; }
        bool OnlyActiveRecords { get; set; }
        string JsonFolderPath { get; set; }
        bool OneEntityPerBatch { get; set; }
        string FilePrefix { get; set; }
        bool SeperateFilesPerEntity { get; set; }
        List<DataGridViewRow> LookupMappings { get; set; }
        //TODO: List<EntityToBeObfuscated> FieldsToObfuscate { get; set; }
        DataFormat DataFormat { get; set; }
        
        IExportLookupMappingsView ExportLookupMappingsForm { get; }
        IExportFilterFormView ExportFilterForm { get; }

        event EventHandler LoadConfigClicked;
        event EventHandler SaveConfigClicked;
        event EventHandler RunConfigClicked;
        event EventHandler SchemaConfigPathChanged;

        string AskForFilePathToOpen();
        string AskForFilePathToSave(string existingFileName = "");
    }
}
