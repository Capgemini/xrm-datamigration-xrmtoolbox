using Capgemini.Xrm.CdsDataMigratorLibrary.Enums;
using Microsoft.Xrm.Sdk;
using System.Collections.Generic;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Presenters
{
    public interface IImportPageView
    {
        //TODO: List<string> ExcludedFields { get; set; }
        //TODO: string FetchXMLFolderPath { get; set; }
        string CrmMigrationToolSchemaPath { get; set; }
        Dictionary<string, string> CrmMigrationToolSchemaFilters { get; set; }
        int PageSize { get; set; }
        int BatchSize { get; set; }
        int TopCount { get; set; }
        bool OnlyActiveRecords { get; set; }
        string JsonFolderPath { get; set; }
        bool OneEntityPerBatch { get; set; }
        string FilePrefix { get; set; }
        bool SeperateFilesPerEntity { get; set; }
        //TODO: List<EntityToBeObfuscated> FieldsToObfuscate { get; set; }
        //TODO: Dictionary<string, Dictionary<string, List<string>>> LookupMapping { get; set; }
        DataFormat DataFormat { get; set; }
        IOrganizationService Service { get; }

        string AskForFilePathToOpen();
        string AskForFilePathToSave(string existingFileName = "");
    }
}