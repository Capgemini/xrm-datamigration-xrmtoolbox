using Capgemini.Xrm.CdsDataMigratorLibrary.Services;
using Capgemini.Xrm.DataMigration.Config;
using Capgemini.Xrm.DataMigration.CrmStore.Config;
using System.Diagnostics.CodeAnalysis;
using System;
using System.IO;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Presenters
{
    public class ImportPagePresenter : IDisposable
    {
        private readonly IImportPageView view;
        private readonly IWorkerHost workerHost;
        private readonly IDataMigrationService dataMigrationService;
        
        private CrmImportConfig config;
        private string configFilePath;
        private readonly INotifier notifier;

        public ImportPagePresenter(IImportPageView view, IWorkerHost workerHost, IDataMigrationService dataMigrationService, INotifier notifier)
        {
            this.view = view;
            this.workerHost = workerHost;
            this.dataMigrationService = dataMigrationService;
            this.notifier = notifier;

            this.view.LoadConfigClicked += LoadConfig;
            this.view.SaveConfigClicked += SaveConfig;
            this.view.RunConfigClicked += RunConfig;
            this.view.SchemaConfigPathChanged += SchemaConfigPathChanged;

            this.config = new CrmImportConfig();
            WriteFormInputFromConfig();
        }

        private void LoadConfig(object sender, EventArgs args)
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

        private void SaveConfig(object sender, EventArgs args)
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

        private void RunConfig(object sender, EventArgs args)
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

        private void SchemaConfigPathChanged(object sender, EventArgs args)
        {
            this.view.SchemaConfiguration = GetSchemaConfiguration();
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
            config.SaveBatchSize = view.SaveBatchSize;
            config.IgnoreStatuses = view.IgnoreStatuses;
            config.IgnoreSystemFields = view.IgnoreSystemFields;
            config.JsonFolderPath = view.JsonFolderPath;
        }

        private void WriteFormInputFromConfig()
        {
            view.SaveBatchSize = config.SaveBatchSize;
            view.IgnoreStatuses = config.IgnoreStatuses;
            view.IgnoreSystemFields = config.IgnoreSystemFields;
            view.JsonFolderPath = config.JsonFolderPath;
        }

        [ExcludeFromCodeCoverage]
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        [ExcludeFromCodeCoverage]
        protected virtual void Dispose(bool disposing)
        {
            this.view.LoadConfigClicked -= LoadConfig;
            this.view.SaveConfigClicked -= SaveConfig;
            this.view.RunConfigClicked -= RunConfig;
        }
    }
}