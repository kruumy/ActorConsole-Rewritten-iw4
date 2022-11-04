using ActorConsole;
namespace ActorConsole.CLI
{
    internal class Program
    {
        /// <summary>
        /// Used for testing currently, will be a fleshed out cli later.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Core.ActorManager am = new Core.ActorManager();
            am.Actors.Add(new Core.Actor.Actor());
            print(am.Actors[0]);
            am.Actors[0].Anims.Idle = "pb_stand_ads";
            am.Actors[0].Models.Head = "void";
            am.Actors[0].Models.Body = "dog";
            am.Actors[0].Weapons.j_gun = "cheytac_mp";
            print(am.Actors[0]);

            am.Actors.Add(new Core.Actor.Actor());
            am.Actors[1].Name = "wowie";
            print(am.Actors[1]);

            am.Actors.Add(new Core.Actor.Actor());
            print(am.Actors[2]);


            Console.ReadLine();
        }

        public static void print(dynamic input, bool endl = true)
        {
            if (input.GetType() == typeof(string))
                Console.Write(input.ToString());
            else
            {
                var options = new System.Text.Json.JsonSerializerOptions();
                options.WriteIndented = true;
                Console.Write(System.Text.Json.JsonSerializer.Serialize(input, input.GetType(), options));
            }
                
            if (endl)
                Console.WriteLine();
        }
    }
}