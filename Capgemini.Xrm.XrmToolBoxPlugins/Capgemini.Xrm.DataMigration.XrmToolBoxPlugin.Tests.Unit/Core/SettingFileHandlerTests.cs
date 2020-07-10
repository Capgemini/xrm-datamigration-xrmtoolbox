using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core.Tests
{
    [TestClass]
    public class SettingFileHandlerTests
    {
        [TestMethod]
        public void GetConfigData()
        {
            SettingFileHandler.GetConfigData(out Settings config);

            config.Should().NotBeNull();
        }

        [TestMethod]
        public void SaveConfigData()
        {
            Settings config = new Settings();

            var actual = SettingFileHandler.SaveConfigData(config);

            actual.Should().BeTrue();
        }
    }
}