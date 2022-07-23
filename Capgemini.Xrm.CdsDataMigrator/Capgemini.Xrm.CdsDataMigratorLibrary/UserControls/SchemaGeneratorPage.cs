using Capgemini.Xrm.CdsDataMigratorLibrary.Presenters;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Capgemini.Xrm.CdsDataMigratorLibrary.Models;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.UserControls
{
    public partial class SchemaGeneratorPage : UserControl, ISchemaGeneratorView
    {
        private List<EntityMetadata> entityMetadataList;

        public SchemaGeneratorPage()
        {
            InitializeComponent();

            entityListView1.ShowSystemEntitiesChanged += EntityListViewShowSystemEntitiesChanged;
            entityListView1.CurrentSelectedEntityChanged += EntityListViewCurrentSelectedEntityChanged;
        }

        private void EntityListViewCurrentSelectedEntityChanged(object sender, MigratorEventArgs<EntityMetadata> e)
        {
            if (CurrentSelectedEntityChanged != null)
            {
                CurrentSelectedEntityChanged(this, e);
            }
        }

        private void EntityListViewShowSystemEntitiesChanged(object sender, EventArgs e)
        {
            if (ShowSystemEntitiesChanged != null)
            {
                ShowSystemEntitiesChanged(this, e);
            }
        }

        public event EventHandler ShowSystemEntitiesChanged;
        public event EventHandler<MigratorEventArgs<EntityMetadata>> CurrentSelectedEntityChanged;

        public List<EntityMetadata> EntityMetadataList
        {
            get => entityMetadataList;
            set
            {
                entityMetadataList = value;
                entityListView1.Entities = entityMetadataList;
            }
        }
        //public List<AttributeMetadata> EntityAttributes
        //{// get { return listManagerView1.EntityAttributes; } 
        //    set
        //    {

        //        listManagerView1.EntityAttributes = value;
        //    }
        //}
        //public ListView EntityAttributeList
        //{// get { return listManagerView1.EntityAttributes; } 
        //    get => listManagerView1.ListView;
        //    //set
        //    //{

        //    //    listManagerView1.ListView = value;
        //    //}
        //}
       
        public Dictionary<string, HashSet<string>> EntityRelationships { get; set; }
        public bool ShowSystemAttributes { get; set; }

        public ListView EntityAttributeList => listManagerView1.ListView;
        public ListView EntityRelationshipList => listManagerView2.ListView;
        public TreeView EntityList => entityListView1.EntityList;

        public List<EntityMetadata> SelectedEntities => entityListView1.SelectedEntities;

        public event EventHandler RetrieveEntities;
        public event EventHandler<MigratorEventArgs<string>> LoadSchema;
        public event EventHandler<MigratorEventArgs<string>> SaveSchema;

        private void RefreshEntitiesButtonClick(object sender, EventArgs e)
        {
            RetrieveEntities?.Invoke(sender, e);
        }

        private void LoadSchemaButtonClick(object sender, EventArgs e)
        {
            LoadSchema?.Invoke(sender, new MigratorEventArgs<string>(schemaLocationControl1.SchemaFilenamePath));
        }

        private void SaveSchemaButtonClick(object sender, EventArgs e)
        {
            SaveSchema?.Invoke(sender, new MigratorEventArgs<string>(schemaLocationControl1.SchemaFilenamePath));
        }
    }
}
