using Capgemini.Xrm.CdsDataMigratorLibrary.Enums;
using System;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Extensions
{
    public static class FormControlExtensions
    {
        public static void PopulateComboBoxLogLevel(this System.Windows.Forms.ComboBox comboBoxLogLevel)
        {
            comboBoxLogLevel.Items.Clear();

            foreach (var item in Enum.GetValues(typeof(LogLevel)))
            {
                comboBoxLogLevel.Items.Add(item);
            }

            comboBoxLogLevel.SelectedItem = LogLevel.Info;
        }
    }
}