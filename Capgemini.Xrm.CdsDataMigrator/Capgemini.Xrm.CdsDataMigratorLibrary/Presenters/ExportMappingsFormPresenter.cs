using Capgemini.Xrm.CdsDataMigratorLibrary.Exceptions;
using Capgemini.Xrm.CdsDataMigratorLibrary.Helpers;
using Capgemini.Xrm.CdsDataMigratorLibrary.Services;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows.Forms;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Presenters
{
    public class ExportLookupMappingsFormPresenter : IDisposable
    {
        private readonly IExportLookupMappingsView view;
        private readonly IMetadataService metadataService;
        private readonly IExceptionService exceptionService;
        private readonly IViewHelpers viewHelpers;
        private readonly Func<IOrganizationService> organizationServiceGetter;
    
        public ExportLookupMappingsFormPresenter(IExportLookupMappingsView view, IMetadataService metaDataService, IExceptionService exceptionService, IViewHelpers viewHelpers, Func<IOrganizationService> organizationServiceGetter)
        {
            this.view = view;
            this.metadataService = metaDataService;
            this.exceptionService = exceptionService;
            this.viewHelpers = viewHelpers;
            this.organizationServiceGetter = organizationServiceGetter;

            this.view.OnVisible += OnVisible;
            this.view.OnEntityColumnChanged += OnEntityColumnChanged;
            this.view.OnRefFieldChanged += OnRefFieldChanged;
        }

        public void OnVisible(object sender, EventArgs e)
        {   
            if (this.organizationServiceGetter() == null)
            {
                ShowErrorMessage();
                return;
            }
            if (!view.EntityListDataSource.Any())
            {
                var entities = metadataService.RetrieveEntities(this.organizationServiceGetter());
                view.EntityListDataSource = entities.Select(x => x.LogicalName).OrderBy(n => n);
            }
        }

        public void OnEntityColumnChanged(object sender, EventArgs e)
        {
            if (this.organizationServiceGetter() == null)
            {
                ShowErrorMessage();
                return;
            }
            view.MappingCells = null;
            var entityMeta = metadataService.RetrieveEntities(view.CurrentCell, this.organizationServiceGetter(), exceptionService);
            view.SetRefFieldDataSource = entityMeta.Attributes.Where(a => a.AttributeType == AttributeTypeCode.Lookup || a.AttributeType == AttributeTypeCode.Owner || a.AttributeType == AttributeTypeCode.Uniqueidentifier).OrderBy(p => p.LogicalName).ToArray();
        }

        public void OnRefFieldChanged(object sender, EventArgs e)
        {
            if (this.organizationServiceGetter() == null)
            {
                ShowErrorMessage();
                return;
            }
            var entityMeta = metadataService.RetrieveEntities(view.CurrentRowEntityName, this.organizationServiceGetter(), exceptionService);
            view.SetMapFieldDataSource = entityMeta.Attributes.OrderBy(p => p.LogicalName).ToArray();
        }

        private void ShowErrorMessage()
        {
            view.Close();
            viewHelpers.ShowMessage("Please make sure you are connected to an organisation", "No connection made", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
