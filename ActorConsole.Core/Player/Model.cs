using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActorConsole.Core.Player
{
    public static class Model
    {
        public static void SetModel(string model, string team, string name)
        {
            Memory.IW4.SendDvar($"mvm_bot_model {name} {model} {team}");
        }
    }
}
