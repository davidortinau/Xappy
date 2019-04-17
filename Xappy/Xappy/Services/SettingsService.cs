using System;
using Xamarin.Forms;

namespace Xappy.Services
{
    public static class SettingsService
    {
        public static void UseTabs(BindableObject context)
        {
            Shell.SetFlyoutBehavior(Shell.Current, Xamarin.Forms.FlyoutBehavior.Disabled);
            Shell.SetTabBarIsVisible(context, true);

        }

        public static void UseFlyout(BindableObject context)
        {
            Shell.SetFlyoutBehavior(Shell.Current, Xamarin.Forms.FlyoutBehavior.Flyout);
            Shell.SetTabBarIsVisible(context, false);
        }
    }
}
