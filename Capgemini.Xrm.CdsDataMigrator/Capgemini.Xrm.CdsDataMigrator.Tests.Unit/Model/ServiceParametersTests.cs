using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Model;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.Xrm.CdsDataMigrator.Tests.Unit.Model
{
    [TestClass]
    public class ServiceParametersTests : TestBase
    {
        [TestMethod]
        public void CanInstantiate()
        {
            SetupServiceMocks();

            var systemUndertest = new ServiceParameters(ServiceMock.Object, MetadataServiceMock.Object, NotificationServiceMock.Object, ExceptionServicerMock.Object);

            systemUndertest.OrganizationService.Should().NotBeNull();
            systemUndertest.MetadataService.Should().NotBeNull();
            systemUndertest.NotificationService.Should().NotBeNull();
            systemUndertest.ExceptionService.Should().NotBeNull();
        }
    }
}