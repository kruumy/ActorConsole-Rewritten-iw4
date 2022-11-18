using System.Text.Json;

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
