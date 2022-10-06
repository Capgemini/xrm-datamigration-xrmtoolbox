using Microsoft.Xrm.Sdk;
using Capgemini.Xrm.CdsDataMigratorLibrary.Exceptions;
using Capgemini.Xrm.CdsDataMigratorLibrary.Services;
using Microsoft.Xrm.Sdk.Metadata;
using System.Collections.Generic;
using System.Linq;
using Capgemini.Xrm.CdsDataMigratorLibrary.Extensions;
using System.Windows.Forms;
using Capgemini.Xrm.CdsDataMigratorLibrary.Forms;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Models
{
    public class ServiceParameters
    {
        public ServiceParameters(IOrganizationService organizationService, IMetadataService metadataService, INotificationService notificationService, IExceptionService exceptionService)
        {
            OrganizationService = organizationService;
            MetadataService = metadataService;
            NotificationService = notificationService;
            ExceptionService = exceptionService;
        }

        public IOrganizationService OrganizationService { get; }

        public IMetadataService MetadataService { get; }

        public INotificationService NotificationService { get; }

        public IExceptionService ExceptionService { get; }

        public List<TreeNode> RetrieveSourceEntitiesList(bool showSystemAttributes, List<EntityMetadata> inputCachedMetadata, Dictionary<string, HashSet<string>> inputEntityAttributes)
        {
            var sourceList = MetadataService.RetrieveEntities(OrganizationService);

            if (!showSystemAttributes)
            {
                sourceList = sourceList.Where(p => !p.IsLogicalEntity.Value && !p.IsIntersect.Value).ToList();
            }

            if (sourceList != null)
            {
                inputCachedMetadata.Clear();
                inputCachedMetadata.AddRange(sourceList.OrderBy(p => p.IsLogicalEntity.Value).ThenBy(p => p.IsIntersect.Value).ThenByDescending(p => p.IsCustomEntity.Value).ThenBy(p => p.LogicalName).ToList());
            }

            var sourceEntitiesList = new List<TreeNode>();

            foreach (EntityMetadata entity in inputCachedMetadata)
            {
                var name = entity.DisplayName.UserLocalizedLabel == null ? string.Empty : entity.DisplayName.UserLocalizedLabel.Label;

                var item = new TreeNode($"{name} ({entity.LogicalName})")
                {
                    Tag = entity
                };

                item.IsInvalidForCustomization(entity);
                item.Checked |= inputEntityAttributes.ContainsKey(entity.LogicalName);

                sourceEntitiesList.Add(item);
            }

            return sourceEntitiesList;
        }

        public List<ListViewItem> RetrieveSourceEntitiesListToBeDeleted(bool showSystemAttributes, List<EntityMetadata> inputCachedMetadata, Dictionary<string, HashSet<string>> inputEntityAttributes)
        {
            var sourceList = MetadataService.RetrieveEntities(OrganizationService);

            if (!showSystemAttributes)
            {
                sourceList = sourceList.Where(p => !p.IsLogicalEntity.Value && !p.IsIntersect.Value).ToList();
            }

            if (sourceList != null)
            {
                inputCachedMetadata.Clear();
                inputCachedMetadata.AddRange(sourceList.OrderBy(p => p.IsLogicalEntity.Value).ThenBy(p => p.IsIntersect.Value).ThenByDescending(p => p.IsCustomEntity.Value).ThenBy(p => p.LogicalName).ToList());
            }

            var sourceEntitiesList = new List<ListViewItem>();

            foreach (EntityMetadata entity in inputCachedMetadata)
            {
                var name = entity.DisplayName.UserLocalizedLabel == null ? string.Empty : entity.DisplayName.UserLocalizedLabel.Label;
                var item = new ListViewItem(name)
                {
                    Tag = entity
                };
                item.SubItems.Add(entity.LogicalName);
                item.IsInvalidForCustomization(entity);
                item.Checked |= inputEntityAttributes.ContainsKey(entity.LogicalName);

                sourceEntitiesList.Add(item);
            }

            return sourceEntitiesList;
        }

        public List<AttributeMetadata> GetAttributeList(string entityLogicalName, bool showSystemAttributes)
        {
            var entitymeta = MetadataService.RetrieveEntities(entityLogicalName, OrganizationService, ExceptionService);

            var attributes = entitymeta.FilterAttributes(showSystemAttributes);

            if (attributes != null)
            {
                attributes = attributes.OrderByDescending(p => p.IsPrimaryId)
                                       .ThenByDescending(p => p.IsPrimaryName)
                                       .ThenByDescending(p => p.IsCustomAttribute != null && p.IsCustomAttribute.Value)
                                       .ThenBy(p => p.IsLogical != null && p.IsLogical.Value)
                                       .ThenBy(p => p.LogicalName).ToList();
            }

            return attributes;
        }

        public List<ListViewItem> PopulateRelationshipAction(string inputEntityLogicalName, Dictionary<string, HashSet<string>> inputEntityRelationships)
        {
            var entityMetaData = MetadataService.RetrieveEntities(inputEntityLogicalName, OrganizationService, ExceptionService);
            var sourceAttributesList = new List<ListViewItem>();
            if (entityMetaData != null && entityMetaData.ManyToManyRelationships != null && entityMetaData.ManyToManyRelationships.Any())
            {
                foreach (var relationship in entityMetaData.ManyToManyRelationships)
                {
                    var item = new ListViewItem(relationship.IntersectEntityName);
                    AddRelationship(relationship, item, sourceAttributesList);
                    item.UpdateAttributeMetadataCheckBoxes(relationship.IntersectEntityName, inputEntityRelationships, inputEntityLogicalName);
                }
            }

            return sourceAttributesList;
        }

        private static void AddRelationship(ManyToManyRelationshipMetadata relationship, ListViewItem item, List<ListViewItem> sourceAttributesList)
        {
            item.SubItems.Add(relationship.IntersectEntityName);
            item.SubItems.Add(relationship.Entity2LogicalName);
            item.SubItems.Add(relationship.Entity2IntersectAttribute);
            sourceAttributesList.Add(item);
        }
    }
}