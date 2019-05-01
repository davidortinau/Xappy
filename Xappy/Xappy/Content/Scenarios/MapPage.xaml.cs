using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace Xappy.Scenarios
{
    public partial class MapPage : ContentPage
    {
        private Thickness _safeInsets;

        public MapPage()
        {
            InitializeComponent();
            SetLocation();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _safeInsets = On<Xamarin.Forms.PlatformConfiguration.iOS>().SafeAreaInsets();

            Device.BeginInvokeOnMainThread(() =>
            {
                AdditionalContent.HeightRequest = this.Height - MainContent.Height;
                InfoPanel.TranslationY = AdditionalContent.HeightRequest;

                var newPadding = InfoPanel.Padding;
                newPadding.Bottom = newPadding.Bottom + _safeInsets.Bottom;
                InfoPanel.Padding = newPadding;
            });
        }

        public void Handle_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                switch (e.StatusType)
                {
                    case GestureStatus.Running:
                        InfoPanel.TranslationY = Math.Max(_safeInsets.Top, Math.Min(AdditionalContent.HeightRequest, InfoPanel.TranslationY + e.TotalY));
                        break;
                }
            });
        }

        public async void Handle_Clicked(object sender, EventArgs e)
        {
            var location = new Location(42.349344, -71.082504);
            var options = new MapLaunchOptions { Name = "Wired Puppy", NavigationMode = NavigationMode.Driving };
            await Xamarin.Essentials.Map.OpenAsync(location, options);
        }

        async void SetLocation()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(42.349344, -71.082504), Distance.FromMiles(0.1)));
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
        }
    }
}
