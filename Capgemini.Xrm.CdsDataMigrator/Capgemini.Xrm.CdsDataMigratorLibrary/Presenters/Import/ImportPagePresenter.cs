using Capgemini.Xrm.CdsDataMigratorLibrary.Services;
using Capgemini.Xrm.DataMigration.Config;
using Capgemini.Xrm.DataMigration.CrmStore.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Presenters
{
    public class ImportPagePresenter
    {
        private readonly IImportPageView view;
        private readonly IWorkerHost workerHost;
        private readonly IDataMigrationService dataMigrationService;

        private CrmImportConfig config;
        private string configFilePath;

        public ImportPagePresenter(IImportPageView view, IWorkerHost workerHost, IDataMigrationService dataMigrationService)
        {
            this.view = view;
            this.workerHost = workerHost;
            this.dataMigrationService = dataMigrationService;

            this.config = new CrmImportConfig();
            WriteFormInputFromConfig();
        }

        public void LoadConfig()
        {
            try
            {
                configFilePath = view.AskForFilePathToOpen();
                if (!File.Exists(configFilePath))
                {
                    return;
                }
                config = CrmImportConfig.GetConfiguration(configFilePath);
                WriteFormInputFromConfig();
            }
            catch
            {
                // TODO: Handle execption. 
            }
        }

        public void SaveConfig()
        {
            try
            {
                ReadFormInputIntoConfig();
                // TODO: Validate Config
                configFilePath = view.AskForFilePathToSave(configFilePath);
                if (File.Exists(configFilePath))
                {
                    File.Delete(configFilePath);
                }
                config.SaveConfiguration(configFilePath);
            }
            catch
            {
                // TODO: Handle exception
            }
        }

        public void RunConfig()
        {
            try
            {
                ReadFormInputIntoConfig();
                // TODO: Validate Config
                workerHost.WorkAsync(new WorkAsyncInfo
                {
                    Message = "Importing data...",
                    Work = (bw, e) => dataMigrationService.ImportData(view.Service, view.DataFormat, config),
                    PostWorkCallBack = (e) =>
                    {
                        if (e.Error != null)
                        {
                            // TODO: Handle error
                        }

                        // TODO: Success message
                    }
                });
            }
            catch
            {
                // TODO: Handle exception
            }
        }

        public CrmSchemaConfiguration GetSchemaConfiguration()
        {
            if (string.IsNullOrWhiteSpace(view.CrmMigrationToolSchemaPath) || !File.Exists(view.CrmMigrationToolSchemaPath))
            {
                return null;
            }

            try
            {
                return CrmSchemaConfiguration.ReadFromFile(view.CrmMigrationToolSchemaPath);
            }
            catch
            {
                return null;
            }
        }

        private void ReadFormInputIntoConfig()
        {
            //config.BatchSize = view.BatchSize;
            //config.PageSize = view.PageSize;
            //config.TopCount = view.TopCount;
            //config.CrmMigrationToolSchemaPaths.Clear();
            //config.CrmMigrationToolSchemaPaths.Add(view.CrmMigrationToolSchemaPath);
            //config.JsonFolderPath = view.JsonFolderPath;
            //config.OnlyActiveRecords = view.OnlyActiveRecords;
            //config.OneEntityPerBatch = view.OneEntityPerBatch;
            //config.SeperateFilesPerEntity = view.SeperateFilesPerEntity;
            //config.FilePrefix = view.FilePrefix;
            //config.CrmMigrationToolSchemaFilters.Clear();
            //if (view.CrmMigrationToolSchemaFilters != null)
            //{
            //    foreach (var filter in view.CrmMigrationToolSchemaFilters)
            //    {
            //        config.CrmMigrationToolSchemaFilters.Add(filter.Key, filter.Value);
            //    }
            //}
        }

        private void WriteFormInputFromConfig()
        {
            //view.BatchSize = config.BatchSize;
            //view.PageSize = config.PageSize;
            //view.TopCount = config.TopCount;
            //view.CrmMigrationToolSchemaPath = config.CrmMigrationToolSchemaPaths.FirstOrDefault();
            //view.JsonFolderPath = config.JsonFolderPath;
            //view.OnlyActiveRecords = config.OnlyActiveRecords;
            //view.OneEntityPerBatch = config.OneEntityPerBatch;
            //view.SeperateFilesPerEntity = config.SeperateFilesPerEntity;
            //view.FilePrefix = config.FilePrefix;
            //view.CrmMigrationToolSchemaFilters = new Dictionary<string, string>(config.CrmMigrationToolSchemaFilters);
        }

    }
}