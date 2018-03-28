
namespace Capgemini.Xrm.XrmToolBoxPluginBase.DataMigration
{
    public class MigrationParameters
    {
        public string ConnectionString { get; set; }

        public MigrationParameters(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public virtual bool Validate(out string message)
        {
            var result = false;

            if (string.IsNullOrWhiteSpace(ConnectionString))
            {
                message = "Please supply a valid CRM connection string for the operation!";
            }
            else
            {
                message = "";
                result = true;
            }

            return result;
        }

    }
}
