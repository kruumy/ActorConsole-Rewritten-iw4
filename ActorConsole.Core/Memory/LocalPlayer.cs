﻿namespace ActorConsole.Core.Memory
{
    public static class LocalPlayer // TODO make class implementing Inotifypropchanged with timer to check props
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

        public static LocalPlayerProperties Properties { get; } = new LocalPlayerProperties();

        public static void GiveKillstreak( Killstreak killstreak )
        {
            IW4.Send($"mvm_killstreak {killstreak}");
        }

        public static void SetModel( Class model, Team team )
        {
            SetModel(model, team, Properties.Name);
        }

        public static void SetModel( Class model, Team team, string name )
        {
            IW4.Send($"mvm_bot_model {name} {model} {team}");
        }
    }
}
