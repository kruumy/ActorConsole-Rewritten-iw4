using System.Diagnostics;
using System.Text;
using System.Threading;

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
                if (IsRunning)
                {
                    try
                    {
                        if (mem.GetValue<int>((PointerEx)Addresses.KeyValuePairs["InGame"]) == 0)
                            return false;
                        else
                            return true;
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
        public static string? Map
        {
            get
            {
                try
                {
                    string map = mem.GetString((PointerEx)Addresses.KeyValuePairs["Map"], 15).Trim();
                    if (map != string.Empty)
                        return map;
                    else
                        return null;
                }
                catch { return null; }

            }
        }
        public static string? PlayerName
        {
            get
            {
                try
                {
                    string name = mem.GetString((PointerEx)Addresses.KeyValuePairs["PlayerName"]).Trim();
                    if (name != string.Empty)
                        return name;
                    else
                        return null;
                }
                catch { return null; }

            }
        }

        // TODO: handle for game closing and reopening creating a new instance
        private static ProcessEx? _mem = new ProcessEx(Game, true);

        public static ProcessEx mem
        {
            get
            {
                _mem = new ProcessEx(Game, true);
                return _mem;

            }
        }

        public static void SendDvar(string text)
        {
            if (ActorConsole.Core.Memory.IW4.IsRunning)
            {
                if (text.Contains('+'))
                {
                    text = text.Split('+')[0];
                }
                else if (text.Contains('-'))
                {
                    text = text.Split("-")[0];
                }
                SendDvarQueue.Add(text);
            }
        }
    }
}
