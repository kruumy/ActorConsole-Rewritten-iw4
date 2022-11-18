namespace ActorConsole.Core
{
    public static class Presets
    {
        /// <summary>
        /// Saves json of given actor object to the path given.
        /// </summary>
        public static void Save(Actor.Actor actor, string path)
        {
            System.Text.Json.JsonSerializerOptions options = new();
            options.WriteIndented = true;
            string ActorJson = System.Text.Json.JsonSerializer.Serialize(actor, options);
            File.WriteAllText(path, ActorJson);
        }
        /// <summary>
        /// Loads path to json into the ActorManager class
        /// </summary>
        public static void Load(string path)
        {
            // TODO: handle for incorrect file pass thru
            string RawJson = File.ReadAllText(path);
            System.Text.Json.JsonElement ActorJson = System.Text.Json.JsonDocument.Parse(RawJson).RootElement;

            ActorManager.Add();
            Core.Actor.Actor actor = ActorManager.Actors.Last();
            actor.Anims.Idle = ActorJson.GetProperty("Anims").GetProperty("Idle").GetString();
            actor.Anims.Death = ActorJson.GetProperty("Anims").GetProperty("Death").GetString();
            actor.Models.Head = ActorJson.GetProperty("Models").GetProperty("Head").GetString();
            actor.Models.Body = ActorJson.GetProperty("Models").GetProperty("Body").GetString();
            actor.Weapons.j_gun = ActorJson.GetProperty("Weapons").GetProperty("j_gun").GetString();
        }
    }
}
