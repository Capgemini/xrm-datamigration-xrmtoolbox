using Capgemini.Xrm.CdsDataMigratorLibrary.Enums;
using Capgemini.Xrm.CdsDataMigratorLibrary.Presenters;
using Microsoft.Xrm.Sdk;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.UserControls
{
    public partial class ImportPage : UserControl, IImportPageView
    {
        //private ImportPagePresenter presenter;

        public event EventHandler LoadConfigClicked;
        public event EventHandler SaveConfigClicked;
        public event EventHandler RunConfigClicked;

        public ImportPage()
        {
            InitializeComponent();
        }

        #region input mapping

        int IImportPageView.SaveBatchSize
        {
            get => (int)nbxSaveBatchSize.Value;
            set => nbxSaveBatchSize.Value = value;
        }

        bool IImportPageView.IgnoreStatuses
        {
            get => tcbIgnoreStatuses.Checked;
            set => tcbIgnoreStatuses.Checked = value;
        }

        bool IImportPageView.IgnoreSystemFields
        {
            get => tcbIgnoreSystemFields.Checked;
            set => tcbIgnoreSystemFields.Checked = value;
        }

        // file being imported from
        string IImportPageView.JsonFolderPath
        {
            get => fisJsonFolderPath.Value;
            set => fisJsonFolderPath.Value = value;
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

        string IImportPageView.CrmMigrationToolSchemaPath
        {
            get => fisSchemaFile.Value;
            set => fisSchemaFile.Value = value;
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
        private void Button3Click(object sender, EventArgs e)
        {
            var fd = openFileDialog.ShowDialog();

            if (fd == DialogResult.OK)
            {
                tbImportSchema.Text = openFileDialog.FileName;
            }
        }

        public void RadioButtonCheckedChanged(object sender, EventArgs e)
        {
            rbnDataFormatCsv.Checked = !rbnDataFormatJson.Checked;
            groupBox1.Visible = false;
        }

        public void RadioButton1CheckedChanged(object sender, EventArgs e)
        {
            rbnDataFormatJson.Checked = !rbnDataFormatCsv.Checked;
            groupBox1.Visible = true;
        }

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
        
        #endregion
    }
}
