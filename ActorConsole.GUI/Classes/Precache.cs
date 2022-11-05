using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActorConsole.GUI.Classes
{
    internal static class Precache
    {
        public static Core.Precache? _Precache;
        public static void FromPrecachePath(string path_to_precache)
        {
            _Precache = new Core.Precache(path_to_precache);
        }

    }
}
