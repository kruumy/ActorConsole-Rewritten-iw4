using System.Diagnostics;
using System.Text;

namespace ActorConsole.Core.Memory
{
    public static class IW4
    {
        private static Process? Game
        {
            get
            {
                Process[] ps = Process.GetProcesses();
                foreach (var p in ps)
                {
                    if (p.ProcessName == "iw4m" || p.ProcessName == "iw4x")
                    {
                        return p;
                    }
                }
                return null;

            }
        }
        public static bool IsRunning
        {
            get
            {
                if (Game != null)
                    return true;
                else
                    return false;
            }
        }
        public static bool InGame
        {
            get
            {
                if (mem.GetValue<int>((PointerEx)Addresses.KeyValuePairs["InGame"]) == 0)
                    return false;
                else
                    return true;
            }
        }
        public static string Map
        {
            get
            {
                return mem.GetString((PointerEx)Addresses.KeyValuePairs["Map"], 15).Trim();
            }
        }

        public static ProcessEx mem = new ProcessEx(Game, true);

        public static void SendDvar(string text)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("[Core.Memory.IW4.SendDvar()] => " + text);
            Console.ForegroundColor = ConsoleColor.White;
            // TODO: uncomment, commented out just for testing so it does not crash while not in game
            //ExternalConsole.Send(text);
        }
    }
}
