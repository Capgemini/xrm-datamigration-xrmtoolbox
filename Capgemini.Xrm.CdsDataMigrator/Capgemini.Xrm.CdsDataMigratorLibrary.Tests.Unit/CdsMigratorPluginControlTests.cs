using Capgemini.Xrm.CdsDataMigratorLibrary;
using FluentAssertions;
using McTools.Xrm.Connection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Moq;

namespace Capgemini.Xrm.CdsDataMigrator.Tests.Unit
{
    [TestClass]
    public class CdsMigratorPluginControlTests
    {
        [Ignore("To be fixed!")]
        [TestMethod]
        public void CdsDataMigratorPluginControl()
        {
            FluentActions.Invoking(() => new CdsMigratorPluginControl())
                         .Should()
                         .NotThrow();
        }

        [Ignore("To be fixed!")]
        [TestMethod]
        public void UpdateConnectionNullConnectionDetail()
        {
            var organisationService = new Mock<IOrganizationService>().Object;
            ConnectionDetail detail = null;
            string actionName = "Custom";
            object parameter = null;

            using (var systemUnderTest = new CdsMigratorPluginControl())
            {
                FluentActions.Invoking(() => systemUnderTest.UpdateConnection(organisationService, detail, actionName, parameter))
                             .Should()
                             .NotThrow();
            }
        }
    }
}