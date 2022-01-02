using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Capgemini.Xrm.CdsDataMigratorLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capgemini.Xrm.CdsDataMigratorLibrary.Enums;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Tests.Unit.Extensions
{
    [TestClass]
    public class FormControlExtensionsTests
    {
        [TestMethod]
        public void PopulateComboBoxLogLevel()
        {
            var systemUnderTest = new System.Windows.Forms.ComboBox();
            var expectedCount = Enum.GetValues(typeof(LogLevel)).Length;

            FluentActions.Invoking(() => systemUnderTest.PopulateComboBoxLogLevel())
                         .Should()
                         .NotThrow();

            systemUnderTest.Items.Count.Should().Be(expectedCount);
        }
    }
}