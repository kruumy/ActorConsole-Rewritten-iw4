﻿namespace ActorConsole.GUI.Classes
{
    internal static class Startup
    {
        /// <summary>
        /// Called when MainWindow.cs is Loaded
        /// </summary>
        public static void Execute()
        {
            Objects.Settings = new Core.Misc.Settings.Settings();
            // TODO: Check for guns.json and models.json
        }
        /// <summary>
        /// Called every second when statusbar mainwindow is updated
        /// </summary>
        public static void StatusBar_Update()
        {
            if (!Core.Memory.IW4.IsInGame)
            {
                Core.Actor.Manager.ResetActorManager();
            }

        }
    }
}
