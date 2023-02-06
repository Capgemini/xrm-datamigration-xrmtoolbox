using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using Capgemini.Xrm.CdsDataMigrator.Tests.Unit;
using System.Windows.Forms;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Extensions.Tests
{
    [TestClass]
    public class TreeViewExtensionsTests : TestBase
    {
        [TestInitialize]
        public void Setup()
        {
            SetupServiceMocks();
        }

        [TestMethod]
        public void PopulateEntitiesListViewWithMoreThanZeroNodes()
        {
            var items = new List<System.Windows.Forms.TreeNode>
            {
                new System.Windows.Forms.TreeNode("Item1"),
                new System.Windows.Forms.TreeNode("Item2")
            };

            using (var treeView = new System.Windows.Forms.TreeView())
            {
                FluentActions.Invoking(() => treeView.PopulateEntitiesTreeView(items, null, NotificationServiceMock.Object))
                            .Should()
                            .NotThrow();

                treeView.Nodes.Count.Should().Be(items.Count);
                NotificationServiceMock.Verify(a => a.DisplayWarningFeedback(It.IsAny<IWin32Window>(), "The system does not contain any entities"), Times.Never);
            }
        }

        [TestMethod]
        public void PopulateEntitiesListViewWithMoreZeroNodes()
        {
            var items = new List<System.Windows.Forms.TreeNode>();

            NotificationServiceMock.Setup(a => a.DisplayWarningFeedback(It.IsAny<IWin32Window>(), "The system does not contain any entities"));

            using (var treeView = new System.Windows.Forms.TreeView())
            {
                FluentActions.Invoking(() => treeView.PopulateEntitiesTreeView(items, null, NotificationServiceMock.Object))
                            .Should()
                            .NotThrow();

                treeView.Nodes.Count.Should().Be(0);
                NotificationServiceMock.Verify(a => a.DisplayWarningFeedback(It.IsAny<IWin32Window>(), "The system does not contain any entities"), Times.Once);
            }
        }
    }
}