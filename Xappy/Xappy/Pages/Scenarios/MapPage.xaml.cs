using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Xappy.Pages.Scenarios
{
    public partial class MapPage : ContentPage
    {
        public MapPage()
        {
            InitializeComponent();

            MyMap.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                    new Position(42.349344, -71.082504),
                    Distance.FromMiles(0.3)));
        }
    }
}
