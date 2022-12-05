namespace ActorConsole.Core.Player
{
    public class Killstreak
    {
        public static void GiveKillstreak(string killstreak)
        {
            Memory.IW4.SendDvar($"mvm_killstreak {killstreak}");
        }
    }
}
