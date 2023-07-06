using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace ActorConsole.GUI.Converters
{
    public class AllTrueMultiValueConverter : IMultiValueConverter
    {
        public object Convert( object[] values, Type targetType, object parameter, CultureInfo culture )
        {
            return values.All(val => val is bool b && b);
        }

        public object[] ConvertBack( object value, Type[] targetTypes, object parameter, CultureInfo culture )
        {
            throw new NotImplementedException();
        }
    }
}
