using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ActorConsole.Core.Misc.Json
{
    public static class GunsWrapper
    {
        public static string RawText = File.ReadAllText("Misc/Json/guns.json");
        public static JsonElement RootElement = JsonDocument.Parse(RawText).RootElement;
        public static string[] Camos
        {
            get
            {
                return (string[])RootElement.GetProperty("camos")
                    .Deserialize(Array.Empty<string>().GetType());
            }
        }

    }
}
