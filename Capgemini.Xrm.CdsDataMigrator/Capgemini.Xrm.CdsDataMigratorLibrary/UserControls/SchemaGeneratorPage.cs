using Capgemini.Xrm.CdsDataMigratorLibrary.Presenters;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.UserControls
{
    public partial class SchemaGeneratorPage : UserControl, ISchemaGeneratorView
    {
        private List<EntityMetadata> entityMetadataList;

        public SchemaGeneratorPage()
        {
            InitializeComponent();

            entityListView1.ShowSystemEntitiesChanged += EntityListView1_ShowSystemEntitiesChanged;
        }

        private void EntityListView1_ShowSystemEntitiesChanged(object sender, EventArgs e)
        {
            if (ShowSystemEntitiesChanged != null)
            {
                ShowSystemEntitiesChanged(this, e);
            }
        }

        public event EventHandler ShowSystemEntitiesChanged;

        public List<EntityMetadata> EntityMetadataList
        {
            get => entityMetadataList;
            set
            {
                entityMetadataList = value;
                entityListView1.Entities = entityMetadataList;
            }
        }
        public Dictionary<string, HashSet<string>> EntityAttributes { get; set; }
        public Dictionary<string, HashSet<string>> EntityRelationships { get; set; }
        public bool ShowSystemAttributes { get; set; }

        public event EventHandler RetrieveEntities;

        private void RaiseRefreshEntitiesEvent(object sender, EventArgs e)
        {
            RetrieveEntities?.Invoke(sender, e);
        }
    }
}
