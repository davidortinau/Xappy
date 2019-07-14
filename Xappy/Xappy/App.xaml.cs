using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xappy.Domain.Global;

namespace Xappy
{
    public partial class App : Application
    {
        public static double ScreenWidth
        {
            get
            {
                return Device.Info.ScaledScreenSize.Width;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public static Color LookupColor(string key)
        {
            try
            {
                Application.Current.Resources.TryGetValue(key, out var newColor);
                return (Color)newColor;
            }
            catch
            {
                return Color.White;
            }
        }

    }
}
