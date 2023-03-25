using AnotherExternalMemoryLibrary.Extensions;

namespace ActorConsole.Core.Memory
{
    public static class Sun
    {
        public static float? X { get => IW4.Game?.Read<float>(Addresses.Sun.x); set => IW4.Game?.Write<float>(Addresses.Sun.x, (float)value); }
        public static float? Y { get => IW4.Game?.Read<float>(Addresses.Sun.y); set => IW4.Game?.Write<float>(Addresses.Sun.y, (float)value); }
        public static float? Z { get => IW4.Game?.Read<float>(Addresses.Sun.z); set => IW4.Game?.Write<float>(Addresses.Sun.z, (float)value); }
        public static float? Red { get => IW4.Game?.Read<float>(Addresses.Sun.red); set => IW4.Game?.Write<float>(Addresses.Sun.red, (float)value); }
        public static float? Green { get => IW4.Game?.Read<float>(Addresses.Sun.green); set => IW4.Game?.Write<float>(Addresses.Sun.green, (float)value); }
        public static float? Blue { get => IW4.Game?.Read<float>(Addresses.Sun.blue); set => IW4.Game?.Write<float>(Addresses.Sun.blue, (float)value); }
    }
}
