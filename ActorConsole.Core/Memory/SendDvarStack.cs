using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace ActorConsole.Core.Memory
{
    internal static class SendDvarStack
    {
        public static readonly Stack<string> Stack = new Stack<string>();

        private static readonly Timer Timer = new Timer(600d);

        static SendDvarStack()
        {
            Timer.Elapsed += Timer_Elapsed;
            Timer.Start();
        }

        private static void ConcatStack()
        {
            StringBuilder sb = new StringBuilder();
            foreach ( string dvar in Stack )
            {
                sb.Append(dvar);
                sb.Append(';');
            }
            if ( sb.Length > 0 )
            {
                Stack.Clear();
                Stack.Push(sb.ToString());
            }
        }

        private static void Timer_Elapsed( object sender, ElapsedEventArgs e )
        {
            if ( IW4.IsRunning && Stack.Count > 0 )
            {
                //ConcatStack(); //TODO dont concat same dvars
                string dvar = Stack.Pop();
                Console.WriteLine(dvar);
                AnotherExternalMemoryLibrary.RemoteProcedureCall.Callx86(IW4.Game.Handle, Addresses.Cbuf_AddText, 0u, 0, dvar);
            }
        }
    }
}
