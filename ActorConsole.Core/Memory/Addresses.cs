using AnotherExternalMemoryLibrary;

namespace ActorConsole.Core.Memory
{
    internal static class Addresses
    {
        internal static IntPtrEx Cbuf_AddText { get; } = 0x404B20u;
        internal static IntPtrEx LocalPlayer_HasSpawned { get; } = 0x7F0F88;
        internal static IntPtrEx? LocalPlayer_MapName => IW4.Game?.MainModule?.BaseAddress + 0x5EE27A0;
        internal static IntPtrEx? LocalPlayer_Name => IW4.Game?.MainModule?.BaseAddress + 0x427464;
        internal static (IntPtrEx red, IntPtrEx blue, IntPtrEx green, IntPtrEx x, IntPtrEx y, IntPtrEx z) Sun { get; } = (0x0085B878, 0x0085B87C, 0x0085B880, 0x0085B884, 0x0085B888, 0x0085B88C);
    }
}
