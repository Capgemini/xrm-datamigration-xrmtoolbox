using System;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Models
{
    public class MigratorEventArgs<T> : EventArgs
    {
        public MigratorEventArgs(T input)
        {
            Input = input;
        }

        public T Input { get; }
    }
}
