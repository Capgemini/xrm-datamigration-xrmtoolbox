namespace Capgemini.Xrm.CdsDataMigratorLibrary.Core
{
    public class Item<TKey, TValue>
    {
        public Item(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }

        public TKey Key { get; set; }

        public TValue Value { get; set; }
    }
}