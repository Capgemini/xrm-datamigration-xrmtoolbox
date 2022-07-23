using System;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.UserControls
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
