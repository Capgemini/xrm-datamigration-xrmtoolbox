using Microsoft.Xrm.Sdk.Metadata;
using System.Drawing;
using System.Windows.Forms;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Extensions
{
    public static class TreeNodeExtensions
    {

        public static string GetEntityLogicalName(this TreeNode entityitem)
        {
            string logicalName = null;
            if (entityitem != null && entityitem.Tag != null)
            {
                var entity = (EntityMetadata)entityitem.Tag;
                logicalName = entity.LogicalName;
            }
            return logicalName;
        }

        public static void IsInvalidForCustomization(this TreeNode item, EntityMetadata entity)
        {
            if (entity != null)
            {
                if (entity.IsCustomEntity != null && entity.IsCustomEntity.Value)
                {
                    item.ForeColor = Color.DarkGreen;
                }

                if (entity.IsIntersect != null && entity.IsIntersect.Value)
                {
                    item.ForeColor = Color.Red;
                    item.ToolTipText = "Intersect Entity, ";
                }

                if (entity.IsLogicalEntity != null && entity.IsLogicalEntity.Value)
                {
                    item.ForeColor = Color.Red;
                    item.ToolTipText = "Logical Entity";
                }
            }
        }

    }
}
