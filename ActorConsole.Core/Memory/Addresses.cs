using AnotherExternalMemoryLibrary;

namespace ActorConsole.Core.Memory
{
    internal static class Addresses
    {
        internal static RGBXYZ_POINTER Sun => new RGBXYZ_POINTER
        {
            Red = 0x0085B878,
            Blue = 0x0085B87C,
            Green = 0x0085B880,
            X = 0x0085B884,
            Y = 0x0085B888,
            Z = 0x0085B88C
        };

        internal static IntPtrEx MapName => IW4.Game.MainModule.BaseAddress + 0x5EE27A0;
        internal static IntPtrEx IsInMatch => 0x7F0F88;
        internal static IntPtrEx PlayerName => IW4.Game.MainModule.BaseAddress + 0x427464;
        internal static IntPtrEx Cbuf_AddText => 0x404B20u;

        internal struct RGBXYZ_POINTER
        {
            public IntPtrEx Red;
            public IntPtrEx Green;
            public IntPtrEx Blue;
            public IntPtrEx X;
            public IntPtrEx Y;
            public IntPtrEx Z;
        }
    }
}