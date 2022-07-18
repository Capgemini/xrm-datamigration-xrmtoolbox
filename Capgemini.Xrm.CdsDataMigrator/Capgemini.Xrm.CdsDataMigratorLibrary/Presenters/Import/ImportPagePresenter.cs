using Capgemini.Xrm.CdsDataMigratorLibrary.Services;
using Capgemini.Xrm.DataMigration.Config;
using Capgemini.Xrm.DataMigration.CrmStore.Config;
using System;
using System.IO;
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
                var schema = GetSchemaConfiguration();
                workerHost.WorkAsync(new WorkAsyncInfo
                {
                    Message = "Importing data...",
                    Work = (bw, e) => dataMigrationService.ImportData(view.Service, view.DataFormat, schema, config),
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
            try
            {
                return CrmSchemaConfiguration.ReadFromFile(view.CrmMigrationToolSchemaPath);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private void ReadFormInputIntoConfig()
        {
            config.SaveBatchSize = view.SaveBatchSize;
            config.IgnoreStatuses = view.IgnoreStatuses;
            config.IgnoreSystemFields = view.IgnoreSystemFields;
            config.JsonFolderPath = view.JsonFolderPath;

            //TO DO: incorporate schema file
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
            view.SaveBatchSize = config.SaveBatchSize;
            view.IgnoreStatuses = config.IgnoreStatuses;
            view.IgnoreSystemFields = config.IgnoreSystemFields;
            view.JsonFolderPath = config.JsonFolderPath;
        }
    }
}