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
            System.Text.Json.JsonSerializerOptions options = new System.Text.Json.JsonSerializerOptions
            {
                WriteIndented = true
            };
            string ActorJson = System.Text.Json.JsonSerializer.Serialize(actor, options);
            File.WriteAllText(path, ActorJson);
        }

        /// <summary>
        /// Loads path to json into the Manager class
        /// </summary>
        public static void Load(string path)
        {
            // TODO: handle for incorrect file pass thru
            string RawJson = File.ReadAllText(path);
            System.Text.Json.JsonElement ActorJson = System.Text.Json.JsonDocument.Parse(RawJson).RootElement;

            Manager.Add();
            Actor actor = Manager.Actors.Last();
            actor.Anims.Idle = ActorJson.GetProperty("Anims").GetProperty("Idle").GetString();
            actor.Anims.Death = ActorJson.GetProperty("Anims").GetProperty("Death").GetString();
            actor.Models.Head = ActorJson.GetProperty("Models").GetProperty("Head").GetString();
            actor.Models.Body = ActorJson.GetProperty("Models").GetProperty("Body").GetString();
            actor.Weapons.j_gun = ActorJson.GetProperty("Weapons").GetProperty("j_gun").GetString();
            actor.Weapons.tag_inhand = ActorJson.GetProperty("Weapons").GetProperty("tag_inhand").GetString();
            actor.Weapons.tag_stowed_back = ActorJson.GetProperty("Weapons").GetProperty("tag_stowed_back").GetString();
            actor.Weapons.tag_stowed_hip_rear = ActorJson.GetProperty("Weapons").GetProperty("tag_stowed_hip_rear").GetString();
            actor.Weapons.tag_weapon_chest = ActorJson.GetProperty("Weapons").GetProperty("tag_weapon_chest").GetString();
            actor.Weapons.tag_weapon_left = ActorJson.GetProperty("Weapons").GetProperty("tag_weapon_left").GetString();
            actor.Weapons.tag_weapon_right = ActorJson.GetProperty("Weapons").GetProperty("tag_weapon_right").GetString();
        }
    }
}