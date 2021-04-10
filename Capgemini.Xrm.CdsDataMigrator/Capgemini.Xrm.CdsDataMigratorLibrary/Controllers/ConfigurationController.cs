using Capgemini.Xrm.CdsDataMigratorLibrary.Core;
using Capgemini.Xrm.CdsDataMigratorLibrary.Forms;
using Capgemini.Xrm.CdsDataMigratorLibrary.Services;
using Capgemini.Xrm.DataMigration.Config;
using Capgemini.Xrm.DataMigration.CrmStore.Config;
using Capgemini.Xrm.DataMigration.Model;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using NuGet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Controllers
{
    public class ConfigurationController : ControllerBase
    {
        public void DataConversion(Dictionary<string, List<Item<EntityReference, EntityReference>>> inputMapping, Dictionary<string, Dictionary<Guid, Guid>> inputMapper)
        {
            inputMapping.Clear();
            foreach (var mappings in inputMapper)
            {
                var list = new List<Item<EntityReference, EntityReference>>();

                foreach (var values in mappings.Value)
                {
                    list.Add(new Item<EntityReference, EntityReference>(new EntityReference(mappings.Key, values.Key), new EntityReference(mappings.Key, values.Value)));
                }

                inputMapping.Add(mappings.Key, list);
            }
        }

        public void LoadImportConfigFile(INotificationService notificationService, TextBox importConfig, Dictionary<string, Dictionary<Guid, Guid>> inputMapper, Dictionary<string, List<Item<EntityReference, EntityReference>>> inputMapping)
        {
            if (!string.IsNullOrWhiteSpace(importConfig.Text))
            {
                try
                {
                    var configImport = CrmImportConfig.GetConfiguration(importConfig.Text);
                    if (configImport.MigrationConfig == null)
                    {
                        notificationService.DisplayFeedback("Invalid Import Config File");
                        importConfig.Text = "";
                        return;
                    }

                    inputMapper = configImport.MigrationConfig.Mappings;
                    DataConversion(inputMapping, inputMapper);

                    notificationService.DisplayFeedback("Guid Id Mappings loaded from Import Config File");
                }
                catch (Exception ex)
                {
                    notificationService.DisplayFeedback($"Load Correct Import Config file, error:{ex.Message}");
                }
            }
        }

        public void LoadExportConfigFile(INotificationService notificationService, TextBox exportConfigTextBox, Dictionary<string, string> inputFilterQuery, Dictionary<string, Dictionary<string, List<string>>> inputLookupMaping)
        {
            if (!string.IsNullOrWhiteSpace(exportConfigTextBox.Text))
            {
                try
                {
                    var configFile = CrmExporterConfig.GetConfiguration(exportConfigTextBox.Text);
                    if (!configFile.CrmMigrationToolSchemaPaths.Any())
                    {
                        notificationService.DisplayFeedback("Invalid Export Config File");
                        exportConfigTextBox.Text = "";
                        return;
                    }

                    inputFilterQuery.Clear();
                    inputFilterQuery.AddRange(configFile.CrmMigrationToolSchemaFilters);

                    inputLookupMaping.Clear();
                    inputLookupMaping.AddRange(configFile.LookupMapping);

                    notificationService.DisplayFeedback("Filters and Lookup Mappings loaded from Export Config File");
                }
                catch (Exception ex)
                {
                    notificationService.DisplayFeedback($"Load Correct Export Config file, error:{ex.Message}");
                }
            }
        }

        public void GenerateImportConfigFile(INotificationService notificationService, TextBox importConfig, Dictionary<string, Dictionary<Guid, Guid>> inputMapper)
        {
            try
            {
                CrmImportConfig migration = new CrmImportConfig()
                {
                    IgnoreStatuses = true,
                    IgnoreSystemFields = true,
                    SaveBatchSize = 1000,
                    JsonFolderPath = "ExtractedData"
                };

                if (File.Exists(importConfig.Text))
                {
                    migration = CrmImportConfig.GetConfiguration(importConfig.Text);
                }

                if (migration.MigrationConfig == null)
                {
                    migration.MigrationConfig = new MappingConfiguration();
                }

                if (inputMapper != null)
                {
                    migration.MigrationConfig.Mappings.Clear();
                    migration.MigrationConfig.Mappings.AddRange(inputMapper);

                    if (File.Exists(importConfig.Text))
                    {
                        File.Delete(importConfig.Text);
                    }

                    migration.SaveConfiguration(importConfig.Text);
                }
            }
            catch (Exception ex)
            {
                notificationService.DisplayFeedback($"Error Saving Import Config file, Error:{ex.Message}");
            }
        }

        public void GenerateExportConfigFile(TextBox exportConfig, TextBox schemaPath, Dictionary<string, string> inputFilterQuery, Dictionary<string, Dictionary<string, List<string>>> inputLookupMaping)
        {
            CrmExporterConfig config = new CrmExporterConfig()
            {
                JsonFolderPath = "ExtractedData",
            };

            if (File.Exists(exportConfig.Text))
            {
                config = CrmExporterConfig.GetConfiguration(exportConfig.Text);
            }

            config.CrmMigrationToolSchemaFilters.Clear();
            config.CrmMigrationToolSchemaFilters.AddRange(inputFilterQuery);

            if (!string.IsNullOrWhiteSpace(schemaPath.Text))
            {
                config.CrmMigrationToolSchemaPaths.Clear();
                config.CrmMigrationToolSchemaPaths.Add(schemaPath.Text);
            }

            if (inputLookupMaping.Count > 0)
            {
                config.LookupMapping.Clear();
                config.LookupMapping.AddRange(inputLookupMaping);
            }

            if (File.Exists(exportConfig.Text))
            {
                File.Delete(exportConfig.Text);
            }

            config.SaveConfiguration(exportConfig.Text);
        }
    }
}