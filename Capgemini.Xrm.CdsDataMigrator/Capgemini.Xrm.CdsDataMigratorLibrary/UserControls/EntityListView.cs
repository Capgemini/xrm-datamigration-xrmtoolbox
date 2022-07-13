using Microsoft.Xrm.Sdk.Metadata;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.UserControls
{
    public partial class EntityListView : UserControl
    {
        private List<EntityMetadata> entities;

        public EntityListView()
        {
            InitializeComponent();
        }

        public List<EntityMetadata> SelectedEntities
        {
            get
            {
                var list = new List<EntityMetadata>();

                if (treeViewEntities?.Nodes !=null)
                {
                    list =  treeViewEntities.Nodes.Cast<TreeNode>()
                                                  .Where(x => x.Checked)
                                                  .Select(x=> (EntityMetadata)x.Tag).ToList();
                }

                return list;
            }
        }

        public List<EntityMetadata> Entities
        {
            get
            {
                return entities;
            }
            set
            {
                entities = value;

                treeViewEntities.Nodes.Clear();

                if (entities != null)
                {
                    foreach (var item in entities)
                    {
                        var entityNode = new TreeNode($"{item.DisplayName.LocalizedLabels.FirstOrDefault()?.Label} ({item.LogicalName})")
                        {
                            Tag = item
                        };

                        treeViewEntities.Nodes.Add(entityNode);
                    }
                }
            }
        }
    }
}
