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
            Objects.Settings = new Core.Misc.Settings.Settings();
        }
        /// <summary>
        /// Called every second when statusbar mainwindow is updated
        /// </summary>
        public static void StatusBar_Update()
        {
            if (!Core.Memory.IW4.InGame)
            {
                Core.ActorManager.ResetActorManager();
            }
        }
    }
}
