using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xappy.Domain.Global;

namespace Xappy
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

#if DEBUG
            HotReloader.Current.Start(this);
#endif

            DependencyService.Register<AppTheme>();

            InitStyles();

            MainPage = new AppShell();
        }

        private void InitStyles()
        {
            var theme = DependencyService.Get<AppTheme>();
            theme.InitTheme();
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
    }
}
