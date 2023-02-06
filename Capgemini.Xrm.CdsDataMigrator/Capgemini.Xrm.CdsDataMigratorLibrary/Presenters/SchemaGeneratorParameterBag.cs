using Microsoft.Xrm.Sdk.Metadata;
using System.Collections.Generic;
using Capgemini.Xrm.CdsDataMigratorLibrary.Core;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Presenters
{
    public class SchemaGeneratorParameterBag
    {
        public SchemaGeneratorParameterBag()
        {
            CachedMetadata = new List<EntityMetadata>();
            AttributeMapping = new AttributeTypeMapping();
            EntityAttributes = new Dictionary<string, HashSet<string>>();
            EntityRelationships = new Dictionary<string, HashSet<string>>();

            CheckedEntity = new HashSet<string>();
            SelectedEntity = new HashSet<string>();
            CheckedRelationship = new HashSet<string>();
        }

        public List<EntityMetadata> CachedMetadata { get; }
        public AttributeTypeMapping AttributeMapping { get; }
        public HashSet<string> CheckedEntity { get; }
        public HashSet<string> SelectedEntity { get; }
        public HashSet<string> CheckedRelationship { get; }
        public Dictionary<string, HashSet<string>> EntityAttributes { get; }
        public Dictionary<string, HashSet<string>> EntityRelationships { get; }
    }
}