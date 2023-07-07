using System;
using System.Globalization;
using System.Windows.Data;

namespace ActorConsole.GUI.Converters
{
    [ValueConversion(typeof(Type), typeof(string[]))]
    public class EnumToStringsConverter : IValueConverter
    {
        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            if ( value is Type type )
            {
                return type.GetEnumNames();
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new NotImplementedException();
        }
    }
}
