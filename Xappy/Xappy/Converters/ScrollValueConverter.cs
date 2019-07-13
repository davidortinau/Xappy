using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Xappy.Converters
{
    public class ScrollValueConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var fmt = new NumberFormatInfo();
            fmt.NegativeSign = "-";

            var percentage = (double)value;

            var allParams = ((string)parameter).Split((';'));
            var factor = Double.Parse(allParams[0], fmt);
            var min = Double.Parse(allParams[1]);
            var max = Double.Parse(allParams[2]);
            var reverse = bool.Parse(allParams[3]);
            var delayUntilPercentage = Double.Parse(allParams[4]);

            if (percentage == 0) return min;

            if (delayUntilPercentage > 0 && percentage < delayUntilPercentage) return min;

            percentage = percentage - delayUntilPercentage;

            if(reverse)
            {
                percentage = 1 - (percentage * factor);
                return (percentage * max);
            }
            else
            {
                return (percentage * max) * factor;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //throw new NotImplementedException();
            return null;
        }
    }
}