using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Android.Content.Res;
using Xappy.Styles;
using Xamarin.Forms.DualScreen;
using Xamarin.Forms.Platform.Android;

[assembly: Android.App.MetaData("com.google.android.maps.v2.API_KEY", Value = Xappy.ApiConstants.GoogleMapsKey),]
namespace Xappy.Droid
{
    [Activity(
        Label = "Xappy", 
        Icon = "@mipmap/icon", 
        Theme = "@style/MainTheme.Launcher",
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        internal static MainActivity Instance { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            DualScreenService.Init(this);

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            this.SetStatusBarColor(Xamarin.Forms.Color.FromHex("#fd7b38").ToAndroid());

            base.OnCreate(savedInstanceState);
            Instance = this;

            
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Xamarin.FormsMaps.Init(this, savedInstanceState);
            global::Xamarin.Forms.FormsMaterial.Init(this, savedInstanceState);
            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}