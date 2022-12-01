namespace ActorConsole.Core.Misc.Settings
{
    public class Settings
    {
        private readonly IniFile ini = new();
        private readonly string FileName = "settings.ini";
        public Core.Precache? Precache = null;
        public string? Path_To_Precache
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
                Precache = new Core.Precache(value);
                ini["options"]["Path_To_Precache"] = value;
                ini.Save(FileName);
            }
        }
        public bool IsPrecacheSelected
        {
            get
            {
                if (string.IsNullOrEmpty(Path_To_Precache))
                {
                    return false;
                }
                else
                {
                    return true;
                }
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
