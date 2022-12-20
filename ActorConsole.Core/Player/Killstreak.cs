namespace ActorConsole.Core.Player
{
    /// <summary>
    /// Class to control to give the player killstreaks.
    /// </summary>
    public class Killstreak
    {
        /// <summary>
        /// Self explanatroy.
        /// </summary>
        /// <param name="killstreak">The name of the killstreak. Refer to the IW4-Cine documentation for a list of the names.</param>
        public static void GiveKillstreak(string killstreak)
        {
            Memory.IW4.SendDvar($"mvm_killstreak {killstreak}");
        }
    }
}