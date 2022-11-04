namespace ActorConsole.Core.Misc
{
    public static class Sun
    {
        private static Memory.Addresses.SunAddrs Addrs = (Memory.Addresses.SunAddrs)Memory.Addresses.KeyValuePairs["Sun"];
        public static float Red
        {
            get
            {
                return Memory.IW4.mem.GetValue<float>(Addrs.Red);
            }
            set
            {
                Memory.IW4.mem.SetValue(Addrs.Red, value);
            }
        }
        public static float Green
        {
            get
            {
                return Memory.IW4.mem.GetValue<float>(Addrs.Green);
            }
            set
            {
                Memory.IW4.mem.SetValue(Addrs.Green, value);
            }
        }
        public static float Blue
        {
            get
            {
                return Memory.IW4.mem.GetValue<float>(Addrs.Blue);
            }
            set
            {
                Memory.IW4.mem.SetValue(Addrs.Blue, value);
            }
        }
        public static float X
        {
            get
            {
                return Memory.IW4.mem.GetValue<float>(Addrs.X);
            }
            set
            {
                Memory.IW4.mem.SetValue(Addrs.X, value);
            }
        }
        public static float Y
        {
            get
            {
                return Memory.IW4.mem.GetValue<float>(Addrs.Y);
            }
            set
            {
                Memory.IW4.mem.SetValue(Addrs.Y, value);
            }
        }
        public static float Z
        {
            get
            {
                return Memory.IW4.mem.GetValue<float>(Addrs.Z);
            }
            set
            {
                Memory.IW4.mem.SetValue(Addrs.Z, value);
            }
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
