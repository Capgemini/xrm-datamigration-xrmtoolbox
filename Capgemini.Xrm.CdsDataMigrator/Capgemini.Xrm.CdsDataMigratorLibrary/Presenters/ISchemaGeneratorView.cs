using Capgemini.Xrm.CdsDataMigratorLibrary.Enums;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Presenters
{
    public interface ISchemaGeneratorView
    {
        bool ShowSystemAttributes { get; set; }
        List<EntityMetadata> EntityMetadataList { get; set; }
        Dictionary<string, HashSet<string>> EntityAttributes { get; set; }
        Dictionary<string, HashSet<string>> EntityRelationships { get; set; }

        event EventHandler RetrieveEntities;
    }
}
