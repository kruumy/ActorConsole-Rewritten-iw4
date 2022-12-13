using AnotherExternalMemoryLibrary.Extensions;
using System.Diagnostics;

namespace ActorConsole.Core.Memory
{
    public static class IW4
    {
        public static Process Game
        {
            get
            {
                Process[] ps = Process.GetProcesses();
                foreach (Process p in ps)
                {
                    if (p.ProcessName == "iw4m" || p.ProcessName == "iw4x")
                    {
                        return p;
                    }
                }
                return null;
            }
        }

        public static bool IsRunning => Game != null;

        public static bool IsInGame
        {
            get
            {
                if (IsRunning)
                {
                    try
                    {
                        return Game.Read<int>(Addresses.IsInGame) != 0;
                    }
                    catch
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        public static string Map
        {
            get
            {
                try
                {
                    if (IsInGame)
                    {
                        string map = Game.Read<byte>(Addresses.MapName, 15).GetString(true);
                        if (map != string.Empty)
                            return map;
                    }
                    return null;
                }
                catch { return null; }
            }
        }

        public static string PlayerName
        {
            get
            {
                try
                {
                    if (IsRunning)
                    {
                        string name = Game.Read<byte>(Addresses.PlayerName, 20).GetString(true);
                        if (name != string.Empty)
                            return name;
                    }
                    return null;

                }
                catch { return null; }
            }
        }

        public static void SendDvar(string text)
        {
            if (IsRunning)
            {
                if (text.Contains("+"))
                {
                    text = text.Split('+')[0];
                }
                else if (text.Contains("-"))
                {
                    text = text.Split('-')[0];
                }
                SendDvarQueue.Add(text);
            }
        }
    }
}