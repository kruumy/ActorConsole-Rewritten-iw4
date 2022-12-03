using System.Text.Json;

namespace ActorConsole.Core.Json
{
    public static class GunsWrapper
    {
        public static JsonElement RootElement = JsonDocument.Parse(File.ReadAllText("Json/guns.json")).RootElement;
    }
}
