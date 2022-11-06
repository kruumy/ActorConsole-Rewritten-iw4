using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActorConsole.GUI.Classes.Settings
{
    internal class Settings
    {
        private IniFile ini = new IniFile();
        private string FileName = "settings.ini";
        public string? Path_To_Precache
        {
            get
            {
                if (Objects.Precache != null)
                    return Objects.Precache.Path;
                else
                    return null;
            }
            set
            {
                Objects.Precache = new Core.Precache(value);
                ini["options"]["Path_To_Precache"] = value;
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
