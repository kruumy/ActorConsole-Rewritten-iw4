using ActorConsole;
using ActorConsole.Core.Misc;
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
            var menu = new CMenu();

            #region actor commands
            int selectedActor = -1;
            var actorItem = menu.Add("actor", "actor <property> => Actor options.");
            actorItem.Add("add", x => { Core.ActorManager.Add(); selectedActor = Core.ActorManager.Search(Core.ActorManager.Actors.Last().Name); }, "actor add => Spawns an actor to your cursor and selects it.");
            actorItem.Add("list", x => print(Core.ActorManager.Actors), "actor list => Dumps the main actor list.");

            actorItem.Add("select", name => selectedActor = Core.ActorManager.Search(name), "actor select <name> => Selects an actor for changing its properties.");
            actorItem.Add("delete", x => { Core.ActorManager.Delete(selectedActor); selectedActor = -1; }, "actor delete => Deletes the selected actor.");

            var animsActorItem = actorItem.Add("anims", "actor anims <type> <anim> => Change selected actor's anims.");
            animsActorItem.Add("idle", anim => Core.ActorManager.Actors[selectedActor].Anims.Idle = anim, "actor anims idle <anim> => Change selected actor's idle anim.");
            animsActorItem.Add("death", anim => Core.ActorManager.Actors[selectedActor].Anims.Death = anim, "actor anims death <anim> => Change selected actor's death anim.");

            var modelsActorItem = actorItem.Add("models", "actor models <type> <model> => Change selected actor's models.");
            modelsActorItem.Add("head", model => Core.ActorManager.Actors[selectedActor].Models.Head = model, "actor models head <model> => Change selected actor's head model.");
            modelsActorItem.Add("body", model => Core.ActorManager.Actors[selectedActor].Models.Body = model, "actor models body <model> => Change selected actor's body model.");

            var weaponsActorItem = actorItem.Add("weapons", "actor weapons <bone> <gun> => Change selected actor's weapons.");
            weaponsActorItem.Add("j_gun", gun => Core.ActorManager.Actors[selectedActor].Weapons.j_gun = gun, "actor weapons j_gun <gun> => Change selected actor's j_gun weapon.");

            var movementActorItem = actorItem.Add("movement", "actor movement <type> => Change selected actor's movement options.");
            movementActorItem.Add("walking", options =>
            {
                string[] optionsArr = options.Split(' ');
                Core.ActorManager.Actors[selectedActor].Movement_Walking.Key = Convert.ToChar(optionsArr[0]);
                Core.ActorManager.Actors[selectedActor].Movement_Walking.Speed = Convert.ToInt32(optionsArr[1]);
            },
            "actor walking <key> <speed> => Change selected actor's walking options.");

            var pathingMovementActorItem = movementActorItem.Add("pathing", "actor movement pathing <option> => Change selected actor's pathing options.");
            pathingMovementActorItem.Add("speed", number => Core.ActorManager.Actors[selectedActor].Movement_Pathing.Speed = Convert.ToInt32(number), "actor movement pathing speed <value> => Change selected actor's pathing speed.");

            var nodePathingMovementActorItem = pathingMovementActorItem.Add("node", "actor movement pathing node <action> => Add or delete selected actor's pathing nodes.");
            nodePathingMovementActorItem.Add("add", x => Core.ActorManager.Actors[selectedActor].Movement_Pathing.CreateNode(), "actor movement pathing node add => Add selected actor pathing node. Max 13");
            nodePathingMovementActorItem.Add("delete", x => Core.ActorManager.Actors[selectedActor].Movement_Pathing.DeleteLastNode(), "actor movement pathing node delete => delete selected actor last pathing node.");
            #endregion
            #region precache commands

            var precacheItem = menu.Add("precache", "precache <type> => Prints precache.");
            Core.Precache? precache = null;
            precacheItem.Add("set", path => precache = new Core.Precache(path));
            var listPrecacheItem = precacheItem.Add("list");
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
                var options = new System.Text.Json.JsonSerializerOptions();
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