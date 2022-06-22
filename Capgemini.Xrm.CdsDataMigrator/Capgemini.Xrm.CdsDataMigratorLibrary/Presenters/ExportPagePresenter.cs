using Capgemini.Xrm.CdsDataMigratorLibrary.Services;
using Capgemini.Xrm.DataMigration.CrmStore.Config;
using System.IO;
using System.Linq;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Presenters
{
    public class ExportPagePresenter
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

            this.config = new CrmExporterConfig();
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
                config = CrmExporterConfig.GetConfiguration(configFilePath);
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
                workerHost.WorkAsync(new WorkAsyncInfo
                {
                    Message = "Exporting data...",
                    Work = (bw, e) => dataMigrationService.ExportData(view.Service, view.DataFormat, config),
                });
            }
            catch
            {
                // TODO: Handle exception
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
            //config.FilePrefix = view.FilePrefix;
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
            //view.FilePrefix = config.FilePrefix;
        }
    }
}