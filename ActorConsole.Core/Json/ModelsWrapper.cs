using System;
using System.IO;
using System.Text.Json;

namespace ActorConsole.Core.Json
{
    public static class ModelsWrapper
    {
        private static JsonElement RootElement = JsonDocument.Parse(File.ReadAllText("Json/models.json")).RootElement;

        public static string[] Get(string Map, ModelType modelType)
        {
            switch (modelType)
            {
                case ModelType.Head:
                    return (string[])RootElement.GetProperty("maps").GetProperty(Map.ToLower()).GetProperty("head").Deserialize(Array.Empty<string>().GetType());

                case ModelType.Body:
                    return (string[])RootElement.GetProperty("maps").GetProperty(Map.ToLower()).GetProperty("body").Deserialize(Array.Empty<string>().GetType());

                default:
                    throw new Exception("Invalid Property in ModelsJsonWrapper");
            }
        }

        public static string[] GetByCurrentMap(ModelType modelType)
        {
            string map = Memory.IW4.Map;
            if (map == null) return null;

            if (map.StartsWith("mp_"))
                map = map.Replace("mp_", string.Empty);

            return Get(map, modelType);
        }
    }

    public enum ModelType
    {
        Head,
        Body
    }
}