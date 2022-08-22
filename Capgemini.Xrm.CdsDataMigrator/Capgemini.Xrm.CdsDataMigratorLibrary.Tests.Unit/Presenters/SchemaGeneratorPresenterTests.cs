using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capgemini.Xrm.CdsDataMigratorLibrary.Presenters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capgemini.Xrm.CdsDataMigratorLibrary.Core;
using FluentAssertions;
using Capgemini.Xrm.CdsDataMigratorLibrary.Controllers;
using FluentAssertions;
using Moq;
using Capgemini.Xrm.CdsDataMigratorLibrary.Services;
using Capgemini.Xrm.CdsDataMigratorLibrary.Exceptions;
using Microsoft.Xrm.Sdk;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Presenters.Tests
{
    [TestClass]
    public class SchemaGeneratorPresenterTests
    {
        private Mock<ISchemaGeneratorView> view;
        private Mock<IOrganizationService> organizationService;
        private Mock<IMetadataService> metadataService;
        private Mock<INotificationService> notificationService;
        private Mock<IExceptionService> exceptionService;
        private Settings settings;

        private SchemaGeneratorPresenter systemUnderTest;

        [TestInitialize]
        public void Setup()
        {
            view = new Mock<ISchemaGeneratorView>();
            organizationService = new Mock<IOrganizationService>();
            metadataService = new Mock<IMetadataService>();
            notificationService = new Mock<INotificationService>();
            exceptionService = new Mock<IExceptionService>();
            settings = new Settings();

            systemUnderTest = new SchemaGeneratorPresenter(view.Object,
                                                        organizationService.Object,
                                                        metadataService.Object,
                                                        notificationService.Object,
                                                        exceptionService.Object,
                                                        settings);
        }

        [TestMethod]
        public void SchemaGeneratorPresenter_ParameterBag()
        {
            systemUnderTest.ParameterBag.Should().NotBeNull();
        }

        [TestMethod]
        public void SchemaGeneratorPresenter_View()
        {
            systemUnderTest.View.Should().NotBeNull();
        }

        [TestMethod]
        public void OnConnectionUpdated()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void GetAttributeList()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void FilterAttributes()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void ClearMemory()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void LoadSchemaFile()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void ClearAllListViews()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void HandleListViewEntitiesSelectedIndexChanged()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void ManageWorkingState()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void PopulateAttributes()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void PopulateRelationship()
        {
            Assert.Fail();
        }

    }
}