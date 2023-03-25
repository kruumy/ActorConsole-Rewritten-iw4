using System;
using System.Globalization;
using System.Windows.Data;

namespace ActorConsole.GUI.Converters
{
    [ValueConversion(typeof(string), typeof(bool))]
    public class IsStringNotEmptyConverter : IValueConverter
    {
        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            if ( value is string input && !string.IsNullOrEmpty(input) )
            {
                return true;
            }
            return false;
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotImplementedException();
        }
    }
}
