using System;
using System.Globalization;
using System.Windows.Data;

namespace ActorConsole.GUI.Converters
{
    [ValueConversion(typeof(int), typeof(bool))]
    public class CountToBoolConverter : IValueConverter // Not nessesary because 0 == false but use it because code feels more readable
    {
        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            if ( value is int count )
            {
                return count > 0;
            }
            return false;
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotImplementedException();
        }
    }
}
