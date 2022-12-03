using System.Text.Json;

namespace ActorConsole.Core.Json
{
    public static class GunsWrapper
    {
        public static JsonElement RootElement = JsonDocument.Parse(File.ReadAllText("Json/guns.json")).RootElement;
        public static string[] Camos => (string[])RootElement.GetProperty("camos").Deserialize(Array.Empty<string>().GetType());
    }
}
