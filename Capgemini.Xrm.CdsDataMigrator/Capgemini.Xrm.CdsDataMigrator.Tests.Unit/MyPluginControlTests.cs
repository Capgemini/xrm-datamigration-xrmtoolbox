using FluentAssertions;
using McTools.Xrm.Connection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Moq;

namespace MyXrmToolBoxPlugin3.Tests
{
    [TestClass]
    public class MyPluginControlTests
    {
        [TestMethod]
        public void MyPluginControl()
        {
            FluentActions.Invoking(() => new MyPluginControl())
                         .Should()
                         .NotThrow();
        }

        [TestMethod]
        public void UpdateConnectionNullConnectionDetail()
        {
            var organisationService = new Mock<IOrganizationService>().Object;
            ConnectionDetail detail = null;
            string actionName = "Custom";
            object parameter = null;

            using (var systemUnderTest = new MyPluginControl())
            {
                FluentActions.Invoking(() => systemUnderTest.UpdateConnection(organisationService, detail, actionName, parameter))
                             .Should()
                             .NotThrow();
            }
        }
    }
}