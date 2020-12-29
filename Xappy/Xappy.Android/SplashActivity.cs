using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xappy.Droid
{
    [Activity(Label = "Xappy",
        MainLauncher =true,
        Theme = "@style/MainTheme.Splash",
        NoHistory = true,
        Icon = "@mipmap/icon")]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
        }

        protected override void OnResume()
        {
            base.OnResume();
            SetContentView(Resource.Layout.splash);
            Task startupWork = new Task(()=> { SimulateStartupAsync();});
            startupWork.Start();
        }

        private async Task SimulateStartupAsync()
        {
            await Task.Delay(1000);

            StartActivity(new Intent(Application.Context, typeof(MainActivity)));        }
    }
}