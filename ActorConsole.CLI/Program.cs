using ActorConsole;
namespace ActorConsole.CLI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Core.ActorManager am = new Core.ActorManager();
            am.Actors.Add(new Core.Actor.Actor());

            am.Actors[0].Anims.Idle = "pb_stand_ads";
            am.Actors[0].Models.Head = "void";
            am.Actors[0].Models.Body = "dog";
            am.Actors[0].Name = "josh";
            am.Actors[0].Weapons.j_gun = "cheytac_mp";

            print(am.Actors);



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