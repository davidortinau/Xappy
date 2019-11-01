using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using ImageCircle.Forms.Plugin.Droid;
using Android.Content.Res;
using Xappy.Styles;

[assembly: Android.App.MetaData("com.google.android.maps.v2.API_KEY", Value = Xappy.ApiConstants.GoogleMapsKey)]
namespace Xappy.Droid
{
    [Activity(
        Label = "Xappy", 
        Icon = "@mipmap/icon", 
        Theme = "@style/MainTheme.Launcher", 
        MainLauncher = true, 
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        internal static MainActivity Instance { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            Instance = this;

            global::Xamarin.Forms.Forms.SetFlags("CarouselView_Experimental");
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Xamarin.FormsMaps.Init(this, savedInstanceState);
            ImageCircleRenderer.Init();
            global::Xamarin.Forms.FormsMaterial.Init(this, savedInstanceState);
            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }


        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);

            if ((newConfig.UiMode & UiMode.NightNo) != 0)
            {
                if (App.AppTheme != "dark")
                    return;
                App.Current.Resources = new LightTheme();
                App.AppTheme = "light";

            }
            else
            {
                // Night mode is active, we're using dark theme
                if (App.AppTheme == "dark")
                    return;
                //Add a Check for App Theme since this is called even when not changed really

                App.Current.Resources = new DarkTheme();
                App.AppTheme = "dark";
            }
        }

    }
}