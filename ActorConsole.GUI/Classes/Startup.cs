using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActorConsole.GUI.Classes
{
    internal static class Startup
    {
        /// <summary>
        /// Called when MainWindow.cs is Loaded
        /// </summary>
        public static void Execute()
        {
            Objects.Settings = new Settings.Settings();
        }
    }
}
