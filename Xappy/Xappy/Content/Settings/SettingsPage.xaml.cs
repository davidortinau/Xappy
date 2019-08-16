using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xappy.Services;
using Xappy.Domain.Global;


namespace Xappy.Content.Settings
{
    [XamlCompilation(XamlCompilationOptions.Skip)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        async void Handle_CloseClicked(object sender, System.EventArgs e)
        {
            await Shell.Current.Navigation.PopModalAsync();
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
