using Capgemini.DataMigration.Resiliency.Polly;
using Capgemini.Xrm.DataMigration.Config;
using Capgemini.Xrm.DataMigration.Core;
using Capgemini.Xrm.DataMigration.CrmStore.Config;
using Capgemini.Xrm.DataMigration.Engine;
using Capgemini.Xrm.DataMigration.Model;
using Capgemini.Xrm.DataMigration.Repositories;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Forms;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Model;
using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;
using static Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core.SettingFileHandler;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin
{
    public partial class PluginControl : PluginControlBase, IXrmToolBoxPluginControl
    {
        #region Private Readonly Object Initialization

        private readonly DataMigrationSettings _dataMigrationSettings = new DataMigrationSettings();
        private readonly DeserializationSettings _deserialization = new DeserializationSettings();
        private readonly SerializationSettings _xmlSettings = new SerializationSettings();
        private readonly ExportConfigSettings _exportSchemaSettings = new ExportConfigSettings();
        private readonly ImportConfigSettingscs _importSchemaSettings = new ImportConfigSettingscs();
        private readonly CrmSchemaConfiguration _crmSchemaConfiguration = new CrmSchemaConfiguration();
        private readonly AttributeTypeMapping _attributeMapping = new AttributeTypeMapping();
        private readonly SaveAllSettings _saveAllSettings = new SaveAllSettings();
        private readonly LoadAllSettings _loadAllSettings = new LoadAllSettings();

        #endregion Private Readonly Object Initialization

        #region Private Properties

        private readonly MessageLogger _logger;
        private bool _workingstate;
        private Task _currentTask;
        private Panel _informationPanel;
        private IOrganizationService _service;
        private Guid _organisationid;
        private Settings _settings;
        private string _entityLogicalName;
        private Dictionary<string, HashSet<string>> _entityAttributes = new Dictionary<string, HashSet<string>>();
        private Dictionary<string, HashSet<string>> _entityRelationships = new Dictionary<string, HashSet<string>>();
        private Dictionary<string, string> _filterQuery = new Dictionary<string, string>();
        private Dictionary<string, Dictionary<string, List<string>>> _lookupMaping = new Dictionary<string, Dictionary<string, List<string>>>();
        private Dictionary<string, List<Item<EntityReference, EntityReference>>> _mapping = new Dictionary<string, List<Item<EntityReference, EntityReference>>>();
        private Dictionary<string, Dictionary<Guid, Guid>> _mapper = new Dictionary<string, Dictionary<Guid, Guid>>();
        private HashSet<string> _checkedEntity = new HashSet<string>();
        private HashSet<string> _selectedEntity = new HashSet<string>();
        private HashSet<string> _checkedRelationship = new HashSet<string>();
        private List<EntityMetadata> _cachedMetadata;

        #endregion Private Properties

        private CancellationTokenSource _tokenSource = new CancellationTokenSource();

        public new event EventHandler OnRequestConnection;

        public PluginControl()
        {
            SettingFileHandler.GetConfigData(out _settings);
            InitializeComponent();

            comboBox1_SelectedIndexChanged(null, null);

            _logger = new MessageLogger(tbMessage, SynchronizationContext.Current);
        }

        private void CollectUserSettings()
        {
            _dataMigrationSettings.FailedValidationMessage = "";
            _dataMigrationSettings.BatchSize = Convert.ToInt32(batchSize.Value);
            _dataMigrationSettings.PageSize = Convert.ToInt32(pageSize.Value);
            _dataMigrationSettings.TopCount = Convert.ToInt32(topCount.Value);
            _dataMigrationSettings.SchemaFilePath = tbSchemaFilePath.Text;
            _dataMigrationSettings.JsonFolderPath = tbFolderPath.Text;
        }

        private async void importButton_Click(object sender, EventArgs e)
        {
            if (btImport.Text == "Import")
            {
                btExport.Enabled = false;
                btExportCsv.Enabled = false;
                btImportCSV.Enabled = false;
                btImport.Text = "STOP";
                _currentTask = StartImport();

                try
                {
                    await _currentTask;
                }
                catch (Exception ex)
                {
                    _logger.Error("Import Error:", ex);
                }

                btExport.Enabled = true;
                btExportCsv.Enabled = true;
                btImportCSV.Enabled = true;
                btImport.Text = "Import";
            }
            else
            {
                _tokenSource.Cancel();
                try
                {
                    await _currentTask;
                }
                catch (Exception ex)
                {
                    _logger.Error("Export Error:", ex);
                }
            }
        }

        private async void exportButton_Click(object sender, EventArgs e)
        {
            if (btExport.Text == "Export")
            {
                if (cbMinJson.Checked)
                    JsonSerializerConfig.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.None;
                else
                    JsonSerializerConfig.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;

                btImport.Enabled = false;
                btImportCSV.Enabled = false;
                btExportCsv.Enabled = false;
                btExport.Text = "STOP";
                _currentTask = StartExport();

                try
                {
                    await _currentTask;
                }
                catch (Exception ex)
                {
                    _logger.Error("Export Error:", ex);
                }

                btImport.Enabled = true;
                btExportCsv.Enabled = true;
                btImportCSV.Enabled = true;
                btExport.Text = "Export";
            }
            else
            {
                _tokenSource.Cancel();

                try
                {
                    await _currentTask;
                }
                catch (Exception ex)
                {
                    _logger.Error("Export Error:", ex);
                }
            }
        }

        public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter = null)
        {
            switch (actionName)
            {
                case "TargetOrganization":
                    _dataMigrationSettings.TargetConnectionString = detail.ConnectionString;
                    _dataMigrationSettings.TargetServiceClient = detail.ServiceClient;
                    labelTargetConnectionString.Text = detail.ConnectionName;
                    break;

                case "SourceOrganization":
                    _dataMigrationSettings.SourceConnectionString = detail.ConnectionString;
                    _dataMigrationSettings.SourceServiceClient = detail.ServiceClient;
                    _service = newService;
                    _organisationid = detail.ConnectionId.Value;
                    labelSourceConnectionString.Text = detail.ConnectionName;
                    labelSchemaConnectionString.Text = detail.ConnectionName;
                    lblRCountConnectionString.Text = detail.ConnectionName;
                    tsBtnExecuteCount.Enabled = true;
                    SettingFileHandler.SaveConfigData(_settings);
                    if (tabControl.SelectedTab == tabPageSchema)
                        RefreshEntities();
                    _cachedMetadata = null;
                    break;

                default:
                    if (detail.ConnectionName != null)
                    {
                        _dataMigrationSettings.SourceConnectionString = detail.ConnectionString;
                        _dataMigrationSettings.SourceServiceClient = detail.ServiceClient;
                        _service = newService;
                        _organisationid = detail.ConnectionId.Value;
                        labelSourceConnectionString.Text = detail.ConnectionName;
                        labelSchemaConnectionString.Text = detail.ConnectionName;
                        lblRCountConnectionString.Text = detail.ConnectionName;
                        SettingFileHandler.SaveConfigData(_settings);
                        tsBtnExecuteCount.Enabled = true;
                        _cachedMetadata = null;
                    }
                    break;
            }
        }

        private void buttonSourceConnectionString_Click(object sender, EventArgs e)
        {
            if (OnRequestConnection != null)
            {
                var args = new RequestConnectionEventArgs { ActionName = "SourceOrganization", Control = this };
                OnRequestConnection(this, args);
            }
        }

        private void buttonTargetConnectionString_Click(object sender, EventArgs e)
        {
            if (OnRequestConnection != null)
            {
                var args = new RequestConnectionEventArgs { ActionName = "TargetOrganization", Control = this };
                OnRequestConnection(this, args);
            }
        }

        private void btSeFile_Click(object sender, EventArgs e)
        {
            fdSchemaFile.DefaultExt = "xml";
            DialogResult fd = fdSchemaFile.ShowDialog();
            if (fd == DialogResult.OK)
            {
                tbSchemaFilePath.Text = fdSchemaFile.FileName;
            }
        }

        private void btFolderPath_Click(object sender, EventArgs e)
        {
            var result = fbExportPath.ShowDialog();

            if (result == DialogResult.OK)
            {
                tbFolderPath.Text = fbExportPath.SelectedPath;
            }
        }

        private async Task StartExport(bool useCSV = false)
        {
            StatusLabel.Text = "";
            CollectUserSettings();
            _dataMigrationSettings.ValidateExport();
            Validate();
            if (_dataMigrationSettings.FailedValidation)
            {
                MessageBox.Show(_dataMigrationSettings.FailedValidationMessage);
                return;
            }

            _tokenSource = new CancellationTokenSource();

            var exportTask = Task.Run(() =>
        {
            var orgService = ConnectionHelper.GetOrganizationalService(_dataMigrationSettings.SourceServiceClient);
            _logger.Info("Connectd to instance " + _dataMigrationSettings.SourceServiceClient.ConnectedOrgFriendlyName);
            EntityRepository entityRepo = new EntityRepository(orgService, new ServiceRetryExecutor());

            //TODO add support for multiple schema files and XMLFolderPath
            CrmExporterConfig exportConfig = new CrmExporterConfig()
            {
                CrmMigrationToolSchemaPaths = new List<string> { _dataMigrationSettings.SchemaFilePath },
                BatchSize = Convert.ToInt32(_dataMigrationSettings.BatchSize),
                PageSize = Convert.ToInt32(_dataMigrationSettings.PageSize),
                TopCount = Convert.ToInt32(_dataMigrationSettings.TopCount),
                OnlyActiveRecords = cbOnlyActive.Checked,
                JsonFolderPath = _dataMigrationSettings.JsonFolderPath,
                CrmMigrationToolSchemaFilters = _dataMigrationSettings.ExportConfig != null ? _dataMigrationSettings.ExportConfig.CrmMigrationToolSchemaFilters : null,
                OneEntityPerBatch = _dataMigrationSettings.ExportConfig == null || _dataMigrationSettings.ExportConfig.OneEntityPerBatch,
            };

            if (_dataMigrationSettings.ExportConfig != null)
            {
                exportConfig.ExcludedFields = _dataMigrationSettings.ExportConfig.ExcludedFields;
                exportConfig.LookupMapping = _dataMigrationSettings.ExportConfig.LookupMapping;
                exportConfig.FilePrefix = _dataMigrationSettings.ExportConfig.FilePrefix;
                exportConfig.SeperateFilesPerEntity = _dataMigrationSettings.ExportConfig.SeperateFilesPerEntity;
            }

            if (!useCSV)
            {
                CrmFileDataExporter fileExporter = new CrmFileDataExporter(_logger, entityRepo, exportConfig, _tokenSource.Token);
                fileExporter.MigrateData();
            }
            else
            {
                CrmSchemaConfiguration schema = CrmSchemaConfiguration.ReadFromFile(_dataMigrationSettings.SchemaFilePath);
                CrmFileDataExporterCsv fileExporter = new CrmFileDataExporterCsv(_logger, entityRepo, exportConfig, _tokenSource.Token, schema);
                fileExporter.MigrateData();
            }
        });

            await exportTask;
        }

        private async Task StartImport(bool useCSV = false)
        {
            StatusLabel.Text = "";
            CollectUserSettings();
            _dataMigrationSettings.ValidateImport();
            Validate();
            if (_dataMigrationSettings.FailedValidation)
            {
                MessageBox.Show(_dataMigrationSettings.FailedValidationMessage);
                return;
            }

            _tokenSource = new CancellationTokenSource();

            var importTask = Task.Run(() =>
            {
                var orgService = ConnectionHelper.GetOrganizationalService(_dataMigrationSettings.TargetServiceClient);
                _logger.Info("Connectd to instance " + _dataMigrationSettings.TargetServiceClient.ConnectedOrgFriendlyName);

                //TODO make it all configurable
                //TODO Add support for fieldstoignore and migrationconfig (mapping)
                //Add option to Save/Read Import config file - there are alread Read/Save methods implemented
                CrmImportConfig importConfig = new CrmImportConfig()
                {
                    IgnoreStatuses = cbIgnoreStatuses.Checked,
                    IgnoreStatusesExceptions = _dataMigrationSettings.ImportConfig != null ? _dataMigrationSettings.ImportConfig.IgnoreStatusesExceptions : null,
                    IgnoreSystemFields = cbIgnoreSystemFields.Checked,
                    AdditionalFieldsToIgnore = _dataMigrationSettings.ImportConfig != null ? _dataMigrationSettings.ImportConfig.AdditionalFieldsToIgnore : null,
                    SaveBatchSize = Convert.ToInt32(nudSavePageSize.Value),
                    JsonFolderPath = _dataMigrationSettings.JsonFolderPath,
                    EntitiesToSync = _dataMigrationSettings.ImportConfig != null ? _dataMigrationSettings.ImportConfig.EntitiesToSync : null,
                    NoUpsertEntities = _dataMigrationSettings.ImportConfig != null ? _dataMigrationSettings.ImportConfig.NoUpsertEntities : null,
                    DeactivateAllProcesses = _dataMigrationSettings.ImportConfig != null ? _dataMigrationSettings.ImportConfig.DeactivateAllProcesses : false,
                    PluginsToDeactivate = _dataMigrationSettings.ImportConfig != null ? _dataMigrationSettings.ImportConfig.PluginsToDeactivate : null,
                    ProcessesToDeactivate = _dataMigrationSettings.ImportConfig != null ? _dataMigrationSettings.ImportConfig.ProcessesToDeactivate : null
                };

                if (_dataMigrationSettings.ImportConfig != null)
                {
                    importConfig.FilePrefix = _dataMigrationSettings.ImportConfig.FilePrefix;
                    importConfig.MigrationConfig = _dataMigrationSettings.ImportConfig.MigrationConfig;
                    importConfig.PassOneReferences = _dataMigrationSettings.ImportConfig.PassOneReferences;
                }

                if (nudMaxThreads.Value > 1 && !string.IsNullOrWhiteSpace(_dataMigrationSettings.TargetConnectionString))
                {
                    _logger.Info("Starting MultiThreaded Processing, using " + nudMaxThreads.Value + " threads");
                    List<IEntityRepository> repos = new List<IEntityRepository>();
                    int cnt = Convert.ToInt32(nudMaxThreads.Value);

                    while (cnt > 0)
                    {
                        cnt--;
                        repos.Add(new EntityRepository(ConnectionHelper.GetOrganizationalService(_dataMigrationSettings.TargetConnectionString), new ServiceRetryExecutor()));
                        _logger.Info("New connection created to " + _dataMigrationSettings.TargetServiceClient.ConnectedOrgFriendlyName);
                    }

                    CrmFileDataImporter fileExporter = new CrmFileDataImporter(_logger, repos, importConfig, _tokenSource.Token);
                    fileExporter.MigrateData();
                }
                else
                {
                    _logger.Info("Starting Single Threaded processing, you must configure connection string for multithreaded processing adn set up max threads to more than 1");
                    EntityRepository entityRepo = new EntityRepository(orgService, new ServiceRetryExecutor());

                    if (!useCSV)
                    {
                        CrmFileDataImporter fileExporter = new CrmFileDataImporter(_logger, entityRepo, importConfig, _tokenSource.Token);
                        fileExporter.MigrateData();
                    }
                    else
                    {
                        CrmSchemaConfiguration schema = CrmSchemaConfiguration.ReadFromFile(_dataMigrationSettings.SchemaFilePath);
                        CrmFileDataImporterCsv fileExporter = new CrmFileDataImporterCsv(_logger, entityRepo, importConfig, schema, _tokenSource.Token);
                        fileExporter.MigrateData();
                    }
                }
            });

            await importTask;
        }

        private void btClearLog_Click(object sender, EventArgs e)
        {
            tbMessage.Text = "";
        }

        private void tbLoadImportConfigFile_Click(object sender, EventArgs e)
        {
            fdSchemaFile.DefaultExt = "json";

            DialogResult fd = fdSchemaFile.ShowDialog();

            if (fd == DialogResult.OK)
            {
                tbImportConfigFile.Text = fdSchemaFile.FileName;
                _dataMigrationSettings.ImportConfig = CrmImportConfig.GetConfiguration(tbImportConfigFile.Text);
            }

            if (_dataMigrationSettings.ImportConfig != null)
            {
                cbIgnoreSystemFields.Checked = _dataMigrationSettings.ImportConfig.IgnoreSystemFields;
                cbIgnoreStatuses.Checked = _dataMigrationSettings.ImportConfig.IgnoreStatuses;
                tbFolderPath.Text = _dataMigrationSettings.ImportConfig.JsonFolderPath;
                nudSavePageSize.Value = _dataMigrationSettings.ImportConfig.SaveBatchSize;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.Text)
            {
                case "Error":
                    MessageLogger.LogLevel = 0;
                    break;

                case "Warning":
                    MessageLogger.LogLevel = 1;
                    break;

                case "Info":
                    MessageLogger.LogLevel = 2;
                    break;

                case "Verbose":
                    MessageLogger.LogLevel = 3;
                    break;

                default:
                    MessageLogger.LogLevel = 2;
                    break;
            }
        }

        private void tsbCloseThisTab_Click(object sender, EventArgs e)
        {
            CloseTool();
        }

        private void btSchema_Click(object sender, EventArgs e)
        {
            if (OnRequestConnection != null)
            {
                var args = new RequestConnectionEventArgs { ActionName = "SchemaOrganization", Control = this };
                OnRequestConnection(this, args);
            }
        }

        private void tsbtRetrieveEntities_Click(object sender, EventArgs e)
        {
            ClearMemory();
            PopulateEntities();
        }

        private void ClearMemory()
        {
            ClearInternalMemory();
            ClearUIMemory();
        }

        private void ClearInternalMemory()
        {
            _checkedEntity.Clear();
            _entityAttributes.Clear();
            _entityRelationships.Clear();
            _mapper.Clear();
            _lookupMaping.Clear();
            _filterQuery.Clear();
            _dataMigrationSettings.JsonFolderPath = null;
            _importSchemaSettings.JsonFilePath = null;
            _exportSchemaSettings.JsonFilePath = null;
            _deserialization.XmlFolderPath = null;
            _selectedEntity.Clear();
            _checkedRelationship.Clear();
            _mapping.Clear();
        }

        private void ClearUIMemory()
        {
            tbSchemaPath.Clear();
            tbImportConfig.Clear();
            tbExportConfig.Clear();
        }

        private void lvEntities_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetEntityLogicalName();
            PopulateAttributes(_entityLogicalName, _service);
            PopulateRelationship(_entityLogicalName, _service);
            AddSelectedEntities();
        }

        private void AddSelectedEntities()
        {
            if (lvEntities.SelectedItems.Count > 0)
            {
                if (!(string.IsNullOrEmpty(_entityLogicalName) && _selectedEntity.Contains(_entityLogicalName)))
                {
                    _selectedEntity.Add(_entityLogicalName);
                }
            }
        }

        private void GetEntityLogicalName()
        {
            if (lvEntities.SelectedItems.Count > 0)
            {
                var entityitem = lvEntities.SelectedItems[0];
                if (entityitem != null && entityitem.Tag != null)
                {
                    var entity = (EntityMetadata)entityitem.Tag;
                    _entityLogicalName = entity.LogicalName;
                }
            }
        }

        private void PopulateRelationship(string entityLogicalName, IOrganizationService service)
        {
            if (!_workingstate)
            {
                lvRelationship.Items.Clear();
                InitFilter();
                if (lvEntities.SelectedItems.Count > 0)
                {
                    var bwFill = new BackgroundWorker();
                    bwFill.DoWork += (sender, e) =>
                    {
                        var entitymeta = MetadataHelper.RetrieveEntities(entityLogicalName, service);
                        var sourceAttributesList = new List<ListViewItem>();
                        if (entitymeta.ManyToManyRelationships.Any())
                        {
                            foreach (var relationship in entitymeta.ManyToManyRelationships)
                            {
                                var item = new ListViewItem(relationship.IntersectEntityName);
                                AddRelationship(relationship, item, sourceAttributesList);
                                UpdateCheckBoxesRelationShip(relationship, item);
                            }
                        }
                        e.Result = sourceAttributesList;
                    };
                    bwFill.RunWorkerCompleted += (sender, e) =>
                    {
                        AsyncRunnerCompleteRelationShip(e);
                    };
                    bwFill.RunWorkerAsync();
                }
            }
        }

        private void AsyncRunnerCompleteRelationShip(RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(this, "An error occured: " + e.Error.Message, "Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            else
            {
                var items = (List<ListViewItem>)e.Result;
                lvRelationship.Items.AddRange(items.ToArray());
            }
            ManageWorkingState(false);
        }

        private void UpdateCheckBoxesRelationShip(ManyToManyRelationshipMetadata relationship, ListViewItem item)
        {
            if (_entityRelationships.ContainsKey(_entityLogicalName))
            {
                foreach (string attr in _entityRelationships[_entityLogicalName])
                {
                    item.Checked |= attr.Equals(relationship.IntersectEntityName);
                }
            }
        }

        private void AddRelationship(ManyToManyRelationshipMetadata relationship, ListViewItem item, List<ListViewItem> sourceAttributesList)
        {
            item.SubItems.Add(relationship.IntersectEntityName);
            item.SubItems.Add(relationship.Entity2LogicalName);
            item.SubItems.Add(relationship.Entity2IntersectAttribute);
            sourceAttributesList.Add(item);
        }

        private void PopulateAttributes(string entityLogicalName, IOrganizationService service)
        {
            if (!_workingstate)
            {
                lvAttributes.Items.Clear();
                chkAllAttributes.Checked = true;
                InitFilter();
                if (lvEntities.SelectedItems.Count > 0)
                {
                    var bwFill = new BackgroundWorker();
                    bwFill.DoWork += (sender, e) =>
                    {
                        var entitymeta = MetadataHelper.RetrieveEntities(entityLogicalName, service);
                        var unmarkedattributes = _settings[_organisationid][_entityLogicalName].UnmarkedAttributes;
                        var sourceAttributesList = new List<ListViewItem>();
                        var attributes = entitymeta.Attributes.ToArray();

                        if (!cbShowSystemAttributes.Checked)
                        {
                            attributes = attributes.Where(p => p.IsLogical != null
                            && !p.IsLogical.Value
                            && p.IsValidForRead != null
                            && p.IsValidForRead.Value
                            && ((p.IsValidForCreate != null && p.IsValidForCreate.Value) || (p.IsValidForUpdate != null && p.IsValidForUpdate.Value))).ToArray();
                        }

                        attributes = attributes.OrderByDescending(p => p.IsPrimaryId).ThenByDescending(p => p.IsPrimaryName).ThenByDescending(p => p.IsCustomAttribute.Value).ThenBy(p => p.IsLogical.Value).ThenBy(p => p.LogicalName).ToArray();

                        foreach (AttributeMetadata attribute in attributes)
                        {
                            var name = attribute.DisplayName.UserLocalizedLabel == null ? string.Empty : attribute.DisplayName.UserLocalizedLabel.Label;
                            var typename = attribute.AttributeTypeName == null ? string.Empty : attribute.AttributeTypeName.Value;
                            var item = new ListViewItem(name);
                            AddAttribute(attribute, item, typename);
                            InvalidUpdate(attribute, item);
                            item.Checked = unmarkedattributes.Contains(attribute.LogicalName);
                            UpdateCheckBoxesAttribute(attribute, item);
                            sourceAttributesList.Add(item);
                            //}
                        }
                        e.Result = sourceAttributesList;
                    };
                    bwFill.RunWorkerCompleted += (sender, e) =>
                    {
                        AsyncRunnerCompleteAttributeOperation(e);
                    };
                    bwFill.RunWorkerAsync();
                }
            }
        }

        private void AsyncRunnerCompleteAttributeOperation(RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(this, "An error occured: " + e.Error.Message, "Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            else
            {
                var items = (List<ListViewItem>)e.Result;
                lvAttributes.Items.AddRange(items.ToArray());
            }

            ManageWorkingState(false);
        }

        private void UpdateCheckBoxesAttribute(AttributeMetadata attribute, ListViewItem item)
        {
            if (_entityAttributes.ContainsKey(_entityLogicalName))
            {
                foreach (string attr in _entityAttributes[_entityLogicalName])
                {
                    item.Checked |= attr.Equals(attribute.LogicalName);
                }
            }
        }

        private void InvalidUpdate(AttributeMetadata attribute, ListViewItem item)
        {
            item.ToolTipText = "";

            if (attribute.IsValidForCreate != null && !attribute.IsValidForCreate.Value)
            {
                item.ForeColor = Color.Gray;
                item.ToolTipText = "Not createable, ";
            }

            if (attribute.IsValidForUpdate != null && !attribute.IsValidForUpdate.Value)
            {
                item.ForeColor = Color.Gray;
                item.ToolTipText += "Not updateable, ";
            }

            if (attribute.IsCustomAttribute != null && attribute.IsCustomAttribute.Value)
            {
                item.ForeColor = Color.DarkGreen;
            }

            if (attribute.IsPrimaryId != null && attribute.IsPrimaryId.Value)
            {
                item.ForeColor = Color.DarkBlue;
            }

            if (attribute.IsPrimaryName != null && attribute.IsPrimaryName.Value)
            {
                item.ForeColor = Color.DarkBlue;
            }

            if (attribute.AttributeType == AttributeTypeCode.Virtual || attribute.AttributeType == AttributeTypeCode.ManagedProperty)
            {
                item.ForeColor = Color.Red;
                item.ToolTipText += "Virtual or managed property, ";
            }

            if (attribute.IsLogical != null && attribute.IsLogical.Value)
            {
                item.ForeColor = Color.Red;
                item.ToolTipText += "Logical attribute, ";
            }

            if (attribute.IsValidForCreate != null && !attribute.IsValidForCreate.Value &&
                attribute.IsValidForUpdate != null && !attribute.IsValidForUpdate.Value)
            {
                item.ForeColor = Color.Red;
            }

            if (attribute.IsValidForRead != null && !attribute.IsValidForRead.Value)
            {
                item.ForeColor = Color.Red;
                item.ToolTipText += "Not readable, ";
            }

            if (attribute.Description != null && attribute.Description.LocalizedLabels.Count > 0)
            {
                item.ToolTipText += attribute.Description.LocalizedLabels.First().Label;
            }

            if (!string.IsNullOrWhiteSpace(attribute.DeprecatedVersion))
            {
                item.ForeColor = Color.Red;
                item.ToolTipText += "DeprecatedVersion:" + attribute.DeprecatedVersion;
            }

            item.SubItems.Add(item.ToolTipText);
        }

        private void AddAttribute(AttributeMetadata attribute, ListViewItem item, string typename)
        {
            item.Tag = attribute;
            item.SubItems.Add(attribute.LogicalName);
            item.SubItems.Add(typename.EndsWith("Type", StringComparison.Ordinal) ? typename.Substring(0, typename.LastIndexOf("Type", StringComparison.Ordinal)) : typename);
        }

        private void InitFilter()
        {
            string filter = null;

            if (lvEntities.SelectedItems.Count > 0)
            {
                var entityitem = lvEntities.SelectedItems[0];

                if (entityitem != null && entityitem.Tag != null)
                {
                    var entity = (EntityMetadata)entityitem.Tag;
                    filter = _settings[_organisationid][entity.LogicalName].Filter;
                }
            }

            tsbtFilters.ForeColor = string.IsNullOrEmpty(filter) ? Color.Black : Color.Blue;
        }

        private void PopulateEntities()
        {
            if (!_workingstate)
            {
                ClearAllListViews();
                ManageWorkingState(true);

                _informationPanel = InformationPanel.GetInformationPanel(this, "Loading entities...", 340, 150);

                var bwFill = new BackgroundWorker();
                bwFill.DoWork += (sender, e) =>
                {
                    List<EntityMetadata> sourceList = MetadataHelper.RetrieveEntities(_service);
                    if (!cbShowSystemAttributes.Checked)
                    {
                        sourceList = sourceList.Where(p => !p.IsLogicalEntity.Value && !p.IsIntersect.Value).ToList();
                    }

                    _cachedMetadata = sourceList.OrderBy(p => p.IsLogicalEntity.Value).ThenBy(p => p.IsIntersect.Value).ThenByDescending(p => p.IsCustomEntity.Value).ThenBy(p => p.LogicalName).ToList();

                    var sourceEntitiesList = new List<ListViewItem>();

                    foreach (EntityMetadata entity in _cachedMetadata)
                    {
                        var name = entity.DisplayName.UserLocalizedLabel == null ? string.Empty : entity.DisplayName.UserLocalizedLabel.Label;
                        var item = new ListViewItem(name);
                        item.Tag = entity;
                        item.SubItems.Add(entity.LogicalName);
                        IsInvalidForCustomization(entity, item);
                        UpdateCheckBoxesEntities(entity, item);

                        sourceEntitiesList.Add(item);
                    }
                    e.Result = sourceEntitiesList;
                };
                bwFill.RunWorkerCompleted += (sender, e) =>
                {
                    _informationPanel.Dispose();
                    AsyncRunnerCompleteEntitiesOperation(e);
                };
                bwFill.RunWorkerAsync();
            }
        }

        private void IsInvalidForCustomization(EntityMetadata entity, ListViewItem item)
        {
            if (entity.IsCustomEntity.Value)
            {
                item.ForeColor = Color.DarkGreen;
            }

            if (entity.IsIntersect.Value)
            {
                item.ForeColor = Color.Red;
                item.ToolTipText = "Intersect Entity, ";
            }

            if (entity.IsLogicalEntity.Value)
            {
                item.ForeColor = Color.Red;
                item.ToolTipText = "Logical Entity";
            }
        }

        private void UpdateCheckBoxesEntities(EntityMetadata entity, ListViewItem item)
        {
            item.Checked |= _entityAttributes.ContainsKey(entity.LogicalName);
        }

        private void AsyncRunnerCompleteEntitiesOperation(RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(this, "An error occured: " + e.Error.Message, "Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            else
            {
                var items = (List<ListViewItem>)e.Result;
                if (items.Count == 0)
                {
                    MessageBox.Show(this, "The system does not contain any entities", "Warning", MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                }
                else
                {
                    lvEntities.Items.AddRange(items.ToArray());
                }
            }

            ManageWorkingState(false);
        }

        private void ManageWorkingState(bool working)
        {
            _workingstate = working;
            gbEntities.Enabled = !working;
            gbAttributes.Enabled = !working;
            gbRelationship.Enabled = !working;
            btSchemaFolderPath.Enabled = !working;
            tbSchemaPath.Enabled = !working;
            btImportConfigPath.Enabled = !working;
            tbImportConfig.Enabled = !working;
            btExportConfigPath.Enabled = !working;
            tbExportConfig.Enabled = !working;

            Cursor = working ? Cursors.WaitCursor : Cursors.Default;
        }

        private void tabControl_Selected(object sender, TabControlEventArgs e)
        {
            if (tabControl.SelectedTab == tabPageSchema && string.IsNullOrWhiteSpace(labelSourceConnectionString.Text.ToString()))
            {
                toolStrip2.Enabled = false;
            }
            else
            {
                if (tabControl.SelectedTab == tabPageSchema)
                    RefreshEntities();
            }
        }

        private void RefreshEntities()
        {
            toolStrip2.Enabled = true;
            labelSchemaConnectionString.Text = labelSourceConnectionString.Text;
            if (_cachedMetadata == null)
            {
                ClearMemory();
                PopulateEntities();
            }
        }

        private void tsbtMappings_Click(object sender, EventArgs e)
        {
            if (lvEntities.Items.Count != 0 && lvEntities.SelectedItems.Count > 0)
            {
                if (!string.IsNullOrEmpty(_entityLogicalName))
                {
                    if (_mapping.ContainsKey(_entityLogicalName))
                    {
                        MappingIfContainsKey();
                    }
                    else
                    {
                        MappingIfKeyDoesNotExist();
                    }
                }
            }
            else
            {
                MessageBox.Show("Entity is not selected");
            }
        }

        private void MappingIfKeyDoesNotExist()
        {
            List<Item<EntityReference, EntityReference>> mappingReference = new List<Item<EntityReference, EntityReference>>();
            var mappingDialog = new MappingList(mappingReference);
            mappingDialog.StartPosition = FormStartPosition.CenterParent;
            mappingDialog.ShowDialog(ParentForm);

            var mapList = mappingDialog.GetMappingList(_entityLogicalName);
            var guidMapList = mappingDialog.GetGuidMappingList();

            if (mapList.Count > 0)
            {
                _mapping.Add(_entityLogicalName, mapList);
                _mapper.Add(_entityLogicalName, guidMapList);
            }

            InitMappings();
            _settings[_organisationid].Mappings.Clear();
        }

        private void MappingIfContainsKey()
        {
            var mappingDialog = new MappingList(_mapping[_entityLogicalName]);
            mappingDialog.StartPosition = FormStartPosition.CenterParent;
            mappingDialog.ShowDialog(ParentForm);

            var mapList = mappingDialog.GetMappingList(_entityLogicalName);
            var guidMapList = mappingDialog.GetGuidMappingList();

            if (mapList.Count == 0)
            {
                _mapping.Remove(_entityLogicalName);
                _mapper.Remove(_entityLogicalName);
            }
            else
            {
                _mapping[_entityLogicalName] = mapList;
                _mapper[_entityLogicalName] = guidMapList;
            }

            InitMappings();
        }

        private void OpenMappingForm()
        {
            var mappingDialog = new MappingListLookup(_lookupMaping, _service, _cachedMetadata, _entityLogicalName);
            mappingDialog.StartPosition = FormStartPosition.CenterParent;
            mappingDialog.ShowDialog(ParentForm);
            mappingDialog.RefreshMappingList();
            InitMappings();
            _settings[_organisationid].Mappings.Clear();
        }

        private void InitMappings()
        {
            tsbtMappings.ForeColor = _settings[_organisationid].Mappings.Count == 0 ? Color.Black : Color.Blue;
        }

        private void tsbtFilters_Click(object sender, EventArgs e)
        {
            if (lvEntities.Items.Count != 0 && lvEntities.SelectedItems.Count > 0)
            {
                if (_filterQuery.ContainsKey(_entityLogicalName))
                {
                    FilterIfContainsKey();
                }
                else
                {
                    FilterIfKeyDoesNotExist();
                }
            }
            else
            {
                MessageBox.Show("Entity list is empty");
            }
        }

        private void FilterIfKeyDoesNotExist()
        {
            var filterDialog = new FilterEditor(null);
            filterDialog.StartPosition = FormStartPosition.CenterParent;
            filterDialog.ShowDialog(ParentForm);

            if (!string.IsNullOrWhiteSpace(filterDialog.QueryString))
            {
                _filterQuery[_entityLogicalName] = filterDialog.QueryString;
            }
        }

        private void FilterIfContainsKey()
        {
            var filterDialog = new FilterEditor(_filterQuery[_entityLogicalName]);
            filterDialog.StartPosition = FormStartPosition.CenterParent;
            filterDialog.ShowDialog(ParentForm);

            if (string.IsNullOrWhiteSpace(filterDialog.QueryString))
                _filterQuery.Remove(_entityLogicalName);
            else
                _filterQuery[_entityLogicalName] = filterDialog.QueryString;
        }

        private void chkAllAttributes_CheckedChanged(object sender, EventArgs e)
        {
            lvAttributes.Items.OfType<ListViewItem>().ToList().ForEach(item => item.Checked = chkAllAttributes.Checked);
        }

        private void lvAttributes_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            var columnNumber = e.Column;
            if (columnNumber != 3)
            {
                SetListViewSorting(lvAttributes, e.Column);
            }
        }

        private void lvEntities_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            SetListViewSorting(lvEntities, e.Column);
        }

        private void SetListViewSorting(ListView listview, int column)
        {
            var setting = _settings[_organisationid].Sortcolumns.FirstOrDefault(s => s.Key == listview.Name);
            if (setting == null)
            {
                setting = new Item<string, int>(listview.Name, -1);
                _settings[_organisationid].Sortcolumns.Add(setting);
            }

            if (setting.Value != column)
            {
                setting.Value = column;
                listview.Sorting = SortOrder.Ascending;
            }
            else
            {
                if (listview.Sorting == SortOrder.Ascending)
                    listview.Sorting = SortOrder.Descending;
                else
                    listview.Sorting = SortOrder.Ascending;
            }

            listview.ListViewItemSorter = new ListViewItemComparer(column, listview.Sorting);
        }

        private void chkAllEntities_CheckedChanged(object sender, EventArgs e)
        {
            lvEntities.Items.OfType<ListViewItem>().ToList().ForEach(item => item.Checked = chkAllEntities.Checked);
        }

        private void lvAttributes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var indexNumber = e.Index;
            var logicalName = lvAttributes.Items[indexNumber].SubItems[1].Text;
            if (_entityAttributes.ContainsKey(_entityLogicalName))
            {
                StoreAttriubteIfKeyExists(logicalName, e);
            }
            else
            {
                StoreAttributeIfRequiresKey(logicalName, e);
            }
        }

        private void StoreAttributeIfRequiresKey(string logicalName, ItemCheckEventArgs e)
        {
            HashSet<string> attributeSet = new HashSet<string>();
            if (e.CurrentValue.ToString() == "Checked")
            {
                if (attributeSet.Contains(logicalName))
                {
                    attributeSet.Remove(logicalName);
                }
            }
            else
            {
                attributeSet.Add(logicalName);
            }

            _entityAttributes.Add(_entityLogicalName, attributeSet);
        }

        private void StoreAttriubteIfKeyExists(string logicalName, ItemCheckEventArgs e)
        {
            HashSet<string> attributeSet = _entityAttributes[_entityLogicalName];

            if (e.CurrentValue.ToString() == "Checked")
            {
                if (attributeSet.Contains(logicalName))
                {
                    attributeSet.Remove(logicalName);
                }
            }
            else
            {
                attributeSet.Add(logicalName);
            }
        }

        private void lvEntities_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var indexNumber = e.Index;
            var logicalName = lvEntities.Items[indexNumber].SubItems[1].Text;
            if (e.CurrentValue.ToString() == "Checked")
            {
                if (_checkedEntity.Contains(logicalName))
                {
                    _checkedEntity.Remove(logicalName);
                }
            }
            else
            {
                _checkedEntity.Add(logicalName);
            }
        }

        private void btExportConfigFile_Click(object sender, EventArgs e)
        {
            fdSchemaFile.DefaultExt = "json";

            DialogResult fd = fdSchemaFile.ShowDialog();

            if (fd == DialogResult.OK)
            {
                tbExportConfigFile.Text = fdSchemaFile.FileName;
                _dataMigrationSettings.ExportConfig = CrmExporterConfig.GetConfiguration(tbExportConfigFile.Text);
            }

            if (_dataMigrationSettings.ExportConfig != null)
            {
                tbSchemaFilePath.Text = _dataMigrationSettings.ExportConfig.CrmMigrationToolSchemaPaths[0];
                tbFolderPath.Text = _dataMigrationSettings.ExportConfig.JsonFolderPath;
                batchSize.Value = _dataMigrationSettings.ExportConfig.BatchSize;
                pageSize.Value = _dataMigrationSettings.ExportConfig.PageSize;
                topCount.Value = _dataMigrationSettings.ExportConfig.TopCount;
                cbOnlyActive.Checked = _dataMigrationSettings.ExportConfig.OnlyActiveRecords;
            }
        }

        private void tbSaveSchema_Click(object sender, EventArgs e)
        {
            GetSchemaFilePath();
            CollectCrmEntityFields();
            GenerateXMLFile();
            GenerateXMLFileMessage();
            ResetEntities();
        }

        private void GenerateXMLFileMessage()
        {
            if (_xmlSettings.FailedValidation == false)
            {
                MessageBox.Show(_xmlSettings.SuccessValidationMessage);
            }
            else
            {
                MessageBox.Show(_xmlSettings.FailedValidationMessage);
            }
        }

        private void ResetEntities()
        {
            _xmlSettings.crmEntity = null;
            _crmSchemaConfiguration.Entities = null;
        }

        private void GetSchemaFilePath()
        {
            _xmlSettings.XmlFilePath = tbSchemaPath.Text;
        }

        private void CollectCrmEntityFields()
        {
            if (_checkedEntity.Count > 0)
            {
                var crmEntityList = new List<CrmEntity>();
                foreach (var entityLogicalName in _checkedEntity)
                {
                    CrmEntity crmEntity = new CrmEntity();
                    var sourceList = MetadataHelper.RetrieveEntities(entityLogicalName, _service);
                    StoreCrmEntityData(crmEntity, sourceList, crmEntityList);
                }
                _crmSchemaConfiguration.Entities = crmEntityList.ToArray();
                _xmlSettings.crmEntity = _crmSchemaConfiguration.Entities;
            }
        }

        private void StoreCrmEntityData(CrmEntity crmEntity, EntityMetadata sourceList, List<CrmEntity> crmEntityList)
        {
            crmEntity.Name = sourceList.LogicalName;
            crmEntity.DisplayName = sourceList.DisplayName.UserLocalizedLabel == null ? string.Empty : sourceList.DisplayName.UserLocalizedLabel.Label;
            crmEntity.EntityCode = sourceList.ObjectTypeCode.ToString();
            crmEntity.PrimaryIdField = sourceList.PrimaryIdAttribute;
            crmEntity.PrimaryNameField = sourceList.PrimaryNameAttribute;
            CollectCrmEntityRelationShip(sourceList, crmEntity);
            CollectCrmAttributesFields(sourceList, crmEntity);
            crmEntityList.Add(crmEntity);
        }

        private void CollectCrmEntityRelationShip(EntityMetadata sourceList, CrmEntity crmEntity)
        {
            var manyToManyRelationship = sourceList.ManyToManyRelationships;
            var relationshipList = new List<CrmRelationship>();
            foreach (var relationship in manyToManyRelationship)
            {
                if (_entityRelationships.ContainsKey(sourceList.LogicalName))
                {
                    foreach (var relationshipName in _entityRelationships[sourceList.LogicalName])
                    {
                        if (relationshipName == relationship.IntersectEntityName)
                        {
                            StoreCrmEntityRelationShipData(crmEntity, relationship, relationshipList);
                        }
                    }
                }
            }
            crmEntity.CrmRelationships = relationshipList.ToArray();
        }

        private void StoreCrmEntityRelationShipData(CrmEntity crmEntity, ManyToManyRelationshipMetadata relationship, List<CrmRelationship> relationshipList)
        {
            CrmRelationship crmRelationShip = new CrmRelationship();
            crmRelationShip.RelatedEntityName = relationship.IntersectEntityName;
            crmRelationShip.ManyToMany = true;
            crmRelationShip.IsReflexive = relationship.IsCustomizable.Value;
            crmRelationShip.TargetEntityPrimaryKey = crmEntity.PrimaryIdField == relationship.Entity2IntersectAttribute ? relationship.Entity1IntersectAttribute : relationship.Entity2IntersectAttribute;
            crmRelationShip.TargetEntityName = crmEntity.Name == relationship.Entity2LogicalName ? relationship.Entity1LogicalName : relationship.Entity2LogicalName;
            crmRelationShip.RelationshipName = relationship.IntersectEntityName;
            relationshipList.Add(crmRelationShip);
        }

        private void CollectCrmAttributesFields(EntityMetadata sourceList, CrmEntity crmEntity)
        {
            if (_entityAttributes != null)
            {
                var attributes = sourceList.Attributes.ToArray();

                //.Where(a => (a.IsValidForCreate != null && a.IsValidForCreate.Value) || (a.IsValidForUpdate != null && a.IsValidForUpdate.Value))
                //.Where(a => a.IsValidForRead != null && a.IsValidForRead.Value)
                //.ToArray();
                var primaryAttribute = sourceList.PrimaryIdAttribute;
                if (_entityAttributes.ContainsKey(sourceList.LogicalName))
                {
                    var crmFieldList = new List<CrmField>();
                    foreach (AttributeMetadata attribute in attributes)
                    {
                        StoreAttributeMetadata(attribute, sourceList, primaryAttribute, crmFieldList);
                    }
                    crmEntity.CrmFields = crmFieldList.ToArray();
                }
            }
        }

        private void StoreAttributeMetadata(AttributeMetadata attribute, EntityMetadata sourceList, string primaryAttribute, List<CrmField> crmFieldList)
        {
            CrmField crmField = new CrmField();
            foreach (var attributeLogicalName in _entityAttributes[sourceList.LogicalName])
            {
                if (attribute.LogicalName.Equals(attributeLogicalName))
                {
                    crmField.DisplayName = attribute.DisplayName.UserLocalizedLabel == null ? string.Empty : attribute.DisplayName.UserLocalizedLabel.Label;
                    crmField.FieldName = attribute.LogicalName;
                    _attributeMapping.AttributeMetadataType = attribute.AttributeTypeName.Value.ToString();
                    _attributeMapping.GetMapping();
                    crmField.FieldType = _attributeMapping.AttributeMetadataTypeResult;
                    StoreLookUpAttribute(attribute, crmField);
                    StoreAttributePrimaryKey(primaryAttribute, crmField);
                    crmFieldList.Add(crmField);
                }
            }
        }

        private void StoreAttributePrimaryKey(string primaryAttribute, CrmField crmField)
        {
            if (crmField.FieldName.Equals(primaryAttribute))
            {
                crmField.PrimaryKey = true;
            }
        }

        private void StoreLookUpAttribute(AttributeMetadata attribute, CrmField crmField)
        {
            if (crmField.FieldType.Equals("entityreference"))
            {
                try
                {
                    if ((LookupAttributeMetadata)attribute != null)
                    {
                        var lookUpAttribute = (LookupAttributeMetadata)attribute;
                        if (lookUpAttribute.Targets.Any())
                        {
                            crmField.LookupType = lookUpAttribute.Targets[0];
                        }
                    }
                }
                catch (InvalidCastException exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        private void GenerateXMLFile()
        {
            _xmlSettings.ValidateAll();
            var path = _xmlSettings.XmlFilePath;
            if (_xmlSettings.FailedValidation == false)
            {
                _crmSchemaConfiguration.SaveToFile(path);
            }
        }

        private void btSchemaFolderPath_Click(object sender, EventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "XML Files|*.xml";
            fileDialog.OverwritePrompt = false;

            var result = fileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                tbSchemaPath.Text = fileDialog.FileName.ToString();

                if (File.Exists(tbSchemaPath.Text))
                    tbLoadSchemaFile_Click(this, new EventArgs());
            }
            else if (result == DialogResult.Cancel)
            {
                tbSchemaPath.Text = null;
            }
        }

        private void LoadSchemaFile()
        {
            _deserialization.XmlFolderPath = tbSchemaPath.Text;
            _deserialization.Validate();
            if (_deserialization.FailedValidation == false)
            {
                CrmSchemaConfiguration crmSchema = CrmSchemaConfiguration.ReadFromFile(_deserialization.XmlFolderPath);
                StoreEntityData(crmSchema.Entities);
                ClearAllListViews();
                PopulateEntities();
            }
        }

        private void ClearAllListViews()
        {
            lvEntities.Items.Clear();
            lvAttributes.Items.Clear();
            lvRelationship.Items.Clear();
        }

        private void StoreEntityData(CrmEntity[] crmEntity)
        {
            _entityAttributes.Clear();
            _entityRelationships.Clear();
            foreach (var entities in crmEntity)
            {
                var logicalName = entities.Name;
                HashSet<string> attributeSet = new HashSet<string>();
                HashSet<string> relationShipSet = new HashSet<string>();
                if (entities.CrmFields != null)
                {
                    foreach (var attributes in entities.CrmFields)
                    {
                        attributeSet.Add(attributes.FieldName);
                    }
                }
                if (entities.CrmRelationships != null)
                {
                    foreach (var relationship in entities.CrmRelationships)
                    {
                        relationShipSet.Add(relationship.RelationshipName);
                    }
                }

                _entityAttributes.Add(logicalName, attributeSet);
                _entityRelationships.Add(logicalName, relationShipSet);
            }
        }

        private void chkAllRelationships_CheckedChanged(object sender, EventArgs e)
        {
            lvRelationship.Items.OfType<ListViewItem>().ToList().ForEach(item => item.Checked = chkAllRelationships.Checked);
        }

        private void lvRelationship_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var indexNumber = e.Index;
            var logicalName = lvRelationship.Items[indexNumber].SubItems[1].Text;

            if (_entityRelationships.ContainsKey(_entityLogicalName))
            {
                StoreRelationshipIfKeyExists(logicalName, e);
            }
            else
            {
                StoreRelationshipIfRequiresKey(logicalName, e);
            }
        }

        private void StoreRelationshipIfRequiresKey(string logicalName, ItemCheckEventArgs e)
        {
            HashSet<string> relationshipSet = new HashSet<string>();
            if (e.CurrentValue.ToString() == "Checked")
            {
                if (relationshipSet.Contains(logicalName))
                {
                    relationshipSet.Remove(logicalName);
                }
            }
            else
            {
                relationshipSet.Add(logicalName);
            }
            _entityRelationships.Add(_entityLogicalName, relationshipSet);
        }

        private void StoreRelationshipIfKeyExists(string logicalName, ItemCheckEventArgs e)
        {
            HashSet<string> relationshipSet = _entityRelationships[_entityLogicalName];

            if (e.CurrentValue.ToString() == "Checked")
            {
                if (relationshipSet.Contains(logicalName))
                {
                    relationshipSet.Remove(logicalName);
                }
            }
            else
            {
                relationshipSet.Add(logicalName);
            }
        }

        private void GetJsonFolderPathImport()
        {
            _importSchemaSettings.JsonFilePath = tbImportConfig.Text;
        }

        private void GetJsonFolderPathExport()
        {
            _exportSchemaSettings.JsonFilePath = tbExportConfig.Text;
        }

        private void DeleteFile(string filePath)
        {
            try
            {
                File.Delete(filePath);
            }
            catch (IOException e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void btImportConfigPath_Click(object sender, EventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "JSON Files|*.json";
            fileDialog.OverwritePrompt = false;
            var result = fileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                tbImportConfig.Text = fileDialog.FileName.ToString();

                if (File.Exists(tbImportConfig.Text))
                    tbLoadMappingsFile_Click(this, new EventArgs());
            }
            else if (result == DialogResult.Cancel)
            {
                tbImportConfig.Text = null;
            }
        }

        private void LoadMappingFileLookup()
        {
            _exportSchemaSettings.JsonFilePathLoad = tbExportConfig.Text;
            _exportSchemaSettings.ValidateLoading();
            try
            {
                if (_exportSchemaSettings.FailedValidationLoading == false)
                {
                    _lookupMaping = CrmExporterConfig.GetConfiguration(_exportSchemaSettings.JsonFilePathLoad).LookupMapping;
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Load Correct file");
            }
        }

        private void LoadMappingFileGuid()
        {
            _importSchemaSettings.JsonFilePathLoad = tbImportConfig.Text;
            _importSchemaSettings.ValidateLoading();
            try
            {
                if (_importSchemaSettings.FailedValidationLoading == false)
                {
                    _mapper = CrmImportConfig.GetConfiguration(_importSchemaSettings.JsonFilePathLoad).MigrationConfig.Mappings;
                    DataConversion();
                }
            }
            catch (NullReferenceException exception)
            {
                MessageBox.Show("Load Correct file, error:" + exception.Message);
            }
        }

        private void btExportConfigPath_Click(object sender, EventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "JSON Files|*.json";
            fileDialog.OverwritePrompt = false;
            var result = fileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                tbExportConfig.Text = fileDialog.FileName.ToString();

                if (File.Exists(tbExportConfig.Text))
                {
                    tbLoadFiltersFile_Click(this, new EventArgs());
                    LoadMappingFileLookup();
                }
            }
            else if (result == DialogResult.Cancel)
            {
                tbExportConfig.Text = null;
            }
        }

        private void LoadFilterFile()
        {
            _exportSchemaSettings.JsonFilePathLoad = tbExportConfig.Text;
            _exportSchemaSettings.ValidateLoading();
            try
            {
                if (_exportSchemaSettings.FailedValidationLoading == false && CrmExporterConfig.GetConfiguration(_exportSchemaSettings.JsonFilePathLoad).CrmMigrationToolSchemaFilters.Count > 0)
                {
                    _filterQuery = CrmExporterConfig.GetConfiguration(_exportSchemaSettings.JsonFilePathLoad).CrmMigrationToolSchemaFilters;
                }
            }
            catch (NullReferenceException exception)
            {
                MessageBox.Show("Load Correct file, error:" + exception.Message);
            }
        }

        private void tbLoadSchema_Click(object sender, EventArgs e)
        {
            _deserialization.XmlFolderPath = tbSchemaPath.Text;
            _deserialization.Validate();
            if (_deserialization.FailedValidation != true)
            {
                CrmSchemaConfiguration crmSchema = CrmSchemaConfiguration.ReadFromFile(_deserialization.XmlFolderPath);
                StoreEntityData(crmSchema.Entities);
                ClearAllListViews();
                PopulateEntities();
            }
            else
            {
                MessageBox.Show(_deserialization.FailedValidationMessage);
            }
        }

        private void tbSaveMappings_Click(object sender, EventArgs e)
        {
            GetJsonFolderPathImport();
            GenerateImportConfigFile();
            GenerateImportConfigMessage();
        }

        private void GenerateImportConfigMessage()
        {
            if (_importSchemaSettings.FailedValidation == false)
            {
                MessageBox.Show(_importSchemaSettings.SuccessValidationMessage);
            }
            else
            {
                MessageBox.Show(_importSchemaSettings.FailedValidationMessage);
            }
        }

        private void GenerateImportConfigFile()
        {
            CrmImportConfig migration = new CrmImportConfig() { IgnoreStatuses = true, IgnoreSystemFields = true, SaveBatchSize = 200 };
            if (File.Exists(_importSchemaSettings.JsonFilePath))
            {
                migration = CrmImportConfig.GetConfiguration(_importSchemaSettings.JsonFilePath);
            }

            if (migration.MigrationConfig == null)
            {
                migration.MigrationConfig = new MappingConfiguration();
            }

            if (_mapping != null)
            {
                migration.MigrationConfig.Mappings = _mapper;
                _importSchemaSettings.Mappings = _mapper;
                _importSchemaSettings.ValidateAll();

                if (_importSchemaSettings.FailedValidation == false)
                {
                    migration.JsonFolderPath = new FileInfo(_importSchemaSettings.JsonFilePath).DirectoryName + "\\ExtractedData";
                    if (File.Exists(_importSchemaSettings.JsonFilePath))
                    {
                        File.Delete(_importSchemaSettings.JsonFilePath);
                    }
                    migration.SaveConfiguration(_importSchemaSettings.JsonFilePath);
                }
            }
        }

        private void tbSaveFilters_Click(object sender, EventArgs e)
        {
            GetJsonFolderPathExport();
            GenerateExportConfigFile();
            GenerateExportConfigMessage();
        }

        private void GenerateExportConfigMessage()
        {
            if (_exportSchemaSettings.FailedValidation == true)
            {
                MessageBox.Show(_exportSchemaSettings.FailedValidationMessage);
            }
            else
            {
                MessageBox.Show(_exportSchemaSettings.SuccessValidationMessage);
            }
        }

        private void GenerateExportConfigFile()
        {
            CrmExporterConfig config = new CrmExporterConfig();
            if (File.Exists(_exportSchemaSettings.JsonFilePath))
            {
                config = CrmExporterConfig.GetConfiguration(_exportSchemaSettings.JsonFilePath);
            }
            config.CrmMigrationToolSchemaFilters = _filterQuery;
            _exportSchemaSettings.Filter = _filterQuery;
            _exportSchemaSettings.Validate();
            _exportSchemaSettings.ValidateSuccess();

            if (!string.IsNullOrWhiteSpace(tbSchemaPath.Text))
            {
                config.CrmMigrationToolSchemaPaths = new List<string>() { tbSchemaPath.Text };

                config.JsonFolderPath = new FileInfo(_exportSchemaSettings.JsonFilePath).DirectoryName + "\\ExtractedData";
            }
            if (_lookupMaping.Count > 0)
            {
                config.LookupMapping = _lookupMaping;
            }
            if (_exportSchemaSettings.FailedValidation == false)
            {
                if (File.Exists(_exportSchemaSettings.JsonFilePath))
                {
                    File.Delete(_exportSchemaSettings.JsonFilePath);
                }
                config.SaveConfiguration(_exportSchemaSettings.JsonFilePath);
            }
        }

        private void DataConversion()
        {
            _mapping.Clear();
            foreach (var mappings in _mapper)
            {
                var list = new List<Item<EntityReference, EntityReference>>();

                foreach (var values in mappings.Value)
                {
                    list.Add(new Item<EntityReference, EntityReference>(new EntityReference(mappings.Key, values.Key), new EntityReference(mappings.Key, values.Value)));
                }
                _mapping.Add(mappings.Key, list);
            }
        }

        private void tbLoadMappingsFile_Click(object sender, EventArgs e)
        {
            LoadMappingFileGuid();

            GenerateLoadMappingFileMessage();
        }

        private void GenerateLoadMappingFileMessage()
        {
            if (_importSchemaSettings.FailedValidationLoading == false)
            {
                MessageBox.Show(_importSchemaSettings.SuccessValidationMessageLoading);
            }
            else
            {
                MessageBox.Show(_importSchemaSettings.FailedValidationLoadingMessage);
            }
        }

        private void tbLoadSchemaFile_Click(object sender, EventArgs e)
        {
            LoadSchemaFile();
            GenerateLoadSchemaFileMessage();
        }

        private void GenerateLoadSchemaFileMessage()
        {
            if (_deserialization.FailedValidation == true)
            {
                MessageBox.Show(_deserialization.FailedValidationMessage);
            }
        }

        private void tbLoadFiltersFile_Click(object sender, EventArgs e)
        {
            LoadFilterFile();
            GenerateLoadFilterFileMessage();
        }

        private void GenerateLoadFilterFileMessage()
        {
            if (_exportSchemaSettings.FailedValidationLoading == false && CrmExporterConfig.GetConfiguration(_exportSchemaSettings.JsonFilePathLoad).CrmMigrationToolSchemaFilters.Count > 0)
            {
                MessageBox.Show(_exportSchemaSettings.SuccessValidationMessageLoading);
            }
            else
            {
                MessageBox.Show(_exportSchemaSettings.FailedValidationLoadingMessage);
            }
        }

        private void loadAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadAllFiles();
        }

        private void LoadAllFiles()
        {
            _loadAllSettings.SchemaPath = tbSchemaPath.Text;
            _loadAllSettings.ImportPath = tbImportConfig.Text;
            _loadAllSettings.ExportPath = tbExportConfig.Text;
            _loadAllSettings.Validate();
            if (_loadAllSettings.FailedValidation == false)
            {
                LoadSchemaFile();
                LoadFilterFile();
                LoadMappingFileLookup();
                LoadMappingFileGuid();
            }
            else
            {
                MessageBox.Show(_loadAllSettings.FailedValidationMessage);
            }
        }

        private void saveAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _xmlSettings.XmlFilePath = tbSchemaPath.Text;
            _importSchemaSettings.JsonFilePath = tbImportConfig.Text;
            _exportSchemaSettings.JsonFilePath = tbExportConfig.Text;
            GenerateImportConfigFile();
            GenerateExportConfigFile();
            CollectCrmEntityFields();
            GenerateXMLFile();
            GenerateSaveAllMessage();
            ResetEntities();
        }

        private void GenerateSaveAllMessage()
        {
            StringBuilder failedMessage = new StringBuilder();
            if (_xmlSettings.FailedValidation == true)
            {
                failedMessage.Append(_xmlSettings.FailedValidationMessage);
            }
            if (_exportSchemaSettings.FailedValidation == true)
            {
                failedMessage.Append(_exportSchemaSettings.FailedValidationMessage);
            }
            if (_importSchemaSettings.FailedValidation == true)
            {
                failedMessage.Append(_importSchemaSettings.FailedValidationMessage);
            }
            if (_exportSchemaSettings.FailedValidation == false && _importSchemaSettings.FailedValidation == false && _xmlSettings.FailedValidation == false)
            {
                MessageBox.Show("Successfully saved all files");
            }
            else
            {
                MessageBox.Show(failedMessage.ToString());
            }
        }

        private void gbEnvironments_Enter(object sender, EventArgs e)
        {
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            OpenMappingForm();
        }

        private async void btExportCsv_Click(object sender, EventArgs e)
        {
            if (btExportCsv.Text == "Export Csv")
            {
                if (cbMinJson.Checked)
                    JsonSerializerConfig.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.None;
                else
                    JsonSerializerConfig.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;

                btImport.Enabled = false;
                btExport.Enabled = false;
                btImportCSV.Enabled = false;
                btExportCsv.Text = "STOP";
                _currentTask = StartExport(true);

                try
                {
                    await _currentTask;
                }
                catch (Exception ex)
                {
                    _logger.Error("Export Error:", ex);
                }

                btImport.Enabled = true;
                btExport.Enabled = true;
                btImportCSV.Enabled = true;
                btExportCsv.Text = "Export Csv";
            }
            else
            {
                _tokenSource.Cancel();

                try
                {
                    await _currentTask;
                }
                catch (Exception ex)
                {
                    _logger.Error("Export Error:", ex);
                }
            }
        }

        private void btnSchemaSourceConnectionString_Click(object sender, EventArgs e)
        {
            if (OnRequestConnection != null)
            {
                var args = new RequestConnectionEventArgs { ActionName = "SourceOrganization", Control = this };
                OnRequestConnection(this, args);
            }
        }

        #region RecordsCountTab

        private void btnRCountConnectionString_Click(object sender, EventArgs e)
        {
            ResetDataGridRCounts();
            if (OnRequestConnection != null)
            {
                var args = new RequestConnectionEventArgs { ActionName = "SourceOrganization", Control = this };
                OnRequestConnection(this, args);
            }
        }

        private void btRCountExportConfigFile_Click(object sender, EventArgs e)
        {
            ResetDataGridRCounts();
            fdSchemaFile.DefaultExt = "json";
            fdSchemaFile.Filter = "JSON Files|*.json";
            DialogResult fd = fdSchemaFile.ShowDialog();

            if (fd == DialogResult.OK)
            {
                tbRCountExportConfigFile.Text = fdSchemaFile.FileName;
                _dataMigrationSettings.ExportConfig = CrmExporterConfig.GetConfiguration(tbRCountExportConfigFile.Text);
            }

            if (_dataMigrationSettings.ExportConfig != null &&
                _dataMigrationSettings.ExportConfig.CrmMigrationToolSchemaPaths != null)
            {
                tbRCountSchemaFilePath.Text = _dataMigrationSettings.ExportConfig.CrmMigrationToolSchemaPaths[0];
            }
        }

        private void btRCountSchmaFile_Click(object sender, EventArgs e)
        {
            ResetDataGridRCounts();
            fdSchemaFile.DefaultExt = "xml";
            fdSchemaFile.Filter = "XML Files|*.xml";
            DialogResult fd = fdSchemaFile.ShowDialog();
            if (fd == DialogResult.OK)
            {
                tbRCountSchemaFilePath.Text = fdSchemaFile.FileName;
            }
        }

        private void tsBtnExecuteCount_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbRCountExportConfigFile.Text))
            {
                MessageBox.Show("Select export config file to execute count.");
                return;
            }
            if (string.IsNullOrEmpty(tbRCountSchemaFilePath.Text))
            {
                MessageBox.Show("Select schema file to execute count.");
                return;
            }

            ResetDataGridRCounts();
            ExecuteRecordsCount();
        }

        private void ExecuteRecordsCount()
        {
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Counting...",
                Work = (w, e) =>
                {
                    RecordcounterProcessor recordcounterProcessor = new RecordcounterProcessor();
                    e.Result = recordcounterProcessor.ExecuteRecordsCount(tbRCountExportConfigFile.Text,
                            tbRCountSchemaFilePath.Text,
                            _service, w, dataGridRCounts);
                },
                ProgressChanged = e =>
                {
                    SetWorkingMessage(e.UserState.ToString());
                },
                PostWorkCallBack = e =>
                {
                    // This code is executed in the main thread
                    MessageBox.Show($"Finished");
                    ResetDataGridRCounts();
                    dataGridRCounts.DataSource = e.Result;
                    dataGridRCounts.Columns[0].Width = 250;
                    dataGridRCounts.Columns[1].Width = 250;
                    tsBtnExportResultToCsv.Enabled = true;
                },
                AsyncArgument = null,
                // Progress information panel size
                MessageWidth = 340,
                MessageHeight = 150
            });
        }

        private void ResetDataGridRCounts()
        {
            dataGridRCounts.DataSource = null;
            dataGridRCounts.Refresh();
        }

        private void tsBtnCloseRCount_Click(object sender, EventArgs e)
        {
            CloseTool();
        }

        private void tsBtnExportResultToCsv_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbRCountExportResultFile.Text))
            {
                MessageBox.Show("Select export config file to execute count.");
                return;
            }
            if (!string.IsNullOrEmpty(tbRCountExportResultFile.Text) &&
                !tbRCountExportResultFile.Text.EndsWith(".csv"))
            {
                MessageBox.Show("Select .csv file to export results.");
                return;
            }
            if (dataGridRCounts.DataSource == null)
            {
                MessageBox.Show("No data to export.");
                return;
            }

            ExecuteExportToCsv();
        }

        private void ExecuteExportToCsv()
        {
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Exporting to csv...",
                Work = (w, e) =>
                {
                    List<RecordCountModel> list = (List<RecordCountModel>)dataGridRCounts.DataSource;
                    RecordcounterProcessor recordcounterProcessor = new RecordcounterProcessor();
                    recordcounterProcessor.WriteDataToCSV(list, tbRCountExportResultFile.Text);
                },
                ProgressChanged = e =>
                {
                    SetWorkingMessage(e.UserState.ToString());
                },
                PostWorkCallBack = e =>
                {
                    // This code is executed in the main thread
                    MessageBox.Show($"Finished");
                },
                AsyncArgument = null,
                // Progress information panel size
                MessageWidth = 340,
                MessageHeight = 150
            });
        }

        private void btRCountExportResultsFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "CSV Files|*.csv";
            fileDialog.OverwritePrompt = false;

            var result = fileDialog.ShowDialog();

            if (result == DialogResult.OK)
                tbRCountExportResultFile.Text = fileDialog.FileName.ToString();
            else if (result == DialogResult.Cancel)
                tbRCountExportResultFile.Text = null;
        }

        #endregion RecordsCountTab

        private async void btImportCSV_Click(object sender, EventArgs e)
        {
            if (btImportCSV.Text == "Import Csv")
            {
                btExport.Enabled = false;
                btExportCsv.Enabled = false;
                btImport.Enabled = false;
                btImportCSV.Text = "STOP";
                _currentTask = StartImport(true);

                try
                {
                    await _currentTask;
                }
                catch (Exception ex)
                {
                    _logger.Error("Import Error:", ex);
                }

                btExport.Enabled = true;
                btExportCsv.Enabled = true;
                btImport.Enabled = true;
                btImportCSV.Text = "Import Csv";
            }
            else
            {
                _tokenSource.Cancel();
                try
                {
                    await _currentTask;
                }
                catch (Exception ex)
                {
                    _logger.Error("Export Error:", ex);
                }
            }
        }

        private void loaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

    }
}