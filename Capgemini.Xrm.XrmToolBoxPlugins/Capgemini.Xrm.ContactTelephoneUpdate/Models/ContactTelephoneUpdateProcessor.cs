using System;
using System.Collections.Generic;
using Capgemini.DataMigration.Core;
using Capgemini.Xrm.DataMigration.Core;
using Capgemini.Xrm.DataMigration.DataStore;
using Microsoft.Xrm.Sdk;

namespace Capgemini.Xrm.ContactTelephoneUpdate.Models
{
    public class ContactTelephoneUpdateProcessor : IEntityProcessor<Entity, EntityWrapper>
    {
        private readonly List<string> _mobilePhones = new List<string> { "07700 900000", "07700 900001" };
        private readonly Random _random = new Random();
        private int _counter = 1;
        private readonly ILogger _logger;

        public ContactTelephoneUpdateProcessor(ILogger logger)
        {
            _logger = logger;
        }

        public int MinRequiredPassNumber => 1;

        public void ImportCompleted()
        {
        }

        public void ImportStarted()
        {
        }

        public void ProcessEntity(EntityWrapper entity, int passNumber, int maxPassNumber)
        {
            if (entity.LogicalName == "contact")
            {
                entity.OriginalEntity["Telephone1"] = $"01632 {DateTime.UtcNow.Second:D6}";
                entity.OperationType = OperationTypes.Update;
                _logger.Verbose($"Original Telephone1 value:{entity.OriginalEntity["Telephone1"]} new  Telephone1 value:{entity.OriginalEntity["Telephone1"]}");
            }
            else
            {
                entity.OperationType = OperationTypes.Ignore;
            }
        }
    }

}
