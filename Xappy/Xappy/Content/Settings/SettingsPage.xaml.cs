using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xappy.Services;
using Xappy.Domain.Global;


namespace Xappy.Content.Settings
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        void Handle_CloseClicked(object sender, System.EventArgs e)
        {
            //awaiting this causes a crash in android
            Shell.Current.Navigation.PopModalAsync();
        }

        void Handle_Clicked_2(object sender, System.EventArgs e)
        {
            Application.Current.Resources["CurrentTheme"] = Application.Current.Resources["BaseStyle"];
        }

        void Handle_Clicked_3(object sender, System.EventArgs e)
        {
            Application.Current.Resources["CurrentTheme"] = Application.Current.Resources["SecondaryShell"];
        }

        void RadioButton_CheckedChanged(System.Object sender, Xamarin.Forms.CheckedChangedEventArgs e)
        {
            OSAppTheme val = (OSAppTheme)((RadioButton)sender).Value;
            if (App.Current.UserAppTheme == val)
                return;

            App.Current.UserAppTheme = val;
        }
    }
}
