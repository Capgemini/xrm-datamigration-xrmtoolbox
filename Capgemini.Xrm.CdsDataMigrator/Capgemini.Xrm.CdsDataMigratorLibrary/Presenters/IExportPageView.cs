using Capgemini.Xrm.CdsDataMigratorLibrary.Enums;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XrmToolBox.Extensibility;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Presenters
{
    public interface IExportPageView
    {
        //List<string> ExcludedFields { get; set; }
        //string FetchXMLFolderPath { get; set; }
        string CrmMigrationToolSchemaPath { get; set; }
        // Dictionary<string, string> CrmMigrationToolSchemaFilters { get; set; }
        int PageSize { get; set; }
        int BatchSize { get; set; }
        int TopCount { get; set; }
        bool OnlyActiveRecords { get; set; }
        string JsonFolderPath { get; set; }
        bool OneEntityPerBatch { get; set; }
        string FilePrefix { get; set; }
        bool SeperateFilesPerEntity { get; set; }
        //List<EntityToBeObfuscated> FieldsToObfuscate { get; set; }
        //Dictionary<string, Dictionary<string, List<string>>> LookupMapping { get; set; }
        DataFormat DataFormat { get; set; }
        IOrganizationService Service { get; } 

        string AskForFilePathToOpen();
        string AskForFilePathToSave(string existingFileName = "");
    }
}
