using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActorConsole.GUI.Classes
{
    internal static class Objects
    {
        public static Core.Precache? Precache { get { return Settings.Precache; } }
        public static Core.Misc.Settings.Settings? Settings;
    }
}
