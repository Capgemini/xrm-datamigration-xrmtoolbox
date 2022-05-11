using Capgemini.Xrm.CdsDataMigratorLibrary.Core;
using Capgemini.Xrm.CdsDataMigratorLibrary.Forms;
using Capgemini.Xrm.CdsDataMigratorLibrary.Models;
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
    public class ListController : ControllerBase
    {
        public void SetListViewSorting(ListView listview, int column, string inputOrganisationId, Core.Settings settings)
        {
            var setting = settings[inputOrganisationId].Sortcolumns.FirstOrDefault(s => s.Key == listview.Name);
            if (setting == null)
            {
                setting = new Item<string, int>(listview.Name, -1);
                settings[inputOrganisationId].Sortcolumns.Add(setting);
            }

            if (setting.Value != column)
            {
                setting.Value = column;
                listview.Sorting = SortOrder.Ascending;
            }
            else
            {
                if (listview.Sorting == SortOrder.Ascending)
                {
                    listview.Sorting = SortOrder.Descending;
                }
                else
                {
                    listview.Sorting = SortOrder.Ascending;
                }
            }

            listview.ListViewItemSorter = new ListViewItemComparer(column, listview.Sorting);
        }

        public void OpenMappingForm(ServiceParameters serviceParameters, IWin32Window owner, List<EntityMetadata> inputCachedMetadata, Dictionary<string, Dictionary<string, List<string>>> inputLookupMaping, string inputEntityLogicalName)
        {
            using (var mappingDialog = new MappingListLookup(inputLookupMaping, serviceParameters.OrganizationService, inputCachedMetadata, inputEntityLogicalName, serviceParameters.MetadataService, serviceParameters.ExceptionService)
            {
                StartPosition = FormStartPosition.CenterParent
            })
            {
                if (owner != null)
                {
                    mappingDialog.ShowDialog(owner);
                }
                mappingDialog.RefreshMappingList();
            }
        }

        public void OnPopulateCompletedAction(RunWorkerCompletedEventArgs e, INotificationService notificationService, IWin32Window owner, ListView listView, bool showSystemAttributes)
        {   
            if (e.Error != null)
            {
                notificationService.DisplayErrorFeedback(owner, $"An error occured: {e.Error.Message}");
            }
            else
            {
                var items = e.Result as List<ListViewItem>;
                if (showSystemAttributes)
                {
                    items = items.Where(x => ((AttributeMetadata)x.Tag)?.IsCustomAttribute == true).ToList();
                }
                if (items != null)
                {
                    listView.Items.AddRange(items.ToArray());
                }
            }
        }

        public void HandleMappingControlItemClick(INotificationService notificationService, string inputEntityLogicalName, bool listViewItemIsSelected, Dictionary<string, List<Item<EntityReference, EntityReference>>> inputMapping, Dictionary<string, Dictionary<Guid, Guid>> inputMapper, Form parentForm)
        {
            if (listViewItemIsSelected)
            {
                if (!string.IsNullOrEmpty(inputEntityLogicalName))
                {
                    if (inputMapping.ContainsKey(inputEntityLogicalName))
                    {
                        MappingIfContainsKey(inputEntityLogicalName, inputMapping, inputMapper, parentForm);
                    }
                    else
                    {
                        MappingIfKeyDoesNotExist(inputEntityLogicalName, inputMapping, inputMapper, parentForm);
                    }
                }
            }
            else
            {
                notificationService.DisplayFeedback("Entity is not selected");
            }
        }

        public void MappingIfKeyDoesNotExist(string inputEntityLogicalName, Dictionary<string, List<Item<EntityReference, EntityReference>>> inputMapping, Dictionary<string, Dictionary<Guid, Guid>> inputMapper, Form parentForm)
        {
            var mappingReference = new List<Item<EntityReference, EntityReference>>();
            using (var mappingDialog = new MappingList(mappingReference)
            {
                StartPosition = FormStartPosition.CenterParent
            })
            {
                if (parentForm != null)
                {
                    mappingDialog.ShowDialog(parentForm);
                }

                var mapList = mappingDialog.GetMappingList(inputEntityLogicalName);
                var guidMapList = mappingDialog.GetGuidMappingList();

                if (mapList.Count > 0)
                {
                    inputMapping.Add(inputEntityLogicalName, mapList);
                    inputMapper.Add(inputEntityLogicalName, guidMapList);
                }
            }
        }

        public void MappingIfContainsKey(string inputEntityLogicalName, Dictionary<string, List<Item<EntityReference, EntityReference>>> inputMapping, Dictionary<string, Dictionary<Guid, Guid>> inputMapper, Form parentForm)
        {
            using (var mappingDialog = new MappingList(inputMapping[inputEntityLogicalName])
            {
                StartPosition = FormStartPosition.CenterParent
            })
            {
                if (parentForm != null)
                {
                    mappingDialog.ShowDialog(parentForm);
                }

                var mapList = mappingDialog.GetMappingList(inputEntityLogicalName);
                var guidMapList = mappingDialog.GetGuidMappingList();

                if (mapList.Count == 0)
                {
                    inputMapping.Remove(inputEntityLogicalName);
                    inputMapper.Remove(inputEntityLogicalName);
                }
                else
                {
                    inputMapping[inputEntityLogicalName] = mapList;
                    inputMapper[inputEntityLogicalName] = guidMapList;
                }
            }
        }

        public void ProcessFilterQuery(INotificationService notificationService, Form parentForm, string inputEntityLogicalName, bool listViewItemIsSelected, Dictionary<string, string> inputFilterQuery, FilterEditor filterDialog)
        {
            if (listViewItemIsSelected)
            {
                if (parentForm != null)
                {
                    filterDialog.ShowDialog(parentForm);
                }

                if (inputFilterQuery.ContainsKey(inputEntityLogicalName))
                {
                    if (string.IsNullOrWhiteSpace(filterDialog.QueryString))
                    {
                        inputFilterQuery.Remove(inputEntityLogicalName);
                    }
                    else
                    {
                        inputFilterQuery[inputEntityLogicalName] = filterDialog.QueryString;
                    }
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(filterDialog.QueryString))
                    {
                        inputFilterQuery[inputEntityLogicalName] = filterDialog.QueryString;
                    }
                }
            }
            else
            {
                notificationService.DisplayFeedback("Entity list is empty");
            }
        }
    }
}