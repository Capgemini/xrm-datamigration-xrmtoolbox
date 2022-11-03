using Capgemini.Xrm.CdsDataMigratorLibrary.Models;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.UserControls
{
    public partial class ListManagerView : UserControl
    {
        public ListManagerView()
        {
            InitializeComponent();
        }

        public event EventHandler<MigratorEventArgs<int>> ListViewColumnClick;
        public event EventHandler<MigratorEventArgs<ItemCheckEventArgs>> ListViewItemCheck;

        public string DisplayedItemsName
        {
            get
            {
                return labelDisplayedItemsName.Text;
            }
            set
            {
                labelDisplayedItemsName.Text = value;
            }
        }

        public ListView ListView => listViewItems;

        private void SelectUnselectAllCheckedChanged(object sender, EventArgs e)
        {
            listViewItems.Items.OfType<ListViewItem>().ToList().ForEach(item => item.Checked = checkBoxSelectUnselectAll.Checked);
        }

        private void listViewItems_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ListViewColumnClick?.Invoke(this, new MigratorEventArgs<int>(e.Column));
        }

        private void listViewItems_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            ListViewItemCheck?.Invoke(this, new MigratorEventArgs<ItemCheckEventArgs>(e));
        }

    }
}
