using Capgemini.DataMigration.Core;
using System;
using Capgemini.Xrm.CdsDataMigratorLibrary.Enums;
using Capgemini.Xrm.CdsDataMigratorLibrary.Services;
using Capgemini.Xrm.CdsDataMigratorLibrary.Models;
using System.Diagnostics.CodeAnalysis;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Presenters
{
    public class ImportPresenter
    {
        private readonly IImportView importView;
        private readonly ILogger logger;
        private readonly IDataMigrationService dataMigrationService;

        public ImportPresenter(IImportView importView, ILogger logger, IDataMigrationService dataMigrationService)
        {
            this.importView = importView;
            this.logger = logger;
            this.dataMigrationService = dataMigrationService;

            this.importView.SelectImportLocationHandler += SelectImportLocation;
            this.importView.SelectImportConfigFileHandler += SelectImportConfig;
            //this.importView.SelectSchemaFileHandler += SelectSchemaFile;
            this.importView.ImportDataHandler += ImportData;
            this.importView.CancelHandler += CancelAction;
        }

        public ImportSettings GetImportSettingsObject()
        {
            ImportSettings settings = new ImportSettings();

            if (importView.FormatJsonSelected)
            {
                settings.DataFormat = DataFormat.Json;
            }
            else if (importView.FormatCsvSelected)
            {
                settings.DataFormat = DataFormat.Csv;
            }

            settings.SavePath = importView.SaveImportLocation;
            settings.EnvironmentConnection = importView.OrganizationService;
            settings.ImportConfigPath = importView.ImportConfigFileLocation;
            settings.SchemaPath = importView.ImportSchemaFileLocation;
            settings.ImportInactiveRecords = importView.ImportInactiveRecordsChecked;
            settings.Minimize = importView.MinimizeJsonChecked;
            settings.BatchSize = (int)importView.BatchSize;

            return settings;
        }

        public void CancelAction(object sender, EventArgs e)
        {
            try
            {
                dataMigrationService.CancelDataImport();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
        }

        public void ImportData(object sender, EventArgs e)
        {
            try
            {
                var settings = GetImportSettingsObject();
                dataMigrationService.ImportData(settings);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
        }

        [ExcludeFromCodeCoverage]
        private void SelectImportLocation(object sender, EventArgs e)
        {
            string importLocation = importView.ShowFolderBrowserDialog();
            importView.SaveImportLocation = importLocation;
        }

        [ExcludeFromCodeCoverage]
        private void SelectImportConfig(object sender, EventArgs e)
        {
            string importConfigFileName = importView.ShowFileDialog();
            importView.ImportConfigFileLocation = importConfigFileName;
        }

        //[ExcludeFromCodeCoverage]
        //private void SelectSchemaFile(object sender, EventArgs e)
        //{
        //    string schemaFileName = importView.ShowFileDialog();
        //    importView.ImportSchemaFileLocation = schemaFileName;
        //}
    }
}