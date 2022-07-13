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
    public partial class SchemaGeneratorPage : UserControl
    {
        public SchemaGeneratorPage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var entities = new List<Microsoft.Xrm.Sdk.Metadata.EntityMetadata>();
            for (int i = 0; i < 50; i++)
            {
                entities.Add(new Microsoft.Xrm.Sdk.Metadata.EntityMetadata()
                {
                    LogicalName = $"Enity {i}",
                    DisplayName = new Microsoft.Xrm.Sdk.Label($"Enity {i}",100)
                });
            }

            entityListView1.Entities = entities;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
           var g =  entityListView1.SelectedEntities;
        }
    }
}
