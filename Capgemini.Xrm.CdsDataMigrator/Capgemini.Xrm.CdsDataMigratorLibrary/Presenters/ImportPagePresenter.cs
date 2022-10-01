using Capgemini.Xrm.CdsDataMigratorLibrary.Services;
using Capgemini.Xrm.CdsDataMigratorLibrary.Helpers;
using Capgemini.Xrm.DataMigration.Config;
using Capgemini.Xrm.DataMigration.CrmStore.Config;
using System.Diagnostics.CodeAnalysis;
using System;
using System.IO;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;
using System.Collections.Generic;
using System.Windows.Forms;
using NuGet;
using Microsoft.Xrm.Sdk;
using System.Linq;
using Microsoft.Xrm.Sdk.Metadata;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Presenters
{
    public class ImportPagePresenter : IDisposable
    {
        private readonly IImportPageView view;
        private readonly IWorkerHost workerHost;
        private readonly IDataMigrationService dataMigrationService;
        private readonly IOrganizationService organisationService;
        private readonly IMetadataService metaDataService;
        private readonly IViewHelpers viewHelpers;
        private readonly IEntityRepositoryService entityRepositoryService;

        private CrmImportConfig config;
        private string configFilePath;

        public ImportPagePresenter(IImportPageView view, IWorkerHost workerHost, IDataMigrationService dataMigrationService, IOrganizationService organizationService, IMetadataService metaDataService, IViewHelpers viewHelpers, IEntityRepositoryService entityRepositoryService)
        {
            this.view = view;
            this.workerHost = workerHost;
            this.dataMigrationService = dataMigrationService;
            this.organisationService = organizationService;
            this.metaDataService = metaDataService;
            this.viewHelpers = viewHelpers;
            this.entityRepositoryService = entityRepositoryService;

            this.view.LoadConfigClicked += LoadConfig;
            this.view.SaveConfigClicked += SaveConfig;
            this.view.RunConfigClicked += RunConfig;
            this.view.SchemaConfigPathChanged += SchemaConfigPathChanged;

            this.config = new CrmImportConfig();
            WriteFormInputFromConfig();
        }

        private void LoadConfig(object sender, EventArgs args)
        {
            try
            {
                configFilePath = view.AskForFilePathToOpen();
                if (!File.Exists(configFilePath))
                {
                    return;
                }
                config = CrmImportConfig.GetConfiguration(configFilePath);
                WriteFormInputFromConfig();
            }
            catch (Exception ex)
            {
                viewHelpers.ShowMessage(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveConfig(object sender, EventArgs args)
        {
            try
            {
                ReadFormInputIntoConfig();
                // TODO: Validate Config
                configFilePath = view.AskForFilePathToSave(configFilePath);
                if (File.Exists(configFilePath))
                {
                    File.Delete(configFilePath);
                }
                config.SaveConfiguration(configFilePath);
            }
            catch (Exception ex)
            {
                viewHelpers.ShowMessage(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RunConfig(object sender, EventArgs args)
        {
            try
            {
                ReadFormInputIntoConfig();
                // TODO: Validate Config
                var schema = GetSchemaConfiguration();
                workerHost.WorkAsync(new WorkAsyncInfo
                {
                    Message = "Importing data...",
                    Work = (bw, e) => dataMigrationService.ImportData(view.Service, view.DataFormat, schema, config, view.maxThreads, entityRepositoryService),
                    PostWorkCallBack = (e) =>
                    {
                        if (e.Error != null)
                        {
                            viewHelpers.ShowMessage(e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            viewHelpers.ShowMessage("Data import is complete.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                viewHelpers.ShowMessage(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
          
        public CrmSchemaConfiguration GetSchemaConfiguration()
        {
            if (string.IsNullOrWhiteSpace(view.CrmMigrationToolSchemaPath) || !File.Exists(view.CrmMigrationToolSchemaPath))
            {
                return null;
            }
            try
            {
                return CrmSchemaConfiguration.ReadFromFile(view.CrmMigrationToolSchemaPath);
            }
            catch
            {
                return null;
            }
        }

        private void ReadFormInputIntoConfig()
        {
            config.SaveBatchSize = view.SaveBatchSize;
            config.IgnoreStatuses = view.IgnoreStatuses;
            config.IgnoreSystemFields = view.IgnoreSystemFields;
            config.JsonFolderPath = view.JsonFolderPath;
            if (config.MigrationConfig == null)
            {
                config.MigrationConfig = new MappingConfiguration();
            }
            Dictionary<string, Dictionary<Guid, Guid>> mappings = GetMappingsInCorrectDataType();
            config.MigrationConfig.Mappings.Clear();
            config.MigrationConfig.Mappings.AddRange(mappings);
        }

        private void WriteFormInputFromConfig()
        {
            view.SaveBatchSize = config.SaveBatchSize;
            view.IgnoreStatuses = config.IgnoreStatuses;
            view.IgnoreSystemFields = config.IgnoreSystemFields;
            view.JsonFolderPath = config.JsonFolderPath;
            if (config.MigrationConfig != null)
            {
                List<DataGridViewRow> mappingsFromConfig = GetConfigMappingsInCorrectDataGridViewType();
                UpdateView(mappingsFromConfig);
            }
        }

        private Dictionary<string, Dictionary<Guid, Guid>> GetMappingsInCorrectDataType()
        {
            Dictionary<string, Dictionary<Guid, Guid>> mappings = new Dictionary<string, Dictionary<Guid, Guid>>();
            foreach (DataGridViewRow row in view.Mappings)
            {
                if (!viewHelpers.AreAllCellsPopulated(row))
                    break;
                var entity = row.Cells[0].Value.ToString();
                var sourceId = Guid.Parse((string)row.Cells[1].Value);
                var targetId = Guid.Parse((string)row.Cells[2].Value);
                var guidsDictionary = new Dictionary<Guid, Guid>();
                mappings = GetUpdatedMappings(sourceId, targetId, entity, mappings, guidsDictionary);
            }
            return mappings;
        }

        private Dictionary<string, Dictionary<Guid, Guid>> GetUpdatedMappings(Guid sourceId, Guid targetId, string entity, Dictionary<string, Dictionary<Guid, Guid>> mappings, Dictionary<Guid, Guid> guidsDictionary)
        {
                if (DoesRowContainDefaultGuids(sourceId, targetId))
                {
                    return mappings;
                }
                guidsDictionary.Add(sourceId, targetId);
                if (DoesEntityMappingAlreadyExist(entity, mappings))
                {
                    mappings[entity].Add(sourceId, targetId);
                }
                else
                {
                    mappings.Add(entity, guidsDictionary);
                }
                return mappings;
        }

        private bool DoesRowContainDefaultGuids(Guid sourceId, Guid targetId)
        {
            if (sourceId == Guid.Empty || targetId == Guid.Empty)
            {
                return true;
            }
            return false;
        }

        private bool DoesEntityMappingAlreadyExist(string entity, Dictionary<string, Dictionary<Guid, Guid>> mappings)
        {
            if (mappings.ContainsKey(entity))
            {
                return true;
            }
            return false;
        }

        private List<DataGridViewRow> GetConfigMappingsInCorrectDataGridViewType()
        {
            var lookupMappings = new List<DataGridViewRow>();
            var entitiesDataSource = metaDataService.RetrieveEntities(organisationService);
            foreach (KeyValuePair<string, Dictionary<Guid, Guid>> entity in config.MigrationConfig.Mappings)
            {
                foreach (Guid guidToMap in entity.Value.Keys)
                {
                    var newRow = AddCellsToDataGridViewRow(entity, entitiesDataSource, guidToMap);
                    lookupMappings.Add(newRow);
                }
            }
            return lookupMappings;
        }

        private DataGridViewRow AddCellsToDataGridViewRow(KeyValuePair<string, Dictionary<Guid, Guid>> entity, List<EntityMetadata> entitiesDataSource, Guid guidToMap)
        {
            var newRow = new DataGridViewRow();
            newRow.Cells.Add(new DataGridViewComboBoxCell { Value = entity.Key, DataSource = entitiesDataSource.Select(x => x.LogicalName).OrderBy(n => n).ToList() });
            newRow.Cells.Add(new DataGridViewTextBoxCell());
            newRow.Cells[1].Value = guidToMap.ToString();
            newRow.Cells.Add(new DataGridViewTextBoxCell());
            newRow.Cells[2].Value = entity.Value[guidToMap].ToString();
            return newRow;
        }

        private void UpdateView(List<DataGridViewRow> mappingsFromConfig)
        {
            if (view.Mappings == null)
            {
                view.Mappings = mappingsFromConfig;
            }
            else
            {
                List<DataGridViewRow> lookupMappingsInView = viewHelpers.GetMappingsFromViewWithEmptyRowsRemoved(view.Mappings);
                List<DataGridViewRow> mappingsLoadedFromConfigPlusAnyManuallyAdded = lookupMappingsInView.Concat(mappingsFromConfig).ToList();
                view.Mappings = mappingsLoadedFromConfigPlusAnyManuallyAdded;
            }
        }
        
        [ExcludeFromCodeCoverage]
        private void SchemaConfigPathChanged(object sender, EventArgs args)
        {
            this.view.SchemaConfiguration = GetSchemaConfiguration();
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
            this.view.LoadConfigClicked -= LoadConfig;
            this.view.SaveConfigClicked -= SaveConfig;
            this.view.RunConfigClicked -= RunConfig;
        }
    }
}