using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Capgemini.DataMigration.Core;
using Capgemini.DataMigration.Resiliency.Polly;
using Capgemini.Xrm.DataMigration.Core;
using Capgemini.Xrm.DataMigration.CrmStore.DataStores;
using Capgemini.Xrm.DataMigration.DataStore;
using Capgemini.Xrm.DataMigration.Repositories;
using Capgemini.Xrm.XrmToolBoxPluginBase.Models;
using Microsoft.Xrm.Sdk;

namespace Capgemini.Xrm.XrmToolBoxPluginBase.DataMigration
{
    public abstract class DataMigrationFetchXmlRunner<TMigrationParameters> : DataMigrationRunnerBase
        where TMigrationParameters : MigrationParameters
    {
        protected DataMigrationFetchXmlRunner(ILogger logger) : base(logger)
        {
        }

        public Task ExecuteMigrationAsync(TMigrationParameters migrationParameters, CancellationToken token)
        {
            return Task.Run(() => ExecuteMigration(migrationParameters, token), token);
        }

        protected virtual void ExecuteMigration(TMigrationParameters migrationParameters, CancellationToken token)
        {
            string valMessage = "";
            if (!migrationParameters.Validate(out valMessage))
            {
                Logger.Error($"Validation error: {valMessage}");
                return;
            }

            var processors = GetProcessors(migrationParameters);

            if (processors == null)
            {
                Logger.Warning("Data migration has not started, no processors added");
                return;
            }

            var timer = new Stopwatch();
            timer.Start();

            var orgService = CreateOrganisationService(migrationParameters.ConnectionString);
            var fetchXmlQueries = GenerateFetchXml(orgService, migrationParameters);


            var entityRepo = new EntityRepository(orgService, new ServiceRetryExecutor());
            var storeReader = new DataCrmStoreReader(Logger, entityRepo, 5000, 50000, 10000000, false, fetchXmlQueries);

            var entityRepos = new List<IEntityRepository>
                     {
                            new EntityRepository(CreateOrganisationService(migrationParameters.ConnectionString), new ServiceRetryExecutor()),
                            new EntityRepository(CreateOrganisationService(migrationParameters.ConnectionString), new ServiceRetryExecutor()),
                            new EntityRepository(CreateOrganisationService(migrationParameters.ConnectionString), new ServiceRetryExecutor())
                     };

            var storeWriter = new DataCrmStoreWriterMultiThreaded(Logger, entityRepos, 800, null);

            var engine = new GenericCrmDataMigrator(Logger, storeReader, storeWriter, token);

            processors.ForEach(p =>
            {
                engine.AddProcessor(p);
                Logger.Info($"Added processor {nameof(p)}");
            });

            engine.MigrateData();

            timer.Stop();
            Logger.Info($"Data processing completed in {timer.Elapsed.Days}days {timer.Elapsed.Hours}hrs {timer.Elapsed.Minutes}mins {timer.Elapsed.Seconds}secs!");

        }

        protected abstract List<IEntityProcessor<Entity, EntityWrapper>> GetProcessors(TMigrationParameters migrationParameters);

        protected abstract List<string> GenerateFetchXml(IOrganizationService service, TMigrationParameters migrationParameters);

    }
}