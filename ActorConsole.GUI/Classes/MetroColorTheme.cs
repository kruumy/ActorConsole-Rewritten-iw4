namespace ActorConsole.GUI.Classes
{
    internal static class MetroColorTheme
    {

        public static string GetRandomColorTheme()
        {
            return new Random().Next(0, 11) switch
            {
                0 => "Red",
                1 => "Green",
                2 => "Teal",
                3 => "Cyan",
                4 => "Purple",
                5 => "Orange",
                6 => "Magenta",
                7 => "Violet",
                8 => "Brown",
                9 => "Pink",
                10 => "Lime",
                11 => "Olive",
                _ => "Orange",
            };
        }


    }
}
