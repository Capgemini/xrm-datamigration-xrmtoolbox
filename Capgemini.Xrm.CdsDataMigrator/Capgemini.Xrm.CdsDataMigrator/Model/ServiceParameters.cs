using Capgemini.Xrm.DataMigration.XrmToolBox.Core;
using Capgemini.Xrm.DataMigration.XrmToolBox.Services;
using Capgemini.Xrm.CdsDataMigrator.Services;
using Microsoft.Xrm.Sdk;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Model
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
    }
}