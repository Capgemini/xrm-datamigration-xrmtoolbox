using System;
using System.Collections;
using System.Windows.Forms;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Core
{
    public class ListViewItemComparer : IComparer
    {
        private readonly int col;
        private readonly SortOrder order;

        public ListViewItemComparer()
        {
            col = 0;
            order = SortOrder.Ascending;
        }

        public ListViewItemComparer(int column, SortOrder order)
        {
            col = column;
            this.order = order;
        }

        public int Compare(object x, object y)
        {
            var returnVal = string.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text, StringComparison.InvariantCulture);

            // Determine whether the sort order is descending.
            if (order == SortOrder.Descending)
            {
                // Invert the value returned by String.Compare.
                returnVal *= -1;
            }

            return returnVal;
        }
    }
}