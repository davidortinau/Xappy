using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Xappy.Content.Common
{
    public class ScrollerView : ScrollView
    {
        public static readonly BindableProperty ScrollPositionProperty = BindableProperty.Create<ScrollerView, double>(s => s.ScrollPosition, default(double));
        public double ScrollPosition
        {
            get { return (double)GetValue(ScrollPositionProperty); }
            set { SetValue(ScrollPositionProperty, value); }
        }

        public static readonly BindableProperty ScrollPercentageProperty = BindableProperty.Create<ScrollerView, double>(s => s.ScrollPercentage, default(double));

        public double ScrollPercentage
        {
            get { return (double)GetValue(ScrollPercentageProperty); }
            set { SetValue(ScrollPercentageProperty, value); }
        }

        public ScrollerView()
        {
            this.Scrolled += ScrollerView_Scrolled;
        }

        private void ScrollerView_Scrolled(object sender, ScrolledEventArgs e)
        {
            ScrollPosition = e.ScrollY;
            ScrollPercentage = e.ScrollY / this.ContentSize.Height;
        }
    }
}
