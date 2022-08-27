using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Capgemini.Xrm.CdsDataMigratorLibrary.Core;
using FluentAssertions;
using Moq;
using Microsoft.Xrm.Sdk;
using Capgemini.Xrm.CdsDataMigrator.Tests.Unit;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Presenters.Tests
{
    [TestClass]
    public class SchemaGeneratorPresenterTests : TestBase
    {
        private Mock<ISchemaGeneratorView> view;
        private Settings settings;

        private SchemaGeneratorPresenter systemUnderTest;

        [TestInitialize]
        public void Setup()
        {
            SetupServiceMocks();

            view = new Mock<ISchemaGeneratorView>();
            settings = new Settings();

            systemUnderTest = new SchemaGeneratorPresenter(view.Object,
                                                    ServiceMock.Object,
                                                    MetadataServiceMock.Object,
                                                    NotificationServiceMock.Object,
                                                    ExceptionServicerMock.Object,
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
            var connectedOrgFriendlyName = "testOrg";

            var entityResultList = new List<Microsoft.Xrm.Sdk.Metadata.EntityMetadata> { };
            MetadataServiceMock.Setup(a => a.RetrieveEntities(It.IsAny<IOrganizationService>()))
                           .Returns(entityResultList);

            using (var entityList = new System.Windows.Forms.TreeView())
            {
                using (var entityAttributeList = new System.Windows.Forms.ListView())
                {
                    using (var entityRelationshipList = new System.Windows.Forms.ListView())
                    {
                        view.SetupGet(a => a.EntityList).Returns(entityList);
                        view.SetupGet(a => a.EntityAttributeList).Returns(entityAttributeList);
                        view.SetupGet(a => a.EntityRelationshipList).Returns(entityRelationshipList);

                        FluentActions.Awaiting(() =>
                                                    systemUnderTest.OnConnectionUpdated(new Guid(), connectedOrgFriendlyName)
                                                )
                                     .Should()
                                     .NotThrow();
                    }
                }
            }

            view.VerifySet(a => a.CurrentConnection = $"Connected to: {connectedOrgFriendlyName}");
            view.VerifyGet(a => a.ShowSystemAttributes);
            MetadataServiceMock.Verify(a => a.RetrieveEntities(It.IsAny<IOrganizationService>()));
            ServiceMock.Verify();
            NotificationServiceMock.VerifyAll();
            ExceptionServicerMock.VerifyAll();
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