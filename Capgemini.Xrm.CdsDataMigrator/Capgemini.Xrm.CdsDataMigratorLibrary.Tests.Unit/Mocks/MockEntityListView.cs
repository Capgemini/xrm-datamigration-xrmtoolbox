using Capgemini.Xrm.CdsDataMigratorLibrary.UserControls;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Tests.Unit.Mocks
{
    public class MockEntityListView : EntityListView
    {
        public void SelectTreeViewNodes(List<int> nodesToSelect)
        {
            var field = GetType().BaseType.GetField("treeViewEntities", BindingFlags.NonPublic |
                         BindingFlags.Instance);

            var treeView = (TreeView)field.GetValue(this);


            foreach (var nodeIndex in nodesToSelect)
            {
                if (treeView.Nodes.Count > nodeIndex)
                {
                    treeView.Nodes[nodeIndex].Checked = true;
                }
            }
        }
    }
}
