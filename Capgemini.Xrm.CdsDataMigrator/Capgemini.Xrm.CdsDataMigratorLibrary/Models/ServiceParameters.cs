using Microsoft.Xrm.Sdk;
using Capgemini.Xrm.CdsDataMigratorLibrary.Exceptions;
using Capgemini.Xrm.CdsDataMigratorLibrary.Services;

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
    }
}