﻿using Capgemini.Xrm.CdsDataMigratorLibrary.Models;
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

        //public EntityMetadata CurrentSelectedEntity { get; private set; }
        //{
        //    get
        //    {
        //        var list = new List<EntityMetadata>();

        //        if (treeViewEntities?.Nodes != null)
        //        {
        //            list = treeViewEntities.Nodes.Cast<TreeNode>()
        //                                          .Where(x => x.Checked)
        //                                          .Select(x => (EntityMetadata)x.Tag).ToList();
        //        }

        //        return list;
        //    }
        //}

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
                    //foreach (EntityMetadata entity in inputCachedMetadata)
                    //{
                    //    var name = entity.DisplayName.UserLocalizedLabel == null ? string.Empty : entity.DisplayName.UserLocalizedLabel.Label;
                    //    var item = new ListViewItem(name)
                    //    {
                    //        Tag = entity
                    //    };
                    //    item.SubItems.Add(entity.LogicalName);
                    //    IsInvalidForCustomization(entity, item);
                    //    UpdateCheckBoxesEntities(entity, item, inputEntityAttributes);

                    //    sourceEntitiesList.Add(item);
                    //}
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
           // var entityMetadata = e.Node.Tag as EntityMetadata;
            //if (entityMetadata != null)
            //{
            var handler = CurrentSelectedEntityChanged;
            if (handler != null)
            {
                handler(this, new MigratorEventArgs<EntityMetadata>(e.Node.Tag as EntityMetadata));
            }
            //    CurrentSelectedEntity = e.Node.Tag as EntityMetadata;
            //}
        }
    }
}
