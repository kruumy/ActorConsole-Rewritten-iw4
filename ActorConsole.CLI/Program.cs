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
            am.Add(new Core.Actor.Actor());
            am.Add(new Core.Actor.Actor());


            print(am.Actors);
            am.Remove(0);
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
                options.MaxDepth = int.MaxValue;
                options.IncludeFields = true;
                Console.Write(System.Text.Json.JsonSerializer.Serialize(input, input.GetType(), options));
            }
                
            if (endl)
                Console.WriteLine();
        }
    }
}