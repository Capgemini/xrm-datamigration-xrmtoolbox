using System;
using XrmToolBox.Extensibility;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Core
{
    public static class SettingFileHandler
    {
        public static bool GetConfigData<T>(out Settings config)
        {
            var allok = SettingsManager.Instance.TryLoad(typeof(T), out config);

            if (config == null)
            {
                config = new Settings();
            }

            return allok;
        }
    }
}