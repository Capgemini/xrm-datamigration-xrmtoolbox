using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.UserControls.Tests
{
    [TestClass]
    public class SchemaGeneratorPageTests
    {
        [TestMethod]
        public void SchemaGeneratorPage()
        {
            using (var systemUnderTest = new SchemaGeneratorPage())
            {
                systemUnderTest.EntityMetadataList.Should().BeNull();
            }
        }

        [TestMethod]
        public void ShowInformationPanel()
        {
            string mesage = "test message";
            int width = 340;
            int height = 150;

            using (var systemUnderTest = new SchemaGeneratorPage())
            {
                FluentActions.Invoking(() =>
                {
                    systemUnderTest.ShowInformationPanel(mesage, width, height);
                })
                    .Should()
                    .NotThrow();
            }
        }

        [TestMethod]
        public void CloseInformationPanel()
        {
            using (var systemUnderTest = new SchemaGeneratorPage())
            {
                FluentActions.Invoking(() =>
                {
                    systemUnderTest.CloseInformationPanel();
                })
                    .Should()
                    .NotThrow();
            }
        }
    }
}