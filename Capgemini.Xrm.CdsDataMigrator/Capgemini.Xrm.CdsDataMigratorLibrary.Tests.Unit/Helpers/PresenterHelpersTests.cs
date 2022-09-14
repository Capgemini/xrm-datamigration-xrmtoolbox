using System.Windows.Forms;
using Capgemini.Xrm.CdsDataMigratorLibrary.Helpers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capgemini.Xrm.DataMigration.XrmToolBox.Helpers.Tests
{
    [TestClass]
    public class PresenterHelpersTests
    {
        [TestMethod]
        public void RowWithoutEmptyCellShouldReturnTrue()
        {
            var rowWithBlankCell = GetRowWithoutBlankCell();
            var allCellsArePopulated = PresenterHelpers.AreAllCellsPopulated(rowWithBlankCell);

            Assert.AreEqual(allCellsArePopulated, true);
        }


        [TestMethod]
        public void RowWithEmptyCellShouldReturnFalse()
        {
            var rowWithBlankCell = GetRowWithBlankCell();
            var allCellsArePopulated = PresenterHelpers.AreAllCellsPopulated(rowWithBlankCell);

            Assert.AreEqual(allCellsArePopulated, false);
        }
        
        private static DataGridViewRow GetRowWithBlankCell()
        {
            DataGridViewRow dataGridViewRow = new DataGridViewRow();
            dataGridViewRow.Cells.Add(new DataGridViewTextBoxCell { Value = "Account" });
            dataGridViewRow.Cells.Add(new DataGridViewTextBoxCell { Value = "" });
            dataGridViewRow.Cells.Add(new DataGridViewTextBoxCell { Value = "accountid" });
            return dataGridViewRow;
        }

        private static DataGridViewRow GetRowWithoutBlankCell()
        {
            DataGridViewRow dataGridViewRow = new DataGridViewRow();
            dataGridViewRow.Cells.Add(new DataGridViewTextBoxCell { Value = "Account" });
            dataGridViewRow.Cells.Add(new DataGridViewTextBoxCell { Value = "accountrelated" });
            dataGridViewRow.Cells.Add(new DataGridViewTextBoxCell { Value = "accountid" });
            return dataGridViewRow;
        }

    }
}