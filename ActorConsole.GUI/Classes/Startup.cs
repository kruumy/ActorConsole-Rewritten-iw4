namespace ActorConsole.GUI.Classes
{
    internal static class Startup
    {
        /// <summary>
        /// Called when MainWindow.cs is Loaded
        /// </summary>
        public static void Execute()
        {
            // TODO: Check for guns.json and models.json
        }

        /// <summary>
        /// Called every second when statusbar mainwindow is updated
        /// </summary>
        public static void StatusBar_Update()
        {
            if (!Core.Memory.IW4.IsInMatch)
            {
                Core.Actor.Manager.Reset();
                Views.ActorBar.Reset();
            }
        }
    }
}