using Capgemini.Xrm.CdsDataMigratorLibrary.Exceptions;
using Capgemini.Xrm.CdsDataMigratorLibrary.Helpers;
using Capgemini.Xrm.CdsDataMigratorLibrary.Services;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
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
            this.view.OnEntityColumnChanged += OnEntityColumnChanged;
            this.view.OnRefFieldChanged += OnRefFieldChanged;
        }

        [ExcludeFromCodeCoverage]
        public IMetadataService MetaDataService { get; set; }

        [ExcludeFromCodeCoverage]
        public IOrganizationService OrganizationService { get; set; }

        [ExcludeFromCodeCoverage]
        public IExceptionService ExceptionService { get; set; }

        [ExcludeFromCodeCoverage]
        public IViewHelpers ViewHelpers { get; set; }

        public void OnVisible(object sender, EventArgs e)
        {   
            if (OrganizationService == null)
            {
                ShowErrorMessage();
                return;
            }
            if (!view.EntityListDataSource.Any())
            {
                var entities = MetaDataService.RetrieveEntities(OrganizationService);
                view.EntityListDataSource = entities.Select(x => x.LogicalName).OrderBy(n => n);
            }
        }

        public void OnEntityColumnChanged(object sender, EventArgs e)
        {
            if (OrganizationService == null)
            {
                ShowErrorMessage();
                return;
            }
                view.MappingCells = null;
                var entityMeta = MetaDataService.RetrieveEntities(view.CurrentCell, OrganizationService, ExceptionService);
                view.SetRefFieldDataSource = entityMeta.Attributes.Where(a => a.AttributeType == AttributeTypeCode.Lookup || a.AttributeType == AttributeTypeCode.Owner || a.AttributeType == AttributeTypeCode.Uniqueidentifier).OrderBy(p => p.LogicalName).ToArray();
        }

        public void OnRefFieldChanged(object sender, EventArgs e)
        {
            if (OrganizationService == null)
            {
                ShowErrorMessage();
                return;
            }
            var entityMeta = MetaDataService.RetrieveEntities(view.CurrentRowEntityName, OrganizationService, ExceptionService);
            view.SetMapFieldDataSource = entityMeta.Attributes.OrderBy(p => p.LogicalName).ToArray();
            view.SetMapFieldToNull();
        }

        private void ShowErrorMessage()
        {
            view.Close();
            ViewHelpers.ShowMessage("Please make sure you are connected to an organisation", "No connection made", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
