using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capgemini.Xrm.CdsDataMigratorLibrary.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Capgemini.Xrm.CdsDataMigratorLibrary.Models;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.UserControls.Tests
{
    [TestClass]
    public class ListManagerViewTests
    {
        [TestMethod]
        public void ListManagerView()
        {
            using (var systemUnderTest = new ListManagerView())
            {
                systemUnderTest.ListView.Items.Count.Should().Be(0);
            }
        }

        [TestMethod]
        public void SetDisplayedItemsName()
        {
            var displayName = "Test value";

            using (var systemUnderTest = new ListManagerView())
            {
                systemUnderTest.DisplayedItemsName = displayName;

                systemUnderTest.DisplayedItemsName.Should().Be(displayName);
            }
        }
    }
}