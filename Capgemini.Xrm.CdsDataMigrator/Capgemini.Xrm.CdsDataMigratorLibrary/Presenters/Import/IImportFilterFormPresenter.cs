namespace Capgemini.Xrm.CdsDataMigratorLibrary.Presenters
{
    public interface IImportFilterFormPresenter
    {
        void OnEntitySelected();
        void OnVisible();
        void UpdateFilterForEntity();
    }
}