using ActorConsole.Core.CompositedActorProperties;
using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows.Data;

namespace ActorConsole.GUI.Converters
{
    public class GetWeaponsFromBoneConverter : IMultiValueConverter
    {
        public object Convert( object[] values, Type targetType, object parameter, CultureInfo culture )
        {
            Weapons weapons = values.Where(val => val is Weapons).FirstOrDefault() as Weapons;
            if ( weapons is Weapons )
            {
                if ( values.FirstOrDefault(val => val is string) is string boneName )
                {
                    PropertyInfo boneProp = typeof(Weapons).GetProperty(boneName);
                    object weapon = boneProp?.GetValue(weapons);
                    if ( weapon != null )
                    {
                        if ( parameter is string propName )
                        {
                            return weapon.GetType().GetProperty(propName)?.GetValue(weapon)?.ToString();
                        }
                        return weapon;
                    }
                }
            }
            return null;
        }

        public object[] ConvertBack( object value, Type[] targetTypes, object parameter, CultureInfo culture )
        {
            return Array.Empty<object>();
        }
    }
}
