using System;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
    public sealed class ValidatedNotNullAttribute : Attribute
    {
    }
}