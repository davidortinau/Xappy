using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace Xappy.Content.Scenarios.OtherLogin.Controls
{
    public class Card : Frame
    {
        public static readonly BindableProperty ElevationProperty = BindableProperty.Create(
            nameof(Elevation),
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

        /// <summary>
        /// Fix for styling a few properties (Padding, BackgroundColor...)
        /// </summary>
        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == StyleProperty.PropertyName)
            {
                if (Style.Setters.Any(s => s.Property == PaddingProperty))
                {
                    Padding = (Thickness)Style.Setters.First(s => s.Property == PaddingProperty).Value;
                }

                //if (Style.Setters.Any(s => s.Property == BackgroundColorProperty))
                //{
                //    BackgroundColor = (Color)Style.Setters.First(s => s.Property == BackgroundColorProperty).Value;
                //}
            }
        }
    }
}
