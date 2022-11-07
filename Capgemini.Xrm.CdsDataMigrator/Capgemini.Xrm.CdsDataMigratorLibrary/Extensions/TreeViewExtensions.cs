using Capgemini.Xrm.CdsDataMigratorLibrary.Services;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Extensions
{
    public static class TreeViewExtensions
    {
        public static void PopulateEntitiesTreeView(this TreeView treeView, List<TreeNode> items, IWin32Window owner, INotificationService notificationService)
        {
            if (items != null && items.Count > 0)
            {
                treeView.Nodes.AddRange(items.ToArray());
            }
            else
            {
                notificationService.DisplayWarningFeedback(owner, "The system does not contain any entities");
            }
        }
    }
}