using ActorConsole.Core.Player;

namespace ActorConsole.GUI.Classes
{
    internal static class Objects
    {
        public static Precache Precache => Settings.Precache;
        public static Classes.Settings.Settings Settings = new Classes.Settings.Settings();
    }
}