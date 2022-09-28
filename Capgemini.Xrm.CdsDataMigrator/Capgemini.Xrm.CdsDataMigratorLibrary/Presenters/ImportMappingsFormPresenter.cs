using Capgemini.Xrm.CdsDataMigratorLibrary.Helpers;
using Capgemini.Xrm.CdsDataMigratorLibrary.Services;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows.Forms;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Presenters
{
    public class ImportMappingsFormPresenter : IDisposable
    {
        public readonly IImportMappingsFormView view;

        [ExcludeFromCodeCoverage]
        public IMetadataService MetaDataService { get; set; }

        [ExcludeFromCodeCoverage]
        public IOrganizationService OrganizationService { get; set; }

        public ImportMappingsFormPresenter(IImportMappingsFormView view)
        {
            this.view = view;
            this.view.OnVisible += OnVisible;
        }

        [ExcludeFromCodeCoverage]
        public IViewHelpers ViewHelpers { get; set; }

        public void OnVisible(object sender, EventArgs e)
        {
            if (OrganizationService == null)
            {
                view.Close();
                ViewHelpers.ShowMessage("Please make sure you are connected to an organisation", "No connection made", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (new List<string>(view.EntityListDataSource).Count == 0)
            {
                var entities = MetaDataService.RetrieveEntities(OrganizationService);
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
