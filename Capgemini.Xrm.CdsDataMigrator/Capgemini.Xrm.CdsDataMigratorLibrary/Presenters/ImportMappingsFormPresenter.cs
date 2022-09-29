using Capgemini.Xrm.CdsDataMigratorLibrary.Helpers;
using Capgemini.Xrm.CdsDataMigratorLibrary.Services;
using Microsoft.Xrm.Sdk;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows.Forms;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Presenters
{
    public class ImportMappingsFormPresenter : IDisposable
    {
        private readonly IImportMappingsFormView view;
        private readonly IMetadataService metadataService;
        private readonly IViewHelpers viewHelpers;
        private readonly Func<IOrganizationService> organizationServiceGetter;

        public ImportMappingsFormPresenter(IImportMappingsFormView view, IMetadataService metadataService, IViewHelpers viewHelpers, Func<IOrganizationService> organizationServiceGetter)
        {
            this.view = view;
            this.metadataService = metadataService;
            this.viewHelpers = viewHelpers;
            this.organizationServiceGetter = organizationServiceGetter;

            this.view.OnVisible += OnVisible;
        }


        public void OnVisible(object sender, EventArgs e)
        {
            if (this.organizationServiceGetter() == null)
            {
                view.Close();
                viewHelpers.ShowMessage("Please make sure you are connected to an organisation", "No connection made", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!view.EntityListDataSource.Any())
            {
                var entities = metadataService.RetrieveEntities(this.organizationServiceGetter());
                view.EntityListDataSource = entities.Select(x => x.LogicalName).OrderBy(n => n);
            }
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
