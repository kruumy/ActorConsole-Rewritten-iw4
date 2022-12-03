namespace ActorConsole.Core.Other
{
    public static class Player
    {
        public static void SetModel(string model, string team, string name)
        {
            Memory.IW4.SendDvar($"mvm_bot_model {name} {model} {team}");
        }
        public static void GiveKillstreak(string killstreak)
        {
            Memory.IW4.SendDvar($"mvm_killstreak {killstreak}");
        }
    }
}
