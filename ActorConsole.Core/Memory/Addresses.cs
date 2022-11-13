namespace ActorConsole.Core.Memory
{
    internal static class Addresses
    {
        public static Dictionary<string, object> KeyValuePairs = new Dictionary<string, object>()
        {
            { "Sun", new SunAddrs(0x0085B878,0x0085B87C,0x0085B880,0x0085B884,0x0085B888,0x0085B88C) },
            { "Map", IW4.mem.BaseAddress + 0x5EE27A0 },
            { "InGame", new PointerEx((IntPtr)0x7F0F88) },
            { "PlayerName" ,IW4.mem.BaseAddress + 0x427464 }
        };

        internal struct SunAddrs
        {
            public PointerEx Red;
            public PointerEx Green;
            public PointerEx Blue;
            public PointerEx X;
            public PointerEx Y;
            public PointerEx Z;
            public SunAddrs(PointerEx red, PointerEx green, PointerEx blue, PointerEx x, PointerEx y, PointerEx z)
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
