using AnotherExternalMemoryLibrary.Extensions;

namespace ActorConsole.Core.World
{
    public static class Sun
    {
        public static float Red
        {
            get => Memory.IW4.Game.Read<float>(Memory.Addresses.Sun.Red);
            set => Memory.IW4.Game.Write(Memory.Addresses.Sun.Red, value);
        }
        public static float Green
        {
            get => Memory.IW4.Game.Read<float>(Memory.Addresses.Sun.Green);
            set => Memory.IW4.Game.Write(Memory.Addresses.Sun.Green, value);
        }
        public static float Blue
        {
            get => Memory.IW4.Game.Read<float>(Memory.Addresses.Sun.Blue);
            set => Memory.IW4.Game.Write(Memory.Addresses.Sun.Blue, value);
        }
        public static float X
        {
            get => Memory.IW4.Game.Read<float>(Memory.Addresses.Sun.X);
            set => Memory.IW4.Game.Write(Memory.Addresses.Sun.X, value);
        }
        public static float Y
        {
            get => Memory.IW4.Game.Read<float>(Memory.Addresses.Sun.Y);
            set => Memory.IW4.Game.Write(Memory.Addresses.Sun.Y, value);
        }
        public static float Z
        {
            get => Memory.IW4.Game.Read<float>(Memory.Addresses.Sun.Z);
            set => Memory.IW4.Game.Write(Memory.Addresses.Sun.Z, value);
        }
    }

}
