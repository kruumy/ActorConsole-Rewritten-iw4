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
    }
    public enum ModelType
    {
        Head,
        Body
    }
}
