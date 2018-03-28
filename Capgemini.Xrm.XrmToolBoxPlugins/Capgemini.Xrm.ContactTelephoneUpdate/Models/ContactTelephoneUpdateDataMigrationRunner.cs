using System.Collections.Generic;
using Capgemini.Xrm.DataMigration.Core;
using System.Text;
using Capgemini.DataMigration.Core;
using Capgemini.Xrm.DataMigration.DataStore;
using Capgemini.Xrm.XrmToolBoxPluginBase.DataMigration;
using Microsoft.Xrm.Sdk;

namespace Capgemini.Xrm.ContactTelephoneUpdate.Models
{
    public class ContactTelephoneUpdateDataMigrationRunner : DataMigrationFetchXmlRunner<MigrationParameters>
    {
        public ContactTelephoneUpdateDataMigrationRunner(ILogger logger) : base(logger)
        {
        }

        protected override List<IEntityProcessor<Entity,EntityWrapper>> GetProcessors(MigrationParameters migrationParameters)
        { 
            var processors = new List<IEntityProcessor<Entity, EntityWrapper>>
            {
                new ContactTelephoneUpdateProcessor(Logger) 
            };
            return processors;
        }
        
        protected override List<string> GenerateFetchXml(IOrganizationService service, MigrationParameters migrationParameters)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("<fetch>");
            stringBuilder.Append("  <entity name='contact'>");
            stringBuilder.Append("    <attribute name='telephone1' />");
            stringBuilder.Append("    <attribute name='mobilephone' />");
            stringBuilder.Append("    <attribute name='nhs_personadonorbloodactivitystatus' />");
            stringBuilder.Append("    <filter type='and'>");
            stringBuilder.Append("      <condition attribute='nhs_personadonorbloodactivitystatus' operator='eq' value='1' />");
            stringBuilder.Append("    </filter>");
            stringBuilder.Append("  </entity>");
            stringBuilder.Append("</fetch>");

            return new List<string> { stringBuilder.ToString() };
        }

    }
}