using System;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace Xappy.Content.Scenarios.Login.Controls
{
    public class Card : Frame
    {
        public static readonly BindableProperty ElevationProperty = BindableProperty.Create(
            "Elevation",
            typeof(int),
            typeof(Card),
            4,
            propertyChanged: (control, _, __) => ((Card)control).UpdateElevation());

        public int Elevation
        {
            get { return (int)GetValue(ElevationProperty); }
            set { SetValue(ElevationProperty, value); }
        }

        public Card()
        {
            HasShadow = false;
            Padding = new Thickness(12, 6);
            BackgroundColor = Color.Transparent;
        }

        private void UpdateElevation()
        {
            On<iOS>()
                .SetIsShadowEnabled(true)
                .SetShadowColor(Color.Black)
                .SetShadowOffset(new Size(0, 4))
                .SetShadowOpacity(0.4)
                .SetShadowRadius(Elevation / 2);

            On<Android>()
                .SetElevation(Elevation);
        }
    }
}
