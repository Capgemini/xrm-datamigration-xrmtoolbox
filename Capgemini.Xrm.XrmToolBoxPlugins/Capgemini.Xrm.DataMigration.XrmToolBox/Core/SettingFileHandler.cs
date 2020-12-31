using System;
using XrmToolBox.Extensibility;

namespace Capgemini.Xrm.DataMigration.XrmToolBoxPlugin.Core
{
    public static class SettingFileHandler
    {
        public static bool GetConfigData(out Settings config)
        {
            var allok = SettingsManager.Instance.TryLoad(typeof(SchemaGenerator), out config);

            if (config == null)
            {
                config = new Settings();
            }

            return allok;
        }

        public static bool SaveConfigData(Settings config)
        {
            try
            {
                SettingsManager.Instance.Save(typeof(SchemaGenerator), config);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}