using ActorConsole.Core.Actor.Attributes;
using ActorConsole.Core.Json.TinyJson;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ActorConsole.Core.Actor
{
    /// <summary>
    /// Class containing functions to save actors as json and load them into the manager.
    /// </summary>
    public static class Presets
    {
        /// <summary>
        /// Saves json of given actor object to the path given.
        /// </summary>
        public static void Save(Actor actor, string path)
        {
            File.WriteAllText(path, actor.ToJson());
        }

        /// <summary>
        /// Loads path to json into the Manager class
        /// </summary>
        public static void Load(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException(path);
            }
            Dictionary<string, Dictionary<string, string>> ActorJson = File.ReadAllText(path).FromJson<Dictionary<string, Dictionary<string, string>>>();

            Manager.Add();
            Actor actor = Manager.Actors.Last();
            ApplyProperties(actor.Anims, ActorJson[nameof(Anims)]);
            ApplyProperties(actor.Models, ActorJson[nameof(Models)]);
            ApplyProperties(actor.Weapons, ActorJson[nameof(Weapons)]);
        }

        private static void ApplyProperties<T>(T targetActorthing, Dictionary<string, string> propName_propValue) where T : Attribute
        {
            foreach (KeyValuePair<string, string> prop in propName_propValue)
            {
                System.Reflection.PropertyInfo p = typeof(T).GetProperty(prop.Key, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
                p.SetValue(targetActorthing, prop.Value);
            }
        }
    }
}