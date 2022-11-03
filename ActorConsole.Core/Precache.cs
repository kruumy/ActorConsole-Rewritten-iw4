using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActorConsole.Core
{
    public class Precache
    {
        string RawText { get; set; }

        public Precache(string raw)
        {
            RawText = raw;
        }
    }
}
