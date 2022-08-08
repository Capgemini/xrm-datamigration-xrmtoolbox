using Capgemini.Xrm.CdsDataMigratorLibrary.Models;
using Microsoft.Xrm.Sdk.Metadata;
using System;
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

        public event EventHandler ShowSystemEntitiesChanged;
        public event EventHandler<MigratorEventArgs<EntityMetadata>> CurrentSelectedEntityChanged;
        public event EventHandler<MigratorEventArgs<TreeNode>> EntitySelected;

        public List<EntityMetadata> SelectedEntities
        {
            get
            {
                var list = new List<EntityMetadata>();

                if (treeViewEntities?.Nodes != null)
                {
                    list = treeViewEntities.Nodes.Cast<TreeNode>()
                                                  .Where(x => x.Checked)
                                                  .Select(x => (EntityMetadata)x.Tag).ToList();
                }

                return list;
            }
        }

        public bool ShowSystemEntities
        {
            get
            {
                return checkBoxShowSystemAttributes.Checked;
            }
            set
            {
                checkBoxShowSystemAttributes.Checked = value;
            }
        }

        public TreeView EntityList => treeViewEntities;

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
                        var displayName = item.DisplayName.UserLocalizedLabel == null ? string.Empty : item.DisplayName.UserLocalizedLabel.Label;

                        var entityNode = new TreeNode($"{displayName} ({item.LogicalName})")
                        {
                            Tag = item
                        };

                        treeViewEntities.Nodes.Add(entityNode);
                    }
                }
            }
        }

        private void ShowSystemAttributesCheckedChanged(object sender, EventArgs e)
        {
            if (ShowSystemEntitiesChanged != null)
            {
                ShowSystemEntitiesChanged(this, e);
            }
        }

        private void SelectUnselectAllCheckedChanged(object sender, EventArgs e)
        {
            foreach (TreeNode item in treeViewEntities.Nodes)
            {
                item.Checked = checkBoxSelectUnselectAll.Checked;
            }
        }
        private void HandleTreeViewEntitiesAfterSelect(object sender, TreeViewEventArgs e)
        {
            CurrentSelectedEntityChanged?.Invoke(this, new MigratorEventArgs<EntityMetadata>(e.Node.Tag as EntityMetadata));
        }

        private void HandleTreeViewEntitiesAfterCheck(object sender, TreeViewEventArgs e)
        {
            EntitySelected?.Invoke(this, new MigratorEventArgs<TreeNode>(e.Node));
        }
    }
}
