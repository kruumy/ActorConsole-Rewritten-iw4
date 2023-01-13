using System;

namespace ActorConsole.GUI.Classes
{
    internal static class MetroColorTheme
    {
        public static string GetRandomColorTheme()
        {
            string[] allColors = new string[]
            {
                "Red","Green","Teal",
                "Cyan","Purple","Orange",
                "Magenta","Violet","Brown",
                "Pink","Lime","Olive"
            };
            return allColors[new Random().Next(0, allColors.Length - 1)];
        }
    }
}