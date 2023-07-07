using AnotherExternalMemoryLibrary;
using AnotherExternalMemoryLibrary.Extensions;
using System;
using System.Diagnostics;
using System.Linq;

namespace ActorConsole.Core.Memory
{
    public sealed class IW4 : Game
    {
        public readonly static string[] TargetProcessNames = { "iw4m", "iw4x" };
        public IW4() : base(Process.GetProcesses().First(p => TargetProcessNames.Any(t => t == p.ProcessName)))
        {
        }
        public override IntPtrEx Cbuf_AddText { get; } = 0x404B20u;
        public override IntPtrEx LocalPlayerIsSpawnedAddress { get; } = 0x7F0F88;
        public override IntPtrEx LocalPlayerMapNameAddress => Process.MainModule.BaseAddress + 0x5EE27A0;
        public override IntPtrEx LocalPlayerNameAddress => Process.MainModule.BaseAddress + 0x427464;

        public override Type CamoEnum { get; } = typeof(Camo);
        public enum Camo
        {
            none,
            artic,
            desert,
            woodland,
            digital,
            urban,
            red,
            blue,
            fall,
            gold
        }

        public IntPtrEx SunRedAddress { get; } = 0x0085B878;

        public IntPtrEx SunBlueAddress { get; } = 0x0085B880;

        public IntPtrEx SunGreenAddress { get; } = 0x0085B87C;

        public IntPtrEx SunXAddress { get; } = 0x0085B884;

        public IntPtrEx SunYAddress { get; } = 0x0085B888;

        public IntPtrEx SunZAddress { get; } = 0x0085B88C;
        public float SunRed
        {
            get => Process.Read<float>(SunRedAddress);
            set => Process.Write<float>(SunRedAddress, value);
        }
        public float SunGreen
        {
            get => Process.Read<float>(SunGreenAddress);
            set => Process.Write<float>(SunGreenAddress, value);
        }
        public float SunBlue
        {
            get => Process.Read<float>(SunBlueAddress);
            set => Process.Write<float>(SunBlueAddress, value);
        }
        public float SunX
        {
            get => Process.Read<float>(SunXAddress);
            set => Process.Write<float>(SunXAddress, value);
        }
        public float SunY
        {
            get => Process.Read<float>(SunYAddress);
            set => Process.Write<float>(SunYAddress, value);
        }
        public float SunZ
        {
            get => Process.Read<float>(SunZAddress);
            set => Process.Write<float>(SunZAddress, value);
        }

        public enum KillstreakModel
        {
            vehicle_ac130_low_mp,
            vehicle_apache_mp,
            vehicle_av8b_harrier_jet_mp,
            vehicle_av8b_harrier_jet_opfor_mp,
            vehicle_b2_bomber,
            vehicle_cobra_helicopter_fly_low,
            vehicle_little_bird_armed,
            vehicle_mi24p_hind_mp,
            //vehicle_mi-28_mp,
            vehicle_mig29_desert,
            vehicle_pavelow,
            vehicle_pavelow_opfor,
            vehicle_uav_static_mp
        }

        public override Type KillStreakEnum { get; } = typeof(Killstreak);

        public override Type KillStreakModelEnum { get; } = typeof(KillstreakModel);

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
    }
}
