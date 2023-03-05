using ActorConsole.Core.Json.TinyJson;
using System.Collections.Generic;
using System.IO;

namespace ActorConsole.Core.Json
{
    /// <summary>
    /// Wrapper for the models.json file.
    /// </summary>
    public static class ModelsWrapper
    {

        /// <summary>
        /// Get all the models in the json.
        /// </summary>
        /// <param name="Map">The map to read from.</param>
        /// <param name="modelType">The model type to read from.</param>
        /// <returns>A string array of all the models of the type an map.</returns>
        public static string[] Get(string Map, ModelType modelType)
        {
            Dictionary<string, Dictionary<string, Dictionary<string, string[]>>> RootElement = File.ReadAllText("Json/models.json").FromJson<Dictionary<string, Dictionary<string, Dictionary<string, string[]>>>>();
            if (RootElement["maps"].TryGetValue(Map.ToLower(), out Dictionary<string, string[]> map))
            {
                return map[modelType.ToString().ToLower()];
            }
            else
            {
                return null;
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
            return map == null ? null : Get(map, modelType);
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