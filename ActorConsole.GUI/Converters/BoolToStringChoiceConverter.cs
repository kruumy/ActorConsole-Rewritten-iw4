using System;
using System.Globalization;
using System.Windows.Data;

namespace ActorConsole.GUI.Converters
{
    [ValueConversion(typeof(bool), typeof(string))]
    public class BoolToStringChoiceConverter : IValueConverter
    {
        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            if ( parameter is string rawstr )
            {
                string[] rawstrsplit = rawstr.Split(':');
                if ( rawstrsplit.Length == 2 )
                {
                    if ( value is bool choice )
                    {
                        return choice ? rawstrsplit[ 0 ] : (object)rawstrsplit[ 1 ];
                    }
                    else if ( value.GetType().IsEnum )
                    {
                        return System.Convert.ToBoolean((uint)value) ? rawstrsplit[ 0 ] : (object)rawstrsplit[ 1 ];
                    }
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
