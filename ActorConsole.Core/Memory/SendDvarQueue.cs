using AnotherExternalMemoryLibrary.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ActorConsole.Core.Memory
{
    /// <summary>
    /// Is used to limit the amount of times Cbuf_AddText is called.
    /// This is a fix to one of the most annoying bugs in the old actorconsole where the game would crash because of error when creating too many threads to call the function to fast.
    /// Dvars are added into the queue by invoking Memeory.IW4.SendDvar();
    /// </summary>
    public static class SendDvarQueue
    {
        private static readonly List<string> Queue = new List<string>();

        /// <summary>
        /// The number of dvars in the queue.
        /// </summary>
        public static int Count => Queue.Count;

        /// <summary>
        /// If <see cref="SendDvarQueue"/> is currently running (<see cref="Count"/> > 0)
        /// </summary>

        /// <summary>
        /// The amount of time to wait before sending another
        /// </summary>
        public static int WaitTime = 1100;

        private static Task LastTask;

        internal static void Add(string input)
        {
            Queue.Add(input);
            if (LastTask == null || LastTask.Status != TaskStatus.Running)
            {
                LastTask = Task.Run(MainWork);
            }
        }

        private static void MainWork()
        {
            while (Count > 0)
            {
                string dvar = Queue.First();
                IW4.Game.Call(Addresses.Cbuf_AddText, 0, dvar);
                Queue.Remove(dvar);
                Thread.Sleep(WaitTime);
            }
        }
    }
}