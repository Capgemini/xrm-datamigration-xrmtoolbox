using System.Collections.Generic;
using System.Windows.Forms;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Helpers
{
    public interface IViewHelpers
    {
        bool AreAllCellsPopulated(DataGridViewRow row);
        List<DataGridViewRow> GetMappingsFromViewWithEmptyRowsRemoved(List<DataGridViewRow> viewLookupMappings);
    }
}
