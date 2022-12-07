using System;

namespace ActorConsole.GUI.Classes
{
    internal static class MetroColorTheme
    {
        public static string GetRandomColorTheme()
        {
            switch (new Random().Next(0, 11))
            {
                case 0:
                    return "Red";

                case 1:
                    return "Green";

                case 2:
                    return "Teal";

                case 3:
                    return "Cyan";

                case 4:
                    return "Purple";

                case 5:
                    return "Orange";

                case 6:
                    return "Magenta";

                case 7:
                    return "Violet";

                case 8:
                    return "Brown";

                case 9:
                    return "Pink";

                case 10:
                    return "Lime";

                case 11:
                    return "Olive";

                default:
                    return "Orange";
            }
        }
    }
}