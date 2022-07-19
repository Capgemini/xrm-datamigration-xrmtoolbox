using Capgemini.Xrm.CdsDataMigratorLibrary.Enums;
using Capgemini.Xrm.CdsDataMigratorLibrary.UserControls;
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
        List<AttributeMetadata> EntityAttributes {   set; }
        Dictionary<string, HashSet<string>> EntityRelationships { get; set; }

        event EventHandler RetrieveEntities;
        event EventHandler ShowSystemEntitiesChanged;
        event EventHandler<MigratorEventArgs<EntityMetadata>> CurrentSelectedEntityChanged;
    }
}
