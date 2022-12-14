using AnotherExternalMemoryLibrary.Extensions;
using System.Text.Json;

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
        public new static string ToString()
        {
            return JsonSerializer.Serialize(new { Red, Green, Blue, X, Y, Z }, new JsonSerializerOptions()
            {
                WriteIndented = true
            });
        }
        public static void LoadJson(string rawJson)
        {
            JsonElement json = JsonDocument.Parse(rawJson).RootElement;
            if (json.TryGetProperty("Red", out JsonElement Rvalue))
                Red = (float)(Rvalue.GetDouble());
            if (json.TryGetProperty("Green", out JsonElement Gvalue))
                Green = (float)(Gvalue.GetDouble());
            if (json.TryGetProperty("Blue", out JsonElement Bvalue))
                Blue = (float)(Bvalue.GetDouble());
            if (json.TryGetProperty("X", out JsonElement Xvalue))
                X = (float)(Xvalue.GetDouble());
            if (json.TryGetProperty("Y", out JsonElement Yvalue))
                Y = (float)(Yvalue.GetDouble());
            if (json.TryGetProperty("Z", out JsonElement Zvalue))
                Z = (float)(Zvalue.GetDouble());
        }
    }
}