using Capgemini.Xrm.CdsDataMigratorLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Extensions
{
    public static class FormControlExtensions
    {
        public static void PopulateComboBoxLogLevel(this System.Windows.Forms.ComboBox comboBoxLogLevel)
        {
            var list = new List<LogLevel>();
            foreach (var item in Enum.GetValues(typeof(LogLevel)))
            {
                list.Add((LogLevel)item);
            }
            comboBoxLogLevel.DataSource = list;
            comboBoxLogLevel.SelectedItem = list.First(x => x == LogLevel.Info);
        }
    }
}