using Capgemini.Xrm.CdsDataMigratorLibrary.Presenters;
using System;
using System.Windows.Forms;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.UserControls
{
    public partial class ExportPage : UserControl, IExportPageView
    {
        private readonly ExportPagePresenter presenter;

        public ExportPage()
        {
            InitializeComponent();
            presenter = new ExportPagePresenter(this);
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
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
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

        #endregion

        #region action mappings

        string IExportPageView.AskForFilePathToOpen()
        {
            openFileDialog1.ShowDialog();
            return openFileDialog1.FileName;
        }

        string IExportPageView.AskForFilePathToSave(string existingFileName)
        {
            saveFileDialog1.FileName = existingFileName;
            saveFileDialog1.ShowDialog();
            return saveFileDialog1.FileName;
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
    }
}
