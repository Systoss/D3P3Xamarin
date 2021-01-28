using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace BlankApp1.Converters
{
    public class PocConverter : IValueConverter
    {
        string firstAnswer = "mon entree etait toto";
        string secondAnswer = "mon entree etait toto";
        string otherAnswer = "bad entry";
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((string)value == "toto")
            {
                return firstAnswer;
            }
            if ((string)value == "tata")
            {
                return secondAnswer;
            }
            return otherAnswer;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var innerValue = (string)value;
            if ( innerValue == firstAnswer )
            {
                return "tata";
            }
            if ( innerValue == secondAnswer )
            {
                return "toto";
            }
            return "unknown";
        }
    }
}
