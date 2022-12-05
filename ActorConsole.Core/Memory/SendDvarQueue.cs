using AnotherExternalMemoryLibrary.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ActorConsole.Core.Memory
{
    public static class SendDvarQueue
    {
        private static readonly List<string> Queue = new List<string>();
        public static int Count => Queue.Count;
        internal static bool IsRunning { get; private set; }
        private static readonly int WaitTime = 1100;
        internal static void Add(string input)
        {
            Queue.Add(input);
            if (!IsRunning)
                Task.Run(MainLoop);
        }
        private static void MainLoop()
        {
            IsRunning = true;
            while (Queue.Count > 0)
            {
                string dvar = Queue.First();
                IW4.Game.Call(Addresses.Cbuf_AddText, 0, dvar);
                Queue.Remove(dvar);
                Thread.Sleep(WaitTime);
            }
            IsRunning = false;
        }
    }
}
