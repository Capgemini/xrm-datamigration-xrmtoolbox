using Capgemini.Xrm.CdsDataMigratorLibrary.Services;
using Capgemini.Xrm.DataMigration.Config;
using Capgemini.Xrm.DataMigration.CrmStore.Config;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Presenters
{
    public class ExportPagePresenter : IDisposable
    {
        private readonly IExportPageView view;
        private readonly IWorkerHost workerHost;
        private readonly IDataMigrationService dataMigrationService;

        private CrmExporterConfig config;
        private string configFilePath;

        public ExportPagePresenter(IExportPageView view, IWorkerHost workerHost, IDataMigrationService dataMigrationService)
        {
            this.view = view;
            this.workerHost = workerHost;
            this.dataMigrationService = dataMigrationService;

            this.view.LoadConfigClicked += LoadConfig;
            this.view.SaveConfigClicked += SaveConfig;
            this.view.RunConfigClicked += RunConfig;
            this.view.SchemaConfigPathChanged += SchemaConfigPathChanged;

            this.config = new CrmExporterConfig();
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
                config = CrmExporterConfig.GetConfiguration(configFilePath);
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
                workerHost.WorkAsync(new WorkAsyncInfo
                {
                    Message = "Exporting data...",
                    Work = (bw, e) => dataMigrationService.ExportData(view.Service, view.DataFormat, config),
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

        private  CrmSchemaConfiguration GetSchemaConfiguration()
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
            config.BatchSize = view.BatchSize;
            config.PageSize = view.PageSize;
            config.TopCount = view.TopCount;
            config.CrmMigrationToolSchemaPaths.Clear();
            config.CrmMigrationToolSchemaPaths.Add(view.CrmMigrationToolSchemaPath);
            config.JsonFolderPath = view.JsonFolderPath;
            config.OnlyActiveRecords = view.OnlyActiveRecords;
            config.OneEntityPerBatch = view.OneEntityPerBatch;
            config.SeperateFilesPerEntity = view.SeperateFilesPerEntity;
            config.FilePrefix = view.FilePrefix;
            config.CrmMigrationToolSchemaFilters.Clear();
            if (view.CrmMigrationToolSchemaFilters != null)
            {
                foreach (var filter in view.CrmMigrationToolSchemaFilters)
                {
                    config.CrmMigrationToolSchemaFilters.Add(filter.Key, filter.Value);
                }
            }
        }

        private void WriteFormInputFromConfig()
        {
            view.BatchSize = config.BatchSize;
            view.PageSize = config.PageSize;
            view.TopCount = config.TopCount;
            view.CrmMigrationToolSchemaPath = config.CrmMigrationToolSchemaPaths.FirstOrDefault();
            view.JsonFolderPath = config.JsonFolderPath;
            view.OnlyActiveRecords = config.OnlyActiveRecords;
            view.OneEntityPerBatch = config.OneEntityPerBatch;
            view.SeperateFilesPerEntity = config.SeperateFilesPerEntity;
            view.FilePrefix = config.FilePrefix;
            view.CrmMigrationToolSchemaFilters = new Dictionary<string, string>(config.CrmMigrationToolSchemaFilters);
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