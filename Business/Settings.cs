using System.IO;
using Newtonsoft.Json;
using ColorClippy.DataObjects;

namespace ColorClippy.Business
{
    public sealed partial class Settings
    {
        private static SettingsObject defaultInstance;
        private static readonly string JsonFile = Path.Combine(Helper.GetBaseSaveDirectory(), "Settings.json");

        public static SettingsObject Default
        {
            get
            {
                if (defaultInstance == null)
                    defaultInstance = LoadSettings();
                return defaultInstance;
            }
        }

        private static SettingsObject LoadSettings()
        {
            SettingsObject settings = null;
            if (File.Exists(JsonFile))
                settings = JsonConvert.DeserializeObject<SettingsObject>(File.ReadAllText(JsonFile));
            if(settings == null)
                settings = new SettingsObject(true);
            return settings;
        }

        public static void Save()
        {
            File.WriteAllText(JsonFile, JsonConvert.SerializeObject(Default, Formatting.Indented));
        }
    }
}
