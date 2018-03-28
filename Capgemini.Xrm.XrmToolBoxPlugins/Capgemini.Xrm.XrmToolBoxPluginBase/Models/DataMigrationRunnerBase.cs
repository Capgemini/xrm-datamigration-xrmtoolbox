using System;
using Capgemini.DataMigration.Core;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;

namespace Capgemini.Xrm.XrmToolBoxPluginBase.Models
{
    public abstract class DataMigrationRunnerBase
    {
        protected readonly ILogger Logger;

        protected DataMigrationRunnerBase(ILogger logger)
        {
            Logger = logger;
        }

        protected IOrganizationService CreateOrganisationService(string connectionString)
        {
            IOrganizationService orgService;

            if (!connectionString.ToUpper().Contains("REQUIRENEWINSTANCE=TRUE"))
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
                throw new Exception("Cannot get IOrganizationService");
            }

            return orgService;
        }
        
    }
}