namespace Capgemini.Xrm.CdsDataMigratorLibrary.Presenters
{
    public interface IExportFilterFormPresenter
    {
        void OnEntitySelected();
        void OnVisible();
        void UpdateFilterForEntity();
    }
}