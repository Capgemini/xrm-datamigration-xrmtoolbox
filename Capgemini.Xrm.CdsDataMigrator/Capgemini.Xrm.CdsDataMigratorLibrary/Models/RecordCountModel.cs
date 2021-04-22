namespace Capgemini.Xrm.CdsDataMigratorLibrary.Models
{
    public class RecordCountModel
    {
        public RecordCountModel()
        {
            RecordCount = 0;
        }

        public string EntityName { get; set; }

        public int RecordCount { get; set; }
    }
}