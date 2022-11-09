namespace ActorConsole.Core.Misc
{
    public static class Player
    {
        public static string? Name => Memory.IW4.PlayerName;
        public static void SetModel(string model, string team, string name = null)
        {
            if (name == null)
                if (Name != null)
                    name = Name;
                else
                    throw new ArgumentNullException(nameof(name));

            Memory.IW4.SendDvar($"mvm_bot_model {name} {model} {team}");
        }
    }
}
