using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xappy.Content.Settings;

namespace Xappy.Content.Common
{
    public partial class FlyoutHeaderTemplate : Grid
    {
        public FlyoutHeaderTemplate()
        {
            InitializeComponent();

        }

        async void Handle_Tapped(object sender, System.EventArgs e)
        {
            await Shell.Current.Navigation.PushModalAsync(new SettingsPages(), true);
        }
    }
}
