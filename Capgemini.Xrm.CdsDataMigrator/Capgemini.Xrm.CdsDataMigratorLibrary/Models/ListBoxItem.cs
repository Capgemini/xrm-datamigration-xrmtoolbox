namespace Capgemini.Xrm.CdsDataMigratorLibrary.Models
{
    public class ListBoxItem<T>
    {
        public string DisplayName { get; set; }
        public T Item { get; set; }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}
