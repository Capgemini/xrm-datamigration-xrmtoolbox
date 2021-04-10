using Capgemini.Xrm.CdsDataMigratorLibrary.UserControls;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.Xrm.CdsDataMigrator.Tests.Unit.UserControls
{
    [TestClass]
    public class ToggleCheckBoxTests
    {
        [TestMethod]
        public void ToggleCheckBoxInitialization()
        {
            FluentActions.Invoking(() => new ToggleCheckBox())
                             .Should()
                             .NotThrow();
        }

        [TestMethod]
        public void ToggleOnPaint()
        {
            using (var systemUnderTest = new ToggleCheckBox())
            {
                using (var image = new System.Drawing.Bitmap(5, 5))
                {
                    using (var graphics = System.Drawing.Graphics.FromImage(image))
                    {
                        using (var pevent = new System.Windows.Forms.PaintEventArgs(graphics, new System.Drawing.Rectangle(2, 2, 4, 4)))
                        {
                            FluentActions.Invoking(() => systemUnderTest.ProcessPaintRequest(pevent))
                                         .Should()
                                         .NotThrow();
                        }
                    }
                }
            }
        }
    }
}