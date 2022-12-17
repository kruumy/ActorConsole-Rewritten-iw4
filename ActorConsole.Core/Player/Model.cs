namespace ActorConsole.Core.Player
{
    /// <summary>
    /// A class containing a method to set the players current model.
    /// </summary>
    public static class Model
    {
        /// <summary>
        /// Set the players current model using /mvm_bot_model.
        /// </summary>
        /// <param name="model">The class model type.</param>
        /// <param name="team">The team. AXIS or ALLIES.</param>
        /// <param name="name">The players name in game. Probably want to use IW4.PlayerName</param>
        public static void SetModel(string model, string team, string name)
        {
            Memory.IW4.SendDvar($"mvm_bot_model {name} {model} {team}");
        }
    }
}