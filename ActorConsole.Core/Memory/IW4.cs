using AnotherExternalMemoryLibrary.Extensions;
using System.Diagnostics;

namespace ActorConsole.Core.Memory
{
    /// <summary>
    /// The class to read information in the memory of the IW4 game.
    /// </summary>
    public static class IW4
    {
        /// <summary>
        /// Gets the game process object and returned null if game is not found.
        /// </summary>
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
        /// <summary>
        /// Determines if the game if running or not.
        /// </summary>
        public static bool IsRunning => Game != null;
        /// <summary>
        /// Determines if the player is in a match or demo.
        /// </summary>
        public static bool IsInMatch
        {
            get
            {
                if (IsRunning)
                {
                    try
                    {
                        return Game.Read<int>(Addresses.IsInMatch) != 0;
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
        /// <summary>
        /// Gets the current map the player is in. Returns null if not in a map or failed.
        /// </summary>
        public static string Map
        {
            get
            {
                try
                {
                    if (IsInMatch)
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
        /// <summary>
        /// Gets the players name. Returns null if failed.
        /// </summary>
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
        /// <summary>
        /// Adds a dvar into the SendDvarQueue class.
        /// </summary>
        /// <param name="text"></param>
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