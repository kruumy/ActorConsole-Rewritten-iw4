using ActorConsole.Core.Json.TinyJson;
using System;
using System.Collections.Generic;
using System.IO;

namespace ActorConsole.Core.Json
{
    public static class Models
    {
        public static Dictionary<string, Dictionary<string, Map>> Element => File.ReadAllText("Json/models.json").FromJson<Dictionary<string, Dictionary<string, Dictionary<string, Map>>>>()[ "maps" ];

        public struct Map
        {
            public static Map Empty = new Map()
            {
                Head = Array.Empty<string>(),
                Body = Array.Empty<string>(),
            };

            public string[] Body { get; set; }
            public string[] Head { get; set; }
        }
    }
}
