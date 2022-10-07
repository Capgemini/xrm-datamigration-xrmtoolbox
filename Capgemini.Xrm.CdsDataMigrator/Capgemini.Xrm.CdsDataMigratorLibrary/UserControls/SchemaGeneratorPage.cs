using Capgemini.Xrm.CdsDataMigratorLibrary.Presenters;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Capgemini.Xrm.CdsDataMigratorLibrary.Models;
using Microsoft.Win32;
using XrmToolBox.Extensibility;
using System.Diagnostics.CodeAnalysis;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.UserControls
{
    // TODO: could this be tested in part like the other pages? 
    [ExcludeFromCodeCoverage]
    public partial class SchemaGeneratorPage : UserControl, ISchemaGeneratorView
    {
        private List<EntityMetadata> entityMetadataList;
        private Panel informationPanel;

        public SchemaGeneratorPage()
        {
            InitializeComponent();

            entityListView1.ShowSystemEntitiesChanged += EntityListViewShowSystemEntitiesChanged;
            entityListView1.CurrentSelectedEntityChanged += EntityListViewCurrentSelectedEntityChanged;
            entityListView1.EntitySelected += HandleEntityListViewEntitySelected;
            lmvAttributes.ListViewColumnClick += AttributesListViewColumnClick;
            lmvAttributes.ListViewItemCheck += AttributesListViewItemCheck;
            lmvRelationships.ListViewColumnClick += RelationshipListViewColumnClick;
            lmvRelationships.ListViewItemCheck += RelationshipListViewItemCheck;
        }

        private void HandleEntityListViewEntitySelected(object sender, MigratorEventArgs<TreeNode> e)
        {
            EntitySelected?.Invoke(this, e);
        }

        private void EntityListViewCurrentSelectedEntityChanged(object sender, MigratorEventArgs<EntityMetadata> e)
        {
            CurrentSelectedEntityChanged?.Invoke(this, e);
        }

        private void EntityListViewShowSystemEntitiesChanged(object sender, EventArgs e)
        {
            ShowSystemEntitiesChanged?.Invoke(this, e);
        }

        private void RelationshipListViewColumnClick(object sender, MigratorEventArgs<int> e)
        {
            SortAttributesList?.Invoke(this, e);
        }

        private void AttributesListViewColumnClick(object sender, MigratorEventArgs<int> e)
        {
            SortRelationshipList?.Invoke(this, e);
        }

        private void AttributesListViewItemCheck(object sender, MigratorEventArgs<ItemCheckEventArgs> e)
        {
            AttributeSelected?.Invoke(this, e);
        }

        private void RelationshipListViewItemCheck(object sender, MigratorEventArgs<ItemCheckEventArgs> e)
        {
            RelationshipSelected?.Invoke(this, e);
        }

        public event EventHandler ShowSystemEntitiesChanged;

        public event EventHandler<MigratorEventArgs<EntityMetadata>> CurrentSelectedEntityChanged;

        public event EventHandler<MigratorEventArgs<int>> SortAttributesList;

        public event EventHandler<MigratorEventArgs<ItemCheckEventArgs>> AttributeSelected;

        public event EventHandler<MigratorEventArgs<int>> SortRelationshipList;

        public event EventHandler<MigratorEventArgs<ItemCheckEventArgs>> RelationshipSelected;

        public event EventHandler RetrieveEntities;

        public event EventHandler<MigratorEventArgs<string>> LoadSchema;

        public event EventHandler<MigratorEventArgs<string>> SaveSchema;

        public event EventHandler<MigratorEventArgs<TreeNode>> EntitySelected;

        public List<EntityMetadata> EntityMetadataList
        {
            get => entityMetadataList;
            set
            {
                entityMetadataList = value;
                entityListView1.Entities = entityMetadataList;
            }
        }

        public Dictionary<string, HashSet<string>> EntityRelationships { get; set; }

        public bool ShowSystemAttributes { get; set; }

        public ListView EntityAttributeList => lmvAttributes.ListView;

        public ListView EntityRelationshipList => lmvRelationships.ListView;

        public TreeView EntityList => entityListView1.EntityList;

        public List<EntityMetadata> SelectedEntities => entityListView1.SelectedEntities;

        public string CurrentConnection { get => tsbtCurrentConnection.Text; set => tsbtCurrentConnection.Text = value; }

        public void ShowInformationPanel(string message, int width = 340, int height = 150)
        {
            informationPanel = InformationPanel.GetInformationPanel(this, message, width, height);
        }

        public void CloseInformationPanel()
        {
            informationPanel?.Dispose();
        }

        private void RefreshEntitiesButtonClick(object sender, EventArgs e)
        {
            RetrieveEntities?.Invoke(sender, e);
        }

        private void LoadSchemaButtonClick(object sender, EventArgs e)
        {
            using (var fileDialog = new System.Windows.Forms.OpenFileDialog())
            {
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    var file = fileDialog.FileName;
                    LoadSchema?.Invoke(sender, new MigratorEventArgs<string>(file));
                }
            }
        }

        private void SaveSchemaButtonClick(object sender, EventArgs e)
        {
            using (var fileDialog = new System.Windows.Forms.SaveFileDialog())
            {
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    var file = fileDialog.FileName;
                    SaveSchema?.Invoke(sender, new MigratorEventArgs<string>(file));
                }
            }
        }
    }
}