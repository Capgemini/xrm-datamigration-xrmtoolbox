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
        //TODO: List<string> MigrationConfig { get; set; }
        List<string> AdditionalFieldsToIgnore { get; set; }
        int SaveBatchSize { get; set; }
        string JsonFolderPath { get; set; }
        List<string> EntitiesToSync { get; set; }
        List<string> NoUpsertEntities { get; set; }
        List<string> PluginsToDeactivate { get; set; }
        List<string> ProcessesToDeactivate { get; set; }
        bool DeactivateAllProcesses { get; set; }
        string FilePrefix { get; set; }
        List<string> PassOneReferences { get; set; }
        List<string> FieldsToObfuscate { get; set; }
        //TODO: List<string> NoUpdateEntities { get; set; }
    }
}