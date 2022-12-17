using System;
using System.IO;
using System.Text.Json;

namespace ActorConsole.Core.Json
{
    /// <summary>
    /// Wrapper for the models.json file.
    /// </summary>
    public static class ModelsWrapper
    {
        private static readonly JsonElement RootElement = JsonDocument.Parse(File.ReadAllText("Json/models.json")).RootElement;
        /// <summary>
        /// Get all the models in the json.
        /// </summary>
        /// <param name="Map">The map to read from.</param>
        /// <param name="modelType">The model type to read from.</param>
        /// <returns>A string array of all the models of the type an map.</returns>
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
        /// <summary>
        /// Get all the models in the json by the players current map in game.
        /// </summary>
        /// <param name="modelType">The type of model.</param>
        /// <returns>A string array of all the model type. Returns null if map is not found.</returns>
        public static string[] GetByCurrentMap(ModelType modelType)
        {
            string map = Memory.IW4.Map;
            if (map == null) return null;

            if (map.StartsWith("mp_"))
                map = map.Replace("mp_", string.Empty);

            return Get(map, modelType);
        }
    }
    /// <summary>
    /// Enum to easily declare the model type.
    /// </summary>
    public enum ModelType
    {
        /// <summary>
        /// The head model.
        /// </summary>
        Head,
        /// <summary>
        /// The body model.
        /// </summary>
        Body
    }
}