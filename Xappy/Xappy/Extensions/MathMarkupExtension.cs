using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Xaml;

namespace Xappy.Extensions
{
    public class MathMarkupExtension : IMarkupExtension<double>
    {
        public double FullWidth { get; set; }

        public double Offset { get; set; }

        public double ProvideValue(IServiceProvider serviceProvider)
        {
            return FullWidth - Offset;
        }
        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return (this as IMarkupExtension<double>).ProvideValue(serviceProvider);
        }
    }
}
