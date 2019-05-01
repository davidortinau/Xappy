using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using Xamarin.Forms;
using Xappy.ControlGallery;

namespace Xappy.Converters
{

    public class PropertyInfoBooleanValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return false;

            var propInfo = (PropertyInfo)value;
            var isVal = (bool)propInfo.GetValue(((ControlPage)parameter).Element);

            return isVal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}