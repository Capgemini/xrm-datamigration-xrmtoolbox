using Capgemini.Xrm.CdsDataMigratorLibrary.Enums;
using Microsoft.Xrm.Sdk;
using System.Collections.Generic;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Presenters
{
    public interface IImportPageView
    {
        int SaveBatchSize { get; set; }
        bool IgnoreStatuses { get; set; }
        bool IgnoreSystemFields { get; set; }
        string JsonFolderPath { get; set; }

        string AskForFilePathToOpen();
        string AskForFilePathToSave(string existingFileName = "");

        IOrganizationService Service { get; }
        DataFormat DataFormat { get; set; }
    }
}
