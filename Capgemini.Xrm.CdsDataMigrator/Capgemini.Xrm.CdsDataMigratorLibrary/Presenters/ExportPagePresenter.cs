using Capgemini.Xrm.DataMigration.CrmStore.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Presenters
{
    public class ExportPagePresenter
    {
        private readonly IExportPageView view;

        private CrmExporterConfig config;
        private string configFilePath;

        public ExportPagePresenter(IExportPageView view)
        {
            this.view = view;
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
            ReadFormInputIntoConfig();
        }

        private void ReadFormInputIntoConfig()
        {
            config.BatchSize = view.BatchSize;
            config.PageSize = view.PageSize;
            config.TopCount = view.TopCount;
            //config.OneEntityPerBatch = view.OneEntityPerBatch;
            //config.JsonFolderPath = view.JsonFolderPath;
            //config.OnlyActiveRecords = view.OnlyActiveRecords;
            //config.SeperateFilesPerEntity = view.SeperateFilesPerEntity;
            //config.FilePrefix = view.FilePrefix;
        }

        private void WriteFormInputFromConfig()
        {
            view.BatchSize = config.BatchSize;
            view.PageSize = config.PageSize;
            view.TopCount = config.TopCount;
            //view.OneEntityPerBatch = config.OneEntityPerBatch;
            //view.JsonFolderPath = config.JsonFolderPath;
            //view.OnlyActiveRecords = config.OnlyActiveRecords;
            //view.SeperateFilesPerEntity = config.SeperateFilesPerEntity;
            //view.FilePrefix = config.FilePrefix;
        }
    }
}