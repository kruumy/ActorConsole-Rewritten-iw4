using AnotherExternalMemoryLibrary;
namespace ActorConsole.Core.Memory
{
    internal static class Addresses
    {
        internal static RGBXYZ_POINTER Sun => new(0x0085B878, 0x0085B87C, 0x0085B880, 0x0085B884, 0x0085B888, 0x0085B88C);
        internal static PointerEx MapName => IW4.Game.MainModule.BaseAddress + 0x5EE27A0;
        internal static PointerEx IsInGame => 0x7F0F88;
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
            public RGBXYZ_POINTER(PointerEx red, PointerEx green, PointerEx blue, PointerEx x, PointerEx y, PointerEx z)
            {
                Red = red;
                Green = green;
                Blue = blue;
                X = x;
                Y = y;
                Z = z;
            }
        }
    }
}
