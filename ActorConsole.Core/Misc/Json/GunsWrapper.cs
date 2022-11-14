using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ActorConsole.Core.Misc.Json
{
    public static class GunsWrapper
    {
        public static string RawText = File.ReadAllText("Misc/Json/guns.json");
        public static JsonElement RootElement = JsonDocument.Parse(RawText).RootElement;
        public static string[] Camos
        {
            get
            {
                return (string[])RootElement.GetProperty("camos")
                    .Deserialize(Array.Empty<string>().GetType());
            }
        }
        public static string[] GetClasses()
        {
            List<string> result = new List<string>();
            foreach (var item in RootElement.GetProperty("weapons").EnumerateObject())
            {
                result.Add(item.Name);
            }
            return result.ToArray();
        }
        public static string[] GetWeapons(string GunClass)
        {
            List<string> result = new List<string>();
            foreach (var item in RootElement.GetProperty("weapons").GetProperty(GunClass).EnumerateObject())
            {
                result.Add(item.Name);
            }
            return result.ToArray();
        }
        public static string[] GetWeaponTypes(string GunClass, string GunName)
        {
            List<string> result = new List<string>();
            foreach (var item in RootElement.GetProperty("weapons").GetProperty(GunClass).GetProperty(GunName).EnumerateArray())
            {
                result.Add(item.GetString());
            }
            return result.ToArray();
        }


        //TODO: finish this

    }
}
