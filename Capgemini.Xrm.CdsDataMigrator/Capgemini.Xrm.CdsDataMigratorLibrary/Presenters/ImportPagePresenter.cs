using Capgemini.Xrm.CdsDataMigratorLibrary.Services;
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

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Presenters
{
    public class ImportPagePresenter : IDisposable
    {
        private readonly IImportPageView view;
        private readonly IWorkerHost workerHost;
        private readonly IDataMigrationService dataMigrationService;
        private readonly INotifier notifier;
        
        private CrmImportConfig config;
        private string configFilePath;

        public ImportPagePresenter(IImportPageView view, IWorkerHost workerHost, IDataMigrationService dataMigrationService, INotifier notifier)
        {
            this.view = view;
            this.workerHost = workerHost;
            this.dataMigrationService = dataMigrationService;
            this.notifier = notifier;

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
                notifier.ShowError(ex);
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
                notifier.ShowError(ex);
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
                    Work = (bw, e) => dataMigrationService.ImportData(view.Service, view.DataFormat, schema, config),
                    PostWorkCallBack = (e) =>
                    {
                        if (e.Error != null)
                        {
                            notifier.ShowError(e.Error);
                        }
                        else
                        {
                            notifier.ShowSuccess("Data import is complete.");
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                notifier.ShowError(ex);
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
        }

        private Dictionary<string, Dictionary<Guid, Guid>> GetMappingsInCorrectDataType()
        {
            Dictionary<string, Dictionary<Guid, Guid>> mappings = new Dictionary<string, Dictionary<Guid, Guid>>();
            foreach (DataGridViewRow row in view.Mappings)
            {
                if (!AreAllCellsPopulated(row))
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
    
        private static bool AreAllCellsPopulated(DataGridViewRow row)
        {
            if (string.IsNullOrEmpty((string)row.Cells[0].Value) || string.IsNullOrEmpty((string)row.Cells[1].Value) || string.IsNullOrEmpty((string)row.Cells[2].Value))
            {
                return false;
            }
            return true;
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