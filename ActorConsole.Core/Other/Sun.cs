namespace ActorConsole.Core.Other
{
    public static class Sun
    {
        private static Memory.Addresses.SunAddrs Addrs = (Memory.Addresses.SunAddrs)Memory.Addresses.KeyValuePairs["Sun"];
        public static float Red
        {
            get => Memory.IW4.mem.Read<float>(Addrs.Red);
            set => Memory.IW4.mem.Write(Addrs.Red, value);
        }
        public static float Green
        {
            get => Memory.IW4.mem.Read<float>(Addrs.Green);
            set => Memory.IW4.mem.Write(Addrs.Green, value);
        }
        public static float Blue
        {
            get => Memory.IW4.mem.Read<float>(Addrs.Blue);
            set => Memory.IW4.mem.Write(Addrs.Blue, value);
        }
        public static float X
        {
            get => Memory.IW4.mem.Read<float>(Addrs.X);
            set => Memory.IW4.mem.Write(Addrs.X, value);
        }
        public static float Y
        {
            get => Memory.IW4.mem.Read<float>(Addrs.Y);
            set => Memory.IW4.mem.Write(Addrs.Y, value);
        }
        public static float Z
        {
            get => Memory.IW4.mem.Read<float>(Addrs.Z);
            set => Memory.IW4.mem.Write<float>(Addrs.Z, value);
        }
        public static object GetSunObject()
        {
            SunObject result;
            result.Red = Red;
            result.Green = Green;
            result.Blue = Blue;
            result.X = X;
            result.Y = Y;
            result.Z = Z;
            return result;
        }
        private struct SunObject
        {
            public float Red;
            public float Green;
            public float Blue;
            public float X;
            public float Y;
            public float Z;
        }
    }

}
