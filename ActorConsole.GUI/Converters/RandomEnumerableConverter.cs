using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace ActorConsole.GUI.Converters
{
    [ValueConversion(typeof(IEnumerable<object>), typeof(object))]
    public class RandomEnumerableConverter : IValueConverter
    {
        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            if ( value is IEnumerable<object> enumerable )
            {
                return enumerable.ElementAtOrDefault(new Random().Next(enumerable.Count() - 1));
            }
            return null;
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotImplementedException();
        }
    }
}
