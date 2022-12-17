using System.IO;
using System.Text.Json;

namespace ActorConsole.Core.Json
{
    /// <summary>
    /// Wrapper for the guns.json file.
    /// </summary>
    public static class GunsWrapper
    {
        /// <summary>
        /// The root element.
        /// </summary>
        public static JsonElement RootElement = JsonDocument.Parse(File.ReadAllText("Json/guns.json")).RootElement;
    }
}