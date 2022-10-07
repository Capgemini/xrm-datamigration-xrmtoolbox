using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capgemini.Xrm.CdsDataMigratorLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Capgemini.Xrm.CdsDataMigratorLibrary.Services;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Models.Tests
{
    [TestClass]
    public class MigratorEventArgsTests
    {
        [TestMethod]
        public void MigratorEventArgs()
        {
            MigratorEventArgs<string> migratorEventArgs = null;
            var input = "MigratorEventArgs";

            FluentActions.Invoking(() => migratorEventArgs = new MigratorEventArgs<string>(input))
                        .Should()
                        .NotThrow();

            migratorEventArgs.Input.Should().Be(input);
        }
    }
}