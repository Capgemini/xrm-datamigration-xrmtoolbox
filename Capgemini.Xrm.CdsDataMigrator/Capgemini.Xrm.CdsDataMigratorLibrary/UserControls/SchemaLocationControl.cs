using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.UserControls
{
    public partial class SchemaLocationControl : UserControl
    {
        public SchemaLocationControl()
        {
            InitializeComponent();
        }

        public string SchemaFilenamePath => fileInputSelectorSchema.Value;
    }
}
