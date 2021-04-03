using System;
using System.Collections.Generic;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;

namespace Capgemini.Xrm.CdsDataMigrator.Tests.Unit.Core
{
    [TestClass]
    public class SettingFileHandlerTest
    {
        [TestMethod]
        public void GetConfigData()
        {
            Settings actual;

            SettingFileHandler.GetConfigData(out actual);

            actual.Should().NotBeNull();
        }

        [TestMethod]
        public void SaveConfigData()
        {
            Settings config;
            SettingFileHandler.GetConfigData(out config);

            var actual = SettingFileHandler.SaveConfigData(config);

            actual.Should().BeFalse();
        }
    }
}