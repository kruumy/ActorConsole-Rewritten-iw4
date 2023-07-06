using ActorConsole.Core.Json.TinyJson;
using System.Collections.Generic;
using System.IO;

namespace ActorConsole.Core.Json
{
    public static class Weapons
    {
        public static Dictionary<string, Dictionary<string, Dictionary<string, string[]>>> Element => File.ReadAllText("Json/weapons.json").FromJson<Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, string[]>>>>>()[ "weapons" ];
    }
}
