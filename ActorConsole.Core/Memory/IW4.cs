namespace ActorConsole.Core.Memory
{
    public static class IW4
    {
        public static bool IsRunning
        {
            get
            {
                return false;
            }
        }
        public static bool InGame
        {
            get
            {
                return false;
            }
        }
        public static string Map
        {
            get
            {
                return "";
            }
        }


        public static void SendDvar(string text)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("[Core.Memory.IW4.SendDvar()] => "+ text);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
