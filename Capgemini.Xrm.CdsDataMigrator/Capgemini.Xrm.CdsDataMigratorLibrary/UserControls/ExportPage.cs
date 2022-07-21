﻿using Capgemini.Xrm.CdsDataMigratorLibrary.Enums;
using Capgemini.Xrm.CdsDataMigratorLibrary.Forms;
using Capgemini.Xrm.CdsDataMigratorLibrary.Presenters;
using Capgemini.Xrm.CdsDataMigratorLibrary.Services;
using Capgemini.Xrm.DataMigration.Config;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;
using XrmToolBox.Extensibility;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.UserControls
{
    public partial class ExportPage : UserControl, IExportPageView
    {
        private ExportFilterForm exportFilterForm;

        public event EventHandler LoadConfigClicked;
        public event EventHandler SaveConfigClicked;
        public event EventHandler RunConfigClicked;
        public event EventHandler SchemaConfigPathChanged;

        public ExportPage()
        {
            InitializeComponent();

            this.exportFilterForm = new ExportFilterForm();
            this.exportFilterForm.Tag = new ExportFilterFormPresenter(this.exportFilterForm);
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
            set { fisSchemaFile.Value = value; SchemaConfigPathChanged?.Invoke(this, EventArgs.Empty); }
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

        CrmSchemaConfiguration IExportPageView.SchemaConfiguration
        {
            get => exportFilterForm.SchemaConfiguration;
            set => exportFilterForm.SchemaConfiguration = value;
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

        public void ShowError(Exception error)
        {
            FindPluginControlBase().ShowError(error);

            // TODO: Replace with `FindPluginControlBase().ShowErrorDialog(error)` when we update XrmToolBox.
            // https://www.xrmtoolbox.com/documentation/for-developers/plugincontrolbase-base-class/#error
        }

        public void ShowSuccess(string message)
        {
            FindPluginControlBase().ShowSuccess(message);
        }


        #endregion

        #region event mappings

        [ExcludeFromCodeCoverage]
        private void loadButton_Click(object sender, EventArgs e)
        {
            this.LoadConfigClicked?.Invoke(this, EventArgs.Empty);
        }

        [ExcludeFromCodeCoverage]
        private void saveButton_Click(object sender, EventArgs e)
        {
            this.SaveConfigClicked?.Invoke(sender, e);
        }

        [ExcludeFromCodeCoverage]
        private void runButton_Click(object sender, EventArgs e)
        {
            this.RunConfigClicked?.Invoke(sender, e);
        }

        [ExcludeFromCodeCoverage]
        private void btnFetchXmlFilters_Click(object sender, EventArgs e)
        {
            this.exportFilterForm.ShowDialog(this);
        }

        #endregion

        private CdsMigratorPluginControl FindPluginControlBase()
        {
            var parent = Parent;

            while (!(parent is CdsMigratorPluginControl || parent is null))
            {
                parent = parent?.Parent;
            }

            return parent as CdsMigratorPluginControl;
        }
    }
}
