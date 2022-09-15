using System.Collections.Generic;
using System.Windows.Forms;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Helpers
{
    public class ViewHelpers : IViewHelpers
    {
        public bool AreAllCellsPopulated(DataGridViewRow row)
        {
            foreach (DataGridViewCell cell in row.Cells)
            {
                if (string.IsNullOrEmpty(cell.Value as string))
                {
                    return false;
                }
            }
            return true;
        }


        public List<DataGridViewRow> GetMappingsFromViewWithEmptyRowsRemoved(List<DataGridViewRow> viewLookupMappings)
        {
            var filteredViewLookupMappings = new List<DataGridViewRow>();
            foreach (DataGridViewRow viewLookupRow in viewLookupMappings)
            {
                if (AreAllCellsPopulated(viewLookupRow))
                {
                    filteredViewLookupMappings.Add(viewLookupRow);
                }
            }
            return filteredViewLookupMappings;
        }
    }
}
