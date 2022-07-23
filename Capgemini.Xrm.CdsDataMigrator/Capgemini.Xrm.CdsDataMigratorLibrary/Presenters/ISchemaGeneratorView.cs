using Capgemini.Xrm.CdsDataMigratorLibrary.Enums;
using Capgemini.Xrm.CdsDataMigratorLibrary.Models;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Presenters
{
    public interface ISchemaGeneratorView
    {
        bool ShowSystemAttributes { get; set; }
        List<EntityMetadata> EntityMetadataList { get; set; }
        //List<AttributeMetadata> EntityAttributes {   set; }
        //Dictionary<string, HashSet<string>> EntityRelationships { get; set; }
        ListView EntityAttributeList { get; }
        ListView EntityRelationshipList { get; }
        TreeView EntityList { get; }
        List<EntityMetadata> SelectedEntities { get; }
        Cursor Cursor { get; set; }

        event EventHandler RetrieveEntities;
        event EventHandler ShowSystemEntitiesChanged;
        event EventHandler<MigratorEventArgs<string>> LoadSchema;
        event EventHandler<MigratorEventArgs<string>> SaveSchema;
        event EventHandler<MigratorEventArgs<EntityMetadata>> CurrentSelectedEntityChanged;
    }
}
