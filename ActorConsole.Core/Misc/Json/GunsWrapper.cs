using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ActorConsole.Core.Misc.Json
{
    public static class GunsWrapper
    {
        public static string[] Camos
        {
            get
            {
                return (string[])JsonDocument.Parse(File.ReadAllText("Misc/guns.json"))
                    .RootElement.GetProperty("camos")
                    .Deserialize(Array.Empty<string>().GetType());
            }
        }

        //TODO: finish this






    }
}
