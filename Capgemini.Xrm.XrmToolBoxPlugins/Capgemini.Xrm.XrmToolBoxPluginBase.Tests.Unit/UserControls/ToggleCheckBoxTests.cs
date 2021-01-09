using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.UserControls.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class ToggleCheckBoxTests
    {
        [TestMethod]
        public void ToggleCheckBoxInitialization()
        {
            FluentActions.Invoking(() => new ToggleCheckBox())
                             .Should()
                             .NotThrow();
        }
    }
}