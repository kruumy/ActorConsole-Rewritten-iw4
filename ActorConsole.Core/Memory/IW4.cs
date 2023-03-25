using System.Diagnostics;
using System.Linq;

namespace ActorConsole.Core.Memory
{
    public static class IW4
    {
        public static Process Game => Process.GetProcesses().FirstOrDefault(p => p.ProcessName == "iw4m" || p.ProcessName == "iw4x");
        public static bool IsRunning => Game != null;

        public static void Send( params string[] dvars )
        {
            foreach ( string dvar in dvars )
            {
                SendDvarStack.Stack.Push(dvar);
            }
            //TODO have always running class so no need to realloc whole rpc instructions every call.
        }
    }
}
