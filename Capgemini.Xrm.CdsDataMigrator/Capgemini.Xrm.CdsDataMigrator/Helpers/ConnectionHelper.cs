using System;
using System.Globalization;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Exceptions;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Extensions;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Helpers
{
    public static class ConnectionHelper
    {
        public static IOrganizationService GetOrganizationalService(string connectionString)
        {
            connectionString.ThrowArgumentNullExceptionIfNull(nameof(connectionString));

            IOrganizationService orgService;

            if (!connectionString.ToUpper(CultureInfo.InvariantCulture).Contains("REQUIRENEWINSTANCE=TRUE"))
            {
                connectionString = $"RequireNewInstance=True; {connectionString}";
            }

            var serviceClient = new CrmServiceClient(connectionString);

            if (serviceClient.OrganizationWebProxyClient != null)
            {
                var service = serviceClient.OrganizationWebProxyClient;
                service.InnerChannel.OperationTimeout = new TimeSpan(1, 0, 0);
                orgService = service;
            }
            else if (serviceClient.OrganizationServiceProxy != null)
            {
                var service = serviceClient.OrganizationServiceProxy;
                service.Timeout = new TimeSpan(1, 0, 0);
                orgService = service;
            }
            else
            {
                throw new OrganizationalServiceException("Cannot get IOrganizationService");
            }

            return orgService;
        }

        public static IOrganizationService GetOrganizationalService(CrmServiceClient serviceClient)
        {
            serviceClient.ThrowArgumentNullExceptionIfNull(nameof(serviceClient));

            return serviceClient.OrganizationWebProxyClient != null ? (IOrganizationService)serviceClient.OrganizationWebProxyClient : serviceClient.OrganizationServiceProxy;
        }
    }
}