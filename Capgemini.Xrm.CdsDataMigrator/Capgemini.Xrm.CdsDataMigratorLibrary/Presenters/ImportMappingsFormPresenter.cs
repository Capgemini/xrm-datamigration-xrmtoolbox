using Capgemini.Xrm.CdsDataMigratorLibrary.Helpers;
using Capgemini.Xrm.CdsDataMigratorLibrary.Models;
using Capgemini.Xrm.DataMigration.Model;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows.Forms;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Presenters
{
    public class ImportMappingsFormPresenter : IDisposable
    {
        public readonly IImportMappingsFormView view;

        public ImportMappingsFormPresenter(IImportMappingsFormView view)
        {
            this.view = view;
            this.view.OnVisible += OnVisible;
        }

        [ExcludeFromCodeCoverage]
        public IViewHelpers ViewHelpers { get; set; }

        public void OnVisible(object sender, EventArgs e)
        {
            if (view.SchemaConfiguration == null || !view.SchemaConfiguration.Entities.Any())
            {
                ViewHelpers.ShowMessage("Please specify a schema file with atleast one entity defined.", "No entities available", MessageBoxButtons.OK, MessageBoxIcon.Information);
                view.Close();
                return;
            }

            view.EntityList = view.SchemaConfiguration.Entities.Select(x => x.Name).OrderBy(n => n);
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
        }
    }
}
