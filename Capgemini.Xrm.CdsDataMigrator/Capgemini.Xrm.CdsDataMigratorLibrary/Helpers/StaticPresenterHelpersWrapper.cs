using System.Collections.Generic;
using System.Windows.Forms;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Helpers
{
    public class StaticPresenterHelpersWrapper : IStaticPresenterHelpersWrapper
    {
        public bool AreAllCellsPopulated(DataGridViewRow row)
        {
            return PresenterHelpers.AreAllCellsPopulated(row);
        }
        public List<DataGridViewRow> GetMappingsFromViewWithEmptyRowsRemoved(List<DataGridViewRow> viewLookupMappings)
        {
            return PresenterHelpers.GetMappingsFromViewWithEmptyRowsRemoved(viewLookupMappings);
        }
    }
}
