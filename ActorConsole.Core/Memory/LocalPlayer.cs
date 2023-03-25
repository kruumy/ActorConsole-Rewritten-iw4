using AnotherExternalMemoryLibrary;
using AnotherExternalMemoryLibrary.Extensions;

namespace ActorConsole.Core.Memory
{
    public static class LocalPlayer
    {
        public enum Class
        {
            ASSAULT,
            SHOTGUN,
            SMG,
            LMG,
            RIOT,
            SNIPER,
            GHILLIE
        }

        public enum Killstreak
        {
            ac130,
            airdrop,
            airdrop_mega,
            airdrop_sentry_minigun,
            counter_uav,
            emp,
            harrier_airstrike,
            helicopter,
            helicopter_flares,
            helicopter_minigun,
            nuke,
            precision_airstrike,
            predator_missile,
            sentry,
            stealth_airstrike,
            uav
        }

        public enum Team
        {
            axis,
            allies
        }

        public static bool HasSpawned => IW4.Game?.Read<int>(Addresses.LocalPlayer_HasSpawned) != 0;
        public static string Map => IW4.Game?.Read<byte>((IntPtrEx)Addresses.LocalPlayer_MapName, 20).GetString();
        public static string Name => IW4.Game?.Read<byte>((IntPtrEx)Addresses.LocalPlayer_Name, 25).GetString();

        public static void GiveKillstreak( Killstreak killstreak )
        {
            IW4.Send($"mvm_killstreak {killstreak}");
        }

        public static void SetModel( Class model, Team team )
        {
            SetModel(model, team, Name);
        }

        public static void SetModel( Class model, Team team, string name )
        {
            IW4.Send($"mvm_bot_model {name} {model} {team}");
        }
    }
}
