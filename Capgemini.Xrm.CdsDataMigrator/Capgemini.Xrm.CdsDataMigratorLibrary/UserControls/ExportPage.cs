using Capgemini.Xrm.CdsDataMigratorLibrary.Enums;
using Capgemini.Xrm.CdsDataMigratorLibrary.Presenters;
using Capgemini.Xrm.CdsDataMigratorLibrary.Services;
using Microsoft.Xrm.Sdk;
using System;
using System.Windows.Forms;
using XrmToolBox.Extensibility;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.UserControls
{
    public partial class ExportPage : UserControl, IExportPageView
    {
        private readonly ExportPagePresenter presenter;

        public ExportPage()
        {
            InitializeComponent();

            var logger = new LogToFileService(new LogManagerContainer(new LogManager(typeof(CdsMigratorPluginControl))));
            var dataMigrationService = new DataMigrationService(logger, new CrmGenericMigratorFactory());

            presenter = new ExportPagePresenter(this, FindPluginControlBase(), dataMigrationService);
        }

        #region input mapping

        int IExportPageView.PageSize
        {
            get => (int)nbxPageSize.Value;
            set => nbxPageSize.Value = value;
        }

        int IExportPageView.BatchSize
        {
            get => (int)nbxBatchSize.Value;
            set => nbxBatchSize.Value = value;
        }

        int IExportPageView.TopCount
        {
            get => (int)nbxTopCount.Value;
            set => nbxTopCount.Value = value;
        }
        bool IExportPageView.OnlyActiveRecords
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        string IExportPageView.JsonFolderPath
        {
            get => fisOutputDirectory.Value;
            set => fisOutputDirectory.Value = value;
        }

        bool IExportPageView.OneEntityPerBatch
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        string IExportPageView.FilePrefix
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        bool IExportPageView.SeperateFilesPerEntity
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        string IExportPageView.CrmMigrationToolSchemaPath
        {
            get => fisSchemaFile.Value;
            set => fisSchemaFile.Value = value;
        }

        DataFormat IExportPageView.DataFormat
        {
            get => rbnDataFormatJson.Checked ? DataFormat.Json : rbnDataFormatCsv.Checked ? DataFormat.Csv : DataFormat.Unknown;
            set
            {
                rbnDataFormatJson.Checked = value == DataFormat.Json;
                rbnDataFormatCsv.Checked = value == DataFormat.Csv;
            }
        }

        IOrganizationService IExportPageView.Service
        {
            get => dataverseEnvironmentSelector1.Service;
        }

        #endregion

        #region action mappings

        string IExportPageView.AskForFilePathToOpen()
        {
            openFileDialog.ShowDialog();
            return openFileDialog.FileName;
        }

        string IExportPageView.AskForFilePathToSave(string existingFileName)
        {
            saveFileDialog.FileName = existingFileName;
            saveFileDialog.ShowDialog();
            return saveFileDialog.FileName;
        }

        #endregion

        #region event mappings

        private void loadButton_Click(object sender, EventArgs e)
        {
            presenter.LoadConfig();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            presenter.SaveConfig();
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            presenter.RunConfig();
        }

        #endregion

        private PluginControlBase FindPluginControlBase()
        {
            var parent = Parent;

            while (!(parent is PluginControlBase || parent is null))
            {
                parent = parent?.Parent;
            }

            return parent as PluginControlBase;
        }
    }
}
