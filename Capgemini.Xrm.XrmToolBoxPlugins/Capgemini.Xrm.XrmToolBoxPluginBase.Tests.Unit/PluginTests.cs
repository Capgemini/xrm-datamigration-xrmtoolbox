using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core;
using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Microsoft.Xrm.Sdk;
using XrmToolBox.Extensibility.Interfaces;

namespace MyXrmToolBoxPlugin3.Tests
{
    [TestClass]
    public class PluginTests
    {
        [TestMethod]
        public void GetControl()
        {
            var systemUnderTest = new Plugin();

            var actual = systemUnderTest.GetControl();

            actual.Should().NotBeNull();
            actual.Should().BeOfType<MyPluginControl>();
        }

        [TestMethod]
        public void PluginInstantiation()
        {
            FluentActions.Invoking(() => new Plugin())
                         .Should()
                         .NotThrow();
        }
    }
}