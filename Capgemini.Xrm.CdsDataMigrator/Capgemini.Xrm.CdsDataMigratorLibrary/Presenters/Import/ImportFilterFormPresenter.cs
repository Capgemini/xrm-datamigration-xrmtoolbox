using Capgemini.Xrm.CdsDataMigratorLibrary.Models;
using Capgemini.Xrm.DataMigration.Model;
using System.Linq;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Presenters
{
    public class ImportFilterFormPresenter : IImportFilterFormPresenter
    {
        public readonly IImportFilterFormView view;

        public ImportFilterFormPresenter(IImportFilterFormView view)
        {
            this.view = view;
        }

        public void OnVisible()
        {
            if (view.SchemaConfiguration == null || !view.SchemaConfiguration.Entities.Any())
            {
                view.ShowMessage(
                    "Please specify a schema file with atleast one entity defined.",
                    "No entities available",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Information);

                view.Close();
                return;
            }

            view.EntityList = view.SchemaConfiguration.Entities
                .Select(x => new ListBoxItem<CrmEntity> { DisplayName = x.DisplayName, Item = x });
            view.SelectedEntity = view.EntityList.First().Item;

            var entitiesNoLongerInSchema = view.EntityFilters.Keys
                .Where(entityName => !view.SchemaConfiguration.Entities.Exists(entity => entity.Name == entityName))
                .ToList();

            foreach (var entityName in entitiesNoLongerInSchema)
            {
                view.EntityFilters.Remove(entityName);
            }
        }

        public void OnEntitySelected()
        {
            view.FilterText = view.EntityFilters.TryGetValue(view.SelectedEntity.Name, out var filters) ? filters : string.Empty;
        }

        public void UpdateFilterForEntity()
        {
            view.EntityFilters[view.SelectedEntity.Name] = view.FilterText;
        }

    }
}
