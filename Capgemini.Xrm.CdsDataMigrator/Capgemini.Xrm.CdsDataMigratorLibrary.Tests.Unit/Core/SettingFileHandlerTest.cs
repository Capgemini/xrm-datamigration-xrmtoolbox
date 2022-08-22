using Capgemini.Xrm.CdsDataMigratorLibrary.Controllers;
using Capgemini.Xrm.CdsDataMigratorLibrary.Core;
using Capgemini.Xrm.CdsDataMigratorLibrary.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.Xrm.CdsDataMigrator.Tests.Unit.Core
{
    [TestClass]
    public class SettingFileHandlerTest
    {
        [TestMethod]
        public void GetConfigData()
        {
            SettingFileHandler.GetConfigData<ListViewExtension>(out Settings actual);

            actual.Should().NotBeNull();
        }
    }
}