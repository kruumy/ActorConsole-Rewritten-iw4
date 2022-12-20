using ActorConsole.Core.Player;
using System.IO;

namespace ActorConsole.GUI.Classes.Settings
{
    public class Settings
    {
        private readonly IniFile ini = new IniFile();
        private readonly string FileName = "settings.ini";
        public Precache Precache { get; private set; } = null;

        public string PrecachePath
        {
            get => Precache?.Path;
            set
            {
                if (File.Exists(value))
                {
                    Precache = new Core.Player.Precache(value);
                    ini["options"]["PrecachePath"] = value;
                    ini.Save(FileName);
                }
            }
        }

        public bool DarkMode
        {
            get => ini["options"]["DarkMode"].ToBool();
            set
            {
                ini["options"]["DarkMode"] = value;
                ini.Save(FileName);
            }
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
            string precachePath = ini["options"]["PrecachePath"].GetString();
            if (File.Exists(precachePath))
                PrecachePath = precachePath;
        }

        private void CreateDefault()
        {
            ini["options"]["PrecachePath"] = "";
            ini["options"]["DarkMode"] = true;
            ini.Save(FileName);
        }
    }
}