using Capgemini.Xrm.CdsDataMigratorLibrary.Enums;
using Capgemini.Xrm.DataMigration.Config;
using Microsoft.Xrm.Sdk;
using System;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Presenters
{
    public interface IImportPageView
    {
        int SaveBatchSize { get; set; }
        bool IgnoreStatuses { get; set; }
        bool IgnoreSystemFields { get; set; }
        string JsonFolderPath { get; set; }

        CrmSchemaConfiguration SchemaConfiguration { get; set; }
        string CrmMigrationToolSchemaPath { get; set; }

        string AskForFilePathToOpen();
        string AskForFilePathToSave(string existingFileName = "");

        DataFormat DataFormat { get; set; }
        IOrganizationService Service { get; }
        
        event EventHandler LoadConfigClicked;
        event EventHandler SaveConfigClicked;
        event EventHandler RunConfigClicked;
        event EventHandler SchemaConfigPathChanged;
    }
}
