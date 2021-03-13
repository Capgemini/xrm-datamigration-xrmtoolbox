using Capgemini.DataMigration.Resiliency;
using Capgemini.DataMigration.Resiliency.Polly;
using Capgemini.Xrm.DataMigration.Core;
using Capgemini.Xrm.DataMigration.Repositories;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capgemini.Xrm.DataMigration.XrmToolBox.Services
{
    public class EntityRepositoryService : IEntityRepositoryService
    {
        public EntityRepositoryService(CrmServiceClient crmServiceClient)
        {
            CrmServiceClient = crmServiceClient;
        }

        public CrmServiceClient CrmServiceClient { get; set; }

        public IEntityRepository InstantiateEntityRepository(bool useCloneConnection)
        {
            if (useCloneConnection)
            {
                return new EntityRepository(CrmServiceClient.Clone(), new ServiceRetryExecutor());
            }
            else
            {
                return new EntityRepository(CrmServiceClient, new ServiceRetryExecutor());
            }
        }
    }
}