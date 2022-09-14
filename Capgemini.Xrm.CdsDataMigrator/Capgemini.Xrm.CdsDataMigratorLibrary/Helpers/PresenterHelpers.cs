using System.Collections.Generic;
using System.Windows.Forms;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Helpers
{
    public static class PresenterHelpers
    {
        public static bool AreAllCellsPopulated(DataGridViewRow row)
        {
            if (string.IsNullOrEmpty((string)row.Cells[0].Value) || string.IsNullOrEmpty((string)row.Cells[1].Value) || string.IsNullOrEmpty((string)row.Cells[2].Value))
            {
                return false;
            }
            return true;
        }


        public static List<DataGridViewRow> GetMappingsFromViewWithEmptyRowsRemoved(List<DataGridViewRow> viewLookupMappings)
        {
            var filteredViewLookupMappings = new List<DataGridViewRow>();
            foreach (DataGridViewRow viewLookupRow in viewLookupMappings)
            {
                if (!string.IsNullOrEmpty((string)viewLookupRow.Cells[0].Value) && !string.IsNullOrEmpty((string)viewLookupRow.Cells[1].Value) && !string.IsNullOrEmpty((string)viewLookupRow.Cells[2].Value))
                {
                    filteredViewLookupMappings.Add(viewLookupRow);
                }
            }
            return filteredViewLookupMappings;
        }
    }
}
