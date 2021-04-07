using Capgemini.Xrm.CdsDataMigratorLibrary.Core;
using Capgemini.Xrm.CdsDataMigratorLibrary.UserControls;
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
            SettingFileHandler.GetConfigData<SchemaWizardDelegate>(out Settings actual);

            actual.Should().NotBeNull();
        }

        [TestMethod]
        public void SaveConfigData()
        {
            SettingFileHandler.GetConfigData<SchemaWizardDelegate>(out Settings config);

            var actual = SettingFileHandler.SaveConfigData<SchemaWizardDelegate>(config);

            actual.Should().BeFalse();
        }
    }
}