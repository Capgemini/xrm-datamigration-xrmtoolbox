using Capgemini.Xrm.CdsDataMigratorLibrary.Exceptions;
using Capgemini.Xrm.CdsDataMigratorLibrary.Models;
using Capgemini.Xrm.CdsDataMigratorLibrary.Services;
using Capgemini.Xrm.DataMigration.Model;
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
            this.view.LoadMappedItems += LoadMappedItems;
        }

        [ExcludeFromCodeCoverage]
        public IMetadataService MetaDataService { get; set; }

        [ExcludeFromCodeCoverage]
        public IOrganizationService OrganizationService { get; set; }

        [ExcludeFromCodeCoverage]
        public IExceptionService ExceptionService { get; set; }

        public void OnVisible(object sender, EventArgs e)
        {

            if (OrganizationService == null)
            {
                view.ShowMessage("Please make sure you are connected to an organisation", "No connection madde",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

                view.Close();
                return;
            }
            if (new List<string>(view.EntityListDataSource).Count == 0)
            {
                var entities = MetaDataService.RetrieveEntities(OrganizationService);
                view.EntityListDataSource = entities.Select(x => x.LogicalName).OrderBy(n => n).ToList();
            }  
        }

        public void OnEntityColumnChanged(object sender, EventArgs e)
        {
            if (OrganizationService == null)
            {
                view.ShowMessage("Please make sure you are connected to an organisation", "No connection madde",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

                view.Close();
                return;
            }
            view.MappingCells = null;
            var entityMeta = MetaDataService.RetrieveEntities(view.CurrentCell, OrganizationService, ExceptionService);
            view.RefFieldDataSource = entityMeta.Attributes.Where(a => a.AttributeType == AttributeTypeCode.Lookup || a.AttributeType == AttributeTypeCode.Owner || a.AttributeType == AttributeTypeCode.Uniqueidentifier).OrderBy(p => p.LogicalName).ToArray();
        }

        public void OnRefFieldChanged(object sender, EventArgs e)
        {
            if (OrganizationService == null)
            {
                view.ShowMessage("Please make sure you are connected to an organisation", "No connection madde",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

                view.Close();
                return;
            }
            var entityMeta = MetaDataService.RetrieveEntities(view.FirstCellInRow, OrganizationService, ExceptionService);
            view.MapFieldDataSource = entityMeta.Attributes.OrderBy(p => p.LogicalName).ToArray();
        }

        public void LoadMappedItems(object sender, EventArgs e)
        {
            // add data source etc to loaded mappings
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
