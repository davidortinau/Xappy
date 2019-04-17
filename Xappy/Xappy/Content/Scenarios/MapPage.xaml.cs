using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Xappy.Scenarios
{
    public partial class MapPage : ContentPage
    {
        public MapPage()
        {
            InitializeComponent();


            SetLocation();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();


        }

        async void SetLocation()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    MyMap.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                    new Position(42.349344, -71.082504),
                    Distance.FromMiles(0.3)));
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
