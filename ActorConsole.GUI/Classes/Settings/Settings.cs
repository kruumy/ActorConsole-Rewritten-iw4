using ActorConsole.Core.Player;
using System.IO;

namespace ActorConsole.Core.Misc.Settings
{
    public class Settings
    {
        private readonly IniFile ini = new IniFile();
        private readonly string FileName = "settings.ini";
        public Precache Precache = null;
        public string Path_To_Precache
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
                    ini["options"]["Path_To_Precache"] = value;
                    ini.Save(FileName);
                }
            }
        }
        public bool IsPrecacheSelected => !string.IsNullOrEmpty(Path_To_Precache);
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
            string precachePath = ini["options"]["Path_To_Precache"].GetString();
            if (File.Exists(precachePath))
                Path_To_Precache = precachePath;
        }
        private void CreateDefault()
        {
            ini["options"]["Path_To_Precache"] = "";
            ini.Save(FileName);
        }
    }
}
