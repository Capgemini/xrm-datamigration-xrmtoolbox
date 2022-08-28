using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.Xrm.Sdk.Metadata;
using Capgemini.Xrm.CdsDataMigrator.Tests.Unit;
using System.Reflection;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Extensions.Tests
{
    [TestClass]
    public class TreeNodeExtensionsTests : TestBase
    {
        private Dictionary<string, HashSet<string>> inputEntityRelationships;
        private Dictionary<string, HashSet<string>> inputEntityAttributes;
        private EntityMetadata entity;

        [TestInitialize]
        public void Setup()
        {
            inputEntityRelationships = new Dictionary<string, HashSet<string>>();
            inputEntityAttributes = new Dictionary<string, HashSet<string>>();

            entity = new EntityMetadata
            {
                LogicalName = "contactattnoentity1",
            };

            SetupServiceMocks();
        }

        [TestMethod]
        public void IsInvalidForCustomizationIsCustomEntity()
        {
            var isLogicalEntityField = entity.GetType().GetRuntimeFields().First(a => a.Name == "_isCustomEntity");
            isLogicalEntityField.SetValue(entity, (bool?)true);

            var item = new System.Windows.Forms.TreeNode("Item1");

            FluentActions.Invoking(() => item.IsInvalidForCustomization(entity))
                           .Should()
                           .NotThrow();

            item.ForeColor.Should().Be(System.Drawing.Color.DarkGreen);
        }

        [TestMethod]
        public void IsInvalidForCustomizationIsIntersect()
        {
            var isLogicalEntityField = entity.GetType().GetRuntimeFields().First(a => a.Name == "_isIntersect");
            isLogicalEntityField.SetValue(entity, (bool?)true);

            var item = new System.Windows.Forms.TreeNode("Item1");

            FluentActions.Invoking(() => item.IsInvalidForCustomization(entity))
                         .Should()
                         .NotThrow();

            item.ForeColor.Should().Be(System.Drawing.Color.Red);
            item.ToolTipText.Should().Contain("Intersect Entity, ");
        }

        [TestMethod]
        public void IsInvalidForCustomizationIsIntersectNull()
        {
            var item = new System.Windows.Forms.TreeNode("Item1");

            FluentActions.Invoking(() => item.IsInvalidForCustomization(entity))
                         .Should()
                         .NotThrow();
            item.ForeColor.Should().NotBe(System.Drawing.Color.Red);
            item.ToolTipText.Should().NotContain("Intersect Entity, ");
        }

        [TestMethod]
        public void IsInvalidForCustomizationIsLogicalEntity()
        {
            var isLogicalEntityField = entity.GetType().GetRuntimeFields().First(a => a.Name == "_isLogicalEntity");
            isLogicalEntityField.SetValue(entity, (bool?)true);

            var item = new System.Windows.Forms.TreeNode("Item1");

            FluentActions.Invoking(() => item.IsInvalidForCustomization(entity))
                         .Should()
                         .NotThrow();

            item.ForeColor.Should().Be(System.Drawing.Color.Red);
            item.ToolTipText.Should().Contain("Logical Entity");
        }

        [TestMethod]
        public void IsInvalidForCustomizationIsLogicalEntityNull()
        {
            var item = new System.Windows.Forms.TreeNode("Item1");

            FluentActions.Invoking(() => item.IsInvalidForCustomization(entity))
                         .Should()
                         .NotThrow();

            item.ForeColor.Should().NotBe(System.Drawing.Color.Red);
            item.ToolTipText.Should().NotContain("Logical Entity");
        }

        [TestMethod]
        public void GetEntityLogicalNameNullTreeNode()
        {
            System.Windows.Forms.TreeNode entityitem = null;

            var actual = entityitem.GetEntityLogicalName();

            actual.Should().BeNull();
        }

        [TestMethod]
        public void GetEntityLogicalName()
        {
            var logicalName = "Case";
            var entityMetadata = InstantiateEntityMetaData(logicalName);
            var entityitem = new System.Windows.Forms.TreeNode(logicalName)
            {
                Tag = entityMetadata
            };

            var actual = entityitem.GetEntityLogicalName();

            actual.Should().Be(logicalName);
        }
    }
}