using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xappy.Content.Blog;
using Xappy.Content.Scenarios.Conversation;
using Xappy.Content.Scenarios.Login;
using Xappy.Content.Scenarios.PhotoGallery;
using Xappy.Content.Scenarios.ProductDetails;
using Xappy.Content.Scenarios.ToDo;
using Xappy.Content.Settings;
using Xappy.ControlGallery;
using Xappy.Scenarios;

namespace Xappy
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            RegisterRoutes();
        }

        private void RegisterRoutes()
        {
            Routing.RegisterRoute("control", typeof(ControlPage));
            Routing.RegisterRoute("map", typeof(MapPage));
            Routing.RegisterRoute("login", typeof(LoginPage));
            Routing.RegisterRoute("otherlogin", typeof(Content.Scenarios.OtherLogin.LoginPage));
            Routing.RegisterRoute("todo", typeof(ItemsPage));
            Routing.RegisterRoute("conversation", typeof(ConversationPage));
            Routing.RegisterRoute("productdetails", typeof(ProductDetailsPage));
            Routing.RegisterRoute("blog", typeof(Content.Blog.IndexPage));
            Routing.RegisterRoute("blogDetail", typeof(BlogDetailPage));
            Routing.RegisterRoute("settings", typeof(SettingsPage));

            // ToDo
            Routing.RegisterRoute("add", typeof(NewItemPage));

            Routing.RegisterRoute("photo", typeof(PhotoDetailsPage));

            Routing.RegisterRoute("onboarding", typeof(Content.Scenarios.Onboarding.IndexPage));
            Routing.RegisterRoute("photogallery", typeof(Content.Scenarios.PhotoGallery.IndexPage));
        }


        //Shell Flyout Smooth Render
        private DateTime LastFlyoutHiddenUtcDateTime { get; set; }
        private bool WasNavigationCancelledToCloseFlyoutAndReRunAfterADelayToAvoidJitteryFlyoutCloseTransitionBug = false;
        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(FlyoutIsPresented))
            {
                if (!FlyoutIsPresented)
                {
                    LastFlyoutHiddenUtcDateTime = DateTime.UtcNow;
                }
            }
        }
        protected override async void OnNavigating(ShellNavigatingEventArgs args)
        {
            if (!WasNavigationCancelledToCloseFlyoutAndReRunAfterADelayToAvoidJitteryFlyoutCloseTransitionBug)
            {
                // if the above value is true, then this is the re-run navigation from the GoToAsync(args.Target) call below - skip this block this second pass through, as the flyout is now closed
                if ((DateTime.UtcNow - LastFlyoutHiddenUtcDateTime).TotalMilliseconds < 1000)
                {
                    args.Cancel();

                    FlyoutIsPresented = false;

                    OnPropertyChanged(nameof(FlyoutIsPresented));

                    await Task.Delay(300);

                    WasNavigationCancelledToCloseFlyoutAndReRunAfterADelayToAvoidJitteryFlyoutCloseTransitionBug = true;

                    // re-run the originally requested navigation
                    await GoToAsync(args.Target);

                    return;
                }
            }

            WasNavigationCancelledToCloseFlyoutAndReRunAfterADelayToAvoidJitteryFlyoutCloseTransitionBug = false;

            base.OnNavigating(args);
        }
    }
}
