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
    public partial class ImportPage : UserControl, IImportPageView
    {
        private ImportPagePresenter presenter;
        private FilterFormImport importFilterForm;

        public ImportPage()
        {
            InitializeComponent();

             this.importFilterForm = new FilterFormImport();
        }

        [ExcludeFromCodeCoverage]
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var logger = new LogToFileService(new LogManagerContainer(new LogManager(typeof(CdsMigratorPluginControl))));
            var dataMigrationService = new DataMigrationService(logger, new CrmGenericMigratorFactory());
            presenter = new ImportPagePresenter(this, FindPluginControlBase(), dataMigrationService);
        }

        #region input mapping

        int IImportPageView.PageSize
        {
            get => (int)nbxPageSize.Value;
            set => nbxPageSize.Value = value;
        }

        int IImportPageView.BatchSize
        {
            get => (int)nbxBatchSize.Value;
            set => nbxBatchSize.Value = value;
        }

        int IImportPageView.TopCount
        {
            get => (int)nbxTopCount.Value;
            set => nbxTopCount.Value = value;
        }
        bool IImportPageView.OnlyActiveRecords
        {
            get => tcbActiveRecords.Checked;
            set => tcbActiveRecords.Checked = value;
        }

        string IImportPageView.JsonFolderPath
        {
            get => fisOutputDirectory.Value;
            set => fisOutputDirectory.Value = value;
        }

        bool IImportPageView.OneEntityPerBatch
        {
            get => tcbOneEntityPerBatch.Checked;
            set => tcbOneEntityPerBatch.Checked = value;
        }

        string IImportPageView.FilePrefix
        {
            get => tbxFileNamePrefix.Text;
            set => tbxFileNamePrefix.Text = value;
        }

        bool IImportPageView.SeperateFilesPerEntity
        {
            get => tcbSeparateFilesPerEntity.Checked;
            set => tcbSeparateFilesPerEntity.Checked = value;
        }

        string IImportPageView.CrmMigrationToolSchemaPath
        {
            get => fisSchemaFile.Value;
            set => fisSchemaFile.Value = value;
        }

        DataFormat IImportPageView.DataFormat
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

        IOrganizationService IImportPageView.Service
        {
            get => dataverseEnvironmentSelector1.Service;
        }

        Dictionary<string, string> IImportPageView.CrmMigrationToolSchemaFilters
        {
            get => importFilterForm.EntityFilters;
            set => importFilterForm.EntityFilters = value;
        }

        #endregion

        #region action mappings

        [ExcludeFromCodeCoverage]
        string IImportPageView.AskForFilePathToOpen()
        {
            openFileDialog.ShowDialog();
            return openFileDialog.FileName;
        }

        [ExcludeFromCodeCoverage]
        string IImportPageView.AskForFilePathToSave(string existingFileName)
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
            importFilterForm.SchemaConfiguration = presenter.GetSchemaConfiguration();
            this.importFilterForm.ShowDialog(this);
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
