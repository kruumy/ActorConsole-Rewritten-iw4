using System.Text.Json;

namespace ActorConsole.Core.Misc
{
    public static class ModelsJsonWrapper
    {
        public static string RawText = File.ReadAllText("Misc/models.json");
        public static JsonElement RootElement = JsonDocument.Parse(RawText).RootElement;

        public static JsonElement Get(string Map, ModelType modelType)
        {
            switch (modelType)
            {
                case ModelType.Head:
                    {
                        return RootElement.GetProperty("maps").GetProperty(Map.ToLower()).GetProperty("head");
                    }
                case ModelType.Body:
                    {
                        return RootElement.GetProperty("maps").GetProperty(Map.ToLower()).GetProperty("body");
                    }
                default:
                    {
                        throw new Exception("Invalid Property in ModelsJsonWrapper");
                    }
            }
        }
        public static string[]? Get(ModelType modelType)
        {
            string? map = Core.Memory.IW4.Map;
            if (map == null)
            {
                return null;
            }

            if (map.StartsWith("mp_"))
                map = map.Replace("mp_", string.Empty);

            return (string[])Get(map, modelType).Deserialize(Array.Empty<string>().GetType());
        }
    }
    public enum ModelType
    {
        Head,
        Body
    }
}
