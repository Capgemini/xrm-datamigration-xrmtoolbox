using Capgemini.Xrm.CdsDataMigratorLibrary.Enums;
using Capgemini.Xrm.CdsDataMigratorLibrary.Forms;
using Capgemini.Xrm.CdsDataMigratorLibrary.Presenters;
using Capgemini.Xrm.CdsDataMigratorLibrary.Services;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;
using XrmToolBox.Extensibility;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.UserControls
{
    public partial class ImportPage : UserControl, IExportPageView
    {
        private ExportPagePresenter presenter;
        private ExportFilterForm exportFilterForm;

        public ImportPage()
        {
            InitializeComponent();

            // this.exportFilterForm = new ExportFilterForm();
        }

        [ExcludeFromCodeCoverage]
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

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
            get => tcbActiveRecords.Checked;
            set => tcbActiveRecords.Checked = value;
        }

        string IExportPageView.JsonFolderPath
        {
            get => fisOutputDirectory.Value;
            set => fisOutputDirectory.Value = value;
        }

        bool IExportPageView.OneEntityPerBatch
        {
            get => tcbOneEntityPerBatch.Checked;
            set => tcbOneEntityPerBatch.Checked = value;
        }

        string IExportPageView.FilePrefix
        {
            get => tbxFileNamePrefix.Text;
            set => tbxFileNamePrefix.Text = value;
        }

        bool IExportPageView.SeperateFilesPerEntity
        {
            get => tcbSeparateFilesPerEntity.Checked;
            set => tcbSeparateFilesPerEntity.Checked = value;
        }

        string IExportPageView.CrmMigrationToolSchemaPath
        {
            get => fisSchemaFile.Value;
            set => fisSchemaFile.Value = value;
        }

        DataFormat IExportPageView.DataFormat
        {
            get
            {
                if (rbnDataFormatJson.Checked) return DataFormat.Json;
                if (rbnDataFormatCsv.Checked) return DataFormat.Csv;
                return DataFormat.Unknown;
            }
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

        Dictionary<string, string> IExportPageView.CrmMigrationToolSchemaFilters
        {
            get => exportFilterForm.EntityFilters;
            set => exportFilterForm.EntityFilters = value;
        }

        #endregion

        #region action mappings

        [ExcludeFromCodeCoverage]
        string IExportPageView.AskForFilePathToOpen()
        {
            openFileDialog.ShowDialog();
            return openFileDialog.FileName;
        }

        [ExcludeFromCodeCoverage]
        string IExportPageView.AskForFilePathToSave(string existingFileName)
        {
            saveFileDialog.FileName = existingFileName;
            saveFileDialog.ShowDialog();
            return saveFileDialog.FileName;
        }

        #endregion

        #region event mappings

        [ExcludeFromCodeCoverage]
        private void loadButton_Click(object sender, EventArgs e)
        {
            presenter.LoadConfig();
        }

        [ExcludeFromCodeCoverage]
        private void saveButton_Click(object sender, EventArgs e)
        {
            presenter.SaveConfig();
        }

        [ExcludeFromCodeCoverage]
        private void runButton_Click(object sender, EventArgs e)
        {
            presenter.RunConfig();
        }

        [ExcludeFromCodeCoverage]
        private void btnFetchXmlFilters_Click(object sender, EventArgs e)
        {
            exportFilterForm.SchemaConfiguration = presenter.GetSchemaConfiguration();
            this.exportFilterForm.ShowDialog(this);
        }

        #endregion

        [ExcludeFromCodeCoverage]
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
