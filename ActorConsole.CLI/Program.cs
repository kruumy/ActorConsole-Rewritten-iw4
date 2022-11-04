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
            
            while (true)
            {
                string[] input = Console.ReadLine().Split(' ');
                switch (input[0])
                {
                    case "create":
                        {
                            am.Add(new Core.Actor.Actor());
                            Console.WriteLine("Created Actor");
                            break;
                        }
                    case "ls":
                        {
                            print(am.Actors);
                            break;
                        }
                    case "sun":
                        {
                            print(Core.Misc.Sun.GetSunObject());
                            break;
                        }
                    case "game":
                        {
                            print(Core.Memory.IW4.InGame);
                            print(Core.Memory.IW4.Map);
                            print(Core.Memory.IW4.IsRunning);
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Invalid Command");
                            break;
                        }
                }


            }
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