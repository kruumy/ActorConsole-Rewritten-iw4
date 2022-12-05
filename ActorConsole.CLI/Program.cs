﻿using ActorConsole.Core.Player;
using NConsoleMenu;

namespace ActorConsole.CLI
{
    internal class Program
    {
        /// <summary>
        /// Used for testing.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            CMenu menu = new CMenu();

            #region actor commands
            int selectedActor = -1;
            CMenuItem actorItem = menu.Add("actor", "actor <property> => Actor options.");
            actorItem.Add("add", x => { Core.Manager.Add(); selectedActor = Core.Manager.Search(Core.Manager.Actors.Last().Name); }, "actor add => Spawns an actor to your cursor and selects it.");
            actorItem.Add("list", x => print(Core.Manager.Actors), "actor list => Dumps the main actor list.");

            actorItem.Add("select", name => selectedActor = Core.Manager.Search(name), "actor select <name> => Selects an actor for changing its properties.");
            actorItem.Add("delete", x => { Core.Manager.Delete(selectedActor); selectedActor = -1; }, "actor delete => Deletes the selected actor.");

            CMenuItem animsActorItem = actorItem.Add("anims", "actor anims <type> <anim> => Change selected actor's anims.");
            animsActorItem.Add("idle", anim => Core.Manager.Actors[selectedActor].Anims.Idle = anim, "actor anims idle <anim> => Change selected actor's idle anim.");
            animsActorItem.Add("death", anim => Core.Manager.Actors[selectedActor].Anims.Death = anim, "actor anims death <anim> => Change selected actor's death anim.");

            CMenuItem modelsActorItem = actorItem.Add("models", "actor models <type> <model> => Change selected actor's models.");
            modelsActorItem.Add("head", model => Core.Manager.Actors[selectedActor].Models.Head = model, "actor models head <model> => Change selected actor's head model.");
            modelsActorItem.Add("body", model => Core.Manager.Actors[selectedActor].Models.Body = model, "actor models body <model> => Change selected actor's body model.");

            CMenuItem weaponsActorItem = actorItem.Add("weapons", "actor weapons <bone> <gun> => Change selected actor's weapons.");
            weaponsActorItem.Add("j_gun", gun => Core.Manager.Actors[selectedActor].Weapons.j_gun = gun, "actor weapons j_gun <gun> => Change selected actor's j_gun weapon.");

            CMenuItem movementActorItem = actorItem.Add("movement", "actor movement <type> => Change selected actor's movement options.");
            movementActorItem.Add("walking", options =>
            {
                string[] optionsArr = options.Split(' ');
                Core.Manager.Actors[selectedActor].Movement_Walking.Key = Convert.ToChar(optionsArr[0]);
                Core.Manager.Actors[selectedActor].Movement_Walking.Speed = Convert.ToInt32(optionsArr[1]);
            },
            "actor walking <key> <speed> => Change selected actor's walking options.");

            CMenuItem pathingMovementActorItem = movementActorItem.Add("pathing", "actor movement pathing <option> => Change selected actor's pathing options.");
            pathingMovementActorItem.Add("speed", number => Core.Manager.Actors[selectedActor].Movement_Pathing.Speed = Convert.ToInt32(number), "actor movement pathing speed <value> => Change selected actor's pathing speed.");

            CMenuItem nodePathingMovementActorItem = pathingMovementActorItem.Add("node", "actor movement pathing node <action> => Add or delete selected actor's pathing nodes.");
            nodePathingMovementActorItem.Add("add", x => Core.Manager.Actors[selectedActor].Movement_Pathing.CreateNode(), "actor movement pathing node add => Add selected actor pathing node. Max 13");
            nodePathingMovementActorItem.Add("delete", x => Core.Manager.Actors[selectedActor].Movement_Pathing.DeleteLastNode(), "actor movement pathing node delete => delete selected actor last pathing node.");
            #endregion
            #region precache commands

            CMenuItem precacheItem = menu.Add("precache", "precache <type> => Prints precache.");
            Precache? precache = null;
            precacheItem.Add("set", path => precache = new Core.Precache(path));
            CMenuItem listPrecacheItem = precacheItem.Add("list");
            listPrecacheItem.Add("all", x => print(precache));
            #endregion

            menu.Add("send", command => Core.Memory.IW4.SendDvar(command), "send <dvar> => Sends dvar to the game.");

            menu.Run();
        }

        public static void print(dynamic input, bool endl = true)
        {
            if (input.GetType() == typeof(string))
                Console.Write(input.ToString());
            else
            {
                System.Text.Json.JsonSerializerOptions options = new System.Text.Json.JsonSerializerOptions();
                options.WriteIndented = true;
                options.MaxDepth = int.MaxValue;
                options.IncludeFields = true;
                Console.Write(System.Text.Json.JsonSerializer.Serialize(input, input.GetType(), options));
            }
            if (endl)
                Console.WriteLine();
        }
    }
}