using System;
using System.Diagnostics.CodeAnalysis;
using Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Exceptions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Helpers.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class ConnectionHelperTests
    {
        private string connectionString;

        private IOrganizationService actual;

        [TestInitialize]
        public void Setup()
        {
            connectionString = "Url = https://capgeminitest.crm4.dynamics.com; Username=someusername@someorg.onmicrosoft.com; Password=SomePassword; AuthType=Office365; RequireNewInstance=True;";
        }

        [TestMethod]
        public void GetOrganizationalServiceNullConnectionString()
        {
            connectionString = null;

            FluentActions.Invoking(() => actual = ConnectionHelper.GetOrganizationalService(connectionString))
                         .Should()
                         .Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void GetOrganizationalServiceInvalidCredentialsAndNoRequireNewInstance()
        {
            FluentActions.Invoking(() => actual = ConnectionHelper.GetOrganizationalService(connectionString))
                         .Should()
                         .Throw<OrganizationalServiceException>();
        }

        [TestMethod]
        public void GetOrganizationalServiceInvalidCredentials()
        {
            FluentActions.Invoking(() => actual = ConnectionHelper.GetOrganizationalService(connectionString))
                         .Should()
                         .Throw<OrganizationalServiceException>();
        }

        [TestMethod]
        public void GetOrganizationalServiceNullCrmServiceClient()
        {
            CrmServiceClient serviceClient = null;

            FluentActions.Invoking(() => actual = ConnectionHelper.GetOrganizationalService(serviceClient))
                         .Should()
                         .Throw<ArgumentException>();
        }

        [TestMethod]
        public void GetOrganizationalServiceCrmServiceClient()
        {
            using (CrmServiceClient serviceClient = new CrmServiceClient(connectionString))
            {
                FluentActions.Invoking(() => actual = ConnectionHelper.GetOrganizationalService(serviceClient))
                             .Should()
                             .NotThrow();

                actual.Should().BeNull();
            }
        }
    }
}