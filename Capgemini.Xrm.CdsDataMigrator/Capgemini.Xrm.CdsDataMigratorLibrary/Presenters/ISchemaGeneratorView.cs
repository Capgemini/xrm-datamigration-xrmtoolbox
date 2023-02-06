using Capgemini.Xrm.CdsDataMigratorLibrary.Models;
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
        ListView EntityAttributeList { get; }
        ListView EntityRelationshipList { get; }
        TreeView EntityList { get; }
        List<EntityMetadata> SelectedEntities { get; }
        Cursor Cursor { get; set; }
        string CurrentConnection { get; set; }

        event EventHandler RetrieveEntities;
        event EventHandler ShowSystemEntitiesChanged;
        event EventHandler<MigratorEventArgs<string>> LoadSchema;
        event EventHandler<MigratorEventArgs<string>> SaveSchema;
        event EventHandler<MigratorEventArgs<EntityMetadata>> CurrentSelectedEntityChanged;
        event EventHandler<MigratorEventArgs<int>> SortAttributesList;
        event EventHandler<MigratorEventArgs<ItemCheckEventArgs>> AttributeSelected;
        event EventHandler<MigratorEventArgs<int>> SortRelationshipList;
        event EventHandler<MigratorEventArgs<ItemCheckEventArgs>> RelationshipSelected;
        event EventHandler<MigratorEventArgs<TreeNode>> EntitySelected;

        void ShowInformationPanel(string message, int width = 340, int height = 150);
        void CloseInformationPanel();
    }
}
