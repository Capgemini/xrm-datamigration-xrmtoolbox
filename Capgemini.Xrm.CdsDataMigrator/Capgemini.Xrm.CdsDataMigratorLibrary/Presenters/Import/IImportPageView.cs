using Capgemini.Xrm.CdsDataMigratorLibrary.Enums;
using Microsoft.Xrm.Sdk;
using System.Collections.Generic;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Presenters
{
    public interface IImportPageView
    {
        bool IgnoreStatuses { get; set; }
        List<string> IgnoreStatusesExceptions { get; set; }
        bool IgnoreSystemFields { get; set; }
        //TODO: Confirm if I actually need the schema filters
        //Dictionary<string, string> CrmMigrationToolSchemaFilters { get; set; }
        //TODO: List<string> MigrationConfig { get; set; }
        List<string> AdditionalFieldsToIgnore { get; set; }
        // TODO: add setter to SaveBatchSize
        int SaveBatchSize { get; set; }
        string JsonFolderPath { get; set; }
        List<string> EntitiesToSync { get; set; }
        List<string> NoUpsertEntities { get; set; }
        List<string> PluginsToDeactivate { get; set; }
        List<string> ProcessesToDeactivate { get; set; }
        // TODO: add setter to DeactivateAllProcesses
        bool DeactivateAllProcesses { get; }
        string FilePrefix { get; set; }
        //TODO: List<string> PassOneReferences { get; set; }
        List<string> FieldsToObfuscate { get; set; }
        //TODO: List<string> NoUpdateEntities { get; set; }


        //TODO: Confirm below
        string AskForFilePathToOpen();
        string AskForFilePathToSave(string existingFileName = "");

        IOrganizationService Service { get; }
        DataFormat DataFormat { get; set; }
    }
}