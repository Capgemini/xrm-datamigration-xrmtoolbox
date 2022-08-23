using Capgemini.Xrm.CdsDataMigratorLibrary.Models;
using Capgemini.Xrm.CdsDataMigratorLibrary.Services;
using Capgemini.Xrm.DataMigration.Model;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows.Forms;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Presenters
{
    public class ExportLookupMappingsFormPresenter : IDisposable
    {
        public readonly IExportLookupMappingsView view;

        public ExportLookupMappingsFormPresenter(IExportLookupMappingsView view)
        {
            this.view = view;

            this.view.OnVisible += OnVisible;
        }

        public void OnVisible(object sender, EventArgs e)
        {

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
