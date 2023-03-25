using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ActorConsole.Core.Memory
{
    public static class IW4
    {
        public static Process Game => Process.GetProcesses().FirstOrDefault(p => p.ProcessName == "iw4m" || p.ProcessName == "iw4x");
        public static bool IsRunning => Game != null;

        public static void Send( params string[] dvars )
        {
            string dvar = ConcatDvars(dvars);
            Console.WriteLine(dvar);
            AnotherExternalMemoryLibrary.RemoteProcedureCall.Callx86(Game.Handle, Addresses.Cbuf_AddText, 0u, 0, dvar);
            //TODO have always running class so no need to realloc whole rpc instructions every call.
        }

        private static string ConcatDvars( IEnumerable<string> dvars )
        {
            StringBuilder sb = new StringBuilder();
            foreach ( string dvar in dvars )
            {
                sb.Append(dvar);
                sb.Append(';');
            }
            return sb.ToString();
        }
    }
}
