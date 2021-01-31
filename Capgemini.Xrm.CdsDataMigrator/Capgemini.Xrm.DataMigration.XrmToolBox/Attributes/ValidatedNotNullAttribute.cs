using System;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
    public sealed class ValidatedNotNullAttribute : Attribute
    {
    }
}