namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Model
{
    public class RecordCountModel
    {
        public string EntityName { get; set; }

        public int RecordCount { get; set; }

        public RecordCountModel()
        {
            RecordCount = 0;
        }
    }
}