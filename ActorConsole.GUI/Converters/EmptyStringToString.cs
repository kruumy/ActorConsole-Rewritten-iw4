using System;
using System.Globalization;
using System.Windows.Data;

namespace ActorConsole.GUI.Converters
{
    [ValueConversion(typeof(string), typeof(string))]
    public class EmptyStringToString : IValueConverter
    {
        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            if ( parameter is string backupstr )
            {
                if ( string.IsNullOrEmpty((string)value) )
                {
                    return backupstr;
                }
                else
                {
                    return value;
                }
            }
            throw new ArgumentException();
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotImplementedException();
        }
    }
}
