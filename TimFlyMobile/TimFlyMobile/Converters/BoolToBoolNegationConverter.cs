using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Converters
{
    /// <summary>
    /// Reverse <see cref="bool" />
    /// </summary>
    public class BoolToBoolNegationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
           return !(bool)value;
        }
    }
}
