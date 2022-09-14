using System.Collections.Generic;
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
        public void AreAllCellsPopulated_RowWithoutEmptyCellShouldReturnTrue()
        {
            var rowWithBlankCell = GetRowWithoutBlankCell();
            var allCellsArePopulated = PresenterHelpers.AreAllCellsPopulated(rowWithBlankCell);

            Assert.AreEqual(allCellsArePopulated, true);
        }


        [TestMethod]
        public void AreAllCellsPopulated_RowWithEmptyCellShouldReturnFalse()
        {
            var rowWithBlankCell = GetRowWithBlankCell();
            var allCellsArePopulated = PresenterHelpers.AreAllCellsPopulated(rowWithBlankCell);

            Assert.AreEqual(allCellsArePopulated, false);
        }

        [TestMethod]
        public void GetMappingsFromViewWithEmptyRowsRemoved_EmptyRowsShouldBeCorrectlyRemoved()
        {
            var lookUpMappings = new List<DataGridViewRow>();
            var rowWithoutBlankCell = GetRowWithoutBlankCell();
            var rowWithBlankCell = GetRowWithBlankCell();
            lookUpMappings.Add(rowWithoutBlankCell);
            lookUpMappings.Add(rowWithBlankCell);
            var updatedLookupMappings = PresenterHelpers.GetMappingsFromViewWithEmptyRowsRemoved(lookUpMappings);
            Assert.AreEqual(updatedLookupMappings.Count, 1);  
        }

        [TestMethod]
        public void GetMappingsFromViewWithEmptyRowsRemoved_NoRowsShouldBeRemoved()
        {
            var lookUpMappings = new List<DataGridViewRow>();
            var rowWithoutBlankCell = GetRowWithoutBlankCell();
            var anotherRowWithoutBlankCell = GetRowWithoutBlankCell();
            lookUpMappings.Add(rowWithoutBlankCell);
            lookUpMappings.Add(anotherRowWithoutBlankCell);
            var updatedLookupMappings = PresenterHelpers.GetMappingsFromViewWithEmptyRowsRemoved(lookUpMappings);
            Assert.AreEqual(updatedLookupMappings.Count, 2);
        }

        private static DataGridViewRow GetRowWithoutBlankCell()
        {
            DataGridViewRow dataGridViewRow = new DataGridViewRow();
            dataGridViewRow.Cells.Add(new DataGridViewTextBoxCell { Value = "Account" });
            dataGridViewRow.Cells.Add(new DataGridViewTextBoxCell { Value = "accountrelated" });
            dataGridViewRow.Cells.Add(new DataGridViewTextBoxCell { Value = "accountid" });
            return dataGridViewRow;
        }

        private static DataGridViewRow GetRowWithBlankCell()
        {
            DataGridViewRow dataGridViewRow = new DataGridViewRow();
            dataGridViewRow.Cells.Add(new DataGridViewTextBoxCell { Value = "Account" });
            dataGridViewRow.Cells.Add(new DataGridViewTextBoxCell { Value = "" });
            dataGridViewRow.Cells.Add(new DataGridViewTextBoxCell { Value = "accountid" });
            return dataGridViewRow;
        }



    }
}