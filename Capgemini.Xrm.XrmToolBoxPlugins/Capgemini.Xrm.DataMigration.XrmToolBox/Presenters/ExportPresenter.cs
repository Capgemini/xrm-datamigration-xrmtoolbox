using Capgemini.DataMigration.Core;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Views;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Models;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Services;
using System;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Presenters
{
    public class ExportPresenter
    {
        private readonly IExportView exportView;
        private readonly ILogger logger;

        public ExportPresenter(IExportView exportView, ILogger logger)
        {
            this.exportView = exportView;
            this.exportView.SelectExportLocationHandler += SelectExportLocation;
            this.exportView.SelectExportConfigFileHandler += SelectExportConfig;
            this.exportView.SelectSchemaFileHandler += SelectSchemaFile;
            this.exportView.ExportDataHandler += ExportData;

            this.logger = logger;
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

        private void ExportData(object sender, EventArgs e)
        {
            try
            {
                DataMigrationService migrationService = new DataMigrationService(this.logger);

                ExportSettings settings = GetExportSettingsObject();
                migrationService.ExportData(settings);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        private ExportSettings GetExportSettingsObject()
        {
            ExportSettings settings = new ExportSettings();
            if (exportView.FormatJsonSelected)
                settings.DataFormat = "json";
            else if (exportView.FormatCsvSelected)
                settings.DataFormat = "csv";

            settings.SavePath = exportView.SaveExportLocation;
            settings.EnvironmentConnection = exportView.CrmServiceClient;
            settings.ExportConfigPath = exportView.ExportConfigFileLocation;
            settings.SchemaPath = exportView.ExportSchemaFileLocation;
            settings.ExportInactiveRecords = exportView.ExportInactiveRecordsChecked;
            settings.Minimize = exportView.MinimizeJsonChecked;
            settings.BatchSize = (int)exportView.BatchSize;

            return settings;
        }
    }
}