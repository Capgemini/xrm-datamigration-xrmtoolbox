using System;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Attributes;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Extensions
{
    public static class ExceptionExtensions
    {
        public static void ThrowArgumentNullExceptionIfNull([ValidatedNotNull] this object input, string argumentName, string message = "input parameter should not be null!")
        {
            if (input == null)
            {
                throw new ArgumentNullException(argumentName, message);
            }
        }

        public static void ThrowIfNull<T>([ValidatedNotNull] this object input, string message)
    where T : Exception
        {
            if (input == null)
            {
                throw Activator.CreateInstance(typeof(T), message) as T;
            }
        }

        public static void ThrowArgumentOutOfRangeExceptionIfTrue(this bool input, string argumentName, string message = "")
        {
            if (input)
            {
                throw new ArgumentOutOfRangeException(argumentName, message);
            }
        }
    }
}