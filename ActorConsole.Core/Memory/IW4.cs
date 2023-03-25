using ActorConsole.Core.Utils;
using System;
using System.Diagnostics;
using System.Linq;

namespace ActorConsole.Core.Memory
{
    public static class IW4
    {
        public static QueuedCooldown<string> DvarQueue = new QueuedCooldown<string>(new TimeSpan(0, 0, 0, 1, 0));

        static IW4()
        {
            DvarQueue.OnInvoke += DvarQueue_OnInvoke;
        }

        public static Process Game => Process.GetProcesses().FirstOrDefault(p => p.ProcessName == "iw4m" || p.ProcessName == "iw4x");
        public static bool IsRunning => Game != null;

        public static void Send( params string[] dvars )
        {
            foreach ( string dvar in dvars )
            {
                DvarQueue.Invoke(dvar);
            }
            //TODO remove reallocing whole rpc instructions every call. and just realloc args
        }

        private static void DvarQueue_OnInvoke( object sender, string dvar )
        {
            Console.WriteLine(dvar);
            AnotherExternalMemoryLibrary.RemoteProcedureCall.Callx86(IW4.Game.Handle, Addresses.Cbuf_AddText, 0u, 0, dvar);
        }
    }
}
