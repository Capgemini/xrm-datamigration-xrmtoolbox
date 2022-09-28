using Capgemini.Xrm.CdsDataMigratorLibrary.Helpers;
using Capgemini.Xrm.CdsDataMigratorLibrary.Models;
using Capgemini.Xrm.CdsDataMigratorLibrary.Services;
using Capgemini.Xrm.DataMigration.Model;
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
                ViewHelpers.ShowMessage("Please specify a schema file with atleast one entity defined.", "No entities available", MessageBoxButtons.OK, MessageBoxIcon.Error);
                view.Close();
                return;
            }
            if (new List<string>(view.EntityListDataSource).Count == 0)
            {
                var entities = MetaDataService.RetrieveEntities(OrganizationService);
                view.EntityListDataSource = entities.Select(x => x.LogicalName).OrderBy(n => n);
        }
    }

        private void ShowErrorMessage()
        {
            view.Close();
            ViewHelpers.ShowMessage("Please make sure you are connected to an organisation", "No connection made", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
