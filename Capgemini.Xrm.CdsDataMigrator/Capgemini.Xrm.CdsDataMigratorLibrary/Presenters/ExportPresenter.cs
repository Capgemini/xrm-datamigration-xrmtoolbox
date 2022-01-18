using Capgemini.DataMigration.Core;
using System;
using Capgemini.Xrm.CdsDataMigratorLibrary.Enums;
using Capgemini.Xrm.CdsDataMigratorLibrary.Services;
using Capgemini.Xrm.CdsDataMigratorLibrary.Models;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Presenters
{
    public class ExportPresenter
    {
        private readonly IExportView exportView;
        private readonly ILogger logger;
        private readonly IDataMigrationService dataMigrationService;

        public ExportPresenter(IExportView exportView, ILogger logger, IDataMigrationService dataMigrationService)
        {
            this.exportView = exportView;
            this.logger = logger;
            this.dataMigrationService = dataMigrationService;

            this.exportView.SelectExportLocationHandler += SelectExportLocation;
            this.exportView.SelectExportConfigFileHandler += SelectExportConfig;
            this.exportView.SelectSchemaFileHandler += SelectSchemaFile;
            this.exportView.ExportDataHandler += ExportData;
            this.exportView.CancelHandler += CancelAction;
        }

        //public void ExportDataAction()
        //{
        //    try
        //    {
        //        var settings = GetExportSettingsObject();
        //        dataMigrationService.ExportData(settings);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError(ex.Message);
        //    }
        //}

        public ExportSettings GetExportSettingsObject()
        {
            ExportSettings settings = new ExportSettings();

            if (exportView.FormatJsonSelected)
            {
                settings.DataFormat = DataFormat.Json;
            }
            else if (exportView.FormatCsvSelected)
            {
                settings.DataFormat = DataFormat.Csv;
            }

            settings.SavePath = exportView.SaveExportLocation;
            settings.EnvironmentConnection = exportView.OrganizationService;
            settings.ExportConfigPath = exportView.ExportConfigFileLocation;
            settings.SchemaPath = exportView.ExportSchemaFileLocation;
            settings.ExportInactiveRecords = exportView.ExportInactiveRecordsChecked;
            settings.Minimize = exportView.MinimizeJsonChecked;
            settings.BatchSize = (int)exportView.BatchSize;

            return settings;
        }

        public void CancelAction(object sender, EventArgs e)
        {
            try
            {
                dataMigrationService.CancelDataExport();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
        }

        public void ExportData(object sender, EventArgs e)
        {
            //ExportDataAction();
            try
            {
                var settings = GetExportSettingsObject();
                dataMigrationService.ExportData(settings);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
        }

        private void SelectExportLocation(object sender, EventArgs e)
        {
            string exportLocation = exportView.ShowFolderBrowserDialog();
            exportView.SaveExportLocation = exportLocation;
        }

        private void SelectExportConfig(object sender, EventArgs e)
        {
            string exportConfigFileName = exportView.ShowFileDialog();
            exportView.ExportConfigFileLocation = exportConfigFileName;
        }

        private void SelectSchemaFile(object sender, EventArgs e)
        {
            string schemaFileName = exportView.ShowFileDialog();
            exportView.ExportSchemaFileLocation = schemaFileName;
        }
    }
}