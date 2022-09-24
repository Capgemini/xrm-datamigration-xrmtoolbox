using Capgemini.Xrm.CdsDataMigratorLibrary.Helpers;
using Capgemini.Xrm.CdsDataMigratorLibrary.Models;
using Capgemini.Xrm.DataMigration.Model;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows.Forms;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Presenters
{
    public class ExportFilterFormPresenter : IDisposable
    {
        public readonly IExportFilterFormView view;

        public ExportFilterFormPresenter(IExportFilterFormView view)
        {
            this.view = view;

            this.view.OnVisible += OnVisible;
            this.view.OnEntitySelected += OnEntitySelected;
            this.view.OnFilterTextChanged += UpdateFilterForEntity;
        }
        [ExcludeFromCodeCoverage]
        public IViewHelpers ViewHelpers { get; set; }

        public void OnVisible(object sender, EventArgs e)
        {
            if (view.SchemaConfiguration == null || !view.SchemaConfiguration.Entities.Any())
            {
                view.Close();
                ViewHelpers.ShowMessage("Please specify a schema file with atleast one entity defined.", "No entities available", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        public void OnEntitySelected(object sender, EventArgs e)
        {
            view.FilterText = view.EntityFilters.TryGetValue(view.SelectedEntity.Name, out var filters) ? filters : string.Empty;
        }

        public void UpdateFilterForEntity(object sender, EventArgs e)
        {
            view.EntityFilters[view.SelectedEntity.Name] = view.FilterText;
        }

        [ExcludeFromCodeCoverage]
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        [ExcludeFromCodeCoverage]
        protected virtual void Dispose(bool disposing)
        {
            this.view.OnVisible -= OnVisible;
            this.view.OnEntitySelected -= OnEntitySelected;
            this.view.OnFilterTextChanged -= UpdateFilterForEntity;
        }
    }
}
