using ActorConsole.Core.Json.TinyJson;
using System.Collections.Generic;
using System.IO;

namespace ActorConsole.Core.Json
{
    /// <summary>
    /// Wrapper for the weapons.json file.
    /// </summary>
    public static class WeaponsWrapper
    {
        /// <summary>
        /// The root element.
        /// </summary>
        public static Dictionary<string, Dictionary<string, Dictionary<string, string[]>>> RootElement = File.ReadAllText("Json/weapons.json").FromJson<Dictionary<string, Dictionary<string, Dictionary<string, string[]>>>>();
    }
}