using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyXrmToolBoxPlugin3
{
    /// <summary>
    /// This class can help you to store settings for your plugin
    /// </summary>
    /// <remarks>
    /// This class must be XML serializable
    /// </remarks>
    public class Settings
    {
        public string LastUsedOrganizationWebappUrl { get; set; }

        public List<KeyValuePair<Guid, Organisations>> Organisations { get; } = new List<KeyValuePair<Guid, Organisations>>();

        public Organisations this[string organisationid]
        {
            get
            {
                var orgId = Guid.Parse(organisationid);
                if (!Organisations.Any(o => o.Key == orgId))
                {
                    Organisations.Add(new KeyValuePair<Guid, Organisations>(orgId, new Organisations()));
                }

                return Organisations.Where(o => o.Key == orgId).Select(o => o.Value).FirstOrDefault();
            }
        }
    }
}