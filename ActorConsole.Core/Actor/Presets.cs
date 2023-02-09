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
            // TODO: handle for incorrect file pass thru
            Dictionary<string, Dictionary<string, string>> ActorJson = File.ReadAllText(path).FromJson<Dictionary<string, Dictionary<string, string>>>();


            // TODO: use reflection instead to set properties
            Manager.Add();
            Actor actor = Manager.Actors.Last();
            actor.Anims.Idle = ActorJson[nameof(Anims)][nameof(Anims.Idle)];
            actor.Anims.Death = ActorJson[nameof(Anims)][nameof(Anims.Death)];
            actor.Models.Head = ActorJson[nameof(Models)][nameof(Models.Head)];
            actor.Models.Body = ActorJson[nameof(Models)][nameof(Models.Body)];
            actor.Weapons.j_gun = ActorJson[nameof(Weapons)][nameof(Weapons.j_gun)];
            actor.Weapons.tag_inhand = ActorJson[nameof(Weapons)][nameof(Weapons.tag_inhand)];
            actor.Weapons.tag_stowed_back = ActorJson[nameof(Weapons)][nameof(Weapons.tag_stowed_back)];
            actor.Weapons.tag_stowed_hip_rear = ActorJson[nameof(Weapons)][nameof(Weapons.tag_stowed_hip_rear)];
            actor.Weapons.tag_weapon_chest = ActorJson[nameof(Weapons)][nameof(Weapons.tag_weapon_chest)];
            actor.Weapons.tag_weapon_left = ActorJson[nameof(Weapons)][nameof(Weapons.tag_weapon_left)];
            actor.Weapons.tag_weapon_right = ActorJson[nameof(Weapons)][nameof(Weapons.tag_weapon_right)];
        }
    }
}