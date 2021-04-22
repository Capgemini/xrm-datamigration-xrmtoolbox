using System;
using System.Collections.Generic;
using System.Linq;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Core
{
    public class Settings
    {
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