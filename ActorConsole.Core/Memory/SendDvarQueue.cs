using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
        /// <summary>
        /// The amount of time to wait before sending another
        /// </summary>
        public static readonly int WaitTime = 1000;

        private static readonly List<string> Queue = new List<string>();

        /// <summary>
        /// If <see cref="SendDvarQueue"/> is currently running (<see cref="Count"/> > 0)
        /// </summary>
        private static Task LastTask;

        /// <summary>
        /// The number of dvars in the queue.
        /// </summary>
        public static int Count => Queue.Count;

        internal static void Add(params string[] dvars)
        {
            Queue.Add(ConcatDvars(dvars));
            if (LastTask == null || LastTask.Status != TaskStatus.Running)
            {
                LastTask = Task.Run(Work);
            }
        }

        private static string ConcatDvars(params string[] dvars)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string dvar in dvars)
            {
                sb.Append(dvar);
                sb.Append(';');
            }
            return sb.ToString();
        }

        private static void ConcatQueue()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Queue.Count; i++)
            {
                if (!Queue[i].EndsWith(";"))
                {
                    Queue[i] = Queue[i] + ";";
                }
                sb.Append(Queue[i]);
                Queue.RemoveAt(i);
            }
            Queue.Add(sb.ToString());
        }

        private static void Work()
        {
            while (Count > 0)
            {
                ConcatQueue();
                string dvar = Queue.First();
                AnotherExternalMemoryLibrary.RemoteProcedureCall.Callx86(IW4.Game.Handle, Addresses.Cbuf_AddText, 0u, 0, dvar);
                Debug.WriteLine(dvar);
                Queue.Remove(dvar);
                Thread.Sleep(WaitTime);
            }
        }
    }
}
