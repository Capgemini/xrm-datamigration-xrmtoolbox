using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Microsoft.Xrm.Sdk.Metadata;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.UserControls
{
    public partial class ListManagerView : UserControl
    {
        public ListManagerView()
        {
            InitializeComponent();
        }

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
            foreach (ListViewItem item in listViewItems.Items)
            {
                item.Checked = checkBoxSelectUnselectAll.Checked;
            }
        }

        //Dictionary<string, HashSet<string>> EntityAttributes
        //public List<AttributeMetadata> EntityAttributes
        //{
        //    //get { }
        //    set
        //    {
        //        listViewItems.Items.Clear();
        //        foreach (var item in value)
        //        {
        //            string newItem = item.SchemaName;//.DisplayName.UserLocalizedLabel.Label;
        //            listViewItems.Items.Add(newItem);
        //        }
        //    }
        //}
    }
}
