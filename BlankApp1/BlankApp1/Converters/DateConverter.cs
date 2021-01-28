using System;
using System.Globalization;
using Xamarin.Forms;

namespace BlankApp1.Converters
{
    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((DateTime)value).ToString("F", CultureInfo.CreateSpecificCulture("fr-FR"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
            //throw new NotImplementedException();
        }
    }
}
