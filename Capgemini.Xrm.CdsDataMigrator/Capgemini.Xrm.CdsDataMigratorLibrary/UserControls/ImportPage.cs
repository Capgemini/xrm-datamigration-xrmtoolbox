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

        public ImportPage()
        {
            InitializeComponent();
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

        // TO DO - decide on value
        int IImportPageView.SaveBatchSize
        {
            get => (int)nbxPageSize.Value;
            set => nbxPageSize.Value = value;
        }

        bool IImportPageView.IgnoreStatuses
        {
            get => tcbIgnoreRecordStatus.Checked;
            set => tcbIgnoreRecordStatus.Checked = value;
        }

        bool IImportPageView.IgnoreSystemFields
        {
            get => tcbIgnoreSystemFields.Checked;
            set => tcbIgnoreSystemFields.Checked = value;
        }

        //TO DO - update to get and set correct list of status exceptions
        List<string> IImportPageView.IgnoreStatusesExceptions
        {
            get
            {
                return new List<string>();
            }
            set => new List<string>();
        }

        // TO DO - migration config

        //TO DO - update to get and set correct list of status exceptions
        List<string> IImportPageView.AdditionalFieldsToIgnore
        {
            get
            {
                return new List<string>();
            }
            set => new List<string>();
        }

        string IImportPageView.JsonFolderPath
        {
            get => fisOutputDirectory.Value;
            set => fisOutputDirectory.Value = value;
        }

        //TO DO - update to get and set correct list of Entities to Sync
        List<string> IImportPageView.EntitiesToSync
        {
            get
            {
                return new List<string>();
            }
            set => new List<string>();
        }

        //TO DO - update to get and set correct list of Entities to Sync
        List<string> IImportPageView.NoUpsertEntities
        {
            get
            {
                return new List<string>();
            }
            set => new List<string>();
        }

        //TO DO - update to get and set correct list of No Upsert Entities
        List<string> IImportPageView.PluginsToDeactivate
        {
            get
            {
                return new List<string>();
            }
            set => new List<string>();
        }

        //TO DO - update to get and set correct list of Plugins To Deactivate
        List<string> IImportPageView.ProcessesToDeactivate
        {
            get
            {
                return new List<string>();
            }
            set => new List<string>();
        }

        // TO DO - add setter to SaveBatch size and decide on value
        bool IImportPageView.DeactivateAllProcesses
        {
            get
            {
                return true;
            }
        }

        string IImportPageView.FilePrefix
        {
            get => fisOutputDirectory.Value;
            set => fisOutputDirectory.Value = value;
        }

        //TO DO - update to get and set correct list of Fields to Obfiscate
        List<string> IImportPageView.FieldsToObfuscate
        {
            get
            {
                return new List<string>();
            }
            set => new List<string>();
        }
        
        //Dictionary<string, string> IImportPageView.CrmMigrationToolSchemaFilters
        //{
        //    get => importFilterForm.EntityFilters;
        //    set => importFilterForm.EntityFilters = value;
        //}

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

        //[ExcludeFromCodeCoverage]
        //private void btnFetchXmlFilters_Click(object sender, EventArgs e)
        //{
        //    importFilterForm.SchemaConfiguration = presenter.GetSchemaConfiguration();
        //    this.importFilterForm.ShowDialog(this);
        //}

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
