using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActorConsole.Core.Misc
{
    public static class KillStreak
    {
        public static void Give(string killstreak)
        {
            Memory.IW4.SendDvar($"mvm_killstreak {killstreak}");
        }
    }

}
