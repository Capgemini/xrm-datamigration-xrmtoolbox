using System;
using System.Collections.Generic;
using Capgemini.Xrm.CdsDataMigratorLibrary.Core;
using Microsoft.Xrm.Sdk;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Models
{
    public class CollectionParameters
    {
        public CollectionParameters(Dictionary<string, HashSet<string>> inputEntityAttributes, Dictionary<string, HashSet<string>> inputEntityRelationships,
            Dictionary<string, string> inputFilterQuery, Dictionary<string, Dictionary<string, List<string>>> inputLookupMaping, Dictionary<string, Dictionary<Guid, Guid>> inputMapper, Dictionary<string, List<Item<EntityReference, EntityReference>>> inputMapping
            )
        {
            EntityAttributes = inputEntityAttributes;
            EntityRelationships = inputEntityRelationships;
            FilterQuery = inputFilterQuery;
            LookupMaping = inputLookupMaping;
            Mapper = inputMapper;
            Mapping = inputMapping;
        }

        public Dictionary<string, HashSet<string>> EntityAttributes { get; }

        public Dictionary<string, HashSet<string>> EntityRelationships { get; }

        public Dictionary<string, string> FilterQuery { get; }

        public Dictionary<string, Dictionary<string, List<string>>> LookupMaping { get; }

        public Dictionary<string, Dictionary<Guid, Guid>> Mapper { get; }

        public Dictionary<string, List<Item<EntityReference, EntityReference>>> Mapping { get; }
    }
}