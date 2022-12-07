using ActorConsole.Core.Player;
using System.IO;

namespace ActorConsole.Core.Misc.Settings
{
    public class Settings
    {
        private readonly IniFile ini = new IniFile();
        private readonly string FileName = "settings.ini";
        public Precache Precache = null;

        public string PathToPrecache
        {
            get
            {
                if (Precache != null)
                    return Precache.Path;
                else
                    return null;
            }
            set
            {
                if (value != null)
                {
                    Precache = new Core.Player.Precache(value);
                    ini["options"]["PathToPrecache"] = value;
                    ini.Save(FileName);
                }
            }
        }

        public bool IsPrecacheSelected => !string.IsNullOrEmpty(PathToPrecache);

        public bool DarkMode
        {
            get => ini["options"]["DarkMode"].ToBool();
            set => ini["options"]["DarkMode"] = value;
        }

        /// <summary>
        /// Loads from settings.ini file or creates one if not.
        /// </summary>
        public Settings()
        {
            if (!File.Exists(FileName))
                CreateDefault();
            Load();
        }

        private void Load()
        {
            ini.Load(FileName);
            string precachePath = ini["options"]["PathToPrecache"].GetString();
            if (File.Exists(precachePath))
                PathToPrecache = precachePath;
        }

        private void CreateDefault()
        {
            ini["options"]["PathToPrecache"] = "";
            ini["options"]["DarkMode"] = true;
            ini.Save(FileName);
        }
    }
}