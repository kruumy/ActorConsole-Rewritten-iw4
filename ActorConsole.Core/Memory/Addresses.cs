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

        internal static PointerEx MapName => IW4.Game.MainModule.BaseAddress + 0x5EE27A0;
        internal static PointerEx IsInMatch => 0x7F0F88;
        internal static PointerEx PlayerName => IW4.Game.MainModule.BaseAddress + 0x427464;
        internal static PointerEx Cbuf_AddText => 0x404B20u;

        internal struct RGBXYZ_POINTER
        {
            public PointerEx Red;
            public PointerEx Green;
            public PointerEx Blue;
            public PointerEx X;
            public PointerEx Y;
            public PointerEx Z;
        }
    }
}