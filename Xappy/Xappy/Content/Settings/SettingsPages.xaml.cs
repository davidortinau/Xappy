using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xappy.Services;

namespace Xappy.Content.Settings
{
    public partial class SettingsPages : ContentPage
    {
        public SettingsPages()
        {
            InitializeComponent();
        }

        async void Handle_CloseClicked(object sender, System.EventArgs e)
        {
            await Shell.Current.Navigation.PopModalAsync();
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            SettingsService.UseFlyout(this);
        }

        void Handle_Clicked_1(object sender, System.EventArgs e)
        {
            SettingsService.UseTabs(this);
        }

        void Handle_Clicked_2(object sender, System.EventArgs e)
        {
            Application.Current.Resources["CurrentTheme"] = Application.Current.Resources["BaseStyle"];
        }

        void Handle_Clicked_3(object sender, System.EventArgs e)
        {
            Application.Current.Resources["CurrentTheme"] = Application.Current.Resources["SecondaryShell"];
        }
    }
}
