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
            SettingFileHandler.GetConfigData<ListController>(out Settings actual);

            actual.Should().NotBeNull();
        }

        [TestMethod]
        public void SaveConfigData()
        {
            SettingFileHandler.GetConfigData<ListController>(out Settings config);

            var actual = SettingFileHandler.SaveConfigData<ListController>(config);

            actual.Should().BeFalse();
        }
    }
}